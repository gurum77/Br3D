using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionAlign : ActionBase
    {
        Point3D firstSourcePoint = null;
        Point3D secondSourcePoint = null;
        Point3D thirdSourcePoint = null;

        Point3D firstDestPoint = null;
        Point3D secondDestPoint = null;
        Point3D thirdDestPoint = null;

        
        public ActionAlign(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        protected override void OnMouseMove(Environment environment, MouseEventArgs e)
        {
            var alpha = 100;
            EntityList entities = new EntityList();
            if (firstSourcePoint != null)
            {
                // source x 축 그리기
                var l = new Line(firstSourcePoint, secondSourcePoint == null ? point3D : secondSourcePoint);
                l.LineWeight = 3;
                l.Color = Color.FromArgb(alpha, Define.AxisXColor);
                l.ColorMethod = colorMethodType.byEntity;
                entities.Add(l);
            }

            // 목적지 점 입력 전인 경우
            if (firstDestPoint == null)
            {
                // source y 축 그리기
                if (secondSourcePoint != null)
                {
                    var l = new Line(firstSourcePoint, thirdSourcePoint == null ? point3D : thirdSourcePoint);
                    l.LineWeight = 3;
                    l.Color = Color.FromArgb(alpha, Define.AxisYColor);
                    l.ColorMethod = colorMethodType.byEntity;
                    entities.Add(l);
                }
            }

            // 목적지 점 입력중인경우
            // dest x 축 그리기
            if (firstDestPoint != null)
            {
                
                var l = new Line(firstDestPoint, secondDestPoint == null ? point3D : secondDestPoint);
                l.LineWeight = 3;
                l.Color = Color.FromArgb(alpha, Define.AxisXColor);
                l.ColorMethod = colorMethodType.byEntity;
                entities.Add(l);
            }

            // dest y축 그리기
            if (secondDestPoint != null)
            {
                var l = new Line(firstDestPoint, thirdDestPoint == null ? point3D : thirdDestPoint);
                l.LineWeight = 3;
                l.Color = Color.FromArgb(alpha, Define.AxisYColor);
                l.ColorMethod = colorMethodType.byEntity;
                entities.Add(l);
            }
            

            environment.TempEntities.Clear();
            environment.TempEntities.AddRange(entities);
        }

        void InitPoints()
        {
            firstSourcePoint = null;
            secondSourcePoint = null;
            thirdSourcePoint = null;

            firstDestPoint = null;
            secondDestPoint = null;
            thirdDestPoint = null;
        }
        public async Task<bool> RunAsync()
        {
            InitPoints();

            StartAction();

            var entities = new List<Entity>();
            
            // 객체 선택 / source point
            while (true)
            {
                entities = await GetEntities(LanguageHelper.Tr("Select entities"));

                firstSourcePoint = await GetPoint3D(LanguageHelper.Tr("First source point"));
                if (IsCanceled() || IsEntered())
                {
                    firstSourcePoint = null;
                    break;
                }

                secondSourcePoint = await GetPoint3D(LanguageHelper.Tr("Second source point"));
                if (IsCanceled() || IsEntered())
                {
                    secondSourcePoint = null;
                    break;
                }

                thirdSourcePoint = await GetPoint3D(LanguageHelper.Tr("Third source point"));
                if (IsCanceled() || IsEntered())
                {
                    thirdSourcePoint = null;
                }

                break;
            }

            while (true)
            {
                firstDestPoint = await GetPoint3D(LanguageHelper.Tr("First destination point"));
                if (IsCanceled() || IsEntered())
                    break;

                if (secondSourcePoint == null)
                    break;
                secondDestPoint = await GetPoint3D(LanguageHelper.Tr("Second destination point"));
                if (IsCanceled() || IsEntered())
                    break;

                if (thirdSourcePoint == null)
                    break;
                thirdDestPoint = await GetPoint3D(LanguageHelper.Tr("Third destination point"));
                if (IsCanceled() || IsEntered())
                    break;

                break;
            }

            // 점 하나만 찍은 경우에는 translate
            if (firstSourcePoint != null && secondSourcePoint == null && thirdSourcePoint == null && firstDestPoint != null)
            {
                var vec = (firstDestPoint - firstSourcePoint).AsVector;
                foreach (var ent in entities)
                {
                    ent.Translate(vec);
                }
            }
            else if(firstSourcePoint != null && secondSourcePoint != null && firstDestPoint != null && secondDestPoint != null)
            {
                Plane sourcePlane, destinationPlane;
                if (thirdSourcePoint == null || thirdDestPoint == null  )
                {
                    sourcePlane = new Plane(firstSourcePoint, (secondSourcePoint - firstSourcePoint).AsVector);
                    destinationPlane = new Plane(firstDestPoint, (secondDestPoint - firstDestPoint).AsVector);
                }
                else
                {
                    sourcePlane = new Plane(firstSourcePoint, (secondSourcePoint - firstSourcePoint).AsVector, (thirdSourcePoint - firstSourcePoint).AsVector);
                    destinationPlane = new Plane(firstDestPoint, (secondDestPoint - firstDestPoint).AsVector, (thirdDestPoint - firstDestPoint).AsVector);
                }

                var trans = new Transformation();
                trans.Rotation(sourcePlane, destinationPlane);
                foreach (var ent in entities)
                {
                    ent.TransformBy(trans);
                }
            }

            environment.Entities.Regen(new devDept.Eyeshot.RegenOptions());
            environment.Invalidate();

            EndAction();
            return true;
        }
    }
}
