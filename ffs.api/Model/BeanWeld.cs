using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api.Model
{

    public class BeanWeld : BeanCalculateCommon
    {
        public int? FabricationTolerance
        {
            get;
            set;
        }

        public double? CenterlineOffset
        {
            get;
            set;
        }

        public int? WeldOrientarion
        {
            get;
            set;
        }

        public double? RComponent1
        {
            get;
            set;
        }

        public double? RComponent2
        {
            get;
            set;
        }

        public double? TComponent1
        {
            get;
            set;
        }

        public double? TComponent2
        {
            get;
            set;
        }

        public double? MaxInternalDiameter
        {
            get;
            set;
        }

        public double? MinInternalDiameter
        {
            get;
            set;
        }

        public double? AngleToDefineToStress
        {
            get;
            set;
        }
    }

}
