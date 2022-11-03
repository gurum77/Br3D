using devDept.Eyeshot.Entities;
using devDept.Geometry;
using devDept.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
//TODO devDept 2022: Eyeshot.Environment class has been renamed in Eyeshot.Workspace.
//using Environment = devDept.Eyeshot.Environment;
using Environment = devDept.Eyeshot.Environment;

namespace hanee.ThreeD
{
    public class Snapping
    {
        Environment environment;
        public Snapping(Environment environment)
        {
            this.environment = environment;
            SnapSymbolSize = 12;
        }

        [Flags]
        public enum objectSnapType
        {
            None = 0,
            Point = 1 << 2,
            End = 1 << 3,
            Mid = 1 << 4,
            Center = 1 << 5,
            Quad = 1 << 6,
            Grid = 1 << 7,    // 가장 우선순위가 낮음(객체에 스냅이 걸리지 않을경우 여기를 검사한다)
            Intersect = 1 << 8,
            All = Point | End | Mid | Center | Quad | Grid | Intersect
        }

        // Current snapped point, which is one of the vertex from model
        private Point3D snapPoint { get; set; }

        // Flags to indicate current snapping mode
        // snap을 활성화 하려면 이 속성을 TRUE를 줘야한다.(프로그램 시작할때 주면 된다)
        public bool objectSnapEnabled { get; set; }

        public bool waitingForSelection { get; set; }

        // snap 모드를 활성/비활성화 한다.
        public void FlagActiveObjectSnap(objectSnapType type)
        {
            SetActiveObjectSnap(type, !IsActiveObjectSnap(type));
        }

        // snap이 활성화 되어 있는지?
        public bool IsActiveObjectSnap(objectSnapType type)
        {
            return activeObjectSnap.HasFlag(type);
        }
        // object snap 활성화 함수
        // 다중 object snap이 되도록 수정해야함
        public void SetActiveObjectSnap(objectSnapType type, bool active)
        {
            if (active == true)
                activeObjectSnap = activeObjectSnap | type;
            else
                activeObjectSnap = activeObjectSnap & ~type;
        }

        objectSnapType activeObjectSnap = objectSnapType.None;
        public SnapPoint snap;
        SnapPoint[] snapPoints;
        bool currentlySnapping = false;

        // snap이 되어 있는상태인지?
        public bool CurrentlySnapping
        {
            get { return currentlySnapping; }
            set { currentlySnapping = value; }
        }

        public int SnapSymbolSize { get; set; }

        public SnapPoint GetSnap()
        {
            return snap;
        }
        // snap된 포인트
        public Point3D GetSnapPoint()
        {
            return snapPoint;
        }


        // onmousemove에서 호출
        public void OnMouseMoveForSnap(System.Windows.Forms.MouseEventArgs e)
        {
            if (objectSnapEnabled && e.Button != System.Windows.Forms.MouseButtons.Middle)
            {
                this.snapPoint = null;
                //snapPoints = GetSnapPoints(cursorPoint);
                snapPoints = GetSnapPoints(e.Location);
            }
        }

        // draw overlay 함수에서 호출
        public void DrawOverlayForSnap()
        {
            if (objectSnapEnabled && snapPoints != null && snapPoints.Length > 0)
            {
                snap = FindClosestPoint(snapPoints);
                currentlySnapping = true;
            }
            else
            {
                currentlySnapping = false;
            }
        }

        // 스크린에서의 지정 좌표와 가장 가까운 좌표와의 거리
        public double ClosestDistanceOnScreen(Point3D ptWorld, List<Point3D> ptWorlds)
        {
            double distMin = 0;

            bool first = true;
            foreach (var pt in ptWorlds)
            {
                double dist = DistanceOnScreen(ptWorld, pt);
                if (first || dist < distMin)
                {
                    distMin = dist;
                    first = false;
                }
            }
            return distMin;
        }

        // 스크린에서의 2개의 world 좌표간의 거리
        public double DistanceOnScreen(Point3D ptWorld1, Point3D ptWorld2)
        {
            Point3D ptScreen1 = environment.WorldToScreen(ptWorld1);
            Point3D ptScreen2 = environment.WorldToScreen(ptWorld2);

            return Point2D.Distance(ptScreen1, ptScreen2);
        }

