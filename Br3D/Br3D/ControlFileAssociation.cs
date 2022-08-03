using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Br3D
{
    public partial class ControlFileAssociation : UserControl
    {
        List<FileAssociationByExt> faByExts = new List<FileAssociationByExt>();
        string programName = "";
        public ControlFileAssociation()
        {
            InitializeComponent();
        }

        public void Apply()
        {
            try
            {
                foreach (var faByExt in faByExts)
                {
                    if (!faByExt.associated)
                        continue;

                    FileAssociationHelper.SetProgramNameByExt(faByExt.ext, programName);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // 초기화
        public void Init(string programName, params string[] exts)
        {
            this.programName = programName;
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
    }
}
