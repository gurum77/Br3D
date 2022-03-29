using devDept.Eyeshot;
using hanee.ThreeD;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionMove : ActionBase
    {
        public ActionMove(Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            var entities = await GetEntity("Select entities");

            EndAction();
            return true;
        }
    }
}
