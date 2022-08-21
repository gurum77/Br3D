using DevExpress.Utils.Helpers;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Br3D
{
    public partial class ControlFileAssociation : UserControl
    {
        List<FileAssociationByExt> faByExts = new List<FileAssociationByExt>();
        string programName
        {
            get
            {
                return Path.GetFileName(programPath);
            }
        }
        string programPath = "";
        public ControlFileAssociation()
        {
            InitializeComponent();
            gridView1.CustomDrawCell += GridView1_CustomDrawCell;
            Translate();
        }

        private void Translate()
        {
            simpleButtonApply.Text = LanguageHelper.Tr("Apply");
            simpleButtonSelectAll.Text = LanguageHelper.Tr("Select all");
            gridView1.Columns[0].Caption = LanguageHelper.Tr("Association");
            gridView1.Columns[1].Caption = LanguageHelper.Tr("Ext.");
        }

        private void GridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.Column.AbsoluteIndex != 1)
                return;

            var fa = gridView1.GetRow(e.RowHandle) as FileAssociationByExt;
            if (fa == null)
                return;

            var itemSize = 16;
            var imageOffset = 0;
            var image = FileSystemHelper.GetFileExtensionImage("."+fa.ext, IconSizeType.Small, new System.Drawing.Size(itemSize, itemSize));
            if (image == null)
                return;
            var rectMain = new Rectangle(e.Bounds.X + imageOffset, e.Bounds.Y, itemSize, itemSize);
            e.Graphics.DrawImage(image, rectMain);

            var textOffset = itemSize + 2;
            var bounds = new Rectangle(e.Bounds.X + textOffset, e.Bounds.Y, e.Bounds.Width - textOffset, e.Bounds.Height);
            e.Appearance.DrawString(e.Cache, fa.ext, bounds);
            e.Handled = true;
        }

        // 파일 연결 적용
        public void Apply()
        {
            try
            {
                foreach (var faByExt in faByExts)
                {
                    if (!faByExt.associated)
                        continue;

                    var ext = faByExt.ext;
                    if (ext.StartsWith("."))
                        ext = ext.Remove(0, 1);
                    FileAssociationHelper.SetAssociation_User(faByExt.ext, programPath, "Br3D.exe");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // 초기화
        public void Init(string programPath, params string[] exts)
        {
            this.programPath = programPath;
            faByExts.Clear();
            foreach (var ext in exts)
            {
                try
                {
                    var faByExt = new FileAssociationByExt();
                    faByExt.ext = ext;
                    var curProgramName = FileAssociationHelper.GetChoicedProgramNameByExt(ext);
                    if (curProgramName == null)
                        faByExt.associated = false;
                    else
                    {
                        faByExt.associated = programName.ToLower().Equals(curProgramName.ToLower());
                    }
                    faByExts.Add(faByExt);
                }
                catch
                {
                }

            }
            

            gridControl1.DataSource = faByExts;
        }

        // 전체 선택
        private void simpleButtonSelectAll_Click(object sender, EventArgs e)
        {
            foreach (var fe in faByExts)
            {
                fe.associated = true;
            }

            gridControl1.RefreshDataSource();

        }

        private void simpleButtonApply_Click(object sender, EventArgs e)
        {
            Apply();
        }
    }
}
