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
                    radius = values[0].value;
                else
                {
                    values = script.ToDoubles('d');
                    if (values != null && values.Count > 0)
                        radius = values[0].value/2;
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
            if (cmd == Command.createCircle)
            {
                var center = points.Count > 0 ? points[0].Clone() as Point3D : null;
                if (center == null)
                    return false;

                var c = new Circle(center, radius);
                model.Entities.Add(c);
            }
            else if (cmd == Command.createLine)
            {
                var pt1 = points.Count > 0 ? points[0].Clone() as Point3D : null;
                var pt2 = points.Count > 1 ? points[1].Clone() as Point3D : null;
                if(pt1 == null || pt2 == null)
                    return false;

                var l = new Line(pt1, pt2);
                model.Entities.Add(l);
            }
            else if (cmd == Command.createPline)
            {
                if (points.Count < 2)
                    return false;

                var lpPoints = new List<Point3D>();
                foreach (var p in points)
                    lpPoints.Add(p.Clone() as Point3D);
                var lp = new LinearPath(lpPoints.ToArray());
                model.Entities.Add(lp);
            }
            else
            {
                new NotImplementedException();
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
        public List<Point3D> points { get; set; }
    }
}
