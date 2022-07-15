
namespace Br3D
{
    partial class FormMain
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraEditors.TileItemElement tileItemElement2 = new DevExpress.XtraEditors.TileItemElement();
            devDept.Eyeshot.CancelToolBarButton cancelToolBarButton2 = new devDept.Eyeshot.CancelToolBarButton("Cancel", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ProgressBar progressBar2 = new devDept.Eyeshot.ProgressBar(devDept.Eyeshot.ProgressBar.styleType.Circular, 0, "Idle", System.Drawing.Color.Black, System.Drawing.Color.Transparent, System.Drawing.Color.Green, 1D, true, cancelToolBarButton2, false, 0.1D, 0.333D, true);
            devDept.Graphics.BackgroundSettings backgroundSettings5 = new devDept.Graphics.BackgroundSettings(devDept.Graphics.backgroundStyleType.LinearGradient, System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(82)))), ((int)(((byte)(103))))), System.Drawing.Color.DodgerBlue, System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(32)))), ((int)(((byte)(41))))), 0.75D, null, devDept.Graphics.colorThemeType.Auto, 0.33D);
            devDept.Eyeshot.Camera camera5 = new devDept.Eyeshot.Camera(new devDept.Geometry.Point3D(-1.2598973293902418D, 2.5657311010967412D, 42.261193606378384D), 307.26899936464309D, new devDept.Geometry.Quaternion(0.018434349666532512D, 0.039532590434972065D, 0.42221602280006187D, 0.90544518284475428D), devDept.Graphics.projectionType.Perspective, 40D, 1.801549330370017D, false, 0.001D);
            devDept.Eyeshot.HomeToolBarButton homeToolBarButton9 = new devDept.Eyeshot.HomeToolBarButton("Home", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.MagnifyingGlassToolBarButton magnifyingGlassToolBarButton9 = new devDept.Eyeshot.MagnifyingGlassToolBarButton("Magnifying Glass", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomWindowToolBarButton zoomWindowToolBarButton9 = new devDept.Eyeshot.ZoomWindowToolBarButton("Zoom Window", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomToolBarButton zoomToolBarButton9 = new devDept.Eyeshot.ZoomToolBarButton("Zoom", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.PanToolBarButton panToolBarButton9 = new devDept.Eyeshot.PanToolBarButton("Pan", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.RotateToolBarButton rotateToolBarButton9 = new devDept.Eyeshot.RotateToolBarButton("Rotate", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomFitToolBarButton zoomFitToolBarButton9 = new devDept.Eyeshot.ZoomFitToolBarButton("Zoom Fit", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.ToolBar toolBar9 = new devDept.Eyeshot.ToolBar(devDept.Eyeshot.ToolBar.positionType.HorizontalTopCenter, true, new devDept.Eyeshot.ToolBarButton[] {
            ((devDept.Eyeshot.ToolBarButton)(homeToolBarButton9)),
            ((devDept.Eyeshot.ToolBarButton)(magnifyingGlassToolBarButton9)),
            ((devDept.Eyeshot.ToolBarButton)(zoomWindowToolBarButton9)),
            ((devDept.Eyeshot.ToolBarButton)(zoomToolBarButton9)),
            ((devDept.Eyeshot.ToolBarButton)(panToolBarButton9)),
            ((devDept.Eyeshot.ToolBarButton)(rotateToolBarButton9)),
            ((devDept.Eyeshot.ToolBarButton)(zoomFitToolBarButton9))});
            devDept.Eyeshot.HomeToolBarButton homeToolBarButton10 = new devDept.Eyeshot.HomeToolBarButton("Home", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.MagnifyingGlassToolBarButton magnifyingGlassToolBarButton10 = new devDept.Eyeshot.MagnifyingGlassToolBarButton("Magnifying Glass", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomWindowToolBarButton zoomWindowToolBarButton10 = new devDept.Eyeshot.ZoomWindowToolBarButton("Zoom Window", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomToolBarButton zoomToolBarButton10 = new devDept.Eyeshot.ZoomToolBarButton("Zoom", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.PanToolBarButton panToolBarButton10 = new devDept.Eyeshot.PanToolBarButton("Pan", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.RotateToolBarButton rotateToolBarButton10 = new devDept.Eyeshot.RotateToolBarButton("Rotate", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomFitToolBarButton zoomFitToolBarButton10 = new devDept.Eyeshot.ZoomFitToolBarButton("Zoom Fit", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.ToolBar toolBar10 = new devDept.Eyeshot.ToolBar(devDept.Eyeshot.ToolBar.positionType.VerticalTopLeft, true, new devDept.Eyeshot.ToolBarButton[] {
            ((devDept.Eyeshot.ToolBarButton)(homeToolBarButton10)),
            ((devDept.Eyeshot.ToolBarButton)(magnifyingGlassToolBarButton10)),
            ((devDept.Eyeshot.ToolBarButton)(zoomWindowToolBarButton10)),
            ((devDept.Eyeshot.ToolBarButton)(zoomToolBarButton10)),
            ((devDept.Eyeshot.ToolBarButton)(panToolBarButton10)),
            ((devDept.Eyeshot.ToolBarButton)(rotateToolBarButton10)),
            ((devDept.Eyeshot.ToolBarButton)(zoomFitToolBarButton10))});
            devDept.Eyeshot.Grid grid9 = new devDept.Eyeshot.Grid(new devDept.Geometry.Point2D(-100D, -100D), new devDept.Geometry.Point2D(100D, 100D), 10D, new devDept.Geometry.Plane(new devDept.Geometry.Point3D(0D, 0D, 0D), new devDept.Geometry.Vector3D(0D, 0D, 1D)), System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0))))), true, true, true, true, 10, 100, 10, System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90))))), System.Drawing.Color.Transparent, false, System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255))))));
            devDept.Eyeshot.Grid grid10 = new devDept.Eyeshot.Grid(new devDept.Geometry.Point3D(-100D, -100D, 0D), new devDept.Geometry.Point3D(100D, 100D, 0D), 10D, new devDept.Geometry.Plane(new devDept.Geometry.Point3D(0D, 0D, 0D), new devDept.Geometry.Vector3D(1D, 0D, 0D)), System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192))))), System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))), System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0))))), true, false, false, true, 10, 100, 0, System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))), System.Drawing.Color.Transparent, true, System.Drawing.Color.Transparent);
            devDept.Eyeshot.OriginSymbol originSymbol6 = new devDept.Eyeshot.OriginSymbol(10, devDept.Eyeshot.originSymbolStyleType.Ball, new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129))), System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Red, System.Drawing.Color.Green, System.Drawing.Color.Blue, "Origin", "X", "Y", "Z", true, null, false);
            devDept.Eyeshot.OriginSymbol originSymbol7 = new devDept.Eyeshot.OriginSymbol(10, devDept.Eyeshot.originSymbolStyleType.CoordinateSystem, new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129))), System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Red, System.Drawing.Color.Green, System.Drawing.Color.Blue, "Origin", "X", "Y", "Z", false, null, true);
            devDept.Eyeshot.RotateSettings rotateSettings5 = new devDept.Eyeshot.RotateSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Ctrl), 10D, true, 1D, devDept.Eyeshot.rotationType.Turntable, devDept.Eyeshot.rotationCenterType.CursorLocation, new devDept.Geometry.Point3D(0D, 0D, 0D), false);
            devDept.Eyeshot.ZoomSettings zoomSettings5 = new devDept.Eyeshot.ZoomSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Shift), 25, true, devDept.Eyeshot.zoomStyleType.AtCursorLocation, false, 1D, System.Drawing.Color.Empty, devDept.Eyeshot.Camera.perspectiveFitType.Accurate, false, 10, true);
            devDept.Eyeshot.PanSettings panSettings5 = new devDept.Eyeshot.PanSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.None), 25, true);
            devDept.Eyeshot.NavigationSettings navigationSettings5 = new devDept.Eyeshot.NavigationSettings(devDept.Eyeshot.Camera.navigationType.Examine, new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Left, devDept.Eyeshot.modifierKeys.None), new devDept.Geometry.Point3D(-1000D, -1000D, -1000D), new devDept.Geometry.Point3D(1000D, 1000D, 1000D), 8D, 50D, 50D);
            devDept.Eyeshot.CoordinateSystemIcon coordinateSystemIcon5 = new devDept.Eyeshot.CoordinateSystemIcon(new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129))), System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Red, System.Drawing.Color.Green, System.Drawing.Color.Blue, "Origin", "X", "Y", "Z", true, devDept.Eyeshot.coordinateSystemPositionType.BottomLeft, 37, null, true);
            devDept.Eyeshot.ViewCubeIcon viewCubeIcon5 = new devDept.Eyeshot.ViewCubeIcon(devDept.Eyeshot.coordinateSystemPositionType.TopRight, true, System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(20)))), ((int)(((byte)(60))))), true, "FRONT", "BACK", "LEFT", "RIGHT", "TOP", "BOTTOM", System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), 'S', 'N', 'W', 'E', true, null, System.Drawing.Color.White, System.Drawing.Color.Black, 120, true, true, null, null, null, null, null, null, false, new devDept.Geometry.Quaternion(0D, 0D, 0D, 1D));
            devDept.Eyeshot.Viewport.SavedViewsManager savedViewsManager5 = new devDept.Eyeshot.Viewport.SavedViewsManager(8);
            devDept.Eyeshot.Viewport viewport5 = new devDept.Eyeshot.Viewport(new System.Drawing.Point(0, 0), new System.Drawing.Size(476, 288), backgroundSettings5, camera5, new devDept.Eyeshot.ToolBar[] {
            toolBar9,
            toolBar10}, new devDept.Eyeshot.Legend[0], devDept.Eyeshot.displayType.Rendered, true, false, false, false, new devDept.Eyeshot.Grid[] {
            grid9,
            grid10}, new devDept.Eyeshot.OriginSymbol[] {
            originSymbol6,
            originSymbol7}, false, rotateSettings5, zoomSettings5, panSettings5, navigationSettings5, coordinateSystemIcon5, viewCubeIcon5, savedViewsManager5, devDept.Eyeshot.viewType.Trimetric);
            devDept.Graphics.BackgroundSettings backgroundSettings6 = new devDept.Graphics.BackgroundSettings(devDept.Graphics.backgroundStyleType.LinearGradient, System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(82)))), ((int)(((byte)(103))))), System.Drawing.Color.DodgerBlue, System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(32)))), ((int)(((byte)(41))))), 0.75D, null, devDept.Graphics.colorThemeType.Auto, 0.33D);
            devDept.Eyeshot.Camera camera6 = new devDept.Eyeshot.Camera(new devDept.Geometry.Point3D(-2.0837870570316238E-14D, 5.903590011596604D, 47.596564948558807D), 293.85071937118977D, new devDept.Geometry.Quaternion(0.49999999999999989D, 0.49999999999999994D, 0.49999999999999994D, 0.5D), devDept.Graphics.projectionType.Perspective, 40D, 1.801549330370017D, false, 0.001D);
            devDept.Eyeshot.HomeToolBarButton homeToolBarButton11 = new devDept.Eyeshot.HomeToolBarButton("Home", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.MagnifyingGlassToolBarButton magnifyingGlassToolBarButton11 = new devDept.Eyeshot.MagnifyingGlassToolBarButton("Magnifying Glass", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomWindowToolBarButton zoomWindowToolBarButton11 = new devDept.Eyeshot.ZoomWindowToolBarButton("Zoom Window", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomToolBarButton zoomToolBarButton11 = new devDept.Eyeshot.ZoomToolBarButton("Zoom", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.PanToolBarButton panToolBarButton11 = new devDept.Eyeshot.PanToolBarButton("Pan", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.RotateToolBarButton rotateToolBarButton11 = new devDept.Eyeshot.RotateToolBarButton("Rotate", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomFitToolBarButton zoomFitToolBarButton11 = new devDept.Eyeshot.ZoomFitToolBarButton("Zoom Fit", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.ToolBar toolBar11 = new devDept.Eyeshot.ToolBar(devDept.Eyeshot.ToolBar.positionType.HorizontalTopCenter, true, new devDept.Eyeshot.ToolBarButton[] {
            ((devDept.Eyeshot.ToolBarButton)(homeToolBarButton11)),
            ((devDept.Eyeshot.ToolBarButton)(magnifyingGlassToolBarButton11)),
            ((devDept.Eyeshot.ToolBarButton)(zoomWindowToolBarButton11)),
            ((devDept.Eyeshot.ToolBarButton)(zoomToolBarButton11)),
            ((devDept.Eyeshot.ToolBarButton)(panToolBarButton11)),
            ((devDept.Eyeshot.ToolBarButton)(rotateToolBarButton11)),
            ((devDept.Eyeshot.ToolBarButton)(zoomFitToolBarButton11))});
            devDept.Eyeshot.HomeToolBarButton homeToolBarButton12 = new devDept.Eyeshot.HomeToolBarButton("Home", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.MagnifyingGlassToolBarButton magnifyingGlassToolBarButton12 = new devDept.Eyeshot.MagnifyingGlassToolBarButton("Magnifying Glass", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomWindowToolBarButton zoomWindowToolBarButton12 = new devDept.Eyeshot.ZoomWindowToolBarButton("Zoom Window", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomToolBarButton zoomToolBarButton12 = new devDept.Eyeshot.ZoomToolBarButton("Zoom", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.PanToolBarButton panToolBarButton12 = new devDept.Eyeshot.PanToolBarButton("Pan", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.RotateToolBarButton rotateToolBarButton12 = new devDept.Eyeshot.RotateToolBarButton("Rotate", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomFitToolBarButton zoomFitToolBarButton12 = new devDept.Eyeshot.ZoomFitToolBarButton("Zoom Fit", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.ToolBar toolBar12 = new devDept.Eyeshot.ToolBar(devDept.Eyeshot.ToolBar.positionType.VerticalTopLeft, true, new devDept.Eyeshot.ToolBarButton[] {
            ((devDept.Eyeshot.ToolBarButton)(homeToolBarButton12)),
            ((devDept.Eyeshot.ToolBarButton)(magnifyingGlassToolBarButton12)),
            ((devDept.Eyeshot.ToolBarButton)(zoomWindowToolBarButton12)),
            ((devDept.Eyeshot.ToolBarButton)(zoomToolBarButton12)),
            ((devDept.Eyeshot.ToolBarButton)(panToolBarButton12)),
            ((devDept.Eyeshot.ToolBarButton)(rotateToolBarButton12)),
            ((devDept.Eyeshot.ToolBarButton)(zoomFitToolBarButton12))});
            devDept.Eyeshot.Grid grid11 = new devDept.Eyeshot.Grid(new devDept.Geometry.Point2D(-100D, -100D), new devDept.Geometry.Point2D(100D, 100D), 10D, new devDept.Geometry.Plane(new devDept.Geometry.Point3D(0D, 0D, 0D), new devDept.Geometry.Vector3D(0D, 0D, 1D)), System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0))))), true, true, true, true, 10, 100, 10, System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90))))), System.Drawing.Color.Transparent, false, System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255))))));
            devDept.Eyeshot.Grid grid12 = new devDept.Eyeshot.Grid(new devDept.Geometry.Point3D(-100D, -100D, 0D), new devDept.Geometry.Point3D(100D, 100D, 0D), 1D, new devDept.Geometry.Plane(new devDept.Geometry.Point3D(0D, 0D, 0D), new devDept.Geometry.Vector3D(0D, 0D, 1D)), System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0))))), true, false, true, true, 10, 100, 10, System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90))))), System.Drawing.Color.Transparent, false, System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255))))));
            devDept.Eyeshot.OriginSymbol originSymbol8 = new devDept.Eyeshot.OriginSymbol(10, devDept.Eyeshot.originSymbolStyleType.Ball, new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129))), System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Red, System.Drawing.Color.Green, System.Drawing.Color.Blue, "Origin", "X", "Y", "Z", true, null, false);
            devDept.Eyeshot.RotateSettings rotateSettings6 = new devDept.Eyeshot.RotateSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Ctrl), 10D, true, 1D, devDept.Eyeshot.rotationType.Turntable, devDept.Eyeshot.rotationCenterType.CursorLocation, new devDept.Geometry.Point3D(0D, 0D, 0D), false);
            devDept.Eyeshot.ZoomSettings zoomSettings6 = new devDept.Eyeshot.ZoomSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Shift), 25, true, devDept.Eyeshot.zoomStyleType.AtCursorLocation, false, 1D, System.Drawing.Color.Empty, devDept.Eyeshot.Camera.perspectiveFitType.Accurate, false, 10, true);
            devDept.Eyeshot.PanSettings panSettings6 = new devDept.Eyeshot.PanSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.None), 25, true);
            devDept.Eyeshot.NavigationSettings navigationSettings6 = new devDept.Eyeshot.NavigationSettings(devDept.Eyeshot.Camera.navigationType.Examine, new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Left, devDept.Eyeshot.modifierKeys.None), new devDept.Geometry.Point3D(-1000D, -1000D, -1000D), new devDept.Geometry.Point3D(1000D, 1000D, 1000D), 8D, 50D, 50D);
            devDept.Eyeshot.CoordinateSystemIcon coordinateSystemIcon6 = new devDept.Eyeshot.CoordinateSystemIcon(new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129))), System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80))))), System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80))))), System.Drawing.Color.OrangeRed, "Origin", "X", "Y", "Z", true, devDept.Eyeshot.coordinateSystemPositionType.BottomLeft, 37, null, false);
            devDept.Eyeshot.ViewCubeIcon viewCubeIcon6 = new devDept.Eyeshot.ViewCubeIcon(devDept.Eyeshot.coordinateSystemPositionType.TopRight, true, System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(20)))), ((int)(((byte)(60))))), true, "FRONT", "BACK", "LEFT", "RIGHT", "TOP", "BOTTOM", System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), 'S', 'N', 'W', 'E', true, null, System.Drawing.Color.White, System.Drawing.Color.Black, 120, true, true, null, null, null, null, null, null, false, new devDept.Geometry.Quaternion(0D, 0D, 0D, 1D));
            devDept.Eyeshot.Viewport.SavedViewsManager savedViewsManager6 = new devDept.Eyeshot.Viewport.SavedViewsManager(8);
            devDept.Eyeshot.Viewport viewport6 = new devDept.Eyeshot.Viewport(new System.Drawing.Point(480, 0), new System.Drawing.Size(476, 288), backgroundSettings6, camera6, new devDept.Eyeshot.ToolBar[] {
            toolBar11,
            toolBar12}, new devDept.Eyeshot.Legend[0], devDept.Eyeshot.displayType.Rendered, true, false, false, false, new devDept.Eyeshot.Grid[] {
            grid11,
            grid12}, new devDept.Eyeshot.OriginSymbol[] {
            originSymbol8}, false, rotateSettings6, zoomSettings6, panSettings6, navigationSettings6, coordinateSystemIcon6, viewCubeIcon6, savedViewsManager6, devDept.Eyeshot.viewType.Top);
            devDept.Graphics.BackgroundSettings backgroundSettings7 = new devDept.Graphics.BackgroundSettings(devDept.Graphics.backgroundStyleType.LinearGradient, System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(82)))), ((int)(((byte)(103))))), System.Drawing.Color.DodgerBlue, System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(32)))), ((int)(((byte)(41))))), 0.75D, null, devDept.Graphics.colorThemeType.Auto, 0.33D);
            devDept.Eyeshot.Camera camera7 = new devDept.Eyeshot.Camera(new devDept.Geometry.Point3D(0D, 0D, 45D), 380D, new devDept.Geometry.Quaternion(0D, 0D, 0.70710678118654746D, 0.70710678118654757D), devDept.Graphics.projectionType.Perspective, 40D, 1.801549330370017D, false, 0.001D);
            devDept.Eyeshot.HomeToolBarButton homeToolBarButton13 = new devDept.Eyeshot.HomeToolBarButton("Home", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.MagnifyingGlassToolBarButton magnifyingGlassToolBarButton13 = new devDept.Eyeshot.MagnifyingGlassToolBarButton("Magnifying Glass", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomWindowToolBarButton zoomWindowToolBarButton13 = new devDept.Eyeshot.ZoomWindowToolBarButton("Zoom Window", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomToolBarButton zoomToolBarButton13 = new devDept.Eyeshot.ZoomToolBarButton("Zoom", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.PanToolBarButton panToolBarButton13 = new devDept.Eyeshot.PanToolBarButton("Pan", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.RotateToolBarButton rotateToolBarButton13 = new devDept.Eyeshot.RotateToolBarButton("Rotate", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomFitToolBarButton zoomFitToolBarButton13 = new devDept.Eyeshot.ZoomFitToolBarButton("Zoom Fit", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.ToolBar toolBar13 = new devDept.Eyeshot.ToolBar(devDept.Eyeshot.ToolBar.positionType.HorizontalTopCenter, true, new devDept.Eyeshot.ToolBarButton[] {
            ((devDept.Eyeshot.ToolBarButton)(homeToolBarButton13)),
            ((devDept.Eyeshot.ToolBarButton)(magnifyingGlassToolBarButton13)),
            ((devDept.Eyeshot.ToolBarButton)(zoomWindowToolBarButton13)),
            ((devDept.Eyeshot.ToolBarButton)(zoomToolBarButton13)),
            ((devDept.Eyeshot.ToolBarButton)(panToolBarButton13)),
            ((devDept.Eyeshot.ToolBarButton)(rotateToolBarButton13)),
            ((devDept.Eyeshot.ToolBarButton)(zoomFitToolBarButton13))});
            devDept.Eyeshot.HomeToolBarButton homeToolBarButton14 = new devDept.Eyeshot.HomeToolBarButton("Home", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.MagnifyingGlassToolBarButton magnifyingGlassToolBarButton14 = new devDept.Eyeshot.MagnifyingGlassToolBarButton("Magnifying Glass", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomWindowToolBarButton zoomWindowToolBarButton14 = new devDept.Eyeshot.ZoomWindowToolBarButton("Zoom Window", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomToolBarButton zoomToolBarButton14 = new devDept.Eyeshot.ZoomToolBarButton("Zoom", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.PanToolBarButton panToolBarButton14 = new devDept.Eyeshot.PanToolBarButton("Pan", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.RotateToolBarButton rotateToolBarButton14 = new devDept.Eyeshot.RotateToolBarButton("Rotate", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomFitToolBarButton zoomFitToolBarButton14 = new devDept.Eyeshot.ZoomFitToolBarButton("Zoom Fit", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.ToolBar toolBar14 = new devDept.Eyeshot.ToolBar(devDept.Eyeshot.ToolBar.positionType.VerticalTopLeft, true, new devDept.Eyeshot.ToolBarButton[] {
            ((devDept.Eyeshot.ToolBarButton)(homeToolBarButton14)),
            ((devDept.Eyeshot.ToolBarButton)(magnifyingGlassToolBarButton14)),
            ((devDept.Eyeshot.ToolBarButton)(zoomWindowToolBarButton14)),
            ((devDept.Eyeshot.ToolBarButton)(zoomToolBarButton14)),
            ((devDept.Eyeshot.ToolBarButton)(panToolBarButton14)),
            ((devDept.Eyeshot.ToolBarButton)(rotateToolBarButton14)),
            ((devDept.Eyeshot.ToolBarButton)(zoomFitToolBarButton14))});
            devDept.Eyeshot.Grid grid13 = new devDept.Eyeshot.Grid(new devDept.Geometry.Point2D(-100D, -100D), new devDept.Geometry.Point2D(100D, 100D), 10D, new devDept.Geometry.Plane(new devDept.Geometry.Point3D(0D, 0D, 0D), new devDept.Geometry.Vector3D(0D, 0D, 1D)), System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0))))), true, true, true, true, 10, 100, 10, System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90))))), System.Drawing.Color.Transparent, false, System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255))))));
            devDept.Eyeshot.Grid grid14 = new devDept.Eyeshot.Grid(new devDept.Geometry.Point3D(-100D, -100D, 0D), new devDept.Geometry.Point3D(100D, 100D, 0D), 1D, new devDept.Geometry.Plane(new devDept.Geometry.Point3D(0D, 0D, 0D), new devDept.Geometry.Vector3D(0D, 0D, 1D)), System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0))))), true, false, true, true, 10, 100, 10, System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90))))), System.Drawing.Color.Transparent, false, System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255))))));
            devDept.Eyeshot.OriginSymbol originSymbol9 = new devDept.Eyeshot.OriginSymbol(10, devDept.Eyeshot.originSymbolStyleType.Ball, new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129))), System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Red, System.Drawing.Color.Green, System.Drawing.Color.Blue, "Origin", "X", "Y", "Z", true, null, false);
            devDept.Eyeshot.RotateSettings rotateSettings7 = new devDept.Eyeshot.RotateSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Ctrl), 10D, true, 1D, devDept.Eyeshot.rotationType.Turntable, devDept.Eyeshot.rotationCenterType.CursorLocation, new devDept.Geometry.Point3D(0D, 0D, 0D), false);
            devDept.Eyeshot.ZoomSettings zoomSettings7 = new devDept.Eyeshot.ZoomSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Shift), 25, true, devDept.Eyeshot.zoomStyleType.AtCursorLocation, false, 1D, System.Drawing.Color.Empty, devDept.Eyeshot.Camera.perspectiveFitType.Accurate, false, 10, true);
            devDept.Eyeshot.PanSettings panSettings7 = new devDept.Eyeshot.PanSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.None), 25, true);
            devDept.Eyeshot.NavigationSettings navigationSettings7 = new devDept.Eyeshot.NavigationSettings(devDept.Eyeshot.Camera.navigationType.Examine, new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Left, devDept.Eyeshot.modifierKeys.None), new devDept.Geometry.Point3D(-1000D, -1000D, -1000D), new devDept.Geometry.Point3D(1000D, 1000D, 1000D), 8D, 50D, 50D);
            devDept.Eyeshot.CoordinateSystemIcon coordinateSystemIcon7 = new devDept.Eyeshot.CoordinateSystemIcon(new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129))), System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80))))), System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80))))), System.Drawing.Color.OrangeRed, "Origin", "X", "Y", "Z", true, devDept.Eyeshot.coordinateSystemPositionType.BottomLeft, 37, null, false);
            devDept.Eyeshot.ViewCubeIcon viewCubeIcon7 = new devDept.Eyeshot.ViewCubeIcon(devDept.Eyeshot.coordinateSystemPositionType.TopRight, true, System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(20)))), ((int)(((byte)(60))))), true, "FRONT", "BACK", "LEFT", "RIGHT", "TOP", "BOTTOM", System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), 'S', 'N', 'W', 'E', true, null, System.Drawing.Color.White, System.Drawing.Color.Black, 120, true, true, null, null, null, null, null, null, false, new devDept.Geometry.Quaternion(0D, 0D, 0D, 1D));
            devDept.Eyeshot.Viewport.SavedViewsManager savedViewsManager7 = new devDept.Eyeshot.Viewport.SavedViewsManager(8);
            devDept.Eyeshot.Viewport viewport7 = new devDept.Eyeshot.Viewport(new System.Drawing.Point(0, 292), new System.Drawing.Size(476, 289), backgroundSettings7, camera7, new devDept.Eyeshot.ToolBar[] {
            toolBar13,
            toolBar14}, new devDept.Eyeshot.Legend[0], devDept.Eyeshot.displayType.Rendered, true, false, false, false, new devDept.Eyeshot.Grid[] {
            grid13,
            grid14}, new devDept.Eyeshot.OriginSymbol[] {
            originSymbol9}, false, rotateSettings7, zoomSettings7, panSettings7, navigationSettings7, coordinateSystemIcon7, viewCubeIcon7, savedViewsManager7, devDept.Eyeshot.viewType.Front);
            devDept.Graphics.BackgroundSettings backgroundSettings8 = new devDept.Graphics.BackgroundSettings(devDept.Graphics.backgroundStyleType.LinearGradient, System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(82)))), ((int)(((byte)(103))))), System.Drawing.Color.DodgerBlue, System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(32)))), ((int)(((byte)(41))))), 0.75D, null, devDept.Graphics.colorThemeType.Auto, 0.33D);
            devDept.Eyeshot.Camera camera8 = new devDept.Eyeshot.Camera(new devDept.Geometry.Point3D(0D, 0D, 45D), 380D, new devDept.Geometry.Quaternion(0D, 0D, 0D, 1D), devDept.Graphics.projectionType.Perspective, 40D, 1.801549330370017D, false, 0.001D);
            devDept.Eyeshot.HomeToolBarButton homeToolBarButton15 = new devDept.Eyeshot.HomeToolBarButton("Home", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.MagnifyingGlassToolBarButton magnifyingGlassToolBarButton15 = new devDept.Eyeshot.MagnifyingGlassToolBarButton("Magnifying Glass", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomWindowToolBarButton zoomWindowToolBarButton15 = new devDept.Eyeshot.ZoomWindowToolBarButton("Zoom Window", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomToolBarButton zoomToolBarButton15 = new devDept.Eyeshot.ZoomToolBarButton("Zoom", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.PanToolBarButton panToolBarButton15 = new devDept.Eyeshot.PanToolBarButton("Pan", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.RotateToolBarButton rotateToolBarButton15 = new devDept.Eyeshot.RotateToolBarButton("Rotate", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomFitToolBarButton zoomFitToolBarButton15 = new devDept.Eyeshot.ZoomFitToolBarButton("Zoom Fit", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.ToolBar toolBar15 = new devDept.Eyeshot.ToolBar(devDept.Eyeshot.ToolBar.positionType.HorizontalTopCenter, true, new devDept.Eyeshot.ToolBarButton[] {
            ((devDept.Eyeshot.ToolBarButton)(homeToolBarButton15)),
            ((devDept.Eyeshot.ToolBarButton)(magnifyingGlassToolBarButton15)),
            ((devDept.Eyeshot.ToolBarButton)(zoomWindowToolBarButton15)),
            ((devDept.Eyeshot.ToolBarButton)(zoomToolBarButton15)),
            ((devDept.Eyeshot.ToolBarButton)(panToolBarButton15)),
            ((devDept.Eyeshot.ToolBarButton)(rotateToolBarButton15)),
            ((devDept.Eyeshot.ToolBarButton)(zoomFitToolBarButton15))});
            devDept.Eyeshot.HomeToolBarButton homeToolBarButton16 = new devDept.Eyeshot.HomeToolBarButton("Home", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.MagnifyingGlassToolBarButton magnifyingGlassToolBarButton16 = new devDept.Eyeshot.MagnifyingGlassToolBarButton("Magnifying Glass", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomWindowToolBarButton zoomWindowToolBarButton16 = new devDept.Eyeshot.ZoomWindowToolBarButton("Zoom Window", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomToolBarButton zoomToolBarButton16 = new devDept.Eyeshot.ZoomToolBarButton("Zoom", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.PanToolBarButton panToolBarButton16 = new devDept.Eyeshot.PanToolBarButton("Pan", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.RotateToolBarButton rotateToolBarButton16 = new devDept.Eyeshot.RotateToolBarButton("Rotate", devDept.Eyeshot.ToolBarButton.styleType.ToggleButton, true, true);
            devDept.Eyeshot.ZoomFitToolBarButton zoomFitToolBarButton16 = new devDept.Eyeshot.ZoomFitToolBarButton("Zoom Fit", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Eyeshot.ToolBar toolBar16 = new devDept.Eyeshot.ToolBar(devDept.Eyeshot.ToolBar.positionType.VerticalTopLeft, true, new devDept.Eyeshot.ToolBarButton[] {
            ((devDept.Eyeshot.ToolBarButton)(homeToolBarButton16)),
            ((devDept.Eyeshot.ToolBarButton)(magnifyingGlassToolBarButton16)),
            ((devDept.Eyeshot.ToolBarButton)(zoomWindowToolBarButton16)),
            ((devDept.Eyeshot.ToolBarButton)(zoomToolBarButton16)),
            ((devDept.Eyeshot.ToolBarButton)(panToolBarButton16)),
            ((devDept.Eyeshot.ToolBarButton)(rotateToolBarButton16)),
            ((devDept.Eyeshot.ToolBarButton)(zoomFitToolBarButton16))});
            devDept.Eyeshot.Grid grid15 = new devDept.Eyeshot.Grid(new devDept.Geometry.Point2D(-100D, -100D), new devDept.Geometry.Point2D(100D, 100D), 10D, new devDept.Geometry.Plane(new devDept.Geometry.Point3D(0D, 0D, 0D), new devDept.Geometry.Vector3D(0D, 0D, 1D)), System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0))))), true, true, true, true, 10, 100, 10, System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90))))), System.Drawing.Color.Transparent, false, System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255))))));
            devDept.Eyeshot.Grid grid16 = new devDept.Eyeshot.Grid(new devDept.Geometry.Point3D(-100D, -100D, 0D), new devDept.Geometry.Point3D(100D, 100D, 0D), 1D, new devDept.Geometry.Plane(new devDept.Geometry.Point3D(0D, 0D, 0D), new devDept.Geometry.Vector3D(0D, 0D, 1D)), System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0))))), System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(0))))), true, false, true, true, 10, 100, 10, System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90))))), System.Drawing.Color.Transparent, false, System.Drawing.Color.FromArgb(((int)(((byte)(12)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255))))));
            devDept.Eyeshot.OriginSymbol originSymbol10 = new devDept.Eyeshot.OriginSymbol(10, devDept.Eyeshot.originSymbolStyleType.Ball, new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129))), System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Red, System.Drawing.Color.Green, System.Drawing.Color.Blue, "Origin", "X", "Y", "Z", true, null, false);
            devDept.Eyeshot.RotateSettings rotateSettings8 = new devDept.Eyeshot.RotateSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Ctrl), 10D, true, 1D, devDept.Eyeshot.rotationType.Turntable, devDept.Eyeshot.rotationCenterType.CursorLocation, new devDept.Geometry.Point3D(0D, 0D, 0D), false);
            devDept.Eyeshot.ZoomSettings zoomSettings8 = new devDept.Eyeshot.ZoomSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Shift), 25, true, devDept.Eyeshot.zoomStyleType.AtCursorLocation, false, 1D, System.Drawing.Color.Empty, devDept.Eyeshot.Camera.perspectiveFitType.Accurate, false, 10, true);
            devDept.Eyeshot.PanSettings panSettings8 = new devDept.Eyeshot.PanSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.None), 25, true);
            devDept.Eyeshot.NavigationSettings navigationSettings8 = new devDept.Eyeshot.NavigationSettings(devDept.Eyeshot.Camera.navigationType.Examine, new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Left, devDept.Eyeshot.modifierKeys.None), new devDept.Geometry.Point3D(-1000D, -1000D, -1000D), new devDept.Geometry.Point3D(1000D, 1000D, 1000D), 8D, 50D, 50D);
            devDept.Eyeshot.CoordinateSystemIcon coordinateSystemIcon8 = new devDept.Eyeshot.CoordinateSystemIcon(new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129))), System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80))))), System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80))))), System.Drawing.Color.OrangeRed, "Origin", "X", "Y", "Z", true, devDept.Eyeshot.coordinateSystemPositionType.BottomLeft, 37, null, false);
            devDept.Eyeshot.ViewCubeIcon viewCubeIcon8 = new devDept.Eyeshot.ViewCubeIcon(devDept.Eyeshot.coordinateSystemPositionType.TopRight, true, System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(20)))), ((int)(((byte)(60))))), true, "FRONT", "BACK", "LEFT", "RIGHT", "TOP", "BOTTOM", System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), 'S', 'N', 'W', 'E', true, null, System.Drawing.Color.White, System.Drawing.Color.Black, 120, true, true, null, null, null, null, null, null, false, new devDept.Geometry.Quaternion(0D, 0D, 0D, 1D));
            devDept.Eyeshot.Viewport.SavedViewsManager savedViewsManager8 = new devDept.Eyeshot.Viewport.SavedViewsManager(8);
            devDept.Eyeshot.Viewport viewport8 = new devDept.Eyeshot.Viewport(new System.Drawing.Point(480, 292), new System.Drawing.Size(476, 289), backgroundSettings8, camera8, new devDept.Eyeshot.ToolBar[] {
            toolBar15,
            toolBar16}, new devDept.Eyeshot.Legend[0], devDept.Eyeshot.displayType.Rendered, true, false, false, false, new devDept.Eyeshot.Grid[] {
            grid15,
            grid16}, new devDept.Eyeshot.OriginSymbol[] {
            originSymbol10}, false, rotateSettings8, zoomSettings8, panSettings8, navigationSettings8, coordinateSystemIcon8, viewCubeIcon8, savedViewsManager8, devDept.Eyeshot.viewType.Right);
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.tileNavSubItemInsertImage = new DevExpress.XtraBars.Navigation.TileNavSubItem();
            this.hModel = new hanee.ThreeD.HModel();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.endPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.intersectionPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.middlePointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.centerPointToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectallToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unselectAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.invertSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.transparencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTransparency0 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTransparency50 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemTransparency100 = new System.Windows.Forms.ToolStripMenuItem();
            this.dockManager1 = new DevExpress.XtraBars.Docking.DockManager(this.components);
            this.hideContainerLeft = new DevExpress.XtraBars.Docking.AutoHideContainer();
            this.panelContainer1 = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanelProperties = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel1_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.propertyGridControl1 = new DevExpress.XtraVerticalGrid.PropertyGridControl();
            this.repositoryItemComboBoxTextStyle = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBoxLayerName = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBoxLineType = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.repositoryItemComboBoxBlock = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.categoryGeneral = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rowEntityType = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowVisible = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowColor = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowColorMethod = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowBoxMin = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowBoxMax = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowGroupIndex = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLayerName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.categoryCircle = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rowRadius = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.categoryBlock = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rowBlockName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.categoryLineType = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rowLineTypeName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLineTypeScale = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLineWeight = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowLineWeightMethod = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.categoryText = new DevExpress.XtraVerticalGrid.Rows.CategoryRow();
            this.rowTextString = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowStyleName = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowHeight = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowBillboard = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowWidthFactor = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowInsertionPoint = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowBackward = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowUpsideDown = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.rowAlignment = new DevExpress.XtraVerticalGrid.Rows.EditorRow();
            this.dockPanelObjectTree = new DevExpress.XtraBars.Docking.DockPanel();
            this.dockPanel3_Container = new DevExpress.XtraBars.Docking.ControlContainer();
            this.treeListObject = new DevExpress.XtraTreeList.TreeList();
            this.dockPanelDynamicInput = new DevExpress.XtraBars.Docking.DockPanel();
            this.controlContainer1 = new DevExpress.XtraBars.Docking.ControlContainer();
            this.controlCommandBar1 = new hanee.ThreeD.ControlCommandBar();
            this.controlAds1 = new Br3D.ControlAds();
            this.toolTipController1 = new DevExpress.Utils.ToolTipController(this.components);
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.barButtonItemDrawCircle = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDrawArc = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenuDrawArc = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barButtonItemDrawArc_FirstSecondThird = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDrawArc_CenterStartEnd = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDrawPolyline = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDrawSpline = new DevExpress.XtraBars.BarButtonItem();
            this.barStaticItem1 = new DevExpress.XtraBars.BarStaticItem();
            this.barStaticItemCoordinates = new DevExpress.XtraBars.BarStaticItem();
            this.barButtonItemDrawLine = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDrawText = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemInsert = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDrawCylinder = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemOpen = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemSaveAs = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemSaveImage = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemExit = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDimHorizontal = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDimVertical = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDimAlign = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDimDiameter = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDimRadius = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDimLeader = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemErase = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemMove = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCopy = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemScale = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemRotate = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemOffset = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemMirror = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemExplode = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemTrim = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemFillet = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemChamfer = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemLanguage = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenuLanguage = new DevExpress.XtraBars.PopupMenu(this.components);
            this.barButtonItemLanguageKorean = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemLanguageEnglish = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCheckForUpdate = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemHomepage = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemAbout = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemLayer = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemTextStyle = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemLineType = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCoordinates = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDistance = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemMemo = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemClearAnnotations = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemSingleView = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1x1View = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem1x2View = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem2x2View = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemOrthoMode = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemOsnapend = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemOsnapIntersection = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemOsnapMiddle = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemOsnapCenter = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemOsnapPoint = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemMultilineText = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemInsertImage = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemShowGrid = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemShowToolbar = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemShowSymbol = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemOptions = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemWorkspace = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemEndPoint = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemIntPoint = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemMidPoint = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemCenterPoint = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemNodePoint = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDrawBox = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDrawCone = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPageHome = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupFile = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupWorkspace = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupSystem = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageDraw = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupDraw = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageDraw3D = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageDimension = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupDim = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageEdit = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupEdit = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageAnnotation = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupDrawAnnotation = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupClearAnnotations = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageTools = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupTools = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupOrthoMode = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupOsnap = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageOptions = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroupViewport = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroupOptions = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.repositoryItemRibbonSearchEdit1 = new DevExpress.XtraBars.Ribbon.Internal.RepositoryItemRibbonSearchEdit();
            this.repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.repositoryItemCheckEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit();
            this.ribbonStatusBar1 = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            this.barButtonItem1 = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItemDrawSphere = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.hModel)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).BeginInit();
            this.hideContainerLeft.SuspendLayout();
            this.panelContainer1.SuspendLayout();
            this.dockPanelProperties.SuspendLayout();
            this.dockPanel1_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxTextStyle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxLayerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxLineType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxBlock)).BeginInit();
            this.dockPanelObjectTree.SuspendLayout();
            this.dockPanel3_Container.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.treeListObject)).BeginInit();
            this.dockPanelDynamicInput.SuspendLayout();
            this.controlContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuDrawArc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuLanguage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRibbonSearchEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // tileNavSubItemInsertImage
            // 
            this.tileNavSubItemInsertImage.Caption = "Image";
            this.tileNavSubItemInsertImage.Name = "tileNavSubItemInsertImage";
            // 
            // 
            // 
            this.tileNavSubItemInsertImage.Tile.DropDownOptions.BeakColor = System.Drawing.Color.Empty;
            tileItemElement2.Text = "Image";
            this.tileNavSubItemInsertImage.Tile.Elements.Add(tileItemElement2);
            this.tileNavSubItemInsertImage.Tile.Name = "tileBarItem1";
            // 
            // hModel
            // 
            this.hModel.AskForAntiAliasing = true;
            this.hModel.ContextMenuStrip = this.contextMenuStrip1;
            this.hModel.Cursor = System.Windows.Forms.Cursors.Cross;
            this.hModel.DefaultColor = System.Drawing.Color.White;
            this.hModel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.hModel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.hModel.Location = new System.Drawing.Point(23, 64);
            this.hModel.Name = "hModel";
            this.hModel.ProgressBar = progressBar2;
            this.hModel.propertyGridHelper = null;
            this.hModel.Renderer = devDept.Eyeshot.rendererType.Direct3D;
            this.hModel.Size = new System.Drawing.Size(956, 581);
            this.hModel.TabIndex = 3;
            this.hModel.Text = "hModel1";
            this.hModel.Transparency = hanee.ThreeD.HModel.TranparencyMode.untransparency;
            this.hModel.ViewportBorder = new devDept.Eyeshot.BorderSettings(System.Drawing.Color.FromArgb(((int)(((byte)(130)))), ((int)(((byte)(130)))), ((int)(((byte)(130))))), 0, false);
            this.hModel.Viewports.Add(viewport5);
            this.hModel.Viewports.Add(viewport6);
            this.hModel.Viewports.Add(viewport7);
            this.hModel.Viewports.Add(viewport8);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.endPointToolStripMenuItem,
            this.intersectionPointToolStripMenuItem,
            this.middlePointToolStripMenuItem,
            this.centerPointToolStripMenuItem,
            this.selectallToolStripMenuItem,
            this.unselectAllToolStripMenuItem,
            this.invertSelectionToolStripMenuItem,
            this.transparencyToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(169, 180);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            this.contextMenuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.contextMenuStrip1_ItemClicked);
            // 
            // endPointToolStripMenuItem
            // 
            this.endPointToolStripMenuItem.Image = global::Br3D.Properties.Resources.snap_endpoint_small;
            this.endPointToolStripMenuItem.Name = "endPointToolStripMenuItem";
            this.endPointToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.endPointToolStripMenuItem.Text = "&End point";
            // 
            // intersectionPointToolStripMenuItem
            // 
            this.intersectionPointToolStripMenuItem.Image = global::Br3D.Properties.Resources.snap_intersection_small;
            this.intersectionPointToolStripMenuItem.Name = "intersectionPointToolStripMenuItem";
            this.intersectionPointToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.intersectionPointToolStripMenuItem.Text = "&Intersection point";
            // 
            // middlePointToolStripMenuItem
            // 
            this.middlePointToolStripMenuItem.Image = global::Br3D.Properties.Resources.snap_middle_small;
            this.middlePointToolStripMenuItem.Name = "middlePointToolStripMenuItem";
            this.middlePointToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.middlePointToolStripMenuItem.Text = "&Middle point";
            // 
            // centerPointToolStripMenuItem
            // 
            this.centerPointToolStripMenuItem.Image = global::Br3D.Properties.Resources.snap_center_small;
            this.centerPointToolStripMenuItem.Name = "centerPointToolStripMenuItem";
            this.centerPointToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.centerPointToolStripMenuItem.Text = "&Center point";
            // 
            // selectallToolStripMenuItem
            // 
            this.selectallToolStripMenuItem.Name = "selectallToolStripMenuItem";
            this.selectallToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.selectallToolStripMenuItem.Text = "Select &all";
            this.selectallToolStripMenuItem.Click += new System.EventHandler(this.selectallToolStripMenuItem_Click);
            // 
            // unselectAllToolStripMenuItem
            // 
            this.unselectAllToolStripMenuItem.Name = "unselectAllToolStripMenuItem";
            this.unselectAllToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.unselectAllToolStripMenuItem.Text = "&Unselect all";
            this.unselectAllToolStripMenuItem.Click += new System.EventHandler(this.unselectAllToolStripMenuItem_Click);
            // 
            // invertSelectionToolStripMenuItem
            // 
            this.invertSelectionToolStripMenuItem.Name = "invertSelectionToolStripMenuItem";
            this.invertSelectionToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.invertSelectionToolStripMenuItem.Text = "&Invert selection";
            this.invertSelectionToolStripMenuItem.Click += new System.EventHandler(this.invertSelectionToolStripMenuItem_Click);
            // 
            // transparencyToolStripMenuItem
            // 
            this.transparencyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemTransparency0,
            this.toolStripMenuItemTransparency50,
            this.toolStripMenuItemTransparency100});
            this.transparencyToolStripMenuItem.Name = "transparencyToolStripMenuItem";
            this.transparencyToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.transparencyToolStripMenuItem.Text = "Transparency";
            // 
            // toolStripMenuItemTransparency0
            // 
            this.toolStripMenuItemTransparency0.Name = "toolStripMenuItemTransparency0";
            this.toolStripMenuItemTransparency0.Size = new System.Drawing.Size(105, 22);
            this.toolStripMenuItemTransparency0.Text = "0%";
            this.toolStripMenuItemTransparency0.Click += new System.EventHandler(this.toolStripMenuItemTransparency0_Click);
            // 
            // toolStripMenuItemTransparency50
            // 
            this.toolStripMenuItemTransparency50.Name = "toolStripMenuItemTransparency50";
            this.toolStripMenuItemTransparency50.Size = new System.Drawing.Size(105, 22);
            this.toolStripMenuItemTransparency50.Text = "50%";
            this.toolStripMenuItemTransparency50.Click += new System.EventHandler(this.toolStripMenuItemTransparency50_Click);
            // 
            // toolStripMenuItemTransparency100
            // 
            this.toolStripMenuItemTransparency100.Name = "toolStripMenuItemTransparency100";
            this.toolStripMenuItemTransparency100.Size = new System.Drawing.Size(105, 22);
            this.toolStripMenuItemTransparency100.Text = "100%";
            this.toolStripMenuItemTransparency100.Click += new System.EventHandler(this.toolStripMenuItemTransparency100_Click);
            // 
            // dockManager1
            // 
            this.dockManager1.AutoHideContainers.AddRange(new DevExpress.XtraBars.Docking.AutoHideContainer[] {
            this.hideContainerLeft});
            this.dockManager1.Form = this;
            this.dockManager1.RootPanels.AddRange(new DevExpress.XtraBars.Docking.DockPanel[] {
            this.dockPanelDynamicInput});
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
            // hideContainerLeft
            // 
            this.hideContainerLeft.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.hideContainerLeft.Controls.Add(this.panelContainer1);
            this.hideContainerLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.hideContainerLeft.Location = new System.Drawing.Point(0, 64);
            this.hideContainerLeft.Name = "hideContainerLeft";
            this.hideContainerLeft.Size = new System.Drawing.Size(23, 581);
            // 
            // panelContainer1
            // 
            this.panelContainer1.ActiveChild = this.dockPanelProperties;
            this.panelContainer1.Controls.Add(this.dockPanelProperties);
            this.panelContainer1.Controls.Add(this.dockPanelObjectTree);
            this.panelContainer1.Dock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.panelContainer1.ID = new System.Guid("2f55103e-9204-4e48-a94f-fab57b48506c");
            this.panelContainer1.Location = new System.Drawing.Point(0, 0);
            this.panelContainer1.Name = "panelContainer1";
            this.panelContainer1.OriginalSize = new System.Drawing.Size(259, 200);
            this.panelContainer1.SavedDock = DevExpress.XtraBars.Docking.DockingStyle.Left;
            this.panelContainer1.SavedIndex = 1;
            this.panelContainer1.Size = new System.Drawing.Size(259, 581);
            this.panelContainer1.Tabbed = true;
            this.panelContainer1.Text = "panelContainer1";
            this.panelContainer1.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
            // 
            // dockPanelProperties
            // 
            this.dockPanelProperties.Controls.Add(this.dockPanel1_Container);
            this.dockPanelProperties.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanelProperties.ID = new System.Guid("c18255af-44ec-4932-9c11-d99e4ddfcd3e");
            this.dockPanelProperties.Location = new System.Drawing.Point(3, 26);
            this.dockPanelProperties.Name = "dockPanelProperties";
            this.dockPanelProperties.OriginalSize = new System.Drawing.Size(252, 524);
            this.dockPanelProperties.Size = new System.Drawing.Size(252, 552);
            this.dockPanelProperties.Text = "Properties";
            // 
            // dockPanel1_Container
            // 
            this.dockPanel1_Container.Controls.Add(this.propertyGridControl1);
            this.dockPanel1_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1_Container.Name = "dockPanel1_Container";
            this.dockPanel1_Container.Size = new System.Drawing.Size(252, 552);
            this.dockPanel1_Container.TabIndex = 0;
            // 
            // propertyGridControl1
            // 
            this.propertyGridControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.propertyGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGridControl1.Location = new System.Drawing.Point(0, 0);
            this.propertyGridControl1.Name = "propertyGridControl1";
            this.propertyGridControl1.OptionsBehavior.AllowSort = false;
            this.propertyGridControl1.OptionsBehavior.PropertySort = DevExpress.XtraVerticalGrid.PropertySort.NoSort;
            this.propertyGridControl1.OptionsView.AllowReadOnlyRowAppearance = DevExpress.Utils.DefaultBoolean.True;
            this.propertyGridControl1.OptionsView.MinRowAutoHeight = 11;
            this.propertyGridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemComboBoxTextStyle,
            this.repositoryItemComboBoxLayerName,
            this.repositoryItemComboBoxLineType,
            this.repositoryItemComboBoxBlock});
            this.propertyGridControl1.Rows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.categoryGeneral,
            this.categoryCircle,
            this.categoryBlock,
            this.categoryLineType,
            this.categoryText});
            this.propertyGridControl1.Size = new System.Drawing.Size(252, 552);
            this.propertyGridControl1.TabIndex = 0;
            // 
            // repositoryItemComboBoxTextStyle
            // 
            this.repositoryItemComboBoxTextStyle.AutoHeight = false;
            this.repositoryItemComboBoxTextStyle.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxTextStyle.Name = "repositoryItemComboBoxTextStyle";
            // 
            // repositoryItemComboBoxLayerName
            // 
            this.repositoryItemComboBoxLayerName.AutoHeight = false;
            this.repositoryItemComboBoxLayerName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxLayerName.Name = "repositoryItemComboBoxLayerName";
            // 
            // repositoryItemComboBoxLineType
            // 
            this.repositoryItemComboBoxLineType.AutoHeight = false;
            this.repositoryItemComboBoxLineType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxLineType.Name = "repositoryItemComboBoxLineType";
            // 
            // repositoryItemComboBoxBlock
            // 
            this.repositoryItemComboBoxBlock.AutoHeight = false;
            this.repositoryItemComboBoxBlock.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemComboBoxBlock.Name = "repositoryItemComboBoxBlock";
            // 
            // categoryGeneral
            // 
            this.categoryGeneral.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowEntityType,
            this.rowVisible,
            this.rowColor,
            this.rowColorMethod,
            this.rowBoxMin,
            this.rowBoxMax,
            this.rowGroupIndex,
            this.rowLayerName});
            this.categoryGeneral.Name = "categoryGeneral";
            this.categoryGeneral.Properties.Caption = "General";
            this.categoryGeneral.Properties.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("categoryGeneral.Properties.ImageOptions.SvgImage")));
            this.categoryGeneral.Properties.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            // 
            // rowEntityType
            // 
            this.rowEntityType.Name = "rowEntityType";
            this.rowEntityType.Properties.Caption = "Entity Type";
            this.rowEntityType.Properties.FieldName = "EntityType";
            // 
            // rowVisible
            // 
            this.rowVisible.Name = "rowVisible";
            this.rowVisible.Properties.Caption = "Visible";
            this.rowVisible.Properties.FieldName = "Visible";
            // 
            // rowColor
            // 
            this.rowColor.Name = "rowColor";
            this.rowColor.Properties.Caption = "Color";
            this.rowColor.Properties.FieldName = "Color";
            // 
            // rowColorMethod
            // 
            this.rowColorMethod.Name = "rowColorMethod";
            this.rowColorMethod.Properties.Caption = "Color Type";
            // 
            // rowBoxMin
            // 
            this.rowBoxMin.Name = "rowBoxMin";
            this.rowBoxMin.Properties.Caption = "Box Min";
            this.rowBoxMin.Properties.DisplayFormat.FormatString = "n2";
            this.rowBoxMin.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rowBoxMin.Properties.FieldName = "BoxMin";
            // 
            // rowBoxMax
            // 
            this.rowBoxMax.Name = "rowBoxMax";
            this.rowBoxMax.Properties.Caption = "Box Max";
            this.rowBoxMax.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rowBoxMax.Properties.FieldName = "BoxMax";
            // 
            // rowGroupIndex
            // 
            this.rowGroupIndex.Name = "rowGroupIndex";
            this.rowGroupIndex.Properties.Caption = "Group";
            this.rowGroupIndex.Properties.FieldName = "GroupIndex";
            // 
            // rowLayerName
            // 
            this.rowLayerName.Name = "rowLayerName";
            this.rowLayerName.Properties.Caption = "Layer";
            this.rowLayerName.Properties.FieldName = "LayerName";
            this.rowLayerName.Properties.RowEdit = this.repositoryItemComboBoxLayerName;
            // 
            // categoryCircle
            // 
            this.categoryCircle.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowRadius});
            this.categoryCircle.Height = 20;
            this.categoryCircle.Name = "categoryCircle";
            this.categoryCircle.Properties.Caption = "Circle";
            // 
            // rowRadius
            // 
            this.rowRadius.Name = "rowRadius";
            this.rowRadius.Properties.Caption = "Radius";
            this.rowRadius.Properties.FieldName = "Radius";
            // 
            // categoryBlock
            // 
            this.categoryBlock.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowBlockName});
            this.categoryBlock.Name = "categoryBlock";
            this.categoryBlock.Properties.Caption = "Block";
            this.categoryBlock.Properties.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("categoryBlock.Properties.ImageOptions.SvgImage")));
            this.categoryBlock.Properties.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            // 
            // rowBlockName
            // 
            this.rowBlockName.Height = 18;
            this.rowBlockName.Name = "rowBlockName";
            this.rowBlockName.Properties.Caption = "Name";
            this.rowBlockName.Properties.FieldName = "BlockName";
            this.rowBlockName.Properties.RowEdit = this.repositoryItemComboBoxBlock;
            // 
            // categoryLineType
            // 
            this.categoryLineType.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowLineTypeName,
            this.rowLineTypeScale,
            this.rowLineWeight,
            this.rowLineWeightMethod});
            this.categoryLineType.Name = "categoryLineType";
            this.categoryLineType.Properties.Caption = "LineType";
            this.categoryLineType.Properties.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("categoryLineType.Properties.ImageOptions.SvgImage")));
            this.categoryLineType.Properties.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            // 
            // rowLineTypeName
            // 
            this.rowLineTypeName.Name = "rowLineTypeName";
            this.rowLineTypeName.Properties.Caption = "Name";
            this.rowLineTypeName.Properties.FieldName = "LineTypeName";
            this.rowLineTypeName.Properties.RowEdit = this.repositoryItemComboBoxLineType;
            // 
            // rowLineTypeScale
            // 
            this.rowLineTypeScale.Name = "rowLineTypeScale";
            this.rowLineTypeScale.Properties.Caption = "Scale";
            this.rowLineTypeScale.Properties.FieldName = "LineTypeScale";
            // 
            // rowLineWeight
            // 
            this.rowLineWeight.Name = "rowLineWeight";
            this.rowLineWeight.Properties.Caption = "Weight";
            this.rowLineWeight.Properties.FieldName = "LineWeight";
            // 
            // rowLineWeightMethod
            // 
            this.rowLineWeightMethod.Name = "rowLineWeightMethod";
            this.rowLineWeightMethod.Properties.Caption = "Weight Type";
            this.rowLineWeightMethod.Properties.FieldName = "LineWeightMethod";
            // 
            // categoryText
            // 
            this.categoryText.ChildRows.AddRange(new DevExpress.XtraVerticalGrid.Rows.BaseRow[] {
            this.rowTextString,
            this.rowStyleName,
            this.rowHeight,
            this.rowBillboard,
            this.rowWidthFactor,
            this.rowInsertionPoint,
            this.rowBackward,
            this.rowUpsideDown,
            this.rowAlignment});
            this.categoryText.Height = 20;
            this.categoryText.Name = "categoryText";
            this.categoryText.Properties.Caption = "Text";
            this.categoryText.Properties.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("categoryText.Properties.ImageOptions.SvgImage")));
            this.categoryText.Properties.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            // 
            // rowTextString
            // 
            this.rowTextString.Name = "rowTextString";
            this.rowTextString.Properties.Caption = "Contents";
            this.rowTextString.Properties.FieldName = "TextString";
            // 
            // rowStyleName
            // 
            this.rowStyleName.Name = "rowStyleName";
            this.rowStyleName.Properties.Caption = "Style";
            this.rowStyleName.Properties.FieldName = "StyleName";
            this.rowStyleName.Properties.RowEdit = this.repositoryItemComboBoxTextStyle;
            // 
            // rowHeight
            // 
            this.rowHeight.Name = "rowHeight";
            this.rowHeight.Properties.Caption = "Height";
            this.rowHeight.Properties.FieldName = "Height";
            // 
            // rowBillboard
            // 
            this.rowBillboard.Name = "rowBillboard";
            this.rowBillboard.Properties.Caption = "Billboard";
            this.rowBillboard.Properties.FieldName = "Billboard";
            // 
            // rowWidthFactor
            // 
            this.rowWidthFactor.Name = "rowWidthFactor";
            this.rowWidthFactor.Properties.Caption = "Width Factor";
            this.rowWidthFactor.Properties.FieldName = "WidthFactor";
            // 
            // rowInsertionPoint
            // 
            this.rowInsertionPoint.Name = "rowInsertionPoint";
            this.rowInsertionPoint.Properties.Caption = "Point";
            this.rowInsertionPoint.Properties.FieldName = "InsertionPoint";
            // 
            // rowBackward
            // 
            this.rowBackward.Name = "rowBackward";
            this.rowBackward.Properties.Caption = "Backward";
            this.rowBackward.Properties.FieldName = "Backward";
            // 
            // rowUpsideDown
            // 
            this.rowUpsideDown.Name = "rowUpsideDown";
            this.rowUpsideDown.Properties.Caption = "Upsidedown";
            this.rowUpsideDown.Properties.FieldName = "UpsideDown";
            // 
            // rowAlignment
            // 
            this.rowAlignment.Name = "rowAlignment";
            this.rowAlignment.Properties.Caption = "Alignment";
            this.rowAlignment.Properties.FieldName = "Alignment";
            // 
            // dockPanelObjectTree
            // 
            this.dockPanelObjectTree.Controls.Add(this.dockPanel3_Container);
            this.dockPanelObjectTree.Dock = DevExpress.XtraBars.Docking.DockingStyle.Fill;
            this.dockPanelObjectTree.ID = new System.Guid("c306bba9-1d83-4cca-b897-1e005975cc6f");
            this.dockPanelObjectTree.Location = new System.Drawing.Point(3, 26);
            this.dockPanelObjectTree.Name = "dockPanelObjectTree";
            this.dockPanelObjectTree.OriginalSize = new System.Drawing.Size(252, 524);
            this.dockPanelObjectTree.Size = new System.Drawing.Size(252, 552);
            this.dockPanelObjectTree.Text = "Object Tree";
            // 
            // dockPanel3_Container
            // 
            this.dockPanel3_Container.Controls.Add(this.treeListObject);
            this.dockPanel3_Container.Location = new System.Drawing.Point(0, 0);
            this.dockPanel3_Container.Name = "dockPanel3_Container";
            this.dockPanel3_Container.Size = new System.Drawing.Size(252, 552);
            this.dockPanel3_Container.TabIndex = 0;
            // 
            // treeListObject
            // 
            this.treeListObject.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeListObject.Location = new System.Drawing.Point(0, 0);
            this.treeListObject.Name = "treeListObject";
            this.treeListObject.Size = new System.Drawing.Size(252, 552);
            this.treeListObject.TabIndex = 0;
            // 
            // dockPanelDynamicInput
            // 
            this.dockPanelDynamicInput.Controls.Add(this.controlContainer1);
            this.dockPanelDynamicInput.Dock = DevExpress.XtraBars.Docking.DockingStyle.Right;
            this.dockPanelDynamicInput.FloatVertical = true;
            this.dockPanelDynamicInput.ID = new System.Guid("754d9043-9e08-4dbc-87a8-0af55da86043");
            this.dockPanelDynamicInput.Location = new System.Drawing.Point(979, 64);
            this.dockPanelDynamicInput.Name = "dockPanelDynamicInput";
            this.dockPanelDynamicInput.OriginalSize = new System.Drawing.Size(260, 200);
            this.dockPanelDynamicInput.Size = new System.Drawing.Size(260, 581);
            this.dockPanelDynamicInput.Text = "Dynamic input";
            // 
            // controlContainer1
            // 
            this.controlContainer1.Controls.Add(this.controlCommandBar1);
            this.controlContainer1.Controls.Add(this.controlAds1);
            this.controlContainer1.Location = new System.Drawing.Point(4, 26);
            this.controlContainer1.Name = "controlContainer1";
            this.controlContainer1.Size = new System.Drawing.Size(253, 552);
            this.controlContainer1.TabIndex = 0;
            // 
            // controlCommandBar1
            // 
            this.controlCommandBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.controlCommandBar1.enabled = true;
            this.controlCommandBar1.Location = new System.Drawing.Point(0, 280);
            this.controlCommandBar1.Name = "controlCommandBar1";
            this.controlCommandBar1.Size = new System.Drawing.Size(253, 22);
            this.controlCommandBar1.TabIndex = 11;
            // 
            // controlAds1
            // 
            this.controlAds1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.controlAds1.Location = new System.Drawing.Point(0, 302);
            this.controlAds1.Name = "controlAds1";
            this.controlAds1.Size = new System.Drawing.Size(253, 250);
            this.controlAds1.TabIndex = 10;
            this.controlAds1.url = "http://hileejaeho.cafe24.com/br3d-ad/";
            this.controlAds1.Visible = false;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.ribbonControl1.SearchEditItem,
            this.barButtonItemDrawCircle,
            this.barButtonItemDrawArc,
            this.barButtonItemDrawPolyline,
            this.barButtonItemDrawSpline,
            this.barStaticItem1,
            this.barStaticItemCoordinates,
            this.barButtonItemDrawLine,
            this.barButtonItemDrawText,
            this.barButtonItemInsert,
            this.barButtonItemDrawCylinder,
            this.barButtonItemDrawArc_FirstSecondThird,
            this.barButtonItemDrawArc_CenterStartEnd,
            this.barButtonItemOpen,
            this.barButtonItemSaveAs,
            this.barButtonItemSaveImage,
            this.barButtonItemExit,
            this.barButtonItemDimHorizontal,
            this.barButtonItemDimVertical,
            this.barButtonItemDimAlign,
            this.barButtonItemDimDiameter,
            this.barButtonItemDimRadius,
            this.barButtonItemDimLeader,
            this.barButtonItemErase,
            this.barButtonItemMove,
            this.barButtonItemCopy,
            this.barButtonItemScale,
            this.barButtonItemRotate,
            this.barButtonItemOffset,
            this.barButtonItemMirror,
            this.barButtonItemExplode,
            this.barButtonItemTrim,
            this.barButtonItemFillet,
            this.barButtonItemChamfer,
            this.barButtonItemLanguage,
            this.barButtonItemCheckForUpdate,
            this.barButtonItemHomepage,
            this.barButtonItemAbout,
            this.barButtonItemLayer,
            this.barButtonItemTextStyle,
            this.barButtonItemLineType,
            this.barButtonItemCoordinates,
            this.barButtonItemDistance,
            this.barButtonItemMemo,
            this.barButtonItemClearAnnotations,
            this.barButtonItemSingleView,
            this.barButtonItem1x1View,
            this.barButtonItem1x2View,
            this.barButtonItem2x2View,
            this.barButtonItemOrthoMode,
            this.barButtonItemOsnapend,
            this.barButtonItemOsnapIntersection,
            this.barButtonItemOsnapMiddle,
            this.barButtonItemOsnapCenter,
            this.barButtonItemOsnapPoint,
            this.barButtonItemLanguageKorean,
            this.barButtonItemLanguageEnglish,
            this.barButtonItemMultilineText,
            this.barButtonItemInsertImage,
            this.barButtonItemShowGrid,
            this.barButtonItemShowToolbar,
            this.barButtonItemShowSymbol,
            this.barButtonItemOptions,
            this.barButtonItemWorkspace,
            this.barButtonItemEndPoint,
            this.barButtonItemIntPoint,
            this.barButtonItemMidPoint,
            this.barButtonItemCenterPoint,
            this.barButtonItemNodePoint,
            this.barButtonItemDrawBox,
            this.barButtonItemDrawCone,
            this.barButtonItemDrawSphere});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.MaxItemId = 77;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPageHome,
            this.ribbonPageDraw,
            this.ribbonPageDraw3D,
            this.ribbonPageDimension,
            this.ribbonPageEdit,
            this.ribbonPageAnnotation,
            this.ribbonPageTools,
            this.ribbonPageOptions});
            this.ribbonControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemRibbonSearchEdit1,
            this.repositoryItemButtonEdit1,
            this.repositoryItemCheckEdit1});
            this.ribbonControl1.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonControlStyle.OfficeUniversal;
            this.ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.Size = new System.Drawing.Size(1239, 64);
            this.ribbonControl1.StatusBar = this.ribbonStatusBar1;
            this.ribbonControl1.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
            this.ribbonControl1.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.ribbonControl1_ItemClick);
            // 
            // barButtonItemDrawCircle
            // 
            this.barButtonItemDrawCircle.Caption = "Circle";
            this.barButtonItemDrawCircle.Id = 2;
            this.barButtonItemDrawCircle.ImageOptions.Image = global::Br3D.Properties.Resources.circle_32px;
            this.barButtonItemDrawCircle.ImageOptions.LargeImage = global::Br3D.Properties.Resources.circle_32px;
            this.barButtonItemDrawCircle.Name = "barButtonItemDrawCircle";
            // 
            // barButtonItemDrawArc
            // 
            this.barButtonItemDrawArc.ActAsDropDown = true;
            this.barButtonItemDrawArc.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.barButtonItemDrawArc.Caption = "Arc";
            this.barButtonItemDrawArc.DropDownControl = this.popupMenuDrawArc;
            this.barButtonItemDrawArc.Id = 3;
            this.barButtonItemDrawArc.ImageOptions.LargeImage = global::Br3D.Properties.Resources.Create_Arc_Small1;
            this.barButtonItemDrawArc.Name = "barButtonItemDrawArc";
            // 
            // popupMenuDrawArc
            // 
            this.popupMenuDrawArc.ItemLinks.Add(this.barButtonItemDrawArc_FirstSecondThird);
            this.popupMenuDrawArc.ItemLinks.Add(this.barButtonItemDrawArc_CenterStartEnd);
            this.popupMenuDrawArc.Name = "popupMenuDrawArc";
            this.popupMenuDrawArc.Ribbon = this.ribbonControl1;
            // 
            // barButtonItemDrawArc_FirstSecondThird
            // 
            this.barButtonItemDrawArc_FirstSecondThird.Caption = "First, Second, Third";
            this.barButtonItemDrawArc_FirstSecondThird.Id = 14;
            this.barButtonItemDrawArc_FirstSecondThird.Name = "barButtonItemDrawArc_FirstSecondThird";
            // 
            // barButtonItemDrawArc_CenterStartEnd
            // 
            this.barButtonItemDrawArc_CenterStartEnd.Caption = "Center, Start, End";
            this.barButtonItemDrawArc_CenterStartEnd.Id = 15;
            this.barButtonItemDrawArc_CenterStartEnd.Name = "barButtonItemDrawArc_CenterStartEnd";
            // 
            // barButtonItemDrawPolyline
            // 
            this.barButtonItemDrawPolyline.Caption = "Polyline";
            this.barButtonItemDrawPolyline.Id = 4;
            this.barButtonItemDrawPolyline.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemDrawPolyline.ImageOptions.SvgImage")));
            this.barButtonItemDrawPolyline.Name = "barButtonItemDrawPolyline";
            // 
            // barButtonItemDrawSpline
            // 
            this.barButtonItemDrawSpline.Caption = "Spline";
            this.barButtonItemDrawSpline.Id = 5;
            this.barButtonItemDrawSpline.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemDrawSpline.ImageOptions.SvgImage")));
            this.barButtonItemDrawSpline.Name = "barButtonItemDrawSpline";
            // 
            // barStaticItem1
            // 
            this.barStaticItem1.Caption = "barStaticItem1";
            this.barStaticItem1.Id = 6;
            this.barStaticItem1.Name = "barStaticItem1";
            // 
            // barStaticItemCoordinates
            // 
            this.barStaticItemCoordinates.Caption = "barStaticItem2";
            this.barStaticItemCoordinates.Id = 7;
            this.barStaticItemCoordinates.Name = "barStaticItemCoordinates";
            // 
            // barButtonItemDrawLine
            // 
            this.barButtonItemDrawLine.Caption = "Line";
            this.barButtonItemDrawLine.Id = 10;
            this.barButtonItemDrawLine.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemDrawLine.ImageOptions.SvgImage")));
            this.barButtonItemDrawLine.Name = "barButtonItemDrawLine";
            // 
            // barButtonItemDrawText
            // 
            this.barButtonItemDrawText.Caption = "Text";
            this.barButtonItemDrawText.Id = 11;
            this.barButtonItemDrawText.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemDrawText.ImageOptions.SvgImage")));
            this.barButtonItemDrawText.Name = "barButtonItemDrawText";
            // 
            // barButtonItemInsert
            // 
            this.barButtonItemInsert.Caption = "Insert";
            this.barButtonItemInsert.Id = 12;
            this.barButtonItemInsert.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemInsert.ImageOptions.SvgImage")));
            this.barButtonItemInsert.Name = "barButtonItemInsert";
            // 
            // barButtonItemDrawCylinder
            // 
            this.barButtonItemDrawCylinder.Caption = "Cylinder";
            this.barButtonItemDrawCylinder.Id = 13;
            this.barButtonItemDrawCylinder.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemDrawCylinder.ImageOptions.SvgImage")));
            this.barButtonItemDrawCylinder.Name = "barButtonItemDrawCylinder";
            // 
            // barButtonItemOpen
            // 
            this.barButtonItemOpen.Caption = "Open";
            this.barButtonItemOpen.Id = 16;
            this.barButtonItemOpen.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemOpen.ImageOptions.SvgImage")));
            this.barButtonItemOpen.Name = "barButtonItemOpen";
            // 
            // barButtonItemSaveAs
            // 
            this.barButtonItemSaveAs.Caption = "Save As";
            this.barButtonItemSaveAs.Id = 17;
            this.barButtonItemSaveAs.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemSaveAs.ImageOptions.SvgImage")));
            this.barButtonItemSaveAs.Name = "barButtonItemSaveAs";
            // 
            // barButtonItemSaveImage
            // 
            this.barButtonItemSaveImage.Caption = "Save Image";
            this.barButtonItemSaveImage.Id = 18;
            this.barButtonItemSaveImage.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemSaveImage.ImageOptions.SvgImage")));
            this.barButtonItemSaveImage.Name = "barButtonItemSaveImage";
            // 
            // barButtonItemExit
            // 
            this.barButtonItemExit.Caption = "Exit";
            this.barButtonItemExit.Id = 19;
            this.barButtonItemExit.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemExit.ImageOptions.SvgImage")));
            this.barButtonItemExit.Name = "barButtonItemExit";
            // 
            // barButtonItemDimHorizontal
            // 
            this.barButtonItemDimHorizontal.Caption = "Horizontal";
            this.barButtonItemDimHorizontal.Id = 20;
            this.barButtonItemDimHorizontal.ImageOptions.Image = global::Br3D.Properties.Resources.Annotation_DimHorizontal_Small;
            this.barButtonItemDimHorizontal.ImageOptions.LargeImage = global::Br3D.Properties.Resources.Annotation_DimHorizontal_Small;
            this.barButtonItemDimHorizontal.Name = "barButtonItemDimHorizontal";
            // 
            // barButtonItemDimVertical
            // 
            this.barButtonItemDimVertical.Caption = "Vertical";
            this.barButtonItemDimVertical.Id = 21;
            this.barButtonItemDimVertical.ImageOptions.Image = global::Br3D.Properties.Resources.Annotation_DimVertical_Small;
            this.barButtonItemDimVertical.ImageOptions.LargeImage = global::Br3D.Properties.Resources.Annotation_DimVertical_Small;
            this.barButtonItemDimVertical.Name = "barButtonItemDimVertical";
            // 
            // barButtonItemDimAlign
            // 
            this.barButtonItemDimAlign.Caption = "Align";
            this.barButtonItemDimAlign.Id = 22;
            this.barButtonItemDimAlign.ImageOptions.Image = global::Br3D.Properties.Resources.Annotation_Dimaligned_Small;
            this.barButtonItemDimAlign.ImageOptions.LargeImage = global::Br3D.Properties.Resources.Annotation_Dimaligned_Small;
            this.barButtonItemDimAlign.Name = "barButtonItemDimAlign";
            // 
            // barButtonItemDimDiameter
            // 
            this.barButtonItemDimDiameter.Caption = "Diameter";
            this.barButtonItemDimDiameter.Id = 23;
            this.barButtonItemDimDiameter.ImageOptions.Image = global::Br3D.Properties.Resources.Annotation_Dimdiameter_Small;
            this.barButtonItemDimDiameter.ImageOptions.LargeImage = global::Br3D.Properties.Resources.Annotation_Dimdiameter_Small;
            this.barButtonItemDimDiameter.Name = "barButtonItemDimDiameter";
            // 
            // barButtonItemDimRadius
            // 
            this.barButtonItemDimRadius.Caption = "Radius";
            this.barButtonItemDimRadius.Id = 24;
            this.barButtonItemDimRadius.ImageOptions.Image = global::Br3D.Properties.Resources.Annotation_Dimradius_Small;
            this.barButtonItemDimRadius.ImageOptions.LargeImage = global::Br3D.Properties.Resources.Annotation_Dimradius_Small;
            this.barButtonItemDimRadius.Name = "barButtonItemDimRadius";
            // 
            // barButtonItemDimLeader
            // 
            this.barButtonItemDimLeader.Caption = "Leader";
            this.barButtonItemDimLeader.Id = 25;
            this.barButtonItemDimLeader.ImageOptions.Image = global::Br3D.Properties.Resources.Annotation_Leader_Small;
            this.barButtonItemDimLeader.ImageOptions.LargeImage = global::Br3D.Properties.Resources.Annotation_Leader_Small;
            this.barButtonItemDimLeader.Name = "barButtonItemDimLeader";
            // 
            // barButtonItemErase
            // 
            this.barButtonItemErase.Caption = "Erase";
            this.barButtonItemErase.Id = 26;
            this.barButtonItemErase.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemErase.ImageOptions.SvgImage")));
            this.barButtonItemErase.Name = "barButtonItemErase";
            // 
            // barButtonItemMove
            // 
            this.barButtonItemMove.Caption = "Move";
            this.barButtonItemMove.Id = 27;
            this.barButtonItemMove.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemMove.ImageOptions.SvgImage")));
            this.barButtonItemMove.Name = "barButtonItemMove";
            // 
            // barButtonItemCopy
            // 
            this.barButtonItemCopy.Caption = "Copy";
            this.barButtonItemCopy.Id = 28;
            this.barButtonItemCopy.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemCopy.ImageOptions.SvgImage")));
            this.barButtonItemCopy.Name = "barButtonItemCopy";
            // 
            // barButtonItemScale
            // 
            this.barButtonItemScale.Caption = "Scale";
            this.barButtonItemScale.Id = 29;
            this.barButtonItemScale.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemScale.ImageOptions.SvgImage")));
            this.barButtonItemScale.Name = "barButtonItemScale";
            // 
            // barButtonItemRotate
            // 
            this.barButtonItemRotate.Caption = "Rotate";
            this.barButtonItemRotate.Id = 30;
            this.barButtonItemRotate.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemRotate.ImageOptions.SvgImage")));
            this.barButtonItemRotate.Name = "barButtonItemRotate";
            // 
            // barButtonItemOffset
            // 
            this.barButtonItemOffset.Caption = "Offset";
            this.barButtonItemOffset.Id = 31;
            this.barButtonItemOffset.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemOffset.ImageOptions.SvgImage")));
            this.barButtonItemOffset.Name = "barButtonItemOffset";
            // 
            // barButtonItemMirror
            // 
            this.barButtonItemMirror.Caption = "Mirror";
            this.barButtonItemMirror.Id = 32;
            this.barButtonItemMirror.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemMirror.ImageOptions.SvgImage")));
            this.barButtonItemMirror.Name = "barButtonItemMirror";
            // 
            // barButtonItemExplode
            // 
            this.barButtonItemExplode.Caption = "Explode";
            this.barButtonItemExplode.Id = 33;
            this.barButtonItemExplode.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemExplode.ImageOptions.SvgImage")));
            this.barButtonItemExplode.Name = "barButtonItemExplode";
            // 
            // barButtonItemTrim
            // 
            this.barButtonItemTrim.Caption = "Trim";
            this.barButtonItemTrim.Id = 34;
            this.barButtonItemTrim.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemTrim.ImageOptions.SvgImage")));
            this.barButtonItemTrim.Name = "barButtonItemTrim";
            // 
            // barButtonItemFillet
            // 
            this.barButtonItemFillet.Caption = "Fillet";
            this.barButtonItemFillet.Id = 35;
            this.barButtonItemFillet.ImageOptions.Image = global::Br3D.Properties.Resources.Edit_Fillet_Small;
            this.barButtonItemFillet.ImageOptions.LargeImage = global::Br3D.Properties.Resources.Edit_Fillet_Small;
            this.barButtonItemFillet.Name = "barButtonItemFillet";
            // 
            // barButtonItemChamfer
            // 
            this.barButtonItemChamfer.Caption = "Chamfer";
            this.barButtonItemChamfer.Id = 36;
            this.barButtonItemChamfer.ImageOptions.Image = global::Br3D.Properties.Resources.Edit_Chamfer_Small;
            this.barButtonItemChamfer.ImageOptions.LargeImage = global::Br3D.Properties.Resources.Edit_Chamfer_Small;
            this.barButtonItemChamfer.Name = "barButtonItemChamfer";
            // 
            // barButtonItemLanguage
            // 
            this.barButtonItemLanguage.ActAsDropDown = true;
            this.barButtonItemLanguage.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.DropDown;
            this.barButtonItemLanguage.Caption = "Language";
            this.barButtonItemLanguage.DropDownControl = this.popupMenuLanguage;
            this.barButtonItemLanguage.Id = 37;
            this.barButtonItemLanguage.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemLanguage.ImageOptions.SvgImage")));
            this.barButtonItemLanguage.Name = "barButtonItemLanguage";
            // 
            // popupMenuLanguage
            // 
            this.popupMenuLanguage.ItemLinks.Add(this.barButtonItemLanguageKorean);
            this.popupMenuLanguage.ItemLinks.Add(this.barButtonItemLanguageEnglish);
            this.popupMenuLanguage.Name = "popupMenuLanguage";
            this.popupMenuLanguage.Ribbon = this.ribbonControl1;
            // 
            // barButtonItemLanguageKorean
            // 
            this.barButtonItemLanguageKorean.Caption = "Korean";
            this.barButtonItemLanguageKorean.Id = 58;
            this.barButtonItemLanguageKorean.ImageOptions.Image = global::Br3D.Properties.Resources.south_korea_16px;
            this.barButtonItemLanguageKorean.Name = "barButtonItemLanguageKorean";
            // 
            // barButtonItemLanguageEnglish
            // 
            this.barButtonItemLanguageEnglish.Caption = "English";
            this.barButtonItemLanguageEnglish.Id = 59;
            this.barButtonItemLanguageEnglish.ImageOptions.Image = global::Br3D.Properties.Resources.usa_16px;
            this.barButtonItemLanguageEnglish.Name = "barButtonItemLanguageEnglish";
            // 
            // barButtonItemCheckForUpdate
            // 
            this.barButtonItemCheckForUpdate.Caption = "Check For Update";
            this.barButtonItemCheckForUpdate.Id = 38;
            this.barButtonItemCheckForUpdate.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemCheckForUpdate.ImageOptions.SvgImage")));
            this.barButtonItemCheckForUpdate.Name = "barButtonItemCheckForUpdate";
            // 
            // barButtonItemHomepage
            // 
            this.barButtonItemHomepage.Caption = "Homepage";
            this.barButtonItemHomepage.Id = 39;
            this.barButtonItemHomepage.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemHomepage.ImageOptions.SvgImage")));
            this.barButtonItemHomepage.Name = "barButtonItemHomepage";
            // 
            // barButtonItemAbout
            // 
            this.barButtonItemAbout.Caption = "About";
            this.barButtonItemAbout.Id = 40;
            this.barButtonItemAbout.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemAbout.ImageOptions.SvgImage")));
            this.barButtonItemAbout.Name = "barButtonItemAbout";
            // 
            // barButtonItemLayer
            // 
            this.barButtonItemLayer.Caption = "Layer";
            this.barButtonItemLayer.Id = 41;
            this.barButtonItemLayer.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemLayer.ImageOptions.SvgImage")));
            this.barButtonItemLayer.Name = "barButtonItemLayer";
            // 
            // barButtonItemTextStyle
            // 
            this.barButtonItemTextStyle.Caption = "Text Style";
            this.barButtonItemTextStyle.Id = 42;
            this.barButtonItemTextStyle.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemTextStyle.ImageOptions.SvgImage")));
            this.barButtonItemTextStyle.Name = "barButtonItemTextStyle";
            // 
            // barButtonItemLineType
            // 
            this.barButtonItemLineType.Caption = "Line Type";
            this.barButtonItemLineType.Id = 43;
            this.barButtonItemLineType.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemLineType.ImageOptions.SvgImage")));
            this.barButtonItemLineType.Name = "barButtonItemLineType";
            // 
            // barButtonItemCoordinates
            // 
            this.barButtonItemCoordinates.Caption = "Coordinates";
            this.barButtonItemCoordinates.Id = 44;
            this.barButtonItemCoordinates.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemCoordinates.ImageOptions.SvgImage")));
            this.barButtonItemCoordinates.Name = "barButtonItemCoordinates";
            // 
            // barButtonItemDistance
            // 
            this.barButtonItemDistance.Caption = "Distance";
            this.barButtonItemDistance.Id = 45;
            this.barButtonItemDistance.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemDistance.ImageOptions.SvgImage")));
            this.barButtonItemDistance.Name = "barButtonItemDistance";
            // 
            // barButtonItemMemo
            // 
            this.barButtonItemMemo.Caption = "Memo";
            this.barButtonItemMemo.Id = 46;
            this.barButtonItemMemo.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemMemo.ImageOptions.SvgImage")));
            this.barButtonItemMemo.Name = "barButtonItemMemo";
            // 
            // barButtonItemClearAnnotations
            // 
            this.barButtonItemClearAnnotations.Caption = "Clear Annotations";
            this.barButtonItemClearAnnotations.Id = 47;
            this.barButtonItemClearAnnotations.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemClearAnnotations.ImageOptions.SvgImage")));
            this.barButtonItemClearAnnotations.Name = "barButtonItemClearAnnotations";
            // 
            // barButtonItemSingleView
            // 
            this.barButtonItemSingleView.Caption = "Single";
            this.barButtonItemSingleView.Id = 48;
            this.barButtonItemSingleView.ImageOptions.Image = global::Br3D.Properties.Resources.view_single;
            this.barButtonItemSingleView.Name = "barButtonItemSingleView";
            // 
            // barButtonItem1x1View
            // 
            this.barButtonItem1x1View.Caption = "1x1";
            this.barButtonItem1x1View.Id = 49;
            this.barButtonItem1x1View.ImageOptions.Image = global::Br3D.Properties.Resources.view_1x1;
            this.barButtonItem1x1View.Name = "barButtonItem1x1View";
            // 
            // barButtonItem1x2View
            // 
            this.barButtonItem1x2View.Caption = "1x2";
            this.barButtonItem1x2View.Id = 50;
            this.barButtonItem1x2View.ImageOptions.Image = global::Br3D.Properties.Resources.view_1x2;
            this.barButtonItem1x2View.Name = "barButtonItem1x2View";
            // 
            // barButtonItem2x2View
            // 
            this.barButtonItem2x2View.Caption = "2x2";
            this.barButtonItem2x2View.Id = 51;
            this.barButtonItem2x2View.ImageOptions.Image = global::Br3D.Properties.Resources.view_2x2;
            this.barButtonItem2x2View.Name = "barButtonItem2x2View";
            // 
            // barButtonItemOrthoMode
            // 
            this.barButtonItemOrthoMode.Caption = "Ortho Mode";
            this.barButtonItemOrthoMode.Id = 52;
            this.barButtonItemOrthoMode.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemOrthoMode.ImageOptions.SvgImage")));
            this.barButtonItemOrthoMode.Name = "barButtonItemOrthoMode";
            // 
            // barButtonItemOsnapend
            // 
            this.barButtonItemOsnapend.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.barButtonItemOsnapend.Caption = "End";
            this.barButtonItemOsnapend.Id = 53;
            this.barButtonItemOsnapend.ImageOptions.Image = global::Br3D.Properties.Resources.snap_endpoint_small;
            this.barButtonItemOsnapend.Name = "barButtonItemOsnapend";
            // 
            // barButtonItemOsnapIntersection
            // 
            this.barButtonItemOsnapIntersection.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.barButtonItemOsnapIntersection.Caption = "Intersection";
            this.barButtonItemOsnapIntersection.Id = 54;
            this.barButtonItemOsnapIntersection.ImageOptions.Image = global::Br3D.Properties.Resources.snap_intersection_small;
            this.barButtonItemOsnapIntersection.Name = "barButtonItemOsnapIntersection";
            // 
            // barButtonItemOsnapMiddle
            // 
            this.barButtonItemOsnapMiddle.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.barButtonItemOsnapMiddle.Caption = "Middle";
            this.barButtonItemOsnapMiddle.Id = 55;
            this.barButtonItemOsnapMiddle.ImageOptions.Image = global::Br3D.Properties.Resources.snap_middle_small;
            this.barButtonItemOsnapMiddle.Name = "barButtonItemOsnapMiddle";
            // 
            // barButtonItemOsnapCenter
            // 
            this.barButtonItemOsnapCenter.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.barButtonItemOsnapCenter.Caption = "Center";
            this.barButtonItemOsnapCenter.Id = 56;
            this.barButtonItemOsnapCenter.ImageOptions.Image = global::Br3D.Properties.Resources.snap_center_small;
            this.barButtonItemOsnapCenter.Name = "barButtonItemOsnapCenter";
            // 
            // barButtonItemOsnapPoint
            // 
            this.barButtonItemOsnapPoint.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.barButtonItemOsnapPoint.Caption = "Point";
            this.barButtonItemOsnapPoint.Id = 57;
            this.barButtonItemOsnapPoint.ImageOptions.Image = global::Br3D.Properties.Resources.snap_node_small;
            this.barButtonItemOsnapPoint.Name = "barButtonItemOsnapPoint";
            // 
            // barButtonItemMultilineText
            // 
            this.barButtonItemMultilineText.Caption = "Multiline Text";
            this.barButtonItemMultilineText.Id = 60;
            this.barButtonItemMultilineText.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemMultilineText.ImageOptions.SvgImage")));
            this.barButtonItemMultilineText.Name = "barButtonItemMultilineText";
            // 
            // barButtonItemInsertImage
            // 
            this.barButtonItemInsertImage.Caption = "Insert Image";
            this.barButtonItemInsertImage.Id = 61;
            this.barButtonItemInsertImage.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemInsertImage.ImageOptions.SvgImage")));
            this.barButtonItemInsertImage.Name = "barButtonItemInsertImage";
            // 
            // barButtonItemShowGrid
            // 
            this.barButtonItemShowGrid.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.barButtonItemShowGrid.Caption = "Grid";
            this.barButtonItemShowGrid.Down = true;
            this.barButtonItemShowGrid.Id = 62;
            this.barButtonItemShowGrid.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemShowGrid.ImageOptions.SvgImage")));
            this.barButtonItemShowGrid.Name = "barButtonItemShowGrid";
            this.barButtonItemShowGrid.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemShowGrid_ItemClick);
            // 
            // barButtonItemShowToolbar
            // 
            this.barButtonItemShowToolbar.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.barButtonItemShowToolbar.Caption = "Toolbar";
            this.barButtonItemShowToolbar.Down = true;
            this.barButtonItemShowToolbar.Id = 63;
            this.barButtonItemShowToolbar.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemShowToolbar.ImageOptions.SvgImage")));
            this.barButtonItemShowToolbar.Name = "barButtonItemShowToolbar";
            this.barButtonItemShowToolbar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemShowToolbar_ItemClick);
            // 
            // barButtonItemShowSymbol
            // 
            this.barButtonItemShowSymbol.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.barButtonItemShowSymbol.Caption = "Symbol";
            this.barButtonItemShowSymbol.Down = true;
            this.barButtonItemShowSymbol.Id = 64;
            this.barButtonItemShowSymbol.Name = "barButtonItemShowSymbol";
            this.barButtonItemShowSymbol.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItemShowSymbol_ItemClick);
            // 
            // barButtonItemOptions
            // 
            this.barButtonItemOptions.Caption = "Options";
            this.barButtonItemOptions.Id = 65;
            this.barButtonItemOptions.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemOptions.ImageOptions.SvgImage")));
            this.barButtonItemOptions.Name = "barButtonItemOptions";
            // 
            // barButtonItemWorkspace
            // 
            this.barButtonItemWorkspace.Caption = "Workspace";
            this.barButtonItemWorkspace.Id = 66;
            this.barButtonItemWorkspace.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemWorkspace.ImageOptions.SvgImage")));
            this.barButtonItemWorkspace.Name = "barButtonItemWorkspace";
            // 
            // barButtonItemEndPoint
            // 
            this.barButtonItemEndPoint.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItemEndPoint.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.barButtonItemEndPoint.Id = 69;
            this.barButtonItemEndPoint.ImageOptions.Image = global::Br3D.Properties.Resources.snap_endpoint_small;
            this.barButtonItemEndPoint.Name = "barButtonItemEndPoint";
            // 
            // barButtonItemIntPoint
            // 
            this.barButtonItemIntPoint.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItemIntPoint.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.barButtonItemIntPoint.Id = 70;
            this.barButtonItemIntPoint.ImageOptions.Image = global::Br3D.Properties.Resources.snap_intersection_small;
            this.barButtonItemIntPoint.Name = "barButtonItemIntPoint";
            // 
            // barButtonItemMidPoint
            // 
            this.barButtonItemMidPoint.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItemMidPoint.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.barButtonItemMidPoint.Id = 71;
            this.barButtonItemMidPoint.ImageOptions.Image = global::Br3D.Properties.Resources.snap_middle_small;
            this.barButtonItemMidPoint.Name = "barButtonItemMidPoint";
            // 
            // barButtonItemCenterPoint
            // 
            this.barButtonItemCenterPoint.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItemCenterPoint.ButtonStyle = DevExpress.XtraBars.BarButtonStyle.Check;
            this.barButtonItemCenterPoint.Id = 72;
            this.barButtonItemCenterPoint.ImageOptions.Image = global::Br3D.Properties.Resources.snap_center_small;
            this.barButtonItemCenterPoint.Name = "barButtonItemCenterPoint";
            // 
            // barButtonItemNodePoint
            // 
            this.barButtonItemNodePoint.Alignment = DevExpress.XtraBars.BarItemLinkAlignment.Right;
            this.barButtonItemNodePoint.Id = 73;
            this.barButtonItemNodePoint.ImageOptions.Image = global::Br3D.Properties.Resources.snap_node_small;
            this.barButtonItemNodePoint.Name = "barButtonItemNodePoint";
            // 
            // barButtonItemDrawBox
            // 
            this.barButtonItemDrawBox.Caption = "Box";
            this.barButtonItemDrawBox.Id = 74;
            this.barButtonItemDrawBox.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemDrawBox.ImageOptions.SvgImage")));
            this.barButtonItemDrawBox.Name = "barButtonItemDrawBox";
            // 
            // barButtonItemDrawCone
            // 
            this.barButtonItemDrawCone.Caption = "Cone";
            this.barButtonItemDrawCone.Id = 75;
            this.barButtonItemDrawCone.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemDrawCone.ImageOptions.SvgImage")));
            this.barButtonItemDrawCone.Name = "barButtonItemDrawCone";
            // 
            // ribbonPageHome
            // 
            this.ribbonPageHome.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupFile,
            this.ribbonPageGroupWorkspace,
            this.ribbonPageGroupSystem});
            this.ribbonPageHome.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("ribbonPageHome.ImageOptions.SvgImage")));
            this.ribbonPageHome.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.ribbonPageHome.Name = "ribbonPageHome";
            this.ribbonPageHome.Text = "Home";
            // 
            // ribbonPageGroupFile
            // 
            this.ribbonPageGroupFile.ItemLinks.Add(this.barButtonItemOpen);
            this.ribbonPageGroupFile.ItemLinks.Add(this.barButtonItemSaveAs);
            this.ribbonPageGroupFile.ItemLinks.Add(this.barButtonItemSaveImage);
            this.ribbonPageGroupFile.Name = "ribbonPageGroupFile";
            this.ribbonPageGroupFile.Text = "File";
            // 
            // ribbonPageGroupWorkspace
            // 
            this.ribbonPageGroupWorkspace.ItemLinks.Add(this.barButtonItemWorkspace);
            this.ribbonPageGroupWorkspace.Name = "ribbonPageGroupWorkspace";
            this.ribbonPageGroupWorkspace.Text = "Workspace";
            // 
            // ribbonPageGroupSystem
            // 
            this.ribbonPageGroupSystem.ItemLinks.Add(this.barButtonItemExit);
            this.ribbonPageGroupSystem.Name = "ribbonPageGroupSystem";
            this.ribbonPageGroupSystem.Text = "System";
            // 
            // ribbonPageDraw
            // 
            this.ribbonPageDraw.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupDraw});
            this.ribbonPageDraw.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("ribbonPageDraw.ImageOptions.SvgImage")));
            this.ribbonPageDraw.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.ribbonPageDraw.Name = "ribbonPageDraw";
            this.ribbonPageDraw.Text = "Draw";
            // 
            // ribbonPageGroupDraw
            // 
            this.ribbonPageGroupDraw.ItemLinks.Add(this.barButtonItemDrawLine);
            this.ribbonPageGroupDraw.ItemLinks.Add(this.barButtonItemDrawCircle);
            this.ribbonPageGroupDraw.ItemLinks.Add(this.barButtonItemDrawArc);
            this.ribbonPageGroupDraw.ItemLinks.Add(this.barButtonItemDrawPolyline);
            this.ribbonPageGroupDraw.ItemLinks.Add(this.barButtonItemDrawSpline);
            this.ribbonPageGroupDraw.ItemLinks.Add(this.barButtonItemDrawText);
            this.ribbonPageGroupDraw.ItemLinks.Add(this.barButtonItemMultilineText);
            this.ribbonPageGroupDraw.ItemLinks.Add(this.barButtonItemInsert);
            this.ribbonPageGroupDraw.ItemLinks.Add(this.barButtonItemInsertImage);
            this.ribbonPageGroupDraw.Name = "ribbonPageGroupDraw";
            this.ribbonPageGroupDraw.Text = "Draw";
            // 
            // ribbonPageDraw3D
            // 
            this.ribbonPageDraw3D.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup2});
            this.ribbonPageDraw3D.Name = "ribbonPageDraw3D";
            this.ribbonPageDraw3D.Text = "Draw3D";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItemDrawCylinder);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItemDrawBox);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItemDrawCone);
            this.ribbonPageGroup2.ItemLinks.Add(this.barButtonItemDrawSphere);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.Text = "ribbonPageGroup2";
            // 
            // ribbonPageDimension
            // 
            this.ribbonPageDimension.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupDim});
            this.ribbonPageDimension.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("ribbonPageDimension.ImageOptions.SvgImage")));
            this.ribbonPageDimension.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.ribbonPageDimension.Name = "ribbonPageDimension";
            this.ribbonPageDimension.Text = "Dimension";
            // 
            // ribbonPageGroupDim
            // 
            this.ribbonPageGroupDim.ItemLinks.Add(this.barButtonItemDimHorizontal);
            this.ribbonPageGroupDim.ItemLinks.Add(this.barButtonItemDimVertical);
            this.ribbonPageGroupDim.ItemLinks.Add(this.barButtonItemDimAlign);
            this.ribbonPageGroupDim.ItemLinks.Add(this.barButtonItemDimDiameter);
            this.ribbonPageGroupDim.ItemLinks.Add(this.barButtonItemDimRadius);
            this.ribbonPageGroupDim.ItemLinks.Add(this.barButtonItemDimLeader);
            this.ribbonPageGroupDim.Name = "ribbonPageGroupDim";
            this.ribbonPageGroupDim.Text = "Dimension";
            // 
            // ribbonPageEdit
            // 
            this.ribbonPageEdit.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupEdit});
            this.ribbonPageEdit.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("ribbonPageEdit.ImageOptions.SvgImage")));
            this.ribbonPageEdit.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.ribbonPageEdit.Name = "ribbonPageEdit";
            this.ribbonPageEdit.Text = "Edit";
            // 
            // ribbonPageGroupEdit
            // 
            this.ribbonPageGroupEdit.ItemLinks.Add(this.barButtonItemErase);
            this.ribbonPageGroupEdit.ItemLinks.Add(this.barButtonItemMove);
            this.ribbonPageGroupEdit.ItemLinks.Add(this.barButtonItemCopy);
            this.ribbonPageGroupEdit.ItemLinks.Add(this.barButtonItemScale);
            this.ribbonPageGroupEdit.ItemLinks.Add(this.barButtonItemRotate);
            this.ribbonPageGroupEdit.ItemLinks.Add(this.barButtonItemOffset);
            this.ribbonPageGroupEdit.ItemLinks.Add(this.barButtonItemMirror);
            this.ribbonPageGroupEdit.ItemLinks.Add(this.barButtonItemExplode);
            this.ribbonPageGroupEdit.ItemLinks.Add(this.barButtonItemTrim);
            this.ribbonPageGroupEdit.ItemLinks.Add(this.barButtonItemFillet);
            this.ribbonPageGroupEdit.ItemLinks.Add(this.barButtonItemChamfer);
            this.ribbonPageGroupEdit.Name = "ribbonPageGroupEdit";
            this.ribbonPageGroupEdit.Text = "Edit";
            // 
            // ribbonPageAnnotation
            // 
            this.ribbonPageAnnotation.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupDrawAnnotation,
            this.ribbonPageGroupClearAnnotations});
            this.ribbonPageAnnotation.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("ribbonPageAnnotation.ImageOptions.SvgImage")));
            this.ribbonPageAnnotation.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.ribbonPageAnnotation.Name = "ribbonPageAnnotation";
            this.ribbonPageAnnotation.Text = "Annotation";
            // 
            // ribbonPageGroupDrawAnnotation
            // 
            this.ribbonPageGroupDrawAnnotation.ItemLinks.Add(this.barButtonItemCoordinates);
            this.ribbonPageGroupDrawAnnotation.ItemLinks.Add(this.barButtonItemDistance);
            this.ribbonPageGroupDrawAnnotation.ItemLinks.Add(this.barButtonItemMemo);
            this.ribbonPageGroupDrawAnnotation.Name = "ribbonPageGroupDrawAnnotation";
            this.ribbonPageGroupDrawAnnotation.Text = "Draw";
            // 
            // ribbonPageGroupClearAnnotations
            // 
            this.ribbonPageGroupClearAnnotations.ItemLinks.Add(this.barButtonItemClearAnnotations);
            this.ribbonPageGroupClearAnnotations.Name = "ribbonPageGroupClearAnnotations";
            this.ribbonPageGroupClearAnnotations.Text = "Clear";
            // 
            // ribbonPageTools
            // 
            this.ribbonPageTools.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupTools,
            this.ribbonPageGroupOrthoMode,
            this.ribbonPageGroupOsnap});
            this.ribbonPageTools.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("ribbonPageTools.ImageOptions.SvgImage")));
            this.ribbonPageTools.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.ribbonPageTools.Name = "ribbonPageTools";
            this.ribbonPageTools.Text = "Tools";
            // 
            // ribbonPageGroupTools
            // 
            this.ribbonPageGroupTools.ItemLinks.Add(this.barButtonItemLayer);
            this.ribbonPageGroupTools.ItemLinks.Add(this.barButtonItemTextStyle);
            this.ribbonPageGroupTools.ItemLinks.Add(this.barButtonItemLineType);
            this.ribbonPageGroupTools.Name = "ribbonPageGroupTools";
            this.ribbonPageGroupTools.Text = "Settings";
            // 
            // ribbonPageGroupOrthoMode
            // 
            this.ribbonPageGroupOrthoMode.ItemLinks.Add(this.barButtonItemOrthoMode);
            this.ribbonPageGroupOrthoMode.Name = "ribbonPageGroupOrthoMode";
            this.ribbonPageGroupOrthoMode.Text = "Mode";
            // 
            // ribbonPageGroupOsnap
            // 
            this.ribbonPageGroupOsnap.ItemLinks.Add(this.barButtonItemOsnapend);
            this.ribbonPageGroupOsnap.ItemLinks.Add(this.barButtonItemOsnapIntersection);
            this.ribbonPageGroupOsnap.ItemLinks.Add(this.barButtonItemOsnapMiddle);
            this.ribbonPageGroupOsnap.ItemLinks.Add(this.barButtonItemOsnapCenter);
            this.ribbonPageGroupOsnap.ItemLinks.Add(this.barButtonItemOsnapPoint);
            this.ribbonPageGroupOsnap.Name = "ribbonPageGroupOsnap";
            this.ribbonPageGroupOsnap.Text = "Osnap";
            // 
            // ribbonPageOptions
            // 
            this.ribbonPageOptions.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroupViewport,
            this.ribbonPageGroup1,
            this.ribbonPageGroupOptions});
            this.ribbonPageOptions.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("ribbonPageOptions.ImageOptions.SvgImage")));
            this.ribbonPageOptions.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.ribbonPageOptions.Name = "ribbonPageOptions";
            this.ribbonPageOptions.Text = "Options";
            // 
            // ribbonPageGroupViewport
            // 
            this.ribbonPageGroupViewport.ImageOptions.Image = global::Br3D.Properties.Resources.view_1x2;
            this.ribbonPageGroupViewport.ItemLinks.Add(this.barButtonItemSingleView);
            this.ribbonPageGroupViewport.ItemLinks.Add(this.barButtonItem1x1View);
            this.ribbonPageGroupViewport.ItemLinks.Add(this.barButtonItem1x2View);
            this.ribbonPageGroupViewport.ItemLinks.Add(this.barButtonItem2x2View);
            this.ribbonPageGroupViewport.Name = "ribbonPageGroupViewport";
            this.ribbonPageGroupViewport.Text = "Viewport";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItemShowGrid);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItemShowToolbar);
            this.ribbonPageGroup1.ItemLinks.Add(this.barButtonItemShowSymbol);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // ribbonPageGroupOptions
            // 
            this.ribbonPageGroupOptions.ItemLinks.Add(this.barButtonItemLanguage);
            this.ribbonPageGroupOptions.ItemLinks.Add(this.barButtonItemCheckForUpdate);
            this.ribbonPageGroupOptions.ItemLinks.Add(this.barButtonItemHomepage);
            this.ribbonPageGroupOptions.ItemLinks.Add(this.barButtonItemOptions);
            this.ribbonPageGroupOptions.ItemLinks.Add(this.barButtonItemAbout);
            this.ribbonPageGroupOptions.Name = "ribbonPageGroupOptions";
            this.ribbonPageGroupOptions.Text = "Options";
            // 
            // repositoryItemRibbonSearchEdit1
            // 
            this.repositoryItemRibbonSearchEdit1.AutoHeight = false;
            this.repositoryItemRibbonSearchEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemRibbonSearchEdit1.Name = "repositoryItemRibbonSearchEdit1";
            // 
            // repositoryItemButtonEdit1
            // 
            this.repositoryItemButtonEdit1.AutoHeight = false;
            this.repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // repositoryItemCheckEdit1
            // 
            this.repositoryItemCheckEdit1.AutoHeight = false;
            this.repositoryItemCheckEdit1.Name = "repositoryItemCheckEdit1";
            // 
            // ribbonStatusBar1
            // 
            this.ribbonStatusBar1.ItemLinks.Add(this.ribbonControl1.SearchEditItem);
            this.ribbonStatusBar1.ItemLinks.Add(this.barStaticItemCoordinates);
            this.ribbonStatusBar1.ItemLinks.Add(this.barButtonItemEndPoint);
            this.ribbonStatusBar1.ItemLinks.Add(this.barButtonItemIntPoint);
            this.ribbonStatusBar1.ItemLinks.Add(this.barButtonItemMidPoint);
            this.ribbonStatusBar1.ItemLinks.Add(this.barButtonItemCenterPoint);
            this.ribbonStatusBar1.ItemLinks.Add(this.barButtonItemNodePoint);
            this.ribbonStatusBar1.Location = new System.Drawing.Point(0, 645);
            this.ribbonStatusBar1.Name = "ribbonStatusBar1";
            this.ribbonStatusBar1.Ribbon = this.ribbonControl1;
            this.ribbonStatusBar1.Size = new System.Drawing.Size(1239, 27);
            // 
            // barButtonItem1
            // 
            this.barButtonItem1.Caption = "Vertical";
            this.barButtonItem1.Id = 21;
            this.barButtonItem1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItem1.ImageOptions.SvgImage")));
            this.barButtonItem1.Name = "barButtonItem1";
            // 
            // barButtonItemDrawSphere
            // 
            this.barButtonItemDrawSphere.Caption = "Sphere";
            this.barButtonItemDrawSphere.Id = 76;
            this.barButtonItemDrawSphere.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("barButtonItemDrawSphere.ImageOptions.SvgImage")));
            this.barButtonItemDrawSphere.Name = "barButtonItemDrawSphere";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1239, 672);
            this.Controls.Add(this.hModel);
            this.Controls.Add(this.dockPanelDynamicInput);
            this.Controls.Add(this.hideContainerLeft);
            this.Controls.Add(this.ribbonStatusBar1);
            this.Controls.Add(this.ribbonControl1);
            this.IconOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("FormMain.IconOptions.SvgImage")));
            this.Name = "FormMain";
            this.Text = "Br3D";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            ((System.ComponentModel.ISupportInitialize)(this.hModel)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dockManager1)).EndInit();
            this.hideContainerLeft.ResumeLayout(false);
            this.panelContainer1.ResumeLayout(false);
            this.dockPanelProperties.ResumeLayout(false);
            this.dockPanel1_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.propertyGridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxTextStyle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxLayerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxLineType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemComboBoxBlock)).EndInit();
            this.dockPanelObjectTree.ResumeLayout(false);
            this.dockPanel3_Container.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.treeListObject)).EndInit();
            this.dockPanelDynamicInput.ResumeLayout(false);
            this.controlContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuDrawArc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenuLanguage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemRibbonSearchEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemButtonEdit1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemCheckEdit1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private hanee.ThreeD.HModel hModel;
        private DevExpress.XtraBars.Docking.DockManager dockManager1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelObjectTree;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel3_Container;
        private DevExpress.XtraTreeList.TreeList treeListObject;
        private DevExpress.Utils.ToolTipController toolTipController1;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelProperties;
        private DevExpress.XtraBars.Docking.ControlContainer dockPanel1_Container;
        private DevExpress.XtraVerticalGrid.PropertyGridControl propertyGridControl1;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryGeneral;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowColor;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryText;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryLineType;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxTextStyle;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowVisible;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowColorMethod;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowBoxMin;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowBoxMax;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowGroupIndex;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLineTypeName;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLineTypeScale;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLineWeight;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLineWeightMethod;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowTextString;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowStyleName;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowHeight;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowBillboard;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowWidthFactor;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowInsertionPoint;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowBackward;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowUpsideDown;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowAlignment;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxLayerName;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowLayerName;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxLineType;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox repositoryItemComboBoxBlock;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryBlock;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowBlockName;
        private DevExpress.XtraBars.Docking.DockPanel panelContainer1;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowEntityType;
        private DevExpress.XtraVerticalGrid.Rows.CategoryRow categoryCircle;
        private DevExpress.XtraVerticalGrid.Rows.EditorRow rowRadius;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem endPointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem intersectionPointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem middlePointToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem centerPointToolStripMenuItem;
        private DevExpress.XtraBars.Navigation.TileNavSubItem tileNavSubItemInsertImage;
        private DevExpress.XtraBars.Docking.DockPanel dockPanelDynamicInput;
        private DevExpress.XtraBars.Docking.ControlContainer controlContainer1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageHome;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupFile;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageDraw;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageDimension;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupDim;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageEdit;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupEdit;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageAnnotation;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupDrawAnnotation;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageTools;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupTools;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageOptions;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupOptions;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDrawCircle;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDrawArc;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDrawPolyline;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDrawSpline;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar1;
        private DevExpress.XtraBars.BarStaticItem barStaticItem1;
        private DevExpress.XtraBars.BarStaticItem barStaticItemCoordinates;
        private DevExpress.XtraBars.Ribbon.Internal.RepositoryItemRibbonSearchEdit repositoryItemRibbonSearchEdit1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupDraw;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDrawLine;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDrawText;
        private DevExpress.XtraBars.BarButtonItem barButtonItemInsert;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDrawCylinder;
        private DevExpress.XtraBars.PopupMenu popupMenuDrawArc;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDrawArc_FirstSecondThird;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDrawArc_CenterStartEnd;
        private DevExpress.XtraBars.BarButtonItem barButtonItemOpen;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSaveAs;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSaveImage;
        private DevExpress.XtraBars.BarButtonItem barButtonItemExit;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupSystem;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDimHorizontal;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDimVertical;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDimAlign;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDimDiameter;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDimRadius;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDimLeader;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemErase;
        private DevExpress.XtraBars.BarButtonItem barButtonItemMove;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCopy;
        private DevExpress.XtraBars.BarButtonItem barButtonItemScale;
        private DevExpress.XtraBars.BarButtonItem barButtonItemRotate;
        private DevExpress.XtraBars.BarButtonItem barButtonItemOffset;
        private DevExpress.XtraBars.BarButtonItem barButtonItemMirror;
        private DevExpress.XtraBars.BarButtonItem barButtonItemExplode;
        private DevExpress.XtraBars.BarButtonItem barButtonItemTrim;
        private DevExpress.XtraBars.BarButtonItem barButtonItemFillet;
        private DevExpress.XtraBars.BarButtonItem barButtonItemChamfer;
        private DevExpress.XtraBars.BarButtonItem barButtonItemLanguage;
        private DevExpress.XtraBars.PopupMenu popupMenuLanguage;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCheckForUpdate;
        private DevExpress.XtraBars.BarButtonItem barButtonItemHomepage;
        private DevExpress.XtraBars.BarButtonItem barButtonItemAbout;
        private DevExpress.XtraBars.BarButtonItem barButtonItemLayer;
        private DevExpress.XtraBars.BarButtonItem barButtonItemTextStyle;
        private DevExpress.XtraBars.BarButtonItem barButtonItemLineType;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCoordinates;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDistance;
        private DevExpress.XtraBars.BarButtonItem barButtonItemMemo;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupClearAnnotations;
        private DevExpress.XtraBars.BarButtonItem barButtonItemClearAnnotations;
        private DevExpress.XtraBars.BarButtonItem barButtonItemSingleView;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1x1View;
        private DevExpress.XtraBars.BarButtonItem barButtonItem1x2View;
        private DevExpress.XtraBars.BarButtonItem barButtonItem2x2View;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupOrthoMode;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupViewport;
        private DevExpress.XtraBars.BarButtonItem barButtonItemOrthoMode;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupOsnap;
        private DevExpress.XtraBars.BarButtonItem barButtonItemOsnapend;
        private DevExpress.XtraBars.BarButtonItem barButtonItemOsnapIntersection;
        private DevExpress.XtraBars.BarButtonItem barButtonItemOsnapMiddle;
        private DevExpress.XtraBars.BarButtonItem barButtonItemOsnapCenter;
        private DevExpress.XtraBars.BarButtonItem barButtonItemOsnapPoint;
        private DevExpress.XtraBars.BarButtonItem barButtonItemLanguageKorean;
        private DevExpress.XtraBars.BarButtonItem barButtonItemLanguageEnglish;
        private DevExpress.XtraBars.BarButtonItem barButtonItemMultilineText;
        private DevExpress.XtraBars.BarButtonItem barButtonItemInsertImage;
        private DevExpress.XtraBars.BarButtonItem barButtonItemShowGrid;
        private DevExpress.XtraBars.BarButtonItem barButtonItemShowToolbar;
        private DevExpress.XtraBars.BarButtonItem barButtonItemShowSymbol;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemOptions;
        private System.Windows.Forms.ToolStripMenuItem selectallToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unselectAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem invertSelectionToolStripMenuItem;
        private DevExpress.XtraBars.BarButtonItem barButtonItemWorkspace;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroupWorkspace;
        private DevExpress.XtraBars.BarButtonItem barButtonItemEndPoint;
        private DevExpress.XtraBars.BarButtonItem barButtonItemIntPoint;
        private DevExpress.XtraBars.BarButtonItem barButtonItemMidPoint;
        private DevExpress.XtraBars.BarButtonItem barButtonItemCenterPoint;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckEdit repositoryItemCheckEdit1;
        private DevExpress.XtraBars.BarButtonItem barButtonItemNodePoint;
        private hanee.ThreeD.ControlCommandBar controlCommandBar1;
        private ControlAds controlAds1;
        private System.Windows.Forms.ToolStripMenuItem transparencyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTransparency0;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTransparency50;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemTransparency100;
        private DevExpress.XtraBars.Docking.AutoHideContainer hideContainerLeft;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDrawBox;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPageDraw3D;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDrawCone;
        private DevExpress.XtraBars.BarButtonItem barButtonItemDrawSphere;
    }
}