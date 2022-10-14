using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
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
            byEntity
        }

        Method method = Method.byLayer;

        public ActionCreateTerrain(Environment environment) : base(environment)
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
                var ek = await GetEntityOrKey(LanguageHelper.Tr("Select layer(A:All layers, E:By entities)"), -1, false, null, new KeyEventArgs(Keys.E), new KeyEventArgs(Keys.A));
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
                else
                {
                    if (!layerNames.ContainsKey(ek.Key.LayerName))
                        layerNames.Add(ek.Key.LayerName, true);
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

                var mesh = MakeMesh(selectedEntities) as Mesh;
                if (mesh != null)
                {
                    entityPropertiesManager?.SetDefaultProperties(mesh, false);
                    environment.Entities.Add(mesh);
                }
            }





            EndAction();
            return true;
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
                allPoints.Add( new PointRGB(p.X, p.Y, p.Z, color));
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
