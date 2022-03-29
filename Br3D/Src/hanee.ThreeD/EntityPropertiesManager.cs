using devDept.Eyeshot.Entities;

namespace hanee.ThreeD
{
    public class EntityPropertiesManager
    {
        HModel model;
        string currentLayerName = "Default";
        public EntityPropertiesManager(HModel model)
        {
            this.model = model;
        }

        public void SetDefaultProperties(Entity ent)
        {
            if (!model.Layers.Contains(currentLayerName))
            {
                currentLayerName = model.Layers[0].Name;
            }

            ent.LayerName = currentLayerName;
            ent.ColorMethod = colorMethodType.byLayer;
        }
    }
}
