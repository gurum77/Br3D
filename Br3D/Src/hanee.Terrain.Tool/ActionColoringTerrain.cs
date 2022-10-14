using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Terrain.Tool
{
    public class ActionColoringTerrain : ActionBase
    {
        public ActionColoringTerrain(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();
            var legend = GetHModel().ActiveViewport.Legends.FirstOrDefault();
            if (legend == null)
            {
                EndAction();
                return true;
            }


            var selectableType = new Dictionary<Type, bool>();
            selectableType.Add(typeof(Mesh), true);
            var mesh = await GetEntity(LanguageHelper.Tr("Select mesh"), -1, false, selectableType) as Mesh;
            if(mesh != null)
            {
                if (mesh.MeshNature != Mesh.natureType.MulticolorPlain)
                {
                    environment.Entities.Remove(mesh);
                    mesh = mesh.ConvertToMesh(0.001, 0.001, Mesh.natureType.MulticolorPlain, false);
                    environment.Entities.Add(mesh);
                }

                
                if (environment.Entities.BoxMin == null || environment.Entities.BoxMin == null)
                {
                    environment.Entities.Regen(null);
                }
                legend.Position = new System.Drawing.Point(100, 5);
                legend.Max = environment.Entities.BoxMax.Z;
                legend.Min = environment.Entities.BoxMin.Z;
                legend.Visible = true;
                legend.Title = "EL.";
                legend.Subtitle = null;
                legend.TextColor = System.Drawing.Color.White;
                legend.TitleColor = legend.TextColor;

                foreach (var v in mesh.Vertices)
                {
                    var rgbPoint = v as PointRGB;
                    if (rgbPoint == null)
                        continue;
                    var color = legend.GetColorByValue(v.Z, true);
                    rgbPoint.R = color.R;
                    rgbPoint.G = color.G;
                    rgbPoint.B = color.B;
                }
                mesh.ColorMethod = colorMethodType.byEntity;
                mesh.Regen(0.001);
                environment.Entities.Regen(null);
                environment.Invalidate();
            }

            EndAction();
            return true;
        }
    }
}
