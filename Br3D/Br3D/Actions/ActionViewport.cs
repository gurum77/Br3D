using devDept.Eyeshot;
using hanee.ThreeD;

namespace Br3D.Actions
{
    public class ActionViewport : ActionBase
    {
        public enum Viewport
        {
            single,
            v1x1,
            v1x2,
            v2x2
        }

        ControlModel controlModel;
        Viewport viewport;
        public ActionViewport(Environment environment, ControlModel controlModel, Viewport viewport) : base(environment)
        {
            this.controlModel = controlModel;
            this.viewport = viewport;
        }

        // viewport 는 action 진행에 영향을 주면 안되므로 start action / end action을 호출 하지 않는다.
        public override void Run()
        {
            if (viewport == Viewport.single)
                controlModel.ViewportSingle();
            else if (viewport == Viewport.v1x1)
                controlModel.Viewport1x1();
            else if (viewport == Viewport.v1x2)
                controlModel.Viewport1x2();
            else if (viewport == Viewport.v2x2)
                controlModel.Viewport2x2();

            CmdBarManager.InitTextEdit();
        }
    }
}
