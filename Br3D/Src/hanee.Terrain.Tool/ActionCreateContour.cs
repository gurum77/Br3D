using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Terrain.Tool
{
    public class ActionCreateContour : ActionBase
    {
        public ActionCreateContour(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }
        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            var selectableType = new Dictionary<Type, bool>();
            selectableType.Add(typeof(Mesh), true);
            while(true)
            {
                var mesh = await GetEntity(LanguageHelper.Tr("Select a terrain"), -1, false, selectableType) as Mesh;
                if (mesh == null)
                    break;
                if (IsCanceled() || IsEntered())
                    break;

                FormCreateContour form = new FormCreateContour(environment as Model);
                if (form.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    break;

                if (mesh.BoxMin == null || mesh.BoxMax == null)
                    mesh.Regen(0.001);
                if (mesh.BoxMin == null || mesh.BoxMax == null)
                    break;
                   
                var min = mesh.BoxMin.Z;
                var max = mesh.BoxMax.Z;
                var elevations = hanee.Geometry.Util.GetAllChainaInRange(min, max, form.textEditMinorHeight.ToDouble(), true);

                var entities = new List<Entity>();
                foreach(var el in elevations)
                {
                    var plane = Plane.XY;
                    plane.Origin.Z = el;
                    var curves = mesh.Section(plane, 0.001);
                    if (curves == null)
                        continue;

                    foreach(var curve in curves)
                    {
                        var ent = curve as Entity;
                        if (ent == null)
                            continue;
                        entities.Add(ent);
                    }
                }

                // dwg로 내보내기를 한다.
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = HModel.FilterForSaveDialog();
                dlg.DefaultExt = "dwg";
                if(dlg.ShowDialog() == DialogResult.OK && environment is Model model)
                {
                    var wf = FileHelper.GetWriteFileAsync(dlg.FileName, false, entities, model.Layers, model.Blocks, model.Materials, model.TextStyles, model.LineTypes, devDept.Serialization.contentType.Geometry,
                        devDept.Serialization.serializationType.Compressed, linearUnitsType.Meters, false, null, model.HatchPatterns);
                    if(wf != null)
                    {
                        wf.DoWork();
                        MessageBox.Show(LanguageHelper.Tr("Completed"));
                    }
                }


                break;
            }
                

            EndAction();
            return true;
        }
    }
}
