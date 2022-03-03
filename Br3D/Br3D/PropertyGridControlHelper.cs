using DevExpress.XtraVerticalGrid;
using DevExpress.XtraVerticalGrid.Rows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br3D
{
    static public class PropertyGridControlHelper
    {
        static public void SetVisibleExistPropertiyOnly(this PropertyGridControl propertyGridControl)
        {
            // field가 있는 경우에만 row 를 표시한다.
            foreach (BaseRow row in propertyGridControl.Rows)
            {
                var category = row as CategoryRow;
                if (category == null)
                    continue;

                foreach (EditorRow inRow in category.ChildRows)
                {
                    if (propertyGridControl.SelectedObject == null)
                        inRow.Visible = false;
                    else
                    {
                        var type = propertyGridControl.SelectedObject.GetType();
                        var prop = type.GetProperty(inRow.Properties.FieldName);
                        if (prop == null)
                            inRow.Visible = false;
                        else
                            inRow.Visible = true;
                    }

                }
            }

            // row가 없는 category는 숨긴다.
            foreach (BaseRow row in propertyGridControl.Rows)
            {
                var category = row as CategoryRow;
                if (category == null)
                    continue;

                category.Visible = category.ExistVisibleRow();
            }

        }
    }
}
