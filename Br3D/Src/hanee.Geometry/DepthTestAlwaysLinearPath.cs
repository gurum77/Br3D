using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using devDept.Graphics;
using System.Collections.Generic;

namespace hanee.Geometry
{
    public class DepthTestAlwaysLinearPath : LinearPath
    {

        public DepthTestAlwaysLinearPath(ICollection<Point3D> points) : base(points)
        { }

        public DepthTestAlwaysLinearPath(Point3D[] points) : base(points)
        { }

        public DepthTestAlwaysLinearPath(LinearPath linearPath) : base(linearPath)
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
