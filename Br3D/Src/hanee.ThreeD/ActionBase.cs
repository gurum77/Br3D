using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry.Entities;
// 

using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
//TODO devDept 2022: Eyeshot.Environment class has been renamed in Eyeshot.Workspace.
//using Environment = devDept.Eyeshot.Workspace;
using Workspace = devDept.Eyeshot.Workspace;
using Label = devDept.Eyeshot.Labels.Label;
namespace hanee.ThreeD
{
    abstract public class ActionBase
    {
        enum UserInput
        {
            GettingPoint3D, // Point3D 를 입력받고 있는지?
            GettingPoint,   // Point를 입력받고 있는지?
            SelectingLabel, // label을 선택받는다.
            SelectingEntity,    // 객체/면등을.. 선택하고 있는지?
            SelectingSubEntity,    // 서브 객체를 선택하고 있는지?
            SelectingFace,
            SelectingEdge,
            
            GettingKey,    // key 를 입력받는다.
            Count
        }

        // 스냅 간격
        static public float Snap = 0.001f;
        static public void ModifyPointBySnap(ref Point3D point3D)
        {
            if (Point3D == null)
                return;

            if (point3D == null)
                return;

            if (ActionBase.Snap == 0)
                return;

            point3D.X = (int)(point3D.X / ActionBase.Snap) * Snap;
            point3D.Y = (int)(point3D.Y / ActionBase.Snap) * Snap;
            point3D.Z = (int)(point3D.Z / ActionBase.Snap) * Snap;
        }

        // end action할때 unselect all을 할지?
        static public bool IsUnselectAllOnEndAction = true;

        // 문서가 편집되었는지?
        // action이 끝나면 무조건 편집 된것으로 본다.
        static public bool IsModified
        { get; set; }

        static private bool[] userInputting = new bool[(int)UserInput.Count];

        static protected Point3D point3D = new Point3D();
        static public Point3D Point3D
        {
            get { return point3D; }
            set { point3D = value; }
        }

        static protected System.Drawing.Point currentMousePoint = new System.Drawing.Point();
        static public System.Drawing.Point CurrentMousePoint
        {
            get { return currentMousePoint; }
            set { currentMousePoint = value; }
        }


        // 사용자에 의해서 입력중인 마우스 좌표
        static protected System.Drawing.Point point = new System.Drawing.Point();
        static public System.Drawing.Point Point
        {
            get { return point; }
            set { point = value; }
        }

        static protected KeyEventArgs key = new KeyEventArgs(Keys.A);
        static public KeyEventArgs Key
        {
            get { return key; }
            set { key = value; }
        }

        static private Entity selectedEntity = null;
        static private Label selectedLabel = null;
        static private devDept.Eyeshot.Design.SelectedFace selectedFace = null;
        static private devDept.Eyeshot.Design.SelectedEdge selectedEdge = null;

        // 시스템 설정값
        static public SystemValue systemValue = new SystemValue();


        // 미리보기 객체
        static public Entity[] PreviewFaceEntities = null;  // face로 그리는 미리보기 객체들
        static public Entity[] PreviewEntities = null;      // wire로 그리는 미리보기 객체들
        static public Entity previewEntity
        {
            get
            {
                if (PreviewEntities == null || PreviewEntities.Length == 0)
                    return null;

                return PreviewEntities[0];
            }
            set
            {
                PreviewEntities = new Entity[1];
                PreviewEntities[0] = value;
            }
        }

        // 임시 객체를 이동한다.
        static public void MoveTempEtt(devDept.Eyeshot.Design vp, Vector3D vMove)
        {
            for (int i = 0; i < vp.TempEntities.Count(); ++i)
            {
                vp.TempEntities[i].Translate(vMove);
            }
        }

