using devDept.Eyeshot.Entities;

namespace hanee.ThreeD
{
    public class EntityPropertiesManager
    {
        HModel model;
        public string currentLayerName { get; set; } = "Default";

        public colorMethodType currentColorMethodType { get; set; } = colorMethodType.byLayer;
        public System.Drawing.Color currentColor { get; set; } = System.Drawing.Color.White;

        public colorMethodType currentLinetypeMethodType { get; set; } = colorMethodType.byLayer;
        public string currentLinetype { get; set; } = "Default";

        public EntityPropertiesManager(HModel model)
        {
            this.model = model;
        }

        public void SetDefaultProperties(Entity ent, bool tempEntity=false)
        {
            if (!model.Layers.Contains(currentLayerName))
            {
                currentLayerName = model.Layers[0].Name;
            }

            ent.LayerName = currentLayerName;
            ent.Color = currentColor;
            ent.ColorMethod = currentColorMethodType;
            ent.LineTypeName = currentLinetype;
            ent.LineTypeMethod = currentLinetypeMethodType;

            // temp entity는 layer참조 못함(직접 지정해야함)
            if(tempEntity)
            {
                ent.Color = currentColorMethodType == colorMethodType.byLayer ? model.Layers[currentLayerName].Color : currentColor;
                ent.ColorMethod = colorMethodType.byEntity;
                ent.LineTypeName = currentLinetypeMethodType == colorMethodType.byLayer ? model.LineTypes[currentLayerName].Name : currentLinetype;
                ent.LineTypeMethod = colorMethodType.byEntity;
            }
        }
    }
}
