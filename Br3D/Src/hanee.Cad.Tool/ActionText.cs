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
            environment.TempEntities.Clear();
        }

        protected override void OnMouseMove(devDept.Eyeshot.Environment environment, MouseEventArgs e)
        {
            if (insPoint == null)
            {
                return;
            }

            // 높이선
            var heightLine = new Line(insPoint, heightPoint == null ? point3D : heightPoint);
            heightLine.Color = System.Drawing.Color.Red;
            heightLine.ColorMethod = colorMethodType.byEntity;


            // 방향선
            if (heightPoint != null)
            {
                var dirLine = new Line(insPoint, dirPoint == null ? point3D : dirPoint);
                dirLine.Color = System.Drawing.Color.Blue;
                dirLine.ColorMethod = colorMethodType.byEntity;
                environment.TempEntities.ReplaceEntitiesAndRegen(heightLine, dirLine);
            }
            else
            {
                environment.TempEntities.ReplaceEntitiesAndRegen(heightLine);
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
                insPoint = await GetPoint3D(LanguageHelper.Tr("Insertion point"));
                SetOrthoModeStartPoint(insPoint);
                if (IsCanceled())
                    break;

                // 높이
                DynamicInputManager.ActiveLengthFactor(insPoint, 1, LanguageHelper.Tr("Height"));

                heightPoint = await GetPoint3D(LanguageHelper.Tr("Height"));
                SetOrthoModeStartPoint(insPoint);
                if (IsCanceled())
                    break;

                // 방향
                DynamicInputManager.point3DType = DynamicInputManager.Point3DType.distanceAngle;
                var formDi = DynamicInputManager.GetFormPoint3DDynamicInput() as ControlDistanceAngleDynamicInput;
                if (formDi != null)
                {
                    DynamicInputManager.ShowDynamicInput(environment);
                    formDi.fixedLength = 10000;
                    formDi.controlDynamicInputEdit1.Visible = false;
                }
                dirPoint = await GetPoint3D(LanguageHelper.Tr("Direction"));
                if (IsCanceled())
                    break;
                SetOrthoModeStartPoint(null);

                // text
                FormInputMessage form = new FormInputMessage(LanguageHelper.Tr("Contents"));
                form.RichTextBox.Multiline = multilineText;
                if (form.ShowDialog() == DialogResult.OK)
                {
                    var height = insPoint.DistanceTo(heightPoint);
                    var text = MakeText(insPoint, dirPoint, height, form.RichTextBox, GetWorkplane());
                    environment.Entities.Add(text);
                }
                else
                    break;

                Init();
            }


            EndAction();
            return true;
        }

        static public Text MakeText(Point3D insPoint, Point3D dirPoint, double height, RichTextBox richTextBox, Plane plane = null)
        {
            var textString = richTextBox.Text;
            textString = textString.Replace("\n", System.Environment.NewLine);
            var angle = (dirPoint - insPoint).To2D().ToDir().ToRadian();
            if (plane == null)
            {
                plane = new Plane(insPoint, Vector3D.AxisZ);
                plane.Rotate(angle, Vector3D.AxisZ, insPoint);
            }
            else
            {
                var dirPoint2D = plane.Project(dirPoint);
                dirPoint = plane.PointAt(dirPoint2D);
            }


            var text = new Text(plane, textString, height);
            if (richTextBox.Multiline)
                text = new MultilineText(plane, textString, 10, height, height * 1.2);
            text.Color = System.Drawing.Color.White;
            text.ColorMethod = colorMethodType.byEntity;

            return text;
        }
    }
}