        // 임시 객체를 회전한다.
        static public void RotateTempEtt(devDept.Eyeshot.Design vp, Vector3D fromDir, Vector3D toDir, Point3D centerPoint)
        {
            Transformation trans = new Transformation();
            trans.Rotation(fromDir, toDir, centerPoint);
            for (int i = 0; i < vp.TempEntities.Count(); ++i)
            {
                vp.TempEntities[i].TransformBy(trans);
            }
        }

        // 임시 객체를 설정한다.
        //TODO devDept 2022: Eyeshot.Environment class has been renamed in Eyeshot.Workspace.
        //static public void SetTempEtt(devDept.Eyeshot.Workspace vp, Entity ent, bool initTempEntities = true)
        static public void SetTempEtt(devDept.Eyeshot.Workspace vp, Entity ent, bool initTempEntities = true)
        {
            if (initTempEntities)
            {
                vp.TempEntities.Clear();
            }
            if (vp == null || ent == null)
                return;

            if (ent is BlockReference)
            {
                BlockReference br = ent as BlockReference;
                Entity[] ents = br.Explode(vp.Blocks, true);

                foreach (var subEnt in ents)
                {
                    ActionBase.SetTempEtt(vp, subEnt, false);
                }
            }
            else
            {
                // solid이면 mesh로 변경해서 추가한다.
                if (ent is Solid)
                {
                    Solid s = ent as Solid;
                    try
                    {
                        Mesh m = s.ConvertToMesh();
                        ActionBase.SetTempEtt(vp, m, false);
                    }
                    catch
                    {
                        s.Regen(0.01);
                        Mesh m = s.ConvertToMesh();
                        ActionBase.SetTempEtt(vp, m, false);
                    }

                }
                else if (ent is Brep)
                {
                    Brep s = ent as Brep;
                    try
                    {
                        Mesh m = s.ConvertToMesh();
                        ActionBase.SetTempEtt(vp, m, false);
                    }
                    catch
                    {
                        s.Regen(0.01);
                        Mesh m = s.ConvertToMesh();
                        ActionBase.SetTempEtt(vp, m, false);
                    }

                }
                else if (ent is devDept.Eyeshot.Entities.Region)
                {
                    devDept.Eyeshot.Entities.Region s = ent as devDept.Eyeshot.Entities.Region;
                    try
                    {
                        Mesh m = s.ConvertToMesh();
                        ActionBase.SetTempEtt(vp, m, false);
                    }
                    catch
                    {
                        s.Regen(0.01);
                        Mesh m = s.ConvertToMesh();
                        ActionBase.SetTempEtt(vp, m, false);
                    }
                }
                else if (ent is IFace/* || ent is ICurve*/)
                {
                    string type = ent.GetType().ToString();
                    Color col = Color.FromArgb(50, ent.Color.R, ent.Color.G, ent.Color.B);
                    ent.Color = col;
                    vp.TempEntities.Add(ent);
                }
                else if (ent is ICurve)
                {
                    string type = ent.GetType().ToString();
                    Color col = Color.FromArgb(50, ent.Color.R, ent.Color.G, ent.Color.B);
                    ent.Color = col;
                    ent.Regen(0.01);
                    vp.TempEntities.Add(ent);
                }
            }
        }

        // 현재 실행중인 액션
        static public ActionBase runningAction = null;

        // 작업 평면
        //static private WorkingPlane workingPlane = new WorkingPlane();
        //static public WorkingPlane WorkingPlane
        //{
        //    get { return workingPlane; }
        //    set { workingPlane = value; }
        //}

        // 현재 step이 중지 된경우
        static private bool IsStopedCurrentStep
        {
            get
            {
                return Canceled || Entered ? true : false;
            }
            set
            {
                Canceled = false;
                Entered = false;
            }
        }
        // 명령이 취소된 경우 
        static public bool Canceled = false;

        // enter가 입력된 경우(다음 step으로 진행하는 의미)
        static public bool Entered = false;

