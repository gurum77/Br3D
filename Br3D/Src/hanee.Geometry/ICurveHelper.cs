using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System.Collections.Generic;

namespace hanee.Geometry
{
    static public class ICurveHelper
    {
        // curve의 가운데 점
        static public Point3D GetMidPoint(this ICurve curve)
        {
            if (!curve.GetParamFromLength(curve.Length() / 2, out double t))
                return curve.StartPoint.Clone() as Point3D;

            return curve.PointAt(t);
        }

        // curve와 객체여러개와의 교점을 parameter로 리턴
        static public List<double> IntersectWith(this ICurve curve, List<Entity> entities)
        {
            var matchParams = new List<double>();
            foreach (var ent in entities)
            {
                if (ent == curve)
                    continue;

                var curCurve = ent as ICurve;
                if (curCurve == null)
                    continue;
                var tmp = curve.IntersectWith(curCurve);
                if (tmp == null)
                    continue;

                foreach (var mp in tmp)
                {
                    var ip = mp as InterPoint;
                    if (ip == null)
                    {
                        continue;
                    }
                    matchParams.Add(ip.u);
                }
            }

            // 교점을 정렬
            matchParams.Sort();

            return matchParams;
        }
    }
}
