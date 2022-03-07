using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using System;
using System.Collections.Generic;

namespace hanee.ThreeD
{
    public static class DesignHelper
    {
        // 기본 라인 타입을 만든다.
        static public void AddDefaultLineTypes(this Model design)
        {
            // hidden
            if (!design.LineTypes.Contains("hidden"))
            {
                design.LineTypes.Add(LineTypeHelper.hidden);
            }

            // Center
            if (!design.LineTypes.Contains("center"))
            {
                design.LineTypes.Add(LineTypeHelper.center);
            }

            // Phantom
            if (!design.LineTypes.Contains("phantom"))
            {
                design.LineTypes.Add(LineTypeHelper.phantom);
            }

        }
        // 선택한 모든 객체 리턴
        static public List<Entity> GetAllSelectedEntities(this Model design)
        {
            List<Entity> selectedEntity = new List<Entity>();
            foreach (Entity ent in design.Entities)
                if (ent.Selected)
                    selectedEntity.Add(ent);


            return selectedEntity;
        }

        static public void UnselectEntity(this Model design, Entity ent)
        {
            if (ent == null)
                return;

            ent.Selected = false;
            if (ent is BlockReference)
            {
                BlockReference block = (BlockReference)ent;
                IList<Entity> entities = block.GetEntities(design.Blocks);
                foreach (var subEnt in entities)
                {
                    design.UnselectEntity(subEnt);
                }
            }
        }

        static public void UnselectAll(this Model design)
        {
            design.SetCurrent(null);
            foreach (var ent in design.Entities)
            {
                design.UnselectEntity(ent);
            }

            foreach (var label in design.ActiveViewport.Labels)
            {
                label.Selected = false;
            }
        }

        // jittering 제거용 block 이름 리턴
        static public String GetElementJitteringBlockName(this Model design, Element element)
        {
            return $"ElementJitteringBlock_{element.id.id.ToString()}";
        }

        static public Point3D GetInsertPointByElement(this Model design, Element element)
        {
            var br = design.FindBlockReferenceByElement(element);
            if (br == null)
                return null;

            return br.InsertionPoint;
        }

        static public BlockReference FindBlockReferenceByElement(this Model design, Element element)
        {
            string blockName = design.GetElementJitteringBlockName(element);

            foreach (var ent in design.Entities)
            {
                if (ent is BlockReference)
                {
                    BlockReference br = ent as BlockReference;
                    if (br.BlockName == blockName)
                        return br;
                }
            }

            return null;
        }


        // element에 대해서 remove jittering을 한다.
        static public BlockReference RemoveJittering(this Model design, Element element)
        {
            var entities = hanee.ThreeD.Util.GetElementEntities(design, element);
            if (entities == null || entities.Count == 0)
                return null;

            // 모두 선택 해제
            design.UnselectAll();

            // element와 연결된 객체 선택
            foreach (var ent in entities)
            {
                ent.Selected = true;
                ent.EntityData = null;
            }

            // jittering 실행
            string blockName = design.GetElementJitteringBlockName(element);

            // 이미 블럭이 있으면 블럭을 제거하고 다시 만들어야 한다.
            if (design.Blocks.Contains(blockName))
            {
                design.Blocks.Remove(blockName);
            }

            var br = design.RemoveJittering(blockName);
            if (br != null)
            {
                element.Attach(br);
            }
            return br;
        }

    }
}
