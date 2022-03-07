using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Labels;
using devDept.Geometry;
using hanee.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;


namespace hanee.ThreeD
{
    public class Util
    {
        static public string GetExePath()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        // jittering 블럭인 경우 일단 객체로 변환하고, block의 변환정보를 리턴
        public static Transformation ConvertJitteringBlockReferenceToEntities(Model model, List<Entity> entities)
        {
            if (entities == null || entities.Count != 1)
                return null;

            BlockReference br = entities[0] as BlockReference;
            if (br == null || !br.BlockName.Contains("Jittering"))
                return null;

            Block b = model.Blocks[br.BlockName];
            if (b == null)
                return null;

            entities.Clear();
            entities.AddRange(b.Entities);

            return br.GetFullTransformation(model.Blocks);
        }

        // texture를 xy 평면에 그대로 내리는 방식으로 texture coords를 설정한다.
        // 이미지가 model의 좌측 하단에서 시작되는 조건이다.
        public static void ApplyTextureMappingToXYPlane(Model model, Mesh mesh, Point2D textureLeftBottom, double scaleX, double scaleY)
        {
            if (model == null || mesh == null)
                return;

            if (!model.Materials.Contains(mesh.MaterialName))
                return;


            var mat = model.Materials[mesh.MaterialName];
            if (mat == null || mat.TextureImage == null)
                return;

            // 이미지 크기
            float w = (float)mat.TextureImage.Width * (float)scaleX;
            float h = (float)mat.TextureImage.Height * (float)scaleY;

            Point2D leftBottom = textureLeftBottom;

            // triangle 개수 * 3 이 texturecoords 개수임
            int idx = 0;
            for (int i = 0; i < mesh.Triangles.Length; ++i)
            {
                var tri = mesh.Triangles[i];
                for (int t = 0; t < 3; ++t)
                {
                    int v = tri.V1;
                    if (t == 1)
                        v = tri.V2;
                    else if (t == 2)
                        v = tri.V3;
                    Point3D pt = mesh.Vertices[v];
                    PointF tc = mesh.TextureCoords[idx];
                    mesh.TextureCoords[idx].X = (float)(pt.X - leftBottom.X) / w;
                    mesh.TextureCoords[idx].Y = (float)(pt.Y - leftBottom.Y) / h;
                    idx++;
                }
            }
        }

        // model에 있는 entity중 유효하지 않은것들을 골라낸다.
        public static EntityList FindInvalidEntities(Model model)
        {
            EntityList entities = new EntityList();
            foreach (var ent in model.Entities)
            {
                if (ent.IsValid())
                    continue;

                entities.Add(ent);
            }

            return entities;
        }

        public static Element GetElementByLabel(Label lab)
        {
            return lab.LabelData as Element;
        }

        public static Element GetElementByEntity(Entity ent)
        {
            return ent.EntityData as Element;
        }

        public static List<Label> GetElementLabels(Model model, Element element)
        {
            if (model.ActiveViewport.Labels == null || model.ActiveViewport.Labels.Count == 0)
                return null;

            var labels = model.ActiveViewport.Labels.Where(x => GetElementByLabel(x) == element);

            return labels == null ? null : labels.ToList();
        }

        public static List<Entity> GetElementEntities(Model model, Element element)
        {
            if (model.Entities == null || model.Entities.Count == 0)
                return null;

            var entities = model.Entities.Where(x => GetElementByEntity(x) == element);

            // jittering때문에 블럭에 들어가 있는 경우가 있다.
            // jittering block안에 있는 객체를 리턴한다.
            //if (entities.Count() == 1)
            //{
            //    foreach (var ent in entities)
            //    {
            //        BlockReference br = ent as BlockReference;
            //        if (br == null)
            //            continue;
            //        if (!br.BlockName.Contains("Jittering"))
            //            continue;
            //        var block = model.Blocks[br.BlockName];
            //        if (block == null)
            //            continue;
            //        entities = block.Entities;
            //    }
            //}


            return entities == null ? null : entities.ToList();
        }

        // element에 연결된 모든 entity의 visible 설정을 변경한다.
        public static void SetVisibleElementEntities(Model model, Element ele)
        {
            DrawableElement dele = ele as DrawableElement;
            if (dele == null)
                return;

            var entities = GetElementEntities(model, ele);
            if (entities == null)
                return;

            foreach (var ent in entities)
            {
                ent.Visible = dele.visible;
            }
        }