        /// <summary>
        /// Finds closest snap point.
        /// </summary>
        /// <param name="snapPoints">Array of snap points</param>
        /// <returns>Closest snap point.</returns>
        public SnapPoint FindClosestPoint(SnapPoint[] snapPoints)
        {
            if (snapPoints == null || snapPoints.Length == 0)
                return null;


            double minDist = double.MaxValue;

            int i = 0;
            int index = 0;

            foreach (SnapPoint vertex in snapPoints)
            {
                Point3D vertexScreen = environment.WorldToScreen(vertex);
                Point2D currentScreen = new Point2D(ActionBase.CurrentMousePoint.X, environment.Height - ActionBase.CurrentMousePoint.Y);

                double dist = Point2D.Distance(vertexScreen, currentScreen);

                if (dist < minDist)
                {
                    index = i;
                    minDist = dist;
                }

                i++;
            }

            SnapPoint snap = (SnapPoint)snapPoints.GetValue(index);
            DisplaySnappedVertex(snap);

            return snap;
        }


        /// <summary>
        /// Displays symbols associated with the snapped vertex type
        /// </summary>
        private void DisplaySnappedVertex(SnapPoint snap)
        {
            environment.renderContext.SetLineSize(2, true, true);

            // white color
            environment.renderContext.SetColorWireframe(Color.FromArgb(255, 255, 0));
            environment.renderContext.SetState(depthStencilStateType.DepthTestOff);

            Point2D onScreen = environment.WorldToScreen(snap);

            this.snapPoint = snap;

            switch (snap.Type)
            {
                case objectSnapType.Point:
                    DrawCircle(new System.Drawing.Point((int)onScreen.X, (int)(onScreen.Y)));
                    DrawCross(new System.Drawing.Point((int)onScreen.X, (int)(onScreen.Y)));
                    break;
                case objectSnapType.Center:
                    DrawCircle(new System.Drawing.Point((int)onScreen.X, (int)(onScreen.Y)));
                    break;
                case objectSnapType.End:
                    DrawQuad(new System.Drawing.Point((int)onScreen.X, (int)(onScreen.Y)));
                    break;
                case objectSnapType.Mid:
                    DrawTriangle(new System.Drawing.Point((int)onScreen.X, (int)(onScreen.Y)));
                    break;
                case objectSnapType.Intersect:
                    DrawCross(new System.Drawing.Point((int)onScreen.X, (int)(onScreen.Y)));
                    DrawQuad(new System.Drawing.Point((int)onScreen.X, (int)(onScreen.Y)));
                    break;
                case objectSnapType.Quad:
                    environment.renderContext.SetLineSize(3.0f);
                    DrawRhombus(new System.Drawing.Point((int)onScreen.X, (int)(onScreen.Y)));
                    break;
            }

            environment.renderContext.SetLineSize(1);
        }

        /// <summary>
        /// Adds entity to scene on active layer and refresh the screen. 
        /// </summary>
        public void AddAndRefresh(Entity entity, int layerIndex)
        {
            // increase dimension of points
            if (entity is devDept.Eyeshot.Entities.Point)
            {
                entity.LineWeightMethod = colorMethodType.byEntity;
                entity.LineWeight = 3;
            }

            // avoid dimensions with width bigger than one
            if (entity is Dimension || entity is Leader)
            {
                //entity.LayerIndex = layerIndex;
                entity.LineWeightMethod = colorMethodType.byEntity;

                environment.Entities.Add(entity);
            }
            else
            {
                if (layerIndex < environment.Layers.Count)
                {
                    environment.Entities.Add(entity, environment.Layers[layerIndex].Name);
                }

            }

            environment.Entities.Regen();
            environment.Invalidate();
        }

     


        #region snap 찾기

        // point 객체 스냅 찾기
        SnapPoint[] GetSnapPoints_Point(devDept.Eyeshot.Entities.Point point)
        {
            List<SnapPoint> pointSnapPoints = new List<SnapPoint>();
            if (activeObjectSnap.HasFlag(objectSnapType.Point) == true)
            {
                Point3D point3d = point.Vertices[0];
                pointSnapPoints.Add(new SnapPoint(point3d, objectSnapType.Point, point));
            }

            return pointSnapPoints.ToArray();
        }

