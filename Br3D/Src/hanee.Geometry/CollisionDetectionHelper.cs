using devDept.Eyeshot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Geometry
{
    static public class CollisionDetectionHelper
    {
        static public bool IsCollided(this CollisionDetection cd)
        {
            if (cd.Result == null)
                return false;
            foreach (var re in cd.Result)
            {
                if (re.CollisionItems == null)
                    continue;
                if (re.CollisionItems.Count > 0)
                    return true;
            }

            return false;
        }
    }
}
