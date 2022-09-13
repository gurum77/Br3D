using devDept.Eyeshot;
using hanee.Geometry;
using hanee.ThreeD;
using System;
using System.Collections.Generic;

namespace hanee.Cad.Tool
{
    public partial class ControlScriptCad : DevExpress.XtraEditors.XtraUserControl
    {
        public Model model { get; set; }
        public ControlScriptCad()
        {
            InitializeComponent();
            labelControlTitle.Text = LanguageHelper.Tr("Script");
            simpleButtonRun.Text = LanguageHelper.Tr("Run");
            Parse();
        }

        // 선택한 command 정보를 표시
        private void ComboBoxEdit1_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGridControl1.SelectedObject = comboBoxEdit1.SelectedItem;
        }

        // 실행
        private void simpleButtonRun_Click(object sender, EventArgs e)
        {
            if (model == null)
                return;
            var cmd = comboBoxEdit1.SelectedItem as ScriptCommand;
            if (cmd == null)
                return;
            if (!cmd.Run(model))
                return;

            model.Entities.RegenAllCurved();
            model.Invalidate();
            
        }

        // text 를 입력할때마다 parsing을 한다.
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            Parse();
        }


        // 현재 입력된 내용을 parsing해서 생성 가능 객체 종류를 만든다.
        void Parse()
        {
            var script = richTextBox1.Text;

            var cmds = new List<ScriptCommand>();

            // command가 있는지?
            var cmd = new ScriptCommand(script);

            if (cmd.cmd != ScriptCommand.Command.unknown)
            {
                cmds.Add(cmd);
            }
            // command가 없으면 command를 자동 판단한다.
            else
            {
                // 좌표를 가져온다.
                var points = script.ToPoint3Ds();
                // 점이 1개이면 원
                if (points.Count == 1)
                {
                    cmd = new ScriptCommand(ScriptCommand.Command.createCircle);
                    cmd.points = points;
                    cmds.Add(cmd);
                }
                else if (points.Count == 2)
                {
                    cmd = new ScriptCommand(ScriptCommand.Command.createLine);
                    cmd.points = new List<devDept.Geometry.Point3D>() { points[0], points[1] };
                    cmds.Add(cmd);
                }
                else if(points.Count > 2)
                {


                    // pline
                    cmd = new ScriptCommand(ScriptCommand.Command.createPline);
                    cmd.points = points;
                    cmds.Add(cmd);
                }
                cmd.points = points;
                

            }

            // combo를 갱신
            comboBoxEdit1.Properties.Items.Clear();
            comboBoxEdit1.Properties.Items.AddRange(cmds);
            if (comboBoxEdit1.Properties.Items.Count > 0)
                comboBoxEdit1.SelectedIndex = 0;

        }
    }
}
