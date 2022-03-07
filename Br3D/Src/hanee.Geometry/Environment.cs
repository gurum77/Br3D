using devDept.Eyeshot;
using System;
using System.Runtime.Serialization;

namespace hanee.Geometry
{
    // 도면 환경
    // import를 하거나 하면 보관해야 하는 것들
    [Serializable]
    public class Environment : ISerializableHelper
    {
        public Environment()
        {
            lineTypes = new LineTypeKeyedCollection();
            textStyles = new TextStyleKeyedCollection();
            materials = new MaterialKeyedCollection();
            layers = new LayerKeyedCollection();
            blocks = new BlockKeyedCollection();
        }

        public void Serialize(SerializationInfo info, bool serialize)
        {
            lineTypes = SerializeHelper.Serialize<LineTypeKeyedCollection>(info, "line_types", lineTypes, serialize);
            textStyles = SerializeHelper.Serialize<TextStyleKeyedCollection>(info, "text_styles", textStyles, serialize);
            materials = SerializeHelper.Serialize<MaterialKeyedCollection>(info, "materials", materials, serialize);
            layers = SerializeHelper.Serialize<LayerKeyedCollection>(info, "layers", layers, serialize);
            blocks = SerializeHelper.Serialize<BlockKeyedCollection>(info, "blocks", blocks, serialize);
        }

        public Environment(SerializationInfo info, StreamingContext context)
        {
            Serialize(info, false);
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Serialize(info, true);
        }



        LineTypeKeyedCollection lineTypes { get; set; }
        TextStyleKeyedCollection textStyles { get; set; }
        MaterialKeyedCollection materials { get; set; }
        LayerKeyedCollection layers { get; set; }
        BlockKeyedCollection blocks { get; set; }

        /// <summary>
        /// mode에 있는 환경을 여기에 추가하거나 교체한다.
        /// </summary>
        /// <param name="model"></param>
        public void AddOrReplaceFromModel(Model model)
        {
            foreach (var lt in model.LineTypes)
                lineTypes.AddOrReplace(lt);

            foreach (var ts in model.TextStyles)
                textStyles.AddOrReplace(ts);

            foreach (var ma in model.Materials)
                materials.AddOrReplace(ma);

            foreach (var la in model.Layers)
                layers.AddOrReplace(la);

            foreach (var b in model.Blocks)
            {
                if (b.Name == "RootBlock")
                    continue;

                blocks.AddOrReplace(b);
            }


        }

        /// <summary>
        /// model에 환경을 넣어준다.
        /// </summary>
        /// <param name="model"></param>
        public void FillToModel(Model model)
        {
            foreach (var lt in lineTypes)
                model.LineTypes.AddOrReplace(lt);

            foreach (var ts in textStyles)
                model.TextStyles.AddOrReplace(ts);

            foreach (var ma in materials)
                model.Materials.AddOrReplace(ma);

            foreach (var la in layers)
                model.Layers.AddOrReplace(la);

            foreach (var b in blocks)
            {
                if (b.Name == "RootBlock")
                    continue;
                model.Blocks.AddOrReplace(b);
            }
        }

    }
}
