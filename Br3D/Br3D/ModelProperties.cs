using devDept.Eyeshot;
using devDept.Graphics;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br3D
{
    class ModelProperties
    {
        public Model model { get; }
        public ModelProperties(Model hModel)
        {
            model = hModel;
            
        }

        public bool showEdges
        {
            get => model.Rendered.ShowEdges;
            set => model.Rendered.ShowEdges = value;
        }

        public float edgeThickness
        {
            get => model.Rendered.EdgeThickness;
            set => model.Rendered.EdgeThickness = value;
        }

        public bool showInteralWires
        {
            get => model.Rendered.ShowInternalWires;
            set => model.Rendered.ShowInternalWires = value;
        }

        public silhouettesDrawingType silhouettesDrawing
        {
            get => model.Rendered.SilhouettesDrawingMode;
            set => model.Rendered.SilhouettesDrawingMode = value;
        }

        public float silhouetteThickness
        {
            get => model.Rendered.SilhouetteThickness;
            set => model.Rendered.SilhouetteThickness = value;
        }

        public bool planarReflections
        {
            get => model.Rendered.PlanarReflections;
            set => model.Rendered.PlanarReflections = value;
        }

        public float planarReflectionsIntensity
        {
            get => model.Rendered.PlanarReflectionsIntensity;
            set => model.Rendered.PlanarReflectionsIntensity = value;
        }

        public realisticShadowQualityType realisticShadowQuality
        {
            get => model.Rendered.RealisticShadowQuality;
            set => model.Rendered.RealisticShadowQuality = value;
        }


    }
}
