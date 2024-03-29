﻿using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.Geometry;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Environment = devDept.Eyeshot.Environment;
using Label = devDept.Eyeshot.Labels.Label;
using GuiLabs.Undo;
namespace hanee.ThreeD
{
    abstract public class ActionBase
    {
        public enum UserInput
        {
            GettingPoint3D, // Point3D 를 입력받고 있는지?
            GettingPoint,   // Point를 입력받고 있는지?
            SelectingLabel, // label을 선택받는다.
            SelectingEntity,    // 객체/면등을.. 선택하고 있는지?
            SelectingSubEntity,    // 서브 객체를 선택하고 있는지?
            SelectingFace,
            SelectingEdge,
            SelectingEntities, // 2개 이상의 객체를 선택한다.
            GettingKey,    // key 를 입력받는다.
            GettingText,    // text를 입력받는다.
            GettingDist,  // 거리를 력받는다.
            Count
        }

        // 스냅 간격
        static public double GridSnap = 0.001;  // float로 하면 안됨(좌표 계산시 소수점 쓰레기가 발생함)



        // end action할때 unselect all을 할지?
        static public bool IsUnselectAllOnEndAction = true;

        // 문서가 편집되었는지?
        // action이 끝나면 무조건 편집 된것으로 본다.
        static public bool IsModified
        { get; set; }

        static public bool[] userInputting = new bool[(int)UserInput.Count];
        static public Workspace tempWorkspace = new Workspace();
        static public ActionManager actionManager = new ActionManager();
        static public EditAction opendEditAction = null;
        static public Transaction transaction = null;
        
        protected void CreateTransaction()
        {
            if(transaction != null)
            {
                CommitTransation();
            }

            transaction = Transaction.Create(actionManager);
        }

        protected void CancelTransaction()
        {
            transaction = null;
        }


        protected void CommitTransation()
        {
            if (transaction == null)
                return;

            transaction.Commit();
            transaction = null;
        }

        // edit 시작(편집전)
        protected void StartEditEntities(params Entity[] originEntities)
        {
            if(opendEditAction != null)
            {
                System.Diagnostics.Debug.Assert(false);
                return;
            }

            opendEditAction = new EditAction(model, originEntities);
        }

        // edit 종료(편집후)
        protected void EndEditEntitites()
        {
            // 열려있는 edit action이 없으면 실행 불가
            if (opendEditAction == null)
            {
                System.Diagnostics.Debug.Assert(false);
                return;
            }

            opendEditAction.EndEdit();
            actionManager.RecordAction(opendEditAction);
        }


        protected void TransformEntities(Transformation trans, params Entity[] entities)
        {
            var ac = new TransformAction(model, trans, entities);
            actionManager.RecordAction(ac);
        }

        protected void AddEntities(params Entity [] entities)
        {
            var ac = new AddAction(model, entities);
            actionManager.RecordAction(ac);
        }

        protected void DeleteEntities(params Entity[] entities)
        {
            var ac = new DeleteAction(model, entities);
            actionManager.RecordAction(ac);
        }
        protected void DeleteSelectedEntities()
        {
            var entities = model.GetAllSelectedEntities();
            if (entities == null)
                return;
            DeleteEntities(entities.ToArray());
        }

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

        static public bool enabled2PointsForDist { get; set; } = false;
        static public double dist { get; set; }
        static Plane planeForDistNormal { get; set; }   // 거리를 법선 방향으로 계산하기 위한 plane

        static public string text { get; set; }
        static protected KeyEventArgs key = new KeyEventArgs(Keys.A);
        static public KeyEventArgs Key
        {
            get { return key; }
            set { key = value; }
        }

        static private Entity selectedEntity = null;    // 객체 1개 선택시
        private List<Entity> selectedEntities => GetHModel().GetAllSelectedEntities();  // 여러개 객체 선택시

