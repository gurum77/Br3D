using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System.Drawing;

namespace Br3D
{
    public class EntityProperties
    {
        Entity ent;
        public BlockReference AsBlockReference => ent as BlockReference;
        public Text AsText => ent as Text;

        public EntityProperties(Entity ent)
        {
            this.ent = ent;
        }

        public string EntityType { get => ent.GetType().Name; }
        public bool Visible { get => ent.Visible; set => ent.Visible = value; }
        public Color Color { get => ent.Color; set => ent.Color = value; }
        public colorMethodType ColorMethod { get => ent.ColorMethod; set => ent.ColorMethod = value; }
        public Point3D BoxMin { get => ent.BoxMin; }
        public Point3D BoxMax { get => ent.BoxMax; }
        public int GroupIndex { get => ent.GroupIndex; set => ent.GroupIndex = value; }
        public string LayerName { get => ent.LayerName; set => ent.LayerName = value; }

        public bool enableBlockName => AsBlockReference != null;
        public string BlockName {
            get => AsBlockReference?.BlockName;
            set
            {
                if (AsBlockReference == null)
                    return;
                AsBlockReference.BlockName = value;
            }
        }

        public string LineTypeName { get => ent.LineTypeName; set => ent.LineTypeName = value; }
        public float LineTypeScale { get => ent.LineTypeScale; set => ent.LineTypeScale = value; }
        public float LineWeight  { get => ent.LineWeight; set => ent.LineWeight = value; }
        public colorMethodType LineWeightMethod { get => ent.LineWeightMethod; set => ent.LineWeightMethod = value; }

        public bool enableTextString => AsText != null;
        public string TextString
        {
            get => AsText?.TextString;
            set
            {
                if (AsText == null)
                    return;
                AsText.TextString = value;
            }
        }


        public bool enableStyleName => AsText != null;
        public string StyleName
        {
            get => AsText?.StyleName;
            set
            {
                if (AsText == null)
                    return;
                AsText.StyleName = value;
            }
        }

        public bool enableHeight => AsText != null;
        public double Height
        {
            get => AsText == null ? 1 : AsText.Height;
            set
            {
                if (AsText == null)
                    return;
                AsText.Height = value;
            }
        }

        public bool enableBillboard => AsText != null;
        public bool Billboard
        {
            get => AsText == null ? false : AsText.Billboard;
            set
            {
                if (AsText == null)
                    return;
                AsText.Billboard = value;
            }
        }

        public bool enableWidthFactor => AsText != null;
        public double WidthFactor
        {
            get => AsText == null ? 1 : AsText.WidthFactor; 
            set
            {
                if (AsText == null)
                    return;
                AsText.WidthFactor = value;
            }
        }

        public bool enableInsertionPoint => AsText != null;
        public Point3D InsertionPoint
        {
            get => AsText?.InsertionPoint;
            set
            {
                if (AsText == null)
                    return;
                AsText.InsertionPoint = value;
            }
        }

        public bool enableBackward => AsText != null;
        public bool Backward
        {
            get => AsText == null ? false : AsText.Backward;
            set
            {
                if (AsText == null)
                    return;
                AsText.Backward = value;
            }
        }

        public bool enableUpsideDown => AsText != null;
        public bool UpsideDown
        {
            get => AsText == null ? false : AsText.UpsideDown;
            set
            {
                if (AsText == null)
                    return;
                AsText.UpsideDown = value;
            }
        }

        public bool enableAlignment => AsText != null;
        public Text.alignmentType Alignment
        {
            get => AsText == null ? Text.alignmentType.BaselineLeft : AsText.Alignment;
            set
            {
                if (AsText == null)
                    return;
                AsText.Alignment = value;
            }
        }

    }
}
