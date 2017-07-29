using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api.Model
{
    public class BeanBrittle : BeanCalculateCommon
    {
        public double? TheCriticalExposureTemperature
        {
            get;
            set;
        }

        public double? TheMinimumAllowableTemperature
        {
            get;
            set;
        }

        public bool? AutomaticcallyTheMinimumAllowableTemperature
        {
            get;
            set;
        }

        public double? TheUncorrodedGoverningThickness
        {
            get;
            set;
        }

        public bool? Fabricated
        {
            get;
            set;
        }

        public bool? WallThickness38
        {
            get;
            set;
        }

        public bool? PWHT
        {
            get;
            set;
        }

        public int? ReductionInTheMATID
        {
            get;
            set;
        }

        public string ReductionInTheMATName
        {
            get;
            set;
        }
    }
}