using devDept.Eyeshot;
using devDept.Eyeshot.Translators;
using devDept.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hanee.ThreeD
{
    public class Units
    {
        public static string GetPointString(double pos)
        {
            return $"{pos:0.000}";
        }

        // point를 문자열로
        public static string GetPointString(double x, double y)
        {
            return $"X:{GetPointString(x)} Y:{GetPointString(y)}";
        }

        // point를 문자열로
        public static string GetPointString(double x, double y, double z)
        {
            return $"X:{GetPointString(x)} Y:{GetPointString(y)} Z:{GetPointString(z)}";
        }

        // 길이단위 문자열 리턴
        public static string GetLengthUnitsString(linearUnitsType type)
        {
            if (type == linearUnitsType.Unitless)
                return "Unitless";
            else if (type == linearUnitsType.Inches)
                return "in";
            else if (type == linearUnitsType.Feet)
                return "ft";
            else if (type == linearUnitsType.Miles)
                return "mile";
            else if (type == linearUnitsType.Millimeters)
                return "mm";
            else if (type == linearUnitsType.Centimeters)
                return "cm";
            else if (type == linearUnitsType.Meters)
                return "m";
            else if (type == linearUnitsType.Kilometers)
                return "km";
            else if (type == linearUnitsType.Microinches)
                return "Microinches";
            else if (type == linearUnitsType.Mils)
                return "Mils";
            else if (type == linearUnitsType.Yards)
                return "yd";
            else if (type == linearUnitsType.Angstroms)
                return "Angstroms";
            else if (type == linearUnitsType.Nanometers)
                return "Nanometers";
            else if (type == linearUnitsType.Microns)
                return "Microns";
            else if (type == linearUnitsType.Decimeters)
                return "Decimeters";
            else if (type == linearUnitsType.Decameters)
                return "Decameters";
            else if (type == linearUnitsType.Hectometers)
                return "Hectometers";
            else if (type == linearUnitsType.Gigameters)
                return "Gigameters";
            else if (type == linearUnitsType.Astronomical)
                return "Astronomical";
            else if (type == linearUnitsType.LightYears)
                return "LightYears";
            else if (type == linearUnitsType.Parsecs)
                return "Parsecs";

            return "";
        }

        // 면적 단위를 문자로
        public static string GetAreaUnitsString(linearUnitsType type)
        {
            if (type == linearUnitsType.Centimeters)
                return "㎠";
            else if (type == linearUnitsType.Meters)
                return "㎡";
            else if (type == linearUnitsType.Kilometers)
                return "㎢";
            else if (type == linearUnitsType.Millimeters)
                return "㎟";

            return type.ToString() + "2";
        }

        // 체적 단위를 문자로
        public static string GetVolumeUnitsString(linearUnitsType type)
        {
            if (type == linearUnitsType.Centimeters)
                return "㎤";
            else if (type == linearUnitsType.Meters)
                return "㎥";
            else if (type == linearUnitsType.Kilometers)
                return "㎦";
            else if (type == linearUnitsType.Millimeters)
                return "㎣";

            return type.ToString() + "3";
        }
        
        // dist를 toUnits 단위로 변환
        public static double ConvertTo(double distInM, linearUnitsType toUnits)
        {
            return distInM * UtilityEx.GetLinearUnitsConversionFactor(linearUnitsType.Meters, toUnits);
        }

        public static double ConvertTo(double dist, linearUnitsType frUnits, linearUnitsType toUnits)
        {
            return dist * UtilityEx.GetLinearUnitsConversionFactor(frUnits, toUnits);
        }

        // 읽어온 파일에 있는 객체의 단위계를 viewport에 맞춘다.
        public static void AdjustUnitsForEntitiesRead(Model vp, ReadFileAsync rf)
        {
            double factor = 1;
         
            if(rf is ReadFileAsyncWithBlocks)
            {
                ReadFileAsyncWithBlocks rfwb = (ReadFileAsyncWithBlocks)rf;
                factor = UtilityEx.GetLinearUnitsConversionFactor(rfwb.Units, vp.CurrentBlock.Units);
            }

            if(rf.Entities != null)
            {
                Transformation trans = new Transformation();
                trans.Scaling(factor, factor, factor);
                foreach (var ent in rf.Entities)
                {
                    ent.TransformBy(trans);
                }
            }
        }

        static linearUnitsType SupportedLinearUnitsTypeToLinearUnitsType(supportedLinearUnitsType fr)
        {
            if (fr == supportedLinearUnitsType.Inches)
                return linearUnitsType.Inches;
            else if (fr == supportedLinearUnitsType.Feet)
                return linearUnitsType.Feet;
            else if (fr == supportedLinearUnitsType.Miles)
                return linearUnitsType.Miles;
            else if (fr == supportedLinearUnitsType.Millimeters)
                return linearUnitsType.Millimeters;
            else if (fr == supportedLinearUnitsType.Centimeters)
                return linearUnitsType.Centimeters;
            else if (fr == supportedLinearUnitsType.Meters)
                return linearUnitsType.Meters;
            else if (fr == supportedLinearUnitsType.Kilometers)
                return linearUnitsType.Kilometers;
            else if (fr == supportedLinearUnitsType.Microinches)
                return linearUnitsType.Microinches;
            else if (fr == supportedLinearUnitsType.Mils)
                return linearUnitsType.Mils;
            else if (fr == supportedLinearUnitsType.Yards)
                return linearUnitsType.Yards;
            else if (fr == supportedLinearUnitsType.Angstroms)
                return linearUnitsType.Angstroms;
            else if (fr == supportedLinearUnitsType.Nanometers)
                return linearUnitsType.Nanometers;
            else if (fr == supportedLinearUnitsType.Microns)
                return linearUnitsType.Microns;
            else if (fr == supportedLinearUnitsType.Decimeters)
                return linearUnitsType.Decimeters;
            else if (fr == supportedLinearUnitsType.Decameters)
                return linearUnitsType.Decameters;
            else if (fr == supportedLinearUnitsType.Hectometers)
                return linearUnitsType.Hectometers;
            else if (fr == supportedLinearUnitsType.Gigameters)
                return linearUnitsType.Gigameters;
            else if (fr == supportedLinearUnitsType.Astronomical)
                return linearUnitsType.Astronomical;
            else if (fr == supportedLinearUnitsType.LightYears)
                return linearUnitsType.LightYears;

            return linearUnitsType.Meters;
        }
    }
}
