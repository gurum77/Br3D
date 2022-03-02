using devDept.Eyeshot.Entities;
using hanee.ThreeD;

namespace Br3D
{
    public class TextProperties : EntityProperties
    {
        public TextProperties(Entity entity) : base(entity)
        {
        }

        Text text { get => entity as Text; }
        [CategoryEx("Text")]
        [DisplayNameEx("Contents")]
        public string contents { get => text.TextString; set => text.TextString = value; }

        public string textStyle{ get => text.StyleName; set => text.StyleName = value; }

        [CategoryEx("Text")]
        [DisplayNameEx("Width factor")]
        public double widthFactor{ get => text.WidthFactor; set => text.WidthFactor= value; }
    }
}
