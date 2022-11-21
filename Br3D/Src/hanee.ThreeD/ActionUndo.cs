namespace hanee.ThreeD
{
    public class ActionUndo : ActionBase
    {
        public ActionUndo(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override void Run()
        {
            if (actionManager.CanUndo)
                actionManager.Undo();
        }
    }
}
