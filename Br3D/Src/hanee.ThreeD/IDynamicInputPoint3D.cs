using devDept.Geometry;
using System;

namespace hanee.ThreeD
{
    interface IDynamicInputPoint3D : IDynamicInput
    {
        void ModifyPoint3D(devDept.Eyeshot.Environment environment, ref Point3D pt);
    }
}
