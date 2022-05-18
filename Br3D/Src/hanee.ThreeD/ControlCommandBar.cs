using DevExpress.XtraBars;
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

namespace hanee.ThreeD
{
    public partial class ControlCommandBar : DevExpress.XtraEditors.XtraUserControl
    {
        Dictionary<string, Action> commands = new Dictionary<string, Action>();
        public ControlCommandBar()
        {
            InitializeComponent();

            comboBoxEdit1.KeyDown += ComboBoxEdit1_KeyDown;
            
        }

        private void ComboBoxEdit1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
            {
                RunAction();
            }
            else
            {
                // 팝업 표시할지? 안할지?
                popupMenu1.AddItem(new BarButtonItem(popupMenu1.Manager, "a"));
                popupMenu1.AddItem(new BarButtonItem(popupMenu1.Manager, "a"));
                popupMenu1.AddItem(new BarButtonItem(popupMenu1.Manager, "a"));
                popupMenu1.AddItem(new BarButtonItem(popupMenu1.Manager, "a"));
                popupMenu1.ShowPopup(Control.MousePosition);
            }
        }

        private void RunAction()
        {
            var command = comboBoxEdit1.SelectedItem.ToString();
            if (!commands.ContainsKey(command))
                return;

            if (commands.TryGetValue(command, out Action ac))
            {
                ac();
                comboBoxEdit1.SelectedItem = null;
                comboBoxEdit1.SelectedText = "";
            }
        }

        public void AddCommand(string command, Action act)
        {
            if (commands.ContainsKey(command))
                return;

            commands.Add(command, act);
            comboBoxEdit1.Properties.Items.Add(command);
        }
    }
}
