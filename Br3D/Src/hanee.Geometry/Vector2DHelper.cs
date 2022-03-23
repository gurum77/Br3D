using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Text;

namespace hanee.Geometry
{
    static public class Vector2DHelper
    {
        static public void RotateByDegree(this Vector2D vec, double degree)
        {
            double ang = Utility.RadToDeg(vec.Angle) + degree;
            vec.FromDegree(ang);
        }

        static public Vector3D To3D(this Vector2D vec)
        {
            return new Vector3D(vec.X, vec.Y, 0);
        }


        // degree를 Vector2D로 설정한다.
        public static void FromDegree(this Vector2D vec, double degree)
        {
            double rad = Utility.DegToRad(degree);
            vec.X = Math.Cos(rad);
            vec.Y = Math.Sin(rad);
        }

        public static double ToDegree(this Vector2D vec)
        {
            return vec.ToRadian().ToDegree();
        }
        /// <summary>
        /// vector2d 를 radian으로 리턴
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static double ToRadian(this Vector2D vec)
        {
            double length = vec.Length;
            if (length == 0) return 0;

            double res = Math.Acos(vec.X / length);
            return vec.Y >= 0.0 ? res : 2 * Math.PI - res;
        }
    }
}
