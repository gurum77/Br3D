using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using devDept.Graphics;
using hanee.Geometry;
using System.Collections.Generic;
using System.Drawing;
using Environment = devDept.Eyeshot.Environment;
namespace hanee.ThreeD
{
    // Model, Drawings 모두를 위한 헬퍼
    public static class EnvironmentHelper
    {
        // world 좌표를 work space에 투영한다.
        static public Point3D ProjectOnWorkspace(this devDept.Eyeshot.Environment environment, Point3D pt)
        {
            var ws = environment.GetWorkspace();
            if (ws == null || !ws.enabled)
                return pt;

            var pt2D = ws.plane.Project(pt);
            return ws.plane.PointAt(pt2D);
        }

        // 마우스 좌표를 world좌표로 변환(workspace 고려함)
        static public Point3D ScreenToWorldWithWorkspace(this devDept.Eyeshot.Environment environment, System.Drawing.Point location)
        {
            Point3D point3D = environment.ScreenToWorld(location);

            // workplane이 활성화 되어있으면 workplane에서 좌표를 찾는다.
            var ws = environment.GetWorkspace();
            if (ws != null && ws.enabled)
                environment.ScreenToPlane(location, ws.plane, out point3D);

            return point3D;
        }

        // workspace symbol을 리턴한다.
        static public Grid GetWorkspaceSymbol(this devDept.Eyeshot.Environment environment)
        {
            var model = environment as Model;
            if (model == null)
                return null;
            if (model.ActiveViewport.Grids.Length < 2)
                return null;


            return model.ActiveViewport.Grids[1];
            //return model.ActiveViewport.OriginSymbols.Length > 1 ? model.ActiveViewport.OriginSymbols[1] : null;
        }

        // workspace를 시작한다.
        static public void StartWorkspace(this devDept.Eyeshot.Environment environment, Point3D pt1, Point3D pt2, Point3D pt3)
        {
            var plane = new Plane();
            plane.CreateFromPoints(pt1, pt2, pt3);
            plane.Normalize();

            environment.StartWorkspace(plane);
        }



        // workspace를 켠다
        static public void StartWorkspace(this devDept.Eyeshot.Environment environment, Plane plane)
        {
            var ws = environment.GetWorkspace();
            if (ws == null)
                return;
            ws.plane = plane;
            ws.enabled = true;


            var sym = environment.GetWorkspaceSymbol();
            if (sym == null)
                return;
            sym.Plane = plane;
            sym.Visible = true;


            //sym.Edit(Color.FromArgb(100, Color.Red));
            //sym.Transformation = new Transformation(ws.plane.Origin, ws.plane.AxisX, ws.plane.AxisY, ws.plane.AxisZ);
            //sym.Visible = true;
            environment.Invalidate();
        }

        // workspace를 끈다.
        static public void EndWorkspace(this devDept.Eyeshot.Environment environment)
        {
            var ws = environment.GetWorkspace();
            if (ws == null)
                return;

            ws.enabled = false;

            var sym = environment.GetWorkspaceSymbol();
            if (sym == null)
                return;

            sym.Visible = false;

            environment.Invalidate();

        }

        static public Plane GetWorkplane(this devDept.Eyeshot.Environment environment)
        {
            var ws = environment.GetWorkspace();
            if (ws == null || !ws.enabled)
                return Plane.XY;

            return ws.plane;
        }

        static public Workspace GetWorkspace(this devDept.Eyeshot.Environment environment)
        {
            HModel hModel = environment as HModel;
            if (hModel == null)
                return null;

            return hModel.workSpace;
        }

        static public bool IsTopViewOnly(this devDept.Eyeshot.Environment environment)
        {
            HModel hDesign = environment as HModel;
            if (hDesign == null)
                return false;

            return hDesign.TopViewOnly;
        }

        // text를 모두 regen한다.
        static public void RegenAllTexts(this Environment environment, double deviation = 0.001)
        {
            var regenParams = new RegenParams(deviation, environment);
            foreach (var ent in environment.Entities)
            {
                var text = ent as devDept.Eyeshot.Entities.Text;
                if (text == null)
                    continue;

                text.Regen(regenParams);
            }
        }

        static public void RunPaintBackBuffer(this Environment environment)
        {
            //if (environment is BRDrawings)
            //{
            //    BRDrawings drawings = environment as BRDrawings;

            //    drawings.RunPaintBackBuffer();
            //}
            //else if (environment is BRModel)
            //{
            //    BRModel model = environment as BRModel;
            //    model.RunPaintBackBuffer();
            //}
        }
        static public void DrawPreviewEntity(this Environment environment, bool drawWire = true)
        {
            Entity[] previewEntities = drawWire ? ActionBase.PreviewEntities : ActionBase.PreviewFaceEntities;
            if (previewEntities == null)
                return;

            environment.renderContext.SetLineSize(1);

            // color
            environment.renderContext.SetColorWireframe(Color.Black);
            environment.renderContext.EnableXOR(false);

            foreach (var ent in previewEntities)
                environment.DrawPreviewEntity(ent, drawWire);


            environment.renderContext.EnableXOR(true);
        }

