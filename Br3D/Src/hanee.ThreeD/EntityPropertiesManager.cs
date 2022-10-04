using devDept.Eyeshot.Entities;

namespace hanee.ThreeD
{
    public class EntityPropertiesManager
    {
        HModel model;
        
        public EntityPropertiesManager(HModel model)
        {
            this.model = model;
        }

        public void SetDefaultProperties(Entity ent, bool tempEntity=false)
        {
            var options = Options.Instance;

            if (!model.Layers.Contains(options.currentLayerName))
                options.currentLayerName = model.Layers[0].Name;
            if (options.currentLinetype != null && !model.LineTypes.Contains(options.currentLinetype))
                options.currentLinetype = null;

            ent.LayerName = options.currentLayerName;
            ent.Color = options.currentColor;
            ent.ColorMethod = options.currentColorMethodType;
            ent.LineTypeName = options.currentLinetype;
            ent.LineTypeMethod = options.currentLinetypeMethodType;
            ent.LineTypeScale = options.curLinetypeScale;

            // temp entity는 layer참조 못함(직접 지정해야함)
            if (tempEntity)
            {
                ent.Color = options.currentColorMethodType == colorMethodType.byLayer ? model.Layers[options.currentLayerName].Color : options.currentColor;
                ent.ColorMethod = colorMethodType.byEntity;
                ent.LineTypeName = options.currentLinetypeMethodType == colorMethodType.byLayer ? model.Layers[options.currentLayerName].LineTypeName : options.currentLinetype;
                ent.LineTypeMethod = colorMethodType.byEntity;
                ent.LineTypeScale = options.curLinetypeScale;
            }
        }
    }
}
