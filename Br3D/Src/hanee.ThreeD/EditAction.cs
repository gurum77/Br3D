using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using GuiLabs.Undo;
using hanee.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.ThreeD
{
    // 객체 변형
    public class EditAction : AbstractAction
    {
        Model model;
        Entity[] originEntities;    // 원래 객체
        Entity[] beforeEntities;    // 원래 객체의 편집전
        Entity[] afterEntities;    // 원래 객체의 편집후
        public EditAction(Model model, params Entity[] originEntities)
        {
            this.model = model;

            // 원본은 그대로 보관
            this.originEntities = originEntities;

            // 편집전 내용 복사해서 보관
            this.beforeEntities = new Entity[originEntities.Length];
            for (int i = 0; i < originEntities.Length; i++)
            {
                Entity e = originEntities[i];
                this.beforeEntities[i] = e.Clone() as Entity;
            }
        }

        // edit 종료한다.
        public void EndEdit()
        {
            if (this.originEntities == null || this.originEntities.Length == 0)
                return;

            // 편집후 객체 복사해서 보관
            this.afterEntities = new Entity[this.originEntities.Length];
            for (int i = 0; i < this.originEntities.Length; i++)
            {
                Entity e = this.originEntities[i];
                this.afterEntities[i] = e.Clone() as Entity;
            }
        }


        // 실행(편집된 객체를 model에 추가)
        protected override void ExecuteCore()
        {
            if (originEntities == null || afterEntities == null)
                return;

            if (originEntities.Length != afterEntities.Length)
                return;

            
            // 편집된 내용을 복사
            for (int i = 0; i < originEntities.Length; i++)
            {
                var originEnt = originEntities[i];
                var afterEnt = afterEntities[i];
                originEnt.CopyFrom(afterEnt);
            }

            var ro = new RegenOptions();
            model.Entities.Regen(ro);
            model.Invalidate();
        }

        // undo : 편집전 내용을 복사
        protected override void UnExecuteCore()
        {
            if (originEntities == null || beforeEntities == null)
                return;

            if (originEntities.Length != beforeEntities.Length)
                return;

            
            // 편집된 내용을 복사
            for (int i = 0; i < originEntities.Length; i++)
            {
                var originEnt = originEntities[i];
                var beforeEnt = beforeEntities[i];
                originEnt.CopyFrom(beforeEnt);
                
            }

            var ro = new RegenOptions();
            model.Entities.Regen(ro);
            model.Invalidate();
        }
    }
}
