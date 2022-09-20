using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionList : ActionBase
    {
        public ActionList(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public async override void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            while (true)
            {
                Entity ent = await GetEntity(LanguageHelper.Tr("Select a entity"), -1, true);
                if (IsCanceled())
                    break;

                List<string> results = new List<string>();
                StringBuilder sb = new StringBuilder();
                if (ent.EntityData is Element)
                {
                    Element ele = ent.EntityData as Element;
                    results.Add(LanguageHelper.Tr("Element"));
                    results.Add("  " + LanguageHelper.Tr("Type") + $" : {ele.GetType().Name}");
                    results.Add("  " + LanguageHelper.Tr("ID") + $" : {ele.id.id.ToString()}");
                    results.Add("  " + LanguageHelper.Tr("Name") + $" : {ele.name}");
                    results.Add("");
                }

                results.Add(LanguageHelper.Tr("General"));
                results.Add("  " + LanguageHelper.Tr("Type") + $" : {ent.GetType().Name}");
                results.Add("  " + LanguageHelper.Tr("Layer") + $" : {ent.LayerName}");
                results.Add("  " + LanguageHelper.Tr("Line type") + $" : {ent.LineTypeName}");

                results.Add("  " + LanguageHelper.Tr("Color") + $" : {ent.Color.ToString()}");
                results.Add("  " + LanguageHelper.Tr("Color Method") + $" : {ent.ColorMethod.ToString()}");
                results.Add("");

                results.Add(LanguageHelper.Tr("Geometry"));
                if (ent is Brep brep)
                {
                    var area = brep.GetArea(out Point3D center);
                    var volume = brep.GetVolume(out center);
                    
                    results.Add("  " + LanguageHelper.Tr("Area") + $" : {area.ToString("0.00000")}");
                    results.Add("  " + LanguageHelper.Tr("Volume") + $" : {volume.ToString("0.00000")}");
                    results.Add("  " + LanguageHelper.Tr("Center point") + $" : {center.ToString()}");
                    results.Add("  " + LanguageHelper.Tr("Closed") + $" : {brep.IsClosed}");

                }
                else if (ent is ICurve curve)
                {
                    results.Add("  " + LanguageHelper.Tr("Start point") + $" : {curve.StartPoint.ToString()}");
                    results.Add("  " + LanguageHelper.Tr("End point") + $" : {curve.EndPoint.ToString()}");
                    results.Add("  " + LanguageHelper.Tr("Length") + $" : {curve.Length().ToString("0.00000")}");
                }
                else if (ent is IFace face)
                {
                    Point3D center = new Point3D();
                    double area = face.GetArea(out center);
                    results.Add("  " + LanguageHelper.Tr("Area") + $" : {area.ToString("0.00000")}");
                    results.Add("  " + LanguageHelper.Tr("Center point") + $" : {center.ToString()}");
                }
                else if (ent is Point point)
                {
                    results.Add("  " + LanguageHelper.Tr("Point") + $" : {point.ToString()}");
                }
                else if(ent is BlockReference br)
                {
                    results.Add("  " + LanguageHelper.Tr("Block Name") + $" : {br.BlockName}");
                    results.Add("  " + LanguageHelper.Tr("Insertion Point") + $" : {br.InsertionPoint.ToString()}");
                    results.Add("  " + LanguageHelper.Tr("Scale X") + $" : {br.GetScaleFactorX().ToString("0.000")}");
                    results.Add("  " + LanguageHelper.Tr("Scale Y") + $" : {br.GetScaleFactorY().ToString("0.000")}");
                    results.Add("  " + LanguageHelper.Tr("Scale Z") + $" : {br.GetScaleFactorZ().ToString("0.000")}");
                }
                else
                {
                    System.Diagnostics.Debug.Assert(false);
                }

                FormResult formResult = new FormResult();
                formResult.RichTextBox.Lines = results.ToArray();
                formResult.ShowDialog();
            }

            EndAction();

            return true;
        }
    }
}
