using DevExpress.XtraVerticalGrid.Rows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br3D
{
    static public class CategoryRowHelper
    {
        static public bool ExistVisibleRow(this CategoryRow category)
        {
            foreach (EditorRow row in category.ChildRows)
            {
                if (row.Visible)
                    return true;
            }

            return false;
        }
    }
}
