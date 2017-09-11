using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api.Model
{

    public class BeanHydrogenItem : BeanCalculateCommon
    {
        public double? HICDepth
        {
            get;
            set;
        }

        public double? DirectionCircumferential
        {
            get;
            set;
        }

        public double? DirectionLongitudinal
        {
            get;
            set;
        }

        public double? InternalSurface
        {
            get;
            set;
        }

        public double? ExternalSurface
        {
            get;
            set;
        }

        public double? TotalOfBothSides
        {
            get;
            set;
        }

        public double? SpacingToNearestHIC
        {
            get;
            set;
        }

        public double? WeldJoint
        {
            get;
            set;
        }

        public double? MajorStructuralDiscontinuity
        {
            get;
            set;
        }
    }
}