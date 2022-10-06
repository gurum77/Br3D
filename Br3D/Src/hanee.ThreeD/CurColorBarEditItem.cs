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

        List<Color> comboColors = new List<Color>();
        // 처음은 bylayer, 마지막 앞은 사용자 color, 마지막은 more colors
        public List<Color> defaultColorTable
        {
            get
            {
                var colors = new List<Color>() { Color.Red, Color.Yellow, Color.Green, Color.Cyan, Color.Blue, Color.Magenta, Color.White };

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
            // 마지막은  more colors
            else if (idx == repositoryItemImageComboBoxCurColor.Items.Count-1)
                e.DisplayText = LanguageHelper.Tr("More colors");
        }

        // cur color 콤보 변경시
        private void RepositoryItemImageComboBoxCurColor_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.EditValue != null)
            {
                var idx = (int)(this.EditValue);
                
                var newColor = Options.Instance.currentColor;
                var newColorMethodType = Options.Instance.currentColorMethodType;

                if (idx == 0)
                {
                    newColorMethodType = colorMethodType.byLayer;
                }
                else
                {
                    newColorMethodType = colorMethodType.byEntity;
                    if (idx < repositoryItemImageComboBoxCurColor.Items.Count - 1)
                    {
                        // 현재 item의 color
                        var imageList = repositoryItemImageComboBoxCurColor.SmallImages as ImageList;
                        if (imageList == null || imageList.Images.Count <= idx)
                            return;
                        
                        var image = imageList.Images[idx] as Bitmap;
                        if (image == null)
                            return;
                        var pixel = image.GetPixel(image.Width / 2, image.Height / 2);
                        if (pixel == null)
                            return;

                        newColor = Color.FromArgb(pixel.ToArgb());
                    }
                    // 마지막은 custom color
                    else
                    {
                        var colorDialog1 = new ColorDialog();
                        colorDialog1.Color = Options.Instance.currentColor;
                        if (colorDialog1.ShowDialog() == DialogResult.OK)
                            newColor = colorDialog1.Color;
                    }
                }

                var selectedEntities = model.GetAllSelectedEntities();
                if (selectedEntities != null && selectedEntities.Count > 0)
                {
                    foreach (var ent in selectedEntities)
                    {
                        if (newColorMethodType == colorMethodType.byEntity)
                            ent.Color = newColor;
                        ent.ColorMethod = newColorMethodType;
                    }
                }
                else
                {
                    Options.Instance.currentColor = newColor;
                    Options.Instance.currentColorMethodType = newColorMethodType;
                }

                UpdateCombo(null);
            }
        }

        public void UpdateCombo(List<Entity> entities)
        {
            repositoryItemImageComboBoxCurColor.Items.Clear();
            var imagesColors = new ImageList();
            repositoryItemImageComboBoxCurColor.SmallImages = imagesColors;


            // by layer
            var layer = model.Layers[Options.Instance.currentLayerName];
            AddColorComboItem(imagesColors, layer.Color);

            // 기본 color table 콤보 아이템 추가
            foreach (Color color in defaultColorTable)
                AddColorComboItem(imagesColors, color);


            var curIdx = -1;
            if (entities != null && entities.Count > 0)
            {
                if(entities.Count == 1)
                {
                    if (entities[0].ColorMethod == colorMethodType.byLayer)
                        curIdx = 0;
                    else
                    {
                        curIdx = defaultColorTable.FindLastIndex(x => x == entities[0].Color);

                        // 객체의 색상이 없으면 마지막에 추가한다.
                        if (curIdx < 0)
                        {
                            AddColorComboItem(imagesColors, entities[0].Color);
                            curIdx = repositoryItemImageComboBoxCurColor.Items.Count - 1;
                        }
                        else
                        {
                            // 첫번째는 bylayer이므로 +1을 해야함
                            curIdx++;
                        }
                    }
                }
                else
                {
                    curIdx = -1;
                }
              
            }
            else
            {
                if (Options.Instance.currentColorMethodType == colorMethodType.byLayer)
                    curIdx = 0;
                else
                {
                    curIdx = defaultColorTable.FindLastIndex(x => x == Options.Instance.currentColor);

                    // 현재 색상이 없으면 마지막에 추가한다.
                    if (curIdx < 0)
                    {
                        AddColorComboItem(imagesColors, Options.Instance.currentColor);
                        curIdx = repositoryItemImageComboBoxCurColor.Items.Count - 1;
                    }
                    else
                    {
                        // 첫번째는 bylayer이므로 +1을 해야함
                        curIdx++;
                    }
                }
            }

            // more color combo 추가
            AddColorComboItem(imagesColors, Color.Transparent, true);

            if (curIdx > -1)
                this.EditValue = curIdx;
            else
                this.EditValue = null;
        }

        // color combo  item 추가
        private void AddColorComboItem(ImageList images, Color color, bool moreColor=false)
        {
            var count = repositoryItemImageComboBoxCurColor.Items.Count;
            if(count == 0)
                repositoryItemImageComboBoxCurColor.Items.Add("ByLayer", count, count);
            else
                repositoryItemImageComboBoxCurColor.Items.Add(color.Name, count, count);

            AddColorImage(images, color, moreColor);
        }

        // image list에 color를 추가한다.
        private void AddColorImage(ImageList images, Color color, bool moreColor)
        {
            int iWidth = 16;
            int iHeight = 16;

            var bmp = new Bitmap(iWidth, iHeight);

            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.FillRectangle(new SolidBrush(color), 1, 1, iWidth - 2, iHeight - 2);
                g.DrawRectangle(new Pen(Color.Black, 2), 0, 0, iWidth, iHeight);
                if (moreColor)
                {
                    var gap = 2;
                    g.DrawRectangle(new Pen(Color.Red, gap), gap, gap, iWidth - gap * 2, iHeight - gap * 2);
                    g.DrawRectangle(new Pen(Color.Yellow, gap * 2), gap * 2, gap * 2, iWidth - gap * 4, iHeight - gap * 4);
                }
            }
            images.Images.Add(bmp);
        }
    }
}
