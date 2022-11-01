using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Terrain.Tool
{
    public class ActionCreateTerrain : ActionBase
    {
        enum Method
        {
            byLayer,
            byEntity,
            byGrid
        }

        Method method = Method.byLayer;
        FormCreateGrid form;
        public ActionCreateTerrain(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            method = Method.byLayer;
            var layerNames = new Dictionary<string, bool>();
            var entities = new Dictionary<Entity, bool>();
            while (true)
            {
                var ek = await GetEntityOrKey(LanguageHelper.Tr("Select layer(A:All layers, E:By entities, G:By grid)"), -1, false, null,
                    new KeyEventArgs(Keys.E), new KeyEventArgs(Keys.A), new KeyEventArgs(Keys.G));
                if (IsCanceled() || IsEntered())
                    break;

                if (ek.Value != null && ek.Value.KeyCode == Keys.A)
                {
                    layerNames.Clear();
                    foreach (var la in environment.Layers)
                    {
                        if (layerNames.ContainsKey(la.Name))
                            continue;

                        layerNames.Add(la.Name, true);
                    }
                    break;
                }
                else if (ek.Value != null && ek.Value.KeyCode == Keys.E)
                {
                    method = Method.byEntity;
                    var curEntities = await GetEntities(LanguageHelper.Tr("Select entities"));
                    if (curEntities == null)
                    {
                        Entered = true;
                        break;
                    }


                    foreach (var ce in curEntities)
                    {
                        if (entities.ContainsKey(ce))
                            continue;

                        entities.Add(ce, true);
                    }

                    if (IsCanceled() || IsEntered())
                        break;
                }
                else if (ek.Value != null && ek.Value.KeyCode == Keys.G)
                {
                    method = Method.byGrid;
                    form = new FormCreateGrid();
                    if (form.ShowDialog() == DialogResult.Cancel)
                    {
                        form = null;
                        Canceled = true;
                    }
                    break;

                }
                else
                {
                    if (!layerNames.ContainsKey(ek.Key.LayerName))
                    {
                        var layerName = ek.Key.LayerName;
                        layerNames.Add(layerName, true);

                        ActionBase.subCursorText.Add(layerName);
                    }
                }
            }


            if (!IsCanceled())
            {
                var selectedEntities = new List<Entity>();

                if (method == Method.byLayer)
                {
                    foreach (var ent in environment.Entities)
                    {
                        if (!layerNames.ContainsKey(ent.LayerName))
                            continue;
                        selectedEntities.Add(ent);
                    }
                }
                else if (method == Method.byEntity)
                {
                    foreach (var ent in entities)
                    {
                        selectedEntities.Add(ent.Key);
                    }
                }


                var mesh = method == Method.byGrid ? MakeGrid(form) : MakeMesh(selectedEntities);
                if (mesh != null)
                {
                    entityPropertiesManager?.SetDefaultProperties(mesh, false);
                    environment.Entities.Add(mesh);
                }
            }





            EndAction();
            return true;
        }

        private Mesh MakeGrid(FormCreateGrid form)
        {
            if (form == null)
                return null;

            return MakeGrid(form.textEditX.Text.ToDouble(), form.textEditY.Text.ToDouble(), form.textEditResolution.Text.ToDouble(), form.textEditXSize.Text.ToInt(), form.textEditYSize.Text.ToInt());
        }

        // 그리드 생성
        private Mesh MakeGrid(double x, double y, double resolution, int xSize, int ySize)
        {
            if (resolution <= 0.001)
                return null;
            if (xSize < 1 || ySize < 1)
                return null;

            var vertices = new Point3D[xSize * ySize];
            var idx = 0;
            for (int row = 0; row < ySize; ++row)
            {
                for (int col = 0; col < xSize; ++col)
                {
                    double curX = x + col * resolution;
                    double curY = y + row * resolution;
                    double curZ = 0;
                    vertices[idx++] = new Point3D(curX, curY, curZ);
                }
            }

            var triangles = new SmoothTriangle[(ySize - 1) * (xSize - 1) * 2];
            idx = 0;
            for (int row = 0; row < ySize - 1; ++row)
            {
                for (int col = 0; col < xSize - 1; ++col)
                {
                    var v1 = col + row * xSize;
                    var v2 = col + row * xSize + 1;
                    var v3 = col + (row + 1) * xSize + 1;
                    triangles[idx++]    = new SmoothTriangle(v1, v2, v3);

                    v1 = col + row * xSize;
                    v2 = col + (row + 1) * xSize + 1;
                    v3 = col + (row + 1) * xSize;
                    triangles[idx++] = new SmoothTriangle(v1, v2, v3);
                }
            }

            var mesh = new Mesh(vertices, triangles);
            return mesh;
        }

        // ent에서 triangle의 source 데이타를 리턴한다.
        bool GetTriangleSource(Entity ent, ref List<Point3D> points, ref List<Segment3D> segments)
        {
            if (ent == null)
                return false;

            if (ent is Point p)
            {
                points.Add(p.Position);
                return true;
            }
            else if (ent is Circle circle)
            {
                points.Add(circle.Center);
                return true;
            }
            else if (ent is Arc arc)
            {
                points.Add(arc.Center);
                points.Add(arc.StartPoint);
                points.Add(arc.EndPoint);
                return true;
            }
            else if (ent is Line l)
            {
                var seg = new Segment3D(l.StartPoint, l.EndPoint);
                segments.Add(seg);

                return true;
            }
            else if (ent is ICurve curve)
            {
                var curves = curve.GetIndividualCurves();
                if (curves.Length == 0)
                    return false;

                if (curves.Length == 1)
                {
                    var seg = new Segment3D(curves[0].StartPoint, curves[0].EndPoint);
                    segments.Add(seg);
                    return true;
                }

                foreach (var c in curves)
                {
                    GetTriangleSource(c as Entity, ref points, ref segments);
                }

                return true;
            }
            else if (ent is Region region)
            {
                foreach (var c in region.ContourList)
                {
                    GetTriangleSource(c as Entity, ref points, ref segments);
                }
                return true;
            }

            return false;
        }
        private Mesh MakeMesh(List<Entity> entities)
        {
            var points = new List<Point3D>();
            var segments = new List<Segment3D>();

            foreach (var ent in entities)
            {
                GetTriangleSource(ent, ref points, ref segments);
            }


            var color = Options.Instance.currentColor;
            var allPoints = new List<Point3D>();
            foreach (var p in points)
                allPoints.Add(new PointRGB(p.X, p.Y, p.Z, color));
            foreach (var seg in segments)
            {
                allPoints.Add(new PointRGB(seg.P0.X, seg.P0.Y, seg.P0.Z, color));
                allPoints.Add(new PointRGB(seg.P1.X, seg.P1.Y, seg.P1.Z, color));
            }

            
            return UtilityEx.Triangulate(allPoints);
            //return UtilityEx.Triangulate(allPoints, Mesh.natureType.MulticolorSmooth);
        }
    }
}
