using Br3D.Properties;
using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Translators;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using hanee.Cad.Tool;
using hanee.Terrain.Tool;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Br3D
{
    public partial class FormMain : DevExpress.XtraEditors.XtraForm
    {
        private Memo lastMemo = null;

        ControlModel controlModel = null;
        HModel hModel => controlModel?.hModel;
        Model model => hModel;
        Dictionary<object, Action> functionByElement = new Dictionary<object, Action>();
        string opendFilePath = "";
        bool modifiedFile = false;
        bool isDwg => string.IsNullOrEmpty(opendFilePath) ? false : Path.GetExtension(opendFilePath).ToLower().EndsWith("dwg");
        GripManager gripManager => hModel?.gripManager;
        bool openMode = true; // 파일 열기인지?

        public FormMain()
        {
            // option load를 제일 먼저 해야 함
            // dll에 대한 translate를 하려면 InitializeComponent를 하기 전에 해야함
            // 언어를 먼저 로드 한다.
            Options.Instance.appName = VersionHelper.appName;
            Options.Instance.LoadOptions();
            LanguageHelper.Load(Options.Instance.language);

            // InitializeComponent 로딩시간 오래 걸림
            // HModel의 생성자에서 오래 걸림 
            // 개선해야함
            InitializeComponent();

            ribbonControl1.SearchItemShortcut = new BarShortcut(Keys.Control | Keys.F);

            AllowDrop = true;
            DragDrop += FormMain_DragDrop;
            DragEnter += FormMain_DragEnter;

            barButtonItemWorkspace.Alignment = BarItemLinkAlignment.Right;
            barButtonItemOsnapend.Alignment = BarItemLinkAlignment.Right;
            barButtonItemOsnapIntersection.Alignment = BarItemLinkAlignment.Right;
            barButtonItemOsnapMiddle.Alignment = BarItemLinkAlignment.Right;
            barButtonItemOsnapCenter.Alignment = BarItemLinkAlignment.Right;
            barButtonItemOsnapPoint.Alignment = BarItemLinkAlignment.Right;

            ribbonStatusBar1.ItemLinks.Add(barButtonItemWorkspace);
            ribbonStatusBar1.ItemLinks.Add(barButtonItemOsnapend);
            ribbonStatusBar1.ItemLinks.Add(barButtonItemOsnapIntersection);
            ribbonStatusBar1.ItemLinks.Add(barButtonItemOsnapMiddle);
            ribbonStatusBar1.ItemLinks.Add(barButtonItemOsnapCenter);
            ribbonStatusBar1.ItemLinks.Add(barButtonItemOsnapPoint);

        }

        // control model을 초기화한다.
        void InitControlModel()
        {
            controlModel = new ControlModel();
            controlModel.Dock = DockStyle.Fill;
            controlModel.barButtonItemOsnapCenter = barButtonItemOsnapCenter;
            controlModel.barButtonItemOsnapend = barButtonItemOsnapend;
            controlModel.barButtonItemOsnapIntersection = barButtonItemOsnapIntersection;
            controlModel.barButtonItemOsnapMiddle = barButtonItemOsnapMiddle;
            controlModel.barButtonItemOsnapPoint = barButtonItemOsnapPoint;

            controlModel.barEditItemCurLayer = barEditItemCurLayer;
            controlModel.barEditItemCurColor = barEditItemCurColor;
            controlModel.barEditItemCurLinetype = barEditItemCurLinetype;

            controlModel.propertyGridControl1 = propertyGridControl1;

            pictureEdit1.Controls.Add(controlModel);
        }

        private void FormMain_Shown(object sender, EventArgs e)
        {
            // model init은 제일 나중에 한다.
            InitControlModel();

            controlScriptCad1.Visible = true;
            controlScriptCad1.model = model;

            simpleButtonInit.Visible = false;
            //this.Controls.Add(controlModel);  // form에 직접 add 하면 controlModel의 크기가 잘못 계산됨

            // control이 만들어 지고 나서 translate를 한다
            Translate();

            InitCurCombos();
            UpdateCurCombos();


            model.MouseDoubleClick += Model_MouseDoubleClick;
            model.WorkCompleted += Model_WorkCompleted;
            model.WorkFailed += Model_WorkFailed;
            model.MouseUp += Model_MouseUp;
            model.MouseMove += Model_MouseMove;
            model.BoundingBoxChanged += Model_BoundingBoxChanged;

            ApplyOptions();

            SplashScreenManagerHelper.SafeCloseForm();

            // startup file open
            var args = System.Environment.GetCommandLineArgs();
            if (args.Length > 1)
            {
                var fileName = args[1];
                Import(fileName, true);
            }

            Update2D3DButton();
            UpdateDisplayModeButton();


        }


        private void Model_BoundingBoxChanged(object sender)
        {
            modifiedFile = true;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {


            EnableDynamicInput(true, false);
            SetLTEnvironment();

            InitRibbonButtonMethod();
            InitTileElementStatus();
            InitObjectTreeList();
            InitPropertyGrid();



            // 테스트 용으로 옵션을 강제적용
            Options.Instance.tempEntityColorMethod = Options.TempEntityColorMethod.byTransparencyColor;
        }

        private void InitCurCombos()
        {
            barEditItemCurLayer.Init(model);
            barEditItemCurColor.Init(model);
            barEditItemCurLinetype.Init(model);

            barEditItemCurLayer.repositoryItemImageComboBoxCurLayer.SelectedIndexChanged += RepositoryItemImageComboBoxCurLayer_SelectedIndexChanged;
        }

        // layer combo를 바꾸면 cur color / cur linetype 콤보를 갱신한다.
        private void RepositoryItemImageComboBoxCurLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            // 선택한 객체가 없는 경우에
            UpdateCurCombos();
        }

        void UpdateCurCombos()
        {
            if (controlModel == null)
                return;

            controlModel.UpdateCurCombos();
        }

        void RefreshPropertyGridControl(object selectedObj)
        {
            if (controlModel == null)
                return;

            controlModel.RefreshPropertyGridControl(selectedObj);
        }


        private void FormMain_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop)) e.Effect = DragDropEffects.Copy;
        }

        private void FormMain_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files == null || files.Length != 1)
                return;


            Import(files[0], true);
        }



        // 옵션을 적용한다.
        private void ApplyOptions(bool regen = false)
        {
            // 배경색
            foreach (Viewport vp in model.Viewports)
            {
                if (hModel.IsTopViewOnly(vp))
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

            // ltscale이 변경되었으면 객체에 반영해야 함
            foreach (var ent in model.Entities)
            {
                ent.LineTypeScale = Options.Instance.curLinetypeScale;
            }
            if (regen)
                model.Entities.RegenAllCurved(null);
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
            else if (enableCommandbar)
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
            barButtonItemNew.Visibility = BarItemVisibility.Never;
            barButtonItemSave.Visibility = BarItemVisibility.Never;
            barButtonItemSaveAs.Visibility = BarItemVisibility.Never;

            EnableDynamicInput(false, false);

            // 편집 리본탭 숨김
            ribbonPageDraw.Visible = false;
            ribbonPageDraw3D.Visible = false;
            ribbonPageEdit.Visible = false;
            ribbonPageEdit3D.Visible = false;
            ribbonPageTerrain.Visible = false;
            ribbonPageDimension.Visible = false;
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
            if (hModel.IsTopViewOnly(hModel.ActiveViewport))
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
            controlScriptCad1.Translate();
            if (controlModel != null)
                controlModel.Translate();

            // subitem
            barSubItem2D3D.Caption = LanguageHelper.Tr("2D/3D");
            barSubItemDisplayMode.Caption = LanguageHelper.Tr("Display mode");


            // page
            ribbonPageAnnotation.Text = LanguageHelper.Tr("Annotation/Measure");
            ribbonPageDimension.Text = LanguageHelper.Tr("Dimension");
            ribbonPageDraw.Text = LanguageHelper.Tr("Draw");
            ribbonPageDraw3D.Text = LanguageHelper.Tr("Draw 3D");
            ribbonPageTerrain.Text = LanguageHelper.Tr("Terrain");
            ribbonPageEdit.Text = LanguageHelper.Tr("Edit");
            ribbonPageEdit3D.Text = LanguageHelper.Tr("Edit 3D");
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











        private void Model_MouseUp(object sender, MouseEventArgs e)
        {
            Update2D3DButton();
            UpdateDisplayModeButton();

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

                    // combo 선택
                    UpdateCurCombos();

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
            SetFunctionByElement(barButtonItemNew, New, LanguageHelper.Tr("New"), "New", "n");
            SetFunctionByElement(barButtonItemOpen, Open, LanguageHelper.Tr("Open"), "Open", "op");
            SetFunctionByElement(barButtonItemReload, Reload, LanguageHelper.Tr("Reload"), "Reload", "reload");
            SetFunctionByElement(barButtonItemSave, Save, LanguageHelper.Tr("Save"), "Save", "sa");
            SetFunctionByElement(barButtonItemSaveAs, SaveAs, LanguageHelper.Tr("Save As"), "SaveAs", "saveas");
            SetFunctionByElement(barButtonItemSaveImage, SaveImage, LanguageHelper.Tr("Save Image"), "SaveImage", "si");
            SetFunctionByElement(barButtonItemWorkspace, Workspace, LanguageHelper.Tr("Workspace"), "Workspace", "ws");
            SetFunctionByElement(barButtonItemExit, Close, LanguageHelper.Tr("Exit"), "Exit", null);

            // draw
            SetFunctionByElement(barButtonItemDrawLine, Line, LanguageHelper.Tr("Line"), "Line", "l");
            SetFunctionByElement(barButtonItemDrawCircle, Circle, LanguageHelper.Tr("Circle"), "Circle", "c");
            SetFunctionByElement(barButtonItemDrawCircleByCenterRadius, CircleByCenterRadius, LanguageHelper.Tr("Center point, Radius"), "CircleByCenterRadius", "ccr");
            SetFunctionByElement(barButtonItemDrawCircleByFirstSecondThird, CircleByFirstSecondThird, LanguageHelper.Tr("First, second, third point"), "CircleByFirstSecondThird", "c3p");
            SetFunctionByElement(barButtonItemDrawCircleByFirstSecond, CircleByFirstSecond, LanguageHelper.Tr("First, second point"), "CircleByFirstSecond", "c2p");

            SetFunctionByElement(barButtonItemDrawArc, Arc, LanguageHelper.Tr("Arc"), "Arc", "a");
            SetFunctionByElement(barButtonItemDrawArc_FirstSecondThird, ArcFirstSecondThird, LanguageHelper.Tr("First, second, third point"), null, null);
            SetFunctionByElement(barButtonItemDrawArc_CenterStartEnd, ArcCenterStartEnd, LanguageHelper.Tr("Center, start, end point"), null, null);
            SetFunctionByElement(barButtonItemDrawPolyline, Polyline, LanguageHelper.Tr("Polyline"), "Polyline", "pl");
            SetFunctionByElement(barButtonItemDrawSpline, Spline, LanguageHelper.Tr("Spline"), "Spline", "sp");
            SetFunctionByElement(barButtonItemDrawText, DrawText, LanguageHelper.Tr("Text"), "Text", "t");
            SetFunctionByElement(barButtonItemMultilineText, MText, LanguageHelper.Tr("Multiline Text"), "MText", "mt");
            SetFunctionByElement(barButtonItemInsertBlock, InsertBlock, LanguageHelper.Tr("Insert Block"), "InsertBlock", "i");
            SetFunctionByElement(barButtonItemInsertModel, InsertModel, LanguageHelper.Tr("Insert Model"), "InsertModel", "im");
            SetFunctionByElement(barButtonItemInsertImage, InsertImage, LanguageHelper.Tr("Insert Image"), "InsertImage", "ii");

            SetFunctionByElement(barButtonItemDrawRegion, DrawRegion, LanguageHelper.Tr("Region"), "Region", "rg");

            SetFunctionByElement(barButtonItemDrawCylinder, Cylinder, LanguageHelper.Tr("Cylinder"), "Cylinder", "cy");
            SetFunctionByElement(barButtonItemDrawBox, Box, LanguageHelper.Tr("Box"), "Box", "bx");
            SetFunctionByElement(barButtonItemDrawCone, Cone, LanguageHelper.Tr("Cone"), "Cone", "cn");
            SetFunctionByElement(barButtonItemDrawSphere, Sphere, LanguageHelper.Tr("Sphere"), "Sphere", "sp");
            SetFunctionByElement(barButtonItemDrawTorus, Torus, LanguageHelper.Tr("Torus"), "Torus", "to");

            SetFunctionByElement(barButtonItemRevolve, Revolve, LanguageHelper.Tr("Revolve"), "Revolve", "rv");
            SetFunctionByElement(barButtonItemLoft, Loft, LanguageHelper.Tr("Loft"), "Loft", "lf");

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
            SetFunctionByElement(barButtonItemBlock, MakeBlock, LanguageHelper.Tr("Block"), "Block", "bl");

            // edit 3d
            SetFunctionByElement(barButtonItemSubtract, Subtract, LanguageHelper.Tr("Subtract"), "Subtract", "sub");
            SetFunctionByElement(barButtonItemUnion, Union, LanguageHelper.Tr("Union"), "Union", "uni");
            SetFunctionByElement(barButtonItemIntersection, Intersection3D, LanguageHelper.Tr("Intersection"), "Intersection", "inte");
            SetFunctionByElement(barButtonItemAlign, Align, LanguageHelper.Tr("Align"), "Align", "align");
            SetFunctionByElement(barButtonItemSmartExtrude, SmartExtrude, LanguageHelper.Tr("Smart extrude"), "SmartExtrude", "se");

            SetFunctionByElement(barButtonItemExtractFace, ExtractFace, LanguageHelper.Tr("Extract face"), "ExtractFace", "ef");
            

            // terrain
            SetFunctionByElement(barButtonItemCreateTerrain, CreateTerrain, LanguageHelper.Tr("Create terrain"), "CreateTerrain", "ct");
            SetFunctionByElement(barButtonItemColoringTerrain, ColoringTerrain, LanguageHelper.Tr("Coloring terrain"), "ColoringTerrain", "crt");
            SetFunctionByElement(barButtonItemCreateContour, CreateContour, LanguageHelper.Tr("Create contour"), "CreateContour", "crc");
            SetFunctionByElement(barButtonItemImportTerrain, ImportTerrain, LanguageHelper.Tr("Import terrain"), "ImportTerrain", "it");
            SetFunctionByElement(barButtonItemExportTerrain, ExportTerrain, LanguageHelper.Tr("Export terrain"), "ExportTerrain", "et");
            SetFunctionByElement(barButtonItemUpDownTerrain, UpDownTerrain, LanguageHelper.Tr("Up/down terrain"), "UpDownTerrain", "udt");




            // annotation
            SetFunctionByElement(barButtonItemCoordinates, Coorindates, LanguageHelper.Tr("Coordinates"), "Coordinates", "coor");
            SetFunctionByElement(barButtonItemDistance, Distance, LanguageHelper.Tr("Distance"), "Distance", "di");
            SetFunctionByElement(barButtonItemMemo, Memo, LanguageHelper.Tr("Memo"), "Memo", "me");
            SetFunctionByElement(barButtonItemClearAnnotations, ClearAnnotations, LanguageHelper.Tr("Clear annotations"), "ClearAnnotations", "ca");

            // measure
            SetFunctionByElement(barButtonItemArea, Area, LanguageHelper.Tr("Area"), "Area", "ar");
            SetFunctionByElement(barButtonItemVolume, Volume, LanguageHelper.Tr("Volume"), "Volume", "v");

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
            SetFunctionByElement(barButtonItemList, List, LanguageHelper.Tr("List"), "List", "list");

            // options
            SetFunctionByElement(barButtonItem2D, Set2DView, LanguageHelper.Tr("2D View"), null, null);
            SetFunctionByElement(barButtonItem3D, Set3DView, LanguageHelper.Tr("3D View"), null, null);
            SetFunctionByElement(barButtonItemRendered, Rendered, LanguageHelper.Tr("Rendered"), null, null);
            SetFunctionByElement(barButtonItemShaded, Shaded, LanguageHelper.Tr("Shaded"), null, null);
            SetFunctionByElement(barButtonItemHiddenLines, HiddenLines, LanguageHelper.Tr("Hidden lines"), null, null);
            SetFunctionByElement(barButtonItemWireframe, Wireframe, LanguageHelper.Tr("Wireframe"), null, null);

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
        }

        async void Subtract() => await new ActionSubtract(model).RunAsync();
        async void Union() => await new ActionUnion(model).RunAsync();
        async void Intersection3D() => await new ActionIntersection(model).RunAsync();
        async void Align() => await new ActionAlign(model).RunAsync();
        async void SmartExtrude() => await new ActionSmartExtrude(model).RunAsync();

        async void ExtractFace() => await new ActionExtractFace(model).RunAsync();

        async void CreateTerrain() => await new ActionCreateTerrain(model).RunAsync();
        async void ColoringTerrain() => await new ActionColoringTerrain(model).RunAsync();
        async void CreateContour() => await new ActionCreateContour(model).RunAsync();
        async void ImportTerrain() => await new ActionImportTerrain(model).RunAsync();
        async void ExportTerrain() => await new ActionExportTerrain(model).RunAsync();
        async void UpDownTerrain() => await new ActionUpDownTerrain(model).RunAsync();


        async void Workspace() => await new ActionWorkspace(model).RunAsync();
        async void DrawRegion() => await new ActionRegion(model).RunAsync();
        async void InsertImage() => await new ActionInsertImage(model).RunAsync();
        async void InsertModel() => await new ActionInsertModel(model).RunAsync();
        async void InsertBlock() => await new ActionInsertBlock(model).RunAsync();
        async void DimLeader() => await new ActionDimLeader(model).RunAsync();
        async void DimAlign() => await new ActionDimLinear(model) { dimDirection = ActionDimLinear.DimDirection.aligned }.RunAsync();
        async void DimVertical() => await new ActionDimLinear(model) { dimDirection = ActionDimLinear.DimDirection.vertical }.RunAsync();
        async void DimHorizontal() => await new ActionDimLinear(model) { dimDirection = ActionDimLinear.DimDirection.horizontal }.RunAsync();
        async void DimRadius() => await new ActionDimDiameter(model) { radius = true }.RunAsync();
        async void DimDiameter() => await new ActionDimDiameter(model).RunAsync();
        async void MakeBlock() => await new ActionBlock(model).RunAsync();
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
        async void Sphere() => await new ActionSphere(model).RunAsync();
        async void Box() => await new ActionBox(model).RunAsync();
        async void Cone() => await new ActionCone(model).RunAsync();
        async void Torus() => await new ActionTorus(model).RunAsync();

        async void Revolve() => await new ActionRevolve(model).RunAsync();
        async void Loft() => await new ActionLoft(model).RunAsync();


        async void ArcCenterStartEnd() => await new ActionArc(model, ActionArc.Method.centerStartEnd).RunAsync();
        async void ArcFirstSecondThird() => await new ActionArc(model, ActionArc.Method.firstSecondThird).RunAsync();
        async void Arc() => await new ActionArc(model, ActionArc.Method.firstSecondThird).RunAsync();
        async void CircleByCenterRadius() => await new ActionCircle(model, ActionCircle.Method.centerRadius).RunAsync();
        async void CircleByFirstSecondThird() => await new ActionCircle(model, ActionCircle.Method.threePoints).RunAsync();
        async void CircleByFirstSecond() => await new ActionCircle(model, ActionCircle.Method.twoPoints).RunAsync();
        async void Circle() => await new ActionCircle(model).RunAsync();
        async void Line() => await new ActionLine(model).RunAsync();
        void RegenAll()
        {
            if (isDwg)
                Flag2D3D(true);
            else
                Flag2D3D(false);

            // zoom fit
            foreach (Viewport v in model.Viewports)
                v.ZoomFit();


            // object tree 갱신
            ObjectTreeListHelper.RegenAsync(treeListObject, model, isDwg);

            RefreshDataSource();
        }
        void List() => new ActionList(model).Run();
        void LineType() => new ActionLineType(model, this).Run();
        void TextStyle() => new ActionTextStyle(model, this).Run();
        void Layer() => new ActionLayer(model, this).Run();
        void About() => new FormAbout().ShowDialog();
        void RunOptions()
        {
            FormOptions form = new FormOptions();
            if (form.ShowDialog() == DialogResult.OK)
            {
                bool regen = Options.Instance.IsNeedRegen(form.lastOptions);
                if (regen)
                {
                    if (form.lastOptions.curLinetypeScale != Options.Instance.curLinetypeScale)
                    {
                        // ltscale이 변경되었으면 객체에 반영해야 함
                        foreach (var ent in model.Entities)
                        {
                            ent.LineTypeScale = Options.Instance.curLinetypeScale;
                        }
                    }
                }

                ApplyOptions(regen);
            }
        }
        void Homepage() => System.Diagnostics.Process.Start("http://hileejaeho.cafe24.com/kr-br3d/");
        void Language() { }

        void Set2DView() => Flag2D3D(true);
        void Set3DView() => Flag2D3D(false);
        void Rendered()
        {
            FlagDisplayMode(displayType.Rendered);
        }
        void Shaded()
        {
            FlagDisplayMode(displayType.Shaded);
        }
        void HiddenLines()
        {
            FlagDisplayMode(displayType.HiddenLines);
        }
        void Wireframe()
        {
            FlagDisplayMode(displayType.Wireframe);
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
            if (e.WorkUnit is ReadFileAsync rfa)
            {
                // 파일 열기에 성공했으면 new 를 한다.
                if (openMode)
                {
                    NewFile();
                }

                // viewport에 추가한다.
                rfa.AddToScene(model);

                UpdateCurCombos();

                // layer color을 background에 따라 변경(검은색을 흰색으로 또는 흰색을 검은색으로)
                hModel.SetLayerColorByBackgroundColor();

                opendFilePath = rfa.FileName;
                this.Text = $"{VersionHelper.appName} - {opendFilePath}";

                RegenAll();
            }
            else if (e.WorkUnit is WriteFileAsync wfa)
            {
                modifiedFile = false;
                opendFilePath = wfa.FileName;
                this.Text = $"{VersionHelper.appName} - {opendFilePath}";
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

        // display mode 설정 / 버튼 체크
        void FlagDisplayMode(displayType displayMode)
        {
            if (hModel == null)
                return;
            if (displayMode == displayType.Rendered)
                hModel.ActiveViewport.DisplayMode = displayType.Rendered;
            else if (displayMode == displayType.Shaded)
                hModel.ActiveViewport.DisplayMode = displayType.Shaded;
            else if (displayMode == displayType.HiddenLines)
                hModel.ActiveViewport.DisplayMode = displayType.HiddenLines;
            else if (displayMode == displayType.Wireframe)
                hModel.ActiveViewport.DisplayMode = displayType.Wireframe;
            hModel.Invalidate();
            UpdateDisplayModeButton();
        }

        void UpdateDisplayModeButton()
        {
            if (hModel == null)
                return;

            var buttonItem = barButtonItemRendered;
            if (hModel.ActiveViewport.DisplayMode == displayType.Rendered)
                buttonItem = barButtonItemRendered;
            else if (hModel.ActiveViewport.DisplayMode == displayType.Shaded)
                buttonItem = barButtonItemShaded;
            else if (hModel.ActiveViewport.DisplayMode == displayType.HiddenLines)
                buttonItem = barButtonItemHiddenLines;
            else if (hModel.ActiveViewport.DisplayMode == displayType.Wireframe)
                buttonItem = barButtonItemWireframe;

            barSubItemDisplayMode.ImageOptions.Image = buttonItem.ImageOptions.Image;
        }

        // 2D/3D View 설정 / 버튼 체크
        void Flag2D3D(bool set2DView)
        {
            if (hModel == null)
                return;

            if (set2DView)
                hModel.Set2DView();
            else
                hModel.Set3DView();
            Update2D3DButton();
        }

        void Update2D3DButton()
        {
            if (hModel == null)
                return;

            var buttonItem = barButtonItem2D;
            if (!hModel.IsTopViewOnly(hModel.ActiveViewport))
                buttonItem = barButtonItem3D;

            barSubItem2D3D.ImageOptions.Image = buttonItem.ImageOptions.Image;
        }

        // 
        void OrthoMode() => FlagOrthoMode(barButtonItemOrthoMode);
        void End() => controlModel.End();
        void Middle() => controlModel.Middle();
        void Point() => controlModel.Point();
        void Intersection() => controlModel.Intersection();
        void Center() => controlModel.Center();
        void ViewportSingle() => controlModel.ViewportSingle();
        void Viewport1x1() => controlModel.Viewport1x1();
        void Viewport1x2() => controlModel.Viewport1x2();
        void Viewport2x2() => controlModel.Viewport2x2();


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

        async void Volume()
        {
            ActionVolume ac = new ActionVolume(model, ActionVolume.ShowResult.form);
            await ac.RunAsync();
        }

        async void Area()
        {
            ActionArea ac = new ActionArea(model, ActionArea.ShowResult.form);
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

        // 편집된 내용 저장 체크
        bool CheckSaveForModifiedFile()
        {
            // 이미 편집중인 내용이 있으면 저장할지 묻는다.
            if (modifiedFile)
            {
                var re = XtraMessageBox.Show(LanguageHelper.Tr("Do you want to save the file you are editing?"), LanguageHelper.Tr("Save"), MessageBoxButtons.YesNoCancel);
                if (re == DialogResult.Cancel)
                    return false;
                if (re == DialogResult.Yes)
                {
                    Save();
                }
            }

            return true;
        }


        void New()
        {
            if (!CheckSaveForModifiedFile())
                return;

            NewFile();
        }

        // 현재 파일 다시 로딩하기
        void Reload()
        {
            var re = XtraMessageBox.Show(LanguageHelper.Tr("Do you want to reload the current file?"), LanguageHelper.Tr("Reload"), MessageBoxButtons.YesNo);
            if (re == DialogResult.No)
                return;

            Import(opendFilePath, true);
        }

        void Open()
        {
            if (!CheckSaveForModifiedFile())
                return;

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

            // 이미 열려 있는 파일이면 리턴
            if (opendFilePath.Equals(dlg.FileName))
                return;

            Import(dlg.FileName);
        }

        // 파일이 열려 있으면 바로 저장
        void Save()
        {
            if (string.IsNullOrEmpty(opendFilePath))
            {
                SaveAs();
            }
            else
            {
                Export(opendFilePath);
            }
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

        // openMode : 파일읽기를 설공하면 현재 상태를 초기화 하고 연다.
        void Import(string pathFileName, bool openMode = true)
        {
            this.openMode = openMode;

            try
            {
                devDept.Eyeshot.Translators.ReadFileAsync rf = FileHelper.GetReadFileAsync(pathFileName);
                if (rf == null)
                {
                    XtraMessageBox.Show(LanguageHelper.Tr($"Unsupported file."));
                    return;
                }

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
                {
                    SaveAs();
                    return;
                }

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

            UpdateCurCombos();
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
            try
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
            catch
            {

            }

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
                if (keyData == Keys.Escape)
                {
                    RefreshPropertyGridControl(null);
                    UpdateCurCombos();
                }
            }

            if (keyData == Keys.F8)
            {
                OrthoMode();
            }

            return base.ProcessCmdKey(ref msg, keyData);
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






        private void barButtonItemDrawCircle_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void popupMenuDrawCircle_BeforePopup(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
    }
}