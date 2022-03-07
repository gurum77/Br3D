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
                Entity ent = await GetEntity("Select a entity", -1, true);
                if (IsCanceled())
                    break;

                List<string> results = new List<string>();
                StringBuilder sb = new StringBuilder();
                if (ent.EntityData is Element)
                {
                    Element ele = ent.EntityData as Element;
                    results.Add($"Element");
                    results.Add($"  Type : {ele.GetType().ToString()}");
                    results.Add($"  ID : {ele.id.id.ToString()}");
                    results.Add($"  Name : {ele.name}");
                    results.Add("");
                }

                results.Add($"General");
                results.Add($"  Type : {ent.ToString()}");
                results.Add($"  Layer : {ent.LayerName}");
                results.Add($"  Line type : {ent.LineTypeName}");

                results.Add($"  Color : {ent.Color.ToString()}");
                results.Add($"  Color Method : {ent.ColorMethod.ToString()}");
                results.Add("");

                if (ent is ICurve)
                {
                    ICurve curve = ent as ICurve;
                    results.Add($"Geometry");
                    results.Add($"  Start point : {curve.StartPoint.ToString()}");
                    results.Add($"  End point : {curve.EndPoint.ToString()}");
                    results.Add($"  Length : {curve.Length().ToString("0.00000")}");
                }
                else if (ent is IFace)
                {
                    IFace face = ent as IFace;
                    Point3D center = new Point3D();
                    double area = face.GetArea(out center);
                    results.Add($"Geometry");
                    results.Add($"  Area : {area.ToString("0.00000")}");
                    results.Add($"  Center point : {center.ToString()}");
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
