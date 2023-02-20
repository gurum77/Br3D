using devDept.Eyeshot.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace hanee.Cad.Tool
{
    public class DimStyleProperties
    {
        public DimStyleProperties(List<Entity> dimEntities)
        {
            this.dimEntities = dimEntities;

            var linearDims = GetLinearDims();
            this.firstLinearDim = linearDims == null || linearDims.Count == 0 ? null : linearDims.First();

            var dimensions = GetDimensions();
            this.firstDimension = dimensions == null || dimensions.Count == 0 ? null : dimensions.First();

            var leaders = GetLeaders();
            this.firstLeader = leaders == null || leaders.Count == 0 ? null : leaders.First();

            this.firstEntity = dimEntities == null || dimEntities.Count == 0 ? null : dimEntities.First();
        }

        [Browsable(false)]
        LinearDim firstLinearDim;

        [Browsable(false)]
        Dimension firstDimension;

        [Browsable(false)]
        Leader firstLeader;

        [Browsable(false)]
        Entity firstEntity;

        [Browsable(false)]
        public List<Entity> dimEntities;

        [Browsable(false)]
        public List<LinearDim> GetLinearDims()
        {
            var ret = dimEntities.FindAll(ent => ent is LinearDim);
            if (ret == null)
                return null;
            return ret.ConvertAll<LinearDim>(ent => ent as LinearDim);
        }

        [Browsable(false)]
        public List<Dimension> GetDimensions()
        {
            var ret = dimEntities.FindAll(ent => ent is Dimension);
            if (ret == null)
                return null;
            return ret.ConvertAll<Dimension>(ent => ent as Dimension);
        }

        [Browsable(false)]
        public List<Leader> GetLeaders()
        {
            var ret = dimEntities.FindAll(ent => ent is Leader);
            if (ret == null)
                return null;
            return ret.ConvertAll<Leader>(ent => ent as Leader);
        }




        // 첫번째 화살표
        [Category("Arrow"), DisplayName("First")]
        public arrowheadType? leftArrowHead
        {
            get
            {
                if (firstLinearDim == null)
                    return null;

                return firstLinearDim.LeftArrowhead;
            }

            set
            {
                var linearDims = GetLinearDims();
                if (linearDims == null)
                    return;

                foreach (var ld in linearDims)
                    ld.LeftArrowhead = value.Value;
            }
        }

        // 두번째 화살표
        [Category("Arrow"), DisplayName("Second")]
        public arrowheadType? rightArrowHead
        {
            get => firstLinearDim?.RightArrowhead;

            set
            {
                var linearDims = GetLinearDims();
                if (linearDims == null)
                    return;

                foreach (var ld in linearDims)
                    ld.RightArrowhead = value.Value;
            }
        }

        // 지시선 화살표
        [Category("Arrow"), DisplayName("Leader")]
        public arrowheadType? leaderArrowHead
        {
            get => firstLeader?.Arrowhead;

            set
            {
                var leaders = GetLeaders();
                if (leaders == null)
                    return;

                foreach (var ld in leaders)
                {
                    ld.Arrowhead = value.Value;
                }
            }
        }


        // line color
        [Browsable(false)]
        [Category("Line"), DisplayName("Color")]
        public System.Drawing.Color? lineColor
        {
            get => firstEntity?.Color;

            set
            {
                if (dimEntities == null)
                    return;
                foreach (var ent in dimEntities)
                    ent.Color = value.Value;
            }
        }


        [Category("Arrow"), DisplayName("Head size")]
        public double? arrowheadSize
        {
            get
            {
                if (firstDimension != null)
                    return firstDimension.ArrowheadSize;
                if (firstLeader != null)
                    return firstLeader.ArrowheadSize;
                return null;
            }

            set
            {
                var dimensions = GetDimensions();
                if (dimensions != null)
                    dimensions.ForEach(dim => dim.ArrowheadSize = value.Value);

                var leaders = GetLeaders();
                if (leaders != null)
                    leaders.ForEach(ld => ld.ArrowheadSize = value.Value);
            }
        }

        // 화살표 
        [Category("Arrow"), DisplayName("Location")]
        public elementPositionType? arrowsLocation
        {
            get => firstDimension?.ArrowsLocation;

            set
            {
                if (value == null)
                    return;

                var dimensions = GetDimensions();
                if (dimensions != null)
                    dimensions.ForEach(dim => dim.ArrowsLocation = value.Value);
            }
        }

        // text override
        [Category("Text"), DisplayName("Override")]
        public string textOverride
        {
            get => firstDimension?.TextOverride;

            set
            {
                if (value == null)
                    return;

                var dimensions = GetDimensions();
                if (dimensions != null)
                    dimensions.ForEach(dim => dim.TextOverride = value);
            }
        }

        // text color
        [Browsable(false)]
        [Category("Text"), DisplayName("Color")]
        public System.Drawing.Color? textColor
        {
            get => firstDimension?.TextColor;
            set
            {
                if (value == null)
                    return;

                var dimensions = GetDimensions();
                if (dimensions != null)
                    dimensions.ForEach(dim => dim.TextColor = value.Value);
            }
        }

        // elementpositiontype
        [Category("Text"), DisplayName("Location")]
        public elementPositionType? textLocation
        {
            get => firstDimension?.TextLocation;

            set
            {
                if (value == null)
                    return;

                var dimensions = GetDimensions();
                if (dimensions != null)
                    dimensions.ForEach(dim => dim.TextLocation = value.Value);
            }
        }


        [Category("Text"), DisplayName("Prefix")]
        public string textPrefix
        {
            get
            {
                if (firstDimension == null)
                    return null;

                return firstDimension.TextPrefix;

            }

            set
            {
                if (value == null)
                    return;
                var dimensions = GetDimensions();
                if (dimensions != null)
                    dimensions.ForEach(dim => dim.TextPrefix = value);
            }
        }

        [Category("Text"), DisplayName("Suffix")]
        public string textSuffix
        {
            get
            {
                if (firstDimension == null)
                    return null;

                return firstDimension.TextSuffix;

            }

            set
            {
                if (value == null)
                    return;

                var dimensions = GetDimensions();
                if (dimensions != null)
                    dimensions.ForEach(dim => dim.TextSuffix = value);
            }
        }



        [Category("Tolerance"), DisplayName("Mode")]
        public toleranceType? toleranceType
        {
            get
            {
                if (firstDimension == null)
                    return null;

                return firstDimension.ToleranceMode;
            }

            set
            {
                if (value == null)
                    return;

                var dimensions = GetDimensions();
                if (dimensions != null)
                    dimensions.ForEach(dim => dim.ToleranceMode = value.Value);
            }
        }

        [Category("Tolerance"), DisplayName("Upper value")]
        public double? upperValue
        {
            get
            {
                if (firstDimension == null)
                    return null;

                return firstDimension.UpperValue;
            }

            set
            {
                if (value == null)
                    return;

                var dimensions = GetDimensions();
                if (dimensions != null)
                    dimensions.ForEach(dim => dim.UpperValue = value.Value);
            }
        }

        [Category("Tolerance"), DisplayName("Lower value")]
        public double? lowerValue
        {
            get
            {
                if (firstDimension == null)
                    return null;

                return firstDimension.LowerValue;
            }

            set
            {
                if (value == null)
                    return;

                var dimensions = GetDimensions();
                if (dimensions != null)
                    dimensions.ForEach(dim => dim.LowerValue = value.Value);
            }
        }

        public enum TolerancePrecision
        {
            [Description("0")]
            tolerance0,
            [Description("0.0")]
            tolerance1,
            [Description("0.00")]
            tolerance2
        }
        [Category("Tolerance"), DisplayName("Precision")]
        public TolerancePrecision? tolerancePrecision
        {
            get
            {
                if (firstDimension == null)
                    return null;

                return (TolerancePrecision)(firstDimension.TolerancePrecision);
            }

            set
            {
                if (value == null)
                    return;

                var dimensions = GetDimensions();
                if (dimensions != null)
                    dimensions.ForEach(dim => dim.TolerancePrecision = (int)value);
            }
        }


    }
}
