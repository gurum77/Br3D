using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionCircle : ActionBase
    {
        public enum Method
        {
            none,
            centerRadius,
            threePoints,
            twoPoints
        }
        Method method = Method.centerRadius;
        public Method defaultMethod = Method.none;  // 기본 method가 있는 경우라면 command 입력시 옵션을 입력받지 않는다.
        protected Point3D centerPoint, radiusPoint, firstPoint, secondPoint, thirdPoint;

        void InitPoints()
        {
            method = Method.centerRadius;
            centerPoint = null;
            radiusPoint = null;
            firstPoint = null;
            secondPoint = null;
            thirdPoint = null;
        }
        public ActionCircle(Environment environment, Method defaultMethod=Method.none) : base(environment)
        {
            this.defaultMethod = defaultMethod;
            InitPoints();
        }

        public override async void Run()
        { await RunAsync(); }

        protected override void OnMouseMove(Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);

            Entity circle = null;
            if (method == Method.centerRadius)
            {
                if (centerPoint == null)
                    return;

                radiusPoint = point3D;
            }
            else if(method == Method.threePoints)
            {
                // 첫번째 점일때는 선으로 그린다.
                if(firstPoint != null && secondPoint == null)
                {
                    var line = new Line(firstPoint, point3D);
                    GetHModel()?.entityPropertiesManager?.SetDefaultProperties(line, true);
                    environment.TempEntities.ReplaceEntityAndRegen(line);
                    return;
                }
                else if(firstPoint != null && secondPoint != null)
                {
                    thirdPoint = point3D;
                }
                else
                {
                    return;
                }
                

                
            }
            else if(method == Method.twoPoints)
            {
                if (firstPoint == null)
                    return;

                secondPoint = point3D;
            }

            circle = MakeEntity(true);
            if (circle == null)
                return;
            environment.TempEntities.ReplaceEntityAndRegen(circle);
        }

        virtual protected Entity MakeEntity(bool tempEntity = false)
        {
            Circle circle = null;

            if (method == Method.centerRadius)
            {
                if (centerPoint == null || radiusPoint == null)
                    return null;
                var radius = centerPoint.DistanceTo(radiusPoint);
                if (radius <= Define.MinimumRadius)
                    return null;

                circle = new Circle(GetWorkplane(), centerPoint, radius);
            }
            else if (method == Method.threePoints)
            {
                if (firstPoint == null || secondPoint == null || thirdPoint == null)
                    return null;
                var plane = new Plane(firstPoint, secondPoint, thirdPoint);
                if (!plane.IsValid())
                    return null;

                circle = new Circle(firstPoint, secondPoint, thirdPoint);
            }
            else if(method == Method.twoPoints)
            {
                if (firstPoint == null || secondPoint == null)
                    return null;
                var radius = firstPoint.DistanceTo(secondPoint) / 2;
                if (radius <= Define.MinimumRadius)
                    return null;

                circle = new Circle(GetWorkplane(), (firstPoint + secondPoint)/2, radius);
            }

            if (circle == null)
                return null;

            GetHModel()?.entityPropertiesManager?.SetDefaultProperties(circle, tempEntity);

            return circle;
        }

        public async Task<bool> RunAsync()
        {
            StartAction();

            
            while (!IsCanceled())
            {
                InitPoints();

                KeyValuePair<Point3D, KeyEventArgs> pk = new KeyValuePair<Point3D, KeyEventArgs>(null, null);

                if(defaultMethod == Method.none)
                {
                    pk = await GetPoint3DOrKey(LanguageHelper.Tr("Center point(3:3 Points, 2:2 Points)"), -1, new KeyEventArgs(Keys.D3), new KeyEventArgs(Keys.D2));
                }
                else if(defaultMethod == Method.centerRadius)
                {
                    pk = await GetPoint3DOrKey(LanguageHelper.Tr("Center point"));
                }
               
                if (IsCanceled() || IsEntered())
                    break;

                if (defaultMethod == Method.threePoints || (pk.Value != null && pk.Value.KeyData == Keys.D3))
                {
                    method = Method.threePoints;

                    firstPoint = await GetPoint3D(LanguageHelper.Tr("First point"));
                    if (IsCanceled() || IsEntered())
                        break;
                    SetOrthoModeStartPoint(firstPoint);

                    secondPoint = await GetPoint3D(LanguageHelper.Tr("Second point"));
                    if (IsCanceled() || IsEntered())
                        break;
                    SetOrthoModeStartPoint(secondPoint);

                    thirdPoint = await GetPoint3D(LanguageHelper.Tr("Third point"));
                    if (IsCanceled() || IsEntered())
                        break;
                }
                else if(defaultMethod == Method.twoPoints || (pk.Value != null && pk.Value.KeyData == Keys.D2))
                {
                    method = Method.twoPoints;

                    firstPoint = await GetPoint3D(LanguageHelper.Tr("First point"));
                    if (IsCanceled() || IsEntered())
                        break;
                    SetOrthoModeStartPoint(firstPoint);

                    secondPoint = await GetPoint3D(LanguageHelper.Tr("Second point"));
                    if (IsCanceled() || IsEntered())
                        break;
                }
                else
                {
                    method = Method.centerRadius;
                    centerPoint = pk.Key;
                    SetOrthoModeStartPoint(centerPoint);
                    DynamicInputManager.ActiveLengthFactor(centerPoint, 1, LanguageHelper.Tr("Radius"));
                    radiusPoint = await GetPoint3D(LanguageHelper.Tr("Radius point"));
                    if (IsCanceled() || IsEntered())
                        break;
                }

                SetOrthoModeStartPoint(null);

                var circle = MakeEntity();
                if (circle != null)
                {
                    environment.Entities.Add(circle);
                    environment.TempEntities.Clear();
                    environment.Invalidate();
                }

                InitPoints();
                break;
            }

            EndAction();
            return true;
        }
    }
}
