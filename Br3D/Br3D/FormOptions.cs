using DevExpress.XtraEditors;
using hanee.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Br3D
{
    public partial class FormOptions : DevExpress.XtraEditors.XtraForm
    {
        public FormOptions()
        {
            InitializeComponent();
            optionsBindingSource.DataSource = Options.Instance;
        }

        private void FormOptions_Load(object sender, EventArgs e)
        {
            InitControl();
            Translate();
        }

        private void Translate()
        {
            this.Text = LanguageHelper.Tr("Options");

            xtraTabPageDrawing.Text = LanguageHelper.Tr("Drawing");
            xtraTabPageColor.Text = LanguageHelper.Tr("Color");
            xtraTabPageGeneral.Text = LanguageHelper.Tr("General");
            xtraTabPageFileAssociation.Text = LanguageHelper.Tr("File association");

            groupControlBackground.Text = LanguageHelper.Tr("Background(3D)");
            labelControlBackgroundTop.Text = LanguageHelper.Tr("Top");
            labelControlBackgroundBottom.Text = LanguageHelper.Tr("Bottom");

            groupControlBackground2D.Text = LanguageHelper.Tr("Background(2D)");
            labelControlAll.Text = LanguageHelper.Tr("All");

            checkEditSaveImageWithUI.Text = LanguageHelper.Tr("UI");
            checkEditSaveImageWithBackground.Text = LanguageHelper.Tr("Background");

            simpleButtonDefault.Text = LanguageHelper.Tr("Default");
            simpleButtonOk.Text = LanguageHelper.Tr("Ok");
            simpleButtonCancel.Text = LanguageHelper.Tr("Cancel");
        }

        void InitControl()
        {
            // drawing

            // color
            colorPickEditBackgroundTop.Color = Options.Instance.backgroundColorTop.colorValue;
            colorPickEditBackgroundBottom.Color = Options.Instance.backgroundColorBottom.colorValue;

            colorPickEditBackground2D.Color = Options.Instance.backgroundColor2D.colorValue;

            checkEditSaveImageWithUI.Checked = Options.Instance.saveImageWithUI;
            checkEditSaveImageWithBackground.Checked = Options.Instance.saveImageWithBackground;

            controlFileAssociation1.Init(Process.GetCurrentProcess().MainModule.FileName, "br3", "dwg", "dxf", 
                "ifc", "3ds", "gcode", "jt", "stp", "step", "igs", "iges", "obj", "stl", "las", "asc", "emf");
        }
        void Save()
        {
            // drawing
            Options.Instance.curLinetypeScale = textEditLineTypeScale.EditValue.ToString().ToFloat();

            // color
            Options.Instance.backgroundColorTop.colorValue = colorPickEditBackgroundTop.Color;
            Options.Instance.backgroundColorBottom.colorValue = colorPickEditBackgroundBottom.Color;

            Options.Instance.backgroundColor2D.colorValue = colorPickEditBackground2D.Color;

            Options.Instance.saveImageWithUI = checkEditSaveImageWithUI.Checked;
            Options.Instance.saveImageWithBackground = checkEditSaveImageWithBackground.Checked;
        }

        private void simpleButtonOk_Click(object sender, EventArgs e)
        {
            Save();
            Options.Instance.SaveOptions();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void simpleButtonDefault_Click(object sender, EventArgs e)
        {
            Options.Instance.Default();
            InitControl();
        }
    }
}