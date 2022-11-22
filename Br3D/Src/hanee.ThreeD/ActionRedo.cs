namespace hanee.ThreeD
{
    public class ActionRedo : ActionBase
    {
        public ActionRedo(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override void Run()
        {
            if (actionManager.CanRedo)
                actionManager.Redo();

            CmdBarManager.InitTextEdit();
        }
    }
}
