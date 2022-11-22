using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using hanee.ThreeD;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionInsertImage : ActionBase
    {
        Image image = null;
        double scale = 1;
        public ActionInsertImage(devDept.Eyeshot.Environment environment) : base(environment)
        {
        }

        public async override void Run()
        { await RunAsync(); }

        protected override void OnMouseMove(devDept.Eyeshot.Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);

            if (image == null)
                return;

            if (point3D == null)
                return;


            var plane = GetWorkplane();
            var pt1 = point3D.Clone() as Point3D;
            var pt2 = pt1 + plane.AxisX * image.Width * scale;// new Point3D(pt1.X + image.Width * scale, pt1.Y);
            var pt3 = pt2 + plane.AxisY * image.Height * scale;// new Point3D(pt1.X + image.Width * scale, pt1.Y + image.Height * scale);
            var pt4 = pt1 + plane.AxisY * image.Height * scale;// new Point3D(pt1.X, pt1.Y + image.Height * scale);
            var pt5 = pt1.Clone() as Point3D;
            var lp = new LinearPath(pt1, pt2, pt3, pt4, pt5);

            environment.TempEntities.Clear();

            var entities = new List<Entity>();
            entities.Add(lp);
            entities.ToTempEntities(environment, false);
            environment.TempEntities.RegenAfterModify();
        }
        public async Task<bool> RunAsync()
        {
            StartAction();

            while (true)
            {
                var dlg = new OpenFileDialog();

                dlg.Filter = FileHelper.RasterImageFilterForOpenDialog();
                dlg.FilterIndex = 0;
                dlg.AddExtension = true;
                dlg.CheckFileExists = true;
                dlg.CheckPathExists = true;
                if (dlg.ShowDialog() != DialogResult.OK)
                    break;

                image = Image.FromFile(dlg.FileName);
                if (image == null)
                {
                    MessageBox.Show(LanguageHelper.Tr("Not supported image."));
                    break;
                }

                Point3D insertionPoint = null;
                while (true)
                {
                    var pt = await GetPoint3DOrText(LanguageHelper.Tr("Insertion point([S] Scale)"));
                    if (IsCanceled())
                        break;
                    if (IsEntered())
                        break;

                    if (pt.Key != null)
                    {
                        insertionPoint = pt.Key;
                        break;
                    }

                    var form = new FormInputMessage(LanguageHelper.Tr("Scale"));
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        scale = form.RichTextBox.Text.ToString().ToDouble();
                        if (scale <= 0)
                            scale = 1;
                    }


                    if (insertionPoint != null)
                        break;
                }

                if (insertionPoint == null)
                    break;
                var plane = GetWorkplane();
                plane.Origin = insertionPoint;
                var pic = new Picture(plane, image.Width * scale, image.Height * scale, image);
                if (pic != null)
                {
                    AddEntities(pic);
                }

                break;
            }

            EndAction();

            return true;
        }
    }
}