        // line 객체 스냅 찾기
        SnapPoint[] GetSnapPoints_Line(devDept.Eyeshot.Entities.Line line)
        {
            List<SnapPoint> lineSnapPoints = new List<SnapPoint>();
            if (activeObjectSnap.HasFlag(objectSnapType.End) == true)
            {
                lineSnapPoints.Add(new SnapPoint(line.StartPoint, objectSnapType.End, line));
                lineSnapPoints.Add(new SnapPoint(line.EndPoint, objectSnapType.End, line));
            }

            if (activeObjectSnap.HasFlag(objectSnapType.Mid))
            {
                lineSnapPoints.Add(new SnapPoint(line.MidPoint, objectSnapType.Mid, line));
            }

            return lineSnapPoints.ToArray();
        }

        // linePath 객체 스냅 찾기
        SnapPoint[] GetSnapPoints_LinearPath(LinearPath polyline, Entity otherEnt = null)
        {
            List<SnapPoint> polyLineSnapPoints = new List<SnapPoint>();

            if (activeObjectSnap.HasFlag(objectSnapType.End))
            {
                foreach (Point3D point in polyline.Vertices)
                    polyLineSnapPoints.Add(new SnapPoint(point, objectSnapType.End, polyline));
            }

            if (activeObjectSnap.HasFlag(objectSnapType.Mid))
            {
                for (int ix = 1; ix < polyline.Vertices.Length; ++ix)
                {
                    polyLineSnapPoints.Add(new SnapPoint((polyline.Vertices[ix - 1] + polyline.Vertices[ix]) / 2, objectSnapType.Mid, polyline));
                }
            }

            //if (activeObjectSnap.HasFlag(objectSnapType.Intersect) && otherEnt != null)
            //{
            //    ICurve otherCurve = otherEnt as ICurve;
            //    if (otherCurve != null)
            //    {
            //        var matchPoints = polyline.IntersectWith(otherCurve);
            //        if (matchPoints != null)
            //        {
            //            foreach (var mp in matchPoints)
            //            {
            //                polyLineSnapPoints.Add(new SnapPoint(mp, objectSnapType.Intersect, polyline));
            //            }
            //        }
            //    }

            //}


            return polyLineSnapPoints.ToArray();
        }

        // compositeCurve 객체 스냅 찾기
        SnapPoint[] GetSnapPoints_CompositeCurve(CompositeCurve composite)
        {
            List<SnapPoint> polyLineSnapPoints = new List<SnapPoint>();

            foreach (ICurve curveSeg in composite.CurveList)
            {
                if (curveSeg is Curve)
                {
                    SnapPoint[] curSnapPoints = GetSnapPoints_Curve((Curve)curveSeg);
                    foreach (var s in curSnapPoints)
                        polyLineSnapPoints.Add(s);
                }

            }


            return polyLineSnapPoints.ToArray();
        }

        // arc 객체 스냅 찾기
        SnapPoint[] GetSnapPoints_Arc(Arc arc)
        {
            List<SnapPoint> arcSnapPoints = new List<SnapPoint>();

            if (activeObjectSnap.HasFlag(objectSnapType.End))
            {
                arcSnapPoints.Add(new SnapPoint(arc.StartPoint, objectSnapType.End, arc));
                arcSnapPoints.Add(new SnapPoint(arc.EndPoint, objectSnapType.End, arc));
            }

            if (activeObjectSnap.HasFlag(objectSnapType.Mid))
            {
                arcSnapPoints.Add(new SnapPoint(arc.MidPoint, objectSnapType.Mid, arc));
            }

            if (activeObjectSnap.HasFlag(objectSnapType.Center))
            {
                arcSnapPoints.Add(new SnapPoint(arc.Center, objectSnapType.Center, arc));
            }


            return arcSnapPoints.ToArray();
        }


