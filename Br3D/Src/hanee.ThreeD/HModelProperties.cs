using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using devDept.Eyeshot;
using devDept.Geometry;
using devDept.Graphics;

namespace hanee.ThreeD
{
    // Model 를 프로퍼티 그리드에 필요한 것만 노출하기 위한 클래스
    public class HModelProperties
    {
        HModel vp = null;
        public HModelProperties(HModel vp)
        {
            this.vp = vp;
        }

        //[Category("Option")]
        //public linearUnitsType Units { get { return vp.CurrentBlock?.Units; } set { vp.CurrentBlock?.Units = value; } }
        [Category("Option")]
        public ShortcutKeysSettings ShortcutKeys { get { return vp.ShortcutKeys; } set { vp.ShortcutKeys = value; } }
        [Category("Option")]
        public MaterialKeyedCollection Materials { get { return vp.Materials; } set { vp.Materials = value; } }
        //[Category("Performance")]
        //public bool HideSmall { get { return vp.HideSmall; } set { vp.HideSmall = value; } }
        //[Category("Performance")]
        //public int MinimumFramerate { get { return vp.MinimumFramerate; } set { vp.MinimumFramerate = value; } }
        [Category("Performance")]
        public int MaxPatternRepetitions { get { return vp.MaxPatternRepetitions; } set { vp.MaxPatternRepetitions = value; } }
        //[Category("Performance")]
        //public int SmallSize { get { return vp.SmallSize; } set { vp.SmallSize = value; } }

        // lighting
        [Category("Lighting")]
        public Color AmbientLight { get { return vp.AmbientLight; } set { vp.AmbientLight = value; } }

        [Category("Lighting")]
        public LightSettings Light1 { get { return vp.Light1; } set { vp.Light1 = value; } }
        [Category("Lighting")]
        public LightSettings Light2 { get { return vp.Light2; } set { vp.Light2 = value; } }
        [Category("Lighting")]
        public LightSettings Light3 { get { return vp.Light3; } set { vp.Light3 = value; } }

        // display setting
        [Category("Display Settings")]
        public Color DefaultColor { get { return vp.DefaultColor; } set { vp.DefaultColor = value; } }
        [Category("Display Settings")]
        public DisplayModeSettings Wireframe { get { return vp.Wireframe; } set { vp.Wireframe = value; } }
        [Category("Display Settings")]
        public HiddenLinesSettings HiddenLines { get { return vp.HiddenLines; } set { vp.HiddenLines = value; } }
        [Category("Display Settings")]
        public DisplayModeSettingsFlat Flat { get { return vp.Flat; } set { vp.Flat = value; } }
        [Category("Display Settings")]
        public DisplayModeSettingsShaded Shaded { get { return vp.Shaded; } set { vp.Shaded = value; } }
        [Category("Display Settings")]
        public DisplayModeSettingsRendered Rendered { get { return vp.Rendered; } set { vp.Rendered = value; } }

        [Category("Selection")]
        public Color SelectionColor { get { return vp.SelectionColor; } set { vp.SelectionColor = value; } }
        [Category("Selection")]
        public Color SelectionColorDynamic { get { return vp.SelectionColorDynamic; } set { vp.SelectionColorDynamic = value; } }
        [Category("Selection")]
        public float SelectionLineWeightScaleFactor { get { return vp.SelectionLineWeightScaleFactor; } set { vp.SelectionLineWeightScaleFactor = value; } }
        [Category("Selection")]
        public int PickBoxSize { get { return vp.PickBoxSize; } set { vp.PickBoxSize = value; } }

        // top view만 지원하는지?(2D view 용)
        [Browsable(false)]
        public bool TopViewOnly
        { get; set; }

        [Category("Object manipulator")]
        public ObjectManipulator ObjectManipulator { get { return vp.ObjectManipulator; } set { vp.ObjectManipulator = value; } } 


    }
}
