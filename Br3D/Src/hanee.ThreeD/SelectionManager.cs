using devDept.Geometry;
using devDept.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    public class SelectionManager
    {
        private enum Step
        {
            firstPoint,
            secondPoint
        };

        private HModel hModel;
        System.Drawing.Point initialLocation;
        System.Drawing.Point currentLocation;
        bool isCrossing { get => currentLocation.X < initialLocation.X; }
        Step step = Step.firstPoint;
        RenderContextBase renderContext { get { return hModel.renderContext; } }

        public SelectionManager(HModel hModel)
        {
            this.hModel = hModel;
        }

        public void DrawOvery()
        {
            if (step != Step.secondPoint)
                return;

            if (isCrossing)
            {
                DrawSelectionBox(initialLocation, currentLocation, Color.Red, true, true);
            }
            else
            {
                DrawSelectionBox(initialLocation, currentLocation, Color.Green, true, false);
            }
        }

        public void OnMouseMove(MouseEventArgs e)
        {
            if (step != Step.secondPoint)
                return;

            currentLocation = e.Location;
        }

        public void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left)
                return;

            // 처음 클릭시 객체 선택
            if (step == Step.firstPoint)
            {
                var ent = hModel.GetEntityUnderMouseCursor(e.Location);
                // 선택된게 없으면 다음으로 넘어감
                if (ent < 0)
                {
                    initialLocation = e.Location;
                    currentLocation = initialLocation;
                    step = Step.secondPoint;
                }
                // 선택되게 있으면 종료
                else
                {
                    hModel.Entities[ent].Selected = true;
                }
            }
            // 두번째 클릭시 객체를 enclosing / crossing으로 선택
            else if (step == Step.secondPoint)
            {
                step = Step.firstPoint;

                int dx = currentLocation.X - initialLocation.X;
                int dy = currentLocation.Y - initialLocation.Y;

                Point p1 = initialLocation;
                Point p2 = currentLocation;
                NormalizeBox(ref p1, ref p2);

                var rect = new System.Drawing.Rectangle(p1, new Size(Math.Abs(dx), Math.Abs(dy)));
                if (rect.Width == 0)
                    rect.Width = 1;
                if (rect.Height == 0)
                    rect.Height = 1;

                // crossing
                if (isCrossing)
                {
                    var ents = hModel.GetAllCrossingEntities(rect);
                    foreach (var ent in ents)
                    {
                        hModel.Entities[ent].Selected = true;
                    }
                }
                else
                {
                    var ents = hModel.GetAllEnclosedEntities(rect);
                    foreach (var ent in ents)
                    {
                        hModel.Entities[ent].Selected = true;
                    }
                }
            }

            hModel.Invalidate();
        }

        void DrawSelectionBox(System.Drawing.Point p1, System.Drawing.Point p2, Color transparentColor, bool drawBorder, bool dottedBorder)
        {
            p1.Y = (int)(hModel.Height - p1.Y);
            p2.Y = (int)(hModel.Height - p2.Y);

            NormalizeBox(ref p1, ref p2);

            // Adjust the bounds so that it doesn't exit from the current viewport frame
            int[] viewFrame = hModel.ActiveViewport.GetViewFrame();
            int left = viewFrame[0];
            int top = viewFrame[1] + viewFrame[3];
            int right = left + viewFrame[2];
            int bottom = viewFrame[1];

            if (p2.X > right - 1)
                p2.X = right - 1;

            if (p2.Y > top - 1)
                p2.Y = top - 1;

            if (p1.X < left + 1)
                p1.X = left + 1;

            if (p1.Y < bottom + 1)
                p1.Y = bottom + 1;

            hModel.renderContext.SetState(blendStateType.Blend);
            renderContext.SetColorWireframe(System.Drawing.Color.FromArgb(50, transparentColor.R, transparentColor.G,
                transparentColor.B));
            renderContext.SetState(rasterizerStateType.CCW_PolygonFill_CullFaceBack_NoPolygonOffset);

            int w = p2.X - p1.X;
            int h = p2.Y - p1.Y;

            renderContext.DrawQuad(new System.Drawing.RectangleF(p1.X + 1, p1.Y + 1, w - 1, h - 1));
            renderContext.SetState(blendStateType.NoBlend);

            if (drawBorder)
            {
                renderContext.SetColorWireframe(System.Drawing.Color.FromArgb(255, transparentColor.R,
                    transparentColor.G, transparentColor.B));

                List<Point3D> pts = null;

                if (dottedBorder)
                {
                    renderContext.SetLineStipple(1, 0x0F0F, hModel.ActiveViewport.Camera);
                    renderContext.EnableLineStipple(true);
                }

                int l = p1.X;
                int r = p2.X;
                if (renderContext.IsDirect3D) // In Eyeshot 9 use renderContext.IsDirect3D
                {
                    l += 1;
                    r += 1;
                }

                pts = new List<Point3D>(new Point3D[]
                {
                new Point3D(l, p1.Y), new Point3D(p2.X, p1.Y),
                new Point3D(r, p1.Y), new Point3D(r, p2.Y),
                new Point3D(r, p2.Y), new Point3D(l, p2.Y),
                new Point3D(l, p2.Y), new Point3D(l, p1.Y),
                });


                renderContext.DrawLines(pts.ToArray());

                if (dottedBorder)
                    renderContext.EnableLineStipple(false);
            }
        }

        internal void ClearSelection()
        {
            hModel.Entities.ClearSelection();
        }

        internal static void NormalizeBox(ref System.Drawing.Point p1, ref System.Drawing.Point p2)
        {

            int firstX = Math.Min(p1.X, p2.X);
            int firstY = Math.Min(p1.Y, p2.Y);
            int secondX = Math.Max(p1.X, p2.X);
            int secondY = Math.Max(p1.Y, p2.Y);

            p1.X = firstX;
            p1.Y = firstY;
            p2.X = secondX;
            p2.Y = secondY;

        }
    }
}
