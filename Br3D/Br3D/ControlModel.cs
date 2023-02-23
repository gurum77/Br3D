using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using hanee.Geometry;
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

        public BarButtonItem barButtonItemOsnapend { get; set; }
        public BarButtonItem barButtonItemOsnapIntersection { get; set; }
        public BarButtonItem barButtonItemOsnapMiddle { get; set; }
        public BarButtonItem barButtonItemOsnapCenter { get; set; }
        public BarButtonItem barButtonItemOsnapPoint { get; set; }
        public CurLayerBarEditItem barEditItemCurLayer { get; set; }
        public CurColorBarEditItem barEditItemCurColor { get; set; }
        public CurLinetypeBarEditItem barEditItemCurLinetype { get; set; }
        public DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControl1 { get; set; }
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

            hModel.BoundingBox.Visible = true;
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

        // 번역
        public void Translate()
        {
            // context menu
            endPointToolStripMenuItem.Text = LanguageHelper.Tr("End point(&E)");
            intersectionPointToolStripMenuItem.Text = LanguageHelper.Tr("Intersection point(&I)");
            middlePointToolStripMenuItem.Text = LanguageHelper.Tr("Middle point(&M)");
            centerPointToolStripMenuItem.Text = LanguageHelper.Tr("Center point(&C)");
            selectallToolStripMenuItem.Text = LanguageHelper.Tr("Select all(&A)");
            unselectAllToolStripMenuItem.Text = LanguageHelper.Tr("Unselect all(&U)");
            invertSelectionToolStripMenuItem.Text = LanguageHelper.Tr("Invert selection(&V)");
            transparencyToolStripMenuItem.Text = LanguageHelper.Tr("Transparency(&T)");
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

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if (hModel == null)
                return;
            
            // ctrl이나 shift가 눌러져 있는지?
            bool withCtrl = true;
            if (!System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftCtrl) &&
                !System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.RightCtrl) &&
                !System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftShift) &&
                !System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.RightShift))
            {
                withCtrl = false;
            }

            // 액션실행중인지?
            bool runningAction = ActionBase.runningAction != null;

            if (withCtrl)
            {
                endPointToolStripMenuItem.Checked = hModel.Snapping.IsActiveObjectSnap(Snapping.objectSnapType.End);
                intersectionPointToolStripMenuItem.Checked = hModel.Snapping.IsActiveObjectSnap(Snapping.objectSnapType.Intersect);
                middlePointToolStripMenuItem.Checked = hModel.Snapping.IsActiveObjectSnap(Snapping.objectSnapType.Mid);
                centerPointToolStripMenuItem.Checked = hModel.Snapping.IsActiveObjectSnap(Snapping.objectSnapType.Center);

                // ctrl이 눌러져 있으면 snap 관련 menu item만 표시
                VisibleContextMenuItems(endPointToolStripMenuItem, intersectionPointToolStripMenuItem,
                    middlePointToolStripMenuItem, centerPointToolStripMenuItem);
            }
            else
            {
                // 아무것도 안눌러져 있으면 선택관련 menu item만 표시
                // 액션을 실행하고 있지 않아야 함
                if (!runningAction)
                {
                    VisibleContextMenuItems(selectallToolStripMenuItem, unselectAllToolStripMenuItem,
                        invertSelectionToolStripMenuItem, transparencyToolStripMenuItem);
                }
                else
                {
                    VisibleContextMenuItems();
                }
            }
        }

        // 해당 아이템을 표시한다.(나머지는 숨긴다)
        private void VisibleContextMenuItems(params ToolStripMenuItem[] toolStripMenuItems)
        {
            foreach (ToolStripMenuItem item in contextMenuStrip1.Items)
            {
                item.Visible = false;
            }

            foreach (ToolStripMenuItem item in toolStripMenuItems)
            {
                item.Visible = true;
            }
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem == endPointToolStripMenuItem)
            {
                End();
            }
            else if (e.ClickedItem == intersectionPointToolStripMenuItem)
            {
                Intersection();
            }
            else if (e.ClickedItem == middlePointToolStripMenuItem)
            {
                Middle();
            }
            else if (e.ClickedItem == centerPointToolStripMenuItem)
            {
                Center();
            }
        }

        public void End() => FlagOsnap(barButtonItemOsnapend, Snapping.objectSnapType.End);
        public void Middle() => FlagOsnap(barButtonItemOsnapMiddle, Snapping.objectSnapType.Mid);
        public void Point() => FlagOsnap(barButtonItemOsnapPoint, Snapping.objectSnapType.Point, null);
        public void Intersection() => FlagOsnap(barButtonItemOsnapIntersection, Snapping.objectSnapType.Intersect);
        public void Center() => FlagOsnap(barButtonItemOsnapCenter, Snapping.objectSnapType.Center);

        void FlagOsnap(BarButtonItem barButtonItem, Snapping.objectSnapType snapType, BarButtonItem barButtonItem2 = null)
        {
            if (hModel == null)
                return;

            hModel.Snapping.FlagActiveObjectSnap(snapType);
            barButtonItem.Down = hModel.Snapping.IsActiveObjectSnap(snapType);
            if (barButtonItem2 != null)
                barButtonItem2.Down = barButtonItem.Down;
        }

        // select all
        private void selectallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hModel.Entities.SelectAll();
            hModel.Invalidate();
            if(hModel.Entities.Count > 0)
                RefreshPropertyGridControl(hModel.Entities[hModel.Entities.Count - 1]);
            UpdateCurCombos();
        }

        // unselect all
        private void unselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hModel.Entities.ClearSelection();
            hModel.Invalidate();

            RefreshPropertyGridControl(null);
            UpdateCurCombos();
        }

        // invert selection
        private void invertSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity lastSelectedEntity = null;
            foreach (var ent in hModel.Entities)
            {
                ent.Selected = !ent.Selected;
                if (ent.Selected)
                    lastSelectedEntity = ent;
            }
            hModel.Invalidate();

            RefreshPropertyGridControl(lastSelectedEntity);
            UpdateCurCombos();
        }

        public void RefreshPropertyGridControl(object selectedObj)
        {
            if(selectedObj == null)
            {
                ModelProperties modelProperties = new ModelProperties(hModel);
                propertyGridControl1.SelectedObject = modelProperties;
            }
            else if (selectedObj is Entity)
            {
                EntityProperties entityProperties = new EntityProperties(selectedObj as Entity);
                propertyGridControl1.SelectedObject = entityProperties;
            }
            else
            {
                propertyGridControl1.SelectedObject = selectedObj;

            }
            propertyGridControl1.SetVisibleExistPropertiyOnly();

            propertyGridControl1.BestFit();
        }

        public void UpdateCurCombos()
        {
            Options.Instance.SyncCurStatus(hModel);

            var entities = hModel.GetAllSelectedEntities();

            // cur layer
            barEditItemCurLayer.UpdateCombo(entities);

            // cur color
            barEditItemCurColor.UpdateCombo(entities);

            // cur linetype
            barEditItemCurLinetype.UpdateCombo(entities);
        }


        // 투명도 - 0 (불투명)
        private void toolStripMenuItemTransparency0_Click(object sender, EventArgs e)
        {
            SetTransparency(255);
        }

        private void toolStripMenuItemTransparency50_Click(object sender, EventArgs e)
        {
            SetTransparency(127);
        }

        private void toolStripMenuItemTransparency100_Click(object sender, EventArgs e)
        {
            SetTransparency(0);
        }

        void SetTransparency(int alpha)
        {
            if (hModel == null)
                return;

            foreach (var ent in hModel.Entities)
            {
                if (!ent.Selected)
                    continue;

                if (ent is BlockReference br)
                {
                    foreach (Entity be in hModel.Blocks[br.BlockName].Entities)
                    {
                        var color = be.GetUsedColor(hModel);

                        be.Color = System.Drawing.Color.FromArgb(alpha, color);
                        be.ColorMethod = colorMethodType.byEntity;
                    }
                }
                else
                {
                    var color = ent.GetUsedColor(hModel);
                    ent.Color = System.Drawing.Color.FromArgb(alpha, color);
                    ent.ColorMethod = colorMethodType.byEntity;

                }
            }
            hModel.Invalidate();
        }

        private void endPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            End();
        }

        private void intersectionPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Intersection();
        }

        private void middlePointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Middle();
        }

        private void centerPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Center();
        }
    }
}
