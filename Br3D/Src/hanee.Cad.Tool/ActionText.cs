using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionText : ActionBase
    {
        public bool multilineText { get; set; } = false;

        Point3D insPoint = null;
        Point3D heightPoint = null;
        Point3D dirPoint = null;

        void Init()
        {
            insPoint = null;
            heightPoint = null;
            dirPoint = null;
        }

        protected override void OnMouseMove(devDept.Eyeshot.Environment environment, MouseEventArgs e)
        {
            environment.TempEntities.Clear();
            if (insPoint == null)
            {
                return;
            }

            // 높이선
            var heightLine = new Line(insPoint, heightPoint == null ? point3D : heightPoint);
            heightLine.Color = System.Drawing.Color.Red;
            heightLine.ColorMethod = colorMethodType.byEntity;
            environment.TempEntities.Add(heightLine);

            // 방향선
            if (heightPoint != null)
            {

                var dirLine = new Line(insPoint, dirPoint == null ? point3D : dirPoint);
                dirLine.Color = System.Drawing.Color.Blue;
                dirLine.ColorMethod = colorMethodType.byEntity;
                environment.TempEntities.Add(dirLine);
            }
        }

        public ActionText(devDept.Eyeshot.Environment environment) : base(environment)
        {

        }

        public override async void Run()
        { await RunAsync(); }

        public async Task<bool> RunAsync()
        {
            StartAction();

            while (true)
            {

                // 삽입점
                insPoint = await GetPoint3D("Insertion point");

                // 높이
                heightPoint = await GetPoint3D("Height");

                // 방향
                dirPoint = await GetPoint3D("Direction");

                // text
                FormInputMessage form = new FormInputMessage("Contents");
                form.RichTextBox.Multiline = multilineText;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var height = insPoint.DistanceTo(heightPoint);
                    var textString = form.RichTextBox.Text;
                    textString = textString.Replace("\n", System.Environment.NewLine);
                    var angle = (dirPoint - insPoint).To2D().ToDir().ToRadian();
                    var plane = new Plane(insPoint, Vector3D.AxisZ);
                    plane.Rotate(angle, Vector3D.AxisZ, insPoint);

                    var text = new Text(plane, textString, height);
                    if(form.RichTextBox.Multiline)
                        text = new MultilineText(plane, textString, 10, height, height * 1.2);
                    text.Color = System.Drawing.Color.White;
                    text.ColorMethod = colorMethodType.byEntity;
                    environment.Entities.Add(text);

                                    }
                else
                    break;

                Init();
            }


            EndAction();
            return true;
        }

    }
}
