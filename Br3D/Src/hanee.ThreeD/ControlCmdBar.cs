using devDept.Geometry;
using hanee.Geometry;
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
        public Action lastCmd { get; set; }
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

        internal Status GetStatus()
        {
            return status;
        }

        internal string GetCmdMessage()
        {
            return labelControl1.Text;
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

        public void AddCmdText(string text)
        {
            textEdit1.Text = textEdit1.Text + text;

            FocusTextEdit();
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

                            // lastCmd 저장
                            lastCmd = act;
                            act();
                        }
                    }
                    // list box가 선택되어 있지 않은 상태라면 마지막 action을 실행한다.
                    else
                    {
                        var commandKey = textEdit1.Text;
                        commandKey = commandKey.Trim();
                        if (string.IsNullOrEmpty(commandKey) && lastCmd != null)
                        {
                            var command = FindCommand(lastCmd);
                            if (!string.IsNullOrEmpty(command))
                                SetTextEdit(command);

                            lastCmd();
                        }
                    }
                }
                // command 입력 상태가 아닌 경우 (text 입력)
                else
                {
                    var text = GetValidTextEditText();
                    // 아무것도 입력안하고 space, enter를 하면 다음 단계로 넘어간다.
                    if (string.IsNullOrEmpty(text))
                    {
                        ActionBase.Entered = true;
                    }
                    // 뭔가 입력이 되어 있다면 데이타(좌표)입력으로 처리한다.
                    else
                    {
                        Point3D startPoint = ActionBase.GetOrthoModeManager()?.startPoint;

                        // 거리 입력인지?
                        if (ActionBase.userInputting[(int)ActionBase.UserInput.GettingDist])
                        {
                            // 숫자로 변환 가능한지?
                            if(double.TryParse(text, out double dist))
                            {
                                ActionBase.dist = dist;
                                ActionBase.EndInput(ActionBase.UserInput.GettingDist);
                                return;
                            }

                            // 숫자로 변환 안되면 좌표로 변환
                            var pt = ParseToPoint3D(text);
                            if (pt != null && startPoint != null)
                            {
                                ActionBase.dist = pt.DistanceTo(startPoint);
                                ActionBase.EndInput(ActionBase.UserInput.GettingDist);
                                return;
                            }
                        }


                        // 좌표 입력인지?
                        if (ActionBase.userInputting[(int)ActionBase.UserInput.GettingPoint3D])
                        {
                            // 유효한 좌표 입력인 경우 좌표값 설정하고 종료
                            Point3D pt = ParseToPoint3D(text);
                            if (pt != null)
                            {
                                ActionBase.Point3D = pt;
                                ActionBase.EndInput(ActionBase.UserInput.GettingPoint3D);
                                return;
                            }
                        }

                        // text 입력인지?
                        if (ActionBase.userInputting[(int)ActionBase.UserInput.GettingText])
                        {
                            // 유효한 text 입력인 경우 text 값 설정하고 종료
                            if (ActionBase.IsAvailableText(text))
                            {
                                ActionBase.text = text;
                                ActionBase.EndInput(ActionBase.UserInput.GettingText);
                                return;
                            }
                        }
                    }
                }
            }
            else if (e.KeyCode == Keys.Escape)
            {
                if (status == Status.command)
                    textEdit1.Text = "";
            }
        }

        // 사용자가 입력한 text를 point로 변환
        private Point3D ParseToPoint3D(string text)
        {
            var pt = text.ToPoint3DByToken(',');
            // 유효한 좌표 입력인 경우 좌표값 설정하고 종료
            if (pt != null)
            {
                return pt;
            }

            // null인경우 상대좌표인지 판단
            if (text.Contains("@") && text.Contains("<"))
            {
                text = text.MakeHeadSpace('@', '<');
                var values = text.ToDoubles('@', '<');
                if (values != null && values.Count == 2 && ActionBase.GetOrthoModeManager()?.startPoint != null)
                {
                    var dist = (double)values[0].value;
                    var deg = (double)values[1].value;

                    return ActionBase.GetOrthoModeManager().startPoint + deg.ToRadians().ToVector().To3D() * dist;
                }
            }

            // 숫자 1개인지?
            if (double.TryParse(text, out double distTmp) && ActionBase.GetOrthoModeManager()?.startPoint != null)
            {
                var vec = (ActionBase.Point3D - ActionBase.GetOrthoModeManager()?.startPoint).ToDir();
                return ActionBase.GetOrthoModeManager().startPoint + vec * distTmp;
            }

            return null;
        }
    }
}
