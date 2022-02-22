using devDept.Eyeshot.Entities;
using hanee.ThreeD;
using System.ComponentModel;
using System.Drawing;

namespace Br3D
{
    public class EntityProperties
    {
        public Entity entity;

        public EntityProperties(Entity entity)
        {
            this.entity = entity;
            
        }

        [CategoryEx(" ")]
        [DisplayNameEx("Type")]
        public string entityType { get => entity.GetType().Name; }

        [CategoryEx("General")]
        [DisplayNameEx("Layer")]
        public string layer { get => entity.LayerName; set => entity.LayerName = value; }

        [CategoryEx("Color")]
        [DisplayNameEx("Color")]
        public Color color { get => entity.Color; set => entity.Color = value; }
        
        [CategoryEx("Color")]
        [DisplayNameEx("Method")]
        public colorMethodType colorMethodtype
        {
            get => entity.ColorMethod;
            set => entity.ColorMethod = value;
        }

        [CategoryEx("Line Type")]
        [DisplayNameEx("Scale")]
        public float lineTypeScale { get => entity.LineTypeScale; set => entity.LineTypeScale = value; }

        [CategoryEx("Line Type")]
        [DisplayNameEx("Line Type")]
        public string lineType { get => entity.LineTypeName; set => entity.LineTypeName = value; }

        [CategoryEx("Line Type")]
        [DisplayNameEx("Method")]
        public colorMethodType lineTypeMethod { get => entity.LineTypeMethod; set => entity.LineTypeMethod = value; }

        [CategoryEx("Line Weight")]
        [DisplayNameEx("Line Weight")]
        public float lineWeight { get => entity.LineWeight; set => entity.LineWeight = value; } 
        
        [CategoryEx("Line Weight")]
        [DisplayNameEx("Method")]
        public colorMethodType lineWeightMethod { get => entity.LineWeightMethod; set => entity.LineWeightMethod = value; }
    }
}
