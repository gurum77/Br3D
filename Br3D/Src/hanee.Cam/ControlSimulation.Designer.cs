
namespace hanee.Cam
{
    partial class ControlSimulation
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            devDept.Eyeshot.CancelToolBarButton cancelToolBarButton1 = new devDept.Eyeshot.CancelToolBarButton("Cancel", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ProgressBar progressBar1 = new devDept.Eyeshot.ProgressBar(devDept.Eyeshot.ProgressBar.styleType.Circular, 0, "Idle", System.Drawing.Color.Black, System.Drawing.Color.Transparent, System.Drawing.Color.Green, 1D, true, cancelToolBarButton1, false, 0.1D, 0.333D, true);
            devDept.Eyeshot.SimulationTimeLine simulationTimeLine1 = new devDept.Eyeshot.SimulationTimeLine(System.Drawing.Color.DarkGray, System.Drawing.Color.Black, System.Drawing.Color.Orange, System.Drawing.Color.Green, System.Drawing.Color.Red, 1D, true, 0.01D);
            devDept.Graphics.BackgroundSettings backgroundSettings1 = new devDept.Graphics.BackgroundSettings(devDept.Graphics.backgroundStyleType.Solid, System.Drawing.Color.DeepSkyBlue, System.Drawing.Color.DodgerBlue, System.Drawing.Color.WhiteSmoke, 0.75D, null, devDept.Graphics.colorThemeType.Auto, 0.33D);
            devDept.Eyeshot.Camera camera1 = new devDept.Eyeshot.Camera(new devDept.Geometry.Point3D(64.0062895995168D, -124.79438226926727D, 110.81242132330227D), 331.10339631698054D, new devDept.Geometry.Quaternion(0.12940952255126034D, 0.22414386804201339D, 0.4829629131445341D, 0.83651630373780794D), devDept.Graphics.projectionType.Perspective, 50D, 28.148344931224457D, false, 0.001D);
            devDept.Eyeshot.BeginningToolBarButton beginningToolBarButton1 = new devDept.Eyeshot.BeginningToolBarButton("Go to beginning", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, false);
            devDept.Eyeshot.PreviousToolBarButton previousToolBarButton1 = new devDept.Eyeshot.PreviousToolBarButton("Go to previous (Ctrl+L)", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, false);
            devDept.Eyeshot.StartToolBarButton startToolBarButton1 = new devDept.Eyeshot.StartToolBarButton("Start the simulation (Ctrl+Space)", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, false);
            devDept.Eyeshot.PauseToolBarButton pauseToolBarButton1 = new devDept.Eyeshot.PauseToolBarButton("Pause the simulation (Ctrl+Space)", devDept.Eyeshot.ToolBarButton.styleType.PushButton, false, false);
            devDept.Eyeshot.NextToolBarButton nextToolBarButton1 = new devDept.Eyeshot.NextToolBarButton("Go to next (Ctrl+R)", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, false);
            devDept.Eyeshot.EndToolBarButton endToolBarButton1 = new devDept.Eyeshot.EndToolBarButton("Go to end", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, false);
            devDept.Eyeshot.ToolBar toolBar1 = new devDept.Eyeshot.ToolBar(devDept.Eyeshot.ToolBar.positionType.HorizontalBottomCenter, true, new devDept.Eyeshot.ToolBarButton[] {
            ((devDept.Eyeshot.ToolBarButton)(beginningToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(previousToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(startToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(pauseToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(nextToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(endToolBarButton1))});
            devDept.Eyeshot.HomeToolBarButton homeToolBarButton1 = new devDept.Eyeshot.HomeToolBarButton("Home", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.MagnifyingGlassToolBarButton magnifyingGlassToolBarButton1 = new devDept.Eyeshot.MagnifyingGlassToolBarButton("Magnifying Glass", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomWindowToolBarButton zoomWindowToolBarButton1 = new devDept.Eyeshot.ZoomWindowToolBarButton("Zoom Window", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomToolBarButton zoomToolBarButton1 = new devDept.Eyeshot.ZoomToolBarButton("Zoom", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.PanToolBarButton panToolBarButton1 = new devDept.Eyeshot.PanToolBarButton("Pan", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.RotateToolBarButton rotateToolBarButton1 = new devDept.Eyeshot.RotateToolBarButton("Rotate", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomFitToolBarButton zoomFitToolBarButton1 = new devDept.Eyeshot.ZoomFitToolBarButton("Zoom Fit", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.ToolBar toolBar2 = new devDept.Eyeshot.ToolBar(devDept.Eyeshot.ToolBar.positionType.HorizontalTopCenter, true, new devDept.Eyeshot.ToolBarButton[] {
            ((devDept.Eyeshot.ToolBarButton)(homeToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(magnifyingGlassToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(zoomWindowToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(zoomToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(panToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(rotateToolBarButton1)),
            ((devDept.Eyeshot.ToolBarButton)(zoomFitToolBarButton1))});
            devDept.Eyeshot.Grid grid1 = new devDept.Eyeshot.Grid(new devDept.Geometry.Point2D(-100D, -100D), new devDept.Geometry.Point2D(100D, 100D), 10D, new devDept.Geometry.Plane(new devDept.Geometry.Point3D(0D, 0D, 0D), new devDept.Geometry.Vector3D(0D, 0D, 1D)), System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0))))), false, true, false, false, 10, 100, 10, System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90))))), System.Drawing.Color.Transparent, false, System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255))))));
            devDept.Eyeshot.OriginSymbol originSymbol1 = new devDept.Eyeshot.OriginSymbol(10, devDept.Eyeshot.originSymbolStyleType.Ball, new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129))), System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Red, System.Drawing.Color.Green, System.Drawing.Color.Blue, "Origin", "X", "Y", "Z", false, null, false);
            devDept.Eyeshot.RotateSettings rotateSettings1 = new devDept.Eyeshot.RotateSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.None), 10D, true, 1D, devDept.Eyeshot.rotationType.Trackball, devDept.Eyeshot.rotationCenterType.CursorLocation, new devDept.Geometry.Point3D(0D, 0D, 0D), false);
            devDept.Eyeshot.ZoomSettings zoomSettings1 = new devDept.Eyeshot.ZoomSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Shift), 25, true, devDept.Eyeshot.zoomStyleType.AtCursorLocation, false, 1D, System.Drawing.Color.Empty, devDept.Eyeshot.Camera.perspectiveFitType.Accurate, false, 10, true);
            devDept.Eyeshot.PanSettings panSettings1 = new devDept.Eyeshot.PanSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Ctrl), 25, true);
            devDept.Eyeshot.NavigationSettings navigationSettings1 = new devDept.Eyeshot.NavigationSettings(devDept.Eyeshot.Camera.navigationType.Examine, new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Left, devDept.Eyeshot.modifierKeys.None), new devDept.Geometry.Point3D(-1000D, -1000D, -1000D), new devDept.Geometry.Point3D(1000D, 1000D, 1000D), 8D, 50D, 50D);
            devDept.Eyeshot.CoordinateSystemIcon coordinateSystemIcon1 = new devDept.Eyeshot.CoordinateSystemIcon(new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129))), System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80))))), System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80))))), System.Drawing.Color.OrangeRed, "Origin", "X", "Y", "Z", true, devDept.Eyeshot.coordinateSystemPositionType.BottomLeft, 37, null, false);
            devDept.Eyeshot.ViewCubeIcon viewCubeIcon1 = new devDept.Eyeshot.ViewCubeIcon(devDept.Eyeshot.coordinateSystemPositionType.TopRight, true, System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(20)))), ((int)(((byte)(60))))), true, "FRONT", "BACK", "LEFT", "RIGHT", "TOP", "BOTTOM", System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), 'S', 'N', 'W', 'E', true, null, System.Drawing.Color.White, System.Drawing.Color.Black, 120, true, true, null, null, null, null, null, null, false, new devDept.Geometry.Quaternion(0D, 0D, 0D, 1D));
            devDept.Eyeshot.Viewport.SavedViewsManager savedViewsManager1 = new devDept.Eyeshot.Viewport.SavedViewsManager(8);
            devDept.Eyeshot.Viewport viewport1 = new devDept.Eyeshot.Viewport(new System.Drawing.Point(0, 0), new System.Drawing.Size(786, 560), backgroundSettings1, camera1, new devDept.Eyeshot.ToolBar[] {
            toolBar1,
            toolBar2}, new devDept.Eyeshot.Legend[0], devDept.Eyeshot.displayType.Rendered, true, false, false, false, new devDept.Eyeshot.Grid[] {
            grid1}, new devDept.Eyeshot.OriginSymbol[] {
            originSymbol1}, false, rotateSettings1, zoomSettings1, panSettings1, navigationSettings1, coordinateSystemIcon1, viewCubeIcon1, savedViewsManager1, devDept.Eyeshot.viewType.Trimetric);
            this.manufacture1 = new devDept.Eyeshot.Manufacture();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.dockPanel1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.groupControlInfo = new DevExpress.XtraEditors.GroupControl();
            this.gridControlInfo = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnTitle = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnContents = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControlSpeed = new DevExpress.XtraEditors.GroupControl();
            this.trackBarControl1 = new DevExpress.XtraEditors.TrackBarControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.checkEditViewPoints = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditViewToolpath = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditViewTool = new DevExpress.XtraEditors.CheckEdit();
            this.checkEditViewStock = new DevExpress.XtraEditors.CheckEdit();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.manufacture1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.dockPanel1.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlInfo)).BeginInit();
            this.groupControlInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSpeed)).BeginInit();
            this.groupControlSpeed.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditViewPoints.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditViewToolpath.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditViewTool.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditViewStock.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // manufacture1
            // 
            this.manufacture1.Cursor = System.Windows.Forms.Cursors.Default;
            this.manufacture1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.manufacture1.Location = new System.Drawing.Point(200, 0);
            this.manufacture1.Name = "manufacture1";
            this.manufacture1.ProgressBar = progressBar1;
            this.manufacture1.SimulationTimeLine = simulationTimeLine1;
            this.manufacture1.Size = new System.Drawing.Size(786, 560);
            this.manufacture1.StopOnCollision = false;
            this.manufacture1.TabIndex = 2;
            this.manufacture1.Text = "manufacture1";
            this.manufacture1.Viewports.Add(viewport1);
            // 
            // dockManager1
            // 
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanel1});
            this.dockManager1.TopZIndexControls.AddRange(new string[] {
            "DevExpress.XtraBars.BarDockControl",
            "DevExpress.XtraBars.StandaloneBarDockControl",
            "System.Windows.Forms.MenuStrip",
            "System.Windows.Forms.StatusStrip",
            "System.Windows.Forms.StatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonStatusBar",
            "DevExpress.XtraBars.Ribbon.RibbonControl",
            "DevExpress.XtraBars.Navigation.OfficeNavigationBar",
            "DevExpress.XtraBars.Navigation.TileNavPane",
            "DevExpress.XtraBars.TabFormControl",
            "DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormControl",
            "DevExpress.XtraBars.ToolbarForm.ToolbarFormControl"});
            // 
            // dockPanel1
            // 
            this.dockPanel1.Controls.Add(this.dockPanel1_Container);
            this.dockPanel1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.dockPanel1.ID = new System.Guid("cb846a53-2595-43ed-8f48-950ed62722ca");
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.OriginalSize = new System.Drawing.Size(200, 200);
            this.dockPanel1.Size = new System.Drawing.Size(200, 560);
            this.dockPanel1.Text = "dockPanel1";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.groupControlInfo);
            this.dockPanel1_Container.Controls.Add(this.groupControlSpeed);
            this.dockPanel1_Container.Controls.Add(this.groupControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(3, 26);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(193, 531);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // groupControlInfo
            // 
            this.groupControlInfo.Controls.Add(this.gridControlInfo);
            this.groupControlInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControlInfo.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.groupControlInfo.Location = new System.Drawing.Point(0, 196);
            this.groupControlInfo.Name = "groupControlInfo";
            this.groupControlInfo.Padding = new System.Windows.Forms.Padding(10, 9, 10, 9);
            this.groupControlInfo.Size = new System.Drawing.Size(193, 335);
            this.groupControlInfo.TabIndex = 6;
            this.groupControlInfo.Text = "Infomation";
            // 
            // gridControlInfo
            // 
            this.gridControlInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlInfo.Location = new System.Drawing.Point(12, 32);
            this.gridControlInfo.MainView = this.gridView1;
            this.gridControlInfo.Name = "gridControlInfo";
            this.gridControlInfo.Size = new System.Drawing.Size(169, 292);
            this.gridControlInfo.TabIndex = 0;
            this.gridControlInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnTitle,
            this.gridColumnContents});
            this.gridView1.DetailHeight = 327;
            this.gridView1.GridControl = this.gridControlInfo;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // gridColumnTitle
            // 
            this.gridColumnTitle.Caption = "Title";
            this.gridColumnTitle.FieldName = "title";
            this.gridColumnTitle.Name = "gridColumnTitle";
            this.gridColumnTitle.Visible = true;
            this.gridColumnTitle.VisibleIndex = 0;
            this.gridColumnTitle.Width = 70;
            // 
            // gridColumnContents
            // 
            this.gridColumnContents.Caption = "Contents";
            this.gridColumnContents.FieldName = "contents";
            this.gridColumnContents.Name = "gridColumnContents";
            this.gridColumnContents.Visible = true;
            this.gridColumnContents.VisibleIndex = 1;
            this.gridColumnContents.Width = 119;
            // 
            // groupControlSpeed
            // 
            this.groupControlSpeed.Controls.Add(this.trackBarControl1);
            this.groupControlSpeed.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControlSpeed.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.groupControlSpeed.Location = new System.Drawing.Point(0, 131);
            this.groupControlSpeed.Name = "groupControlSpeed";
            this.groupControlSpeed.Size = new System.Drawing.Size(193, 65);
            this.groupControlSpeed.TabIndex = 5;
            this.groupControlSpeed.Text = "Speed";
            // 
            // trackBarControl1
            // 
            this.trackBarControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trackBarControl1.EditValue = 1;
            this.trackBarControl1.Location = new System.Drawing.Point(2, 23);
            this.trackBarControl1.Name = "trackBarControl1";
            this.trackBarControl1.Properties.LabelAppearance.Options.UseTextOptions = true;
            this.trackBarControl1.Properties.LabelAppearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.trackBarControl1.Properties.Maximum = 20;
            this.trackBarControl1.Size = new System.Drawing.Size(189, 40);
            this.trackBarControl1.TabIndex = 0;
            this.trackBarControl1.Value = 1;
            this.trackBarControl1.EditValueChanged += new System.EventHandler(this.trackBarControl1_EditValueChanged);
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.checkEditViewPoints);
            this.groupControl1.Controls.Add(this.checkEditViewToolpath);
            this.groupControl1.Controls.Add(this.checkEditViewTool);
            this.groupControl1.Controls.Add(this.checkEditViewStock);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.GroupStyle = DevExpress.Utils.GroupStyle.Light;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(193, 131);
            this.groupControl1.TabIndex = 4;
            this.groupControl1.Text = "View";
            // 
            // checkEditViewPoints
            // 
            this.checkEditViewPoints.Location = new System.Drawing.Point(6, 100);
            this.checkEditViewPoints.Name = "checkEditViewPoints";
            this.checkEditViewPoints.Properties.Caption = "Points";
            this.checkEditViewPoints.Size = new System.Drawing.Size(160, 20);
            this.checkEditViewPoints.TabIndex = 3;
            this.checkEditViewPoints.CheckedChanged += new System.EventHandler(this.checkEditViewPoints_CheckedChanged);
            // 
            // checkEditViewToolpath
            // 
            this.checkEditViewToolpath.EditValue = true;
            this.checkEditViewToolpath.Location = new System.Drawing.Point(6, 76);
            this.checkEditViewToolpath.Name = "checkEditViewToolpath";
            this.checkEditViewToolpath.Properties.Caption = "Toolpath";
            this.checkEditViewToolpath.Size = new System.Drawing.Size(160, 20);
            this.checkEditViewToolpath.TabIndex = 2;
            this.checkEditViewToolpath.CheckedChanged += new System.EventHandler(this.checkEditViewToolpath_CheckedChanged);
            // 
            // checkEditViewTool
            // 
            this.checkEditViewTool.EditValue = true;
            this.checkEditViewTool.Location = new System.Drawing.Point(6, 51);
            this.checkEditViewTool.Name = "checkEditViewTool";
            this.checkEditViewTool.Properties.Caption = "Tool";
            this.checkEditViewTool.Size = new System.Drawing.Size(160, 20);
            this.checkEditViewTool.TabIndex = 1;
            this.checkEditViewTool.CheckedChanged += new System.EventHandler(this.checkEditViewTool_CheckedChanged);
            // 
            // checkEditViewStock
            // 
            this.checkEditViewStock.EditValue = true;
            this.checkEditViewStock.Location = new System.Drawing.Point(6, 27);
            this.checkEditViewStock.Name = "checkEditViewStock";
            this.checkEditViewStock.Properties.Caption = "Stock";
            this.checkEditViewStock.Size = new System.Drawing.Size(160, 20);
            this.checkEditViewStock.TabIndex = 0;
            this.checkEditViewStock.CheckedChanged += new System.EventHandler(this.checkEditViewStock_CheckedChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // ControlSimulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.manufacture1);
            this.Controls.Add(this.dockPanel1);
            this.Name = "ControlSimulation";
            this.Size = new System.Drawing.Size(986, 560);
            this.Load += new System.EventHandler(this.ControlSimulation_Load);
            ((System.ComponentModel.ISupportInitialize)(this.manufacture1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.dockPanel1.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControlInfo)).EndInit();
            this.groupControlInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControlSpeed)).EndInit();
            this.groupControlSpeed.ResumeLayout(false);
            this.groupControlSpeed.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.checkEditViewPoints.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditViewToolpath.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditViewTool.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.checkEditViewStock.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private devDept.Eyeshot.Manufacture manufacture1;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanel1;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraEditors.GroupControl groupControlSpeed;
        private DevExpress.XtraEditors.TrackBarControl trackBarControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraEditors.CheckEdit checkEditViewPoints;
        private DevExpress.XtraEditors.CheckEdit checkEditViewToolpath;
        private DevExpress.XtraEditors.CheckEdit checkEditViewTool;
        private DevExpress.XtraEditors.CheckEdit checkEditViewStock;
        private DevExpress.XtraEditors.GroupControl groupControlInfo;
        private DevExpress.XtraGrid.GridControl gridControlInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnTitle;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnContents;
        private System.Windows.Forms.Timer timer1;
    }
}
