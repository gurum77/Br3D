using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionSection : ActionBase
    {
        Point3D origin, xPoint, yPoint;

        public ActionSection(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        protected override void OnMouseMove(devDept.Eyeshot.Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);

            environment.TempEntities.Clear();

            // 미리보기
            if (origin != null && (xPoint == null || yPoint == null))
            {
                var ent = ActionAlign.MakeAxis(origin, point3D, xPoint == null ? Define.AxisXColor : Define.AxisYColor);
                if (ent != null)
                    environment.TempEntities.Add(ent);
            }


            // x 축 그리기
            if (origin != null && xPoint != null)
            {
                var ent = ActionAlign.MakeAxis(origin, xPoint, Define.AxisXColor);
                if (ent != null)
                    environment.TempEntities.Add(ent);
            }

            // y 축 그리기
            if (origin != null && yPoint != null)
            {
                var ent = ActionAlign.MakeAxis(origin, yPoint, Define.AxisYColor);
                if (ent != null)
                    environment.TempEntities.Add(ent);
            }
        }

        public async Task<bool> RunAsync()
        {
            StartAction();

            while (true)
            {
                // 객체 선택
                var entities = await GetEntities(LanguageHelper.Tr("Select entities"));
                if (IsCanceled() || entities.Count == 0)
                    break;

                // 세 점 지정
                origin = await GetPoint3D(LanguageHelper.Tr("Origin point for cutting plane"));
                if (IsCanceled() || IsEntered())
                    break;

                xPoint = await GetPoint3D(LanguageHelper.Tr("X-Axis point for cutting plane"));
                if (IsCanceled() || IsEntered())
                    break;


                yPoint = await GetPoint3D(LanguageHelper.Tr("Y-Axis point for cutting plane"));
                if (IsCanceled() || IsEntered())
                    break;


                if (origin == null || xPoint == null || yPoint == null)
                    break;

                var plane = new Plane(origin, (xPoint - origin).AsVector, (yPoint - origin).AsVector);
                if (!plane.IsValid())
                    break;

                var tol = 0.001;
                var curves = new List<ICurve>();

                // 잘라내기
                var faces = GetAllFaces(entities);
                if (faces != null)
                {
                    foreach (var face in faces)
                    {

                        var tmp = face.Section(plane, tol);
                        if (tmp == null || tmp.Count() == 0)
                            continue;

                        curves.AddRange(tmp);
                    }
                }


                // 그릴 위치, 두께 입력
                Point3D targetPoint = null;
                var lineWidth = 0.5;
                while (true)
                {
                    var pk = await GetPoint3DOrText(LanguageHelper.Tr("Point to place section(Enter : place at origin, W : line weight"), -1, "w");
                    if (IsCanceled() || IsEntered())
                        break;

                    // 선 두께 입력인지?
                    if (pk.Key == null && pk.Value.EqualsIgnoreCase("w"))
                    {
                        lineWidth = await GetDist(LanguageHelper.Tr("Line weight"));
                        if (IsCanceled() || IsEntered())
                            break;
                    }
                    else if (pk.Key != null)
                    {
                        targetPoint = pk.Key;
                        break;
                    }
                }

                entities.Clear();
                entities = curves.ConvertAll<Entity>(x => x as Entity);
                
                // targetPoint가 있으면 평면으로 변환
                if (targetPoint != null)
                {
                    var targetPlane = new Plane(targetPoint, Vector3D.AxisX, Vector3D.AxisY);
                    var trans = new Transformation();
                    trans.Rotation(plane, targetPlane);

                    foreach (var ent in entities)
                    {
                        ent.TransformBy(trans);
                    }
                }

                // 선 두께 반영
                foreach (var ent in entities)
                {
                    ent.LineWeight = (float)lineWidth;
                    ent.LineWeightMethod = colorMethodType.byEntity;
                }

                // 객체 추가
                AddEntities(entities.ToArray());

                break;
            }

            EndAction();
            return true;
        }

        // face를 모두 추출
        private List<IFace> GetAllFaces(List<Entity> entities)
        {
            var faces = new List<IFace>();
            foreach (var ent in entities)
            {
                if (ent is IFace face)
                {
                    faces.Add(face);
                }
                else if (ent is BlockReference br)
                {
                    var curEntities = br.GetEntities(model.Blocks);
                    if (curEntities == null)
                        continue;

                    var trans = br.GetFullTransformation(model.Blocks);
                    var cloneEntities = new List<Entity>();
                    foreach (var ce in curEntities)
                    {
                        var cloneEnt = ce.Clone() as Entity;
                        cloneEnt.TransformBy(trans);
                        cloneEntities.Add(cloneEnt);
                    }

                    var curFaces = GetAllFaces(cloneEntities);
                    if (curFaces == null)
                        continue;

                    faces.AddRange(curFaces);
                }
            }

            return faces;
        }
    }
}
