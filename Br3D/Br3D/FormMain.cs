using Br3D.Properties;
using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Translators;
using devDept.Geometry;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using hanee.Cad.Tool;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

using ToolBarButton = devDept.Eyeshot.ToolBarButton;

namespace Br3D
{
    public partial class FormMain : DevExpress.XtraEditors.XtraForm
    {
        devDept.Eyeshot.ToolBarButton toolBarButtonWireframe = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources.wireframe_32x, "Wireframe", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);
        devDept.Eyeshot.ToolBarButton toolBarButtonHiddenLine = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources.hiddenline_32x, "HiddenLine", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);
        devDept.Eyeshot.ToolBarButton toolBarButtonShaded = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources.shaded_32x, "Shade", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);
        devDept.Eyeshot.ToolBarButton toolBarButtonRendered = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources.rendered_32x, "Render", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);
        devDept.Eyeshot.ToolBarSeparator toolBarSeparator = new ToolBarSeparator();
        devDept.Eyeshot.ToolBarButton toolBarButtonPerspectiveMode = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources.perspective, "Perspective", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);
        devDept.Eyeshot.ToolBarButton toolBarButtonOrthographicMode = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources.orthographic, "Orthographic", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);
        devDept.Eyeshot.ToolBarButton toolBarButton2DMode = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources._2d_32px, "2D", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);
        devDept.Eyeshot.ToolBarButton toolBarButton3DMode = new devDept.Eyeshot.ToolBarButton(global::Br3D.Properties.Resources._3d_32px, "3D", null, devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true, null, null);

        List<Viewport> viewports = new List<Viewport>();

        private Memo lastMemo = null;
        Model model => hModel;
        Dictionary<object, Action> functionByElement = new Dictionary<object, Action>();
        string opendFilePath = "";
        bool isDwg => string.IsNullOrEmpty(opendFilePath) ? false : Path.GetExtension(opendFilePath).ToLower().EndsWith("dwg");
        GripManager gripManager => hModel?.gripManager;

        public FormMain()
        {
            // InitializeComponent 로딩시간 오래 걸림
            // HModel의 생성자에서 오래 걸림 
            // 개선해야함
            InitializeComponent();
            model.Unlock("US21-D8G5N-12J8F-5F65-RD3W");

            model.MouseDoubleClick += Model_MouseDoubleClick;
            model.WorkCompleted += Model_WorkCompleted;
            model.WorkFailed += Model_WorkFailed;
            model.MouseUp += Model_MouseUp;
            model.MouseMove += Model_MouseMove;
            hModel.SaveBackgroundColor();

            foreach (Viewport vp in model.Viewports)
                viewports.Add(vp);

            Options.Instance.appName = VersionHelper.appName;
            Options.Instance.LoadOptions();

            // 테스트 용으로 옵션을 강제적용
            Options.Instance.tempEntityColorMethod = Options.TempEntityColorMethod.byTransparencyColor;


            ApplyOptions();

            InitGraphics();
            InitDisplayMode();

            InitSnapping();
            InitRibbonButtonMethod();
            InitTileElementStatus();
            InitObjectTreeList();
            InitPropertyGrid();

            InitToolbar();
            Translate();

            hModel.ActionMode = actionType.None;
            hModel.BoundingBoxChanged += HModel_BoundingBoxChanged;
            ribbonControl1.SearchItemShortcut = new BarShortcut(Keys.Control | Keys.F);

            EnableDynamicInput(true, false);
            SetLTEnvironment();

            
        }

        // 옵션을 적용한다.
        private void ApplyOptions()
        {
            // 배경색
            foreach (Viewport vp in model.Viewports)
            {
                if (hModel.IsTopViewOnly())
                {
                    vp.Background.TopColor = Options.Instance.backgroundColor2D.colorValue;
                    vp.Background.BottomColor = Options.Instance.backgroundColor2D.colorValue;
                }
                else
                {
                    vp.Background.TopColor = Options.Instance.backgroundColorTop.colorValue;
                    vp.Background.BottomColor = Options.Instance.backgroundColorBottom.colorValue;

                }
            }

            // 언어
            LanguageHelper.Load(Options.Instance.language);


            model.Invalidate();
        }

        // dynamic input을 활성화 한다.
        void EnableDynamicInput(bool enableDynamicInput, bool enableCommandbar)
        {
            // 둘다 off 인 경우
            if (!enableDynamicInput && !enableCommandbar)
            {
                controlCommandBar1.enabled = false;
                DynamicInputManager.enabled = false;
                DynamicInputManager.controlCommandBar = null;
                dockPanelDynamicInput.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Hidden;
                return;
            }

            // 그 외의 경우는 panel을 활성화 한다.
            dockPanelDynamicInput.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible;
            dockPanelDynamicInput.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;

            // 둘다 on 인 경우
            if (enableDynamicInput && enableCommandbar)
            {
                // command bar 활성화
                controlCommandBar1.enabled = true;

                // dynamic input manager에 command bar 연결
                DynamicInputManager.controlCommandBar = controlCommandBar1;

                // dynamic input manager 활성화
                DynamicInputManager.enabled = true;
            }
            // 하나만 on인 경우
            else if(enableCommandbar)
            {
                // command bar 활성화
                controlCommandBar1.enabled = true;
                controlCommandBar1.Visible = true;

                // dynamic input manager에 command bar 연결
                DynamicInputManager.controlCommandBar = controlCommandBar1;

                // dynamic input manager 활성화
                DynamicInputManager.enabled = true;
                DynamicInputManager.parentControls = null;
            }
            else if (enableDynamicInput)
            {
                // command bar 비활성화
                controlCommandBar1.enabled = false;
                controlCommandBar1.Visible = false;

                // dynamic input manager에 command bar 연결안함
                DynamicInputManager.controlCommandBar = null;

                // dynamic input manager 활성화
                DynamicInputManager.enabled = true;
                DynamicInputManager.parentControls = dockPanelDynamicInput.Controls;

            }
        }
        // lt 버전인 경우 LT 버전에 맞게 환경을 설정한다.
        private void SetLTEnvironment()
        {
            if (!VersionHelper.isLT)
            {
                // 광고 표시
                controlAds1.ShowAd();
                return;
            }

            this.Text = VersionHelper.appName;

            // save 기능 숨김
            barButtonItemSaveAs.Visibility = BarItemVisibility.Never;

            EnableDynamicInput(false, false);

            // 편집 리본탭 숨김
            ribbonPageDraw.Visible = false;
            ribbonPageEdit.Visible = false;
            ribbonPageDimension.Visible = false;

            

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

        private void FormMain_Load(object sender, EventArgs e)
        {
            ViewportSingle();

            Circle c = new Circle(Plane.XY, 200);
            hModel.Entities.Add(c);

            hModel.Set2DView();

            hModel.Entities.Remove(c);
        }


        private void Model_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateCoordinatesControl(e);
            if (e.Button != MouseButtons.None)
                return;

            Memo memo = GetMemoUnderMouseCursor(e.Location);
            if (memo != null && lastMemo != memo)
            {
                toolTipController1.ShowHint(memo.OneLineText);
                lastMemo = memo;
            }
            else
            {
                lastMemo = memo;
            }

            if (lastMemo == null)
                toolTipController1.HideHint();
        }

        private void UpdateCoordinatesControl(MouseEventArgs e)
        {

            var point = ActionBase.GetPoint3DWithSnapAndOrthoMode(hModel, e);
            if (point == null)
                return;

            var text = "";
            if (hModel.TopViewOnly)
                text = Units.GetPointString(point.X, point.Y);
            else
                text = Units.GetPointString(point.X, point.Y, point.Z);
            barStaticItemCoordinates.Caption = text;
        }

        // 마우스 커서 아래에 있는 memo를 리턴한다.
        private Memo GetMemoUnderMouseCursor(System.Drawing.Point location)
        {
            int index = model.GetLabelUnderMouseCursor(location);
            if (index != -1)
            {
                //get the entity
                var label = model.ActiveViewport.Labels[index];
                return label as Memo;
            }
            return null;
        }


        private void Translate()
        {
            InitRibbonButtonMethod();

            // context menu
            endPointToolStripMenuItem.Text = LanguageHelper.Tr("End point");
            intersectionPointToolStripMenuItem.Text = LanguageHelper.Tr("Intersection point");
            middlePointToolStripMenuItem.Text = LanguageHelper.Tr("Middle point");
            centerPointToolStripMenuItem.Text = LanguageHelper.Tr("Center point");
            selectallToolStripMenuItem.Text = LanguageHelper.Tr("Select all");
            unselectAllToolStripMenuItem.Text = LanguageHelper.Tr("Unselect all");
            invertSelectionToolStripMenuItem.Text = LanguageHelper.Tr("Invert selection");


            // page
            ribbonPageAnnotation.Text = LanguageHelper.Tr("Annotation");
            ribbonPageDimension.Text = LanguageHelper.Tr("Dimension");
            ribbonPageDraw.Text = LanguageHelper.Tr("Draw");
            ribbonPageEdit.Text = LanguageHelper.Tr("Edit");
            ribbonPageHome.Text = LanguageHelper.Tr("Home");
            ribbonPageOptions.Text = LanguageHelper.Tr("Options");
            ribbonPageTools.Text = LanguageHelper.Tr("Tools");

            // group
            ribbonPageGroupDim.Text = LanguageHelper.Tr("Dimension");
            ribbonPageGroupDraw.Text = LanguageHelper.Tr("Draw");
            ribbonPageGroupDrawAnnotation.Text = LanguageHelper.Tr("Annotation");
            ribbonPageGroupEdit.Text = LanguageHelper.Tr("Edit");
            ribbonPageGroupFile.Text = LanguageHelper.Tr("File");
            ribbonPageGroupOptions.Text = LanguageHelper.Tr("Options");
            ribbonPageGroupOrthoMode.Text = LanguageHelper.Tr("Ortho Mode");
            ribbonPageGroupOsnap.Text = LanguageHelper.Tr("Osnap");
            ribbonPageGroupSystem.Text = LanguageHelper.Tr("System");
            ribbonPageGroupTools.Text = LanguageHelper.Tr("Tools");
            ribbonPageGroupClearAnnotations.Text = LanguageHelper.Tr("Clear");
            ribbonPageGroupViewport.Text = LanguageHelper.Tr("Viewports");




            // control
            dockPanelObjectTree.Text = LanguageHelper.Tr("Object Tree");
            dockPanelProperties.Text = LanguageHelper.Tr("Properties");

            // propertygrid
            categoryGeneral.Properties.Caption = LanguageHelper.Tr("General");
            categoryBlock.Properties.Caption = LanguageHelper.Tr("Block");
            categoryLineType.Properties.Caption = LanguageHelper.Tr("Line Type");
            categoryText.Properties.Caption = LanguageHelper.Tr("Text");

            rowEntityType.Properties.Caption = LanguageHelper.Tr("Entity Type");
            rowVisible.Properties.Caption = LanguageHelper.Tr("Visible");
            rowBoxMax.Properties.Caption = LanguageHelper.Tr("Max");
            rowBoxMin.Properties.Caption = LanguageHelper.Tr("Min");
            rowLayerName.Properties.Caption = LanguageHelper.Tr("Layer");
            rowColor.Properties.Caption = LanguageHelper.Tr("Color");
            rowColorMethod.Properties.Caption = LanguageHelper.Tr("Color Type");
            rowGroupIndex.Properties.Caption = LanguageHelper.Tr("Group Index");
            rowLineTypeName.Properties.Caption = LanguageHelper.Tr("Line Type Name");
            rowLineTypeScale.Properties.Caption = LanguageHelper.Tr("Scale");
            rowLineWeight.Properties.Caption = LanguageHelper.Tr("Weight");
            rowLineWeightMethod.Properties.Caption = LanguageHelper.Tr("Weight Type");
            rowBlockName.Properties.Caption = LanguageHelper.Tr("Block Name");
            rowTextString.Properties.Caption = LanguageHelper.Tr("Contents");
            rowStyleName.Properties.Caption = LanguageHelper.Tr("Style");
            rowHeight.Properties.Caption = LanguageHelper.Tr("Height");
            rowBillboard.Properties.Caption = LanguageHelper.Tr("Billboard");
            rowWidthFactor.Properties.Caption = LanguageHelper.Tr("Width Factor");
            rowInsertionPoint.Properties.Caption = LanguageHelper.Tr("Insertion Point");
            rowBackward.Properties.Caption = LanguageHelper.Tr("Backward");
            rowUpsideDown.Properties.Caption = LanguageHelper.Tr("Upside Down");
            rowAlignment.Properties.Caption = LanguageHelper.Tr("Alignment");


        }


        private void InitToolbar()
        {
            foreach (Viewport vp in model.Viewports)
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

        // 3d 모드로 변경
        private void ToolBarButton3DMode_Click(object sender, EventArgs e)
        {
            hModel.Set3DView();
        }

        private void ToolBarButton2DMode_Click(object sender, EventArgs e)
        {
            hModel.Set2DView();
        }

        private void ToolBarButtonOrthographicMode_Click(object sender, EventArgs e)
        {
            model.ActiveViewport.Camera.ProjectionMode = devDept.Graphics.projectionType.Orthographic;
            model.ActiveViewport.ZoomFit();
            model.ActiveViewport.Invalidate();
        }

        private void ToolBarButtonPerspectiveMode_Click(object sender, EventArgs e)
        {
            model.ActiveViewport.Camera.ProjectionMode = devDept.Graphics.projectionType.Perspective;
            model.ActiveViewport.ZoomFit();
            model.ActiveViewport.Invalidate();
        }

        private void ToolBarButtonDisplayMode_Click(object sender, EventArgs e)
        {

            var toolBar = sender as ToolBarButton;
            if (toolBar == toolBarButtonWireframe)
                model.ActiveViewport.DisplayMode = displayType.Wireframe;
            else if (toolBar == toolBarButtonHiddenLine)
                model.ActiveViewport.DisplayMode = displayType.HiddenLines;
            else if (toolBar == toolBarButtonShaded)
                model.ActiveViewport.DisplayMode = displayType.Shaded;
            else if (toolBar == toolBarButtonRendered)
                model.ActiveViewport.DisplayMode = displayType.Rendered;

            model.Invalidate();
        }

        private void InitGraphics()
        {
            model.AntiAliasing = true;
            model.AntiAliasingSamples = devDept.Graphics.antialiasingSamplesNumberType.x4;
            model.AskForAntiAliasing = true;
        }

        private void InitDisplayMode()
        {
            model.Shaded.ShowInternalWires = false;
            model.Shaded.EdgeColorMethod = edgeColorMethodType.SingleColor;
            model.Shaded.EdgeThickness = 1;

            model.Rendered.SilhouettesDrawingMode = silhouettesDrawingType.Never;
            model.Rendered.ShadowMode = devDept.Graphics.shadowType.None;
        }

        void RefreshPropertyGridControl(object selectedObj)
        {
            if (selectedObj is Entity)
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

        private void Model_MouseUp(object sender, MouseEventArgs e)
        {
            if (model.ActionMode != actionType.None)
                return;

            if (e.Button == MouseButtons.Left)
            {
                bool gripEditing = gripManager != null && gripManager.EditingGripPoints();
                if (!gripEditing && ActionBase.runningAction == null)
                {
                    var item = model.GetItemUnderMouseCursor(e.Location);

                    // 속성창 갱신
                    if (propertyGridControl1.Visible)
                        RefreshPropertyGridControl(item?.Item);

                    // tree에서 선택
                    if (treeListObject.Visible && item != null)
                    {
                        treeListObject.ClearSelection();
                        var node = treeListObject.FindNode(x => x.Tag == item.Item);
                        if (node == null)
                            return;
                        if (node.ParentNode != null)
                            node.ParentNode.Expand();

                        treeListObject.SelectNode(node);
                        treeListObject.TopVisibleNodeIndex = node.Id;
                    }
                }
            }
        }


        private void InitPropertyGrid()
        {
            propertyGridControl1.CellValueChanged += PropertyGridControl1_CellValueChanged;
            propertyGridControl1.ShowingEditor += PropertyGridControl1_ShowingEditor;
            RefreshPropertyGridControl(null);
        }

        private void PropertyGridControl1_ShowingEditor(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (propertyGridControl1.FocusedRow.Properties.RowEdit == repositoryItemComboBoxTextStyle)
            {
                repositoryItemComboBoxTextStyle.Items.Clear();
                foreach (var ts in hModel.TextStyles)
                {
                    repositoryItemComboBoxTextStyle.Items.Add(ts.Name);
                }
            }
            else if (propertyGridControl1.FocusedRow.Properties.RowEdit == repositoryItemComboBoxLayerName)
            {
                repositoryItemComboBoxLayerName.Items.Clear();
                foreach (var la in hModel.Layers)
                {
                    repositoryItemComboBoxLayerName.Items.Add(la.Name);
                }
            }
            else if (propertyGridControl1.FocusedRow.Properties.RowEdit == repositoryItemComboBoxLineType)
            {

                repositoryItemComboBoxLineType.Items.Clear();
                foreach (var lt in hModel.LineTypes)
                {
                    repositoryItemComboBoxLineType.Items.Add(lt.Name);
                }
            }
            else if (propertyGridControl1.FocusedRow.Properties.RowEdit == repositoryItemComboBoxBlock)
            {
                repositoryItemComboBoxBlock.Items.Clear();
                foreach (var b in hModel.Blocks)
                {
                    repositoryItemComboBoxBlock.Items.Add(b.Name);
                }
            }

        }

        private void PropertyGridControl1_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            var entProp = propertyGridControl1.SelectedObject as EntityProperties;
            if (entProp == null)
                return;

            try
            {
                model.TempEntities.Clear();
                model.Entities.Regen();

                propertyGridControl1.UpdateData();

                model.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void InitObjectTreeList()
        {
            treeListObject.FocusedNodeChanged += TreeListObject_FocusedNodeChanged;
            treeListObject.AfterCheckNode += TreeListObject_AfterCheckNode;
        }

        // check 변경시
        private void TreeListObject_AfterCheckNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            var node = e.Node;
            if (node == null)
                return;

            AfterCheckNode(node);
            model.Invalidate();
        }

        private void AfterCheckNode(DevExpress.XtraTreeList.Nodes.TreeListNode node)
        {
            var ent = node.Tag as Entity;
            if (ent != null)
            {
                ent.Visible = node.Checked;
            }


            if (node.Nodes == null)
                return;

            foreach (DevExpress.XtraTreeList.Nodes.TreeListNode childNode in node.Nodes)
            {
                AfterCheckNode(childNode);
            }
        }

        private void TreeListObject_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            var entities = GetAllEntitiesByNode(e.Node, true);
            if (entities == null)
                return;

            model.Entities.ClearSelection();
            entities.ForEach(x => x.Selected = true);
            model.Invalidate();
        }

        private List<Entity> GetAllEntitiesByNode(DevExpress.XtraTreeList.Nodes.TreeListNode node, bool subEntities)
        {
            List<Entity> entities = new List<Entity>();
            if (node.Tag is Layer)
            {
                var layerName = ((Layer)node.Tag).Name;
                var result = model.Entities.FindAll(x => x.LayerName == layerName);
                if (result != null)
                    entities.AddRange(result);
            }
            else if (node.Tag is Block)
            {
                var blockName = ((Block)node.Tag).Name;
                var result = model.Entities.FindAll(x => x is BlockReferenceEx && ((BlockReference)x).BlockName == blockName);
                if (result != null)
                    entities.AddRange(result);
            }
            else if (node.Tag is LineType)
            {
                var lineTypeName = ((LineType)node.Tag).Name;
                var result = model.Entities.FindAll(x => x.LineTypeName == lineTypeName);
                if (result != null)
                    entities.AddRange(result);
            }
            else if (node.Tag is HatchPattern)
            {
                var hatchPatternName = ((HatchPattern)node.Tag).Name;
                var result = model.Entities.FindAll(x => x is Hatch && ((Hatch)x).PatternName == hatchPatternName);
                if (result != null)
                    entities.AddRange(result);
            }
            else if (node.Tag is TextStyle)
            {
                var textStyleName = ((TextStyle)node.Tag).Name;
                var result = model.Entities.FindAll(x => x is Text && ((Text)x).StyleName == textStyleName);
                if (result != null)
                    entities.AddRange(result);
            }
            else
            {

                var subNodes = node.GetAllSubNodes();
                foreach (DevExpress.XtraTreeList.Nodes.TreeListNode subNode in subNodes)
                {
                    if (subNode.Tag is Entity)
                    {
                        entities.Add((Entity)subNode.Tag);
                    }

                }

            }

            return entities;
        }


        void InitTileElementStatus()
        {
            barButtonItemLanguageKorean.Down = false;
            barButtonItemLanguageEnglish.Down = false;
            if (Options.Instance.language == "ko-KR")
                barButtonItemLanguageKorean.Down = true;
            else if (Options.Instance.language == "en-US")
                barButtonItemLanguageEnglish.Down = true;
        }

        void SetFunctionByElement(BarButtonItem barButtonItem, Action action, string caption, string command, string shortcut)
        {
            barButtonItem.Caption = caption;

            if (functionByElement.ContainsKey(barButtonItem))
                return;

            functionByElement.Add(barButtonItem, action);
            if (!string.IsNullOrEmpty(command))
            {
                controlCommandBar1.AddCommand(command, command, action);
            }

            if (!string.IsNullOrEmpty(shortcut))
            {
                controlCommandBar1.AddCommand(shortcut, command, action);
            }
        }

        // ribbon button별 method 목록 초기화
        void InitRibbonButtonMethod()
        {
            // home
            SetFunctionByElement(barButtonItemOpen, Open, LanguageHelper.Tr("Open"), "Open", "op");
            SetFunctionByElement(barButtonItemSaveAs, SaveAs, LanguageHelper.Tr("Save As"), "SaveAs", "sa");
            SetFunctionByElement(barButtonItemSaveImage, SaveImage, LanguageHelper.Tr("Save Image"), "SaveImage", "si");
            SetFunctionByElement(barButtonItemWorkspace, Workspace, LanguageHelper.Tr("Workspace"), "Workspace", "ws");
            SetFunctionByElement(barButtonItemExit, Close, LanguageHelper.Tr("Exit"), "Exit", null);

            // draw
            SetFunctionByElement(barButtonItemDrawLine, Line, LanguageHelper.Tr("Line"), "Line", "l");
            SetFunctionByElement(barButtonItemDrawCircle, Circle, LanguageHelper.Tr("Circle"), "Circle", "c");
            SetFunctionByElement(barButtonItemDrawArc, Arc, LanguageHelper.Tr("Arc"), "Arc", "a");
            SetFunctionByElement(barButtonItemDrawArc_FirstSecondThird, ArcFirstSecondThird, LanguageHelper.Tr("First, second, third point"), null, null);
            SetFunctionByElement(barButtonItemDrawArc_CenterStartEnd, ArcCenterStartEnd, LanguageHelper.Tr("Center, start, end point"), null, null);
            SetFunctionByElement(barButtonItemDrawPolyline, Polyline, LanguageHelper.Tr("Polyline"), "Polyline", "pl");
            SetFunctionByElement(barButtonItemDrawSpline, Spline, LanguageHelper.Tr("Spline"), "Spline", "sp");
            SetFunctionByElement(barButtonItemDrawText, DrawText, LanguageHelper.Tr("Text"), "Text", "t");
            SetFunctionByElement(barButtonItemMultilineText, MText, LanguageHelper.Tr("Multiline Text"), "MText", "mt");
            SetFunctionByElement(barButtonItemInsert, Insert, LanguageHelper.Tr("Insert"), "Insert", "i");
            SetFunctionByElement(barButtonItemInsertImage, InsertImage, LanguageHelper.Tr("Insert Image"), "InsertImage", "ii");
            SetFunctionByElement(barButtonItemDrawCylinder, Cylinder, LanguageHelper.Tr("Cylinder"), "Cylinder", "cy");

            // dimension
            SetFunctionByElement(barButtonItemDimHorizontal, DimHorizontal, LanguageHelper.Tr("Horizontal"), "Horizontal", "hor");
            SetFunctionByElement(barButtonItemDimVertical, DimVertical, LanguageHelper.Tr("Vertical"), "Vertical", "vert");
            SetFunctionByElement(barButtonItemDimAlign, DimAlign, LanguageHelper.Tr("Aligned"), "Aligned", "ali");
            SetFunctionByElement(barButtonItemDimDiameter, DimDiameter, LanguageHelper.Tr("Diameter"), "Diameter", "dia");
            SetFunctionByElement(barButtonItemDimRadius, DimRadius, LanguageHelper.Tr("Radius"), "Radius", "r");
            SetFunctionByElement(barButtonItemDimLeader, DimLeader, LanguageHelper.Tr("Leader"), "Leader", "le");

            // edit
            SetFunctionByElement(barButtonItemErase, EraseEntity, LanguageHelper.Tr("Erase"), "Earse", "e");
            SetFunctionByElement(barButtonItemMove, MoveEntity, LanguageHelper.Tr("Move"), "Move", "m");
            SetFunctionByElement(barButtonItemCopy, CopyEntity, LanguageHelper.Tr("Copy"), "Copy", "c");
            SetFunctionByElement(barButtonItemScale, ScaleEntity, LanguageHelper.Tr("Scale"), "Scale", "sc");
            SetFunctionByElement(barButtonItemRotate, RotateEntity, LanguageHelper.Tr("Rotate"), "Rotate", "r");
            SetFunctionByElement(barButtonItemOffset, OffsetEntity, LanguageHelper.Tr("Offset"), "Offset", "o");
            SetFunctionByElement(barButtonItemMirror, MirrorEntity, LanguageHelper.Tr("Mirror"), "Mirror", "mi");
            SetFunctionByElement(barButtonItemExplode, ExplodeEntity, LanguageHelper.Tr("Explode"), "Explode", "x");
            SetFunctionByElement(barButtonItemTrim, TrimEntity, LanguageHelper.Tr("Trim"), "Trim", "tr");
            SetFunctionByElement(barButtonItemFillet, FilletEntity, LanguageHelper.Tr("Fillet"), "Fillet", "f");
            SetFunctionByElement(barButtonItemChamfer, ChamferEntity, LanguageHelper.Tr("Chamfer"), "Chamfer", "ch");

            // annotation
            SetFunctionByElement(barButtonItemCoordinates, Coorindates, LanguageHelper.Tr("Coordinates"), "Coordinates", "coor");
            SetFunctionByElement(barButtonItemDistance, Distance, LanguageHelper.Tr("Distance"), "Distance", "di");
            SetFunctionByElement(barButtonItemMemo, Memo, LanguageHelper.Tr("Memo"), "Memo", "me");
            SetFunctionByElement(barButtonItemClearAnnotations, ClearAnnotations, LanguageHelper.Tr("Clear annotations"), "ClearAnnotations", "ca");


            // osnap
            SetFunctionByElement(barButtonItemOrthoMode, OrthoMode, LanguageHelper.Tr("Ortho mode"), "OrthoMode", "or");
            SetFunctionByElement(barButtonItemOsnapend, End, LanguageHelper.Tr("End Point"), "End", "end");
            SetFunctionByElement(barButtonItemOsnapIntersection, Intersection, LanguageHelper.Tr("Intersection Point"), "Int", null);
            SetFunctionByElement(barButtonItemOsnapMiddle, Middle, LanguageHelper.Tr("Midle Point"), "Mid", null);
            SetFunctionByElement(barButtonItemOsnapCenter, Center, LanguageHelper.Tr("Center Point"), "Cen", null);
            SetFunctionByElement(barButtonItemOsnapPoint, Point, LanguageHelper.Tr("Point"), null, null);

            // tools
            SetFunctionByElement(barButtonItemSingleView, ViewportSingle, LanguageHelper.Tr("Single"), "Single", null);
            SetFunctionByElement(barButtonItem1x1View, Viewport1x1, LanguageHelper.Tr("1x1"), "1x1", null);
            SetFunctionByElement(barButtonItem1x2View, Viewport1x2, LanguageHelper.Tr("1x2"), "1x2", null);
            SetFunctionByElement(barButtonItem2x2View, Viewport2x2, LanguageHelper.Tr("2x2"), "2x2", null);

            SetFunctionByElement(barButtonItemLayer, Layer, LanguageHelper.Tr("Layer"), "Layer", "la");
            SetFunctionByElement(barButtonItemTextStyle, TextStyle, LanguageHelper.Tr("Text Style"), "TextStyle", "ts");
            SetFunctionByElement(barButtonItemLineType, LineType, LanguageHelper.Tr("Line Type"), "LineType", "lt");

            // options
            SetFunctionByElement(barButtonItemShowGrid, null, LanguageHelper.Tr("Grid"), null, null);
            SetFunctionByElement(barButtonItemShowToolbar, null, LanguageHelper.Tr("Toolbar"), null, null);
            SetFunctionByElement(barButtonItemShowSymbol, null, LanguageHelper.Tr("Symbol"), null, null);

            SetFunctionByElement(barButtonItemLanguage, null, LanguageHelper.Tr("Language"), null, null);
            SetFunctionByElement(barButtonItemLanguageKorean, Korean, LanguageHelper.Tr("Korean"), "Korean", null);
            SetFunctionByElement(barButtonItemLanguageEnglish, English, LanguageHelper.Tr("English"), "English", null);
            SetFunctionByElement(barButtonItemHomepage, Homepage, LanguageHelper.Tr("Homepage"), "Homepage", null);
            SetFunctionByElement(barButtonItemCheckForUpdate, CheckForUpdate, LanguageHelper.Tr("Check For Update"), "CheckForUpdate", null);
            SetFunctionByElement(barButtonItemOptions, RunOptions, LanguageHelper.Tr("Options"), null, null);
            SetFunctionByElement(barButtonItemAbout, About, LanguageHelper.Tr("About"), "About", null);

            // status bar
            SetFunctionByElement(barButtonItemEndPoint, End, "", "End", null);
            SetFunctionByElement(barButtonItemIntPoint, Intersection, "", "Int", null);
            SetFunctionByElement(barButtonItemMidPoint, Middle, "", "Mid", null);
            SetFunctionByElement(barButtonItemCenterPoint, Center, "", "Cen", null);
            SetFunctionByElement(barButtonItemNodePoint, Point, "", null, null);

        }

        async void Workspace() => await new ActionWorkspace(model).RunAsync();
        async void InsertImage() => await new ActionInsertImage(model).RunAsync();
        async void Insert() => await new ActionInsert(model).RunAsync();
        async void DimLeader() => await new ActionDimLeader(model).RunAsync();
        async void DimAlign() => await new ActionDimLinear(model) { dimDirection = ActionDimLinear.DimDirection.aligned }.RunAsync();
        async void DimVertical() => await new ActionDimLinear(model) { dimDirection = ActionDimLinear.DimDirection.vertical }.RunAsync();
        async void DimHorizontal() => await new ActionDimLinear(model) { dimDirection = ActionDimLinear.DimDirection.horizontal }.RunAsync();
        async void DimRadius() => await new ActionDimDiameter(model) { radius = true }.RunAsync();
        async void DimDiameter() => await new ActionDimDiameter(model).RunAsync();
        async void ChamferEntity() => await new ActionFillet(model) { chamfer = true }.RunAsync();
        async void FilletEntity() => await new ActionFillet(model).RunAsync();
        //async void ExtendEntity() => await new ActionExtend(model).RunAsync();
        async void TrimEntity() => await new ActionTrim(model).RunAsync();
        async void ExplodeEntity() => await new ActionExplode(model).RunAsync();
        async void MirrorEntity() => await new ActionMirror(model).RunAsync();
        async void OffsetEntity() => await new ActionOffset(model).RunAsync();
        async void RotateEntity() => await new ActionRotate(model).RunAsync();
        async void ScaleEntity() => await new ActionScale(model).RunAsync();
        async void CopyEntity() => await new ActionCopy(model).RunAsync();
        async void MoveEntity() => await new ActionMove(model).RunAsync();
        async void EraseEntity() => await new ActionErase(model).RunAsync();
        async void MText()
        {
            var ac = new ActionText(model);
            ac.multilineText = true;
            await ac.RunAsync();
        }
        async void DrawText() => await new ActionText(model).RunAsync();
        async void Polyline() => await new ActionPolyline(model).RunAsync();
        async void Spline() => await new ActionPolyline(model) { spline = true }.RunAsync();
        async void Cylinder() => await new ActionCylinder(model).RunAsync();
        async void ArcCenterStartEnd() => await new ActionArc(model, ActionArc.Method.centerStartEnd).RunAsync();
        async void ArcFirstSecondThird() => await new ActionArc(model, ActionArc.Method.firstSecondThird).RunAsync();
        async void Arc() => await new ActionArc(model, ActionArc.Method.firstSecondThird).RunAsync();
        async void Circle() => await new ActionCircle(model).RunAsync();
        async void Line() => await new ActionLine(model).RunAsync();
        void RegenAll()
        {
            if (isDwg)
                hModel.Set2DView();
            else
                hModel.Set3DView();

            // zoom fit
            foreach (Viewport v in model.Viewports)
                v.ZoomFit();


            // object tree 갱신
            ObjectTreeListHelper.RegenAsync(treeListObject, model, isDwg);

            RefreshDataSource();
        }
        void LineType() => new ActionLineType(model, this).Run();
        void TextStyle() => new ActionTextStyle(model, this).Run();
        void Layer() => new ActionLayer(model, this).Run();
        void About() => new FormAbout().ShowDialog();
        void RunOptions()
        {
            FormOptions form = new FormOptions();
            if (form.ShowDialog() == DialogResult.OK)
            {
                ApplyOptions();
            }
        }
        void Homepage() => System.Diagnostics.Process.Start("http://hileejaeho.cafe24.com/kr-br3d/");
        void Language()
        {
        }

        void Korean()
        {
            Options.Instance.language = "ko-KR";
            InitTileElementStatus();
            LanguageHelper.Load(Options.Instance.language);
            Translate();
        }

        void English()
        {
            Options.Instance.language = "en-US";
            InitTileElementStatus();
            LanguageHelper.Load(Options.Instance.language);
            Translate();
        }
        private void InitSnapping()
        {
            if (model is HModel)
            {
                HModel vp = (HModel)model;
                vp.Snapping.SetActiveObjectSnap(Snapping.objectSnapType.None, true);
                vp.Snapping.objectSnapEnabled = true;
            }
        }

        private void Model_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Middle)
            {
                model.ZoomFit();
                return;
            }

            // zoom all
            if (e.Button == MouseButtons.Middle)
            {
                model.ZoomFit();
                return;
            }

            if (ActionBase.IsUserInputting())
            {
                return;
            }

            if (model.ObjectManipulator.Visible == false)
            {
                devDept.Geometry.Transformation trans = new devDept.Geometry.Transformation();
                IList<Entity> selectedEntities = ((HModel)model).GetAllSelectedEntities();
                trans.Identity();
                model.ObjectManipulator.Enable(trans, true, selectedEntities);
            }
            else
            {
                model.ObjectManipulator.Apply();
                model.Entities.Regen();
            }
        }

        private void Model_WorkFailed(object sender, WorkFailedEventArgs e)
        {
            MessageBox.Show(e.Error);

        }

        private void Model_WorkCompleted(object sender, WorkCompletedEventArgs e)
        {
            if (e.WorkUnit is ReadFileAsync)
            {
                ReadFileAsync rfa = (ReadFileAsync)e.WorkUnit;

                // viewport에 추가한다.
                rfa.AddToScene(model);

                // layer color을 background에 따라 변경(검은색을 흰색으로 또는 흰색을 검은색으로)
                hModel.SetLayerColorByBackgroundColor();

                opendFilePath = rfa.FileName;

                RegenAll();
            }
        }

        private void RefreshDataSource()
        {
            if (ActionLayer.formLayer != null)
                ActionLayer.formLayer.RefreshDataSource();
            if (ActionTextStyle.formTextStyle != null)
                ActionTextStyle.formTextStyle.RefreshDataSource();
            if (ActionLineType.formLineType != null)
                ActionLineType.formLineType.RefreshDataSource();
        }

        private void navButtonMain_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {

        }


        void FlagOrthoMode(BarButtonItem barButtonItem)
        {
            HModel hModel = model as HModel;
            if (hModel == null)
                return;

            hModel.orthoModeManager.enabled = !hModel.orthoModeManager.enabled;
            barButtonItem.Down = hModel.orthoModeManager.enabled;
        }

        // 
        void FlagOsnap(BarButtonItem barButtonItem, Snapping.objectSnapType snapType, BarButtonItem barButtonItem2 = null)
        {
            HModel hModel = model as HModel;
            if (hModel == null)
                return;

            hModel.Snapping.FlagActiveObjectSnap(snapType);
            barButtonItem.Down = hModel.Snapping.IsActiveObjectSnap(snapType);
            if(barButtonItem2 != null)
                barButtonItem2.Down = barButtonItem.Down;
        }

        void ViewportSingle()
        {
            if (model.Viewports.Contains(viewports[1]))
                model.Viewports.Remove(viewports[1]);
            if (model.Viewports.Contains(viewports[2]))
                model.Viewports.Remove(viewports[2]);
            if (model.Viewports.Contains(viewports[3]))
                model.Viewports.Remove(viewports[3]);
            model.Invalidate();
        }

        void Viewport1x1()
        {
            if (!model.Viewports.Contains(viewports[1]))
                model.Viewports.Add(viewports[1]);
            if (model.Viewports.Contains(viewports[2]))
                model.Viewports.Remove(viewports[2]);
            if (model.Viewports.Contains(viewports[3]))
                model.Viewports.Remove(viewports[3]);
            foreach (Viewport vp in model.Viewports)
                vp.ZoomFit();
            model.Invalidate();
        }
        void Viewport1x2()
        {
            if (!model.Viewports.Contains(viewports[1]))
                model.Viewports.Add(viewports[1]);
            if (!model.Viewports.Contains(viewports[2]))
                model.Viewports.Add(viewports[2]);
            if (model.Viewports.Contains(viewports[3]))
                model.Viewports.Remove(viewports[3]);
            foreach (Viewport vp in model.Viewports)
                vp.ZoomFit();
            model.Invalidate();
        }
        void Viewport2x2()
        {
            if (!model.Viewports.Contains(viewports[1]))
                model.Viewports.Add(viewports[1]);
            if (!model.Viewports.Contains(viewports[2]))
                model.Viewports.Add(viewports[2]);
            if (!model.Viewports.Contains(viewports[3]))
                model.Viewports.Add(viewports[3]);
            foreach (Viewport vp in model.Viewports)
                vp.ZoomFit();
            model.Invalidate();
        }

        void OrthoMode() => FlagOrthoMode(barButtonItemOrthoMode);
        void End() => FlagOsnap(barButtonItemOsnapend, Snapping.objectSnapType.End, barButtonItemEndPoint);
        void Middle() => FlagOsnap(barButtonItemOsnapMiddle, Snapping.objectSnapType.Mid, barButtonItemMidPoint);
        void Point() => FlagOsnap(barButtonItemOsnapPoint, Snapping.objectSnapType.Point, null);
        void Intersection() => FlagOsnap(barButtonItemOsnapIntersection, Snapping.objectSnapType.Intersect, barButtonItemIntPoint);
        void Center() => FlagOsnap(barButtonItemOsnapCenter, Snapping.objectSnapType.Center, barButtonItemCenterPoint);

        async void Coorindates()
        {
            ActionID ac = new ActionID(model, ActionID.ShowResult.label);
            await ac.RunAsync();
        }

        async void Distance()
        {
            ActionDist ac = new ActionDist(model, ActionDist.ShowResult.label);
            await ac.RunAsync();
        }

        async void Memo()
        {
            ActionMemo ac = new ActionMemo(model);
            await ac.RunAsync();
        }

        void ClearAnnotations()
        {
            model.ActiveViewport.Labels.Clear();
            model.Invalidate();
        }

        private void SaveImage()
        {
            var dlg = new XtraSaveFileDialog();
            dlg.Filter = "Bitmap (*.bmp)|*.bmp|" +
                "Portable Network Graphics (*.png)|*.png|" +
                "Windows metafile (*.wmf)|*.wmf|" +
                "Enhanced Windows Metafile (*.emf)|*.emf";

            dlg.FilterIndex = 2;
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {

                if (!Options.Instance.saveImageWithUI)
                {
                    ShowGrid(false);
                    ShowToolbar(false);
                    ShowSymbol(false);
                }

                double lineWeightFactor = 1;
                switch (dlg.FilterIndex)
                {

                    case 1:
                        model.WriteToFileRaster(2, lineWeightFactor, dlg.FileName, System.Drawing.Imaging.ImageFormat.Bmp, Options.Instance.saveImageWithBackground);
                        break;
                    case 2:
                        model.WriteToFileRaster(2, lineWeightFactor, dlg.FileName, System.Drawing.Imaging.ImageFormat.Png, Options.Instance.saveImageWithBackground);
                        break;
                    case 3:
                        model.WriteToFileRaster(2, lineWeightFactor, dlg.FileName, System.Drawing.Imaging.ImageFormat.Wmf, Options.Instance.saveImageWithBackground);
                        break;
                    case 4:
                        model.WriteToFileRaster(2, lineWeightFactor, dlg.FileName, System.Drawing.Imaging.ImageFormat.Emf, Options.Instance.saveImageWithBackground);
                        break;

                }

                if (!Options.Instance.saveImageWithUI)
                {
                    ShowGrid(barButtonItemShowGrid.Down);
                    ShowToolbar(barButtonItemShowToolbar.Down);
                    ShowSymbol(barButtonItemShowSymbol.Down);
                }

            }
        }

        void Open()
        {

            // 파일 선택
            var dlg = new XtraOpenFileDialog();
            Dictionary<string, string> additionalSupportFormats = new Dictionary<string, string>();
            dlg.Filter = FileHelper.FilterForOpenDialog(additionalSupportFormats);
            dlg.FilterIndex = 0;
            dlg.AddExtension = true;
            dlg.CheckFileExists = true;
            dlg.CheckPathExists = true;
            if (dlg.ShowDialog() != DialogResult.OK)
                return;

            NewFile();

            Import(dlg.FileName);
        }

        void SaveAs()
        {
            var dlg = new XtraSaveFileDialog();
            dlg.Filter = FileHelper.FilterForSaveDialog();
            dlg.DefaultExt = "dwg";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Export(dlg.FileName);
            }
        }

        // ribbon - import
        // iges, igs, stl, step, stp, obj, las, dwg, dxf, ifc, ifczip, 3ds, lus
        void Import(string pathFileName)
        {
            try
            {
                devDept.Eyeshot.Translators.ReadFileAsync rf = FileHelper.GetReadFileAsync(pathFileName);
                if (rf == null)
                    return;

                model.StartWork(rf);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);

            }

        }

        void Export(string pathFileName)
        {
            try
            {
                devDept.Eyeshot.Translators.WriteFileAsync wf = FileHelper.GetWriteFileAsync(model, pathFileName);
                if (wf == null)
                    return;

                model.StartWork(wf);
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }

        }

        void NewFile()
        {
            model.Clear();
            model.Invalidate();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            Options.Instance.SaveOptions();

            // 업데이트 체크
#if DEBUG
#else
            if (!IsCheckedAutoForUpdateToday())
            {
                CheckForUpdate();
                SaveLastAutoCheckForUpdate();
            }
#endif
        }

        void CheckForUpdate()
        {
            this.Cursor = Cursors.WaitCursor;

            var filePath = Path.Combine(hanee.ThreeD.Util.GetExePath(), "wyUpdate.exe");
            if (File.Exists(filePath))
            {
                var pi = System.Diagnostics.Process.Start(filePath, "/quickcheck /justcheck /noerr");
                pi.WaitForExit(5000);
                if (pi.ExitCode == 2)
                {
                    if (XtraMessageBox.Show(LanguageHelper.Tr("Install now?"), VersionHelper.appName + LanguageHelper.Tr(" update available"), MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(filePath);
                        Close();
                    }
                }
            }
            else
            {

                XtraMessageBox.Show(LanguageHelper.Tr("Update check failed! - wyUpdate.exe not found!"));
            }

            this.Cursor = Cursors.Default;
        }

        // 오늘 자동 업데이트 체크를 했는지?
        bool IsCheckedAutoForUpdateToday()
        {
            var checkedDate = Settings.Default.LastAutoCheckForUpdate;
            if (checkedDate == null)
                return false;

            // 오늘 체크했다면 true
            if (DateTime.Today.Date == checkedDate.Date)
                return true;

            return false;

        }

        // 마지막으로 자동으로 업데이트 체크한 날짜 기록
        private void SaveLastAutoCheckForUpdate()
        {
            Settings.Default.LastAutoCheckForUpdate = DateTime.Today.Date;
            Settings.Default.Save();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            HModel hModel = model as HModel;
            if (hModel != null)
            {
                hModel.KeyEventListener(keyData);
            }

            if (keyData == Keys.F8)
            {
                OrthoMode();
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            HModel hModel = model as HModel;
            if (hModel == null)
                return;

            // ctrl이나 shift가 눌러져 있는지?
            bool withCtrl = true;
            if(!System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftCtrl) && 
                !System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.RightCtrl) &&
                !System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.LeftShift) &&
                !System.Windows.Input.Keyboard.IsKeyDown(System.Windows.Input.Key.RightShift))
            {
                withCtrl = false;
            }

            if (withCtrl)
            {
                endPointToolStripMenuItem.Checked = hModel.Snapping.IsActiveObjectSnap(Snapping.objectSnapType.End);
                intersectionPointToolStripMenuItem.Checked = hModel.Snapping.IsActiveObjectSnap(Snapping.objectSnapType.Intersect);
                middlePointToolStripMenuItem.Checked = hModel.Snapping.IsActiveObjectSnap(Snapping.objectSnapType.Mid);
                centerPointToolStripMenuItem.Checked = hModel.Snapping.IsActiveObjectSnap(Snapping.objectSnapType.Center);

                VisibleContextMenuItems(endPointToolStripMenuItem, intersectionPointToolStripMenuItem, 
                    middlePointToolStripMenuItem, centerPointToolStripMenuItem);
            }
            else
            {
                VisibleContextMenuItems(selectallToolStripMenuItem, unselectAllToolStripMenuItem, 
                    invertSelectionToolStripMenuItem);
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

        private void ribbonControl1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (functionByElement.TryGetValue(e.Item, out Action act))
            {
                if (act != null)
                    act();
            }
            else
            {
#if DEBUG
                //MessageBox.Show("undefined function");
#endif
            }
        }

        void ShowGrid(bool visible)
        {
            foreach (Viewport v in model.Viewports)
            {
                foreach (var g in v.Grids)
                {
                    g.Visible = visible;
                }
            }
        }



        private void barButtonItemShowGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowGrid(barButtonItemShowGrid.Down);
            model.Invalidate();
        }

        void ShowToolbar(bool visible)
        {
            foreach (Viewport v in model.Viewports)
            {
                foreach (var t in v.ToolBars)
                {
                    t.Visible = visible;
                }
            }
        }
        private void barButtonItemShowToolbar_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowToolbar(barButtonItemShowToolbar.Down);
            model.Invalidate();
        }

        void ShowSymbol(bool visible)
        {
            foreach (Viewport v in model.Viewports)
            {
                foreach (var o in v.OriginSymbols)
                {
                    o.Visible = visible;
                }

                v.CoordinateSystemIcon.Visible = visible;
                v.ViewCubeIcon.Visible = visible;
            }
        }
        private void barButtonItemShowSymbol_ItemClick(object sender, ItemClickEventArgs e)
        {
            ShowSymbol(barButtonItemShowSymbol.Down);
            model.Invalidate();
        }

        // select all
        private void selectallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            model.Entities.SelectAll();
            model.Invalidate();

            RefreshPropertyGridControl(model.Entities[model.Entities.Count - 1]);
        }

        // unselect all
        private void unselectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            model.Entities.ClearSelection();
            model.Invalidate();

            RefreshPropertyGridControl(null);
        }

        // invert selection
        private void invertSelectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Entity lastSelectedEntity = null;
            foreach (var ent in model.Entities)
            {
                ent.Selected = !ent.Selected;
                if (ent.Selected)
                    lastSelectedEntity = ent;
            }
            model.Invalidate();

            RefreshPropertyGridControl(lastSelectedEntity);
        }
    }
}