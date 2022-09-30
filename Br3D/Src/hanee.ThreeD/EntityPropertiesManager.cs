using devDept.Eyeshot.Entities;

namespace hanee.ThreeD
{
    public class EntityPropertiesManager
    {
        HModel model;
        public string currentLayerName { get; set; } = "Default";
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
            ent.ColorMethod = colorMethodType.byLayer;

            // temp entity는 layer참조 못함(직접 지정해야함)
            if(tempEntity)
            {
                ent.Color = model.Layers[currentLayerName].Color;
                ent.ColorMethod = colorMethodType.byEntity;
            }
            
        }
    }
}
