using devDept.Eyeshot;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionRotate : ActionBase
    {
        protected Point3D fromPoint = null;
        protected Point3D toPoint = null;
        protected Point3D lastPoint = null;
        double ?baseAngle = null;
        double lastRotationAngle = 0;
        

        public ActionRotate(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        protected override void OnMouseMove(devDept.Eyeshot.Environment environment, MouseEventArgs e)
        {
            if (fromPoint == null)
                return;

            var regenParams = new RegenParams(0.001, environment);

            var rotationAngle = GetRotationAngle();
            if (rotationAngle != 0)
            {
                foreach (var ent in environment.TempEntities)
                {
                    if (lastRotationAngle != 0)
                    {
                        ent.Rotate2D(fromPoint.X, fromPoint.Y, -lastRotationAngle);
                    }
                    ent.Rotate2D(fromPoint.X, fromPoint.Y, rotationAngle);
                    ent.Regen(regenParams);
                }

                ActionBase.cursorText = $"∠ = {rotationAngle.ToDegree()}°";
                lastRotationAngle = rotationAngle;
            }

            lastPoint = point3D.Clone() as Point3D;

            // 미리보기
            var wp = GetWorkplane();
            var len = fromPoint.DistanceTo(lastPoint);
            var startPoint = fromPoint + wp.AxisX * len;
            if (baseAngle != null)
            {
                var vec = baseAngle.Value.ToVector();
                startPoint = fromPoint +  new Vector3D(vec.X, vec.Y, 0) * len;
            }
            PreviewLabel.PreviewAngleLabel(model, fromPoint, startPoint, lastPoint, 0);
        }

        private double GetRotationAngle()
        {
            var curAngle = (point3D - fromPoint).AsVector;
            curAngle.Normalize();
            var rotationAngle = curAngle.ToRadian();
            if (baseAngle != null)
                rotationAngle -= baseAngle.Value;
            return rotationAngle;
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            while (true)
            {
                environment.TempEntities.Clear();
                fromPoint = null;
                toPoint = null;
                lastPoint = null;

                var entities = await GetEntities(LanguageHelper.Tr("Select entities"));
                if (IsCanceled())
                    break;

                // temp entities로 설정
                entities.ToTempEntities(GetModel());
                CalcBaseAngle();

                var pointOrKey = await GetPoint3DOrKey(LanguageHelper.Tr("From point(R : Reference)"));
                if (IsCanceled())
                    break;

                // 참조인 경우
                if (pointOrKey.Key == null)
                {
                    var refPt1 = await GetPoint3D(LanguageHelper.Tr("From reference point"));
                    if (IsCanceled())
                        break;
                    var refPt2 = await GetPoint3D(LanguageHelper.Tr("From reference point"));
                    if (IsCanceled())
                        break;

                    baseAngle = (refPt2 - refPt1).AsVector.ToRadian();

                    // 다시 from point 입력
                    fromPoint = await GetPoint3D(LanguageHelper.Tr("From point"));
                    if (IsCanceled())
                        break;
                }
                else
                {
                    fromPoint = pointOrKey.Key;
                }

                if (fromPoint == null)
                    break;


                lastPoint = fromPoint.Clone() as Point3D;

                // 회전 각도
                if (baseAngle == 0)
                {
                    DynamicInputManager.ActiveLengthAndAngle(fromPoint, null, 10000);
                }
                toPoint = await GetPoint3D(LanguageHelper.Tr("Angle"));
                if (IsCanceled())
                    break;

                Finish(entities);

                break;
            }

            EndAction();
            return true;
        }

        private void Finish(List<devDept.Eyeshot.Entities.Entity> entities)
        {
            var angle = GetRotationAngle();
            if (angle == 0)
                return;

            var entityList = new EntityList();
            entityList.AddRange(entities);
            entityList.Rotate(angle, Vector3D.AxisZ, fromPoint);

            GetModel().Entities.Regen();
            GetModel().Invalidate();

        }

        void CalcBaseAngle()
        {
            baseAngle = 0;
        }
    }
}