        // circle 객체 스냅 찾기
        SnapPoint[] GetSnapPoints_Circle(Circle circle)
        {
            List<SnapPoint> circleSnapPoints = new List<SnapPoint>();

            if (activeObjectSnap.HasFlag(objectSnapType.End))
            {
                circleSnapPoints.Add(new SnapPoint(circle.EndPoint, objectSnapType.End, circle));
            }

            if (activeObjectSnap.HasFlag(objectSnapType.Mid))
            {
                circleSnapPoints.Add(new SnapPoint(circle.PointAt(circle.Domain.Mid), objectSnapType.Mid, circle));
            }

            if (activeObjectSnap.HasFlag(objectSnapType.Center))
            {
                circleSnapPoints.Add(new SnapPoint(circle.Center, objectSnapType.Center, circle));
            }

            if (activeObjectSnap.HasFlag(objectSnapType.Quad))
            {
                Point3D quad1 = new Point3D(circle.Center.X, circle.Center.Y + circle.Radius);
                Point3D quad2 = new Point3D(circle.Center.X + circle.Radius, circle.Center.Y);
                Point3D quad3 = new Point3D(circle.Center.X, circle.Center.Y - circle.Radius);
                Point3D quad4 = new Point3D(circle.Center.X - circle.Radius, circle.Center.Y);

                circleSnapPoints.Add(new SnapPoint(quad1, objectSnapType.Quad, circle));
                circleSnapPoints.Add(new SnapPoint(quad2, objectSnapType.Quad, circle));
                circleSnapPoints.Add(new SnapPoint(quad3, objectSnapType.Quad, circle));
                circleSnapPoints.Add(new SnapPoint(quad4, objectSnapType.Quad, circle));
            }


            return circleSnapPoints.ToArray();
        }

        // curve 객체 스냅 찾기
        SnapPoint[] GetSnapPoints_Curve(Curve curve)
        {
            List<SnapPoint> curveSnapPoints = new List<SnapPoint>();

            if (activeObjectSnap.HasFlag(objectSnapType.End))
            {
                curveSnapPoints.Add(new SnapPoint(curve.StartPoint, objectSnapType.End, curve));
                curveSnapPoints.Add(new SnapPoint(curve.EndPoint, objectSnapType.End, curve));
            }

            if (activeObjectSnap.HasFlag(objectSnapType.Mid))
            {
                curveSnapPoints.Add(new SnapPoint(curve.PointAt(0.5), objectSnapType.Mid, curve));
            }


            return curveSnapPoints.ToArray();
        }

        // EllipticalArc 객체 스냅 찾기
        SnapPoint[] GetSnapPoints_EllipticalArc(EllipticalArc elArc)
        {
            List<SnapPoint> elArcSnapPoints = new List<SnapPoint>();

            if (activeObjectSnap.HasFlag(objectSnapType.End))
            {
                elArcSnapPoints.Add(new SnapPoint(elArc.StartPoint, objectSnapType.End, elArc));
                elArcSnapPoints.Add(new SnapPoint(elArc.EndPoint, objectSnapType.End, elArc));
            }

            if (activeObjectSnap.HasFlag(objectSnapType.Center))
            {
                elArcSnapPoints.Add(new SnapPoint(elArc.Center, objectSnapType.Center, elArc));
            }

            return elArcSnapPoints.ToArray();
        }

        // Ellipse 객체 스냅 찾기
        SnapPoint[] GetSnapPoints_Ellipse(Ellipse ellipse)
        {
            List<SnapPoint> ellipseSnapPoints = new List<SnapPoint>();


            if (activeObjectSnap.HasFlag(objectSnapType.End))
            {
                ellipseSnapPoints.Add(new SnapPoint(ellipse.EndPoint, objectSnapType.End, ellipse));
            }

            if (activeObjectSnap.HasFlag(objectSnapType.Mid))
            {
                ellipseSnapPoints.Add(new SnapPoint(ellipse.PointAt(ellipse.Domain.Mid), objectSnapType.Mid, ellipse));
            }

            if (activeObjectSnap.HasFlag(objectSnapType.Center))
            {
                ellipseSnapPoints.Add(new SnapPoint(ellipse.Center, objectSnapType.Center, ellipse));
            }


            return ellipseSnapPoints.ToArray();
        }

        // mesh 객체 스냅 찾기
        SnapPoint[] GetSnapPoints_Mesh(Mesh mesh, System.Drawing.Point mouseLocation)
        {
            List<SnapPoint> meshSnapPoints = new List<SnapPoint>();

            if (activeObjectSnap.HasFlag(objectSnapType.End))
            {
                for (int i = 0; i < mesh.Vertices.Length; i++)
                {
                    Point3D pt = mesh.Vertices[i];
                    meshSnapPoints.Add(new SnapPoint(pt, objectSnapType.End, mesh));
                }
            }
            else if (activeObjectSnap.HasFlag(objectSnapType.Mid))
            {
                foreach (var edge in mesh.Edges)
                {
                    Point3D pt1 = mesh.Vertices[edge.V1];
                    Point3D pt2 = mesh.Vertices[edge.V2];
                    meshSnapPoints.Add(new SnapPoint((pt1 + pt2) / 2, objectSnapType.Mid, mesh));
                }
            }


            return meshSnapPoints.ToArray();
        }

