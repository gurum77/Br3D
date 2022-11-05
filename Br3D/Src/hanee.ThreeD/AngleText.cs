using devDept.Eyeshot;
using devDept.Geometry;
using devDept.Graphics;
using System.Collections.Generic;
using System.Drawing;

namespace hanee.ThreeD
{
    public class AngleText : LeaderAndTextAndBox
    {
        public Point3D basePoint, startPoint, endPoint;
        public bool drawLine = true;
        devDept.Eyeshot.Model model;

        public AngleText(Model model, Point3D basePoint, Point3D startPoint, Point3D endPoint, string text, Font textFont, Color textColor, Vector2D offset) : base(basePoint, text, textFont, textColor, offset)
        {
            this.model = model;
            this.basePoint = basePoint;
            this.startPoint = startPoint;
            this.endPoint = endPoint;
        }


        public override void Draw(RenderContextBase renderContext)
        {
            base.Draw(renderContext);

            if (drawLine)
            {
                for (int stt = 0; stt < 2; ++stt)
                {
                    var points = new List<Point3D>();
                    if (stt == 0)
                    {
                        if (basePoint.Equals(startPoint))
                            continue;
                        points.Add(basePoint);
                        points.Add(startPoint);
                    }
                    else
                    {
                        if (basePoint.Equals(endPoint))
                            continue;
                        points.Add(basePoint);
                        points.Add(endPoint);
                    }

                    points = points.ConvertAll<Point3D>(x => model.WorldToScreen(x));

                    Color[] colors = new Color[] { Color.Yellow, Color.Yellow };
                    renderContext.DrawLines(points.ToArray(), colors);
                }
            }
        }
    }
}
