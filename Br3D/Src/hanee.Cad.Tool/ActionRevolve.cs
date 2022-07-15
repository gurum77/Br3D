using devDept.Eyeshot.Entities;
using hanee.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionRevolve : ActionBase
    {
        public ActionRevolve(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            var selectableType = new Dictionary<Type, bool>();
            selectableType.Add(typeof(LinearPath), true);
            selectableType.Add(typeof(Circle), true);
            selectableType.Add(typeof(Line), true);
            selectableType.Add(typeof(Arc), true);
            selectableType.Add(typeof(Ellipse), true);
            selectableType.Add(typeof(EllipticalArc), true);
            selectableType.Add(typeof(Region), true);
            while (true)
            {
                var section = await GetEntity(LanguageHelper.Tr("Select section entity"), -1, true, selectableType);
                if (IsCanceled())
                    break;

                var pt1 = await GetPoint3D(LanguageHelper.Tr("Axis start point"));
                if (IsCanceled())
                    break;
                var pt2 = await GetPoint3D(LanguageHelper.Tr("Axis end point"));
                if (IsCanceled())
                    break;

                if (section is Region region)
                {
                    var ent = region.RevolveAsBrep(0.0, (360.0).ToRadians(), pt1, pt2);
                    if (ent != null)
                    {
                        GetHModel()?.entityPropertiesManager?.SetDefaultProperties(ent, false);
                        environment.Entities.Add(ent);
                        environment.Entities.Regen();
                        environment.Invalidate();
                    }
                }

                break;
            }

            EndAction();
            return true;
        }
    }
}
