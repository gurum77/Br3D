using devDept.Eyeshot.Entities;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionLoft : ActionBase
    {
        public ActionLoft(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            Dictionary<Type, bool> selectableType = new Dictionary<Type, bool>();
            selectableType.Add(typeof(Circle), true);
            selectableType.Add(typeof(LinearPath), true);
            selectableType.Add(typeof(Line), true);
            selectableType.Add(typeof(Ellipse), true);
            selectableType.Add(typeof(EllipticalArc), true);
            selectableType.Add(typeof(Region), true);
            


            while (true)
            {
                List<ICurve> curves = new List<ICurve>();
                while (true)
                {
                    var curve = await GetEntity(LanguageHelper.Tr("Select closed curve"), -1, true, selectableType) as ICurve;
                    if (IsCanceled() || IsEntered())
                        break;
                    if (curve == null)
                        continue;


                    if (!curve.IsClosed)
                        continue;

                    curves.Add(curve);
                }

                if (IsCanceled())
                    break;

                var ent = Brep.Loft(curves.ToArray());
                if (ent != null)
                {
                    GetHModel()?.entityPropertiesManager?.SetDefaultProperties(ent, false);
                    AddEntities(ent);
                }
                break;
            }

            EndAction();
            return true;
        }
    }
}