        /// <summary>
        /// identify snapPoints of the entity under mouse cursor in that moment, using PickBoxSize as tolerance
        /// </summary>
        public SnapPoint[] GetSnapPoints(System.Drawing.Point mouseLocation)
        {

            SnapPoint[] snapPoints = new SnapPoint[0];
            if (activeObjectSnap == objectSnapType.None)
                return snapPoints;

            // 선택 액션중일때는 snap 찾지 않는다.
            if (ActionBase.IsSelectingEntity())
                return snapPoints;

            //changed PickBoxSize to define a range for display snapPoints
            int oldSize = environment.PickBoxSize;
            environment.PickBoxSize = 20;


            try
            {

                //select the entity under mouse cursor
                if (!environment.IsOpenRootLevel)
                    environment.ResetOpenBlocks(false);

                var entities = environment.GetAllEntitiesUnderMouseCursor(mouseLocation);
                if (entities == null || entities.Length == 0)
                    return snapPoints;


                var entity = environment.Entities[entities[0]];
                var entity2 = entities.Length > 1 ? environment.Entities[entities[1]] : null;
                // 
                //exctract the entity selected with GetEntityUnderMouseCursor
                // BlockReference이면 BlockReference 내부에서 다시 검색
                if (entity is BlockReference)
                {
                    BlockReference br = entity as BlockReference;
                    entity.Selected = true;
                    environment.SetSelectionAsCurrent(false);
                    //environment.SetCurrent(br);
                    var entityIndex = environment.GetEntityUnderMouseCursor(mouseLocation);
                    if (entityIndex > -1)
                        entity = environment.Entities[entityIndex];
                    environment.ResetOpenBlocks(false);
                    //environment.SetCurrent(null);

                    snapPoints = GetSnapPointsFromEntity(entity, mouseLocation, entity2);


                    // block이면 transform해야함
                    var trans = br.GetFullTransformation(environment.Blocks);
                    if (trans != null && snapPoints != null)
                    {
                        foreach (var p in snapPoints)
                        {
                            p.TransformBy(trans);
                        }
                    }
                }
                else
                {
                    snapPoints = GetSnapPointsFromEntity(entity, mouseLocation, entity2);
                }
            }
            catch (Exception)
            {

            }


            //// 그리드 스냅 on이고, 다른 스냅을 찾지 못 한 경우
            //if (snapPoints != null && snapPoints.Count() == 0 && activeObjectSnap.HasFlag(objectSnapType.Grid))
            //{

            //    Point3D pt3D = ActionBase.GetPoint3DByMouseLocation(environment, mouseLocation);
            //    if (SnapToGrid(ref pt3D))
            //    {
            //        List<SnapPoint> gridSnapPoints = new List<SnapPoint>();
            //        gridSnapPoints.Add(new SnapPoint(pt3D, objectSnapType.Grid, null));
            //        snapPoints = gridSnapPoints.ToArray();
            //    }

            //}

            environment.PickBoxSize = oldSize;
            return snapPoints;
        }

