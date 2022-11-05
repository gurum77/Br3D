using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Labels;
using devDept.Geometry;
using hanee.Geometry;
using System;

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

        // angle label 미리보기를 한다.
        public static void PreviewAngleLabel(Model model, Point3D basePoint, Point3D startPoint, Point3D endPoint, int idx, bool drawLine=true)
        {
            if (model == null || basePoint == null || startPoint == null || endPoint == null)
                return;

            var label = GetPreviewLabel(model, idx);
            if (label == null)
            {
                label = new AngleText(model, basePoint, startPoint, endPoint, "", Define.DefaultFont, Define.DefaultTextColor, new Vector2D(0, 0))
                {
                    Alignment = System.Drawing.ContentAlignment.MiddleCenter,
                    FillColor = System.Drawing.Color.Yellow
                };
                label.LabelData = new LabelData() { property = LabelData.Property.preview };
                model.ActiveViewport.Labels.Add(label);
            }

            var angleText = label as AngleText;
            if (angleText == null)
                return;
            angleText.drawLine = drawLine;
            angleText.basePoint = basePoint;
            angleText.startPoint = startPoint; 
            angleText.endPoint = endPoint;
            angleText.Text = "";
            angleText.AnchorPoint = basePoint;
            angleText.Regen(model.renderContext, 1);
        }

        static Label GetPreviewLabel(Model model, int idx)
        {
            var labels = model.ActiveViewport.Labels.FindAll(x => x.LabelData is LabelData &&  ((LabelData)x.LabelData).property.HasFlag(LabelData.Property.preview));
            if(labels != null && labels.Count > idx)
            {
                return labels[idx];
            }

            return null;
        }

        // distance label 미리보기를 한다.
        static public void PreviewDistanceLabel(Model model, Point3D from, Point3D to, int idx, bool drawLine=false, string header=null, Plane plane=null, string customText=null)
        {
            if (model == null || from == null || to == null)
                return;

            var toTmp = plane==null ? to :  plane.Project3D(to);
            var label = GetPreviewLabel(model, idx);
            if (label == null)
            {
                label = new DistanceText(model, from, toTmp, from.DistanceTo(toTmp).ToString("0.000"), Define.DefaultFont, Define.DefaultTextColor, new Vector2D(0, 0))
                {
                    Alignment = System.Drawing.ContentAlignment.MiddleCenter,
                    FillColor = System.Drawing.Color.Yellow
                };
                label.LabelData = new LabelData() { property = LabelData.Property.preview };
                model.ActiveViewport.Labels.Add(label);
            }

            var distanceText = label as DistanceText;
            if (distanceText == null)
                return;
            distanceText.drawLine = drawLine;
            distanceText.pt1 = from;
            distanceText.pt2 = toTmp;
            distanceText.AnchorPoint = (from + toTmp) / 2;
            if (customText == null)
            {
                distanceText.Text = distanceText.pt1.DistanceTo(distanceText.pt2).ToString("0.000");
                if (!string.IsNullOrEmpty(header))
                    distanceText.Text = header + distanceText.Text;
            }
            else
                distanceText.Text = customText;

            distanceText.Regen(model.renderContext, 1);
        }

     
    }
}
