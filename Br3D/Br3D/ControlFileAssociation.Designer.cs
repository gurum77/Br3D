
namespace Br3D
{
    partial class ControlFileAssociation
    {
        /// <summary> 
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumnAssociation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumnExt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.simpleButtonSelectAll = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButtonApply = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(5, 5);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(267, 301);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumnAssociation,
            this.gridColumnExt});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // gridColumnAssociation
            // 
            this.gridColumnAssociation.Caption = "Associated";
            this.gridColumnAssociation.FieldName = "associated";
            this.gridColumnAssociation.Name = "gridColumnAssociation";
            this.gridColumnAssociation.Visible = true;
            this.gridColumnAssociation.VisibleIndex = 0;
            this.gridColumnAssociation.Width = 70;
            // 
            // gridColumnExt
            // 
            this.gridColumnExt.Caption = "Ext.";
            this.gridColumnExt.FieldName = "ext";
            this.gridColumnExt.Name = "gridColumnExt";
            this.gridColumnExt.Visible = true;
            this.gridColumnExt.VisibleIndex = 1;
            this.gridColumnExt.Width = 178;
            // 
            // simpleButtonSelectAll
            // 
            this.simpleButtonSelectAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.simpleButtonSelectAll.Location = new System.Drawing.Point(8, 312);
            this.simpleButtonSelectAll.Name = "simpleButtonSelectAll";
            this.simpleButtonSelectAll.Size = new System.Drawing.Size(75, 23);
            this.simpleButtonSelectAll.TabIndex = 1;
            this.simpleButtonSelectAll.Text = "Select All";
            this.simpleButtonSelectAll.Click += new System.EventHandler(this.simpleButtonSelectAll_Click);
            // 
            // simpleButtonApply
            // 
            this.simpleButtonApply.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButtonApply.Location = new System.Drawing.Point(132, 312);
            this.simpleButtonApply.Name = "simpleButtonApply";
            this.simpleButtonApply.Size = new System.Drawing.Size(137, 23);
            this.simpleButtonApply.TabIndex = 2;
            this.simpleButtonApply.Text = "Apply file association";
            this.simpleButtonApply.Click += new System.EventHandler(this.simpleButtonApply_Click);
            // 
            // ControlFileAssociation
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Controls.Add(this.simpleButtonApply);
            this.Controls.Add(this.simpleButtonSelectAll);
            this.Controls.Add(this.gridControl1);
            this.Name = "ControlFileAssociation";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(277, 343);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnAssociation;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumnExt;
        private DevExpress.XtraEditors.SimpleButton simpleButtonSelectAll;
        private DevExpress.XtraEditors.SimpleButton simpleButtonApply;
    }
}
