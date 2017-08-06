using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api.Model
{

    public class BeanPitting : BeanCalculateCommon
    {
        public double[,] inspectionGridData
        {
            get;
            set;
        }

        public double? theMaximumPitDepth
        {
            get;
            set;
        }

        public double? theStandardPitChart
        {
            get;
            set;
        }

        public string theStandardPitChartName
        {
            get;
            set;
        }
    }

}
