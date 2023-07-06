using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionRadius : ActionBase
    {
        public ActionRadius(devDept.Eyeshot.Model vp) : base(vp)
        {
        }


        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            var selectableType = new Dictionary<Type, bool>();
            selectableType.Add(typeof(Circle), true);
            selectableType.Add(typeof(Arc), true);

            var edgeItem = await GetEdgeOrEntity(LanguageHelper.Tr("Select a edge or a curve"), -1, selectableType);
            if (IsCanceled() || IsEntered() || (edgeItem.Key == null && edgeItem.Value == null))
            {
                EndAction();
                return true;
            }

            Entity selectedEntity = null;
            if (edgeItem.Key != null && edgeItem.Key.Item is Brep brep && edgeItem.Key.Index > -1 && edgeItem.Key.Index < brep.Edges.Length)
            {
                selectedEntity = brep.Edges[edgeItem.Key.Index].Curve as Entity;
            }
            else if (edgeItem.Value != null)
            {
                selectedEntity = edgeItem.Value;
            }

            if(selectedEntity == null)
            {
                EndAction();
                return true;
            }

            var typeText = "Edge";
            var r = 0.0;
            var centerPoint = new Point3D(0, 0);
            var startPoint = new Point3D(0, 0);
            
            // type Text
            var typeSplit = selectedEntity.GetType().ToString().Split('.');
            if (typeSplit != null && typeSplit.Length > 0)
            {
                typeText = typeSplit[typeSplit.Length - 1];
            }

            // len
            if (selectedEntity is Circle cir)
            {
                r = cir.Radius;
                centerPoint = cir.Center;
                startPoint = cir.StartPoint;
            }
            else if(selectedEntity is Arc arc)
            {
                r = arc.Radius;
                centerPoint = arc.Center;
                startPoint = arc.StartPoint;
            }
            
            var distanceText = ActionDist.CreateDistanceText(model, centerPoint, startPoint);
            if (distanceText != null)
            {
                distanceText.Text = $"{typeText}:R={r:0.000}";
                model.ActiveViewport.Labels.Add(distanceText);
                model.Invalidate();
            }
            
            
            EndAction();
            return true;
        }
    }
}
