using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
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

        // height label 미리보기를 한다.
        // centerPoint에서 평면에 z 축 방향으로 떨어진 거리까지 치수를 미리보기 한다.
        static public void PreviewHeightLabel(Model model, Point3D from, double height, int idx, Plane plane, bool drawLine = false, string header = null)
        {
            var to = from + plane.AxisZ * height;
            PreviewDistanceLabel(model, from, to, idx, drawLine, header);
        }

        // distance label 미리보기를 한다.
        static public void PreviewDistanceLabel(Model model, Point3D from, Point3D to, int idx, bool drawLine=false, string header=null, Plane plane=null)
        {
            if (model == null || from == null || to == null)
                return;

            var toTmp = plane==null ? to :  plane.Project3D(to);
            var labels = model.ActiveViewport.Labels.FindAll(x => x is DistanceText);
            DistanceText label = null;
            if (labels == null || labels.Count <= idx)
            {
                label = new DistanceText(model, from, toTmp, from.DistanceTo(toTmp).ToString("0.000"), Define.DefaultFont, Define.DefaultTextColor, new Vector2D(0, 0))
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
            label.pt1 = from;
            label.pt2 = toTmp;
            label.AnchorPoint = (from + toTmp) / 2;
            label.Text = label.pt1.DistanceTo(label.pt2).ToString("0.000");
            if (!string.IsNullOrEmpty(header))
                label.Text = header + label.Text;
            label.Regen(model.renderContext, 1);
        }
    }
}
