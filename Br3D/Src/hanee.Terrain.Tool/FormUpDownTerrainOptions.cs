using DevExpress.XtraEditors;
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

namespace hanee.Terrain.Tool
{
    public partial class FormUpDownTerrainOptions : DevExpress.XtraEditors.XtraForm
    {
        public FormUpDownTerrainOptions()
        {
            InitializeComponent();

            Translate();
        }

        private void Translate()
        {
            this.Text = LanguageHelper.Tr("Up/Down terrain options");
            labelControlHeight.Text = LanguageHelper.Tr("Height");
            labelControlRadius.Text = LanguageHelper.Tr("Radius");
            checkEditUpDown.Text = LanguageHelper.Tr("Up");
            checkEditAutoUpdateColor.Text = LanguageHelper.Tr("Update color");
        }
    }
}