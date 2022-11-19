using Br3D.Actions;
using hanee.ThreeD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br3D
{
    static public class MRUManager
    {
        static DevExpress.XtraBars.PopupMenu popuMenu;
        static public void Init(DevExpress.XtraBars.PopupMenu popuMenu)
        {
            MRUManager.popuMenu = popuMenu;
            Load();
         
        }

        static public void Load()
        {
            foreach (var f in Options.Instance.recentFiles)
            {
                AddItem(f);
            }
        }

        static public void Save()
        {
            Options.Instance.recentFiles.Clear();
            foreach (DevExpress.XtraBars.BarItemLink f in popuMenu.ItemLinks)
            {
                Options.Instance.recentFiles.Add(f.Caption);
            }
        }


        static public void AddItem(string itemText)
        {
            // 이미 같은게 있으면 제거
            var existItem = popuMenu.ItemLinks.FirstOrDefault(x => x.Caption.Equals(itemText));
            if (existItem != null)
            {
                popuMenu.ItemLinks.Remove(existItem);
            }

            var item = new DevExpress.XtraBars.BarButtonItem();
            item.Caption = itemText;
            item.ItemClick += Item_ItemClick;
            popuMenu.ItemLinks.Insert(0, item);

            // 최대 갯수를 넘으면 마지막꺼 하나 제거
            if(popuMenu.ItemLinks.Count > 10)
            {
                popuMenu.ItemLinks.RemoveAt(popuMenu.ItemLinks.Count - 1);
            }
        }

        private static void Item_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ActionOpen.fileName = e.Item.Caption;
            CmdBarManager.RunCommand("open");
        }
    }
}
