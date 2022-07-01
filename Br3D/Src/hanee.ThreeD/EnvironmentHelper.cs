using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using System.Collections.Generic;
using System.Drawing;
using Environment = devDept.Eyeshot.Environment;
namespace hanee.ThreeD
{
    // Model, Drawings 모두를 위한 헬퍼
    public static class EnvironmentHelper
    {
        // workspace를 켠다
        static public void StartWorkspace(this devDept.Eyeshot.Environment environment, Plane plane)
        {
            var ws = environment.GetWorkspace();
            if (ws == null)
                return;
            ws.plane = plane;
            ws.enabled = true;
        }

        // workspace를 끈다.
        static public void EndWorkspace(this devDept.Eyeshot.Environment environment)
        {
            var ws = environment.GetWorkspace();
            if (ws == null)
                return;

            ws.enabled = false;
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
    }
}
