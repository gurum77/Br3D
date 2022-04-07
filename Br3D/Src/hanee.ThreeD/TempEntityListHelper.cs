using devDept.Eyeshot;
using devDept.Eyeshot.Entities;

namespace hanee.ThreeD
{
    static public class TempEntityListHelper
    {
        // template entity 수정후 regen
        // 필요한 객체만 하자.
        // 대부분은 안해도 된다.
        static public void RegenAfterModify(this TempEntityList entities)
        {
            foreach (Entity ent in entities)
            {
                if (ent is CompositeCurve)
                    ent.Regen(0.001);
            }
        }
    }
}
