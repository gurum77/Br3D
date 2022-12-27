using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Translators;
using DevExpress.XtraEditors;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cam
{
    public partial class ControlSimulation : DevExpress.XtraEditors.XtraUserControl
    {
        public ControlSimulation()
        {
            InitializeComponent();
            manufacture1.Unlock("US21-D8G5N-12J8F-5F65-RD3W");

            Translate();
            
        }

        private void ControlSimulation_Load(object sender, EventArgs e)
        {
            manufacture1.ActiveViewport.Rotate.RotationMode = rotationType.Turntable;
            manufacture1.ActiveViewport.Rotate.MouseButton = new MouseButton(MouseButtons.Middle, modifierKeys.Ctrl);
            manufacture1.ActiveViewport.Pan.MouseButton = new MouseButton(MouseButtons.Middle, modifierKeys.None);

            manufacture1.PositionChanged += Manufacture1_PositionChanged;

        }

        private void Manufacture1_PositionChanged(object sender, Manufacture.PositionChangedEventArgs e)
        {
            UpdateInfos(e.CutterLocation);
        }

        void Translate()
        {
            dockPanel1.Text = LanguageHelper.Tr("Options");
            groupControl1.Text = LanguageHelper.Tr("View");
            groupControlInfo.Text = LanguageHelper.Tr("Information");
            groupControlSpeed.Text = LanguageHelper.Tr("Speed");

            checkEditViewStock.Text = LanguageHelper.Tr("Stock"); 
            checkEditViewPoints.Text = LanguageHelper.Tr("Point");
            checkEditViewTool.Text = LanguageHelper.Tr("Tool");
            checkEditViewToolpath.Text = LanguageHelper.Tr("Toolpath");
        }

        // 파일 로딩
        public bool OpenFile(string nccFileName)
        {
            ReadFileAsync rf = FileHelper.GetReadFileAsync(nccFileName);
            if (rf is ReadGCode)
            {
                rf.DoWork();
                rf.AddToScene(manufacture1);
                manufacture1.SetView(viewType.Trimetric, true, manufacture1.AnimateCamera);
                manufacture1.DrawGCode(new devDept.Eyeshot.Milling.EndMill(0.6, 0.3));
                manufacture1.ZoomFit();
                manufacture1.Invalidate();
                return true;
            }

            return false;
        }

        // show stock
        private void checkEditViewStock_CheckedChanged(object sender, EventArgs e)
        {
            manufacture1.SimulationStock.Visible = checkEditViewStock.Checked;

            manufacture1.Entities.Regen();
            manufacture1.Invalidate();
        }

        // show tool
        private void checkEditViewTool_CheckedChanged(object sender, EventArgs e)
        {
            manufacture1.SimulationTool.Visible = checkEditViewTool.Checked;

            manufacture1.Entities.Regen();
            manufacture1.Invalidate();
        }

        // tool path
        private void checkEditViewToolpath_CheckedChanged(object sender, EventArgs e)
        {
            manufacture1.Entities.Where(el => el is Toolpath).ToList().ForEach(el => el.Visible = checkEditViewToolpath.Checked);

            manufacture1.Entities.Regen();
            manufacture1.Invalidate();
        }

        // points
        private void checkEditViewPoints_CheckedChanged(object sender, EventArgs e)
        {
            manufacture1.Entities.OfType<Toolpath>().ToList().ForEach(tp => tp.ShowPoints = checkEditViewPoints.Checked);
            manufacture1.Invalidate();
        }

        // track bar speed 변경
        private void trackBarControl1_EditValueChanged(object sender, EventArgs e)
        {
            manufacture1.AutoSpeed(trackBarControl1.Value);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            var ml = manufacture1.SimulationToolpath.MotionList;
            //UpdateInfos(manufacture1.Tool.);
        }

        private void UpdateInfos(PointCL cutterLocation)
        {
            if (cutterLocation == null)
                return;

            var infos = new List<TitleContents>();
            infos.Add(new TitleContents("Tool", cutterLocation.Tool.ToString()));
            infos.Add(new TitleContents("X", cutterLocation.X.ToString("0.000")));
            infos.Add(new TitleContents("Y", cutterLocation.Y.ToString("0.000")));
            infos.Add(new TitleContents("Z", cutterLocation.Z.ToString("0.000")));
            infos.Add(new TitleContents("Code", cutterLocation.Code.ToString()));
            infos.Add(new TitleContents("Speed", cutterLocation.Speed.ToString()));
            infos.Add(new TitleContents("Feed", cutterLocation.Feed.ToString()));
            gridControlInfo.DataSource = infos;
        }
    }
}
