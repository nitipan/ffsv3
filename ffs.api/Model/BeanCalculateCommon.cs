using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api.Model
{
    public class BeanCalculateCommon
    {
        public string analysisBy
        {
            get;
            set;
        }

        public DateTime? analysisDate
        {
            get;
            set;
        }

        public string analysisDetail
        {
            get;
            set;
        }

        public string equipmentNumber
        {
            get;
            set;
        }

        public string equipmentType
        {
            get;
            set;
        }

        public string methodology
        {
            get;
            set;
        }

        public string yearOfFabrication
        {
            get;
            set;
        }

        public string unit
        {
            get;
            set;
        }

        public int? unitID
        {
            get;
            set;
        }

        public string designCode
        {
            get;
            set;
        }

        public string componentType
        {
            get;
            set;
        }

        public string material
        {
            get;
            set;
        }

        public string materialName
        {
            get;
            set;
        }

        public string materialType
        {
            get;
            set;
        }

        public int? materialTypeID
        {
            get;
            set;
        }

        public int? componentTypeID
        {
            get;
            set;
        }

        public int? componentShapeID
        {
            get;
            set;
        }

        public int? materialID
        {
            get;
            set;
        }

        public double? designPressure
        {
            get;
            set;
        }

        public double? designTemperature
        {
            get;
            set;
        }

        public double? yieldStrength
        {
            get;
            set;
        }

        public double? ultimatedTensileStrength
        {
            get;
            set;
        }

        public double? allowableStress
        {
            get;
            set;
        }

        public int? asmeExemptionCurvesID
        {
            get;
            set;
        }

        public string asmeExemptionCurvesName
        {
            get;
            set;
        }

        public bool? automaticCalculationReferenceTemperature
        {
            get;
            set;
        }

        public double? referenceTemperature
        {
            get;
            set;
        }

        public double? insideDiameter
        {
            get;
            set;
        }

        public double? nominalThickness
        {
            get;
            set;
        }

        public double? fca
        {
            get;
            set;
        }

        public double? loss
        {
            get;
            set;
        }

        public double? weldJointEfficiency
        {
            get;
            set;
        }

        public bool? autoCalculateMinRequireThickness
        {
            get;
            set;
        }

        public double? minRequireLongitutinalThickness
        {
            get;
            set;
        }

        public double? minRequireCircumferentialThickness
        {
            get;
            set;
        }

        public double? allowRSF
        {
            get;
            set;
        }

        public int assessmentLevel
        {
            get;
            set;
        }

        public bool? autoAllowableRSF
        {
            get;
            set;
        }

        public bool? automaticallyCalculationAllowableStress
        {
            get;
            set;
        }

        public double? operatingPressure
        {
            get;
            set;
        }

        public bool? externalPressure
        {
            get;
            set;
        }

        public double? operatingTemperature
        {
            get;
            set;
        }

        public bool? automaticallyCalculationTheNominalStressOfTheComponent
        {
            get;
            set;
        }

        public double? theNominalStressOfTheComponent
        {
            get;
            set;
        }

        public bool? automaticcallyPrimaryStress
        {
            get;
            set;
        }

        public double? primaryStress
        {
            get;
            set;
        }

        public bool? supplementalLoad
        {
            get;
            set;
        }

        public double? supplementalStress
        {
            get;
            set;
        }

        public double? youngModulus
        {
            get;
            set;
        }

        public double? poissonRatio
        {
            get;
            set;
        }

        public byte[] equipmentImage
        {
            get;
            set;
        }
    }

}
