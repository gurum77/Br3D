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

        enum Step
        {
            entitis,
            firstSourcePoint,
            secondSourcePoint,
            thirdSourcePoint,
            firstDestPoint,
            secondDestPoint,
            thirdDestPoint

        }
        
        public ActionAlign(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        protected override void OnMouseMove(Environment environment, MouseEventArgs e)
        {
            EntityList entities = new EntityList();

            // 입력이 완료된 걸 그린다.
            // source x 축 그리기
            if (firstSourcePoint != null && secondSourcePoint != null)
                entities.Add(MakeAxis(firstSourcePoint, secondSourcePoint, Define.AxisXColor));

            // source y 축 그리기
            if(firstSourcePoint != null && thirdSourcePoint != null)
                entities.Add(MakeAxis(firstSourcePoint, thirdSourcePoint, Define.AxisYColor));

            // dest x 축 그리기
            if(firstDestPoint != null && secondDestPoint != null)
                entities.Add(MakeAxis(firstDestPoint, secondDestPoint, Define.AxisXColor));
            
            // dest y 축 그리기
            if (firstDestPoint != null && thirdDestPoint != null)
                entities.Add(MakeAxis(firstDestPoint, thirdDestPoint, Define.AxisYColor));

            // 이동 위치 그리기
            if(firstSourcePoint != null && firstDestPoint != null)
                entities.Add(MakeAxis(firstSourcePoint, firstDestPoint, Define.translateColor));


            // 입력중인 선(축) 그리기
            var step = (Step)ActionBase.StepID;
            if(step == Step.secondSourcePoint || step == Step.thirdSourcePoint)
                entities.Add(MakeAxis(firstSourcePoint, point3D, step == Step.secondSourcePoint ? Define.AxisXColor : Define.AxisYColor));
            else if (step == Step.secondDestPoint || step == Step.thirdDestPoint)
                entities.Add(MakeAxis(firstDestPoint, point3D, step == Step.secondDestPoint ? Define.AxisXColor : Define.AxisYColor));
            else if(step == Step.firstDestPoint)
                entities.Add(MakeAxis(firstSourcePoint, point3D, Define.translateColor));



            environment.TempEntities.Clear();
            environment.TempEntities.AddRange(entities);
        }

        // 축을 그린다.
        static public Entity MakeAxis(Point3D pt1, Point3D pt2, Color color)
        {
            var alpha = 100;
            var l = new Line(pt1, pt2);
            l.LineWeight = 3;
            l.Color = Color.FromArgb(alpha, color);
            l.ColorMethod = colorMethodType.byEntity;
            return l;
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
                entities = await GetEntities(LanguageHelper.Tr("Select entities"), (int)Step.entitis);

                firstSourcePoint = await GetPoint3D(LanguageHelper.Tr("First source point"), (int)Step.firstSourcePoint);
                if (IsCanceled() || IsEntered())
                {
                    firstSourcePoint = null;
                    break;
                }

                secondSourcePoint = await GetPoint3D(LanguageHelper.Tr("Second source point"), (int)Step.secondSourcePoint);
                if (IsCanceled() || IsEntered())
                {
                    secondSourcePoint = null;
                    break;
                }

                thirdSourcePoint = await GetPoint3D(LanguageHelper.Tr("Third source point"), (int)Step.thirdSourcePoint);
                if (IsCanceled() || IsEntered())
                {
                    thirdSourcePoint = null;
                }

                break;
            }

            while (true)
            {
                firstDestPoint = await GetPoint3D(LanguageHelper.Tr("First destination point"), (int)Step.firstDestPoint);
                if (IsCanceled() || IsEntered())
                    break;

                if (secondSourcePoint == null)
                    break;
                secondDestPoint = await GetPoint3D(LanguageHelper.Tr("Second destination point"), (int)Step.secondDestPoint);
                if (IsCanceled() || IsEntered())
                    break;

                if (thirdSourcePoint == null)
                    break;
                thirdDestPoint = await GetPoint3D(LanguageHelper.Tr("Third destination point"), (int)Step.thirdDestPoint);
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
