using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionFillet : ActionBase
    {
        double radius = 1.0;
        public ActionFillet(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            var entityTypes = new Dictionary<Type, bool>();
            entityTypes.Add(typeof(ICurve), true);
            var regenOptions = new RegenOptions();
            while (true)
            {
                Entity firstEntity = null;

                // 첫번째 객체 선택 / 옵션
                // 객체를 선택할때까지 반복
                while (true)
                {
                    var firstEntityOrKey = await GetEntityOrKey(LanguageHelper.Tr("Select first curve") + $"(Radius : {radius}, R : Input radius)", -1, true, entityTypes);
                    if (IsCanceled())
                        break;

                    // 반지름 입력
                    if (firstEntityOrKey.Key == null && firstEntityOrKey.Value.KeyCode == Keys.R)
                    {
                        FormInputMessage form = new FormInputMessage(LanguageHelper.Tr("Radius"));
                        if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            radius = form.RichTextBox.Text.ToString().ToDouble();
                    }
                    else
                    {
                        firstEntity = firstEntityOrKey.Key;
                    }

                    if (firstEntity != null)
                        break;
                }

                if (firstEntity == null)
                    break;
                

                var firstPoint = ActionBase.GetPoint3DByMouseLocation(environment, ActionBase.currentMousePoint);

                var secondEntity = await GetEntity(LanguageHelper.Tr("Select second curve"), -1, true, entityTypes);
                if (IsCanceled())
                    break;
                var secondPoint = ActionBase.GetPoint3DByMouseLocation(environment, ActionBase.currentMousePoint);

                var firstCurve = firstEntity as ICurve;
                var secondCurve = secondEntity as ICurve;
                if (firstCurve == null || secondCurve == null || firstPoint == null || secondPoint == null)
                    continue;

                firstCurve.ClosestPointTo(firstPoint, out double firstParam);
                secondCurve.ClosestPointTo(secondPoint, out double secondParam);

                // 클릭한 지점을 curve상으로 조정
                firstPoint = firstCurve.PointAt(firstParam);
                secondPoint = secondCurve.PointAt(secondParam);

                var firstDir = firstCurve.TangentAt(firstParam);
                var secondDir = secondCurve.TangentAt(secondParam);

                // 교점에 따라 flip 방향이 달라짐
                // 첫번째 선은 교점 방향으로 진행해야 하고
                // 두번째 선은 교점 반대 방향으로 진행해야 한다.
                var matchPoint = firstPoint.IntersectionWith(firstDir, secondPoint, secondDir);
                if (matchPoint == null)
                    continue;

                var dir = (matchPoint - firstPoint).ToDir();
                var flip1 = !dir.Equals(firstDir);
                dir = (secondPoint - matchPoint).ToDir();
                var flip2 = !dir.Equals(secondDir);

                try
                {
                    if (Curve.Fillet(firstCurve, secondCurve, radius, flip1, flip2, true, true, out Arc filletArc))
                    {
                        filletArc.CopyAttributes(firstEntity);

                        GetModel().Entities.Add(filletArc);
                        GetModel().Entities.RegenAllCurved(regenOptions);

                        GetModel().Invalidate();
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                }

            }

            EndAction();
            return true;
        }
    }
}
