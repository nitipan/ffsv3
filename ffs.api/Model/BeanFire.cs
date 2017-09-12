using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api.Model
{

    public class BeanFire : BeanCalculateCommon
    {
        public int? MaterialGroupID
        {
            get;
            set;
        }

        public string MaterialGroupName
        {
            get;
            set;
        }

        public int? HeatExposureZoneID
        {
            get;
            set;
        }

        public string HeatExposureZoneName
        {
            get;
            set;
        }

        public bool Leak
        {
            get;
            set;
        }

        public bool Coat
        {
            get;
            set;
        }

        public bool Damage
        {
            get;
            set;
        }

        public double? VickersHardnessNo
        {
            get;
            set;
        }

        public double? AllowableStressFlaw
        {
            get;
            set;
        }
    }
}
