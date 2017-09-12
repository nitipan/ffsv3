using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api.Model
{
    public class BeanCreep : BeanCalculateCommon
    {
        public int? CreepID
        {
            get;
            set;
        }

        public int? AssessmentID
        {
            get;
            set;
        }

        public int? AssessmentMaterialID
        {
            get;
            set;
        }

        public string AssessmentMaterial
        {
            get;
            set;
        }

        public bool? AutomaticallyCalculationTheMaximumPermissibleTime
        {
            get;
            set;
        }

        public double? TheMaximumPermissibleTime
        {
            get;
            set;
        }

        public bool? AutomaticallyCalculationTheCreepDamageRate
        {
            get;
            set;
        }

        public double? TheCreepDamageRate
        {
            get;
            set;
        }

        public bool? TheComponentContainsAWeldJoint
        {
            get;
            set;
        }

        public bool? TheWeldJointIsPWHT
        {
            get;
            set;
        }

        public double? ExcursionDuration
        {
            get;
            set;
        }
    }
}

