using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api.Model
{

    public class BeanDent : BeanCalculateCommon
    {
        public double? DentDepth
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

        public double? NumberOfCycle
        {
            get;
            set;
        }

        public double? MaxOperatingPressure
        {
            get;
            set;
        }

        public double? MinOperatingPressure
        {
            get;
            set;
        }
    }
}

