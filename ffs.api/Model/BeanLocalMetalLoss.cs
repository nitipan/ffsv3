using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api.Model
{

    public class BeanLocalMetalLoss : BeanCalculateCommon
    {
        public double? minMeasurementThickness
        {
            get;
            set;
        }

        public double? lmsd
        {
            get;
            set;
        }

        public double? widthOfTheLongGrid
        {
            get;
            set;
        }

        public double? widthOfTheCirGrid
        {
            get;
            set;
        }

        public double[,] inspectionGridData
        {
            get;
            set;
        }

        public int? numberOfInspectionColumn
        {
            get;
            set;
        }

        public int? numberOfInspectionRow
        {
            get;
            set;
        }

        public int? thicknessDataID
        {
            get;
            set;
        }
    }

}
