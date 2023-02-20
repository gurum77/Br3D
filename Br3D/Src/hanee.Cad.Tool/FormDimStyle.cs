using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using hanee.ThreeD;
using System;
using System.Collections.Generic;

namespace hanee.Cad.Tool
{
    public partial class FormDimStyle : DevExpress.XtraEditors.XtraForm
    {
        Model model;
        public FormDimStyle(List<Entity> entities, Model model)
        {
            InitializeComponent();

            this.model = model;
            var dsProp = new DimStyleProperties(entities);
            propertyGridControl1.SelectedObject = dsProp;
            propertyGridControl1.CellValueChanged += PropertyGridControl1_CellValueChanged;

            Translate();
        }

        // 번역
        private void Translate()
        {
            TranslateRowByFieldName("leftArrowHead", LanguageHelper.Tr("First"));
            TranslateRowByFieldName("rightArrowHead", LanguageHelper.Tr("Second"));
            TranslateRowByFieldName("leaderArrowHead", LanguageHelper.Tr("Leader"));

            TranslateRowByFieldName("textPrefix", LanguageHelper.Tr("Prefix"));
            TranslateRowByFieldName("textSuffix", LanguageHelper.Tr("Suffix"));
            TranslateRowByFieldName("arrowheadSize", LanguageHelper.Tr("Head size"));
            TranslateRowByFieldName("arrowsLocation", LanguageHelper.Tr("Location"));
            TranslateRowByFieldName("textOverride", LanguageHelper.Tr("Contents"));
            TranslateRowByFieldName("textColor", LanguageHelper.Tr("Color"));
            TranslateRowByFieldName("textColorMethod", LanguageHelper.Tr("Method"));
            TranslateRowByFieldName("textLocation", LanguageHelper.Tr("Location"));

            TranslateRowByFieldName("toleranceType", LanguageHelper.Tr("Tolerance type"));
            TranslateRowByFieldName("upperValue", LanguageHelper.Tr("Upper"));
            TranslateRowByFieldName("lowerValue", LanguageHelper.Tr("Lower"));
            TranslateRowByFieldName("tolerancePrecision", LanguageHelper.Tr("Precision"));

            TranslateCategoryByFieldName("textPrefix", LanguageHelper.Tr("Text"));
            TranslateCategoryByFieldName("arrowheadSize", LanguageHelper.Tr("Arrow"));
            TranslateCategoryByFieldName("lowerValue", LanguageHelper.Tr("Tolerance"));
        }

        // field의 category를 번역
        private void TranslateCategoryByFieldName(string fieldName, string caption)
        {
            var row = propertyGridControl1.GetRowByFieldName(fieldName);
            if (row == null)
                return;

            var parentProp = row.ParentRow?.Properties;
            if (parentProp == null)
                return;

            parentProp.Caption = caption;
        }

        private void TranslateRowByFieldName(string fieldName, string caption)
        {
            var prop = propertyGridControl1.GetRowPropertiesByFieldName(fieldName);
            if (prop == null)
                return;

            prop.Caption = caption;
        }

        // 값이 변경되면 치수들을 즉시 다시 그린다.
        private void PropertyGridControl1_CellValueChanged(object sender, DevExpress.XtraVerticalGrid.Events.CellValueChangedEventArgs e)
        {
            var dsProp = propertyGridControl1.SelectedObject as DimStyleProperties;
            if (dsProp == null)
                return;

            var data = new RegenParams(0.001, model);
            var dimensions = dsProp.GetDimensions();
            if(dimensions != null)
            {
                foreach (var dim in dimensions)
                    dim.Regen(data);
            }
            

            model.Entities.Regen();
            model.Invalidate();
        }

        private void simpleButtonOk_Click(object sender, System.EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }

        private void simpleButtonCancel_Click(object sender, System.EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Close();
        }
    }
}