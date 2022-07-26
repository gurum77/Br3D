using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionSmartExtrude : ActionBase
    {
        protected Plane oldPlane;
        Entity selectedEntity;
        public ActionSmartExtrude(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        Plane GetSelectedEntityPlane()
        {
            if (selectedEntity is Region region)
                return region.Plane;
            else if (selectedEntity is Circle circle)
                return circle.Plane;
            else if (selectedEntity is Arc arc)
                return arc.Plane;
            else if (selectedEntity is Ellipse el)
                return el.Plane;
            else if (selectedEntity is EllipticalArc elArc)
                return elArc.Plane;

            return GetWorkplane();
        }

        Entity Make3D(Point3D pt, bool tempEntity)
        {
            if (selectedEntity == null || pt == null) 
                return null;

            var plane = GetSelectedEntityPlane();
            if (plane == null)
                return null;

            plane.Project(pt, out double s, out double t);

            var amount = plane.DistanceTo(pt) * plane.AxisZ;
            if (amount.Length == 0)
                return null;



            Entity ent = null;
            if(selectedEntity is Region region)
                ent = tempEntity ? region.ExtrudeAsMesh(amount, 0.1, Mesh.natureType.Plain) as Entity : region.ExtrudeAsBrep(amount) as Entity;
            else if(selectedEntity is ICurve curve)
                ent = tempEntity ? curve.ExtrudeAsMesh(amount, 0.1, Mesh.natureType.Plain) as Entity : curve.ExtrudeAsBrep(amount) as Entity;
            if (ent == null)
                return null;

            GetHModel()?.entityPropertiesManager?.SetDefaultProperties(ent, tempEntity);
            return ent;
        }
        protected override void OnMouseMove(devDept.Eyeshot.Environment environment, MouseEventArgs e)
        {
            var ent = Make3D(point3D, true);
            environment.TempEntities.Clear();
            if (ent == null)
                return;

            environment.TempEntities.Add(ent);
        }
        public async Task<bool> RunAsync()
        {
            StartAction();

            var selectableTypes = new Dictionary<Type, bool>();
            selectableTypes.Add(typeof(Region), true);
            selectableTypes.Add(typeof(Circle), true);
            selectableTypes.Add(typeof(LinearPath), true);
            selectableTypes.Add(typeof(Ellipse), true);
            selectableTypes.Add(typeof(EllipticalArc), true);
            selectableTypes.Add(typeof(Arc), true);
            selectableTypes.Add(typeof(Line), true);
            selectableTypes.Add(typeof(Curve), true);

            var ws = GetWorkspace();
            while (true)
            {
                selectedEntity = null;
                selectedEntity = await GetEntity(LanguageHelper.Tr("Select region"), -1, true, selectableTypes);
                if (selectedEntity == null)
                    break;
                if (IsCanceled())
                    break;

                var oldEnable = ws.enabled;
                oldPlane = ws.plane;
                ws.enabled = false;

                var plane = GetSelectedEntityPlane();
                DynamicInputManager.ActiveLengthFactor(plane.Origin, 1, LanguageHelper.Tr("Height"), plane.AxisZ);
                var pt = await GetPoint3D(LanguageHelper.Tr("Height"));
                DynamicInputManager.Init();

                ws.enabled = oldEnable;

                if (pt == null)
                    break;
                if (IsCanceled())
                    break;

                var ent = Make3D(pt, false);
                if (ent == null)
                    break;

                environment.Entities.Add(ent);
                environment.Entities.Regen(new devDept.Eyeshot.RegenOptions());
                environment.Invalidate();
                break;
            }

            EndAction();
            return true;
        }
    }
}
