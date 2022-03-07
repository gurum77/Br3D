using devDept.Eyeshot;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using hanee.Geometry;
using hanee.ThreeD;
using System;
using System.ComponentModel;

namespace hanee.Cad.Tool
{
    public partial class LayerControl : DevExpress.XtraEditors.XtraUserControl
    {
        enum Field
        {
            [Description("Locked")]
            locked,
            [Description("Exportable")]
            exportable,
            [Description("Name")]
            name,
            [Description("Color")]
            color,
            [Description("MaterialName")]
            materialName,
            [Description("Visible")]
            visible,
            [Description("LineWeight")]
            lineWeight,
            [Description("LineTypeName")]
            lineTypeName
        }

        Model design;
        internal void RefreshDataSource()
        {
            gridControl1.RefreshDataSource();
        }

        public LayerControl()
        {
            InitializeComponent();
        }

        public GridView GetGridView() => gridView1;
        public GridControl GetGridControl() => gridControl1;
        

        
        public void SetDesign(Model design)
        {
            this.design = design;
            gridControl1.DataSource = design.Layers;
            
            gridView1.Columns.ForEach(x => x.Visible = false);

            int idx = 0;
            SetColumn(Field.visible.GetDescription(), idx++, LanguageHelper.Tr("Visible"));
            SetColumn(Field.name.GetDescription(), idx++, LanguageHelper.Tr("Name"));
            SetColumn(Field.color.GetDescription(), idx++, LanguageHelper.Tr("Color"));
            SetColumn(Field.locked.GetDescription(), idx++, LanguageHelper.Tr("Locked"));
            SetColumn(Field.lineTypeName.GetDescription(), idx++, LanguageHelper.Tr("Line Type Name"));
            SetColumn(Field.lineWeight.GetDescription(), idx++, LanguageHelper.Tr("Line Weight"));
            SetColumn(Field.materialName.GetDescription(), idx++, LanguageHelper.Tr("Material Name"));

            gridView1.Columns[Field.name.GetDescription()].Width = 200;
            gridView1.Columns[Field.visible.GetDescription()].Width = 50;
            gridView1.Columns[Field.locked.GetDescription()].Width = 50;
            gridView1.Columns[Field.lineWeight.GetDescription()].Width = 60;

            gridView1.CellValueChanged += GridView1_CellValueChanged;

            RepositoryItemCheckEdit checkEdit = new RepositoryItemCheckEdit();
            gridControl1.RepositoryItems.Add(checkEdit);
            gridView1.Columns[Field.locked.GetDescription()].ColumnEdit = checkEdit;
            gridView1.Columns[Field.visible.GetDescription()].ColumnEdit = checkEdit;

            RepositoryItemColorEdit colorEdit = new RepositoryItemColorEdit();
            gridControl1.RepositoryItems.Add(colorEdit);
            gridView1.Columns[Field.color.GetDescription()].ColumnEdit = colorEdit;

            foreach (RepositoryItem item in gridControl1.RepositoryItems)
            {
                item.EditValueChanged += Item_EditValueChanged;
            }
        }

        private void Item_EditValueChanged(object sender, EventArgs e)
        {
            if(gridView1.PostEditor())
                gridView1.UpdateCurrentRow();
        }

        private void GridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (design == null)
                return;

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
