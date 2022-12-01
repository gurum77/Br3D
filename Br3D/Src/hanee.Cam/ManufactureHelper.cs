using devDept.Eyeshot;
using devDept.Eyeshot.Entities;
using devDept.Eyeshot.Milling;
using devDept.Geometry;

namespace hanee.Cam
{
    static public class ManufactureHelper
    {
        // simulation stock 초기화
        static public void ResetSimulationStock(this Manufacture mu)
        {
            if (mu.Setup == null)
            {
                System.Diagnostics.Debug.Assert(false);
                return;
            }
            
            mu.Entities.RemoveAll(x => x is SimulationStock);
            mu.SimulationStock = mu.Setup.Stock.GetSimulationStock();
        }

        // toolpath에 최적화된 기본 setup 생성
        static public void CreateDefaultSetup(this Manufacture mu)
        {
            Point3D CoreEntBoxMin = (Point3D)mu.Entities.BoxMin.Clone();
            Point3D CoreEntBoxMax = (Point3D)mu.Entities.BoxMax.Clone();

            var margin = mu.Entities.BoxSize.Z * 1.1;
            Interval zRangeStock1 = new Interval(CoreEntBoxMin.Z, CoreEntBoxMin.Z + margin);
            var stock = Stock.CreateBox(CoreEntBoxMin.X, CoreEntBoxMin.Y, CoreEntBoxMax.X - CoreEntBoxMin.X, CoreEntBoxMax.Y - CoreEntBoxMin.Y, zRangeStock1);
            mu.Setup = new Setup("Top", devDept.Geometry.linearUnitsType.Millimeters, Plane.XY, stock);
        }


        static public void DrawGCode(this Manufacture mu, EndMill cutter)
        {
            if (mu.Entities.Count == 0)
                return;

            Toolpath toolPath = mu.Entities.Find(x => x is Toolpath) as Toolpath;
            if (toolPath == null)
                return;

            mu.Tool = cutter;
            mu.SimulationToolpath = toolPath;
            mu.AnimationInterval = 100;

            mu.CreateDefaultSetup();
            mu.ResetSimulationStock();
        }
    }
}
