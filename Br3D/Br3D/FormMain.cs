﻿using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Translators;
using devDept.Geometry;
using DevExpress.XtraBars.Navigation;
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
        Dictionary<NavElement, Action> functionByElement = new Dictionary<NavElement, Action>();
        string opendFilePath = "";
        bool isDwg => string.IsNullOrEmpty(opendFilePath) ? false : Path.GetExtension(opendFilePath).ToLower().EndsWith("dwg");
        
        public FormMain()
        {
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

            Options.Instance.appName = "Br3D";
            Options.Instance.LoadOptions();

            LanguageHelper.Load(Options.Instance.language);
            InitGraphics();
            InitDisplayMode();

            InitSnapping();
            InitTileElementMethod();
            InitTileElementStatus();
            InitObjectTreeList();
            InitPropertyGrid();

            InitToolbar();
            Translate();

            hModel.ActionMode = actionType.None;
        }

       

        private void FormMain_Load(object sender, EventArgs e)
        {
            ViewportSingle();

            // test
            Text t = new Text(new Point3D(0, 0, 0), "asdf", 12);

            model.TextStyles[0].FontFamilyName = "Arial";

            model.Entities.Regen();
            model.Entities.Add(t);
        }


        private void Model_MouseMove(object sender, MouseEventArgs e)
        {
            hModel.gripManager.MouseMove(e);

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

            var point = ActionBase.GetPoint3DWithSnapping(hModel, e);
            if (point == null)
                return;

            var text = "";
            if (hModel.TopViewOnly)
                text = Units.GetPointString(point.X, point.Y);
            else
                text = Units.GetPointString(point.X, point.Y, point.Z);
            toolStripStatusLabelCoordinates.Text = text;
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
            // navButton
            navButtonHome.Caption = LanguageHelper.Tr("Home");

            // category
            tileNavCategoryOptions.Caption = LanguageHelper.Tr("Options");
            tileNavCategoryOsnap.Caption = LanguageHelper.Tr("Osnap");
            tileNavCategoryAnnotation.Caption = LanguageHelper.Tr("Annotation");
            tileNavCategoryViewport.Caption = LanguageHelper.Tr("Viewport");
            tileNavCategoryTools.Caption = LanguageHelper.Tr("Tools");


            //tile
            SetTileText(tileNavItemOpen, LanguageHelper.Tr("Open"));
            SetTileText(tileNavItemSaveAs, LanguageHelper.Tr("Save As"));
            SetTileText(tileNavItemSaveImage, LanguageHelper.Tr("Save Image"));
            SetTileText(tileNavItemExit, LanguageHelper.Tr("Exit"));
            SetTileText(tileNavItemEnd, LanguageHelper.Tr("End Point"));
            SetTileText(tileNavItemIntersection, LanguageHelper.Tr("Intersection Point"));
            SetTileText(tileNavItemCenter, LanguageHelper.Tr("Center Point"));
            SetTileText(tileNavItemPoint, LanguageHelper.Tr("Point"));
            SetTileText(tileNavItemMiddle, LanguageHelper.Tr("Middle Point"));
            SetTileText(tileNavItemCoordinates, LanguageHelper.Tr("Coordinates"));
            SetTileText(tileNavItemDistance, LanguageHelper.Tr("Distance"));
            SetTileText(tileNavItemMemo, LanguageHelper.Tr("Memo"));
            SetTileText(tileNavItemClearAnnotations, LanguageHelper.Tr("Clear Annotations"));
            SetTileText(tileNavItemLanguage, LanguageHelper.Tr("Language"));

            SetTileText(tileNavItemCheckForUpdate, LanguageHelper.Tr("Check For Update"));
            SetTileText(tileNavItemHomePage, LanguageHelper.Tr("Homepage"));
            SetTileText(tileNavItemAbout, LanguageHelper.Tr("About"));

            SetTileText(tileNavItemLayer, LanguageHelper.Tr("Layer"));
            SetTileText(tileNavItemLineType, LanguageHelper.Tr("Line Type"));
            SetTileText(tileNavItemTextStyle, LanguageHelper.Tr("Text Style"));

            // sub tile

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

        private void SetTileText(TileNavItem tileNavItem, string text)
        {


            tileNavItem.TileText = text;
            tileNavItem.Caption = text;
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
                var item = model.GetItemUnderMouseCursor(e.Location);
                if (item != null)
                    item.Item.Selected = true;

                // 그림 관련
                if (hModel?.gripManager != null)
                    hModel?.gripManager.MouseUp(e);
                    

                // 속성창 갱신
                if (propertyGridControl1.Visible)
                    RefreshPropertyGridControl(item?.Item);

                // tree에서 선택
                if (treeListObject.Visible)
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
            tileNavSubItemKorean.Tile.Checked = false;
            tileNavSubItemEnglish.Tile.Checked = false;
            if (Options.Instance.language == "ko-KR")
                tileNavSubItemKorean.Tile.Checked = true;
            else if (Options.Instance.language == "en-US")
                tileNavSubItemEnglish.Tile.Checked = true;
        }

        // element별 method 목록 초기화
        private void InitTileElementMethod()
        {
            functionByElement.Add(tileNavItemOpen, Open);
            functionByElement.Add(tileNavItemSaveAs, SaveAs);
            functionByElement.Add(tileNavItemSaveImage, SaveImage);
            functionByElement.Add(tileNavItemExit, Close);
            functionByElement.Add(tileNavItemRegenAll, RegenAll);
            functionByElement.Add(tileNavItemCoordinates, Coorindates);
            functionByElement.Add(tileNavItemDistance, Distance);
            functionByElement.Add(tileNavItemMemo, Memo);
            functionByElement.Add(tileNavItemClearAnnotations, ClearAnnotations);
            functionByElement.Add(tileNavItemEnd, End);
            functionByElement.Add(tileNavItemIntersection, Intersection);
            functionByElement.Add(tileNavItemMiddle, Middle);
            functionByElement.Add(tileNavItemCenter, Center);
            functionByElement.Add(tileNavItemPoint, Point);

            functionByElement.Add(tileNavItemViewportsingle, ViewportSingle);
            functionByElement.Add(tileNavItemViewport1x1, Viewport1x1);
            functionByElement.Add(tileNavItemViewport1x2, Viewport1x2);
            functionByElement.Add(tileNavItemViewport2x2, Viewport2x2);

            functionByElement.Add(tileNavItemLanguage, Language);
            functionByElement.Add(tileNavSubItemKorean, Korean);
            functionByElement.Add(tileNavSubItemEnglish, English);

            functionByElement.Add(tileNavItemHomePage, Homepage);
            functionByElement.Add(tileNavItemCheckForUpdate, CheckForUpdate);
            functionByElement.Add(tileNavItemAbout, About);
            functionByElement.Add(tileNavItemLayer, Layer);
            functionByElement.Add(tileNavItemTextStyle, TextStyle);
            functionByElement.Add(tileNavItemLineType, LineType);
        }



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
        

        void LineType()
        {
            ActionLineType ac = new ActionLineType(model, this);
            ac.Run();
        }

        void TextStyle()
        {
            ActionTextStyle ac = new ActionTextStyle(model, this);
            ac.Run();
        }

        void Layer()
        {
            ActionLayer ac = new ActionLayer(model, this);
            ac.Run();
        }

        void About()
        {
            FormAbout form = new FormAbout();
            form.ShowDialog();

        }
        void CheckForUpdate()
        {
            // 업데이트 체크
            var filePath = Path.Combine(hanee.ThreeD.Util.GetExePath(), "wyUpdate.exe");
            if (File.Exists(filePath))
            {
                System.Diagnostics.Process.Start(filePath);
            }
            else
                XtraMessageBox.Show(LanguageHelper.Tr("Update check failed! - wyUpdate.exe not found!"));
        }

        void Homepage()
        {
            System.Diagnostics.Process.Start("http://hileejaeho.cafe24.com/kr-br3d/");
        }

        void Language()
        {
            tileNavItemLanguage.Tile.ShowDropDown();
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

        private void tileNavPane1_ElementClick(object sender, DevExpress.XtraBars.Navigation.NavElementEventArgs e)
        {
            if (!e.IsTile)
                return;

            if (functionByElement.TryGetValue(e.Element, out Action act))
            {
                act();
            }
            else
            {
#if DEBUG
                MessageBox.Show("undefined function");
#endif
            }
        }

        // 
        void FlagOsnap(DevExpress.XtraBars.Navigation.TileNavItem tile, Snapping.objectSnapType snapType)
        {
            HModel hModel = model as HModel;
            if (hModel == null)
                return;

            hModel.Snapping.FlagActiveObjectSnap(snapType);
            tile.Tile.Checked = hModel.Snapping.IsActiveObjectSnap(snapType);
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

        void End() => FlagOsnap(tileNavItemEnd, Snapping.objectSnapType.End);
        void Middle() => FlagOsnap(tileNavItemMiddle, Snapping.objectSnapType.Mid);
        void Point() => FlagOsnap(tileNavItemPoint, Snapping.objectSnapType.Point);
        void Intersection() => FlagOsnap(tileNavItemIntersection, Snapping.objectSnapType.Intersect);
        void Center() => FlagOsnap(tileNavItemCenter, Snapping.objectSnapType.Center);

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
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Bitmap (*.bmp)|*.bmp|" +
                "Portable Network Graphics (*.png)|*.png|" +
                "Windows metafile (*.wmf)|*.wmf|" +
                "Enhanced Windows Metafile (*.emf)|*.emf";

            dlg.FilterIndex = 2;
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == DialogResult.OK)
            {

                switch (dlg.FilterIndex)
                {

                    case 1:
                        model.WriteToFileRaster(2, dlg.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                    case 2:
                        model.WriteToFileRaster(2, dlg.FileName, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    case 3:
                        model.WriteToFileRaster(2, dlg.FileName, System.Drawing.Imaging.ImageFormat.Wmf);
                        break;
                    case 4:
                        model.WriteToFileRaster(2, dlg.FileName, System.Drawing.Imaging.ImageFormat.Emf);
                        break;

                }

            }
        }

        void Open()
        {
            // 파일 선택
            OpenFileDialog openFile = new OpenFileDialog();


            Dictionary<string, string> additionalSupportFormats = new Dictionary<string, string>();
            //additionalSupportFormats.Add("Br3D(model, drawings)", "*.br3");
            openFile.Filter = FileHelper.FilterForOpenDialog(additionalSupportFormats);
            openFile.FilterIndex = 0;
            openFile.AddExtension = true;
            openFile.CheckFileExists = true;
            openFile.CheckPathExists = true;
            if (openFile.ShowDialog() != DialogResult.OK)
                return;

            NewFile();

            Import(openFile.FileName);
        }

        void SaveAs()
        {
            SaveFileDialog dlg = new SaveFileDialog();
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
        }


    }
}