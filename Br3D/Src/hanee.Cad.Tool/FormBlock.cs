using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
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

namespace hanee.Cad.Tool
{
    public partial class FormBlock : DevExpress.XtraEditors.XtraForm
    {
        Model mainModel { get; set; }
        
        public FormBlock(Model mainModel)
        {
            InitializeComponent();
            model1.Unlock("US21-D8G5N-12J8F-5F65-RD3W");
            if (mainModel == null)
                System.Diagnostics.Debug.Assert(false);

            this.mainModel = mainModel;
            this.mainModel.CopyTo(model1, false);
        }


        private void FormBlock_Load(object sender, EventArgs e)
        {
            
            InitCombo();
            model1.Set2DViewStyle();
        }

        void InitCombo()
        {
            comboBoxEditBlock.Properties.Items.Clear();
            if (mainModel == null)
                return;

            foreach (var b in mainModel.Blocks)
            {
                comboBoxEditBlock.Properties.Items.Add(b.Name);
            }
        }

        string curBlockName => comboBoxEditBlock.SelectedItem as string;
        private void simpleButtonDel_Click(object sender, EventArgs e)
        {
            if (mainModel == null)
                return;

            // 현재 선택한 블럭 삭제
            try
            {
                mainModel.Blocks.TryRemove(curBlockName);
                InitCombo();
                comboBoxEditBlock.SelectedText = "";

            }
            catch
            {

            }
        }

        private void checkButton2D3D_CheckedChanged(object sender, EventArgs e)
        {
            if (checkButton3D.Checked)
                model1.Set3DViewStyle();
            else
                model1.Set2DViewStyle();
        }

        private void simpleButtonOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void simpleButtonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void comboBoxEditBlock_SelectedIndexChanged(object sender, EventArgs e)
        {
            DrawBlock();
        }

        void DrawBlock()
        {
            if (!model1.Blocks.Contains(curBlockName))
                return;

            var br = new BlockReference(curBlockName);
            model1.Entities.Clear();
            model1.Entities.Add(br);
            
            model1.Entities.Regen(new RegenOptions());
            model1.Invalidate();
            model1.ZoomFit();
        }

    }
}