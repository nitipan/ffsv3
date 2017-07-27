// **  do not change the name - should be machine with API and UI should be the same name as this model
export class InputBase {

    analysisBy: string;
    analysisDate: Date;
    analysisDetail: string;
    equipmentNumber: string;
    equipmentType: string;
    methodology: string;
    yearOfFabrication: string;
    unitID: number;
    designCode: string;
    componentType: string;
    material: string;
    materialName: string;
    materialType: string;
    materialTypeID: number;
    componentTypeID: number;
    componentShapeID: number;
    materialID: number;
    designPressure: number;
    designTemperature: number;
    yieldStrength: number;
    ultimatedTensileStrength: number;
    allowableStress: number;
    asmeExemptionCurvesID: number;
    asmeExemptionCurvesName: string;
    automaticCalculationReferenceTemperature: boolean;
    referenceTemperature: number;
    insideDiameter: number;
    nominalThickness: number;
    fca: number;
    loss: number;
    weldJointEfficiency: number;
    autoCalculateMinRequireThickness: boolean;
    minRequireLongitutinalThickness: number;
    minRequireCircumferentialThickness: number;
    allowRSF: number;
    assessmentLevel: number;
    autoAllowableRSF: boolean;
    automaticallyCalculationAllowableStress: boolean;
    operatingPressure: number;
    externalPressure: boolean;
    operatingTemperature: number;
    automaticallyCalculationTheNominalStressOfTheComponent: boolean;
    theNominalStressOfTheComponent: number;
    automaticcallyPrimaryStress: boolean;
    primaryStress: number;
    supplementalLoad: boolean;
    supplementalStress: number;
    youngModulus: number;
    poissonRatio: number;
    equipmentImage: string;

}