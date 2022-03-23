using devDept.Geometry;
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
        static public void Init()
        {
            fixedX = null;
            fixedY = null;
            fixedZ = null;
        }
        static public double? fixedX { get; set; }
        static public double? fixedY { get; set; }
        static public double? fixedZ { get; set; }

        static public double? fixedLength { get; set; }
        static public double? fixedAngle { get; set; }
        static FormXyzDynamicInput formDynamicInput;
        static FormLengthAngleDynamicInput formPoint3DDynamicInputByLengthLength;

        static public Point3DType point3DType { get; set; } = Point3DType.xyz;

        // 현재 사용중인 dynamic input form을 리턴
        static Form GetFormPoint3DDynamicInput()
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
            Init();

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
            HideDynamicInput();
            if (DynamicInputManager.point3DType == Point3DType.xyz)
                DynamicInputManager.point3DType = DynamicInputManager.Point3DType.lengthAngle;
            else
                DynamicInputManager.point3DType = DynamicInputManager.Point3DType.xyz;
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
