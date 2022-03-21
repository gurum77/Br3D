using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    static public class KeysHelper
    {
        // 숫자입력인지?
        static public bool IsDigit(this Keys keys)
        {
            if (keys >= Keys.D0 && keys <= Keys.D9)
                return true;
            if (keys == Keys.Oemcomma)
                return true;
            if (keys >= Keys.NumPad0 && keys <= Keys.NumPad9)
                return true;
            if (keys == Keys.OemMinus)
                return true;
            if (keys == Keys.Oemplus)
                return true;

            return false;
        }
    }
}
