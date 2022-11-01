using devDept.Eyeshot.Labels;
using devDept.Geometry;
using devDept.Graphics;
using System.Drawing;

namespace hanee.ThreeD
{
    public class LeaderAndTextAndBox : LeaderAndText
    {
        public LeaderAndTextAndBox(Point3D p, string text, Font textFont, Color textColor, Vector2D offset) : base(p, text, textFont, textColor, offset)
        {

        }

        public override void Draw(RenderContextBase renderContext)
        {
            base.Draw(renderContext);

            //// 외곽선
            //RectangleF boundary = new RectangleF((float)OnScreenPosition.X, (float)OnScreenPosition.Y, Size.Width, Size.Height);
            //if (Alignment == ContentAlignment.MiddleCenter)
            //{
            //    boundary.X -= Size.Width / 2;
            //    boundary.Y -= Size.Height / 2;
            //}

            //Point2D[] points = new Point2D[5];
            //points[0] = new Point2D(boundary.Left, boundary.Bottom);
            //points[1] = new Point2D(boundary.Right, boundary.Bottom);
            //points[2] = new Point2D(boundary.Right, boundary.Top);
            //points[3] = new Point2D(boundary.Left, boundary.Top);
            //points[4] = new Point2D(boundary.Left, boundary.Bottom);

            //renderContext.DrawLineStrip(points);


            //// 화살표
            //Vector2D direction = Offset.Clone() as Vector2D;
            //direction.Normalize();

            //Vector2D directionForWidth = direction.Clone() as Vector2D;
            //Transformation xform = new Transformation();
            //xform.Rotation(Utility.DegToRad(90), new Vector3D(0, 0, 1));
            //directionForWidth.TransformBy(xform);

            //double arrowLength = 20;
            //double arrowWidth = 13;

            //Point2D[] vertices = new Point2D[4];
            //vertices[0] = new Point2D(OnScreenPosition.X, OnScreenPosition.Y);
            //vertices[0] -= Offset;
            //vertices[1] = vertices[0] + direction * arrowLength;
            //vertices[1] = vertices[1] + directionForWidth * (-arrowWidth / 2.0);
            //vertices[2] = vertices[1] + directionForWidth * arrowWidth;
            //vertices[3] = vertices[0].Clone() as Point2D;
            //renderContext.DrawTriangles2D(vertices);
        }
    }
}