        static private Label selectedLabel = null;
        static private devDept.Eyeshot.Model.SelectedFace selectedFace = null;
        static private devDept.Eyeshot.Model.SelectedEdge selectedEdge = null;

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
                if (value == null)
                    PreviewEntities = null;
                else
                {
                    PreviewEntities = new Entity[] { value };
                }
            }
        }

        static public void ModifyPointByGridSnap(ref Point3D point3D)
        {
            if (Point3D == null)
                return;

            if (point3D == null)
                return;

            if (ActionBase.GridSnap == 0)
                return;

            point3D.X = (int)(point3D.X / ActionBase.GridSnap) * GridSnap;
            point3D.Y = (int)(point3D.Y / ActionBase.GridSnap) * GridSnap;
            point3D.Z = (int)(point3D.Z / ActionBase.GridSnap) * GridSnap;
        }

        // 임시 객체를 이동한다.
        static public void MoveTempEtt(devDept.Eyeshot.Model model, Vector3D vMove)
        {
            for (int i = 0; i < model.TempEntities.Count(); ++i)
            {
                model.TempEntities[i].Translate(vMove);
            }
        }

        // 임시 객체를 회전한다.
        static public void RotateTempEtt(devDept.Eyeshot.Model model, Vector3D fromDir, Vector3D toDir, Point3D centerPoint)
        {
            Transformation trans = new Transformation();
            trans.Rotation(fromDir, toDir, centerPoint);
            for (int i = 0; i < model.TempEntities.Count(); ++i)
            {
                model.TempEntities[i].TransformBy(trans);
            }
        }

        // 임시 객체를 설정한다.
        // 속도 느림
        static public void SetTempEtt(devDept.Eyeshot.Environment environment, Entity ent, bool initTempEntities = true)
        {
            if (initTempEntities)
            {
                environment.TempEntities.Clear();
            }
            if (environment == null || ent == null)
                return;

            if (ent is BlockReference)
            {
                BlockReference br = ent as BlockReference;
                Entity[] ents = br.Explode(environment.Blocks, true);

                foreach (var subEnt in ents)
                {
                    ActionBase.SetTempEtt(environment, subEnt, false);
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
                        ActionBase.SetTempEtt(environment, m, false);
                    }
                    catch
                    {
                        s.Regen(0.01);
                        Mesh m = s.ConvertToMesh();
                        ActionBase.SetTempEtt(environment, m, false);
                    }

                }
                else if (ent is Brep)
                {
                    Brep s = ent as Brep;
                    try
                    {
                        Mesh m = s.ConvertToMesh();
                        ActionBase.SetTempEtt(environment, m, false);
                    }
                    catch (Exception ex)
                    {
                        s.Regen(1);
                        Mesh m = s.ConvertToMesh();
                        ActionBase.SetTempEtt(environment, m, false);
                    }

                }
                else if (ent is devDept.Eyeshot.Entities.Region)
                {
                    devDept.Eyeshot.Entities.Region s = ent as devDept.Eyeshot.Entities.Region;
                    try
                    {
                        Mesh m = s.ConvertToMesh();
                        ActionBase.SetTempEtt(environment, m, false);
                    }
                    catch
                    {
                        s.Regen(0.01);
                        Mesh m = s.ConvertToMesh();
                        ActionBase.SetTempEtt(environment, m, false);
                    }
                }
                else if (ent is IFace/* || ent is ICurve*/)
                {
                    Color col = Color.FromArgb(50, ent.Color.R, ent.Color.G, ent.Color.B);
                    ent.Color = col;
                    environment.TempEntities.Add(ent);
                }
                else if (ent is ICurve)
                {
                    Color col = Color.FromArgb(50, ent.Color.R, ent.Color.G, ent.Color.B);
                    ent.Color = col;
                    ent.Regen(0.01);
                    environment.TempEntities.Add(ent);
                }
            }
        }

        // 현재 실행중인 액션
        static public ActionBase runningAction = null;

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

        // orthomode가 필요한지?
        static public bool IsNeedOrthoMode(GripManager gripManager)
        {
            return IsNeedSnapping(gripManager);
        }

        // snap찾기가 필요한지?
        static public bool IsNeedSnapping(GripManager gripManager)
        {
            if (ActionBase.userInputting[(int)ActionBase.UserInput.GettingPoint3D] == true ||
                ActionBase.userInputting[(int)ActionBase.UserInput.GettingDist] == true)
                return true;

            if(gripManager != null)
            {
                if (gripManager.EditingGripPoints())
                    return true;
            }
            
            
            return false;
        }

        // 커서 메세지
        static public void InitCursorText()
        {
            cursorText = null;
        }

        static public void InitSubCursorText()
        {
            subCursorText.Clear();
        }

        static public string cursorText;
        static public List<string> subCursorText = new List<string>();

        // 액션에서 현재 스탭 ID
        static public int StepID
        { get; set; }

        // select시 dynamic highlight를 할지?
        static bool dynamicHighlight = true;
        static public devDept.Eyeshot.Model.SelectedItem LastSelectedItem = null;
        static public Dictionary<Type, bool> selectableTypes = new Dictionary<Type, bool>();
        static public KeyEventArgs[] availableKeys;
        static public string[] availableTexts;

        static public bool HasAvailableTexts()
        {
            if (availableTexts == null || availableTexts.Length == 0)
                return false;
            return true;
        }

        static public bool IsAvailableText(string text)
        {
            if (availableTexts == null || availableTexts.Length == 0)
                return true;

            foreach (var te in availableTexts)
            {
                if (te.EqualsIgnoreCase(text))
                    return true;
            }

            return false;
        }

        static public bool HasAvailableKeys()
        {
            if (availableKeys == null || availableKeys.Length == 0)
                return false;
            return true;
        }
        static public bool IsAvailableKey(KeyEventArgs key)
        {
            if (availableKeys == null || availableKeys.Length == 0)
                return true;

            foreach (var ak in availableKeys)
            {
                if (ak.KeyCode != key.KeyCode)
                    continue;
                if (ak.KeyData != key.KeyData)
                    continue;
                if (ak.Modifiers != key.Modifiers)
                    continue;
                if (ak.Alt != key.Alt)
                    continue;
                if (ak.Control != key.Control)
                    continue;

                return true;
            }

            return false;
        }
        static public bool HasSelectableTypes()
        {
            if (selectableTypes == null || selectableTypes.Count == 0)
                return false;
            return true;
        }
        static public bool IsSelectableType(Entity ent)
        {
            if (selectableTypes == null || selectableTypes.Count == 0)
                return true;
            return selectableTypes.ContainsKey(ent.GetType());
        }


        // 객체를 선택중인지?
        static public bool IsSelectingEntity()
        {
            if (userInputting[(int)UserInput.SelectingSubEntity] ||
                userInputting[(int)UserInput.SelectingEntity] ||
                userInputting[(int)UserInput.SelectingLabel] ||
                userInputting[(int)UserInput.SelectingEntities] ||
                userInputting[(int)UserInput.SelectingFace] ||
                userInputting[(int)UserInput.SelectingEdge]
                )
                return true;

            return false;
        }
        // mouse move 이벤트 처리
        static public void MouseMoveHandler(Environment environment, MouseEventArgs e, bool fromDynamicInput = false)
        {
            if (!fromDynamicInput)
            {
                if (ActionBase.currentMousePoint == e.Location)
                    return;
            }

            // 현재 마우스 좌표는 스냅찾을때 사용되므로 mouse move 때마다 설정한다.
            ActionBase.currentMousePoint = e.Location;

            if (userInputting[(int)UserInput.GettingPoint] == true)
                SetPointByMouseEventArgs(environment, e);
            if (userInputting[(int)UserInput.GettingPoint3D] == true ||
                userInputting[(int)UserInput.GettingDist] == true)
                SetPoint3DByMouseEventArgs(environment, e);

            // 객체 선택중이고 dynamic highlight 해야하는 경우
            DynamicHighlight(environment, e);

            // dynamic input
            //if (!fromDynamicInput)
            //{
            //    if (ActionBase.IsUserInputting())
            //        DynamicInputManager.ShowDynamicInput(environment);
            //    else
            //        DynamicInputManager.HideDynamicInput();
            //}


            if (runningAction != null)
                runningAction.OnMouseMove(environment, e);
        }
        // 마우스 위치에서의 dynamic highlight
        private static void DynamicHighlight(Environment environment, MouseEventArgs e)
        {
            if (!IsSelectingEntity() || !dynamicHighlight || e.Button == MouseButtons.Middle)
                return;


            environment.SetCurrent(null);

            devDept.Eyeshot.Model.SelectedItem item = environment.GetItemUnderMouseCursor(e.Location);
            if (item != null && item.Item != null && selectableTypes != null && selectableTypes.Count() > 0 && !selectableTypes.ContainsKey(item.Item.GetType()))
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
                        if (environment.Blocks.Contains(br.BlockName))
                            environment.SetCurrent(item.Item as BlockReference);
                        item = environment.GetItemUnderMouseCursor(e.Location);
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
                bool highlightable = false;
                var sf = item as Environment.SelectedFace;
                if (sf != null)
                {
                    highlightable = item.Item is Brep;
                }

                // 기존 highlight를 제거
                if (LastSelectedItem != null && LastSelectedItem.Item is Brep lastBrep)
                {
                    lastBrep.ClearFacesSelection();
                }

                LastSelectedItem = item;

                if (highlightable)
                {
                    if (LastSelectedItem != null)
                    {
                        if (sf != null && item.Item is Brep brep)
                        {
                            brep.SetFaceSelection(sf.Index, true);
                        }
                    }
                }

                environment.Invalidate();
            }
        }

        // mouse event args로 Point 좌표를 설정한다.
        static void SetPointByMouseEventArgs(Environment environment, MouseEventArgs e)
        {
            point = e.Location;
        }

        // snap / ortho mode를 고려한 3D 좌표 리턴
        static public Point3D GetPoint3DWithSnapAndOrthoMode(Environment environment, MouseEventArgs e)
        {
            // snapPoint 우선
            var model = environment as HModel;
            if (model != null)
            {
                if (model.Snapping.CurrentlySnapping)
                {
                    return model.Snapping.GetSnapPoint();
                }
            }

            // 그다음이 ortho mode
            var pt = GetPoint3DByMouseLocation(environment, e.Location);
            if (model != null)
            {
                // grid snap
                pt = model.gridSnapping.GetGridSnapPoint3D(pt);

                // ortho mode
                pt = model.orthoModeManager.GetOrthoPoint3D(e, pt);
            }

            return pt;
        }
        // mouse event args로 Point3D 좌표를 설정한다.
        static void SetPoint3DByMouseEventArgs(Environment environment, MouseEventArgs e)
        {
            point3D = GetPoint3DWithSnapAndOrthoMode(environment, e);
        }



        // mouse 위치의 Point3D 좌표를 리턴
        static public Point3D GetPoint3DByMouseLocation(Environment environment, System.Drawing.Point location)
        {
            Point3D point3D = environment.ScreenToWorldWithWorkspace(location);

            // null 이면 camere 가 바라보는 평면을 기준으로 좌표를 계산한다
            if (point3D == null)
            {
                Model model = environment as Model;
                if (model != null)
                    environment.ScreenToPlane(location, model.ActiveViewport.Camera.NearPlane, out point3D);
            }

            if (point3D != null)
            {
                // 허용 소수점 자릿수로 조정(우선순위는 제일 낮다. snap, grid등에 의해서 조정되는 소수점은 허용한다)
                ActionBase.ModifyPointByDecimals(ref point3D);

                // 그리드 스냅 적용
                ActionBase.ModifyPointByGridSnap(ref point3D);

                // workspace에 투영
                point3D = environment.ProjectOnWorkspace(point3D);

                if (environment is HModel hModel)
                {
                    if (hModel.IsTopViewOnly(hModel.ActiveViewport))
                        point3D.Z = 0;
                }
            }

            return point3D;
        }

        // 허용 소수점 자릿수로 잘라낸다.
        private static void ModifyPointByDecimals(ref Point3D point3D)
        {
            point3D.X = Math.Round(point3D.X, Options.Instance.decimals);
            point3D.Y = Math.Round(point3D.Y, Options.Instance.decimals);
            point3D.Z = Math.Round(point3D.Z, Options.Instance.decimals);
        }

        static public void CameraMoveEndHandler(devDept.Eyeshot.Model model, object sender, devDept.Eyeshot.Model.CameraMoveEventArgs e)
        {

        }
        // selection changed 이벤트 처리
        static public void SelectionChangedHandler(devDept.Eyeshot.Model model, object sender, devDept.Eyeshot.Model.SelectionChangedEventArgs e)
        {
            // face 선택중인 경우
            if (userInputting[(int)UserInput.SelectingFace] == true)
            {
                foreach (var item in e.AddedItems)
                {
                    if (item is devDept.Eyeshot.Model.SelectedFace)
                    {
                        selectedFace = (devDept.Eyeshot.Model.SelectedFace)item;
                        ActionBase.EndInput(UserInput.SelectingFace);
                    }
                }
            }

            // edge 선택중인 경우
            if (userInputting[(int)UserInput.SelectingEdge] == true)
            {
                foreach (var item in e.AddedItems)
                {
                    if (item is devDept.Eyeshot.Model.SelectedEdge)
                    {
                        selectedEdge = (devDept.Eyeshot.Model.SelectedEdge)item;
                        ActionBase.EndInput(UserInput.SelectingEdge);
                    }
                }
            }
        }

        // mouse click 이벤트 처리
        static public void MouseDownHandler(Environment environment, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                MouseDownHandler_LeftButton(environment, e);
            }
            else if (e.Button == MouseButtons.Right)
            {
                MouseDownHandler_RightButton(environment, e);
            }

        }

        // 마우스 오른키를 누르면 입력 완료로 처리한다.
        private static void MouseDownHandler_RightButton(Environment environment, MouseEventArgs e)
        {
            // control / shift / alt을 같이 누르면 완료 아님
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control ||
                (Control.ModifierKeys & Keys.Alt) == Keys.Alt ||
                (Control.ModifierKeys & Keys.Shift) == Keys.Shift)
                return;

            Entered = true;
        }

        private static void MouseDownHandler_LeftButton(Environment environment, MouseEventArgs e)
        {
            if (userInputting[(int)UserInput.GettingPoint] == true)
            {
                SetPointByMouseEventArgs(environment, e);

                ActionBase.EndInput(UserInput.GettingPoint);
            }

            if (userInputting[(int)UserInput.GettingPoint3D] == true)
            {
                SetPoint3DByMouseEventArgs(environment, e);

                ActionBase.EndInput(UserInput.GettingPoint3D);
            }

            if (userInputting[(int)UserInput.GettingDist] == true)
            {
                SetPoint3DByMouseEventArgs(environment, e);

                var startPoint = GetOrthoModeStartPoint();
                if (startPoint != null)
                {
                    if (planeForDistNormal == null)
                        dist = point3D.DistanceTo(startPoint);
                    else
                        dist = point3D.DistanceTo(planeForDistNormal);
                    ActionBase.EndInput(UserInput.GettingDist);
                }
                else
                {
                    // startPoint가 없는 경우 2click 활성화면 이번에 입력한 좌표를 startPoint로 설정
                    if (ActionBase.enabled2PointsForDist)
                    {
                        ActionBase.SetOrthoModeStartPoint(point3D);

                        var cmdMessage = CmdBarManager.GetCmdMessage();
                        var status = CmdBarManager.GetStatus();
                        CmdBarManager.AddCmdText(point3D.ToString());

                        CmdBarManager.AddHistory();
                        CmdBarManager.SetCmdMessage(cmdMessage, status);
                    }
                    else
                    {
                        ActionBase.cursorText = ActionBase.cursorText + " : Disable input by click";
                    }
                }
            }

            if (userInputting[(int)UserInput.SelectingLabel] == true)
            {
                var model = environment as Model;
                int idx = environment.GetLabelUnderMouseCursor(e.Location);
                if (model != null && idx > -1 && idx < model.ActiveViewport.Labels.Count)
                {

                    selectedLabel = model.ActiveViewport.Labels[idx];
                    ActionBase.EndInput(UserInput.SelectingLabel);
                }
            }

            if (userInputting[(int)UserInput.SelectingEntity] == true)
            {
                selectedEntity = null;
                int[] entityIndexes;
                if (ActionBase.HasSelectableTypes())
                {
                    Rectangle selectionBox = new Rectangle();
                    selectionBox.X = e.Location.X - environment.PickBoxSize / 2;
                    selectionBox.Y = e.Location.Y - environment.PickBoxSize / 2;
                    selectionBox.Width = environment.PickBoxSize;
                    selectionBox.Height = environment.PickBoxSize;
                    entityIndexes = environment.GetAllCrossingEntities(selectionBox);
                }
                else
                {
                    entityIndexes = environment.GetAllEntitiesUnderMouseCursor(e.Location);
                }


                foreach (var idx in entityIndexes)
                {
                    Entity entityTmp = environment.Entities[idx];
                    if (entityTmp == null)
                        continue;

                    if (!ActionBase.IsSelectableType(entityTmp))
                        continue;

                    selectedEntity = entityTmp;
                    break;
                }

                if (selectedEntity != null)
                {
                    ActionBase.EndInput(UserInput.SelectingEntity);
                }
            }


            if (userInputting[(int)UserInput.SelectingFace] == true)
            {
                devDept.Eyeshot.Model.SelectedItem item = environment.GetItemUnderMouseCursor(e.Location);
                if (item != null)
                {
                    if (item is devDept.Eyeshot.Model.SelectedFace)
                    {
                        selectedFace = (devDept.Eyeshot.Model.SelectedFace)item;
                        ActionBase.EndInput(UserInput.SelectingFace);
                    }
                }
            }

            if (userInputting[(int)UserInput.SelectingEdge] == true)
            {
                devDept.Eyeshot.Model.SelectedItem item = environment.GetItemUnderMouseCursor(e.Location);
                if (item != null)
                {
                    if (item is devDept.Eyeshot.Model.SelectedEdge)
                    {
                        selectedEdge = (devDept.Eyeshot.Model.SelectedEdge)item;
                        ActionBase.EndInput(UserInput.SelectingEdge);
                    }
                }
            }

            if (userInputting[(int)UserInput.SelectingSubEntity] == true)
            {
                devDept.Eyeshot.Model.SelectedItem item = environment.GetItemUnderMouseCursor(e.Location);
                if (item != null)
                {
                    // sub 객체를 탐색한다.
                    while (item.Item is BlockReference)
                    {
                        try
                        {
                            BlockReference br = item.Item as BlockReference;
                            if (environment.Blocks.Contains(br.BlockName))
                                environment.SetCurrent(item.Item as BlockReference);
                            item = environment.GetItemUnderMouseCursor(e.Location);
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
                            ActionBase.EndInput(UserInput.SelectingSubEntity);

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
                // enter, space는 cmdbar 입력상황에 따라 판단해야함
                // enter, space에 대한 처리는 cmdbar에서만 한다.
                //if(!userInputting[(int)UserInput.GettingText])
                //    Entered = true;
            }
            // shfit, control, alt키를 누르는건 무시한다.
            else if (e.KeyCode == Keys.Shift || e.KeyCode == Keys.ShiftKey || e.KeyCode == Keys.ControlKey || e.KeyCode == Keys.Alt || e.KeyCode == Keys.Control)
                return;

            if (userInputting[(int)UserInput.GettingKey] == true)
            {
                if (HasAvailableKeys() && !IsAvailableKey(e))
                {
                    key = null;
                    return;
                }
                key = e;

                ActionBase.EndInput(UserInput.GettingKey);
            }
        }

        // action에서 mouse move에 대한 처리를 할때 재정의 한다.
        protected virtual void OnMouseMove(devDept.Eyeshot.Environment environment, MouseEventArgs e)
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
        // normalPoint workplane의 법선방향으로의 좌표를 입력받는다.
        public async Task<Point3D> GetPoint3D(string message = null, int stepID = -1)
        {
            ActionBase.StartInput(environment, message, stepID, UserInput.GettingPoint3D);

            while (ActionBase.userInputting[(int)UserInput.GettingPoint3D] == true)
            {
                // 스탭이 중지되었다면 그냥 보낸다.
                if (ActionBase.IsStopedCurrentStep)
                {
                    ActionBase.EndInput(UserInput.GettingPoint3D);
                    break;
                }

                await Task.Delay(100);
            }

            ActionBase.InitCursorText();


            return point3D;
        }

        // user input이 끝났는지?
        bool FinishedUserInput(params ActionBase.UserInput[] userInputs)
        {
            foreach (var ui in userInputs)
            {
                if (!ActionBase.userInputting[(int)ui])
                    return true;
            }
            return false;
        }

        // input을 시작하고 사용자가 입력을 할때까지 기다린다.
        async Task StartAndWaitUserInput(string message, int stepId, params ActionBase.UserInput[] userInputs)
        {
            // 시작
            ActionBase.StartInput(environment, message, stepId, userInputs);

            // 대기
            while (!FinishedUserInput(userInputs))
            {
                // 스탭이 중지되었다면 그냥 보낸다.
                if (ActionBase.IsStopedCurrentStep)
                {
                    ActionBase.EndInput(userInputs);
                    break;
                }

                await Task.Delay(100);
            }

            // cursor text를 초기화 한다.
            ActionBase.InitCursorText();
        }

        // 거리를 입력받는다.
        // 거리 입력시 마우스로도 입력받을 수 있다.
        public async Task<double> GetDist(string message = null, Point3D startPoint = null, int stepID = -1, double? min = null, double? max = null, Plane planeForDistNormal = null, bool enabled2PointsForDist = false)
        {
            // dist를 입력받을때는 마우스 입력도 가능하게 하기 위해서 startPoint를 입력받아야 함.
            SetOrthoModeStartPoint(startPoint);
            ActionBase.enabled2PointsForDist = enabled2PointsForDist;
            ActionBase.planeForDistNormal = planeForDistNormal;
            await StartAndWaitUserInput(message, stepID, UserInput.GettingDist);

            // 입력을 완전히 종료
            EndInput(UserInput.GettingDist);

            return dist;
        }

        // text를 입력받는다.
        public async Task<string> GetText(string message = null, int stepID = -1, params string[] availableTexts)
        {
            ActionBase.availableTexts = availableTexts;

            await StartAndWaitUserInput(message, stepID, UserInput.GettingText);

            // 정상 입력이 아닌 경우라면 null을 준다.
            // 그래야 사용하는 곳에서 어떤 값이 입력 되었는지 알수 있다.
            var resultText = text;
            if (ActionBase.userInputting[(int)UserInput.GettingText])
                resultText = null;

            // 입력을 완전히 종료
            EndInput(UserInput.GettingText);

            return resultText;
        }

        // 마우스로 point3D를 입력받거나 text를 입력받는다.
        public async Task<KeyValuePair<Point3D, string>> GetPoint3DOrText(string message = null, int stepID = -1, params string[] availableTexts)
        {
            ActionBase.availableTexts = availableTexts;

            await StartAndWaitUserInput(message, stepID, UserInput.GettingPoint3D, UserInput.GettingText);

            // 정상 입력이 아닌 경우라면 null을 준다.
            // 그래야 사용하는 곳에서 어떤 값이 입력 되었는지 알수 있다.
            var resultPoint3D = point3D;
            var resultText = text;
            if (ActionBase.userInputting[(int)UserInput.GettingPoint3D])
                resultPoint3D = null;
            if (ActionBase.userInputting[(int)UserInput.GettingText])
                resultText = null;

            // 입력을 완전히 종료
            EndInput(UserInput.GettingPoint3D, UserInput.GettingText);

            return new KeyValuePair<Point3D, string>(resultPoint3D, resultText);
        }

        // 마우스로 point3D를 입력받거나 key를 입력받는다.
        public async Task<KeyValuePair<Point3D, KeyEventArgs>> GetPoint3DOrKey(string message = null, int stepID = -1, params KeyEventArgs[] availableKeys)
        {
            ActionBase.StartInput(environment, message, stepID, UserInput.GettingPoint3D, UserInput.GettingKey);
            ActionBase.availableKeys = availableKeys;

            while (ActionBase.userInputting[(int)UserInput.GettingPoint3D] == true &&
                ActionBase.userInputting[(int)UserInput.GettingKey] == true
                )
            {
                // 스탭이 중지되었다면 그냥 보낸다.
                if (ActionBase.IsStopedCurrentStep)
                {
                    ActionBase.EndInput(UserInput.GettingPoint3D);
                    ActionBase.EndInput(UserInput.GettingKey);
                    break;
                }

                await Task.Delay(100);
            }

            ActionBase.InitCursorText();

            // 정상 입력이 아닌 경우라면 null을 준다.
            // 그래야 사용하는 곳에서 어떤 값이 입력 되었는지 알수 있다.
            var resultPoint3D = point3D;
            var resultKey = key;
            if (ActionBase.userInputting[(int)UserInput.GettingPoint3D])
                resultPoint3D = null;
            if (ActionBase.userInputting[(int)UserInput.GettingKey])
                resultKey = null;

            ActionBase.userInputting[(int)UserInput.GettingPoint3D] = false;
            ActionBase.userInputting[(int)UserInput.GettingKey] = false;
            return new KeyValuePair<Point3D, KeyEventArgs>(resultPoint3D, resultKey);
        }

        #endregion


        // 마우스로 point를 입력받는다.
        public async Task<System.Drawing.Point> GetPoint(string message = null, int stepID = -1)
        {
            ActionBase.StartInput(environment, message, stepID, UserInput.GettingPoint);
            while (ActionBase.userInputting[(int)UserInput.GettingPoint] == true)
            {
                // 스탭이 중지되었다면 그냥 보낸다.
                if (ActionBase.IsStopedCurrentStep)
                {
                    ActionBase.EndInput(UserInput.GettingPoint);
                    break;
                }

                await Task.Delay(100);
            }

            ActionBase.InitCursorText();

            return point;
        }

        // edge 1개를 선택받는다
        public async Task<devDept.Eyeshot.Model.SelectedEdge> GetEdge(string message = null, int stepID = -1)
        {
            ActionBase.StartInput(environment, message, stepID, UserInput.SelectingEdge);
            actionType oldActionType = environment.ActionMode;
            selectionFilterType oldSelectionFilterType = selectionFilterType.Entity;

            devDept.Eyeshot.Model model = environment as devDept.Eyeshot.Model;
            if (model != null)
            {
                oldSelectionFilterType = model.SelectionFilterMode;
                model.SelectionFilterMode = selectionFilterType.Edge;
            }

            environment.ActionMode = actionType.SelectVisibleByPickDynamic;

            while (ActionBase.userInputting[(int)UserInput.SelectingEdge] == true)
            {
                // 스탭이 중지되었다면 그냥 보낸다.
                if (ActionBase.IsStopedCurrentStep)
                {
                    ActionBase.EndInput(UserInput.SelectingEdge);
                    break;
                }

                await Task.Delay(100);
            }

            ActionBase.InitCursorText();

            if (selectedEdge != null)
            {
                selectedEdge.Select(environment, true);
                environment.Invalidate();
            }

            if (model != null)
            {
                model.ActionMode = oldActionType;
                model.SelectionFilterMode = oldSelectionFilterType;
            }


            return selectedEdge;
        }

        // face 1개를 선택받는다
        public async Task<devDept.Eyeshot.Model.SelectedFace> GetFace(string message = null, int stepID = -1, bool dynamicHighlight = false)
        {
            ActionBase.StartInput(environment, message, stepID, UserInput.SelectingFace);
            ActionBase.dynamicHighlight = dynamicHighlight;
            selectionFilterType oldSelectionFilterType = selectionFilterType.Entity;

            devDept.Eyeshot.Model model = environment as devDept.Eyeshot.Model;
            if (model != null)
            {
                oldSelectionFilterType = model.SelectionFilterMode;
                model.SelectionFilterMode = selectionFilterType.Face;
            }

            while (ActionBase.userInputting[(int)UserInput.SelectingFace] == true)
            {
                // 스탭이 중지되었다면 그냥 보낸다.
                if (ActionBase.IsStopedCurrentStep)
                {
                    ActionBase.EndInput(UserInput.SelectingFace);
                    break;
                }

                await Task.Delay(100);
            }

            ActionBase.InitCursorText();

            if (selectedFace != null)
            {
                selectedFace.Item.Selected = true;
                environment.Invalidate();
            }

            if (model != null)
                model.SelectionFilterMode = oldSelectionFilterType;

            return selectedFace;
        }

        // face 1개를 선택받는다
        public async Task<KeyValuePair<devDept.Eyeshot.Model.SelectedFace, string>> GetFaceOrText(string message = null, int stepID = -1, bool dynamicHighlight = false, params string[] availableTexts )
        {
            ActionBase.availableTexts = availableTexts;
            selectionFilterType oldSelectionFilterType = selectionFilterType.Entity;

            devDept.Eyeshot.Model model = environment as devDept.Eyeshot.Model;
            if (model != null)
            {
                oldSelectionFilterType = model.SelectionFilterMode;
                model.SelectionFilterMode = selectionFilterType.Face;
            }

            await StartAndWaitUserInput(message, stepID, UserInput.SelectingFace, UserInput.GettingText);

            // 정상 입력이 아닌 경우라면 null을 준다.
            // 그래야 사용하는 곳에서 어떤 값이 입력 되었는지 알수 있다.
            var resultFace = selectedFace;
            var resultKey = text;
            if (ActionBase.userInputting[(int)UserInput.SelectingFace])
                resultFace = null;
            if (ActionBase.userInputting[(int)UserInput.GettingKey])
                resultKey = null;

            if (selectedFace != null)
            {
                selectedFace.Item.Selected = true;
                environment.Invalidate();
            }

            if (model != null)
                model.SelectionFilterMode = oldSelectionFilterType;

            // 입력을 완전히 종료
            EndInput(UserInput.SelectingFace, UserInput.GettingText);

            return new KeyValuePair<devDept.Eyeshot.Model.SelectedFace, string>(resultFace, resultKey);
        }

        // face 1개를 선택받는다
        public async Task<KeyValuePair<devDept.Eyeshot.Model.SelectedFace, KeyEventArgs>> GetFaceOrKey(string message = null, int stepID = -1, bool dynamicHighlight = false)
        {
            ActionBase.StartInput(environment, message, stepID, UserInput.SelectingFace, UserInput.GettingKey);
            ActionBase.dynamicHighlight = dynamicHighlight;
            selectionFilterType oldSelectionFilterType = selectionFilterType.Entity;

            devDept.Eyeshot.Model model = environment as devDept.Eyeshot.Model;
            if (model != null)
            {
                oldSelectionFilterType = model.SelectionFilterMode;
                model.SelectionFilterMode = selectionFilterType.Face;
            }

            while (ActionBase.userInputting[(int)UserInput.SelectingFace] == true &&
                ActionBase.userInputting[(int)UserInput.GettingKey] == true)
            {
                // 스탭이 중지되었다면 그냥 보낸다.
                if (ActionBase.IsStopedCurrentStep)
                {
                    ActionBase.EndInput(UserInput.SelectingFace);
                    ActionBase.EndInput(UserInput.GettingKey);
                    break;
                }

                await Task.Delay(100);
            }

            ActionBase.InitCursorText();

            if (selectedFace != null)
            {
                selectedFace.Item.Selected = true;
                environment.Invalidate();
            }

            if (model != null)
                model.SelectionFilterMode = oldSelectionFilterType;

            var resultFace = selectedFace;
            var resultKey = key;
            if (ActionBase.userInputting[(int)UserInput.SelectingFace])
                resultFace = null;
            if (ActionBase.userInputting[(int)UserInput.GettingKey])
                resultKey = null;

            // 2가지 모두 종료(GetFaceOrKey에서는 둘중 하나만 입력받기 때문에 나머지는 여전히  true이다)
            ActionBase.EndInput(UserInput.SelectingFace);
            ActionBase.EndInput(UserInput.GettingKey);

            return new KeyValuePair<devDept.Eyeshot.Model.SelectedFace, KeyEventArgs>(resultFace, resultKey);
        }


        // input 시작
        public static void StartInput(Environment environment, string message, int stepID, params UserInput[] userInputs)
        {
            ActionBase.StepID = stepID;
            ActionBase.cursorText = message;
            foreach (var ui in userInputs)
                ActionBase.userInputting[(int)ui] = true;

            ActionBase.IsStopedCurrentStep = false;

            // cmdbar 메세지 변경
            CmdBarManager.AddHistory();
            CmdBarManager.SetCmdMessage(message, ControlCmdBar.Status.input);



            //DynamicInputManager.ShowDynamicInput(environment);

            //// command bar의 메시지를 바꾼다.
            //if (DynamicInputManager.controlCommandBar != null)
            //    DynamicInputManager.controlCommandBar.labelControlMessage.Text = message;
        }

        // input 끝
        public static void EndInput(params UserInput[] userInputs)
        {
            foreach (var ui in userInputs)
                ActionBase.userInputting[(int)ui] = false;



            //DynamicInputManager.HideDynamicInput();

            //// command bar의 메시지를 초기화
            //if (DynamicInputManager.controlCommandBar != null)
            //    DynamicInputManager.controlCommandBar.labelControlMessage.Text = "Command";
        }


        // 키보드로 char를 입력 받는다.
        public async Task<KeyEventArgs> GetKey(string message = null, int stepID = -1)
        {
            ActionBase.StartInput(environment, message, stepID, UserInput.GettingKey);
            while (ActionBase.userInputting[(int)UserInput.GettingKey] == true)
            {
                // 스탭이 중지되었다면 그냥 보낸다.
                if (ActionBase.IsStopedCurrentStep)
                {
                    ActionBase.EndInput(UserInput.GettingKey);
                    break;
                }

                await Task.Delay(100);
            }

            ActionBase.InitCursorText();

            return key;
        }

        // label 1개를 선택받는다. 
        public async Task<Label> GetLabel(string message = null, int stepID = -1, bool dynamicHighlight = false, Dictionary<Type, bool> selectableType = null)
        {
            ActionBase.StartInput(environment, message, stepID, UserInput.SelectingLabel);
            ActionBase.dynamicHighlight = dynamicHighlight;
            ActionBase.selectableTypes = selectableType;

            while (ActionBase.userInputting[(int)UserInput.SelectingLabel] == true)
            {
                // 스탭이 중지되었다면 그냥 보낸다.
                if (ActionBase.IsStopedCurrentStep)
                {
                    ActionBase.EndInput(UserInput.SelectingLabel);
                    break;
                }

                await Task.Delay(100);
            }

            ActionBase.InitCursorText();

            if (selectedLabel != null)
            {
                selectedLabel.Selected = true;
                environment.Invalidate();
            }

            return selectedLabel;
        }

        // 객체 여러개와 text를 선택는다.
        public async Task<KeyValuePair<List<Entity>, string>> GetEntitiesOrText(string message = null, int stepID = -1, bool dynamicHighlight = false, Dictionary<Type, bool> selectableType = null, params string[] availableTexts)
        {
            ActionBase.dynamicHighlight = dynamicHighlight;
            ActionBase.selectableTypes = selectableType;
            ActionBase.availableTexts = availableTexts;

            await StartAndWaitUserInput(message, stepID, UserInput.SelectingEntities, UserInput.GettingText);

            var resultEntities= selectedEntities;
            var resultText = ActionBase.text;
            if (ActionBase.userInputting[(int)UserInput.SelectingEntities])
                resultEntities = null;
            if (ActionBase.userInputting[(int)UserInput.GettingText])
                resultText = null;

            ActionBase.EndInput(UserInput.SelectingEntities, UserInput.GettingText);

            return new KeyValuePair<List<Entity>, string>(resultEntities, resultText);
        }

        // 객체 여러개를 선택받는다.
        public async Task<List<Entity>> GetEntities(string message = null, int stepID = -1, bool dynamicHighlight = false, Dictionary<Type, bool> selectableType = null)
        {
            ActionBase.dynamicHighlight = dynamicHighlight;
            ActionBase.selectableTypes = selectableType;

            await StartAndWaitUserInput(message, stepID, UserInput.SelectingEntities);

            return selectedEntities;
        }


        // 객체 1개와 text를 선택는다.
        public async Task<KeyValuePair<Entity, string>> GetEntityOrText(string message = null, int stepID = -1, bool dynamicHighlight = false, Dictionary<Type, bool> selectableType = null, params string[] availableTexts)
        {
            ActionBase.dynamicHighlight = dynamicHighlight;
            ActionBase.selectableTypes = selectableType;
            ActionBase.availableTexts = availableTexts;
            ActionBase.selectedEntity = null;

            await StartAndWaitUserInput(message, stepID, UserInput.SelectingEntity, UserInput.GettingText);


            if (ActionBase.selectedEntity != null)
            {
                ActionBase.selectedEntity.Selected = true;
                environment.Invalidate();
            }

            var resultEntity = ActionBase.selectedEntity;
            var resultText = ActionBase.text;
            if (ActionBase.userInputting[(int)UserInput.SelectingEntity])
                resultEntity = null;
            if (ActionBase.userInputting[(int)UserInput.GettingText])
                resultText = null;

            ActionBase.EndInput(UserInput.SelectingEntity, UserInput.GettingText);

            return new KeyValuePair<Entity, string>(resultEntity, resultText);
        }


        // 객체 1개를 선택받는다.
        public async Task<KeyValuePair<Entity, KeyEventArgs>> GetEntityOrKey(string message = null, int stepID = -1, bool dynamicHighlight = false, Dictionary<Type, bool> selectableType = null, params KeyEventArgs[] availableKeys)
        {
            ActionBase.StartInput(environment, message, stepID, UserInput.SelectingEntity, UserInput.GettingKey);

            ActionBase.dynamicHighlight = dynamicHighlight;
            ActionBase.selectableTypes = selectableType;
            ActionBase.availableKeys = availableKeys;


            ActionBase.selectedEntity = null;

            while (ActionBase.userInputting[(int)UserInput.SelectingEntity] == true &&
                ActionBase.userInputting[(int)UserInput.GettingKey] == true
                )
            {
                // 스탭이 중지되었다면 그냥 보낸다.
                if (ActionBase.IsStopedCurrentStep)
                {
                    ActionBase.EndInput(UserInput.SelectingEntity, UserInput.GettingKey);
                    break;
                }

                await Task.Delay(100);
            }

            ActionBase.InitCursorText();

            if (selectedEntity != null)
            {
                selectedEntity.Selected = true;
                environment.Invalidate();
            }

            var resultEntity = selectedEntity;
            var resultKey = key;
            if (ActionBase.userInputting[(int)UserInput.SelectingEntity])
                resultEntity = null;
            if (ActionBase.userInputting[(int)UserInput.GettingKey])
                resultKey = null;

            ActionBase.EndInput(UserInput.SelectingEntity, UserInput.GettingKey);

            return new KeyValuePair<Entity, KeyEventArgs>(resultEntity, resultKey);
        }

        // 객체 1개를 선택받는다.
        public async Task<Entity> GetEntity(string message = null, int stepID = -1, bool dynamicHighlight = false, Dictionary<Type, bool> selectableType = null)
        {
            ActionBase.StartInput(environment, message, stepID, UserInput.SelectingEntity);
            ActionBase.dynamicHighlight = dynamicHighlight;
            ActionBase.selectableTypes = selectableType;
            ActionBase.selectedEntity = null;

            while (ActionBase.userInputting[(int)UserInput.SelectingEntity] == true)
            {
                // 스탭이 중지되었다면 그냥 보낸다.
                if (ActionBase.IsStopedCurrentStep)
                {
                    ActionBase.EndInput(UserInput.SelectingEntity);
                    break;
                }

                await Task.Delay(100);
            }

            ActionBase.InitCursorText();

            if (selectedEntity != null)
            {
                selectedEntity.Selected = true;
                environment.Invalidate();
            }

            return selectedEntity;
        }

        // 서브 객체 1개를 선택받거나 키를 입력받는다.
        public async Task<Entity> GetSubEntity(string message = null, int stepID = -1, bool dynamicHighlight = true)
        {
            ActionBase.StartInput(environment, message, stepID, UserInput.SelectingSubEntity);
            ActionBase.dynamicHighlight = dynamicHighlight;
            ActionBase.LastSelectedItem = null;

            environment.SetCurrent(null);

            while (ActionBase.userInputting[(int)UserInput.SelectingSubEntity] == true)
            {
                // 스탭이 중지되었다면 그냥 보낸다.
                if (ActionBase.IsStopedCurrentStep)
                {
                    ActionBase.EndInput(UserInput.SelectingSubEntity);
                    break;
                }

                await Task.Delay(100);
            }

            ActionBase.LastSelectedItem = null;
            ActionBase.InitCursorText();

            if (selectedEntity != null)
            {
                selectedEntity.Selected = true;
                environment.Invalidate();
            }

            environment.SetCurrent(null);
            return selectedEntity;
        }

        // 반드시 구현해야함
        abstract public void Run();



        protected OrthoModeManager orthoModeManager => (GetModel() as HModel)?.orthoModeManager;


        static public OrthoModeManager GetOrthoModeManager()
        {
            if (ActionBase.runningAction == null)
                return null;
            return ActionBase.runningAction.orthoModeManager;
        }

        static public Point3D GetOrthoModeStartPoint()
        {
            var mng = GetOrthoModeManager();
            if (mng == null)
                return null;

            return mng.startPoint;
        }

        static public void SetOrthoModeStartPoint(Point3D startPoint)
        {
            var mng = GetOrthoModeManager();
            if (mng == null)
                return;
            mng.startPoint = startPoint;
        }
        protected Point3D GetOrthoPoint3D(MouseEventArgs e, Point3D curPoint)
        {
            if (orthoModeManager == null)
                return curPoint;
            return orthoModeManager.GetOrthoPoint3D(e, curPoint);
        }
        #region 생성자
        protected Environment environment;
        protected Model model => environment as Model;
        protected Model GetModel() { return environment as devDept.Eyeshot.Model; }
        protected HModel GetHModel() { return environment as HModel; }
        protected EntityPropertiesManager entityPropertiesManager => GetHModel()?.entityPropertiesManager;
        protected Plane GetWorkplane()
        {
            return environment?.GetWorkplane();
        }

        // workspace가 비활성인 경우 자동 설정한다.
        protected void SetAutoWorkspace(Point3D modifyPoint = null)
        {
            if (environment is HModel model)
            {
                // 2D View인 경우에는 자동설정 하지 않는다.
                if (model.IsTopViewOnly(model.ActiveViewport))
                    return;

                var ws = model.GetWorkspace();
                if (ws == null)
                    return;

                // 이미 활성화 되어 있는 경우에는 자동설정을 하지 않는다.
                if (ws.enabled)
                    return;

                var oldSelectionFilterMode = model.SelectionFilterMode;
                model.SelectionFilterMode = selectionFilterType.Face;

                var item = environment.GetItemUnderMouseCursor(ActionBase.CurrentMousePoint);
                if (item is Environment.SelectedFace sf)
                {
                    // 기존 workspace를 복사
                    ActionBase.tempWorkspace = ws.Clone() as Workspace;

                    // 새로운 workspace 시작
                    environment.StartWorkspace(sf);

                    // 새로운 workspace에 맞게 좌표 조정
                    if (modifyPoint != null)
                    {
                        var newPt = environment.ProjectOnWorkspace(modifyPoint);
                        if (newPt != null)
                        {
                            modifyPoint.X = newPt.X;
                            modifyPoint.Y = newPt.Y;
                            modifyPoint.Z = newPt.Z;
                        }
                    }
                }
                else
                {
                    if (modifyPoint != null)
                    {
                        // 기존 workspace를 복사
                        ActionBase.tempWorkspace = ws.Clone() as Workspace;

                        // 새로운 workspace 시작
                        var plane = ws.plane.Clone() as Plane;
                        plane.Origin = modifyPoint;
                        environment.StartWorkspace(plane);

                    }
                }


                model.SelectionFilterMode = oldSelectionFilterMode;
            }
        }

        protected void SetWorkplane(Plane plane)
        {
            var ws = GetWorkspace();
            if (ws == null)
                return;
            ws.plane = plane;
        }
        protected Workspace GetWorkspace()
        {
            return environment?.GetWorkspace();
        }


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

        public ActionBase(devDept.Eyeshot.Environment environment)
        {
            this.environment = environment;

            //// 액션을 시작하면 기존에 실행중이던 액션을 먼저 종료한다.
            //if (runningAction != null)
            //{
            //    runningAction.StoppedActionByNewActionStart = true;
            //}

            //runningAction = this;
        }




        #endregion

        // 액션을 중지 시킨다.
        static public void StopAction()
        {
            // 시작할때 이미 실행중인 액션이 있다면 종료 시킨다.
            if (ActionBase.runningAction != null)
            {
                ActionBase.Canceled = true;
                while (ActionBase.runningAction != null)
                {
                    // c++의 PeekMessage와 비슷한 역할을 한다.
                    System.Windows.Forms.Application.DoEvents();
                    System.Threading.Thread.Sleep(100);
                }
            }
        }
        // 액션 시작할때 반드시 호출해야 한다.
        virtual protected void StartAction()
        {
            ActionBase.StopAction();
            ActionBase.InitCursorText();
            ActionBase.InitSubCursorText();
            ActionBase.runningAction = this;
            ActionBase.Canceled = false;
            for (int i = 0; i < (int)UserInput.Count; ++i)
            {
                userInputting[i] = false;
            }

            ActionBase.PreviewEntities = null;
            ActionBase.PreviewFaceEntities = null;
            ActionBase.tempWorkspace = null;// GetWorkspace()?.Clone() as Workspace;
            ActionBase.SetTempEtt(environment, null);
        }

        // 액션 종료할때 반드시 호출해야 한다.
        virtual public void EndAction()
        {
            PreviewLabel.Clear(model);
            ActionBase.PreviewEntities = null;
            ActionBase.PreviewFaceEntities = null;
            var ws = GetWorkspace();
            if (ActionBase.tempWorkspace != null && ws != null)
            {
                if (!ActionBase.tempWorkspace.enabled)
                    environment.EndWorkspace();
                else
                    ws.CopyFrom(ActionBase.tempWorkspace);
                ActionBase.tempWorkspace = null;
            }
            ActionBase.SetTempEtt(environment, null);
            ActionBase.IsModified = true;
            ActionBase.Canceled = true;
            ActionBase.selectableTypes?.Clear();
            ActionBase.availableKeys = null;
            ActionBase.InitCursorText();
            ActionBase.InitSubCursorText();

            // cmdbar 메세지를 초기화
            CmdBarManager.AddHistory();
            CmdBarManager.SetCmdMessage("Command : ", ControlCmdBar.Status.command);

            // dynamic input manager 초기화
            //DynamicInputManager.Init();

            environment.ActionMode = actionType.None;
            devDept.Eyeshot.Model design = environment as devDept.Eyeshot.Model;
            if (design != null)
                design.SelectionFilterMode = selectionFilterType.Entity;

            if (orthoModeManager != null)
                orthoModeManager.startPoint = null;


            environment.Cursor = System.Windows.Forms.Cursors.Default;
            ActionBase.runningAction = null;

            if (ActionBase.IsUnselectAllOnEndAction && environment is devDept.Eyeshot.Model)
            {
                devDept.Eyeshot.Model vl = (devDept.Eyeshot.Model)environment;
                vl.Entities.ClearSelection();
            }

            // invalidate를 해 줘야 미리보기라던가, 마우스 text가 바로 사라짐
            environment.Invalidate();
        }
    }
}