        private SnapPoint[] GetSnapPointsFromEntity(Entity ent, System.Drawing.Point mouseLocation, Entity otherEnt = null)
        {
            if (ent == null)
                return null;

            SnapPoint[] curSnapPoints = null;
            //check wich type of entity is it and then,identify snap points
            if (ent is devDept.Eyeshot.Entities.Point)
            {
                devDept.Eyeshot.Entities.Point point = (devDept.Eyeshot.Entities.Point)ent;
                curSnapPoints =  GetSnapPoints_Point(point);
            }
            
            else if (ent is Line) //line
            {
                Line line = (Line)ent;
                curSnapPoints = GetSnapPoints_Line(line);
            }
            else if (ent is LinearPath)//polyline
            {
                LinearPath polyline = (LinearPath)ent;
                curSnapPoints = GetSnapPoints_LinearPath(polyline);
            }
            else if (ent is CompositeCurve)//composite
            {
                CompositeCurve composite = (CompositeCurve)ent;
                curSnapPoints = GetSnapPoints_CompositeCurve(composite);
            }
            else if (ent is Arc) //Arc
            {
                Arc arc = (Arc)ent;
                curSnapPoints = GetSnapPoints_Arc(arc);
            }
            else if (ent is Circle) //Circle
            {
                Circle circle = (Circle)ent;
                curSnapPoints = GetSnapPoints_Circle(circle);
            }
            else if (ent is Curve) // Spline
            {
                Curve curve = (Curve)ent;
                curSnapPoints = GetSnapPoints_Curve(curve);
            }
            else if (ent is EllipticalArc) //Elliptical Arc
            {
                EllipticalArc elArc = (EllipticalArc)ent;
                curSnapPoints = GetSnapPoints_EllipticalArc(elArc);
            }
            else if (ent is Ellipse) //Ellipse
            {
                Ellipse ellipse = (Ellipse)ent;
                curSnapPoints = GetSnapPoints_Ellipse(ellipse);

            }
            else if (ent is Mesh) //Mesh
            {
                Mesh mesh = (Mesh)ent;
                curSnapPoints = GetSnapPoints_Mesh(mesh, mouseLocation);
            }
            else if (ent is devDept.Eyeshot.Entities.Brep)
            {
                List<SnapPoint> snapPoints = new List<SnapPoint>();
                var brep = ent as Brep;
                foreach(var edge in brep.Edges)
                {
                    SnapPoint[] points = GetSnapPointsFromEntity(edge.Curve as Entity, mouseLocation);
                    if (points == null)
                        continue;

                    snapPoints.AddRange(points);
                }
                curSnapPoints = snapPoints.ToArray();
            }
            else if (ent is BlockReference)
            {
                List<SnapPoint> snapPoints = new List<SnapPoint>();
                BlockReference br = (BlockReference)ent;
                Transformation trans = br.GetFullTransformation(environment.Blocks);
                foreach (var subEnt in br.GetEntities(environment.Blocks))
                {
                    SnapPoint[] points = GetSnapPointsFromEntity(subEnt, mouseLocation);
                    if (points == null)
                        continue;
                    
                    foreach (var p in points)
                        p.TransformBy(trans);

                    snapPoints.AddRange(points);
                }
                curSnapPoints = snapPoints.ToArray();
            }

            // 교점 검색
            if (activeObjectSnap.HasFlag(objectSnapType.Intersect) && otherEnt != null)
            {
                List<SnapPoint> intersectSnapPoints = new List<SnapPoint>();
                var curve = ent as ICurve;
                var otherCurve = otherEnt as ICurve;
                if (curve != null && otherCurve != null)
                {
                    var matchPoints = curve.IntersectWith(otherCurve);
                    if (matchPoints != null)
                    {
                        foreach (var mp in matchPoints)
                        {
                            intersectSnapPoints.Add(new SnapPoint(mp, objectSnapType.Intersect, ent));
                        }
                    }
                }

                intersectSnapPoints.AddRange(curSnapPoints);
                curSnapPoints = intersectSnapPoints.ToArray();
            }

            return curSnapPoints;
        }
        #endregion

        #region SnappingData

        /// <summary>
        /// Represents a 3D point from model vertices and associated snap type.
        /// </summary>
        public class SnapPoint : devDept.Geometry.Point3D
        {
            public objectSnapType Type;
            public Entity entity;

            public SnapPoint()
                : base()
            {
                Type = objectSnapType.None;
            }

            public SnapPoint(Point3D point3D) : base(point3D.X, point3D.Y, point3D.Z)
            {
                this.Type = objectSnapType.None;
            }

            public SnapPoint(Point3D point3D, objectSnapType objectSnapType, Entity ent) : base(point3D.X, point3D.Y, point3D.Z)
            {
                this.Type = objectSnapType;
                this.entity = ent;
            }

            public override string ToString()
            {
                return base.ToString() + " | " + Type;
            }
        }
        #endregion

        #region Snapping symbols



