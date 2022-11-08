using DevExpress.XtraBars;
using hanee.ThreeD;

namespace Br3D.Actions
{
    public class ActionGridSnap : ActionBase
    {
        BarButtonItem barButtonItem;
        public ActionGridSnap(devDept.Eyeshot.Environment environment, BarButtonItem barButtonItem) : base(environment)
        {
            this.barButtonItem = barButtonItem;
          
        }

        public override void Run()
        {
            StartAction();


            var hModel = GetHModel();            
            hModel.gridSnapping.enabled = !hModel.gridSnapping.enabled;
            barButtonItem.Down = hModel.gridSnapping.enabled;

            EndAction();
        }
    }
}
