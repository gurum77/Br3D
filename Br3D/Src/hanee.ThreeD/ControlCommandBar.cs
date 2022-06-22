using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    public partial class ControlCommandBar : DevExpress.XtraEditors.XtraUserControl
    {
        Dictionary<string, Action> commands = new Dictionary<string, Action>();
        public bool enabled
        {
            get
            {
                return this.Enabled;
            }

            set
            {
                comboBoxEdit1.Enabled = value;
                this.Enabled = value;
            }
        }
        public ControlCommandBar()
        {
            InitializeComponent();

            comboBoxEdit1.KeyDown += ComboBoxEdit1_KeyDown;
            comboBoxEdit1.KeyUp += ComboBoxEdit1_KeyUp;
            comboBoxEdit1.DrawItem += ComboBoxEdit1_DrawItem;
        }

        private void ComboBoxEdit1_DrawItem(object sender, ListBoxDrawItemEventArgs e)
        {
            e.Handled = true;
            var commandItem = e.Item as CommandItem;
            if (commandItem == null)
                return;
            if ((e.State & DrawItemState.Selected) > 0)
            {
                e.Appearance.FontStyleDelta = FontStyle.Bold | FontStyle.Italic | FontStyle.Underline;
            }

            var bounds = e.Bounds;
            bounds.X += comboBoxEdit1.Margin.Left;
            if (!commandItem.command.Equals(commandItem.displayText))
            {
                e.Cache.DrawString($"{commandItem.command}({commandItem.displayText})", e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), bounds);
            }
            else
            {
                e.Cache.DrawString($"{commandItem.command}", e.Appearance.Font, e.Appearance.GetForeBrush(e.Cache), bounds);
            }



            e.Handled = true;
        }

        private void ContextMenuStrip1_Opened(object sender, EventArgs e)
        {

        }

        private void ComboBoxEdit1_KeyUp(object sender, KeyEventArgs e)
        {
            comboBoxEdit1.ShowPopup();
        }

        private void ComboBoxEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                // 선택한 아이템이 없으면 검색중인 문자로 아이템을 강제 선택한다.
                if (comboBoxEdit1.SelectedIndex == -1)
                {
                    foreach (CommandItem item in comboBoxEdit1.Properties.Items)
                    {
                        if (item.command.StartsWith(comboBoxEdit1.AutoSearchText, true, null))
                        {
                            comboBoxEdit1.SelectedItem = item;
                            break;
                        }
                    }

                }
                RunAction();
            }
        }

        private void RunAction()
        {
            var commandItem = comboBoxEdit1.SelectedItem as CommandItem;
            if (commandItem == null)
                return;

            if (commandItem.act != null)
                commandItem.act();

            comboBoxEdit1.SelectedItem = null;
            comboBoxEdit1.SelectedText = "";
        }

        public void AddCommand(string command, string displayText, Action act)
        {
            var commandItem = new CommandItem(command, displayText, act);

            for (int i = 0; i < comboBoxEdit1.Properties.Items.Count; ++i)
            {
                var item = comboBoxEdit1.Properties.Items[i] as CommandItem;
                if (item.command.CompareTo(commandItem.command) > 0)
                {
                    comboBoxEdit1.Properties.Items.Insert(i, commandItem);
                    return;
                }
            }

            comboBoxEdit1.Properties.Items.Add(commandItem);
        }
    }
}
