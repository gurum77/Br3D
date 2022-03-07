using devDept.Eyeshot;
using devDept.Geometry;
using devDept.Graphics;
using System.Drawing;

namespace hanee.Cad.Tool
{
    public class DistanceText : LeaderAndTextAndBox
    {
        Point3D pt1, pt2;
        devDept.Eyeshot.Model model;

        //public DistanceText(Model model, Point3D pt1, Point3D pt2, string text, Font textFont, Color textColor) : base((pt1+pt2)/2, text, textFont, textColor, ContentAlignment.MiddleCenter)
        public DistanceText(Model model, Point3D pt1, Point3D pt2, string text, Font textFont, Color textColor, Vector2D offset) : base((pt1 + pt2) / 2, text, textFont, textColor, offset)
        {
            this.model = model;
            this.pt1 = pt1;
            this.pt2 = pt2;
        }

        public override void Draw(RenderContextBase renderContext)
        {
            Point3D[] points = new Point3D[2] { pt1, pt2 };
            points[0] = model.WorldToScreen(pt1);
            points[1] = model.WorldToScreen(pt2);

            Color[] colors = new Color[] { Color.Yellow, Color.Yellow };
            renderContext.DrawLines(points, colors);

            base.Draw(renderContext);
        }
    }
}
