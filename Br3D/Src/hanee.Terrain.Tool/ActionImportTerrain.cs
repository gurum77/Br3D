using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace hanee.Terrain.Tool
{
    public class ActionImportTerrain : ActionBase
    {
        public ActionImportTerrain(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            while (true)
            {
                var k = await GetKey(LanguageHelper.Tr("X:LandXML 1.2"));
                if (IsEntered() || IsCanceled())
                    break;

                if (k.KeyCode == Keys.X)
                {
                    OpenFileDialog dlg = new OpenFileDialog();
                    dlg.Filter = "LandXML 1.2|*.xml";
                    dlg.DefaultExt = "xml";
                    if (dlg.ShowDialog() == DialogResult.OK)
                    {
                        XmlSerializer xml = new XmlSerializer(typeof(hanee.Terrain.Exchange.LandXML));
                        using (FileStream fileStream = File.Open(dlg.FileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                        {
                            using (XmlTextReader xmlReader = new XmlTextReader(fileStream))
                            {
                                var landXml = xml.Deserialize(xmlReader) as hanee.Terrain.Exchange.LandXML;
                                if(landXml != null)
                                {
                                    if(landXml.Surfaces != null && landXml.Surfaces.Surface != null)
                                    {
                                        

                                        var surface = landXml.Surfaces.Surface;
                                        if(surface.Definition != null && surface.Definition.SurfType == "TIN" && 
                                            surface.Definition.Faces != null && surface.Definition.Faces.F != null &&
                                            surface.Definition.Pnts != null && surface.Definition.Pnts.P != null)
                                        {

                                            List<Point3D> vertices = new List<Point3D>();
                                            List<IndexTriangle> triangles = new List<IndexTriangle>();

                                            foreach (var f in surface.Definition.Faces.F)
                                            {
                                                var tri = f.ToPoint3DBySpace();
                                                triangles.Add(new IndexTriangle((int)tri.X-1, (int)tri.Y-1, (int)tri.Z-1));
                                            }

                                            
                                            foreach (var p in surface.Definition.Pnts.P)
                                            {
                                                vertices.Add(p.Text.ToPoint3DBySpace());
                                            }


                                            var mesh = new Mesh(vertices, triangles);
                                            environment.Entities.Add(mesh);
                                            environment.Entities.Regen(null);
                                            environment.Invalidate();

                                        }
                                        
                                    }
                                }
                            }
                        }
                    }

                }

            }


            EndAction();
            return true;
        }
    }
}
