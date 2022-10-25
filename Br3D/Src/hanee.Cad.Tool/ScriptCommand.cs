using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.Cad.Tool
{
    // script command 아이템
    public class ScriptCommand
    {
        public enum Command
        {
            [Description("Unknown")]
            unknown,
            [Description("Line")]
            createLine,
            [Description("Polyline")]
            createPline,
            [Description("Circle")]
            createCircle
        }
        public ScriptCommand(Command cmd)
        {
            this.cmd = cmd;
            if (cmd == Command.createCircle)
                radius = 1;
        }

        public ScriptCommand(string script)
        {
            
            var tmp = script.ToLower();
            if (Contains(tmp, "create", "line"))
            {
                cmd = Command.createLine;
                points = script.ToPoint3Ds();
            }
            else if (Contains(tmp, "create", "polyline"))
            {
                cmd = Command.createPline;
                points = script.ToPoint3Ds();
            }
            else if (Contains(tmp, "create", "circle"))
            {
                cmd = Command.createCircle;
                points = script.ToPoint3Ds();
                var values = script.ToDoubles('r');
                if (values != null && values.Count > 0)
                    radius = values[0].GetDouble();
                else
                {
                    values = script.ToDoubles('d');
                    if (values != null && values.Count > 0)
                        radius = values[0].GetDouble()/2;
                }
            }
            else
            {
                cmd = Command.unknown;
            }

        }

        internal bool Run(Model model)
        {
            if (cmd == Command.unknown)
                return false;


            Entity newEntity = null;
            if (cmd == Command.createCircle)
            {
                var center = points.Count > 0 ? points[0].Clone() as Point3D : null;
                if (center == null)
                    return false;

                newEntity = new Circle(center, radius);
            }
            else if (cmd == Command.createLine)
            {
                var pt1 = points.Count > 0 ? points[0].Clone() as Point3D : null;
                var pt2 = points.Count > 1 ? points[1].Clone() as Point3D : null;
                if(pt1 == null || pt2 == null)
                    return false;

                newEntity = new Line(pt1, pt2);
            }
            else if (cmd == Command.createPline)
            {
                if (points.Count < 2)
                    return false;

                var lpPoints = new List<Point3D>();
                foreach (var p in points)
                    lpPoints.Add(p.Clone() as Point3D);
                newEntity = new LinearPath(lpPoints.ToArray());
            }
            else
            {
                new NotImplementedException();
            }

            if (model is HModel hModel)
                hModel.entityPropertiesManager.SetDefaultProperties(newEntity);

            newEntity.LineWeight = width;
            newEntity.LineWeightMethod = colorMethodType.byEntity;

            if(color != null && color.Value != System.Drawing.Color.Empty)
            {
                newEntity.Color = color.Value;
                newEntity.ColorMethod = colorMethodType.byEntity;
            }

            if(newEntity != null)
            {
                model.Entities.ClearSelection();
                model.Entities.Add(newEntity);
                newEntity.Selected = true;
            }

            return true;
        }

        public override string ToString()
        {
            return cmd.GetDescription();
            //return cmd.ToString();
        }
        private bool Contains(string tmp, params  string[] tokens)
        {

            foreach (var t in tokens)
            {
                if (!tmp.Contains(t))
                    return false;
            }

            return true;
        }

        [Browsable(false)]
        public Command cmd { get; set; }
        public double radius { get; set; }
        public float width { get; set; } = 0.5f;
        public System.Drawing.Color ?color { get; set; }
        public Point3D startPoint => points != null ? points.First() : null;
        public Point3D endPoint => points != null ? points.Last() : null;
            
        public List<Point3D> points { get; set; }
    }
}
