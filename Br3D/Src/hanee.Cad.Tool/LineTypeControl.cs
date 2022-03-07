using devDept.Eyeshot;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using hanee.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public partial class LineTypeControl : DevExpress.XtraEditors.XtraUserControl
    {
        enum Field
        {
            [Description("Name")]
            name,
            [Description("Pattern")]
            pattern,
            [Description("Description")]
            description,
            [Description("Length")]
            length
        }

        Model design;
        public LineTypeControl()
        {
            InitializeComponent();
        }

        public GridView GetGridView() => gridView1;
        public GridControl GetGridControl() => gridControl1;

        public void RefreshDataSource()
        {
            gridControl1.RefreshDataSource();
        }

        public void SetDesign(Model design)
        {
            this.design = design;
            gridControl1.DataSource = design.LineTypes;
            gridView1.OptionsBehavior.ReadOnly = true;

            gridView1.Columns.ForEach(x => x.Visible = false);

            int idx = 0;
            SetColumn(Field.name.GetDescription(), idx++, LanguageHelper.Tr("Name"));
            //SetColumn(Field.pattern.GetDescription(), idx++, LanguageHelper.Tr("Pattern"));
            SetColumn(Field.description.GetDescription(), idx++, LanguageHelper.Tr("Description"));
            SetColumn(Field.length.GetDescription(), idx++, LanguageHelper.Tr("Length"));


            gridView1.Columns[Field.name.GetDescription()].Width = 150;
            gridView1.Columns[Field.pattern.GetDescription()].Width = 200;
            gridView1.Columns[Field.description.GetDescription()].Width = 200;
            gridView1.Columns[Field.length.GetDescription()].Width = 80;

            gridView1.CellValueChanged += GridView1_CellValueChanged;

            foreach (RepositoryItem item in gridControl1.RepositoryItems)
            {
                item.EditValueChanged += Item_EditValueChanged;
            }

        }


        private void Item_EditValueChanged(object sender, EventArgs e)
        {
            if (gridView1.PostEditor())
                gridView1.UpdateCurrentRow();
        }


        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (design == null)
                return;

            // text 를 regen
            design.RegenAllTexts();
            design.Invalidate();
        }

        private void SetColumn(string fieldName, int visibleIndex, string caption)
        {
            gridView1.Columns[fieldName].VisibleIndex = visibleIndex;
            gridView1.Columns[fieldName].Caption = caption;
            gridView1.Columns[fieldName].Visible = true;
        }
    }
}
