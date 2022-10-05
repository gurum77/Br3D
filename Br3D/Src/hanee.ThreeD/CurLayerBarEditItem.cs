using devDept.Eyeshot;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    public class CurLayerBarEditItem : BarEditItem
    {
        Model model { get; set; }
        public void Init(Model model)
        {
            this.model = model;

            repositoryItemImageComboBoxCurLayer.CustomItemDisplayText += RepositoryItemImageComboBoxCurLayer_CustomItemDisplayText;
            repositoryItemImageComboBoxCurLayer.SelectedIndexChanged += RepositoryItemImageComboBoxCurLayer_SelectedIndexChanged;
        }

        public DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBoxCurLayer
        {
            get
            {
                return this.Edit as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            }
        }


        // cur layer 콤보 변경시
        private void RepositoryItemImageComboBoxCurLayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.EditValue is Layer la)
            {
                Options.Instance.currentLayerName = la.Name;
            }
        }

        private void RepositoryItemImageComboBoxCurLayer_CustomItemDisplayText(object sender, CustomItemDisplayTextEventArgs e)
        {
            var item = e.Value as ImageComboBoxItem;
            if (item == null)
                return;

            var la = item.Value as Layer;
            if (la == null)
                return;

            e.DisplayText = la.Name;
        }

        public void UpdateCombo(devDept.Eyeshot.Entities.Entity entity)
        {
            // 이미지만들기
            var imagesColors = new ImageList();
            int iWidth = 16;
            int iHeight = 16;
            foreach (var la in model.Layers)
            {
                var bmp = new Bitmap(iWidth, iHeight);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.DrawRectangle(new Pen(Color.Black, 2), 0, 0, iWidth, iHeight);
                    g.FillRectangle(new SolidBrush(la.Color), 1, 1, iWidth - 2, iHeight - 2);

                }
                imagesColors.Images.Add(bmp);
            }

            repositoryItemImageComboBoxCurLayer.SmallImages = imagesColors;


            repositoryItemImageComboBoxCurLayer.Items.Clear();
            for (int i = 0; i < model.Layers.Count; i++)
            {
                Layer la = model.Layers[i];
                repositoryItemImageComboBoxCurLayer.Items.Add(la.Name, la, i);
            }

            if (entity != null)
                this.EditValue = model.Layers[entity.LayerName];
            else if (model.Layers.Count > 0)
            {
                var layer = model.Layers[Options.Instance.currentLayerName];
                this.EditValue = layer;

            }
        }
    }
}
