using devDept.Eyeshot.Entities;
using hanee.Geometry;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace hanee.ThreeD
{
    public class Options : Singleton<Options>
    {
        public enum TempEntityColorMethod
        {
            byOneColor,
            byTransparencyColor
        }

        public void Default()
        {
            dimTextHeight = 2.0f;
            annotationTextHeight = 30;
            tempEntityColor = Color.White;
            backgroundColorTop.colorValue = Color.FromArgb(255, 18, 32, 41);
            backgroundColorBottom.colorValue = Color.FromArgb(255, 46, 82, 103);
            backgroundColor2D.colorValue = Color.Black;
        }

        public const string defaultLanguage = "en-US";


        public string appName { get; set; } = "hanee.ThreeD";
        public string language { get; set; } = defaultLanguage;

        // drawing

        public float dimTextHeight { get; set; } = 2.0f;
        public float annotationTextHeight { get; set; } = 30;
        public int decimals { get; set; } = 5;  // 좌표, 길이값등의 허용 소수점 자릿수

        public string currentLayerName { get; set; } = "Default";

        public colorMethodType currentColorMethodType { get; set; } = colorMethodType.byLayer;
        public System.Drawing.Color currentColor { get; set; } = System.Drawing.Color.White;

        public colorMethodType currentLinetypeMethodType { get; set; } = colorMethodType.byLayer;
        public string currentLinetype { get; set; } = "Default";

        public float curLinetypeScale { get; set; } = 1.0f;
        // color


        public TempEntityColorMethod tempEntityColorMethod { get; set; } = TempEntityColorMethod.byTransparencyColor;
        public Color tempEntityColor { get; set; } = Color.White;
        public XmlColor backgroundColorTop { get; set; } = new XmlColor(Color.FromArgb(255, 18, 32, 41));
        public XmlColor backgroundColorBottom { get; set; } = new XmlColor(Color.FromArgb(255, 46, 82, 103));
        public XmlColor backgroundColor2D { get; set; } = new XmlColor(Color.Black);
        public bool saveImageWithUI { get; set; } = false;    // 이미지 저장할때 UI 포함(툴바등..)
        public bool saveImageWithBackground { get; set; } = false;  // 이미지 저장할때 배경 포함
        


        // 즐겨찾기 저장하는 파일 경로
        string GetOptionsFIlePath()
        {
            var path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ApplicationData), appName, $"{appName}Options.xml");
            return path;
        }

        // xml 파일에서 즐겨찾기를 로드한다.
        public void LoadOptions()
        {
            try
            {
                var path = GetOptionsFIlePath();
                if (System.IO.File.Exists(path))
                {
                    using (FileStream fileStream = File.Open(path, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        XmlSerializer xml = new XmlSerializer(typeof(Options));
                        var tmpOptions = xml.Deserialize(fileStream) as Options;
                        Options.Reinitialize(tmpOptions);

                        if (Options.Instance.tempEntityColor.A == 0)
                            Options.Instance.tempEntityColor = System.Drawing.Color.White;
                    }
                }
            }
            catch/* (Exception ex)*/
            {
#if DEBUG
                //MessageBox.Show(ex.Message);
#endif
            }
        }

        // 즐겨찾기를 저장한다.
        public void SaveOptions()
        {
            try
            {
                var path = GetOptionsFIlePath();
                var directory = System.IO.Path.GetDirectoryName(path);
                System.IO.Directory.CreateDirectory(directory);
                XmlSerializer xml = new XmlSerializer(typeof(Options));
                using (FileStream fileStream = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                {
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    var settings = new XmlWriterSettings();
                    settings.Indent = true;
                    settings.OmitXmlDeclaration = true;
                    using (var writer = XmlWriter.Create(fileStream, settings))
                    {
                        xml.Serialize(writer, Options.Instance, ns);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Save options faild.", ex.Message);
            }
        }
    }
}
