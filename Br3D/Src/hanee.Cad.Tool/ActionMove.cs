﻿using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using hanee.ThreeD;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hanee.Cad.Tool
{
    public class ActionMove : ActionBase
    {
        protected Point3D fromPoint = null;
        protected Point3D toPoint = null;
        protected Point3D lastPoint = null;

        public ActionMove(Environment environment) : base(environment)
        {
        }

        public override async void Run()
        { await RunAsync(); }

        protected override void OnMouseMove(Environment environment, MouseEventArgs e)
        {
            base.OnMouseMove(environment, e);

            if (fromPoint == null)
                return;

            var vec = point3D - lastPoint;
            foreach (var ent in environment.TempEntities)
            {
                ent.Translate(vec.X, vec.Y, vec.Z);
            }

            lastPoint = point3D.Clone() as Point3D;
        }

        public async Task<bool> RunAsync()
        {
            StartAction();

            while (true)
            {
                environment.TempEntities.Clear();
                fromPoint = null;
                toPoint = null;
                lastPoint = null;

                var entities = await GetEntities("Select entities");
                if (IsCanceled())
                    break;

                fromPoint = await GetPoint3D("From point");
                if (IsCanceled())
                    break;

                // temp entities로 설정
                entities.ToTempEntities(GetModel());

                lastPoint = fromPoint.Clone() as Point3D;

                toPoint = await GetPoint3D("To point");
                if (IsCanceled())
                    break;

                var vec = toPoint - fromPoint;

                Finish(entities, vec.AsVector);
                
                break;
            }

            EndAction();
            return true;
        }

        virtual protected void Finish(List<Entity> entities, Vector3D vec)
        {
            var entityList = new EntityList();
            entityList.AddRange(entities);
            entityList.Translate(vec.X, vec.Y, vec.Z);
            GetModel().Entities.Regen();
            GetModel().Invalidate();
        }
    }
}