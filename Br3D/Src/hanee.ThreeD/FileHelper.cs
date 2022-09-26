using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Translators;
using devDept.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace hanee.ThreeD
{
    public class FileHelper
    {
        // 열려있는 모든 stream들..
        static protected List<Stream> opendStreams = new List<Stream>();

        static public string FilterForSaveDialog()
        {
            Dictionary<string, string> supportFormats = new Dictionary<string, string>();
            supportFormats.Add("All", "*.*");
            supportFormats.Add("Br3D", "*.br3");
            supportFormats.Add("AutoCAD", "*.dwg; *.dxf");
            supportFormats.Add("IGES", "*.igs; *.iges");
            supportFormats.Add("STEP", "*.stp; *.step");
            supportFormats.Add("Stereolithography", ".stl");
            supportFormats.Add("WaveFront OBJ", "*.obj");
            supportFormats.Add("WebGL", "*.html");
            supportFormats.Add("Points", "*.asc");
            supportFormats.Add("XML", "*.xml");
            supportFormats.Add("PRC", "*.prc");

            return FileHelper.FilterBySupportFormats(supportFormats);

        }

        // 이미지파일 filter
        // ".JPG" || ext == ".PNG" || ext == ".JPEG" || ext == ".BMP")
        static public string RasterImageFilterForOpenDialog()
        {
            Dictionary<string, string> supportFormats = new Dictionary<string, string>();
            supportFormats.Add("All", "*.*");
            supportFormats.Add("JPEG", "*.jpeg;*.jpg");
            supportFormats.Add("PNG", "*.png");
            supportFormats.Add("BMP", "*.bmp");
            return FileHelper.FilterBySupportFormats(supportFormats);
        }

        static public string FilterForOpenDialog(Dictionary<string, string> additionalSupportFormats = null)
        {
            Dictionary<string, string> supportFormats = new Dictionary<string, string>();
            supportFormats.Add("All", "*.*");
            supportFormats.Add("Br3D", "*.br3");
            supportFormats.Add("AutoCAD", "*.dwg; *.dxf");
            supportFormats.Add("IFC", "*.ifc");
            supportFormats.Add("3DS", "*.3ds");
            supportFormats.Add("GCode", "*.gcode");
            supportFormats.Add("JT", "*.jt");
            supportFormats.Add("STEP", "*.stp; *.step");
            supportFormats.Add("IGES", "*.igs; *.iges");
            supportFormats.Add("WaveFront OBJ", "*.obj");
            supportFormats.Add("Stereolithography", "*.stl");
            supportFormats.Add("Laser LAS", "*.las");
            supportFormats.Add("Points", "*.asc");
            supportFormats.Add("EMF", "*.emf");
            if (additionalSupportFormats != null)
            {
                foreach (var asf in additionalSupportFormats)
                {
                    if (supportFormats.ContainsKey(asf.Key))
                        continue;

                    supportFormats.Add(asf.Key, asf.Value);
                }
            }

            return FileHelper.FilterBySupportFormats(supportFormats);
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

        static public void AddOpendStream(Stream stream)
        {
            if (stream == null)
                return;

            opendStreams.Add(stream);
        }

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

        // 파일이름을 받아서 ReadFileAsync를 리턴한다.
        static public ReadFileAsync GetReadFileAsync(string filename)
        {
            if (!System.IO.File.Exists(filename))
                return null;

            string ext = System.IO.Path.GetExtension(filename);
            ext = ext.ToUpper();

            devDept.Eyeshot.Translators.ReadFileAsync rf;
            if(ext == ".BR3")
            {
                rf = new devDept.Eyeshot.Translators.ReadFile(filename);
            }
            else if (ext == ".IGES" || ext == ".IGS")
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
                rf = new devDept.Eyeshot.Translators.ReadAutodesk(filename);
            }
            else if (ext == ".IFC" || ext == ".IFCZIP")
            {
                rf = new devDept.Eyeshot.Translators.ReadIFC(filename);
            }
            else if (ext == ".3DS")
            {
                rf = new devDept.Eyeshot.Translators.Read3DS(filename);
            }
            else if (ext == ".PDF")
            {
                rf = new devDept.Eyeshot.Translators.ReadPDF(filename);
            }
            else if (ext == ".GCODE")
            {
                rf = new devDept.Eyeshot.Translators.ReadGCode(filename);
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
                    List<string> directories = new List<string>();
                    directories.Add(directory);
                    directories.Add(System.IO.Path.Combine(directory, onlyFileName));
                    foreach (var directoryName in directories)
                    {
                        if (System.IO.Directory.Exists(directoryName))
                        {
                            string[] textureFiles = System.IO.Directory.GetFiles(directoryName);
                            foreach (var textureFile in textureFiles)
                            {
                                if (!FileHelper.IsImageFile(textureFile))
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
        static public WriteFileAsync GetWriteFileAsync(devDept.Eyeshot.Model model, string filename, bool ascii = false)
        {
            string ext = System.IO.Path.GetExtension(filename);
            ext = ext.ToUpper();

            WriteParamsWithDrawings writeParam = new WriteParamsWithDrawings(model, null);
            WriteFileAsync wf = null;
            if (ext == ".BR3")
            {
                var writeFileParam = new WriteFileParams(model.Entities, model.Layers, 
                    model.Blocks, model.Materials, model.TextStyles, model.LineTypes, 
                    devDept.Serialization.contentType.Geometry, devDept.Serialization.serializationType.Compressed, 
                    devDept.Geometry.linearUnitsType.Meters, false, null, model.HatchPatterns);
                wf = new WriteFile(writeFileParam, filename);
            }
            else if (ext == ".IGES" || ext == ".IGS")
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
                WriteAutodeskParams aWriteParam = new WriteAutodeskParams(model, null);
                wf = new WriteAutodesk(aWriteParam, filename);
            }
            else if (ext == ".HTML")
            {
                WriteParamsWithMaterials writeParamTmp = new WriteParamsWithMaterials(model);
                writeParamTmp.Materials = model.Materials;

                wf = new WriteWebGL(writeParamTmp, Material.Aluminium, filename);

            }
            else if (ext == ".XML")
            {
                wf = new WriteXML(writeParam, filename);
            }
            else if (ext == ".PRC")
            {
                WritePrcParams prcParam = new WritePrcParams(model);
                wf = new WritePRC(prcParam, filename);
            }
            else
            {
                // 저장불가 파일확장자는 save as로 해야함
                return null;
                //System.Diagnostics.Debug.Assert(false);
            }


            return wf;
        }
    }
}