        // 사용자 입력중인지?
        static public bool IsUserInputting()
        {
            // 실행중인 액션이 없으면 사용자 입력이 아님
            // 액션시작할때 StartAction 함수에서 설정된다.
            if (ActionBase.runningAction == null)
                return false;

            for (int i = 0; i < (int)UserInput.Count; ++i)
            {
                if (userInputting[i] == true)
                    return true;
            }


            return false;
        }

        // snap찾기가 필요한지?
        static public bool IsNeedSnapping()
        {
            if (ActionBase.userInputting[(int)ActionBase.UserInput.GettingPoint3D] == true)
                return true;

            return false;
        }

        // 커서 메세지
        static public string cursorText;

        // 액션에서 현재 스탭 ID
        static public int StepID
        { get; set; }

        // select시 dynamic highlight를 할지?
        static bool dynamicHighlight = true;
        static public devDept.Eyeshot.Design.SelectedItem LastSelectedItem = null;
        static Dictionary<Type, bool> selectableType = new Dictionary<Type, bool>();


        // 객체를 선택중인지?
        static public bool IsSelectingEntity()
        {
            if (userInputting[(int)UserInput.SelectingSubEntity] ||
                userInputting[(int)UserInput.SelectingEntity] ||
                userInputting[(int)UserInput.SelectingLabel])
                return true;

            return false;
        }
        // mouse move 이벤트 처리
        static public void MouseMoveHandler(Workspace vp, MouseEventArgs e)
        {
            if (ActionBase.currentMousePoint == e.Location)
                return;

            // 현재 마우스 좌표는 스냅찾을때 사용되므로 mouse move 때마다 설정한다.
            ActionBase.currentMousePoint = e.Location;

            if (userInputting[(int)UserInput.GettingPoint] == true)
                SetPointByMouseEventArgs(vp, e);
            if (userInputting[(int)UserInput.GettingPoint3D] == true)
                SetPoint3DByMouseEventArgs(vp, e);

            // 객체 선택중이고 dynamic highlight 해야하는 경우
            if (IsSelectingEntity() && dynamicHighlight && e.Button != MouseButtons.Middle)
            {
                vp.SetCurrent(null);

                devDept.Eyeshot.Design.SelectedItem item = vp.GetItemUnderMouseCursor(e.Location);
                if (item != null && item.Item != null && selectableType != null && selectableType.Count() > 0 && !selectableType.ContainsKey(item.Item.GetType()))
                {
                    item = null;
                }

                // sub entity 선택중이면..
                if (item != null && userInputting[(int)UserInput.SelectingSubEntity])
                {
                    // sub 객체를 탐색한다.
                    while (item.Item is BlockReference)
                    {
                        try
                        {
                            BlockReference br = item.Item as BlockReference;
                            if (vp.Blocks.Contains(br.BlockName))
                                vp.SetCurrent(item.Item as BlockReference);
                            item = vp.GetItemUnderMouseCursor(e.Location);
                            if (item == null)
                                break;
                        }
                        catch
                        {
                            break;
                        }

                    }
                }

                // 현재 선택된 item이 마지막 선택과 다르면 갱신한다.
                if (LastSelectedItem != item)
                {
                    if (LastSelectedItem != null)
                        LastSelectedItem.Select(vp, false);

                    LastSelectedItem = item;

                    if (LastSelectedItem != null)
                        LastSelectedItem.Select(vp, true);

                    vp.Invalidate();
                }
            }


            if (runningAction != null)
                runningAction.OnMouseMove(vp, e);
        }

        // mouse event args로 Point 좌표를 설정한다.
        static void SetPointByMouseEventArgs(Workspace ws, MouseEventArgs e)
        {
            point = e.Location;
        }

