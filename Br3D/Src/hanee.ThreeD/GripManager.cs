using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace hanee.ThreeD
{
    public class GripManager
    {
        static public Dictionary<Type, Type> entityGripManagers { get; set; } = new Dictionary<Type, Type>();
        static public float gripSize = 15;
        Design design;
        HDesign hDesign => design as HDesign;

        public GripManager(Design design)
        {
            this.design = design;

            GripManager.entityGripManagers.Add(typeof(Line), typeof(LineGripManager));
            GripManager.entityGripManagers.Add(typeof(LinearPath), typeof(LinearPathGripManager));
        }

        // grip point를 모두 지운다.
        public void ClearGripPoints()
        {
            design.TempEntities.RemoveAll(x => x is GripPoint || x.EntityData is Entity);
            design.Invalidate();
        }

        // 이미 grip이 만들어진 객체인지?
        public bool HasGripPoint(Entity ent)
        {
            return design.TempEntities.Exists(x => x.EntityData == ent);
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

            var gripPoints = gm.GetGripPoints(cloneEnt);
            if (gripPoints == null)
                return;

            design.TempEntities.Add(cloneEnt);
            design.TempEntities.AddRange(gripPoints);
            design.Invalidate();
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
            foreach (var te in design.TempEntities)
            {
                GripPoint gp = te as GripPoint;
                if (gp == null)
                    continue;

                // grip point의 screen 좌표
                var gripPointScreenPos = hDesign.GetMouseLocationFromWorldPoint(gp.StartPoint);

                // grip point와 마우스 좌표와의 거리
                var gripPointScreenPos2D = new devDept.Geometry.Point2D(gripPointScreenPos.X, gripPointScreenPos.Y);
                var dist = gripPointScreenPos2D.DistanceTo(new devDept.Geometry.Point2D(e.Location.X, e.Location.Y));
                if (dist < gripSize)
                {
                    // 두번째 부터는 첫번째와 좌표가 같아야 함
                    if (points.Count > 0 && !points[0].Equals(gp.StartPoint))
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
            return design.TempEntities.Exists(x => x is GripPoint && x.Selected);
        }

        // 그립 편집을 종료한다.
        public void EndEdit()
        {
            // 객체 편집된 내용을 원본에 반영한다.
            design.TempEntities.ForEach(x =>
            {
                if (x is GripPoint)
                    return;

                var ent = x as Entity;
                if (ent == null)
                    return;

                var originEnt = ent.EntityData as Entity;
                if (originEnt == null)
                    return;

                if (ent.Vertices.Length != originEnt.Vertices.Length)
                    return;

                for (int i = 0; i < ent.Vertices.Length; ++i)
                {
                    originEnt.Vertices[i].X = ent.Vertices[i].X;
                    originEnt.Vertices[i].Y = ent.Vertices[i].Y;
                    originEnt.Vertices[i].Z = ent.Vertices[i].Z;
                }

                originEnt.Regen(new RegenParams(0.001, design));
            });

            // 그립의 선택을 해제
            design.TempEntities.ForEach(x =>
            {
                if (x is GripPoint)
                {
                    x.Selected = false;
                    x.Color = System.Drawing.Color.Blue;

                }
            });

            design.Entities.Regen();
            design.Invalidate();
        }

        public void MouseUp(MouseEventArgs e)
        {
            // 이미 그립이 잡혀 있다면 편집 종료
            if (EditingGripPoints())
            {
                EndEdit();
                return;
            }

            // 그림을 클릭했다면 그립 편집 시작
            var gripPoints = GetAllGripPointsUnderMouse(e);
            if (gripPoints != null && gripPoints.Count > 0)
            {
                StartEdit(gripPoints);
                return;
            }


            // 아니면 그립을 생성함.
            var item = design.GetItemUnderMouseCursor(e.Location);
            MakeGripPoints(item?.Item as Entity);
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

            var newPt = ActionBase.GetPoint3DByMouseLocation(design, e.Location);
            if (newPt == null)
                return;

            design.TempEntities.ForEach(x =>
            {
                var gp = x as GripPoint;
                if (gp == null || !gp.Selected || gp.entity == null)
                    return;
                gp.Position.X = newPt.X;
                gp.Position.Y = newPt.Y;
                gp.Position.Z = newPt.Z;

                gp.entity.Regen(0.001);
                gp.Regen(0.001);
            });

            design.Invalidate();
        }
    }
}
