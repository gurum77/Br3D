using devDept.Geometry;
using hanee.ThreeD;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionID : ActionBase
    {
        public enum ShowResult
        {
            label,
            form
        }

        ShowResult showResult = ShowResult.form;
        public ActionID(devDept.Eyeshot.Model vp, ShowResult showResult=ShowResult.form) : base(vp)
        {
            this.showResult = showResult;
        }
        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();
            var pt = await GetPoint3D(LanguageHelper.Tr("Pick point"));
            if (!IsCanceled())
            {
                if (showResult == ShowResult.form)
                {

                    List<string> results = new List<string>();
                    results.Add($"X = {pt.X:0.0000}   Y = {pt.Y:0.0000}   Z = {pt.Z:0.0000}");

                    FormResult formResult = new FormResult();
                    formResult.RichTextBox.Lines = results.ToArray();
                    formResult.ShowDialog();
                }
                else if (showResult == ShowResult.label)
                {
                    string value;
                    value = LanguageHelper.Tr("Point : ") + pt.ToString();
                    LeaderAndTextAndBox label = new LeaderAndTextAndBox(pt, value, Define.DefaultFont, Define.DefaultTextColor, new Vector2D(60, 60));
                    label.FillColor = Color.GreenYellow;
                    GetModel().ActiveViewport.Labels.Add(label);
                    GetModel().Invalidate();
                }
            }

            EndAction();
            return true;
        }
    }
}
