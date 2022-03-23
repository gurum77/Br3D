using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        static FormPoint3DDynamicInput formDynamicInput;
        static FormPoint3DDynamicInputByLengthAngle formPoint3DDynamicInputByLengthLength;
        static Point3DType point3DType { get; set; } = Point3DType.xyz;

        static Form GetFormPoint3DDynamicInput()
        {
            if (point3DType == Point3DType.xyz)
            {
                if (formDynamicInput == null)
                {
                    formDynamicInput = new FormPoint3DDynamicInput();
                    formDynamicInput.TopMost = true;
                }
                return formDynamicInput;
            }
            else if (point3DType == Point3DType.lengthAngle)
            {
                if (formPoint3DDynamicInputByLengthLength == null)
                {
                    formPoint3DDynamicInputByLengthLength = new FormPoint3DDynamicInputByLengthAngle();
                    formDynamicInput.TopMost = true;
                }
                return formPoint3DDynamicInputByLengthLength;
            }

            return null;
        }

        static public void ShowDynamicInput()
        {
            var form = GetFormPoint3DDynamicInput();
            if (form == null)
                return;

            IDynamicInput di = form as IDynamicInput;
            if (!form.Visible)
            {
                form.Visible = true;
                
                if(di != null)
                    di.Init();
            }

            var loc = Cursor.Position;
            loc.X += 50;
            form.Location = loc;
            if(di != null)
                di.UpdateControls();
        }

        static public void HideDynamicInput()
        {
            Init();

            if (formDynamicInput == null)
                return;
            if (formDynamicInput.Visible)
                formDynamicInput.Visible = false;
        }

        internal static void OnKeyUp(KeyEventArgs e)
        {
            if (formDynamicInput == null ||
                formDynamicInput.Visible == false)
                return;
            formDynamicInput.Focus();
        }
    }
}
