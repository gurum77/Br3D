using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    public class ActionLength : ActionBase
    {
        public ActionLength(devDept.Eyeshot.Environment environment) : base(environment)
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
            selectableType.Add(typeof(Line), true);
            selectableType.Add(typeof(LinearPath), true);
            selectableType.Add(typeof(Ellipse), true);
            selectableType.Add(typeof(EllipticalArc), true);
            selectableType.Add(typeof(Curve), true);
            selectableType.Add(typeof(CompositeCurve), true);

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

            var typeText = "Edge";
            var len = 0.0;
            var midPoint = new Point3D(0, 0);
            if (selectedEntity != null)
            {
                // type Text
                var typeSplit = selectedEntity.GetType().ToString().Split('.');
                if (typeSplit != null && typeSplit.Length > 0)
                {
                    typeText = typeSplit[typeSplit.Length - 1];
                }

                // len
                if (selectedEntity is ICurve curve)
                {
                    len = curve.Length();
                    midPoint = curve.GetMidPoint();
                }
            }


            var text = $"{typeText}:L={len:0.000}";
            LeaderAndTextAndBox label = new LeaderAndTextAndBox(midPoint, text, Define.DefaultFont, Define.DefaultTextColor, new Vector2D(10, 10));
            if (label != null)
            {
                label.FillColor = System.Drawing.Color.RoyalBlue;
                model.ActiveViewport.Labels.Add(label);
                model.Invalidate();
            }


            EndAction();
            return true;
        }
    }
}
