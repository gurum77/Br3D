using hanee.Geometry;
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace hanee.ThreeD
{
    public class Options : Singleton<Options>
    {
        public const string defaultLanguage = "en-US";


        public string appName { get; set; } = "hanee.ThreeD";
        public string language { get; set; } = defaultLanguage;
        public float dimTextHeight { get; set; } = 2.0f;
        public int decimals { get; set; } = 5;  // 좌표, 길이값등의 허용 소수점 자릿수
        
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
            catch/* (Exception ex)*/
            {
                //MessageBox.Show("Save options faild.", ex.Message);
            }
        }
    }
}
