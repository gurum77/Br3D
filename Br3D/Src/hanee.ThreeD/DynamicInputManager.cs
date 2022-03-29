using devDept.Geometry;
using DevExpress.Utils;
using DevExpress.XtraLayout;
using System;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    public class DynamicInputManager
    {
        public enum Point3DType
        {
            xyz,
            lengthAngle
        }
        
        static public devDept.Eyeshot.Environment environment { get; set; }
        static FormXyzDynamicInput formDynamicInput;
        static FormLengthAngleDynamicInput formPoint3DDynamicInputByLengthLength;
        static public bool disableFlagDynamicInput { get; set; } = false;
        static public Point3DType point3DType { get; set; } = Point3DType.xyz;
        static SvgImageCollection svgImageCollection { get; set; }
        static public System.Drawing.Brush foreBrush = new System.Drawing.SolidBrush(System.Drawing.Color.Black);

        // 이미지 콜렉션
        // 
        static public SvgImageCollection GetSvgImageCollection()
        {
            if(svgImageCollection == null)
            {
                svgImageCollection = new SvgImageCollection();
                svgImageCollection.Add("unlock", "image://svgimages/actions/cleartablestyle.svg");
                svgImageCollection.Add("lock", "image://svgimages/outlook inspired/private.svg");
            }

            return svgImageCollection;
        }

        // lock, unlock 아이콘을 그린다.
        static public void DrawLayoutControl(ref ItemCustomDrawEventArgs e, string title, int imageIdx)
        {
            e.Cache.DrawImage(DynamicInputManager.GetSvgImageCollection().GetImage(imageIdx), new System.Drawing.Point(e.Bounds.X+4, e.Bounds.Y+4));
            e.Cache.DrawString(title, Control.DefaultFont, DynamicInputManager.foreBrush, e.Bounds.X + 20, e.Bounds.Y + 7);
            e.Handled = true;
        }

        // 현재 사용중인 dynamic input form을 리턴
        static public Form GetFormPoint3DDynamicInput()
        {
            if (point3DType == Point3DType.xyz)
            {
                if (formDynamicInput == null)
                {
                    formDynamicInput = new FormXyzDynamicInput();
                    formDynamicInput.TopMost = true;
                }
                return formDynamicInput;
            }
            else if (point3DType == Point3DType.lengthAngle)
            {
                if (formPoint3DDynamicInputByLengthLength == null)
                {
                    formPoint3DDynamicInputByLengthLength = new FormLengthAngleDynamicInput();
                    formDynamicInput.TopMost = true;
                }
                return formPoint3DDynamicInputByLengthLength;
            }

            return null;
        }

        static public void ShowDynamicInput(devDept.Eyeshot.Environment environment)
        {
            DynamicInputManager.environment = environment;
            var form = GetFormPoint3DDynamicInput();
            if (form == null)
                return;

            IDynamicInput di = form as IDynamicInput;

            if (!form.Visible)
            {
                form.Visible = true;

                // 숨김상태에서 처음 보여지게 되면 init을 한다.
                if (di != null)
                    di.Init();
            }

            var loc = Cursor.Position;
            loc.X += 50;
            form.Location = loc;
            if (di != null)
                di.UpdateControls(environment);
        }

        static public void HideDynamicInput()
        {
            var form = GetFormPoint3DDynamicInput();
            if (form == null)
                return;
            if (form.Visible)
                form.Visible = false;
        }

        internal static void OnKeyUp(KeyEventArgs e)
        {
            var form = GetFormPoint3DDynamicInput();
            if (form == null ||
                form.Visible == false)
                return;
            form.Focus();
        }

        internal static void FlagPoint3DType()
        {
            if (disableFlagDynamicInput)
                return;

            HideDynamicInput();
            if (point3DType == Point3DType.xyz)
                point3DType = Point3DType.lengthAngle;
            else
                point3DType = Point3DType.xyz;
        }

        // dynamic input에서 fixed 된거 고려해서 좌표 조정
        internal static void ModifyPoint3D(devDept.Eyeshot.Environment environment, ref Point3D pt)
        {
            var form = GetFormPoint3DDynamicInput();
            var dynamicInput = form as IDynamicInputPoint3D;
            if (dynamicInput == null)
                return;

            dynamicInput.ModifyPoint3D(environment, ref pt);
        }
    }
}
