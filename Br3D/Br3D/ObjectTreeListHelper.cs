
using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Br3D
{
    public static class ObjectTreeListHelper
    {
        async static public void RegenAsync(DevExpress.XtraTreeList.TreeList treeList, devDept.Eyeshot.Model model, bool isDwg)
        {
            SetOptionsAsElementTreeList(treeList);
            treeList.TreeViewFieldName = "Name";
            var options = await RunTaskGenerateDataSourceAsync(model, isDwg);

            treeList.DataSource = options;
            treeList.ForceInitialize();

            foreach (TreeListNode node in treeList.Nodes)
            {
                node.ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;
                SetChildrenCheckBoxStyle(node);
            }

            // tag 연결
            foreach (var op in options)
            {
                var node = treeList.FindNodeByID(op.ID);
                if (node == null)
                    continue;
                node.Tag = op.Tag;
                node.Checked = op.Checked;
            }

            treeList.ExpandToLevel(0);
        }

        static public List<TreeListNode> GetAllSubNodes(this TreeListNode node)
        {
            List<TreeListNode> nodes = new List<TreeListNode>();
            nodes.Add(node);

            foreach (TreeListNode subNode in node.Nodes)
            {
                var subNodes = GetAllSubNodes(subNode);
                if (subNodes == null || subNodes.Count == 0)
                    continue;
                nodes.AddRange(subNodes);
            }
            return nodes;
        }

        static Task<BindingList<TreeListNodeOption>> RunTaskGenerateDataSourceAsync(devDept.Eyeshot.Model model, bool isDwg)
        {
            return Task.Run(() => GenerateDataSource(model, isDwg));
        }
        // element를 tree로 만들기 위한 data source를 만든다.
        static private BindingList<TreeListNodeOption> GenerateDataSource(devDept.Eyeshot.Model model, bool isDwg)
        {
            if (isDwg)
            {
                return GenerateDataSourceDwg(model);
            }
            else
            {
                return GenerateDataSource3D(model);
            }

        }
        static BindingList<TreeListNodeOption> ConvertTreeDataToTreeOptions(TreeData rootData)
        {
            // TreeListNodeOption으로 변환
            // 즉, 처음 정했던 id가 바뀔 수 있다.
            // 변경되는 id 정보를 보관했다가 parent id에 변경 id를 적용해준다.
            var allData = TreeData.GetAllTreeData(rootData);

            BindingList<TreeListNodeOption> options = new BindingList<TreeListNodeOption>();
            int realID = 0;
            var idChangeInfo = new Dictionary<int, int>();
            foreach (TreeData data in allData)
            {
                if (data.ID == -1)
                    continue;

                // 실제로 반영되는 id
                idChangeInfo.Add(data.ID, realID);

                // 속성이 이미 있는지 찾는다.
                TreeListNodeOption option = new TreeListNodeOption();
                option.ParentID = data.ParentID;
                option.ID = realID;
                option.Name = data.Name;
                option.Checked = true;
                option.Tag = data.Tag;
                options.Add(option);

                realID++;
            }

            // 변경된 id로 원래 parent id를 변경해준다.
            foreach (var option in options)
            {
                if (!idChangeInfo.ContainsKey(option.ParentID))
                    continue;

                option.ParentID = idChangeInfo[option.ParentID];
            }

            return options;
        }

        // dwg는 속성만 트리로 구성한다.
        private static BindingList<TreeListNodeOption> GenerateDataSourceDwg(Model model)
        {
            int id = -1;
            TreeData rootData = new TreeData(id++, -1, "root");

            // 모든 layer
            foreach (var item in model.Layers)
            {
                List<string> properties = GetTreeNodeProperties(item);
                if (properties == null || properties.Count == 0)
                    continue;

                AddTreeNode(rootData, properties, item, ref id);
            }

            // 모든 linetype
            foreach (var item in model.LineTypes)
            {
                List<string> properties = GetTreeNodeProperties(item);
                if (properties == null || properties.Count == 0)
                    continue;

                AddTreeNode(rootData, properties, item, ref id);
            }

            // 모든 hatch patterns
            foreach (var item in model.HatchPatterns)
            {
                List<string> properties = GetTreeNodeProperties(item);
                if (properties == null || properties.Count == 0)
                    continue;

                AddTreeNode(rootData, properties, item, ref id);
            }

            // 모든 textstyle
            foreach (var item in model.TextStyles)
            {
                List<string> properties = GetTreeNodeProperties(item);
                if (properties == null || properties.Count == 0)
                    continue;

                AddTreeNode(rootData, properties, item, ref id);
            }

            // 모든 block
            foreach (var item in model.Blocks)
            {
                List<string> properties = GetTreeNodeProperties(item);
                if (properties == null || properties.Count == 0)
                    continue;

                AddTreeNode(rootData, properties, item, ref id);
            }
            return ConvertTreeDataToTreeOptions(rootData);
        }

        private static BindingList<TreeListNodeOption> GenerateDataSource3D(Model model)
        {
            int id = -1;
            TreeData rootData = new TreeData(id++, -1, "root");

            // 모든 entity의 property를 가져옴
            for (int i = 0; i < model.Entities.Count; ++i)
            {
                var ele = model.Entities[i];


                List<string> properties = GetTreeNodeProperties(ele);
                if (properties == null || properties.Count == 0)
                    continue;

                AddTreeNode(rootData, properties, ele, ref id);
            }

            return ConvertTreeDataToTreeOptions(rootData);
        }

        static private void AddTreeNode(TreeData rootData, List<string> listProperties, object tag, ref int id)
        {
            TreeData curData = rootData;

            for (int i = 0; i < listProperties.Count; i++)
            {
                string prop = listProperties[i];
                if (prop == null)
                    continue;

                // 마지막 노드가 아닌경우 
                if (i < listProperties.Count - 1)
                {
                    // 없으면 만든다.
                    if (!curData.dic.TryGetValue(prop, out TreeData tmpData))
                    {
                        tmpData = new TreeData(id++, curData.ID, prop);
                        curData.dic[prop] = tmpData;
                        curData = tmpData;
                    }
                    else
                    {
                        curData = tmpData;
                    }
                }
                // 마지막 노드는 무조건 추가
                else
                {
                    // 마지막 노드가 이미 있다면 새로운 이름으로 만들어야 한다.
                    var newProp = prop;
                    int num = 1;
                    while (curData.dic.ContainsKey(newProp))
                    {
                        newProp = $"{prop}{num++}";
                    }

                    var newData = new TreeData(id++, curData.ID, newProp);
                    newData.Tag = tag;
                    curData.dic[newProp] = newData;
                    curData = newData;
                }
            }
        }

        static public void SetChildrenCheckBoxStyle(TreeListNode node)
        {
            if (node == null)
                return;

            foreach (TreeListNode childnode in node.Nodes)
            {
                childnode.ChildrenCheckBoxStyle = NodeCheckBoxStyle.Check;

                if (childnode.Nodes != null)
                    SetChildrenCheckBoxStyle(childnode);
            }
        }

        static public void SetOptionsAsElementTreeList(DevExpress.XtraTreeList.TreeList treeList)
        {
            treeList.OptionsView.RootCheckBoxStyle = NodeCheckBoxStyle.Check;
            treeList.OptionsView.FocusRectStyle = DrawFocusRectStyle.RowFullFocus;
            treeList.OptionsBehavior.ReadOnly = false;
            treeList.OptionsBehavior.Editable = false;
            treeList.OptionsBehavior.AllowRecursiveNodeChecking = true;
            treeList.OptionsSelection.EnableAppearanceFocusedRow = true;
            treeList.OptionsSelection.KeepSelectedOnClick = true;
        }

        private static TreeListNode NewNode(TreeListNodes nodes, string prop, devDept.Eyeshot.Entities.Entity ent)
        {
            var node = nodes.Add(prop);
            node.SetValue(nodes.TreeList.Columns[0], prop);
            node.Tag = ent;

            return node;
        }

        // nodes에서 prop 이름을 가진 노드를 찾는다.
        private static TreeListNode GetNode(TreeListNodes nodes, string prop)
        {
            var firstColumn = nodes.TreeList.Columns[0];
            foreach (TreeListNode node in nodes)
            {
                var text = node.GetDisplayText(firstColumn);
                if (text == prop)
                    return node;
            }

            return NewNode(nodes, prop, null);
        }

        // 객체의 tree node 속성 리턴
        private static List<string> GetTreeNodeProperties(object ent)
        {
            List<string> properties = new List<string>();
            properties.Add("Root");
            if (ent is devDept.Eyeshot.Translators.IEyeIfcObject)
            {
                var ifc = ent as devDept.Eyeshot.Translators.IEyeIfcObject;
                if (ifc != null)
                {
                    string[] keys = new string[] { "KeyWord", "Name" };
                    foreach (var key in keys)
                    {
                        if (ifc.Identification.TryGetValue(key, out string val))
                            properties.Add(val);
                    }

                }
            }
            else if (ent is BlockReference)
            {
                properties.Add("Block");
                properties.Add(((BlockReference)ent).BlockName);
            }
            else if (ent is Entity)
            {

                string type = ent.GetType().ToString().Split('.').LastOrDefault();
                properties.Add(type);
                properties.Add(type);   // 마지막 노드는 이름이 같으면 번호를 자동으로 붙인다.
            }
            else if (ent is Layer)
            {
                properties.Add("Layer");
                properties.Add(((Layer)ent).Name);
            }
            else if (ent is Block)
            {
                properties.Add("Block");
                properties.Add(((Block)ent).Name);
            }
            else if (ent is LineType)
            {
                properties.Add("LineType");
                properties.Add(((LineType)ent).Name);
            }
            else if (ent is HatchPattern)
            {
                properties.Add("HatchPattern");
                properties.Add(((HatchPattern)ent).Name);
            }
            else if (ent is TextStyle)
            {
                properties.Add("TextStyle");
                properties.Add(((TextStyle)ent).Name);
            }


            // .Ifc 문자가 있으면 Ifc 이후 문자만 취한다.
            foreach (var prop in properties)
            {
                var idx = prop.IndexOf(".Ifc");
                if (idx < 0)
                    continue;
            }

            return properties;
        }
    }
}
