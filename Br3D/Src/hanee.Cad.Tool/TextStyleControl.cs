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
    public partial class TextStyleControl : DevExpress.XtraEditors.XtraUserControl
    {
        enum Field
        {
            [Description("Name")]
            name,
            [Description("FontFamilyName")]
            font,
            [Description("FileName")]
            shxFileName,
            [Description("Style")]
            style,
            [Description("WidthFactor")]
            widthFactor
        }

        Model design;
        public TextStyleControl()
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
            gridControl1.DataSource = design.TextStyles;

            gridView1.Columns.ForEach(x => x.Visible = false);

            int idx = 0;
            SetColumn(Field.name.GetDescription(), idx++, LanguageHelper.Tr("Name"));
            SetColumn(Field.font.GetDescription(), idx++, LanguageHelper.Tr("Font"));
            SetColumn(Field.shxFileName.GetDescription(), idx++, LanguageHelper.Tr("Shx File Name"));
            SetColumn(Field.style.GetDescription(), idx++, LanguageHelper.Tr("Style"));
            SetColumn(Field.widthFactor.GetDescription(), idx++, LanguageHelper.Tr("WidthFactor"));

            gridView1.Columns[Field.name.GetDescription()].Width = 200;
            gridView1.Columns[Field.font.GetDescription()].Width = 200;
            gridView1.Columns[Field.shxFileName.GetDescription()].Width = 200;
            gridView1.Columns[Field.widthFactor.GetDescription()].Width = 50;

            gridView1.CellValueChanged += GridView1_CellValueChanged;

            RepositoryItemFontEdit fontEdit = new RepositoryItemFontEdit();
            gridControl1.RepositoryItems.Add(fontEdit);
            gridView1.Columns[Field.font.GetDescription()].ColumnEdit = fontEdit;

            RepositoryItemButtonEdit fileEdit = new RepositoryItemButtonEdit();
            gridControl1.RepositoryItems.Add(fileEdit);
            gridView1.Columns[Field.shxFileName.GetDescription()].ColumnEdit = fileEdit;

            foreach (RepositoryItem item in gridControl1.RepositoryItems)
            {
                item.EditValueChanged += Item_EditValueChanged;
            }

            fileEdit.ButtonClick += FileEdit_ButtonClick;
        }

        private void FileEdit_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = "shx";
            dlg.Filter = "SHX font file|*.shx";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var textStyle = gridView1.FocusedRowObject as TextStyle;
                if (textStyle != null)
                {
                    textStyle.FileName = dlg.FileName;
                    gridView1.UpdateCurrentRow();
                }
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