        // snap을 고려한 3D 좌표 리턴
        static public Point3D GetPoint3DWithSnapping(Workspace ws, MouseEventArgs e)
        {
            // snapPoint 우선
            if (ws is devDept.Eyeshot.Design)
            {
                hanee.ThreeD.HDesign vp = (hanee.ThreeD.HDesign)ws;
                if (vp.Snapping.CurrentlySnapping)
                {
                    return vp.Snapping.GetSnapPoint();
                }
            }
            //else if (viewportLayout is BRDrawings)
            //{
            //    BRDrawings vp = (BRDrawings)viewportLayout;
            //    if (vp.Snapping.CurrentlySnapping)
            //    {
            //        point3D = vp.Snapping.GetSnapPoint();
            //        return;
            //    }
            //}


            return GetPoint3DByMouseLocation(ws, e.Location);
        }
        // mouse event args로 Point3D 좌표를 설정한다.
        static void SetPoint3DByMouseEventArgs(Workspace ws, MouseEventArgs e)
        {
            point3D = GetPoint3DWithSnapping(ws, e);
        }

        // mouse 위치의 Point3D 좌표를 리턴
        static public Point3D GetPoint3DByMouseLocation(Workspace ws, System.Drawing.Point location)
        {
            Point3D point3D = ws.ScreenToWorld(location);
            // null 이면 camere 가 바라보는 평면을 기준으로 좌표를 계산한다
            if(point3D == null)
            {
                Design model = ws as Design;
                if(model != null)
                {
                    ws.ScreenToPlane(location, model.ActiveViewport.Camera.NearPlane, out point3D);
                }
            }
            //if (point3D == null && WorkingPlane != null)
            //{
            //    if (viewportLayout.ScreenToPlane(location, WorkingPlane.Plane, out point3D) != true && point3D != null)
            //    {
            //        ActionBase.ModifyPointBySnap(ref point3D);
            //        return point3D;
            //    }

            //}

            if (point3D != null)
            {
                if (ws.IsTopViewOnly())
                    point3D.Z = 0;

                ActionBase.ModifyPointBySnap(ref point3D);
            }


            return point3D;
        }

        static public void CameraMoveEndHandler(devDept.Eyeshot.Design vp, object sender, devDept.Eyeshot.Design.CameraMoveEventArgs e)
        {

        }
        // selection changed 이벤트 처리
        static public void SelectionChangedHandler(devDept.Eyeshot.Design vp, object sender, devDept.Eyeshot.Design.SelectionChangedEventArgs e)
        {
            // face 선택중인 경우
            if (userInputting[(int)UserInput.SelectingFace] == true)
            {
                foreach (var item in e.AddedItems)
                {
                    if (item is devDept.Eyeshot.Design.SelectedFace)
                    {
                        selectedFace = (devDept.Eyeshot.Design.SelectedFace)item;
                        userInputting[(int)UserInput.SelectingFace] = false;
                    }
                }
            }

            // edge 선택중인 경우
            if (userInputting[(int)UserInput.SelectingEdge] == true)
            {
                foreach (var item in e.AddedItems)
                {
                    if (item is devDept.Eyeshot.Design.SelectedEdge)
                    {
                        selectedEdge = (devDept.Eyeshot.Design.SelectedEdge)item;
                        userInputting[(int)UserInput.SelectingEdge] = false;
                    }
                }
            }
        }

        // mouse click 이벤트 처리
        static public void MouseDownHandler(Workspace ws, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseDownHandler_LeftButton(ws, e);
            }
            else if(e.Button == MouseButtons.Right)
            {
                MouseDownHandler_RightButton(ws, e);
            }

        }

        // 마우스 오른키를 누르면 입력 완료로 처리한다.
        private static void MouseDownHandler_RightButton(Workspace ws, MouseEventArgs e)
        {
            Entered = true;
        }

