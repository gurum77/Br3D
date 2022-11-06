using System;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    static public class CmdBarManager
    {
        static public void Init(ControlCmdBar cmdBar)
        {
            CmdBarManager.cmdBar = cmdBar;
        }

        static ControlCmdBar cmdBar { get; set; } = null;

        static public string FindCommand(Action act)
        {
            if (cmdBar == null)
                return null;

            return cmdBar.FindCommand(act);
        }
        static public void SetTextEdit(string text)
        {
            if (cmdBar == null)
                return;

            cmdBar.SetTextEdit(text);
        }

        static public void FocusTextEdit(KeyEventArgs key)
        {
            if (cmdBar == null)
                return;

            cmdBar.FocusTextEdit(key);
        }
        // cmd message 설정
        // 현재 message는 history로 올린다.
        static public void SetCmdMessage(string message, ControlCmdBar.Status status)
        {
            if (cmdBar == null)
                return;

            cmdBar.SetCmdMessage(message, status);
        }

        // 리스토리 추가
        static public void AddHistory()
        {
            if (cmdBar == null)
                return;

            cmdBar.AddHistory();
        }
    }
}
