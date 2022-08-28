using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System.Drawing;
using Region = devDept.Eyeshot.Entities.Region;

namespace hanee.Geometry
{
    // entity helper
    static public class EntityHelper
    {
        // 실제로 사용된 색 리턴
        public static Color GetUsedColor(this Entity ent, Model model)
        {
            if (ent.ColorMethod == colorMethodType.byEntity)
                return ent.Color;
            else if (ent.ColorMethod == colorMethodType.byLayer)
            {
                var la = model.Layers[ent.LayerName];
                return la.Color;
            }

            return ent.Color;
        }
        // entity의 제원을 복사한다.
        // 느린 함수이다.
        public static void CopyFrom(this Entity ent, Entity source)
        {
            ent.CopyAttributes(source);

            // 타입이 다르면 복사할 수 없다.
            if (ent.GetType() != source.GetType())
                return;

            if (ent is Line)
            {
                var t = (Line)ent;
                var s = (Line)source;
                t.StartPoint = s.StartPoint;
                t.EndPoint = s.EndPoint;
            }
            else if(ent is LinearPath)
            {
                var t = (LinearPath)ent;
                var s = (LinearPath)source;
                t.Vertices = s.Vertices;
            }
            else if (ent is Arc)
            {
                var t = (Arc)ent;
                var s = (Arc)source;
                t.Center.CopyFrom(s.Center);
                t.Radius = s.Radius;
                t.StartPoint.CopyFrom(s.StartPoint);
                t.EndPoint.CopyFrom(s.EndPoint);
            }
            else if (ent is Circle)
            {
                var t = (Circle)ent;
                var s = (Circle)source;
                t.Center.CopyFrom(s.Center);
                t.Radius = s.Radius;
            }
            
            else
            {
                // brep는 복사불가하다. 
                // brep는 이쪽으로 들어오지 않도록 코딩해야함
                // 다른 객체는 복사 가능한 경우 추가로 코딩한다.
                System.Diagnostics.Debug.Assert(false);
            }

        }

        public static bool IsDepthTestAlwaysEntity(this Entity ent)
        {
            if (ent is DepthTestAlwaysBlockReference)
                return true;
            else if (ent is DepthTestAlwaysLinearPath)
                return true;
            else if (ent is DepthTestAlwaysRegion)
                return true;
            else if (ent is DepthTestAlwaysText)
                return true;

            return false;

        }
        public static Entity CloneToDepthTestAlwaysEntity(this Entity ent)
        {
            if (ent is LinearPath)
            {
                return new DepthTestAlwaysLinearPath(ent as LinearPath);
            }
            else if (ent is Text)
            {
                return new DepthTestAlwaysText(ent as Text);
            }
            else if (ent is Region)
            {
                return new DepthTestAlwaysRegion(ent as Region);
            }

            return ent.Clone() as Entity;
        }

        /// <summary>
        /// 객체 2D방식으로 회전 한다.
        /// </summary>
        /// <param name="ent"></param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="angleInRadian"></param>
        public static void Rotate2D(this Entity ent, double x, double y, double angleInRadian)
        {
            ent.Rotate(angleInRadian, new Vector3D(0, 0, 1), new Point3D(x, y, 0));
        }

        // 유효한 객체인지?
        public static bool IsValid(this Entity ent)
        {
            LinearPath lp = ent as LinearPath;
            if (lp.Vertices.Length == 0)
                return false;

            return true;
        }
    }
}
