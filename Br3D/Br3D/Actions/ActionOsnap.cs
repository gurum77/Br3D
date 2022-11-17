using hanee.ThreeD;

namespace Br3D.Actions
{
    public class ActionOsnap : ActionBase
    {
        ControlModel controlModel;
        Snapping.objectSnapType osnapType;
        public ActionOsnap(devDept.Eyeshot.Environment environment, ControlModel controlModel, Snapping.objectSnapType osnapType) : base(environment)
        {
            this.controlModel = controlModel;
            this.osnapType = osnapType;
        }


        // osnap 는 action 진행에 영향을 주면 안되므로 start action / end action을 호출 하지 않는다.
        public override void Run()
        {
            if (osnapType == Snapping.objectSnapType.End)
                controlModel.End();
            else if (osnapType == Snapping.objectSnapType.Intersect)
                controlModel.Intersection();
            else if (osnapType == Snapping.objectSnapType.Point)
                controlModel.Point();
            else if (osnapType == Snapping.objectSnapType.Center)
                controlModel.Center();
            else if (osnapType == Snapping.objectSnapType.Mid)
                controlModel.Middle();

            CmdBarManager.InitTextEdit();
        }
    }
}