        private static void MouseDownHandler_LeftButton(Workspace ws, MouseEventArgs e)
        {
            if (userInputting[(int)UserInput.GettingPoint] == true)
            {
                SetPointByMouseEventArgs(ws, e);

                userInputting[(int)UserInput.GettingPoint] = false;
            }

            if (userInputting[(int)UserInput.GettingPoint3D] == true)
            {
                SetPoint3DByMouseEventArgs(ws, e);

                userInputting[(int)UserInput.GettingPoint3D] = false;
            }

            if (userInputting[(int)UserInput.SelectingLabel] == true)
            {
                Design model = ws as Design;
                int idx = ws.GetLabelUnderMouseCursor(e.Location);
                if (model != null && idx > -1 && idx < model.ActiveViewport.Labels.Count)
                {

                    selectedLabel = model.ActiveViewport.Labels[idx];
                    userInputting[(int)UserInput.SelectingLabel] = false;

                }
            }

            if (userInputting[(int)UserInput.SelectingEntity] == true)
            {
                devDept.Eyeshot.Design.SelectedItem item = ws.GetItemUnderMouseCursor(e.Location);
                if (item != null)
                {
                    Entity entityTmp = item.Item as Entity;
                    if (entityTmp != null)
                    {
                        selectedEntity = entityTmp;
                        userInputting[(int)UserInput.SelectingEntity] = false;
                    }
                }
            }


            if (userInputting[(int)UserInput.SelectingSubEntity] == true)
            {
                devDept.Eyeshot.Design.SelectedItem item = ws.GetItemUnderMouseCursor(e.Location);
                if (item != null)
                {
                    // sub 객체를 탐색한다.
                    while (item.Item is BlockReference)
                    {
                        try
                        {
                            BlockReference br = item.Item as BlockReference;
                            if (ws.Blocks.Contains(br.BlockName))
                                ws.SetCurrent(item.Item as BlockReference);
                            item = ws.GetItemUnderMouseCursor(e.Location);
                            if (item == null)
                                break;
                        }
                        catch
                        {


                            break;
                        }

                    }

                    if (item != null)
                    {
                        Entity entityTmp = item.Item as Entity;
                        if (entityTmp != null)
                        {
                            selectedEntity = entityTmp;
                            userInputting[(int)UserInput.SelectingSubEntity] = false;

                            //if(viewportLayout.Entities.CurrentBlockReference != null)
                            //    viewportLayout.Entities.SetCurrent(null);
                        }
                    }
                }
            }
        }

        // key up에 대한 이벤트 핸들러
        static public void KeyUpHandler(KeyEventArgs e)
        {
            // esc는 명령 취소
            if (e.KeyCode == Keys.Escape)
            {
                Canceled = true;
            }
            // space, enter는 다음 step으로 진행
            else if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                Entered = true;
            }

