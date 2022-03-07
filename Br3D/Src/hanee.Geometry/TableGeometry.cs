using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using System.Drawing;

namespace hanee.Geometry
{
    public class Cell
    {
        public Cell(string text, int columnCount)
        {
            this.text = text;
            this.columnCount = columnCount;
        }
        public string text { get; set; } = "";
        public int columnCount { get; set; } = 1;

    }
    public class TableGeometry
    {

        double rowHeight { get; set; }
        double colWidth { get; set; }
        public TableGeometry(double rowHeight, double colWidth)
        {
            this.rowHeight = rowHeight;
            this.colWidth = colWidth;
        }

        // 한줄짜리 row를 그린다.
        public EntityList CalcRow(int row, params Cell[] contents)
        {
            EntityList entities = new EntityList();

            int col = 0;
            foreach (var c in contents)
            {
                var cell = CalcCell(row, row, col, col + c.columnCount - 1, c.text);
                col = col + c.columnCount;
                if (cell == null)
                    continue;

                entities.AddRange(cell);
            }

            return entities;
        }

        // table cell 1개와 안에 text를 그린다.
        public EntityList CalcCell(int startRow, int endRow, int startCol, int endCol, string text)
        {
            double w = colWidth * (endCol - startCol + 1);
            double h = rowHeight * (endRow - startRow + 1);

            double x = colWidth * startCol + w / 2;
            double y = -(rowHeight * startRow) - h / 2;
            var box = hanee.Geometry.LinearPathHelper.CreateRectangle(x, y, 0, w, h, true);
            var content = new Text(new Point3D(x, y, 0), text, rowHeight / 3);
            content.Alignment = Text.alignmentType.MiddleCenter;
            EntityList entities = new EntityList();
            entities.Add(box);
            entities.Add(content);

            foreach (var ent in entities)
            {
                ent.Color = Color.White;
                ent.ColorMethod = colorMethodType.byEntity;
            }
            return entities;
        }
    }
}