        // vertices를 screen 2d point로 변환한다.
        static public Point2D[] GetScreenVertices(this Environment environment, IList<Point3D> vertices)
        {
            if (vertices == null || vertices.Count == 0)
                return null;

            Point2D[] screenPts = new Point2D[vertices.Count];

            for (int i = 0; i < vertices.Count; i++)
            {
                screenPts[i] = environment.WorldToScreen(vertices[i]);
            }
            return screenPts;
        }

        // 미리보기 mesh 를 그린다.
        static public void DrawPreviewMesh(this Environment environment, Mesh mesh, bool drawWire)
        {
            if (mesh == null)
                return;

            if (mesh.Vertices == null)
                return;
            if (mesh.Vertices.Length == 0)
                return;

            Point2D[] screenPts = environment.GetScreenVertices(mesh.Vertices);
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

                environment.renderContext.DrawTriangles2D(triangles.ToArray());
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
                    environment.renderContext.DrawLineStrip(triPts);
                }
            }


        }

        // 미리보기 solid3d를 그린다.
        static public void DrawPreviewSolid3D(this Environment environment, Brep solid3D)
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
                    environment.DrawPreviewLinearPathByVertices(vertices);
                }
                else if (edge.Curve is Circle)
                {
                    Circle circle = (Circle)edge.Curve;
                    environment.DrawPreviewLinearPathByVertices(circle.GetPointsByLengthPerSegment(5));
                }
            }

        }

        // vertices로 미리보기 line path를 그린다.
        static public void DrawPreviewLinearPathByVertices(this Environment environment, Point3D[] vertices)
        {
            Point2D[] screenPts = environment.GetScreenVertices(vertices);
            if (screenPts == null)
                return;
            if (screenPts.Length == 0)
                return;

            environment.renderContext.DrawLineStrip(screenPts);
        }


        // 미리보기 객체를 그린다.
        static public void DrawPreviewEntity(this Environment environment, Entity ent, bool drawWire = true)
        {
            if (ent is Solid)
            {
                try
                {
                    environment.DrawPreviewMesh(((Solid)ent).ConvertToMesh(), drawWire);
                }
                catch
                {

                }

            }
            else if (ent is Brep)
            {
                Brep solid3D = (Brep)ent;
                environment.DrawPreviewSolid3D(solid3D);
            }
            else if (ent is Mesh)
            {
                environment.DrawPreviewMesh(((Mesh)ent), drawWire);
            }
            else if (ent is LinearDim)
            {
                LinearDim dim = (LinearDim)ent;


                Point3D extLineEnd1 = dim.GetExtLineEnd(true);
                Point3D extLineEnd2 = dim.GetExtLineEnd(false);


                Point3D[] points = new Point3D[4];
                points[0] = dim.ExtLine1;
                points[1] = extLineEnd1;
                points[2] = extLineEnd2;
                points[3] = dim.ExtLine2;

                environment.DrawPreviewLinearPathByVertices(points);
            }
            else if (ent is AngularDim)
            {
                AngularDim dim = (AngularDim)ent;
                try
                {
                    dim.Regen(new RegenParams(0.001, environment));
                    Arc arc = dim.UnderlyingArc;
                    Point3D[] points = arc.GetPointsByLengthPerSegment(arc.Length() / 10);
                    environment.DrawPreviewLinearPathByVertices(points);

                    points = new Point3D[] { arc.StartPoint, dim.ExtLine1 };
                    environment.DrawPreviewLinearPathByVertices(points);

                    points = new Point3D[] { arc.EndPoint, dim.ExtLine2 };
                    environment.DrawPreviewLinearPathByVertices(points);

                }
                catch
                {

                }
            }
            else if (ent is DiametricDim)
            {
                DiametricDim dim = (DiametricDim)ent;
                try
                {
                    Vector3D dir = Vector3D.Subtract(dim.DimLinePosition, dim.InsertionPoint);
                    dir.Normalize();


                    Point3D[] points = new Point3D[] { dim.InsertionPoint + dir * -dim.Radius, dim.DimLinePosition };
                    environment.DrawPreviewLinearPathByVertices(points);
                }
                catch
                {

                }
            }
            else if (ent is RadialDim)
            {
                RadialDim dim = (RadialDim)ent;
                try
                {
                    Point3D[] points = new Point3D[] { dim.InsertionPoint, dim.DimLinePosition };
                    environment.DrawPreviewLinearPathByVertices(points);
                }
                catch
                {

                }
            }
            else if (ent is LinearPath)
            {
                LinearPath path = (LinearPath)ent;
                if (path.GlobalWidth == 0)
                    environment.DrawPreviewLinearPathByVertices(path.Vertices);
                else
                    environment.DrawPreviewLinearPathByVerticesAndWidthWithColor(path.Vertices, (float)path.GlobalWidth, path.Color);
            }
            else if (ent is Line)
            {
                Line line = (Line)ent;
                if (line.LineWeight == 0)
                    environment.DrawPreviewLinearPathByVertices(line.Vertices);
                else
                    environment.DrawPreviewLinearPathByVerticesAndWidthWithColor(line.Vertices, (float)line.LineWeight, line.Color);
            }
            else if (ent is BlockReference)
            {
                BlockReference blockRef = (BlockReference)ent;
                Transformation trans = blockRef.Transformation;
                Transformation invertTrans = trans.Clone() as Transformation;
                invertTrans.Invert();

                Block block = null;
                environment.Blocks.TryGetValue(blockRef.BlockName, out block);
                if (block != null)
                {
                    foreach (var child in block.Entities)
                    {
                        child.TransformBy(trans);
                        environment.DrawPreviewEntity(child, drawWire);
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
                    environment.DrawCurve(curve);
                }
            }
        }

        // curve를 미리보기로 그린다.
        static public void DrawCurve(this Environment environment, ICurve curve)
        {
            if (curve is Entity)
            {
                environment.DrawPreviewEntity((Entity)curve);
            }
        }

        // vertices로 미리보기 line path를 그린다.
        static public void DrawPreviewLinearPathByVerticesAndWidthWithColor(this Environment environment, Point3D[] vertices, float width, Color color)
        {
            Point2D[] screenPts = environment.GetScreenVertices(vertices);
            if (screenPts == null)
                return;
            if (screenPts.Length == 0)
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
                colors[i] = color;

            float[] widths = new float[lines.Length / 2];
            for (int i = 0; i < widths.Length; ++i)
                widths[i] = width;

            environment.renderContext.DrawLines(lines, colors, widths);
        }

        static public void SetLayerColorByBackgroundColor(this Environment env)
        {
            // 배경색이 어두우면 layer의 검은색을 흰색으로 변경
            if (env.IsDarkBackground())
            {
                foreach (var la in env.Layers)
                {
                    if (la.Color == Color.Black)
                        la.Color = Color.White;
                }
            }
            // 배경색이 밝으면 layer의 흰색을 검은색으로 변경
            else
            {
                foreach (var la in env.Layers)
                {
                    if (la.Color == Color.White)
                        la.Color = Color.Black;
                }

            }
        }


        // 배경색이 어두운지?
        static public bool IsDarkBackground(this Environment environment)
        {
            if (environment is Model model)
                return model.ActiveViewport.Background.IsDark;
            return false;
        }

        // 2D view로 설정한다.
        static public void Set2DViewStyle(this Environment env)
        {
            if (env is Model model)
            {
                model.ActiveViewport.Camera.ProjectionMode = projectionType.Orthographic;
                model.SetView(viewType.Top, true, true);

                // 배경을 검은색으로
                model.ActiveViewport.Background.BottomColor = Options.Instance.backgroundColor2D.colorValue;
                model.ActiveViewport.Background.TopColor = Options.Instance.backgroundColor2D.colorValue;
                model.ActiveViewport.DisplayMode = displayType.Flat;

                // layer color을 background에 따라 변경(검은색을 흰색으로 또는 흰색을 검은색으로)
                model.SetLayerColorByBackgroundColor();
            }
        }

        // 3D view로 설정한다.
        static public void Set3DViewStyle(this Environment env)
        {
            if (env is Model model)
            {
                model.Visible = true;

                model.ActiveViewport.Camera.ProjectionMode = projectionType.Perspective;
                model.SetView(viewType.Isometric, true, true);

                // 배경을 검은색으로
                model.ActiveViewport.Background.BottomColor = Options.Instance.backgroundColorBottom.colorValue;
                model.ActiveViewport.Background.TopColor = Options.Instance.backgroundColorTop.colorValue;
                model.ActiveViewport.DisplayMode = displayType.Rendered;

                // layer color을 background에 따라 변경(검은색을 흰색으로 또는 흰색을 검은색으로)
                model.SetLayerColorByBackgroundColor();
            }
        }
    }
}