            if (userInputting[(int)UserInput.GettingKey] == true)
            {
                key = e;

                userInputting[(int)UserInput.GettingKey] = false;
            }
        }

        // action에서 mouse move에 대한 처리를 할때 재정의 한다.
        //TODO devDept 2022: Eyeshot.Environment class has been renamed in Eyeshot.Workspace.
        //protected virtual void OnMouseMove(devDept.Eyeshot.Workspace vp, MouseEventArgs e)
        protected virtual void OnMouseMove(devDept.Eyeshot.Workspace vp, MouseEventArgs e)
        {

        }

        #region 사용자 입력 함수

        public async Task<Point2D> GetPoint2D(string message = null, int stepID = -1)
        {
            Point3D pt3D = await GetPoint3D(message, stepID);
            if (pt3D == null)
                return null;

            return new Point2D(pt3D.X, pt3D.Y);
        }

        // 마우스로 point3D를 입력받는다.
        public async Task<Point3D> GetPoint3D(string message = null, int stepID = -1)
        {
            ActionBase.StepID = stepID;
            ActionBase.cursorText = message;
            ActionBase.userInputting[(int)UserInput.GettingPoint3D] = true;
            ActionBase.IsStopedCurrentStep = false;
            while (ActionBase.userInputting[(int)UserInput.GettingPoint3D] == true)
            {
                // 스탭이 중지되었다면 그냥 보낸다.
                if (ActionBase.IsStopedCurrentStep)
                {
                    ActionBase.userInputting[(int)UserInput.GettingPoint3D] = false;
                    break;
                }

                await Task.Delay(100);
            }

            ActionBase.cursorText = null;


            return point3D;
        }
        #endregion


        // 마우스로 point를 입력받는다.
        public async Task<System.Drawing.Point> GetPoint(string message = null, int stepID = -1)
        {
            ActionBase.StepID = StepID;
            ActionBase.cursorText = message;
            ActionBase.userInputting[(int)UserInput.GettingPoint] = true;
            ActionBase.IsStopedCurrentStep = false;
            while (ActionBase.userInputting[(int)UserInput.GettingPoint] == true)
            {
                // 스탭이 중지되었다면 그냥 보낸다.
                if (ActionBase.IsStopedCurrentStep)
                {
                    ActionBase.userInputting[(int)UserInput.GettingPoint] = false;
                    break;
                }

                await Task.Delay(100);
            }

            ActionBase.cursorText = null;
            return point;
        }

        // edge 1개를 선택받는다
        public async Task<devDept.Eyeshot.Design.SelectedEdge> GetEdge(string message = null, int stepID = -1)
        {

            actionType oldActionType = workspace.ActionMode;
            selectionFilterType oldSelectionFilterType = selectionFilterType.Entity;

            devDept.Eyeshot.Design model = workspace as devDept.Eyeshot.Design;
            if (model != null)
            {
                oldSelectionFilterType = model.SelectionFilterMode;
                model.SelectionFilterMode = selectionFilterType.Edge;
            }

            workspace.ActionMode = actionType.SelectVisibleByPickDynamic;

            ActionBase.StepID = StepID;
            ActionBase.cursorText = message;
            ActionBase.userInputting[(int)UserInput.SelectingEdge] = true;
            ActionBase.IsStopedCurrentStep = false;
            while (ActionBase.userInputting[(int)UserInput.SelectingEdge] == true)
            {
                // 스탭이 중지되었다면 그냥 보낸다.
                if (ActionBase.IsStopedCurrentStep)
                {
                    ActionBase.userInputting[(int)UserInput.SelectingEdge] = false;
                    break;
                }

                await Task.Delay(100);
            }

            ActionBase.cursorText = null;
            if (selectedEdge != null)
            {
                selectedEdge.Select(workspace, true);
                workspace.Invalidate();
            }

            if (model != null)
            {
                model.ActionMode = oldActionType;
                model.SelectionFilterMode = oldSelectionFilterType;
            }


            return selectedEdge;
        }

        // face 1개를 선택받는다
        public async Task<devDept.Eyeshot.Design.SelectedFace> GetFace(string message = null, int stepID = -1)
        {
            actionType oldActionType = workspace.ActionMode;
            selectionFilterType oldSelectionFilterType = selectionFilterType.Entity;

            devDept.Eyeshot.Design model = workspace as devDept.Eyeshot.Design;
            if (model != null)
            {
                oldSelectionFilterType = model.SelectionFilterMode;
                model.SelectionFilterMode = selectionFilterType.Face;
            }


            workspace.ActionMode = actionType.SelectVisibleByPickDynamic;


            ActionBase.StepID = StepID;
            ActionBase.cursorText = message;
            ActionBase.userInputting[(int)UserInput.SelectingFace] = true;
            ActionBase.IsStopedCurrentStep = false;
            while (ActionBase.userInputting[(int)UserInput.SelectingFace] == true)
            {
                // 스탭이 중지되었다면 그냥 보낸다.
                if (ActionBase.IsStopedCurrentStep)
                {
                    ActionBase.userInputting[(int)UserInput.SelectingFace] = false;
                    break;
                }

                await Task.Delay(100);
            }

            ActionBase.cursorText = null;
            if (selectedFace != null)
            {
                selectedFace.Item.Selected = true;
                workspace.Invalidate();
            }


            if (model != null)
            {
                model.ActionMode = oldActionType;
                model.SelectionFilterMode = oldSelectionFilterType;
            }


            return selectedFace;
        }

        // 키보드로 char를 입력 받는다.
        public async Task<KeyEventArgs> GetKey(string message = null, int stepID = -1)
        {
            ActionBase.StepID = StepID;
            ActionBase.cursorText = message;
            ActionBase.userInputting[(int)UserInput.GettingKey] = true;
            ActionBase.IsStopedCurrentStep = false;
            while (ActionBase.userInputting[(int)UserInput.GettingKey] == true)
            {
                // 스탭이 중지되었다면 그냥 보낸다.
                if (ActionBase.IsStopedCurrentStep)
                {
                    ActionBase.userInputting[(int)UserInput.GettingKey] = false;
                    break;
                }

                await Task.Delay(100);
            }

            ActionBase.cursorText = null;
            return key;
        }

        // label 1개를 선택받는다. 
        public async Task<Label> GetLabel(string message = null, int stepID = -1, bool dynamicHighlight = false, Dictionary<Type, bool> selectableType = null)
        {
            ActionBase.StepID = StepID;
            ActionBase.cursorText = message;
            ActionBase.userInputting[(int)UserInput.SelectingLabel] = true;
            ActionBase.dynamicHighlight = dynamicHighlight;
            ActionBase.selectableType = selectableType;
            ActionBase.IsStopedCurrentStep = false;

            while (ActionBase.userInputting[(int)UserInput.SelectingLabel] == true)
            {
                // 스탭이 중지되었다면 그냥 보낸다.
                if (ActionBase.IsStopedCurrentStep)
                {
                    ActionBase.userInputting[(int)UserInput.SelectingLabel] = false;
                    break;
                }

                await Task.Delay(100);
            }

            ActionBase.cursorText = null;
            if (selectedLabel != null)
            {
                selectedLabel.Selected = true;
                workspace.Invalidate();
            }

            return selectedLabel;
        }

        // 객체 1개를 선택받거나 키를 입력받는다.
        public async Task<Entity> GetEntity(string message = null, int stepID = -1, bool dynamicHighlight = false, Dictionary<Type, bool> selectableType = null)
        {
            ActionBase.StepID = StepID;
            ActionBase.cursorText = message;
            ActionBase.userInputting[(int)UserInput.SelectingEntity] = true;
            ActionBase.dynamicHighlight = dynamicHighlight;
            ActionBase.selectableType = selectableType;
            ActionBase.IsStopedCurrentStep = false;

            while (ActionBase.userInputting[(int)UserInput.SelectingEntity] == true)
            {
                // 스탭이 중지되었다면 그냥 보낸다.
                if (ActionBase.IsStopedCurrentStep)
                {
                    ActionBase.userInputting[(int)UserInput.SelectingEntity] = false;
                    break;
                }

                await Task.Delay(100);
            }

            ActionBase.cursorText = null;
            if (selectedEntity != null)
            {
                selectedEntity.Selected = true;
                workspace.Invalidate();
            }

            return selectedEntity;
        }

        // 서브 객체 1개를 선택받거나 키를 입력받는다.
        public async Task<Entity> GetSubEntity(string message = null, int stepID = -1, bool dynamicHighlight = true)
        {
            ActionBase.StepID = StepID;
            ActionBase.cursorText = message;
            ActionBase.userInputting[(int)UserInput.SelectingSubEntity] = true;
            ActionBase.IsStopedCurrentStep = false;
            ActionBase.dynamicHighlight = dynamicHighlight;
            ActionBase.LastSelectedItem = null;

            workspace.SetCurrent(null);

            while (ActionBase.userInputting[(int)UserInput.SelectingSubEntity] == true)
            {
                // 스탭이 중지되었다면 그냥 보낸다.
                if (ActionBase.IsStopedCurrentStep)
                {
                    ActionBase.userInputting[(int)UserInput.SelectingSubEntity] = false;
                    break;
                }

                await Task.Delay(100);
            }

            ActionBase.LastSelectedItem = null;
            ActionBase.cursorText = null;
            if (selectedEntity != null)
            {
                selectedEntity.Selected = true;
                workspace.Invalidate();
            }

            workspace.SetCurrent(null);
            return selectedEntity;
        }

        // 반드시 구현해야함
        abstract public void Run();


        #region 생성자
        //TODO devDept 2022: Eyeshot.Environment class has been renamed in Eyeshot.Workspace.
        //protected devDept.Eyeshot.Workspace environment;
        protected devDept.Eyeshot.Workspace workspace;
        protected devDept.Eyeshot.Design GetDesign() { return workspace as devDept.Eyeshot.Design; }


        // 액션이 취소 되었는지?
        protected bool IsCanceled()
        {
            if (ActionBase.Canceled == true)
                return true;
            if (StoppedActionByNewActionStart == true)
                return true;

            return false;
        }

        // enter가 입력 되었는지?
        protected bool IsEntered()
        {
            if (ActionBase.Entered == true)
                return true;

            return false;
        }



        // 새로운 액션이 시작되어서 중지 되었는지?
        public bool StoppedActionByNewActionStart
        { get; set; }
        //TODO devDept 2022: Eyeshot.Environment class has been renamed in Eyeshot.Workspace.
        //public ActionBase(devDept.Eyeshot.Workspace environment)
        public ActionBase(devDept.Eyeshot.Workspace environment)
        {
            this.workspace = environment;

            //// 액션을 시작하면 기존에 실행중이던 액션을 먼저 종료한다.
            //if (runningAction != null)
            //{
            //    runningAction.StoppedActionByNewActionStart = true;
            //}

            //runningAction = this;
        }




        #endregion

        // 액션 시작할때 반드시 호출해야 한다.
        virtual protected void StartAction()
        {
            // 시작할때 이미 실행중인 액션이 있다면 종료 시킨다.
            if (ActionBase.runningAction != null && ActionBase.runningAction != this)
            {
                ActionBase.Canceled = true;
                while (ActionBase.runningAction != null)
                {
                    // c++의 PeekMessage와 비슷한 역할을 한다.
                    System.Windows.Forms.Application.DoEvents();
                    System.Threading.Thread.Sleep(100);
                }
            }

            ActionBase.runningAction = this;
            ActionBase.Canceled = false;
            for (int i = 0; i < (int)UserInput.Count; ++i)
            {
                userInputting[i] = false;
            }

            ActionBase.PreviewEntities = null;
            ActionBase.PreviewFaceEntities = null;
            ActionBase.SetTempEtt(workspace, null);
        }

        // 액션 종료할때 반드시 호출해야 한다.
        virtual public void EndAction()
        {
            ActionBase.PreviewEntities = null;
            ActionBase.PreviewFaceEntities = null;
            ActionBase.SetTempEtt(workspace, null);
            ActionBase.IsModified = true;

            workspace.ActionMode = actionType.None;
            devDept.Eyeshot.Design design = workspace as devDept.Eyeshot.Design;
            if (design != null)
            {
                design.SelectionFilterMode = selectionFilterType.Entity;
            }

            workspace.Cursor = System.Windows.Forms.Cursors.Default;
            ActionBase.runningAction = null;

            if (ActionBase.IsUnselectAllOnEndAction && workspace is devDept.Eyeshot.Design)
            {
                devDept.Eyeshot.Design vl = (devDept.Eyeshot.Design)workspace;
                vl.Entities.ClearSelection();
            }

            // invalidate를 해 줘야 미리보기라던가, 마우스 text가 바로 사라짐
            workspace.Invalidate();
        }
    }
}
