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

        // grid snap는 action 진행에 영향을 주면 안되므로 start action / end action을 호출 하지 않는다.
        public override void Run()
        {
            var hModel = GetHModel();            
            hModel.gridSnapping.enabled = !hModel.gridSnapping.enabled;
            barButtonItem.Down = hModel.gridSnapping.enabled;
        }
    }
}
