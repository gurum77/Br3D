using devDept.Eyeshot;
using devDept.Geometry;
using hanee.Geometry;

namespace hanee.ThreeD
{
    static public class PreviewLabel
    {
        static public void Clear(Model model)
        {
            model.ActiveViewport.Labels.RemoveAll(x => x.LabelData is LabelData && ((LabelData)(x.LabelData)).property == LabelData.Property.preview);
        }
        // distance label 미리보기를 한다.
        static public void PreviewDistanceLabel(Model model, Point3D pt1, Point3D pt2, int idx, bool drawLine=false, string header=null)
        {
            var labels = model.ActiveViewport.Labels.FindAll(x => x is DistanceText);
            DistanceText label = null;
            if (labels == null || labels.Count <= idx)
            {
                label = new DistanceText(model, pt1, pt2, pt1.DistanceTo(pt2).ToString("0.000"), Define.DefaultFont, Define.DefaultTextColor, new Vector2D(0, 0))
                {
                    Alignment = System.Drawing.ContentAlignment.MiddleCenter,
                    FillColor = System.Drawing.Color.Yellow
                };
                label.LabelData = new LabelData() { property = LabelData.Property.preview };
                model.ActiveViewport.Labels.Add(label);
            }
            else
            {
                label = labels[idx] as DistanceText;
            }

            if (label == null)
                return;

            label.drawLine = drawLine;
            label.pt1 = pt1;
            label.pt2 = pt2;
            label.AnchorPoint = (pt1 + pt2) / 2;
            label.Text = label.pt1.DistanceTo(label.pt2).ToString("0.000");
            if (!string.IsNullOrEmpty(header))
                label.Text = header + label.Text;
            label.Regen(model.renderContext, 1);
        }
    }
}
