using devDept.Eyeshot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.ThreeD
{
    static public class WorkspaceHelper
    {
        static public bool IsTopViewOnly(this Workspace ws)
        {
            HDesign hDesign = ws as HDesign;
            if (hDesign == null)
                return false;

            return hDesign.TopViewOnly;
        }
    }
}
