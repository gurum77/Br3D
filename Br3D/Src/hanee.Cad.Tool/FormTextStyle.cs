using devDept.Eyeshot;
using hanee.ThreeD;

namespace hanee.Cad.Tool
{
    public partial class FormTextStyle : DevExpress.XtraEditors.XtraForm
    {
        Model design;
        public FormTextStyle(Model design)
        {
            InitializeComponent();

            this.design = design;
            textStyleControl1.SetDesign(design);

            Translate();
        }

        public void RefreshDataSource()
        {
            textStyleControl1.RefreshDataSource();
        }

        private void Translate()
        {
            Text = LanguageHelper.Tr("Text style");
            simpleButtonAdd.Text = LanguageHelper.Tr("Add");
            simpleButtonDel.Text = LanguageHelper.Tr("Delete");
            simpleButtonClose.Text = LanguageHelper.Tr("Close");
        }

        private void simpleButtonAdd_Click(object sender, System.EventArgs e)
        {
            // eyeshot은 무조건 1개의 text style이 있음.
            if (design.TextStyles.Count == 0)
                return;

            string newName = "New Text Style";
            int num = 1;
            while (true)
            {
                newName = $"New Text Style{num++}";
                if (!design.TextStyles.Contains(newName))
                    break;
            }

            var newTextStyle = new TextStyle(design.TextStyles[0]);
            newTextStyle.Name = newName;
            design.TextStyles.Add(newTextStyle);

            RefreshDataSource();

        }

        private void simpleButtonDel_Click(object sender, System.EventArgs e)
        {
            textStyleControl1.GetGridView().DeleteSelectedRows();
        }

        private void simpleButtonClose_Click(object sender, System.EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Close();
        }
    }
}