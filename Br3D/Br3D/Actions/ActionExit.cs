using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br3D.Actions
{
    class ActionExit : ActionBase
    {
        FormMain formMain;
        public ActionExit(devDept.Eyeshot.Environment environment, FormMain formMain) : base(environment)
        {
            this.formMain = formMain;
        }

        public override void Run()
        {
            // exit는 end action을 미리해야함
            // 안그럼 form이 닫히고 나서 addhisotry등을 하다가 에러남
            StartAction();
            EndAction();

            formMain.Close();
        }
    }
}
