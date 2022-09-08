using DevExpress.XtraEditors;
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
    public partial class ControlScriptCad : DevExpress.XtraEditors.XtraUserControl
    {
        public ControlScriptCad()
        {
            InitializeComponent();
        }

        // 실행
        private void simpleButtonRun_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            Parse();
            UpdateResultControls();
        }

        // parsing 결과를 control에 업데이트한다.
        private void UpdateResultControls()
        {
        }

        // 현재 입력된 내용을 parsing해서 생성 가능 객체 종류를 만든다.
        void Parse()
        {

        }
    }
}
