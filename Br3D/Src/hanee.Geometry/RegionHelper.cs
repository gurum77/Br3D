using devDept.Eyeshot.Entities;
using System.Collections.Generic;

namespace hanee.Geometry
{
    static public class RegionHelper
    {
        // region을 x좌표 기준으로 trim한다.
        public static List<Region> TrimByX(this Region region, double x, bool trimLeft)
        {
            List<ICurve> trimCurves = new List<ICurve>();
            trimCurves.Add(LineHelper.CreateVerticalInfinitLine(x));

            if (trimLeft)
                trimCurves[0].Reverse();

            // trim을 한다.
            List<Region> trimmedRegions = new List<Region>();
            {
                Region[] regions;
                Region.Trim(region, trimCurves, out regions);
                if (regions == null || regions.Length == 0)
                {
                    trimmedRegions.Add(region);
                }
                else
                {
                    trimmedRegions.AddRange(regions);
                }
            }

            // valid regions
            List<Region> validRegions = new List<Region>();
            foreach (var trimmedRegion in trimmedRegions)
            {
                trimmedRegion.Regen(0.0001);
                double trimmedRegionX = (trimmedRegion.BoxMin.X + trimmedRegion.BoxMax.X) / 2;
                if (trimLeft && trimmedRegionX < x)
                    continue;
                if (!trimLeft && trimmedRegionX > x)
                    continue;

                trimmedRegion.Color = region.Color;
                trimmedRegion.ColorMethod = region.ColorMethod;
                validRegions.Add(trimmedRegion);
            }

            return validRegions;
        }


    }
}
