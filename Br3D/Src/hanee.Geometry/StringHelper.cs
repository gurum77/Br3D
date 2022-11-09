using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Text;

namespace hanee.Geometry
{
    public class HeadValue
    {
        public HeadValue(char head, object value)
        {
            this.head = head;
            this.value = value;
        }
        
        public double GetDouble()
        {
            return (double)value;
        }

        public string GetString()
        {
            return value.ToString();
        }

        public char head;
        public object value;
    }

    public static class StringHelper
    {
        // string 비교
        public static bool EqualsIgnoreCase(this string str, string otherStr)
        {
            return str.Equals(otherStr, StringComparison.OrdinalIgnoreCase);
        }

     
        public static List<HeadValue> ToStrings(this string str, params char[] heads)
        {
            char[] tokens = { ' ', ',', '=', '\n', '\t' };
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

                values.Add(new HeadValue(head, text));
                head = emptyHead;
            }

            return values;
        }

        // token 들 앞뒤로 공백을 만들어 준다.(그래야 ToDoubles에서 값을 분리해서 인식할 수 있음)
        public static string MakeHeadSpace(this string str, params char[] tokens)
        {
            var str2 = str.Clone() as string;
            foreach (var token in tokens)
            {
                str2 = str2.Replace(token.ToString(), $" {token} ");
            }


            return str2;
        }

        // string을 공백 또는 ,로 구분했을때 숫자만 추출
        public static List<HeadValue> ToDoubles(this string str, params char[] heads)
        {
            var strings = str.ToStrings(heads);
            var doubles = new List<HeadValue>();
            foreach(var curStr in strings)
            {
                if(double.TryParse(curStr.value.ToString(), out double result))
                {
                    curStr.value = result;
                    doubles.Add(curStr);
                }
            }

            return doubles;
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
                    point.X = x.GetDouble();
                    values.Remove(x);
                }

                if(y != null)
                {
                    point.Y = y.GetDouble();
                    values.Remove(y);
                }
                
                if (z != null)
                {
                    point.Z = z.GetDouble();
                    values.Remove(z);
                }

                points.Add(point);
                if (x == null && y == null && z == null)
                    break;

            }
          
            return points;
        }

        // token으로 분리된 문자를 Point3D로 변환
        public static Point3D ToPoint3DByToken(this string str, char token=' ')
        {
            var values = str.Split(token);
            if (values == null || values.Length < 2)
                return null;

            var pt = new Point3D(values[0].ToDouble(), values[1].ToDouble(), 0);
            if(values.Length > 2)
                pt.Z = values[2].ToDouble();
            return pt;
        }

        // 공백으로 분리된 문자를 Point3D로 변환
        public static Point3D ToPoint3DBySpace(this string str)
        {
            return str.ToPoint3DByToken(' ');
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
                point.X = x.GetDouble();
            if (y != null)
                point.Y = y.GetDouble();
            if (z != null)
                point.Z = z.GetDouble();


            return point;
        }

        public static double ToDouble(this string str)
        {
            if (double.TryParse(str, out double val))
                return val;
            return 0;
        }

        public static float ToFloat(this string str)
        {
            if (float.TryParse(str, out float val))
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