        /// <summary>
        /// Draw cross. Drawn with a circle identifies a single point
        /// </summary>
        public void DrawCross(System.Drawing.Point onScreen)
        {
            double dim1 = onScreen.X + (SnapSymbolSize / 2);
            double dim2 = onScreen.Y + (SnapSymbolSize / 2);
            double dim3 = onScreen.X - (SnapSymbolSize / 2);
            double dim4 = onScreen.Y - (SnapSymbolSize / 2);

            Point3D topLeftVertex = new Point3D(dim3, dim2);
            Point3D topRightVertex = new Point3D(dim1, dim2);
            Point3D bottomRightVertex = new Point3D(dim1, dim4);
            Point3D bottomLeftVertex = new Point3D(dim3, dim4);

            environment.renderContext.DrawLines(
                new Point3D[]
                {
                    bottomLeftVertex,
                    topRightVertex,

                    topLeftVertex,
                    bottomRightVertex,

                });
        }

        /// <summary>
        /// Draw circle with openGl to rapresent CENTER snap point
        /// </summary>
        public void DrawCircle(System.Drawing.Point onScreen)
        {
            double radius = SnapSymbolSize / 2;

            double x2 = 0, y2 = 0;

            List<Point3D> pts = new List<Point3D>();

            for (int angle = 0; angle < 360; angle += 10)
            {
                double rad_angle = Utility.DegToRad(angle);

                x2 = onScreen.X + radius * Math.Cos(rad_angle);
                y2 = onScreen.Y + radius * Math.Sin(rad_angle);

                Point3D circlePoint = new Point3D(x2, y2);
                pts.Add(circlePoint);
            }

            environment.renderContext.DrawLineLoop(pts.ToArray());
        }

        /// <summary>
        /// Draw quad with openGl to rapresent END snap point
        /// </summary>
        public void DrawQuad(System.Drawing.Point onScreen)
        {
            double dim1 = onScreen.X + (SnapSymbolSize / 2);
            double dim2 = onScreen.Y + (SnapSymbolSize / 2);
            double dim3 = onScreen.X - (SnapSymbolSize / 2);
            double dim4 = onScreen.Y - (SnapSymbolSize / 2);

            Point3D topLeftVertex = new Point3D(dim3, dim2);
            Point3D topRightVertex = new Point3D(dim1, dim2);
            Point3D bottomRightVertex = new Point3D(dim1, dim4);
            Point3D bottomLeftVertex = new Point3D(dim3, dim4);

            environment.renderContext.DrawLineLoop(new Point3D[]
            {
                bottomLeftVertex,
                bottomRightVertex,
                topRightVertex,
                topLeftVertex
            });
        }

        /// <summary>
        /// Draw triangle with openGl to rapresent MID snap point
        /// </summary>
        void DrawTriangle(System.Drawing.Point onScreen)
        {
            double dim1 = onScreen.X + (SnapSymbolSize / 2);
            double dim2 = onScreen.Y + (SnapSymbolSize / 2);
            double dim3 = onScreen.X - (SnapSymbolSize / 2);
            double dim4 = onScreen.Y - (SnapSymbolSize / 2);
            double dim5 = onScreen.X;

            Point3D topCenter = new Point3D(dim5, dim2);

            Point3D bottomRightVertex = new Point3D(dim1, dim4);
            Point3D bottomLeftVertex = new Point3D(dim3, dim4);

            environment.renderContext.DrawLineLoop(new Point3D[]
            {
                bottomLeftVertex,
                bottomRightVertex,
                topCenter
            });
        }


        void DrawRhombus(System.Drawing.Point onScreen)
        {
            double dim1 = onScreen.X + (SnapSymbolSize / 1.5);
            double dim2 = onScreen.Y + (SnapSymbolSize / 1.5);
            double dim3 = onScreen.X - (SnapSymbolSize / 1.5);
            double dim4 = onScreen.Y - (SnapSymbolSize / 1.5);

            Point3D topVertex = new Point3D(onScreen.X, dim2);
            Point3D bottomVertex = new Point3D(onScreen.X, dim4);
            Point3D rightVertex = new Point3D(dim1, onScreen.Y);
            Point3D leftVertex = new Point3D(dim3, onScreen.Y);

            environment.renderContext.DrawLineLoop(new Point3D[]
            {
                bottomVertex,
                rightVertex,
                topVertex,
                leftVertex,
            });
        }

        #endregion
    }
}
