using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using DevExpress.XtraEditors;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Br3D
{
    public partial class ControlModel : DevExpress.XtraEditors.XtraUserControl
    {
        List<Viewport> viewports = new List<Viewport>();
        devDept.Eyeshot.ToolBarButton toolBarButtonWireframe = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources.wireframe_32x, "Wireframe", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);
        devDept.Eyeshot.ToolBarButton toolBarButtonHiddenLine = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources.hiddenline_32x, "HiddenLine", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);
        devDept.Eyeshot.ToolBarButton toolBarButtonShaded = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources.shaded_32x, "Shade", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);
        devDept.Eyeshot.ToolBarButton toolBarButtonRendered = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources.rendered_32x, "Render", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);
        devDept.Eyeshot.ToolBarSeparator toolBarSeparator = new ToolBarSeparator();
        devDept.Eyeshot.ToolBarButton toolBarButtonPerspectiveMode = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources.perspective, "Perspective", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);
        devDept.Eyeshot.ToolBarButton toolBarButtonOrthographicMode = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources.orthographic, "Orthographic", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);
        devDept.Eyeshot.ToolBarButton toolBarButton2DMode = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources._2d_32px, "2D", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);
        devDept.Eyeshot.ToolBarButton toolBarButton3DMode = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources._3d_32px, "3D", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);

        public ControlModel()
        {
            InitializeComponent();
            hModel.Unlock("US21-D8G5N-12J8F-5F65-RD3W");

            InitGraphics();
            InitDisplayMode();
            InitSnapping();
            InitGrids();
            InitToolbar();

            hModel.SaveBackgroundColor();
            hModel.ActionMode = actionType.None;
            hModel.BoundingBoxChanged += HModel_BoundingBoxChanged;
            
            foreach (Viewport vp in hModel.Viewports)
                viewports.Add(vp);

            ViewportSingle();

            // zoom을 넉넉하게 하기 위해서 circle을 그린후 set3dview를 한다.
            Circle c = new Circle(Plane.XY, 200);
            hModel.Entities.Add(c);
            hModel.Set3DView();
            hModel.Entities.Remove(c);
        }

        private void InitGraphics()
        {
            hModel.AntiAliasing = true;
            hModel.AntiAliasingSamples = devDept.Graphics.antialiasingSamplesNumberType.x4;
            hModel.AskForAntiAliasing = true;
        }

        private void InitDisplayMode()
        {
            hModel.Shaded.ShowInternalWires = false;
            hModel.Shaded.EdgeColorMethod = edgeColorMethodType.SingleColor;
            hModel.Shaded.EdgeThickness = 1;

            hModel.Rendered.SilhouettesDrawingMode = silhouettesDrawingType.Never;
            hModel.Rendered.ShadowMode = devDept.Graphics.shadowType.None;
        }

        private void InitSnapping()
        {
            hModel.Snapping.SetActiveObjectSnap(Snapping.objectSnapType.None, true);
            hModel.Snapping.objectSnapEnabled = true;
        }

        private void InitGrids()
        {
            if (hModel.Viewports.Count < 2)
                return;

            // 첫번째 viewport의 그리드를 나머지 viewport에 속성을 복사
            var vp1 = hModel.Viewports[0];
            if (vp1.Grids.Length < 2)
                return;

            var grid1 = vp1.Grids[1];
            for (int i = 1; i < hModel.Viewports.Count; ++i)
            {
                var vpCur = hModel.Viewports[i];
                if (vpCur.Grids.Length < 2)
                    continue;

                var gridCur = vpCur.Grids[1];
                gridCur.AutoSize = grid1.AutoSize;
                gridCur.AutoStep = grid1.AutoStep;
                gridCur.BorderColor = grid1.BorderColor;
                gridCur.ColorAxisX = grid1.ColorAxisX;
                gridCur.ColorAxisY = grid1.ColorAxisY;
                gridCur.FillColor = grid1.FillColor;
                gridCur.AlwaysBehind = grid1.AlwaysBehind;
                gridCur.Lighting = grid1.Lighting;
                gridCur.LineColor = grid1.LineColor;
                gridCur.MajorLineColor = grid1.MajorLineColor;
                gridCur.MajorLinesEvery = grid1.MajorLinesEvery;
                gridCur.MaxNumberOfLines = grid1.MaxNumberOfLines;
                gridCur.MinNumberOfLines = grid1.MinNumberOfLines;
                gridCur.Step = grid1.Step;
            }

        }

        private void HModel_BoundingBoxChanged(object sender)
        {
            var boxSize = hModel.Entities.BoxSize;
            if (boxSize == null || boxSize.X < 100 || boxSize.Y < 100)
            {
                hModel.ActiveViewport.Grid.AutoSize = false;
                hModel.ActiveViewport.Grid.Min = new devDept.Geometry.Point2D(-100, -100);
                hModel.ActiveViewport.Grid.Max = new devDept.Geometry.Point2D(100, 100);
            }
            else
            {
                hModel.ActiveViewport.Grid.AutoSize = true;
            }
        }

        public void ViewportSingle()
        {
            if (hModel.Viewports.Contains(viewports[1]))
                hModel.Viewports.Remove(viewports[1]);
            if (hModel.Viewports.Contains(viewports[2]))
                hModel.Viewports.Remove(viewports[2]);
            if (hModel.Viewports.Contains(viewports[3]))
                hModel.Viewports.Remove(viewports[3]);
            hModel.Invalidate();
        }

        public void Viewport1x1()
        {
            if (!hModel.Viewports.Contains(viewports[1]))
                hModel.Viewports.Add(viewports[1]);
            if (hModel.Viewports.Contains(viewports[2]))
                hModel.Viewports.Remove(viewports[2]);
            if (hModel.Viewports.Contains(viewports[3]))
                hModel.Viewports.Remove(viewports[3]);
            foreach (Viewport vp in hModel.Viewports)
                vp.ZoomFit();
            hModel.Invalidate();
        }
        public void Viewport1x2()
        {
            if (!hModel.Viewports.Contains(viewports[1]))
                hModel.Viewports.Add(viewports[1]);
            if (!hModel.Viewports.Contains(viewports[2]))
                hModel.Viewports.Add(viewports[2]);
            if (hModel.Viewports.Contains(viewports[3]))
                hModel.Viewports.Remove(viewports[3]);
            foreach (Viewport vp in hModel.Viewports)
                vp.ZoomFit();
            hModel.Invalidate();
        }
        public void Viewport2x2()
        {
            if (!hModel.Viewports.Contains(viewports[1]))
                hModel.Viewports.Add(viewports[1]);
            if (!hModel.Viewports.Contains(viewports[2]))
                hModel.Viewports.Add(viewports[2]);
            if (!hModel.Viewports.Contains(viewports[3]))
                hModel.Viewports.Add(viewports[3]);
            foreach (Viewport vp in hModel.Viewports)
                vp.ZoomFit();
            hModel.Invalidate();
        }

        private void InitToolbar()
        {
            foreach (Viewport vp in hModel.Viewports)
            {
                if (vp.ToolBars.Length > 1)
                {
                    var displayModelToolbar = vp.ToolBars[1];
                    displayModelToolbar.Position = devDept.Eyeshot.ToolBar.positionType.VerticalTopLeft;
                    displayModelToolbar.Buttons.Clear();
                    displayModelToolbar.Buttons.Add(toolBarButtonWireframe);
                    displayModelToolbar.Buttons.Add(toolBarButtonHiddenLine);
                    displayModelToolbar.Buttons.Add(toolBarButtonShaded);
                    displayModelToolbar.Buttons.Add(toolBarButtonRendered);
                    displayModelToolbar.Buttons.Add(toolBarSeparator);
                    displayModelToolbar.Buttons.Add(toolBarButtonPerspectiveMode);
                    displayModelToolbar.Buttons.Add(toolBarButtonOrthographicMode);
                    displayModelToolbar.Buttons.Add(toolBarSeparator);
                    displayModelToolbar.Buttons.Add(toolBarButton2DMode);
                    displayModelToolbar.Buttons.Add(toolBarButton3DMode);
                }

                vp.Rotate.MouseButton = new MouseButton(MouseButtons.Middle, modifierKeys.Ctrl);
                vp.Rotate.RotationMode = rotationType.Turntable;

                vp.Pan.MouseButton = new MouseButton(MouseButtons.Middle, modifierKeys.None);
            }

            toolBarButtonWireframe.Click += ToolBarButtonDisplayMode_Click;
            toolBarButtonHiddenLine.Click += ToolBarButtonDisplayMode_Click;
            toolBarButtonShaded.Click += ToolBarButtonDisplayMode_Click;
            toolBarButtonRendered.Click += ToolBarButtonDisplayMode_Click;

            toolBarButtonPerspectiveMode.Click += ToolBarButtonPerspectiveMode_Click;
            toolBarButtonOrthographicMode.Click += ToolBarButtonOrthographicMode_Click;

            toolBarButton2DMode.Click += ToolBarButton2DMode_Click;
            toolBarButton3DMode.Click += ToolBarButton3DMode_Click;

        }

        private void ToolBarButtonDisplayMode_Click(object sender, EventArgs e)
        {

            var toolBar = sender as devDept.Eyeshot.ToolBarButton;
            if (toolBar == toolBarButtonWireframe)
                hModel.ActiveViewport.DisplayMode = displayType.Wireframe;
            else if (toolBar == toolBarButtonHiddenLine)
                hModel.ActiveViewport.DisplayMode = displayType.HiddenLines;
            else if (toolBar == toolBarButtonShaded)
                hModel.ActiveViewport.DisplayMode = displayType.Shaded;
            else if (toolBar == toolBarButtonRendered)
                hModel.ActiveViewport.DisplayMode = displayType.Rendered;

            hModel.Invalidate();
        }

        private void ToolBarButtonPerspectiveMode_Click(object sender, EventArgs e)
        {
            hModel.ActiveViewport.Camera.ProjectionMode = devDept.Graphics.projectionType.Perspective;
            hModel.ActiveViewport.ZoomFit();
            hModel.ActiveViewport.Invalidate();
        }
        private void ToolBarButtonOrthographicMode_Click(object sender, EventArgs e)
        {
            hModel.ActiveViewport.Camera.ProjectionMode = devDept.Graphics.projectionType.Orthographic;
            hModel.ActiveViewport.ZoomFit();
            hModel.ActiveViewport.Invalidate();
        }
        // 3d 모드로 변경
        private void ToolBarButton3DMode_Click(object sender, EventArgs e)
        {
            hModel.Set3DView();
        }

        private void ToolBarButton2DMode_Click(object sender, EventArgs e)
        {
            hModel.Set2DView();
        }


    }
}
