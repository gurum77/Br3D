using devDept.Eyeshot.Labels;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionMemo : ActionBase
    {
        public ActionMemo(devDept.Eyeshot.Model vp) : base(vp)
        {

        }
        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();
            var pt = await GetPoint3D(LanguageHelper.Tr("Pick point"));
            if (!IsCanceled())
            {
                
                
                FormInputMessage form = new FormInputMessage();

                // 만약 선택한 label이 있다면 richtextbox에 내용을 넣어준다.
                int idx = GetModel().GetLabelUnderMouseCursor(CurrentMousePoint);
                if (idx != -1)
                {
                    var label = GetModel().ActiveViewport.Labels[idx];
                    if(label is Memo)
                    {
                        Memo memo = label as Memo;
                        form.RichTextBox.Lines = memo.textLines.Clone() as string[];
                    }
                }

                if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (idx > -1)
                    {
                        var label = GetModel().ActiveViewport.Labels[idx];
                        if (label is Memo)
                        {
                            Memo memo = label as Memo;
                            memo.textLines = form.RichTextBox.Lines.Clone() as string[];
                        }
                    }
                    else
                    {
                        Memo memo = new Memo(pt, hanee.Cad.Tool.Resources.Resource1.caution_label, Color.Red, new devDept.Geometry.Vector2D(10, 10), form.RichTextBox.Lines);
                        GetModel().ActiveViewport.Labels.Add(memo);
                    }
                    
                }
                
            }

            EndAction();
            return true;
        }
    }
}
