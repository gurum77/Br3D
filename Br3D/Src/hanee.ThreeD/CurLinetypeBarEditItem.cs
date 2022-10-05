using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using System;
using System.Collections.Generic;

namespace hanee.ThreeD
{
    public class CurLinetypeBarEditItem : BarEditItem
    {
        Model model { get; set; }
        public void Init(Model model)
        {
            this.model = model;

            repositoryItemImageComboBoxCurLinetype.CustomItemDisplayText += RepositoryItemImageComboBoxCurLinetype_CustomItemDisplayText;
            repositoryItemImageComboBoxCurLinetype.SelectedIndexChanged += RepositoryItemImageComboBoxCurLinetype_SelectedIndexChanged;
        }

        DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox repositoryItemImageComboBoxCurLinetype
        {
            get
            {
                return this.Edit as DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox;
            }
        }

        // 처음은 bylayer
        List<string> linetypeTable
        {
            get
            {
                var linetypes = new List<string>();
                linetypes.Add("ByLayer");
                linetypes.Add("Continuous");    // 기본 linetype(null) 신)
                foreach (var lt in model.LineTypes)
                    linetypes.Add(lt.Name);


                return linetypes;
            }
        }


        // cur linetype combo changed
        private void RepositoryItemImageComboBoxCurLinetype_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.EditValue != null)
            {
                var idx = (int)(this.EditValue);
                if (idx == 0)
                {
                    Options.Instance.currentLinetypeMethodType = colorMethodType.byLayer;
                }
                else
                {
                    Options.Instance.currentLinetypeMethodType = colorMethodType.byEntity;
                    if (idx == 1)
                    {
                        Options.Instance.currentLinetype = null;
                    }
                    else
                    {
                        Options.Instance.currentLinetype = model.LineTypes[idx - 2].Name;
                    }
                }

            }
        }

        // cur linetype custom display text
        private void RepositoryItemImageComboBoxCurLinetype_CustomItemDisplayText(object sender, CustomItemDisplayTextEventArgs e)
        {
            var item = e.Value as ImageComboBoxItem;
            if (item == null)
                return;

            var idx = (int)item.Value;
            if (idx == 0)
                e.DisplayText = "ByLayer";
            else if (idx == 1)
                e.DisplayText = "Continuous";
            else
            {
                var lt = model.LineTypes[idx - 2];
                e.DisplayText = $"{lt.Name} {lt.Description}";
            }
        }

        public void UpdateCombo(Entity entity)
        {
            repositoryItemImageComboBoxCurLinetype.Items.Clear();
            for (int i = 0; i < linetypeTable.Count; i++)
            {
                var lt = linetypeTable[i];
                repositoryItemImageComboBoxCurLinetype.Items.Add(lt, i, i);
            }

            if(entity != null)
            {
                if (entity.LineTypeMethod == colorMethodType.byLayer)
                    this.EditValue = 0;
                else
                {
                    if(!string.IsNullOrEmpty(entity.LineTypeName))
                    {
                        var idx = linetypeTable.FindIndex(x => x == entity.LineTypeName);
                        if (idx > 0)
                            this.EditValue = idx;
                    }
                }
            }
            else
            {
                if (Options.Instance.currentLinetypeMethodType == colorMethodType.byLayer)
                {
                    this.EditValue = 0;
                }
                else
                {
                    var idx = linetypeTable.FindIndex(x => x == Options.Instance.currentLinetype);
                    if (idx > 0)
                        this.EditValue = idx;
                }
            }
            
        }
    }
}
