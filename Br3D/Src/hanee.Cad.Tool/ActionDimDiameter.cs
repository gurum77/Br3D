using devDept.Eyeshot.Entities;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionDimDiameter : ActionBase
    {
        public bool radius { get; set; } = false;
        Circle selectedCircle;
        public ActionDimDiameter(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        protected override void OnMouseMove(devDept.Eyeshot.Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);

            if (selectedCircle == null || point3D == null)
            {
                ActionBase.previewEntity = null;
                return;
            }

            var line = new Line(selectedCircle.Center, point3D);
            ActionBase.previewEntity = line;
        }

        public async Task<bool> RunAsync()
        {
            StartAction();

            var entityTypes = new Dictionary<Type, bool>();
            entityTypes.Add(typeof(Circle), true);
            entityTypes.Add(typeof(Arc), true);
            while (true)
            {
                selectedCircle = null;
                var curve = await GetEntity("Select circle, arc", -1, true, entityTypes);
                if (IsCanceled())
                    break;
                selectedCircle = curve as Circle;
                if (selectedCircle == null)
                    continue;


                var msg = $"Text point - Diameter : {selectedCircle.Diameter:0.000}";
                if(radius)
                    msg = $"Text point - Radius : {selectedCircle.Radius:0.000}";
                var pt = await GetPoint3D(msg);
                if (IsCanceled())
                    break;

                // arc 는 지름치수를 넣으면 글자가 뒤집힌다.
                // circle을 만들어서 지정 entity를 넣어준다.
                var tempCircle = new Circle(selectedCircle.Center, selectedCircle.Radius);
                
                var dim = radius ? new RadialDim(tempCircle, pt, Define.DefaultTextHeight) : new DiametricDim(tempCircle, pt, Define.DefaultTextHeight);
                GetHModel()?.entityPropertiesManager?.SetDefaultProperties(dim);
                GetHModel().Entities.Regen();
                GetHModel().Entities.Add(dim);

            }

            EndAction();
            return true;
        }
    }
}
