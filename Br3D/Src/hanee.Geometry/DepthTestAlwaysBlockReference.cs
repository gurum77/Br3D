using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using devDept.Graphics;

namespace hanee.Geometry
{
    public class DepthTestAlwaysBlockReference : BlockReference
    {
        public DepthTestAlwaysBlockReference(Point3D insPoint, string blockName, double sx, double sy, double sz, double rotationAngleInRadians) :
            base(insPoint, blockName, sx, sy, sz, rotationAngleInRadians)
        {

        }

        protected override void Draw(DrawParams data)
        {
            data.RenderContext.PushDepthStencilState();
            data.RenderContext.SetState(depthStencilStateType.DepthTestAlways);

            base.Draw(data);

            data.RenderContext.PopDepthStencilState();
        }


    }
}
