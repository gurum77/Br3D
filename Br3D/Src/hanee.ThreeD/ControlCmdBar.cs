using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    public partial class ControlCmdBar : DevExpress.XtraEditors.XtraUserControl
    {
        public enum Status
        {
            command,
            input
        }

        Dictionary<string, Action> cmds = new Dictionary<string, Action>();
        Status status { get; set; } = Status.command;
        public ControlCmdBar()
        {
            InitializeComponent();
            textEdit1.KeyDown += TextEdit1_KeyDown;
        }




        // 현재 cmd line의 내용을 history에 등록한다.
        public void AddHistory()
        {
            var str = labelControl1.Text + textEdit1.Text;
            if (string.IsNullOrEmpty(str))
                return;

            // 한줄 추가
            richTextBox1.AppendText(str + "\n");

            // 마지막 줄로 스크롤
            richTextBox1.SelectionStart = richTextBox1.Text.Length;
            richTextBox1.ScrollToCaret();
        }

        internal void SetTextEdit(string text)
        {
            textEdit1.Text = text;
        }

        public void FocusTextEdit(KeyEventArgs key = null)
        {
            if (textEdit1.IsEditorActive)
                return;

            Focus();
            textEdit1.Focus();

            if (key != null)
            {
                if (!key.KeyCode.IsAlphabet() && !key.KeyCode.IsDigit())
                    return;

                textEdit1.AppendText(((char)key.KeyValue).ToString());
            }

        }

        public void SetCmdMessage(string message, Status status)
        {
            labelControl1.Text = message;
            textEdit1.Text = "";
            this.status = status;

            FocusTextEdit();
        }

        // command를 추가한다.
        public void AddCommand(string command, Action action)
        {
            var commandKey = command.ToLower();
            if (cmds.ContainsKey(commandKey))
                return;

            cmds.Add(commandKey, action);
        }

        // command를 찾는다.
        public string FindCommand(Action act)
        {
            foreach (var cmd in cmds)
            {
                if (cmd.Value == act)
                    return cmd.Key;
            }

            return null;
        }


        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
        }


        // enter를 누르면 명령을 실행한다.
        private void TextEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                if (status == Status.command)
                {
                    var commandKey = textEdit1.Text.ToLower().Trim();
                    if (cmds.TryGetValue(commandKey, out Action act) && act != null)
                    {
                        var command = FindCommand(act);
                        if (!string.IsNullOrEmpty(command))
                            SetTextEdit(command);
                        act();
                    }
                }
            }
            else if(e.KeyCode == Keys.Escape)
            {
                if(status == Status.command)
                    textEdit1.Text = "";
            }
        }

    }
}
