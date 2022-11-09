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
            textEdit1.KeyUp += TextEdit1_KeyUp;
            textEdit1.TextChanged += TextEdit1_TextChanged;
        }

        
        // text edit의 유효한 text를 리턴
        string GetValidTextEditText()
        {
            var str = textEdit1.Text;
            if (ActionBase.userInputting[(int)ActionBase.UserInput.GettingText] && str.Length > 0 && str.EndsWith(" "))
            {
                str = str.Remove(str.Length - 1, 1);
            }
            return str;
        }
        private void TextEdit1_TextChanged(object sender, EventArgs e)
        {
            UpdateCmdList();
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

        // cmd list를 표시하거나 숨김
        private void VisibleCmdList(bool visible)
        {
            if (status != Status.command)
                return;

            listBoxControl1.Visible = visible;
        }

        // cmd list를 갱신한다.
        private void UpdateCmdList()
        {
            var str = textEdit1.Text.ToLower();

            // command 입력상태에서는 space가 있으면 안됨.
            if (status == Status.command)
                str = str.Trim();

            if (string.IsNullOrEmpty(str))
            {
                VisibleCmdList(false);
                return;
            }

            if (!listBoxControl1.Visible)
                VisibleCmdList(true);

            var cmdList = new List<string>();
            foreach (var cmd in cmds)
            {
                if (cmd.Key.StartsWith(str))
                    cmdList.Add(cmd.Key);
            }

            cmdList.Sort();

            listBoxControl1.DataSource = cmdList;
        }

        // command를 추가한다.
        public void AddCommand(string command, string displayText, Action action)
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

        void TextEdit1_KeyEvent(KeyEventArgs e)
        {
            
        }


        // enter를 누르면 명령을 실행한다.
        private void TextEdit1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                // command 입력 상태인 경우
                if (status == Status.command)
                {
                    // list box가 선택되어 있는 상태라면 해당 action을 찾아서 실행한다.
                    if (listBoxControl1.SelectedItem != null)
                    {
                        var commandKey = listBoxControl1.SelectedItem.ToString().ToLower();
                        if (cmds.TryGetValue(commandKey, out Action act) && act != null)
                        {
                            var command = FindCommand(act);
                            if (!string.IsNullOrEmpty(command))
                                SetTextEdit(command);
                            act();
                        }
                    }
                }
                // command 입력 상태가 아닌 경우 (text 입력)
                else
                {
                    if (ActionBase.userInputting[(int)ActionBase.UserInput.GettingText])
                    {
                        ActionBase.text = GetValidTextEditText();
                        ActionBase.EndInput(ActionBase.UserInput.GettingText);
                    }
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (status == Status.command)
                    textEdit1.Text = "";
            }
        }
    }
}
