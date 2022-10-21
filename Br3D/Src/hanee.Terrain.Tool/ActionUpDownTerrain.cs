using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using static devDept.Eyeshot.Environment;

namespace hanee.Terrain.Tool
{
    public class ActionUpDownTerrain : ActionBase
    {
        Mesh mesh = null;
        FormUpDownTerrainOptions form = new FormUpDownTerrainOptions();
        double radius => form.spinEditRadius.Value.ToDouble();
        double height => form.spinEditHeight.Value.ToDouble();
        bool isUp
        {
            get => form.checkEditUpDown.Checked;
            set => form.checkEditUpDown.Checked = value;
        }

        bool isUpdateColor
        {
            get => form.checkEditAutoUpdateColor.Checked;
            set => form.checkEditAutoUpdateColor.Checked = value;
        }
        
        

        public ActionUpDownTerrain(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        // 
        bool IsValidMousePosition()
        {
            if (mesh == null)
                return false;
            var items = environment.GetAllItemsUnderMouseCursor(currentMousePoint);
            if (items == null)
                return false;

            foreach(SelectedItem item in items)
            {
                if (item.Item == mesh)
                    return true;
            }

            return false;
        }
        protected override void OnMouseMove(devDept.Eyeshot.Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);

            if (!IsValidMousePosition())
            {
                environment.TempEntities.Clear();
                return;
            }

            var plane = new Plane();
            plane.Origin = point3D;

            var circle = new Circle(plane, radius);
            circle.LineWeight = 2;
            circle.LineWeightMethod = colorMethodType.byEntity;
            circle.Color = System.Drawing.Color.Red;
            circle.ColorMethod = colorMethodType.byEntity;
            environment.TempEntities.ReplaceEntityAndRegen(circle);
        }

        public override void EndAction()
        {
            base.EndAction();
            form.Close();
        }
        public async Task<bool> RunAsync()
        {
            StartAction();

            mesh = null;

            // mesh를 선택
            var selectableTypes = new Dictionary<Type, bool>();
            selectableTypes.Add(typeof(Mesh), true);
            mesh = await GetEntity(LanguageHelper.Tr("Select a mesh"), -1, false, selectableTypes) as Mesh;
            if(IsCanceled() || IsEntered() || mesh == null)
            {
                EndAction();
                return true;
            }

            form.TopMost = true;
            form.Show();

            mesh.Selected = false;
            while(true)
            {
                var point = await GetPoint3D(LanguageHelper.Tr("Click to up/down"));
                if (IsCanceled() || IsEntered())
                    break;
                if(IsValidMousePosition())
                {
                    // 클릭한 위치의 mesh를 높인다.
                    foreach (var v in mesh.Vertices)
                    {
                        var dist = point.To2D().DistanceTo(v.To2D());
                        if (dist > radius)
                            continue;

                        var curHeight = (radius - dist) / radius * height;
                        v.Z += isUp ? curHeight : -curHeight;
                    }

                    if(isUpdateColor)
                        ActionColoringTerrain.SetColor(environment as Model, mesh);
                    else
                        mesh.Regen(0.001);
                    environment.Entities.Regen(null);
                    environment.Invalidate();
                }
            }

            EndAction();
            return true;
        }
    }
}
