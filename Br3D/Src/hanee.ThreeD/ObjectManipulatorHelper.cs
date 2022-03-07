using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    public class ObjectManipulatorHelper : ObjectManipulator
    {
        public ObjectManipulatorHelper(ObjectManipulator another) : base(another.Size, another.Visible, another.ShowOriginalWhileEditing, another.Ball,
          another.TranslateX, another.TranslateY, another.TranslateZ, another.RotateX, another.RotateY, another.RotateZ, another.RotationStep, another.TranslationStep)
        {
            selectedEntity = null;
        }

        public bool manipulating { get; private set; }
        private bool first = true;
        protected Entity selectedEntity { get; set; }
        protected override bool OnDrag(ref System.Drawing.Point lastPoint, System.Drawing.Point curPoint, Viewport viewport)
        {
            if (first)
                lastPoint = curPoint;

            first = false;

            return base.OnDrag(ref lastPoint, curPoint, viewport);
        }

        public virtual void StartManipulating(HModel model, MouseEventArgs e)
        {
            if (manipulating)
            {
                EndManipulating(model);
            }

            // 선택된 객체가 없다면 불가
            var selectedEntities = model.GetAllSelectedEntities();
            if (selectedEntities == null || selectedEntities.Count == 0)
                return;

            selectedEntity = selectedEntities[0];

            manipulating = true;
            Enable(new Identity(), true);
        }

        public virtual void EndManipulating(HModel model)
        {
            Apply();
            selectedEntity = null;
            model.Entities.Regen();
            model.ActiveViewport.Invalidate();
            manipulating = false;
        }

        public virtual void CancelManipulating()
        {
            Cancel();
            manipulating = false;
        }
    }
}
