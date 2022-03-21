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
        static public void Init()
        {
            fixedX = null;
            fixedY = null;
        }
        static public double? fixedX { get; set; }
        static public double? fixedY { get; set; }
        static FormDynamicInput formDynamicInput;
        static public void ShowDynamicInput()
        {
            if (formDynamicInput == null)
            {
                formDynamicInput = new FormDynamicInput();
                formDynamicInput.TopMost = true;
            }
            if (!formDynamicInput.Visible)
                formDynamicInput.Visible = true;


            var loc = Cursor.Position;
            loc.X += 50;
            formDynamicInput.Location = loc;
            formDynamicInput.UpdateControls();
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
