using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Text;

namespace hanee.Geometry
{
    public class HeadValue
    {
        public HeadValue(char head, double value)
        {
            this.head = head;
            this.value = value;
        }
        public char head;
        public double value;
    }

    public static class StringHelper
    {
     
        // string을 공백 또는 ,로 구분했을때 숫자만 추출
        public static List<HeadValue> ToDoubles(this string str, params char[] heads)
        {
            char[] tokens = { ' ', ',' , '=', '\n', '\t'};
            var texts = str.Split(tokens, StringSplitOptions.RemoveEmptyEntries);

            var values = new List<HeadValue>();
            const char emptyHead = ' ';
            char head = emptyHead;
            foreach (var text in texts)
            {
                // 헤더인지?
                if (text.Length == 1)
                {
                    bool isHead = false;
                    var ch = text.ToLower()[0];
                    foreach (var h in heads)
                    {
                        if (ch == h)
                        {
                            head = ch;
                            isHead = true;
                            break;
                        }
                    }
                    if (isHead)
                        continue;
                }

                // head가 없으면 숫자인지 보지도 말자.
                if (head == emptyHead)
                    continue;
                
                // head가 아니면 숫자인지?
                if (double.TryParse(text, out double val))
                {
                    values.Add(new HeadValue(head, val));
                    head = emptyHead;
                }
            }

            return values;
        }

        // string을 point3D list로 변환
        public static List<Point3D> ToPoint3Ds(this string str)
        {
            var values = str.ToDoubles('x', 'y', 'z');
            if (values == null || values.Count== 0)
                return null;

            var points = new List<Point3D>();
            while(values.Count > 0)
            {
                var point = new Point3D();
                var x = values.Find(val => val.head == 'x');
                var y = values.Find(val => val.head == 'y');
                var z = values.Find(val => val.head == 'z');
                if (x != null)
                {
                    point.X = x.value;
                    values.Remove(x);
                }

                if(y != null)
                {
                    point.Y = y.value;
                    values.Remove(y);
                }
                
                if (z != null)
                {
                    point.Z = z.value;
                    values.Remove(z);
                }

                points.Add(point);
                if (x == null && y == null && z == null)
                    break;

            }
          
            return points;
        }


        // string을 Point3D로 변환
        public static Point3D ToPoint3D(this string str)
        {
            var values = str.ToDoubles('x', 'y', 'z');
            if (values == null || values.Count == 0)
                return null;

            var point = new Point3D();
            var x = values.Find(v => v.head == 'x');
            var y = values.Find(v => v.head == 'y');
            var z = values.Find(v => v.head == 'z');
            if (x != null)
                point.X = x.value;
            if (y != null)
                point.Y = y.value;
            if (z != null)
                point.Z = z.value;


            return point;
        }

        public static double ToDouble(this string str)
        {
            if (double.TryParse(str, out double val))
                return val;
            return 0;
        }
        public static bool ToBool(this string str)
        {
            if (bool.TryParse(str, out bool val))
                return val;
            return false;
        }

        public static int ToInt(this string str)
        {
            if (int.TryParse(str, out int val))
                return val;
            return 0;
        }

        // 문자열을 count 횟수만큼 반복해서 리턴
        public static string Mutiple(this string str, int count)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < count; ++i)
                sb.Append(str);
            return sb.ToString();
        }
    }
}
