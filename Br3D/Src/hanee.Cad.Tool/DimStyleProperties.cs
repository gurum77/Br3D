using devDept.Eyeshot.Entities;
using System.Collections.Generic;

namespace hanee.Cad.Tool
{
    public class DimStyleProperties
    {
        List<Dimension> dimensions { get; set; }
        public DimStyleProperties(List<Dimension> dimensions)
        {
            this.dimensions = dimensions;
        }


        public double? arrowheadSize
        {
            get
            {
                if (dimensions == null || dimensions.Count == 0)
                    return null;
                return dimensions[0].ArrowheadSize;
            }

            set
            {
                if (value == null || dimensions == null)
                    return;
                dimensions.ForEach(dim => dim.ArrowheadSize = value.Value);
            }
        }

        // 화살표 
        public elementPositionType? arrowsLocation
        {
            get
            {
                if (dimensions == null || dimensions.Count == 0)
                    return null;
                return dimensions[0].ArrowsLocation;
            }

            set
            {
                if (value == null || dimensions == null)
                    return;
                dimensions.ForEach(dim => dim.ArrowsLocation= value.Value);
            }
        }

        // text override
        public string textOverride
        {
            get
            {
                if (dimensions == null || dimensions.Count == 0)
                    return null;
                return dimensions[0].TextOverride;

            }

            set
            {
                if (value == null || dimensions == null)
                    return;
                dimensions.ForEach(dim => dim.TextOverride = value);
            }
        }

        // text color
        public System.Drawing.Color? textColor
        {
            get
            {
                if (dimensions == null || dimensions.Count == 0)
                    return null;
                return dimensions[0].TextColor;

            }

            set
            {
                if (value == null || dimensions == null)
                    return;
                dimensions.ForEach(dim => dim.TextColor = value.Value);
            }
        }

        // text color method
        public colorMethodType? textColorMethod
        {
            get
            {
                if (dimensions == null || dimensions.Count == 0)
                    return null;
                return dimensions[0].TextColorMethod;

            }

            set
            {
                if (value == null || dimensions == null)
                    return;
                dimensions.ForEach(dim => dim.TextColorMethod = value.Value);
            }
        }



        // elementpositiontype
        public elementPositionType? textLocation
        {
            get
            {
                if (dimensions == null || dimensions.Count == 0)
                    return null;
                return dimensions[0].TextLocation;

            }

            set
            {
                if (value == null || dimensions == null)
                    return;
                dimensions.ForEach(dim => dim.TextLocation = value.Value);
            }
        }


        public string textPrefix
        {
            get
            {
                if (dimensions == null || dimensions.Count == 0)
                    return null;
                return dimensions[0].TextPrefix;

            }

            set
            {
                if (value == null || dimensions == null)
                    return;
                dimensions.ForEach(dim => dim.TextPrefix = value);
            }
        }

        public string textSuffix
        {
            get
            {
                if (dimensions == null || dimensions.Count == 0)
                    return null;
                return dimensions[0].TextSuffix;

            }

            set
            {
                if (value == null || dimensions == null) 
                    return;
                dimensions.ForEach(dim => dim.TextSuffix = value);
            }
        }

    }
}
