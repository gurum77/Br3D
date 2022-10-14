using devDept.Eyeshot;
using devDept.Geometry;
using System.Drawing;

namespace hanee.ThreeD
{
    static public class LegendColorHelper
    {
        public static Color GetColorByValue(this Legend legend, double value, bool interpolatedColor = false)
        {
            int ColorTableLen = legend.ColorTable.Length;
            for (int c = 0; c < ColorTableLen; c++)
            {
                if (value <= legend.Values[c + 1])
                {
                    // 보간된 색상 리턴
                    if (interpolatedColor && c > 0)
                    {
                        var min = legend.Values[c];
                        var max = legend.Values[c + 1];
                        var curDiff = value - min;
                        var factor = curDiff / (max - min);
                        var topColor = legend.ColorTable[c];
                        var bottomColor = legend.ColorTable[c - 1];
                        int r = (int)((topColor.R - bottomColor.R) * factor);
                        int g = (int)((topColor.G - bottomColor.G) * factor);
                        int b = (int)((topColor.B - bottomColor.B) * factor);
                        r = bottomColor.R + r;
                        g = bottomColor.G + g;
                        b = bottomColor.B + b;

                        Utility.LimitRange<int>(0, ref r, 255);
                        Utility.LimitRange<int>(0, ref g, 255);
                        Utility.LimitRange<int>(0, ref b, 255);
                        return Color.FromArgb(r, g, b);
                    }
                    else
                        return Color.FromArgb(legend.ColorTable[c].R, legend.ColorTable[c].G, legend.ColorTable[c].B);
                }
            }

            return Color.White;
        }
    }
}