        // element에 연결된 모든 entity를 선택한다.
        public static void SelectElementEntities(Model model, Element element)
        {
            var entities = GetElementEntities(model, element);
            if (entities == null)
                return;

            foreach (var ent in entities)
            {
                ent.Selected = true;
            }
        }

        // T 타입의 모든 element의 eyeshot label을모두 삭제
        public static void ClearAllElementLabelsByType<T>(Model model) where T : Element
        {
            var eles = ElementManager.Instance.GetAllElements<T>();
            foreach (var ele in eles)
            {
                ClearElementLabels(model, ele);
            }
        }

        // T 타입의 모든 element의 eyeshot entity를 모두 삭제
        public static void ClearAllElementEntitiesByType<T>(Model model) where T : Element
        {
            var eles = ElementManager.Instance.GetAllElements<T>();
            foreach (var ele in eles)
            {
                ClearElementEntities(model, ele);
            }
        }

        // element에 연결된 모든 entity를 제거한다.
        public static void ClearElementLabels(Model model, Element element)
        {
            if (model.Entities == null || model.Entities.Count == 0)
                return;

            var labels = GetElementLabels(model, element);
            foreach (var lab in labels)
            {
                model.ActiveViewport.Labels.Remove(lab);
            }
        }

        // element에 연결된 모든 entity를 제거한다.
        public static void ClearElementEntities(Model model, Element element)
        {
            if (model.Entities == null || model.Entities.Count == 0)
                return;

            var entities = GetElementEntities(model, element);
            foreach (var ent in entities)
            {
                model.Entities.Remove(ent);
            }
        }

        // block reference를 explode한다.
        public static EntityList ExplodeBlockReference(BlockKeyedCollection blocks, EntityList entities)
        {
            EntityList explodedEntityList = new EntityList();

            // block reference는 explode해서 그린다.
            foreach (var ent in entities)
            {
                BlockReference br = ent as BlockReference;
                if (br == null)
                {
                    explodedEntityList.Add(ent);
                    continue;
                }

                // block reference이면 깬다.
                if (!blocks.Contains(br.BlockName))
                    continue;
                var entitiesTmp = br.Explode(blocks);
                if (entitiesTmp == null)
                    continue;

                // attribute는 text로 변환해서 넣고, 아닌것들은 그냥 넣는다.
                foreach (var entSub in entitiesTmp)
                {
                    devDept.Eyeshot.Entities.Attribute att = entSub as devDept.Eyeshot.Entities.Attribute;

                    if (att == null)
                    {
                        explodedEntityList.Add(entSub);
                        continue;
                    }


                    // tag의 값을 가져와서 text로 변환
                    AttributeReference attValue;
                    if (!br.Attributes.TryGetValue(att.Tag, out attValue))
                        continue;

                    Text newText = new Text(att.InsertionPoint, attValue.Value, att.Height);
                    newText.CopyAttributes(att);
                    newText.StyleName = att.StyleName;
                    newText.Alignment = att.Alignment;

                    explodedEntityList.Add(newText);
                }
            }

            return explodedEntityList;
        }

        // element의 속성으로 entity에 material을 적용한다.
        public static void ApplyMaterialByElement(Model model, Entity ent, DrawableElement element)
        {

            if (!element.enabledMaterial)
                return;

            string materialName = element.materialName;
            if (element.entityHasSelfMaterialName)
                materialName = ent.MaterialName;

            // 없으면 리턴
            if (materialName == null || !model.Materials.Contains(materialName))
                return;

            if (ent is Mesh)
            {
                Mesh mesh = ent as Mesh;
                if (mesh.Triangles.Count() == 0)
                    return;

                mesh.ApplyMaterial(materialName, textureMappingType.Plate, 1, 1);

                ApplyTextureMappingToXYPlane(model, mesh, element.mappingLeftBottom, element.materialScaleU, element.materialScaleV);
            }
            else if (ent is Solid)
            {
                Solid solid = ent as Solid;
                solid.ApplyMaterial(materialName, textureMappingType.Plate, element.materialScaleU, element.materialScaleV);
            }
        }

        // element에 포함된 모든 객체에 material을 적용한다.
        public static void ApplyMaterialEntitisInElement(Model model, DrawableElement element)
        {
            if (element == null)
                return;

            if (!element.enabledMaterial)
                return;

            var entities = GetElementEntities(model, element);
            if (entities == null)
                return;

            foreach (var ent in entities)
            {
                ApplyMaterialByElement(model, ent, element);
            }
        }
    }
}
