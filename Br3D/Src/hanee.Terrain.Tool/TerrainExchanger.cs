using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.Terrain.Exchange;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace hanee.Terrain.Tool
{
    static public class TerrainExchanger
    {
        // landxml 파일로 terrain을 만든다.
        static public Mesh FromLandXML(string fileName)
        {
            XmlSerializer xml = new XmlSerializer(typeof(hanee.Terrain.Exchange.LandXML));
            using (var fileStream = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                using (var xmlReader = new XmlTextReader(fileStream))
                {
                    var landXml = xml.Deserialize(xmlReader) as hanee.Terrain.Exchange.LandXML;
                    if (landXml == null)
                        return null;

                    if (landXml.Surfaces != null && landXml.Surfaces.Surface != null)
                    {
                        var surface = landXml.Surfaces.Surface;
                        if (surface.Definition != null && surface.Definition.SurfType == "TIN" &&
                            surface.Definition.Faces != null && surface.Definition.Faces.F != null &&
                            surface.Definition.Pnts != null && surface.Definition.Pnts.P != null)
                        {

                            List<Point3D> vertices = new List<Point3D>();
                            List<IndexTriangle> triangles = new List<IndexTriangle>();

                            foreach (var f in surface.Definition.Faces.F)
                            {
                                var tri = f.ToPoint3DBySpace();
                                triangles.Add(new IndexTriangle((int)tri.X - 1, (int)tri.Y - 1, (int)tri.Z - 1));
                            }


                            foreach (var p in surface.Definition.Pnts.P)
                            {
                                vertices.Add(p.Text.ToPoint3DBySpace());
                            }


                            return new Mesh(vertices, triangles);
                        }
                    }
                }
            }

            return null;
        }

        // Terrain을 land xml 파일로 내보내기 한다.
        internal static bool ToLandXML(Mesh mesh, string fileName)
        {
            if (mesh == null)
                return false;

            var landXml = new LandXML();
            landXml.Surfaces = new Surfaces();
            landXml.Surfaces.Surface = new Exchange.Surface();
            landXml.Surfaces.Surface.Definition = new Definition();

            var definition = landXml.Surfaces.Surface.Definition;
            definition.SurfType = "TIN";
            definition.Faces = new Faces();
            definition.Faces.F = new List<string>();
            foreach (var tri in mesh.Triangles)
                definition.Faces.F.Add($"{tri.V1 + 1} {tri.V2 + 1} {tri.V3 + 1}");

            definition.Pnts = new Pnts();
            definition.Pnts.P = new List<P>();

            var id = 1;
            foreach (var v in mesh.Vertices)
            {
                var p = new P();
                p.Id = id.ToString();
                p.Text = v.ToStringWithSpace();
                definition.Pnts.P.Add(p);
            }

            // 파일로 저장
            XmlSerializer xml = new XmlSerializer(typeof(LandXML));
            using (FileStream fileStream = File.Open(fileName, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                xml.Serialize(fileStream, landXml);
                return true;
            }

            return false;
        }
    }
}
