using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Translators;
using devDept.Geometry;
using devDept.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    [Serializable]
    public partial class HModel : devDept.Eyeshot.Model
    {
        // 투명도 옵션
        public enum TranparencyMode
        {
            untransparency, // 불투명(기본값)
            selectedOnly,   // 선택객체만 투명
            unselectedOnly  // 비선택객체만 투명
        };

        public Color drawingColor
        {
            get
            {
                if (this.IsDarkBackground())
                    return Color.White;
                return Color.Black;
            }
        }
        public static Font drawingFont = new Font("Tahoma", 10.0f, FontStyle.Bold);
        public System.Drawing.Point cursorPoint;
        System.Drawing.Point lastMouseDownPoint { get; set; }
        public bool displayHelp = true;
        public bool TopViewOnly = false;
        public Snapping Snapping = null;
        public OrthoModeManager orthoModeManager = null;
        public GripManager gripManager = null;
        public SelectionManager selectionManager = null;
        public EntityPropertiesManager entityPropertiesManager = null;
        public Workspace workSpace = null;

        // property grid를 지정하면 객체 선택시 property grid에 속성이 표시됨
        public PropertyGridHelper propertyGridHelper { get; set; }


        // 투명도 옵션 설정
        public TranparencyMode Transparency
        { get; set; }

        // 열려있는 모든 stream들..
        static protected List<Stream> opendStreams = new List<Stream>();

        // 투명도 모드를 적용하기 위한 객체의 기본 색상을 보관
        Dictionary<Entity, EntityColor> dicColorOld = new Dictionary<Entity, EntityColor>();

        public HModel()
        {
            DefaultColor = System.Drawing.Color.White;
            foreach (var la in Layers)
            {
                la.Color = DefaultColor;
            }
            Snapping = new Snapping(this);
            orthoModeManager = new OrthoModeManager(this);
            gripManager = new GripManager(this);
            selectionManager = new SelectionManager(this);
            entityPropertiesManager = new EntityPropertiesManager(this);
            workSpace = new Workspace();
            CursorTypes[devDept.Eyeshot.cursorType.Default] = Cursors.Cross;
        }


        #region event
        // draw overlay 함수 마지막에 호출
        public event EventHandler<DrawSceneParams> AfterDrawOverlay;
        #endregion

      
        public void KeyEventListener(Keys keyData)
        {
            var e = new KeyEventArgs(keyData);

            // 액션 사용자 입력중인 경우
            if (ActionBase.IsUserInputting())
            {
                ActionBase.KeyUpHandler(e);

                if (DynamicInputManager.enabled)
                {
                    if (keyData == Keys.Oem3)
                    {
                        DynamicInputManager.FlagPoint3DType();
                    }
                    else
                    {
                        DynamicInputManager.OnKeyUp(e);
                    }
                }
            }
            else
            {
                if (DynamicInputManager.controlCommandBar != null && !DynamicInputManager.controlCommandBar.comboBoxEdit1.IsEditorActive)
                {
                    DynamicInputManager.controlCommandBar.comboBoxEdit1.Focus();
                    
                    return;
                }

                // esc 누르거나 delete로 객체가 삭제된 경우 grip point를 제거한다.
                if (e.KeyCode == Keys.Escape || e.KeyCode == Keys.Delete)
                {
                    // 입력중이 아니더라도 esc를 누르면 액션을 종료한다.
                    // 액션도중 예외상황 때문에 종료가 안된경우에 대한 예외처리
                    if (ActionBase.runningAction != null)
                    {
                        ActionBase.runningAction = null;
                    }
                    gripManager.ClearGripPoints();

                    if (e.KeyCode == Keys.Escape)
                    {
                        Entities.ClearSelection();
                        selectionManager.ClearSelection();
                    }

                    UpdatePropertyGridControl(null);
                    Invalidate();
                }

            }
        }

        bool IsMouseOnToolBar(MouseEventArgs e)
        {
            foreach (var toolbar in ActiveViewport.ToolBars)
            {
                // toolbar의 bounds
                var rect = toolbar.GetBounds(ActiveViewport);

                // mouse의 위치가 toolbar의 bounds안이면 그냥 리턴시킴
                if (rect.Left < e.Location.X && rect.Right > e.Location.X && rect.Top < e.Location.Y && rect.Bottom > e.Location.Y)
                    return true;
            }

            return false;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (IsMouseOnToolBar(e))
            {
                base.OnMouseDown(e);
                return;
            }

            lastMouseDownPoint = e.Location;

            base.OnMouseDown(e);

            // eyeshot액션이 동작중이면 리턴
            if (this.ActionMode != actionType.None &&
                this.ActionMode != actionType.SelectVisibleByPickDynamic)
                return;

            if (e.Button == MouseButtons.Left)
            {
                // selection 
                SelectionManager.Step step = selectionManager.step;
                var selectedEntities = selectionManager.OnMouseDown(e);

                // 그립
                gripManager.OnMouseDown(e, selectedEntities, step);
            }


            // 
            if (ActionBase.IsUserInputting())
            {
                ActionBase.MouseDownHandler(this, e);
            }
        }

        /// <summary>
        /// 선택되어 있는 객체로 property grid를 갱신한다.
        /// </summary>
        private void UpdatePropertyGridControl(Object obj)
        {
            if (propertyGridHelper == null)
                return;

            propertyGridHelper.UpdatePropertyGridControl(obj);
        }

        // 최소 사양의 viewport layout을 만든다
        [Obsolete]
        public static HModel CreateMinimumViewportLayout()
        {
            HModel viewportLayout = new HModel();
            viewportLayout.CreateControl();

            ((System.ComponentModel.ISupportInitialize)(viewportLayout)).BeginInit();

            #region viewport 초기화
            devDept.Eyeshot.Grid grid1 = new devDept.Eyeshot.Grid(new devDept.Geometry.Point3D(-50D, -50D, 0D),
                new devDept.Geometry.Point3D(100D, 100D, 0D),
                10D,
                new devDept.Geometry.Plane(new devDept.Geometry.Point3D(0D, 0D, 0D),
                new devDept.Geometry.Vector3D(0D, 0D, 1D)),
                System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128))))),
                System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32))))),
                System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32))))),
                true, true, false, true, 10, 100, 10, System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90))))), System.Drawing.Color.Empty, false);

            devDept.Eyeshot.HomeToolBarButton homeToolBarButton1 = new devDept.Eyeshot.HomeToolBarButton("Home", devDept.Eyeshot.ToolBarButton.styleType.PushButton, true, true);
            devDept.Graphics.BackgroundSettings backgroundSettings1 = new devDept.Graphics.BackgroundSettings(devDept.Graphics.backgroundStyleType.Solid, System.Drawing.Color.WhiteSmoke, System.Drawing.Color.White, System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(237)))), ((int)(((byte)(237))))), 0.75D, null);
            devDept.Eyeshot.Camera camera1 = new devDept.Eyeshot.Camera(new devDept.Geometry.Point3D(0D, 0D, 0D), 617.10100716628347D, new devDept.Geometry.Quaternion(0.12940952255126034D, 0.22414386804201339D, 0.4829629131445341D, 0.83651630373780794D), devDept.Graphics.projectionType.Perspective, 50D, 2.5185184487794166D, false);
            devDept.Eyeshot.ToolBar toolBar1 = new devDept.Eyeshot.ToolBar(devDept.Eyeshot.ToolBar.positionType.HorizontalTopCenter, true, new devDept.Eyeshot.ToolBarButton[] {
            ((devDept.Eyeshot.ToolBarButton)(homeToolBarButton1))});
            devDept.Eyeshot.Legend legend1 = new devDept.Eyeshot.Legend(0D, 100D, "Title", "Subtitle", new System.Drawing.Point(24, 24), new System.Drawing.Size(10, 30), true, false, false, "{0:0.##}", System.Drawing.Color.Transparent, System.Drawing.Color.Black, System.Drawing.Color.Black, new System.Drawing.Color[] {
            System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(63)))), ((int)(((byte)(255))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(127)))), ((int)(((byte)(255))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(191)))), ((int)(((byte)(255))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(191))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(127))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(63))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(0))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(255)))), ((int)(((byte)(0))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(255)))), ((int)(((byte)(0))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(255)))), ((int)(((byte)(0))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(0))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(191)))), ((int)(((byte)(0))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(127)))), ((int)(((byte)(0))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(63)))), ((int)(((byte)(0))))),
            System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))))}, false);
            devDept.Eyeshot.RotateSettings rotateSettings1 = new devDept.Eyeshot.RotateSettings(
                            new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle,
                            devDept.Eyeshot.modifierKeys.Ctrl),
                            10D, true, 1D,
                            devDept.Eyeshot.rotationType.Turntable,
                            devDept.Eyeshot.rotationCenterType.CursorLocation,
                            new devDept.Geometry.Point3D(0D, 0D, 0D), false);
            devDept.Eyeshot.OriginSymbol originSymbol1 = new devDept.Eyeshot.OriginSymbol(10, devDept.Eyeshot.originSymbolStyleType.Ball, System.Drawing.Color.Black, System.Drawing.Color.Red, System.Drawing.Color.Green, System.Drawing.Color.Blue, "Origin", "X", "Y", "Z", true, false);
            devDept.Eyeshot.ViewCubeIcon viewCubeIcon1 = new devDept.Eyeshot.ViewCubeIcon(devDept.Eyeshot.coordinateSystemPositionType.TopRight, true, System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(69)))), ((int)(((byte)(0))))), true, "FRONT", "BACK", "LEFT", "RIGHT", "TOP", "BOTTOM", System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(77)))), ((int)(((byte)(77)))), ((int)(((byte)(77))))), 'S', 'N', 'W', 'E', true, System.Drawing.Color.White, System.Drawing.Color.Black, 120, true, true, null, null, null, null, null, null, false);
            devDept.Eyeshot.ZoomSettings zoomSettings1 = new devDept.Eyeshot.ZoomSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.Shift), 25, true, devDept.Eyeshot.zoomStyleType.AtCursorLocation, false, 1D, System.Drawing.Color.DeepSkyBlue, devDept.Eyeshot.Camera.perspectiveFitType.Accurate, false, 10);
            devDept.Eyeshot.PanSettings panSettings1 = new devDept.Eyeshot.PanSettings(new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Middle, devDept.Eyeshot.modifierKeys.None), 25, true);
            devDept.Eyeshot.NavigationSettings navigationSettings1 = new devDept.Eyeshot.NavigationSettings(devDept.Eyeshot.Camera.navigationType.Examine, new devDept.Eyeshot.MouseButton(devDept.Eyeshot.mouseButtonsZPR.Left, devDept.Eyeshot.modifierKeys.None), new devDept.Geometry.Point3D(-1000D, -1000D, -1000D), new devDept.Geometry.Point3D(1000D, 1000D, 1000D), 8D, 50D, 50D);
            devDept.Eyeshot.Viewport.SavedViewsManager savedViewsManager1 = new devDept.Eyeshot.Viewport.SavedViewsManager(8);
            devDept.Eyeshot.CoordinateSystemIcon coordinateSystemIcon1 = new devDept.Eyeshot.CoordinateSystemIcon(System.Drawing.Color.Black, System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80))))), System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(80)))), ((int)(((byte)(80))))), System.Drawing.Color.OrangeRed, "Origin", "X", "Y", "Z", true, devDept.Eyeshot.coordinateSystemPositionType.BottomLeft, 37, false);

            devDept.Eyeshot.Viewport viewport1 = new devDept.Eyeshot.Viewport(
              new System.Drawing.Point(0, 0), new System.Drawing.Size(731, 510),
              backgroundSettings1, camera1, new devDept.Eyeshot.ToolBar[] { toolBar1 }, new devDept.Eyeshot.Legend[] { legend1 }, devDept.Eyeshot.displayType.Rendered, true, false, false, false, new devDept.Eyeshot.Grid[] {
            grid1}, originSymbol1, false, rotateSettings1, zoomSettings1, panSettings1, navigationSettings1, coordinateSystemIcon1, viewCubeIcon1, savedViewsManager1, devDept.Eyeshot.viewType.Trimetric);
            #endregion


            #region viewportlayout  초기화
            devDept.Eyeshot.DisplayModeSettingsRendered displayModeSettingsRendered1 = new devDept.Eyeshot.DisplayModeSettingsRendered(true, devDept.Eyeshot.edgeColorMethodType.EntityColor, System.Drawing.Color.Black, 1F, 2F, devDept.Eyeshot.silhouettesDrawingType.LastFrame, false, devDept.Graphics.shadowType.Realistic, null, true, true, 0.3F, devDept.Graphics.realisticShadowQualityType.High);

            viewportLayout.AllowDrop = true;
            viewportLayout.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            viewportLayout.Cursor = System.Windows.Forms.Cursors.Default;
            viewportLayout.Location = new System.Drawing.Point(12, 12);
            viewportLayout.MinimumSize = new System.Drawing.Size(8, 8);
            viewportLayout.Name = "viewportLayout1";
            viewportLayout.Rendered = displayModeSettingsRendered1;
            viewportLayout.Size = new System.Drawing.Size(400, 400);
            viewportLayout.TabIndex = 0;
            viewportLayout.Viewports.Add(viewport1);
            #endregion

            ((System.ComponentModel.ISupportInitialize)(viewportLayout)).EndInit();


            // flat에서도 material이 보이도록 
            viewportLayout.Flat.ColorMethod = flatColorMethodType.EntityMaterial;


            // 안티알리아싱 최소로 적용
            viewportLayout.AntiAliasing = true;
            viewportLayout.AntiAliasingSamples = antialiasingSamplesNumberType.x2;
            viewportLayout.AskForAntiAliasing = true;

            return viewportLayout;
        }
        // 객체를 투명하게 한다.
        public void TransparentEntity(Entity ent, bool transparent)
        {
            EntityColor color = new EntityColor();
            EntityColor oldColor = new EntityColor();
            // 원래 색상이 없으면 일단 보관한다.
            if (!dicColorOld.TryGetValue(ent, out oldColor))
            {
                EntityColor newColor = new EntityColor();
                newColor.color = Color.FromArgb(ent.Color.A, ent.Color.R, ent.Color.G, ent.Color.B);
                newColor.colorMethod = ent.ColorMethod;

                dicColorOld.Add(ent, newColor);
                oldColor = newColor;
            }

            if (transparent)
            {
                // 객체의 색이 원래 layer를 다르면 layer의 색을 준다.
                Color entityColor = oldColor.color;
                if (oldColor.colorMethod == colorMethodType.byLayer)
                {
                    Layer layer;
                    if (Layers.TryGetValue(ent.LayerName, out layer) && layer != null)
                    {
                        entityColor = layer.Color;
                    }
                }


                color.color = Color.FromArgb(50, entityColor);
                color.colorMethod = colorMethodType.byEntity;
            }
            else
            {
                color = oldColor;
            }

            ent.Color = color.color;
            ent.ColorMethod = color.colorMethod;
        }

        // 객체 투명도를 업데이트 한다.
        public void UpdateTransparency()
        {
            foreach (var ent in Entities)
            {
                if ((ent.Selected && Transparency == TranparencyMode.selectedOnly) ||
                   (ent.Selected == false && Transparency == TranparencyMode.unselectedOnly))
                {
                    TransparentEntity(ent, true);
                }
                else
                {
                    TransparentEntity(ent, false);
                }
            }

            Invalidate();
        }

        public void SaveBackgroundColor()
        {
            Options.Instance.backgroundColorTop.colorValue = ActiveViewport.Background.TopColor;
            Options.Instance.backgroundColorBottom.colorValue = ActiveViewport.Background.BottomColor;
        }

        // 2D view로 설정한다.
        public void Set2DView()
        {
            TopViewOnly = true;
            this.Set2DViewStyle();
        }

        // 3D view로 설정한다.
        public void Set3DView()
        {
            TopViewOnly = false;
            this.Set3DViewStyle();
        }


        /// <summary>
        /// fromModel에서 환경을 복사해서 채운다.
        /// </summary>
        /// <param name="from"></param>
        public void FillEnvironment(devDept.Eyeshot.Model fromModel)
        {
            // line type
            foreach (var lt in fromModel.LineTypes)
            {
                LineTypes.AddOrReplace(lt.Clone() as LineType);
                //TODO devDept: LinePattern has been renamed to LineType.
            }

            // text style
            foreach (var ts in fromModel.TextStyles)
            {
                TextStyles.AddOrReplace(ts.Clone() as TextStyle);
            }

            // text style
            foreach (var ma in fromModel.Materials)
            {
                Materials.AddOrReplace(ma.Clone() as Material);
            }

            // layer
            foreach (var la in fromModel.Layers)
            {
                Layers.AddOrReplace(la.Clone() as Layer);
            }

            // block
            foreach (var b in fromModel.Blocks)
            {
                if (b.Name == "RootBlock")
                    continue;

                Blocks.AddOrReplace(b.Clone() as Block);
            }
        }



        #region 액션중 미리 보기
        // 마우스 옆에 따라 다니는 text를 그린다.
        public void DrawMouseText(string text, System.Drawing.Point location)
        {
            renderContext.EnableXOR(false);

            DrawText(location.X, (int)Size.Height - location.Y + 10,
                 text, drawingFont, drawingColor, ContentAlignment.BottomLeft);



            renderContext.EnableXOR(true);
        }


        // vertices를 screen 2d point로 변환한다.
        public Point2D[] GetScreenVertices(IList<Point3D> vertices)
        {
            if (vertices == null || vertices.Count == 0)
                return null;

            List<Point2D> screenPts = new List<Point2D>();
            

            for (int i = 0; i < vertices.Count; i++)
            {
                if (vertices[i] == null)
                    continue;

                screenPts.Add(WorldToScreen(vertices[i]));
            }
            return screenPts.ToArray();
        }

        // 길이를 screen 2d 길이로 변환
        // 3D뷰에서는 원근에 의해서 기준 좌표마다 길이가 다르게 계산되므로 basePoint가 있어야 함.
        public float GetScreenLength(Point3D basePoint, float len)
        {
            if (basePoint == null)
                return len;

            Point2D pt1 = WorldToScreen(basePoint);
            Point2D pt2 = WorldToScreen(basePoint + new Point3D(len, 0, 0));
            var dist1 = pt1.DistanceTo(pt2);

            pt1 = WorldToScreen(basePoint);
            pt2 = WorldToScreen(basePoint + new Point3D(0, len, 0));
            var dist2 = pt1.DistanceTo(pt2);

            pt1 = WorldToScreen(basePoint);
            pt2 = WorldToScreen(basePoint + new Point3D(0, 0, len));
            var dist3 = pt1.DistanceTo(pt2);

            var dist = Math.Max(dist1, dist2);
            dist = Math.Max(dist, dist3);
            return (float)dist;
        }

        public System.Drawing.Point GetMouseLocationFromWorldPoint(Point3D pt)
        {
            var points = GetMouseLocationsFromWorldPoints(new List<Point3D>() { pt });
            return points != null && points.Length > 0 ? points[0] : new System.Drawing.Point(0, 0);
        }

        // vertices를 마우스 위치로 변환한다.
        public System.Drawing.Point[] GetMouseLocationsFromWorldPoints(List<Point3D> vertices)
        {
            Point2D[] points = GetScreenVertices(vertices);
            System.Drawing.Rectangle rc = ClientRectangle;

            List<System.Drawing.Point> mouseLocations = new List<System.Drawing.Point>();
            foreach (var point in points)
            {
                System.Drawing.Point mouseLocation = new System.Drawing.Point((int)point.X, (int)point.Y);
                mouseLocation.Y = rc.Height - mouseLocation.Y;
                mouseLocations.Add(mouseLocation);
            }

            return mouseLocations.ToArray();
        }

        // curve를 미리보기로 그린다.
        public void DrawCurve(ICurve curve)
        {
            if (curve is Entity)
            {
                DrawPreviewEntity((Entity)curve);
            }
        }

        // vertices로 미리보기 line path를 그린다.
        public void DrawPreviewLinearPathByVerticesAndWidth(Point3D[] vertices, float width)
        {
            Point2D[] screenPts = GetScreenVertices(vertices);
            if (screenPts == null)
                return;
            if (screenPts.Length == 0 || screenPts.Length != vertices.Length)
                return;

            Point3D[] lines = new Point3D[(vertices.Length - 1) * 2];
            int idx = 0;
            for (int i = 1; i < vertices.Length; ++i)
            {
                lines[idx++] = screenPts[i - 1].Clone() as Point3D;
                lines[idx++] = screenPts[i].Clone() as Point3D;
            }
            Color[] colors = new Color[lines.Length];
            for (int i = 0; i < colors.Length; ++i)
                colors[i] = drawingColor;

            float[] widths = new float[lines.Length / 2];
            for (int i = 0; i < widths.Length; ++i)
                widths[i] = width;

            renderContext.DrawLines(lines, colors, widths);
        }

        // vertices로 미리보기 line path를 그린다.
        public void DrawPreviewLinearPathByVertices(Point3D[] vertices)
        {
            Point2D[] screenPts = GetScreenVertices(vertices);
            if (screenPts == null)
                return;
            if (screenPts.Length == 0)
                return;

            renderContext.DrawLineStrip(screenPts);
        }

        // 미리보기 solid3d를 그린다.
        public void DrawPreviewSolid3D(Brep solid3D)
        {
            if (solid3D == null)
                return;

            foreach (var edge in solid3D.Edges)
            {
                if (edge.Curve is Line)
                {
                    Line line = (Line)edge.Curve;
                    Point3D[] vertices = new Point3D[2];
                    vertices[0] = line.StartPoint;
                    vertices[1] = line.EndPoint;
                    DrawPreviewLinearPathByVertices(vertices);
                }
                else if (edge.Curve is Circle)
                {
                    Circle circle = (Circle)edge.Curve;
                    DrawPreviewLinearPathByVertices(circle.GetPointsByLengthPerSegment(5));
                }
            }

        }
        // 미리보기 mesh 를 그린다.
        public void DrawPreviewMesh(Mesh mesh, bool drawWire)
        {
            if (mesh == null)
                return;

            if (mesh.Vertices == null)
                return;
            if (mesh.Vertices.Length == 0)
                return;

            Point2D[] screenPts = GetScreenVertices(mesh.Vertices);
            if (screenPts == null)
                return;
            if (screenPts.Length == 0)
                return;

            if (!drawWire)
            {
                List<Point2D> triangles = new List<Point2D>();
                foreach (var tri in mesh.Triangles)
                {
                    triangles.Add(screenPts[tri.V1]);
                    triangles.Add(screenPts[tri.V2]);
                    triangles.Add(screenPts[tri.V3]);
                }

                renderContext.DrawTriangles2D(triangles.ToArray());
                //mesh.DrawFace(renderContext, null);
            }
            else
            {
                foreach (var tri in mesh.Triangles)
                {
                    Point2D[] triPts = new Point2D[3];
                    triPts[0] = screenPts[tri.V1];
                    triPts[1] = screenPts[tri.V2];
                    triPts[2] = screenPts[tri.V3];
                    renderContext.DrawLineStrip(triPts);
                }
            }


        }


        // 미리보기 객체를 그린다.
        void DrawPreviewEntity(Entity ent)
        {
            if (ent is devDept.Eyeshot.Entities.Region)
            {
                devDept.Eyeshot.Entities.Region r = ent as devDept.Eyeshot.Entities.Region;
                if (r != null)
                {
                    Mesh mesh = r.ConvertToMesh();
                    if (mesh != null)
                    {
                        DrawPreviewMesh(mesh, false);
                    }
                }


            }
            else if (ent is Solid)
            {
                try
                {
                    DrawPreviewMesh(((Solid)ent).ConvertToMesh(), false);
                }
                catch
                {

                }

            }
            else if (ent is Brep)
            {
                Brep solid3D = (Brep)ent;
                DrawPreviewSolid3D(solid3D);
            }
            else if (ent is Mesh)
            {
                DrawPreviewMesh(((Mesh)ent), false);
            }
            else if (ent is LinearDim)
            {
                //LinearDim dim = (LinearDim)ent;


                //// 치수 보조선의 방향과 text로의 방향이 같은지?
                //Vector2D v1 = new Vector2D(dim.ExtLine1, dim.ExtLine2);
                //Vector2D v2 = new Vector2D(dim.ExtLine1, dim.DimLinePosition);

                //double sign = Math.Sign(Vector2D.SignedAngleBetween(v1, v2));

                //double height = BREntity.Util.CalcDistPointToPointByAxis(dim.ExtLine1, dim.DimLinePosition, dim.Plane.AxisY) * sign;
                //Point3D[] points = new Point3D[4];
                //points[0] = dim.ExtLine1;
                //points[1] = dim.ExtLine1 + dim.Plane.AxisY * height;
                //points[2] = dim.ExtLine2 + dim.Plane.AxisY * height;
                //points[3] = dim.ExtLine2;

                //DrawPreviewLinearPathByVertices(points);

                //// text

                //ActionBase.cursorText = points[0].DistanceTo(points[3]).ToString("0.000");

            }
            else if (ent is LinearPath)
            {
                LinearPath path = (LinearPath)ent;
                if (path.GlobalWidth == 0 && path.LineWeight == 0)
                    DrawPreviewLinearPathByVertices(path.Vertices);
                else if (path.LineWeight != 0)
                    DrawPreviewLinearPathByVerticesAndWidth(path.Vertices, (float)path.LineWeight);
                else
                    DrawPreviewLinearPathByVerticesAndWidth(path.Vertices, (float)path.GlobalWidth);
            }
            else if (ent is Line)
            {
                Line line = (Line)ent;
                if (line.LineWeight != 0)
                    DrawPreviewLinearPathByVerticesAndWidth(line.Vertices, (float)line.LineWeight);
                else
                    DrawPreviewLinearPathByVerticesAndWidth(line.Vertices, 1.0f);
            }
            else if (ent is Circle)
            {
                Circle circle = (Circle)ent;

                var vertices = circle.GetPointsByLength(circle.Length() / 10.0);
                if (circle.LineWeight != 0)
                    DrawPreviewLinearPathByVerticesAndWidth(vertices, (float)circle.LineWeight);
                else
                    DrawPreviewLinearPathByVerticesAndWidth(vertices, 1.0f);
            }
            else if (ent is BlockReference)
            {
                BlockReference blockRef = (BlockReference)ent;
                Transformation trans = blockRef.Transformation;
                Transformation invertTrans = trans.Clone() as Transformation;
                invertTrans.Invert();

                Block block = null;
                Blocks.TryGetValue(blockRef.BlockName, out block);
                if (block != null)
                {
                    foreach (var child in block.Entities)
                    {
                        child.TransformBy(trans);
                        DrawPreviewEntity(child);
                        child.TransformBy(invertTrans);
                    }
                }

            }
            else if (ent is CompositeCurve)
            {
                CompositeCurve comCurve = (CompositeCurve)ent;
                var curves = comCurve.GetIndividualCurves();
                foreach (var curve in curves)
                {
                    DrawCurve(curve);
                }
            }

        }


        protected void DrawPreviewEntity()
        {
            Entity[] previewEntities = ActionBase.PreviewEntities;
            if (previewEntities == null)
                return;

            renderContext.SetLineSize(1);

            // color
            renderContext.SetColorWireframe(drawingColor/* Color.Black*/);
            renderContext.EnableXOR(false);

            foreach (var ent in previewEntities)
                DrawPreviewEntity(ent);


            renderContext.EnableXOR(true);
        }
        //protected override int[] GetVisibleEntitiesFromBackBuffer(Viewport viewport, byte[] rgbValues, int stride, int bpp, Rectangle selectionBox, bool firstOnly)
        //{
        //    var indices = base.GetVisibleEntitiesFromBackBuffer(viewport, rgbValues, stride, bpp, selectionBox, firstOnly);

        //    return indices;
        //}

        protected override void OnMouseMove(System.Windows.Forms.MouseEventArgs e)
        {
            cursorPoint = e.Location;

            ActionBase.MouseMoveHandler(this, e);

            gripManager.MouseMove(e);
            selectionManager.OnMouseMove(e);

            // If ObjectSnap is ON, we need to find closest vertex (if any)
            if (ActionBase.IsNeedSnapping())
            {
                Snapping.OnMouseMoveForSnap(e);
            }

            // draw overlay가 호출된다.(back buffer에 그림)
            PaintBackBuffer();

            // back buffer를 화면에 표시
            SwapBuffers();

            // 2D view일 때는 좌우로만 회전하도록 한다.
            if (TopViewOnly && e.Button == MouseButtons.Middle && Control.ModifierKeys == Keys.Control)
            {
                System.Windows.Forms.MouseEventArgs eNew = new MouseEventArgs(e.Button, e.Clicks, e.X, lastMouseDownPoint.Y, e.Delta);
                base.OnMouseMove(eNew);
            }
            else
            {
                base.OnMouseMove(e);
            }
        }

        // navigation 도움말 출력
        private void RenderNavigationHelp()
        {
            if (displayHelp == false)
                return;

            // 도움말 스트링
            List<string> helpStrings = new List<string>();

            if (Viewports[0].Navigation.Mode != Camera.navigationType.Examine)
            {
                helpStrings.Add("First person camera behavior");
                helpStrings.Add("  Press W and S to move forwards and backwards");
                helpStrings.Add("  Press A and D to strafe left and right");
                helpStrings.Add("  Press E and Q to move up and down");
                helpStrings.Add("  Move mouse to free look");

                helpStrings.Add("");
                helpStrings.Add("Press M to enable/disable mouse smoothing");
                helpStrings.Add("Press + and - to change camera rotation speed");
                helpStrings.Add("Press , and . to change mouse sensitivity");
                helpStrings.Add("Press ESC to exit");
                helpStrings.Add("");
                helpStrings.Add("Press H to hide help");
            }

            RenderHelpStrings(helpStrings, Color.Black);

        }

        // help string을 화면에 표시한다.
        public void RenderHelpStrings(List<string> helpString, Color color)
        {
            int posY = Height - 2 * Font.Height;

            for (int i = 0; i < helpString.Count; i++)
            {
                DrawText(10, posY, helpString[i], Font, color, ContentAlignment.BottomLeft);
                posY -= (int)1.5 * Font.Height;
            }
        }

        public static string FilterForSaveDialog()
        {
            Dictionary<string, string> supportFormats = new Dictionary<string, string>
            {
                { "All", "*.*" },
                { "AutoCAD", "*.dwg; *.dxf" },
                { "IGES", "*.igs; *.iges" },
                { "STEP", "*.stp; *.step" },
                { "Stereolithography", ".stl" },
                { "WaveFront OBJ", "*.obj" },
                { "WebGL", "*.html" },
                { "Points", "*.asc" },
                { "XML", "*.xml" },
                { "PRC", "*.prc" }
            };


            return HModel.FilterBySupportFormats(supportFormats);

        }
        public static string FilterForOpenDialog()
        {
            Dictionary<string, string> supportFormats = new Dictionary<string, string>
            {
                { "All", "*.*" },
                { "AutoCAD", "*.dwg; *.dxf" },
                { "IFC", "*.ifc" },
                { "3DS", ".dwg; *.dxf" },
                { "JT", "*.jt" },
                { "STEP", "*.stp; *.step" },
                { "IGES", "*.igs; *.iges" },
                { "WaveFront OBJ", "*.obj" },
                { "Stereolithography", "*.stl" },
                { "Laser LAS", "*.las" },
                { "Points", "*.asc" },
                { "LUSAS", "*.las" },
                { "EMF", "*.emf" }
            };

            return HModel.FilterBySupportFormats(supportFormats);
        }

        public static string FilterBySupportFormats(Dictionary<string, string> supportFormats)
        {
            // all formats 추가
            {
                StringBuilder sbAllFilter = new StringBuilder();
                // all filter
                foreach (var format in supportFormats)
                {
                    if (format.Key == "All")
                        continue;

                    sbAllFilter.Append($";{format.Value}");
                }

                supportFormats["All"] = sbAllFilter.ToString().Remove(0, 1);
            }


            StringBuilder sb = new StringBuilder();
            foreach (var format in supportFormats)
            {
                sb.Append($"|{format.Key}({format.Value})|{format.Value}");
            }
            string filter = sb.ToString();
            filter = filter.Remove(0, 1);
            return filter;
        }


        protected override void DrawOverlay(HModel.DrawSceneParams myParams)
        {
            selectionManager.DrawOvery();

            if (ActionBase.IsUserInputting() == true)
            {
                // 사용자 입력중일때만 draw overlay
                //DrawOverlayForSnap();
                Snapping.DrawOverlayForSnap();
                if (orthoModeManager != null)
                    orthoModeManager.DrawOverlayForOrthoMode();

                renderContext.EnableXOR(true);

                renderContext.SetState(depthStencilStateType.DepthTestOff);

                if (ActionBase.cursorText != null)
                    DrawMouseText(ActionBase.cursorText, cursorPoint);



                // preview entity가 있다면 그걸 그린다.
                if (ActionBase.PreviewEntities != null)
                    DrawPreviewEntity();


                renderContext.EnableXOR(false);
            }


            RenderNavigationHelp();

            // drawoverlay
            if (AfterDrawOverlay != null)
            {
                AfterDrawOverlay(this, myParams);
            }

            base.DrawOverlay(myParams);
        }


        #endregion

        // ReadFileAsync에서 열었던 stream을 닫는다.
        static public void CloseReadFileAsyncStream()
        {
            foreach (var stream in opendStreams)
            {
                if (stream == null)
                    continue;
                stream.Close();
            }

            opendStreams.Clear();
        }

        static public void AddOpendStream(Stream stream)
        {
            if (stream == null)
                return;

            opendStreams.Add(stream);
        }

        // 파일이름을 받아서 ReadFileAsync를 리턴한다.
        public static ReadFileAsync GetReadFileAsync(string filename)
        {
            if (!System.IO.File.Exists(filename))
                return null;

            string ext = System.IO.Path.GetExtension(filename);
            ext = ext.ToUpper();




            devDept.Eyeshot.Translators.ReadFileAsync rf = null;
            if (ext == ".IGES" || ext == ".IGS")
            {
                rf = new devDept.Eyeshot.Translators.ReadIGES(filename);
            }
            else if (ext == ".STL")
            {
                rf = new devDept.Eyeshot.Translators.ReadSTL(filename);
            }
            else if (ext == ".STEP" || ext == ".STP")
            {
                rf = new devDept.Eyeshot.Translators.ReadSTEP(filename);
            }
            else if (ext == ".OBJ")
            {
                Stream stream, matStream;
                Dictionary<string, Stream> textureStreams;

                Get3DModelStreams(filename, out stream, out matStream, out textureStreams);
                rf = new devDept.Eyeshot.Translators.ReadOBJ(stream, matStream, textureStreams, Mesh.edgeStyleType.Free);
            }
            else if (ext == ".LAS")
            {
                rf = new devDept.Eyeshot.Translators.ReadLAS(filename);
            }
            else if (ext == ".DWG" || ext == ".DXF")
            {
                rf = new devDept.Eyeshot.Translators.ReadAutodesk(filename, null, false);
            }
            else if (ext == ".IFC" || ext == ".IFCZIP")
            {
                rf = new devDept.Eyeshot.Translators.ReadIFC(filename);
            }
            else if (ext == ".3DS")
            {
                rf = new devDept.Eyeshot.Translators.Read3DS(filename);
            }
            else if (ext == ".JT")
            {
                rf = new devDept.Eyeshot.Translators.ReadJT(filename);
            }
            else
            {
                rf = null;
            }

            return rf;
        }

        // 3d model data의 stream을 모두 가져온다.
        private static void Get3DModelStreams(string filename, out Stream stream, out Stream materialStream, out Dictionary<string, Stream> textureStreams)
        {
            stream = File.OpenRead(filename);
            AddOpendStream(stream);

            {
                string directory = System.IO.Path.GetDirectoryName(filename);
                string onlyFileName = System.IO.Path.GetFileNameWithoutExtension(filename);
                string matFileName = System.IO.Path.Combine(directory, onlyFileName + ".mtl");
                try
                {
                    materialStream = File.OpenRead(matFileName);
                    AddOpendStream(materialStream);
                }
                catch
                {
                    materialStream = null;
                }

                textureStreams = null;

                if (materialStream != null)
                {
                    textureStreams = new Dictionary<string, Stream>();

                    // 현재 directory와 파일명과 동일한 sub directory에서 이미지 파일을 모두 찾는다.
                    List<string> directories = new List<string>
                    {
                        directory,
                        System.IO.Path.Combine(directory, onlyFileName)
                    };
                    foreach (var directoryName in directories)
                    {
                        if (System.IO.Directory.Exists(directoryName))
                        {
                            string[] textureFiles = System.IO.Directory.GetFiles(directoryName);
                            foreach (var textureFile in textureFiles)
                            {
                                if (!HModel.IsImageFile(textureFile))
                                    continue;

                                Stream textureStream = File.OpenRead(textureFile);
                                if (textureStream == null)
                                    continue;

                                AddOpendStream(textureStream);
                                textureStreams.Add(System.IO.Path.GetFileName(textureFile), textureStream);
                            }
                        }
                    }
                }
            }
        }

        // 이미지 파일인지 확장자로 판단한다.
        static bool IsImageFile(string filename)
        {
            string ext = System.IO.Path.GetExtension(filename);
            ext = ext.ToUpper();
            if (ext == ".JPG" || ext == ".PNG" || ext == ".JPEG" || ext == ".BMP")
                return true;

            return false;
        }

        // 파일이름을 받아서 WriteFileAsync를 리턴한다.
        public WriteFileAsync GetWriteFileAsync(string filename, WriteParamsWithTextStyles writeParam = null, bool ascii = false)
        {
            return GetWriteFileAsync(this, filename, writeParam, ascii);
        }

        // 파일이름을 받아서 WriteFileAsync를 리턴한다.
        public static WriteFileAsync GetWriteFileAsync(HModel vp, string filename, WriteParamsWithTextStyles writeParam = null, bool ascii = false)
        {
            string ext = System.IO.Path.GetExtension(filename);
            ext = ext.ToUpper();


            if (writeParam == null)
            {
                writeParam = new WriteParamsWithTextStyles(vp.Entities, vp.Layers, vp.Blocks, vp.Materials, vp.TextStyles, vp.LineTypes, linearUnitsType.Meters);
            }


            WriteFileAsync wf = null;
            if (ext == ".IGES" || ext == ".IGS")
            {
                wf = new WriteIGES(writeParam, filename);
            }
            else if (ext == ".STL")
            {
                wf = new WriteSTL(writeParam, filename, ascii);
            }
            else if (ext == ".STEP" || ext == ".STP")
            {
                wf = new WriteSTEP(writeParam, filename);
            }
            else if (ext == ".OBJ")
            {
                wf = new WriteOBJ(writeParam, filename);
            }
            else if (ext == ".DWG")
            {

                WriteAutodeskParams aWriteParam = null;
                aWriteParam = new WriteAutodeskParams(vp.Entities, vp.Layers, vp.Blocks, vp.Materials, vp.TextStyles, vp.LineTypes);
                wf = new WriteAutodesk(aWriteParam, filename);
            }
            else if (ext == ".HTML")
            {
                WriteParamsWithMaterials writeParamTmp = new WriteParamsWithMaterials(vp);

                wf = new WriteWebGL(writeParamTmp, null, filename);
            }
            else if (ext == ".XML")
            {
                wf = new WriteXML(writeParam, filename);
            }
            else if (ext == ".PRC")
            {
#if WIN64
                WritePrcParams prcParam = new WritePrcParams(vp);
                wf = new WritePRC(prcParam, filename);
#endif
            }
            else
            {
                System.Diagnostics.Debug.Assert(false);
            }


            return wf;
        }


    }
}
