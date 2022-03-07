using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Graphics;

namespace hanee.Geometry
{
    public class DepthTestAlwaysRegion : devDept.Eyeshot.Entities.Region
    {
        public DepthTestAlwaysRegion(ICurve outer) : base(outer)
        {

        }

        public DepthTestAlwaysRegion(Region region) : base(region)
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
