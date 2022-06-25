using System.Drawing;
using System.Xml.Serialization;

// xml로 저장할 수 잇는 color
namespace hanee.ThreeD
{
    public class XmlColor
    {
        public XmlColor()
        {

        }
        public XmlColor(Color color)
        {
            colorValue = color;
        }
        [XmlIgnore]
        public Color colorValue { get; set; }
        public string color
        {
            get { return ColorTranslator.ToHtml(colorValue); }
            set { colorValue = ColorTranslator.FromHtml(value); }
        }
    }
}
