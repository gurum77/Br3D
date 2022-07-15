using devDept.Eyeshot.Entities;
using DevExpress.XtraEditors;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionRegion : ActionBase
    {
        public ActionRegion(devDept.Eyeshot.Environment environment) : base(environment)
        {

        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            var selectableTypes = new Dictionary<Type, bool>();// { typeof(LinearPath), typeof(Circle), typeof(CompositeCurve) };
            selectableTypes.Add(typeof(LinearPath), true);
            selectableTypes.Add(typeof(Circle), true);
            selectableTypes.Add(typeof(CompositeCurve), true);

            while (true)
            {
                // 외곽선 선택
                var outline = await GetEntity(LanguageHelper.Tr("Select outline"), -1, true, selectableTypes) as ICurve;
                if (IsCanceled() || IsEntered() || outline == null)
                    break;

                // hole 선택
                var holes = await GetEntities(LanguageHelper.Tr("Select holes"), -1, true, selectableTypes);
                if (IsCanceled())
                    break;


                var contours = new List<ICurve>();
                contours.Add(outline);
                if (holes != null)
                {
                    foreach (var hole in holes)
                    {
                        var holeCurve = hole as ICurve;
                        if (holeCurve == null)
                            continue;
                        if (contours.Contains(holeCurve))
                            continue;

                        contours.Add(holeCurve);
                    }
                }

                // region
                try
                {
                    var region = new Region(contours);
                    GetHModel()?.entityPropertiesManager?.SetDefaultProperties(region, false);
                    environment.Entities.Add(region);
                    environment.Entities.Regen();
                    environment.TempEntities.Clear();
                    environment.Invalidate();

                }
                catch (Exception ex)
                {
                    XtraMessageBox.Show(ex.Message);
                }
                
            }

            EndAction();
            return true;
        }
    }
}
