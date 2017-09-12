using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api.Model
{

    public class BeanLaminationItem : BeanCalculateCommon
    {
        public double? LaminationHeight
        {
            get;
            set;
        }

        public double? FlawDimensionCircumferentialDirection
        {
            get;
            set;
        }

        public double? FlawDimensionLongituidinalDirection
        {
            get;
            set;
        }

        public double? MinimumMeasuredThickness
        {
            get;
            set;
        }

        public double? EdgeToEdgeSpacingToNearestLamination
        {
            get;
            set;
        }

        public double? SpacingToNearestWeldJoint
        {
            get;
            set;
        }

        public double? SpacingToNearestMajorStructuralDiscontinuity
        {
            get;
            set;
        }
    }
}