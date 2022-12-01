using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Translators;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    public class GripManager
    {
        static public Dictionary<Type, Type> entityGripManagers { get; set; } = new Dictionary<Type, Type>();
        static public float gripSize = 15;
        Model model;
        HModel hModel => model as HModel;
        RegenParams regenParams = null;
        public GripManager(Model design)
        {
            this.model = design;
            regenParams = new RegenParams(0.001, this.model);

            GripManager.entityGripManagers.Clear();
            GripManager.entityGripManagers.Add(typeof(Line), typeof(LineGripManager));
            GripManager.entityGripManagers.Add(typeof(Arc), typeof(ArcGripManager)); 
            GripManager.entityGripManagers.Add(typeof(Circle), typeof(CircleGripManager));
            GripManager.entityGripManagers.Add(typeof(LinearPath), typeof(LinearPathGripManager));
            GripManager.entityGripManagers.Add(typeof(Text), typeof(TextGripManager));
            GripManager.entityGripManagers.Add(typeof(BlockReference), typeof(BlockReferenceGripManager));
            GripManager.entityGripManagers.Add(typeof(BlockReferenceEx), typeof(BlockReferenceGripManager));
            GripManager.entityGripManagers.Add(typeof(Leader), typeof(LeaderGripManager));
            GripManager.entityGripManagers.Add(typeof(RadialDim), typeof(RadialDimGripManager));
            GripManager.entityGripManagers.Add(typeof(DiametricDim), typeof(RadialDimGripManager));
            GripManager.entityGripManagers.Add(typeof(LinearDim), typeof(LinearDimGripManager));
            GripManager.entityGripManagers.Add(typeof(Curve), typeof(CurveGripManager));

        }

        // grip point를 모두 지운다.
        public void ClearGripPoints()
        {
            model.TempEntities.Clear();
            model.Invalidate();
        }

        // 이미 grip이 만들어진 객체인지?
        public bool HasGripPoint(Entity ent)
        {
            return model.TempEntities.Exists(x => x.EntityData == ent);
        }


        public void MakeGripPoints(Entity ent)
        {
            if (ent == null)
                return;

            IEntityGripManager gm = GetEntityGripManager(ent);
            if (gm == null)
                return;

            var cloneEnt = ent.Clone() as Entity;
            if (cloneEnt == null)
                return;

            cloneEnt.Color = System.Drawing.Color.Yellow;
            cloneEnt.ColorMethod = colorMethodType.byEntity;
            cloneEnt.EntityData = ent;
            cloneEnt.Selected = true;

            // regenParams는 그립을 만들때 생성해야함
            regenParams = new RegenParams(0.001, this.model);
            cloneEnt.Regen(regenParams);

            var gripPoints = gm.GetGripPoints(cloneEnt, model);
            if (gripPoints == null)
                return;

            // block인 경우 explode 한 객체만 추가한다.
            bool existExplodedEntities = gripPoints[0].explodedEntities?.Length > 0;
            if (existExplodedEntities)
            {
                cloneEnt.Selected = false;
                foreach (var gp in gripPoints)
                {
                    if (gp.explodedEntities == null)
                        continue;

                    foreach (var ee in gp.explodedEntities)
                    {
                        if (ee is BlockReference)
                            continue;

                        ee.Color = System.Drawing.Color.Yellow;
                        ee.ColorMethod = colorMethodType.byEntity;
                        ee.Selected = true;
                        model.TempEntities.Add(ee);
                    }
                }
            }
            else
            {
                model.TempEntities.Add(cloneEnt);
            }
            model.TempEntities.AddRange(gripPoints);
            model.Invalidate();
        }

        private IEntityGripManager GetEntityGripManager(Entity ent)
        {
            var type = ent.GetType();
            if (GripManager.entityGripManagers.TryGetValue(type, out Type gm))
            {
                return Activator.CreateInstance(gm) as IEntityGripManager;
            }

            return null;
        }

        // Mouse 아래에 있는 grip point를 찾는다.
        public List<GripPoint> GetAllGripPointsUnderMouse(MouseEventArgs e)
        {
            List<GripPoint> points = new List<GripPoint>();
            // 마우스 위치에 gripPoint가 있으면 gripPoint를 선택한다.
            foreach (var te in model.TempEntities)
            {
                GripPoint gp = te as GripPoint;
                if (gp == null)
                    continue;

                // grip point의 screen 좌표
                var gripPointScreenPos = hModel.GetMouseLocationFromWorldPoint(gp.StartPoint);

                // grip point와 마우스 좌표와의 거리
                var gripPointScreenPos2D = new devDept.Geometry.Point2D(gripPointScreenPos.X, gripPointScreenPos.Y);
                var dist = gripPointScreenPos2D.DistanceTo(new devDept.Geometry.Point2D(e.Location.X, e.Location.Y));
                if (dist < gripSize)
                {
                    // 두번째 부터는 첫번째와 좌표가 같아야 함
                    if (points.Count > 0 && !points[0].StartPoint.Equals(gp.StartPoint))
                        continue;

                    points.Add(gp);
                }

            }

            return points;
        }

        // 그립을 편집 중인지?
        // 선택된 그립이 있다면 편집중이다.
        public bool EditingGripPoints()
        {
            return model.TempEntities.Exists(x => x is GripPoint && x.Selected);
        }

        // 그립 편집을 종료한다.
        public void EndEdit()
        {
            // 객체 편집된 내용을 원본에 반영한다.
            model.TempEntities.ForEach(x =>
            {
                var gp = x as GripPoint;
                if (gp == null)
                    return;

                var ent = gp.entity;
                if (ent == null)
                    return;

                var originEnt = ent.EntityData as Entity;
                if (originEnt == null)
                    return;

                var mng = GetEntityGripManager(ent);
                if (mng == null)
                    return;

                mng.EndEdit(ent, originEnt);

                originEnt.Regen(regenParams);
            });

            // 그립의 선택을 해제
            model.TempEntities.ForEach(x =>
            {
                if (x is GripPoint)
                {
                    x.Selected = false;
                    x.Color = System.Drawing.Color.Blue;

                }
            });

            model.Entities.Regen();
            model.TempEntities.Clear();
            model.Invalidate();
            model.EndWorkspace();
        }

        public void OnMouseDown(MouseEventArgs e, List<Entity> selectedEntities = null, SelectionManager.Step step = default)
        {
            if (ActionBase.runningAction != null)
                return;

           
            

            // 이미 그립이 잡혀 있다면 편집 종료
            if (EditingGripPoints())
            {
                EndEdit();
                return;
            }

            // 그림을 클릭했다면 그립 편집 시작
            // box 로 선택중일때는 grip이 선택 안되도록 해야함
            if (step != SelectionManager.Step.secondPoint)
            {
                var gripPoints = GetAllGripPointsUnderMouse(e);
                if (gripPoints != null && gripPoints.Count > 0)
                {
                    StartEdit(gripPoints);
                    return;
                }
            }
            
            // 아니면 그립을 생성함.
            if (selectedEntities != null && selectedEntities.Count < 10)
            {
                foreach (var ent in selectedEntities)
                {
                    MakeGripPoints(ent);
                }
            }
            else
            {
                var item = model.GetItemUnderMouseCursor(e.Location);
                MakeGripPoints(item?.Item as Entity);
            }
        }

        // 그립편집을시작한다.
        private void StartEdit(List<GripPoint> gripPoints)
        {
            gripPoints.ForEach(x => { x.Selected = true; x.Color = System.Drawing.Color.Yellow; });
        }

        // 마우스 이동
        public void MouseMove(MouseEventArgs e)
        {
            // 편집중이면 해당 그립을 마우스 위치로 이동한다.
            if (!EditingGripPoints())
                return;

            var newPt = ActionBase.GetPoint3DWithSnapAndOrthoMode(model, e);
            if (newPt == null)
                return;


            foreach (var ent in model.TempEntities)
            {
                var gp = ent as GripPoint;
                if (gp == null || !gp.Selected || gp.entity == null)
                    continue;


                var mng = GetEntityGripManager(gp.entity);
                if (mng == null)
                    continue;

                // 객체를 편집한다.
                mng.MouseMove(model, gp, newPt);

                gp.Position.X = newPt.X;
                gp.Position.Y = newPt.Y;
                gp.Position.Z = newPt.Z;

                // grip point regen
                gp.Regen(0.001);
            }

            model.Invalidate();
        }
    }
}
