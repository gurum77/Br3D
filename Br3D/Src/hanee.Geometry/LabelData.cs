namespace hanee.Geometry
{
    public class LabelData
    {
        public enum Property
        {
            normal,
            preview
        }

        public Property property { get; set; } = Property.normal;
    }
}
