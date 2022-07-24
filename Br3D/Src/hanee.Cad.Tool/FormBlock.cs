using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using DevExpress.XtraEditors;
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
    public partial class FormBlock : DevExpress.XtraEditors.XtraForm
    {
        public enum Mode
        {
            newBlockName,
            existBlockName
        }
        Model mainModel { get; set; }
        Mode mode { get; set; } = Mode.newBlockName;
        
        public FormBlock(Model mainModel, Mode mode)
        {
            InitializeComponent();

            model1.Unlock("US21-D8G5N-12J8F-5F65-RD3W");
            if (mainModel == null)
                System.Diagnostics.Debug.Assert(false);

            this.mode = mode;
            this.mainModel = mainModel;
            this.mainModel.CopyTo(model1, false);

            panelControlExistBlockOptions.Visible = mode == Mode.existBlockName;
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
                if (b.Name == "RootBlock")
                    continue;

                comboBoxEditBlock.Properties.Items.Add(b.Name);
            }

            if (mode == Mode.existBlockName && comboBoxEditBlock.Properties.Items.Count > 0)
                comboBoxEditBlock.SelectedIndex = 0;
        }

        public string curBlockName => comboBoxEditBlock.SelectedItem as string;
        public double xScale => textEditXScale.Text.ToDouble();
        public double yScale => textEditYScale.Text.ToDouble();
        public double zScale => textEditZScale.Text.ToDouble();

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
            if(!IsValidBlockName())
            {
                XtraMessageBox.Show(LanguageHelper.Tr("Block name is incorrect"));
                return;
            }

            // 새로운 block name 입력받는데 이미 있는걸 입력했다면 고칠지 물어본다.
            if (mode == Mode.newBlockName)
            {
                if(mainModel.Blocks.Contains(curBlockName))
                {
                    var msg = curBlockName + LanguageHelper.Tr(" is aleady exist. Replace?");
                    if (XtraMessageBox.Show(msg, LanguageHelper.Tr("New block"), MessageBoxButtons.YesNo) == DialogResult.No)
                        return;
                }
            }
            else if (mode == Mode.existBlockName)
            {
                if (!mainModel.Blocks.Contains(curBlockName))
                {
                    var msg = curBlockName + LanguageHelper.Tr(" is not found");
                    XtraMessageBox.Show(msg);
                    return;
                }

                
            }

            DialogResult = DialogResult.OK;
            Close();
        }

        private bool IsValidBlockName()
        {
            if (string.IsNullOrEmpty(curBlockName))
                return false;
            if (curBlockName.Contains(" "))
                return false;

            return true;
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