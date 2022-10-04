using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    public class CurColorBarEditItem : BarEditItem
    {
        Model model { get; set; }
        public void Init(Model model)
        {
            this.model = model;

            repositoryItemImageComboBoxCurColor.CustomItemDisplayText += RepositoryItemImageComboBoxCurColor_CustomItemDisplayText;
            repositoryItemImageComboBoxCurColor.SelectedIndexChanged += RepositoryItemImageComboBoxCurColor_SelectedIndexChanged;

        }

        DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBoxCurColor
        {
            get
            {
                return this.Edit as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            }
        }

        // 처음은 bylayer, 마지막 앞은 사용자 color, 마지막은 more colors
        public List<Color> colorTable
        {
            get
            {
                var curLayer = model.Layers[0];
                if (model.Layers.Contains(Options.Instance.currentLayerName))
                {
                    curLayer = model.Layers[Options.Instance.currentLayerName];
                }

                var colors = new List<Color>() { curLayer.Color, Color.Red, Color.Yellow, Color.Green, Color.Cyan, Color.Blue, Color.Magenta, Color.White };

                // custom이 없으면 추가한다.
                var idx = colors.FindLastIndex(x => x == Options.Instance.currentColor);
                if (idx < 1)
                {
                    colors.Add(Options.Instance.currentColor);
                }

                // 마지막은 more colors
                colors.Add(Color.Transparent);

                return colors;


            }
        }

        // cur color custom display text
        private void RepositoryItemImageComboBoxCurColor_CustomItemDisplayText(object sender, CustomItemDisplayTextEventArgs e)
        {
            var item = e.Value as ImageComboBoxItem;
            if (item == null)
                return;

            var idx = (int)item.Value;
            if (idx == 0)
                e.DisplayText = "ByLayer";
            else if (idx == colorTable.Count - 1)
                e.DisplayText = LanguageHelper.Tr("More colors");
        }

        // cur color 콤보 변경시
        private void RepositoryItemImageComboBoxCurColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.EditValue != null)
            {
                var colors = colorTable;
                var idx = (int)(this.EditValue);
                if (idx == 0)
                {
                    Options.Instance.currentColorMethodType = colorMethodType.byLayer;
                }
                else
                {
                    Options.Instance.currentColorMethodType = colorMethodType.byEntity;
                    if (idx < colors.Count - 1)
                    {
                        Options.Instance.currentColor = colors[idx];
                    }
                    // 마지막은 custom color
                    else
                    {
                        var colorDialog1 = new ColorDialog();
                        colorDialog1.Color = Options.Instance.currentColor;
                        if (colorDialog1.ShowDialog() == DialogResult.OK)
                        {
                            Options.Instance.currentColor = colorDialog1.Color;
                            UpdateCombo();
                        }
                    }
                }
            }
        }

        public void UpdateCombo()
        {
            // 이미지만들기
            var imagesColors = new ImageList();
            int iWidth = 16;
            int iHeight = 16;
            var colors = colorTable;
            for (int i = 0; i < colors.Count; i++)
            {
                Color color = colors[i];
                var bmp = new Bitmap(iWidth, iHeight);
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.FillRectangle(new SolidBrush(color), 1, 1, iWidth - 2, iHeight - 2);
                    g.DrawRectangle(new Pen(Color.Black, 2), 0, 0, iWidth, iHeight);
                    if (i == colors.Count - 1)
                    {
                        var gap = 2;
                        g.DrawRectangle(new Pen(Color.Red, gap), gap, gap, iWidth - gap * 2, iHeight - gap * 2);
                        g.DrawRectangle(new Pen(Color.Yellow, gap * 2), gap * 2, gap * 2, iWidth - gap * 4, iHeight - gap * 4);
                    }
                }
                imagesColors.Images.Add(bmp);
            }

            repositoryItemImageComboBoxCurColor.Items.Clear();
            for (int i = 0; i < colorTable.Count; i++)
            {
                var color = colorTable[i];
                repositoryItemImageComboBoxCurColor.Items.Add(color.Name, i, i);
            }

            repositoryItemImageComboBoxCurColor.SmallImages = imagesColors;
            if (Options.Instance.currentColorMethodType == colorMethodType.byLayer)
            {
                this.EditValue = 0;
            }
            else
            {
                var idx = colorTable.FindIndex(x => x == Options.Instance.currentColor);
                if (idx > 0)
                    this.EditValue = idx;
            }
        }
    }
}
