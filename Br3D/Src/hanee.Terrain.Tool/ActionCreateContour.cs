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
            selectableType.Add(typeof(Brep), true);
            while (true)
            {
                var ent = await GetEntity(LanguageHelper.Tr("Select a terrain"), -1, false, selectableType);
                if (ent == null)
                    break;
                if (IsCanceled() || IsEntered())
                    break;

                FormCreateContour form = new FormCreateContour(environment as Model);
                if (form.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    break;

                if (ent.BoxMin == null || ent.BoxMax == null)
                    ent.Regen(0.001);
                if (ent.BoxMin == null || ent.BoxMax == null)
                    break;

                var minorHeight = form.textEditMinorHeight.Text.ToDouble();
                var majorHeight = form.textEditMajorHeight.Text.ToDouble();
                var minorLayerName = form.comboBoxEditMinorLayer.SelectedItem.ToString();
                var majorLayerName = form.comboBoxEditMajorLayer.SelectedItem.ToString();
                var min = ent.BoxMin.Z;
                var max = ent.BoxMax.Z;
                var elevations = hanee.Geometry.Util.GetAllChainaInRange(min, max, minorHeight, true);

                var entities = new List<Entity>();
                foreach(var el in elevations)
                {
                    var plane = new Plane(new Point3D(0, 0, el), Vector3D.AxisZ);
                    ICurve[] curves = null;
                    if(ent is Mesh mesh)
                    {
                        curves = mesh.Section(plane, 0.001);
                    }
                    else if(ent is Brep brep)
                    {
                        curves = brep.Section(plane, 0.001);
                    }
                    if (curves == null)
                        continue;

                    foreach(var curve in curves)
                    {
                        var tmpEnt = curve as Entity;
                        if (tmpEnt == null)
                            continue;
                        if (hanee.Geometry.Util.IsTick(el, majorHeight))
                            tmpEnt.LayerName = majorLayerName;
                        else
                            tmpEnt.LayerName = minorLayerName;
                        tmpEnt.ColorMethod = colorMethodType.byLayer;
                        entities.Add(tmpEnt);
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
