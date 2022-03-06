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
        static public List<EditorRow> GetAllEditorRows(this PropertyGridControl propertyGridControl)
        {
            List<EditorRow> rows = new List<EditorRow>();
            foreach (BaseRow row in propertyGridControl.Rows)
            {
                var category = row as CategoryRow;
                if (category == null)
                    continue;

                foreach (BaseRow inRow in category.ChildRows)
                {
                    if (inRow is EditorRow)
                    {
                        rows.Add(inRow as EditorRow);
                    }
                }
            }
            return rows;
        }

        static public void SetVisibleExistPropertiyOnly(this PropertyGridControl propertyGridControl)
        {
            // field가 있는 경우에만 row 를 표시한다.
            var editorRows = propertyGridControl.GetAllEditorRows();
            foreach (var row in editorRows)
            {
                if (propertyGridControl.SelectedObject == null)
                {
                    row.Visible = false;
                    continue;
                }

                // property가 있는지 체크
                var type = propertyGridControl.SelectedObject.GetType();
                var prop = type.GetProperty(row.Properties.FieldName);
                // property가 없으면 숨김
                if (prop == null)
                {
                    row.Visible = false;
                    continue;
                }

                // property가 있어도 활성화 되어 있는지?
                var enableProp = type.GetProperty($"enable{row.Properties.FieldName}");
                
                // 활성화 함수가 있는데 false이면 숨김
                if (enableProp != null && !(bool)enableProp.GetValue(propertyGridControl.SelectedObject))
                {
                    row.Visible = false;
                    continue;
                }

                row.Visible = true;
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
