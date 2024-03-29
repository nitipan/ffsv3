-- Script Date: 7/22/2017 10:05 PM  - ErikEJ.SqlCeScripting version 3.5.2.69
-- Database information:
-- Locale Identifier: 1033
-- Encryption Mode: Platform Default
-- Case Sensitive: False
-- Database: C:\Users\nitipanp\AppData\Roaming\repdata\db\db.sdf
-- ServerVersion: 4.0.8876.1
-- DatabaseSize: 448 KB
-- SpaceAvailable: 3.999 GB
-- Created: 5/4/2016 1:32 PM

-- User Table information:
-- Number of tables: 37
-- ASMEExemptionCurves: -1 row(s)
-- Assessment: 2 row(s)
-- AssessmentLevel: -1 row(s)
-- AssessmentMaterial: 23 row(s)
-- Brittle: 1 row(s)
-- ComponentShape: 2 row(s)
-- ComponentType: -1 row(s)
-- Crack: 0 row(s)
-- CrackLocation: 3 row(s)
-- CrackType: 7 row(s)
-- Creep: 0 row(s)
-- Dent: 0 row(s)
-- DesignCode: -1 row(s)
-- EquipmentType: -1 row(s)
-- FabricationTolerance: -1 row(s)
-- FailureMode: -1 row(s)
-- Fire: 0 row(s)
-- GeneralMetalLoss: 0 row(s)
-- HeatExposureZone: -1 row(s)
-- HICDamage: -1 row(s)
-- Hydrogen: 0 row(s)
-- InspectionGridData: 59 row(s)
-- Lamination: 0 row(s)
-- LaminationItem: 0 row(s)
-- LocalMetalLoss: 1 row(s)
-- Material: -1 row(s)
-- MaterialGroup: -1 row(s)
-- MaterialSpecification: -1 row(s)
-- MaterialType: 3 row(s)
-- Methodology: -1 row(s)
-- Pitting: 0 row(s)
-- ReductionInTheMAT: -1 row(s)
-- TheStandardPitChart: 8 row(s)
-- ThicknessData: -1 row(s)
-- Unit: -1 row(s)
-- Weld: 0 row(s)
-- WeldOrientarion: -1 row(s)

CREATE TABLE [WeldOrientarion] (
  [WeldOrientarionID] tinyint NULL
, [WeldOrientarionName] nvarchar(100) NULL
);
GO
CREATE TABLE [Weld] (
  [WeldID] int IDENTITY (1,1) NOT NULL
, [AssessmentID] int NULL
, [ResultLevel1] nvarchar(200) NULL
, [ResultLevel2] nvarchar(200) NULL
, [CreatedDate] datetime NULL
, [CreatedBy] nvarchar(100) NULL
, [UpdatedDate] datetime NULL
, [UpdatedBy] nvarchar(100) NULL
, [FabricationToleranceID] int NULL
, [CenterlineOffset] float NULL
, [WeldOrientarionID] int NULL
, [RComponent1] float NULL
, [RComponent2] float NULL
, [TComponent1] float NULL
, [TComponent2] float NULL
, [MaxInternalDiameter] float NULL
, [MinInternalDiameter] float NULL
, [AngleToDefineToStress] float NULL
, [AllowableRSF] float NULL
, [AutomaticallyAllowableRSF] bit NULL
);
GO
CREATE TABLE [Unit] (
  [UnitID] int NULL
, [UnitName] nvarchar(100) NULL
);
GO
CREATE TABLE [ThicknessData] (
  [ThicknessDataID] int NULL
, [ThicknessDataName] nvarchar(100) NULL
);
GO
CREATE TABLE [TheStandardPitChart] (
  [TheStandardPitChartID] int NOT NULL
, [TheStandardPitChartName] nvarchar(100) NULL
);
GO
CREATE TABLE [ReductionInTheMAT] (
  [ReductionInTheMATID] int NULL
, [ReductionInTheMATName] nvarchar(100) NULL
);
GO
CREATE TABLE [Pitting] (
  [PittingID] int IDENTITY (40000001,1) NOT NULL
, [CreatedDate] datetime NULL
, [CreatedBy] nvarchar(100) NULL
, [UpdatedDate] datetime NULL
, [UpdatedBy] nvarchar(100) NULL
, [ResultLevel1] nvarchar(300) NULL
, [ResultLevel2] nvarchar(300) NULL
, [AssessmentID ] int NULL
, [TheMaximumPitDepth ] float NULL
, [TheStandardPitChartID] int NULL
, [AllowableRSF] float NULL
, [AutomaticallyAllowableRSF] bit NULL
);
GO
CREATE TABLE [Methodology] (
  [MethodologyID] int NULL
, [MethodologyName] nvarchar(100) NULL
);
GO
CREATE TABLE [MaterialType] (
  [MaterialTypeID] int NOT NULL
, [MaterialTypeName] nvarchar(100) NULL
);
GO
CREATE TABLE [MaterialSpecification] (
  [MaterialSpecificationID] int NULL
, [MaterialSpecificationName] nvarchar(100) NULL
);
GO
CREATE TABLE [MaterialGroup] (
  [MaterialGroupID] tinyint NULL
, [MaterialGroupName] nvarchar(100) NULL
);
GO
CREATE TABLE [Material] (
  [MaterialID] int NULL
, [MaterialName] nvarchar(200) NULL
, [MaterialCode] nvarchar(100) NULL
, [YieldStrength] float NULL
, [TensileStrength] float NULL
, [AllowableStress] float NULL
, [CreatedDate] datetime NULL
, [CreatedBy] nvarchar(50) NULL
, [UpdatedDate] datetime NULL
, [UpdatedBy] nvarchar(50) NULL
, [Curve] nvarchar(1) NULL
, [YieldStrengthKSI] float NULL
, [TensileStrengthKSI] float NULL
, [AllowableStressKSI] float NULL
, [YoungModulas] float NULL
, [PossionRatio] float NULL
, [YoungModulasKSI] float NULL
);
GO
CREATE TABLE [LocalMetalLoss] (
  [LocalMetalLossID] int IDENTITY (20000003,1) NOT NULL
, [WidthOfTheLongGrid] float NULL
, [WidthOfTheCirGrid] float NULL
, [CreatedDate] datetime NULL
, [CreatedBy] nvarchar(100) NULL
, [UpdatedDate] datetime NULL
, [UpdatedBy] nvarchar(100) NULL
, [ResultLevel1] nvarchar(300) NULL
, [ResultLevel2] nvarchar(300) NULL
, [AssessmentID] int NULL
, [ThicknessDataTypeID] int NULL
, [DistanceNearest] float NULL
, [NumberOfInspectionColumn] int NULL
, [NumberOfInspectionRow] int NULL
, [AllowableRSF] float NULL
, [AutomaticallyAllowableRSF] nvarchar(100) NULL
);
GO
CREATE TABLE [LaminationItem] (
  [LaminationItemID] int IDENTITY (1,1) NOT NULL
, [LaminationID] int NULL
, [No] int NULL
, [LaminationHeight] float NULL
, [FlawDimensionCircumferentialDirection] float NULL
, [FlawDimensionLongituidinalDirection] float NULL
, [MinimumMeasuredThickness] float NULL
, [EdgeToEdgeSpacingToNearestLamination] float NULL
, [SpacingToNearestWeldJoint] float NULL
, [SpacingToNearestMajorStructuralDiscontinuity] float NULL
, [CreatedDate] datetime NULL
, [CreatedBy] nvarchar(100) NULL
, [UpdatedDate] datetime NULL
, [UpdatedBy] nvarchar(100) NULL
);
GO
CREATE TABLE [Lamination] (
  [LaminationID] int IDENTITY (1,1) NOT NULL
, [AssessmentID] int NULL
, [NumberOfFlow] int NULL
, [Damage] bit NULL
, [ResultLevel1] nvarchar(200) NULL
, [ResultLevel2] nvarchar(200) NULL
, [CreatedDate] datetime NULL
, [CreatedBy] nvarchar(100) NULL
, [UpdatedDate] datetime NULL
, [UpdatedBy] nvarchar(100) NULL
);
GO
CREATE TABLE [InspectionGridData] (
  [InspectionGridDataID] int IDENTITY (78,1) NOT NULL
, [ObjectID] bigint NULL
, [RowNo] int NULL
, [ColumnNo] int NULL
, [Data] float NULL
, [CreatedDate] datetime NULL
, [CreatedBy] nvarchar(100) NULL
, [UpdatedDate] datetime NULL
, [UpdatedBy] nvarchar(100) NULL
);
GO
CREATE TABLE [Hydrogen] (
  [HydrogenID] int IDENTITY (1,1) NOT NULL
, [CreatedDate] datetime NULL
, [CreatedBy] nvarchar(100) NULL
, [UpdatedDate] datetime NULL
, [UpdatedBy] nvarchar(100) NULL
, [AllowableRSF] float NULL
, [AutomaticallyAllowableRSF] bit NULL
, [AssessmentID] int NULL
, [NumberOfFlow] int NULL
, [ResultLevel1] nvarchar(200) NULL
, [ResultLevel2] nvarchar(200) NULL
);
GO
CREATE TABLE [HICDamage] (
  [HICDamageID] tinyint NULL
, [HICDamageName] nvarchar(100) NULL
);
GO
CREATE TABLE [HeatExposureZone] (
  [HeatExposureZoneID] tinyint NULL
, [HeatExposureZoneName] nvarchar(100) NULL
);
GO
CREATE TABLE [GeneralMetalLoss] (
  [GeneralMetalLossID] int IDENTITY (10000011,1) NOT NULL
, [CreatedDate] datetime NULL
, [CreatedBy] nvarchar(50) NULL
, [UpdatedDate] datetime NULL
, [UpdatedBy] nvarchar(50) NULL
, [WidthOfTheLongGrid] float NULL
, [WidthOfTheCirGrid] float NULL
, [NumOfGridColumn] int NULL
, [NumOfGridRow] int NULL
, [ResultLevel1] nvarchar(300) NULL
, [ResultLevel2] nvarchar(300) NULL
, [AssessmentID] int NULL
, [ThicknessDataTypeID] int NULL
, [MinimumMeasurementThickness] float NULL
, [NumberOfInspectionColumn] int NULL
, [NumberOfInspectionRow] int NULL
);
GO
CREATE TABLE [Fire] (
  [FireID] int IDENTITY (1,1) NOT NULL
, [AssessmentID] int NOT NULL
, [MaterialGroupID] int NULL
, [HeatExposureZoneID] int NULL
, [Leak] bit NULL
, [Coat] bit NULL
, [Damage] bit NULL
, [VickersHardnessNo] float NULL
, [AllowableStressFlaw] float NULL
, [ResultLevel1] nvarchar(100) NULL
, [ResultLevel2] nvarchar(100) NULL
, [CreatedDate] datetime NULL
, [CreatedBy] nvarchar(200) NULL
, [UpdatedDate] datetime NULL
, [UpdatedBy] nvarchar(200) NULL
);
GO
CREATE TABLE [FailureMode] (
  [FailureModeID] int NULL
, [FailureModeName] nvarchar(100) NULL
);
GO
CREATE TABLE [FabricationTolerance] (
  [FabricationToleranceID] tinyint NULL
, [FabricationToleranceName] nvarchar(100) NULL
, [EquipmentTypeID] int NULL
);
GO
CREATE TABLE [EquipmentType] (
  [EquipmentTypeID] tinyint NULL
, [EquipmentTypeName] nvarchar(100) NULL
);
GO
CREATE TABLE [DesignCode] (
  [DesignCodeID] int NULL
, [DesignCodeName] nvarchar(100) NULL
, [EquipmentTypeID] int NULL
);
GO
CREATE TABLE [Dent] (
  [DentID] int IDENTITY (30000001,1) NOT NULL
, [EquipmentID] int NULL
, [Dent] float NULL
, [Lmsd] float NULL
, [Lw] float NULL
, [MaxOperatingPressure] float NULL
, [MinOperatingPressure] float NULL
, [CreatedDate] datetime NULL
, [CreatedBy] nvarchar(100) NULL
, [UpdatedDate] datetime NULL
, [UpdatedBy] nvarchar(100) NULL
, [NumOfGridColumn] smallint NULL
, [NumOfGridRow] smallint NULL
, [ResultLevel1] bit NULL
, [ResultLevel2] bit NULL
, [AssessmentID] int NULL
, [DentDepth] float NULL
, [WeldJoint] float NULL
, [MajorStructuralDiscontinuity] float NULL
, [NumberOfCycle] int NULL
);
GO
CREATE TABLE [Creep] (
  [CreepID] int IDENTITY (50000001,1) NOT NULL
, [CreatedDate] datetime NULL
, [CreatedBy] nvarchar(100) NULL
, [UpdatedDate] datetime NULL
, [UpdatedBy] nvarchar(100) NULL
, [ResultLevel1] nvarchar(300) NULL
, [ResultLevel2] nvarchar(300) NULL
, [AssessmentMaterialID] int NULL
, [AutomaticallyCalculationTheMaximumPermissibleTime ] bit NULL
, [TheMaximumPermissibleTime ] float NULL
, [AutomaticallyCalculationTheCreepDamageRate ] bit NULL
, [TheCreepDamageRate ] float NULL
, [TheComponentContainsAWeldJoint ] bit NULL
, [TheWeldJointIsPWHT ] bit NULL
, [ExcursionDuration ] float NULL
, [AssessmentID] int NULL
);
GO
CREATE TABLE [CrackType] (
  [CrackTypeID] int NOT NULL
, [CrackTypeName] nvarchar(100) NULL
);
GO
CREATE TABLE [CrackLocation] (
  [CrackLocationID] int NOT NULL
, [CrackLocationName] nvarchar(100) NULL
);
GO
CREATE TABLE [Crack] (
  [CrackID] int IDENTITY (60000001,1) NOT NULL
, [CreatedDate] datetime NULL
, [CreatedBy] nvarchar(100) NULL
, [UpdatedDate] datetime NULL
, [UpdatedBy] nvarchar(100) NULL
, [ResultLevel1] nvarchar(300) NULL
, [ResultLevel2] nvarchar(300) NULL
, [CrackTypeID] int NULL
, [CrackLocationID] int NULL
, [CrackLength] float NULL
, [CrackDepth] nvarchar(100) NULL
, [AssessmentID] int NULL
);
GO
CREATE TABLE [ComponentType] (
  [ComponentTypeID] int NULL
, [ComponentTypeName] nvarchar(100) NULL
, [EquipmentTypeID] int NULL
);
GO
CREATE TABLE [ComponentShape] (
  [ComponentShapeID] int NOT NULL
, [ComponentShapeName] nvarchar(100) NULL
);
GO
CREATE TABLE [Brittle] (
  [BrittleID] int IDENTITY (2,1) NOT NULL
, [AssessmentID] int NULL
, [TheCriticalExposureTemperature] float NULL
, [TheMinimumAllowableTemperature] float NULL
, [AutomaticcallyTheMinimumAllowableTemperature] bit NULL
, [TheUncorrodedGoverningThickness] float NULL
, [Fabricated] bit NULL
, [WallThickness38] bit NULL
, [PWHT] bit NULL
, [ResultLevel1] nvarchar(300) NULL
, [ResultLevel2] nvarchar(300) NULL
, [CreatedDate] datetime NULL
, [CreatedBy] nvarchar(100) NULL
, [UpdatedDate] datetime NULL
, [UpdatedBy] nvarchar(100) NULL
, [ReductionInTheMATID] int NULL
);
GO
CREATE TABLE [AssessmentMaterial] (
  [AssessmentMaterialID] int NOT NULL
, [AssessmentMaterialName] nvarchar(100) NULL
, [MaterialID] int NULL
);
GO
CREATE TABLE [AssessmentLevel] (
  [AssessmentLevelID] smallint NULL
, [AssessmentLevelName] nvarchar(100) NULL
);
GO
CREATE TABLE [Assessment] (
  [AssessmentID] int IDENTITY (5,1) NOT NULL
, [EquipmentTypeID] int NULL
, [MethodologyID] int NULL
, [UnitID] int NULL
, [AssessmentLevelID] int NULL
, [AnalysisBy] nvarchar(100) NULL
, [AnalysisDate] datetime NULL
, [AnalysisDetail] ntext NULL
, [ComponentTypeID] int NULL
, [DesignCodeID] int NULL
, [PressureDesign] float NULL
, [TemperatureDesign] float NULL
, [InsideDiameter] float NULL
, [NominalThickness] float NULL
, [AutomaticallyMinRequireThickness] bit NULL
, [MinRequireLongitutinalThickness] float NULL
, [MinRequireCircumferentialThickness] float NULL
, [FutureCorrosionAllowance] float NULL
, [UniformMetalLoss] float NULL
, [WeldEfficiency] float NULL
, [AllowableRSF] float NULL
, [MaterialID] int NULL
, [AllowableStress] float NULL
, [AutomaticallyCalculationAllowableStress] bit NULL
, [UltimatedTensileStrength] float NULL
, [FailureModeID] int NULL
, [OperatingPressure] float NULL
, [OperatingTemperature] float NULL
, [CreatedDate] datetime NULL
, [CreatedBy] nvarchar(100) NULL
, [UpdatedDate] datetime NULL
, [UpdatedBy] nvarchar(100) NULL
, [AutomaticallyAllowableRSF] bit NULL
, [YieldStrength] float NULL
, [EquipmentNumber] nvarchar(250) NULL
, [EquipmentImage] image NULL
, [ExternalPressure] bit NULL
, [AssessmentResult] bit NULL
, [ComponentShapeID] int NULL
, [YearOfFabrication] nvarchar(4) NULL
, [ASMEExemptionCurvesID] int NULL
, [MaterialTypeID] int NULL
, [ReferenceTemperature] float NULL
, [AutomaticallyCalculationTheNominalStressOfTheComponent] bit NULL
, [TheNominalStressOfTheComponent] float NULL
, [AutomaticCalculationReferenceTemperature] bit NULL
, [MaterialName] nvarchar(100) NULL
);
GO
CREATE TABLE [ASMEExemptionCurves] (
  [ASMEExemptionCurvesID] int NULL
, [ASMEExemptionCurvesName] nvarchar(100) NULL
);
GO
INSERT INTO [WeldOrientarion] ([WeldOrientarionID],[WeldOrientarionName]) VALUES (
1,N'Longitudinal Joints');
GO
INSERT INTO [WeldOrientarion] ([WeldOrientarionID],[WeldOrientarionName]) VALUES (
2,N'Circumferential Joints');
GO
SET IDENTITY_INSERT [Weld] OFF;
GO
INSERT INTO [Unit] ([UnitID],[UnitName]) VALUES (
1,N'mm-MPa-°C');
GO
INSERT INTO [Unit] ([UnitID],[UnitName]) VALUES (
2,N'in-psi-°F');
GO
INSERT INTO [ThicknessData] ([ThicknessDataID],[ThicknessDataName]) VALUES (
1,N'Point Thickness  Reading');
GO
INSERT INTO [ThicknessData] ([ThicknessDataID],[ThicknessDataName]) VALUES (
2,N'Thickness Profile Reading');
GO
INSERT INTO [TheStandardPitChart] ([TheStandardPitChartID],[TheStandardPitChartName]) VALUES (
1,N'Grade 1 Pitting');
GO
INSERT INTO [TheStandardPitChart] ([TheStandardPitChartID],[TheStandardPitChartName]) VALUES (
2,N'Grade 2 Pitting');
GO
INSERT INTO [TheStandardPitChart] ([TheStandardPitChartID],[TheStandardPitChartName]) VALUES (
3,N'Grade 3 Pitting');
GO
INSERT INTO [TheStandardPitChart] ([TheStandardPitChartID],[TheStandardPitChartName]) VALUES (
4,N'Grade 4 Pitting');
GO
INSERT INTO [TheStandardPitChart] ([TheStandardPitChartID],[TheStandardPitChartName]) VALUES (
5,N'Grade 5 Pitting');
GO
INSERT INTO [TheStandardPitChart] ([TheStandardPitChartID],[TheStandardPitChartName]) VALUES (
6,N'Grade 6 Pitting');
GO
INSERT INTO [TheStandardPitChart] ([TheStandardPitChartID],[TheStandardPitChartName]) VALUES (
7,N'Grade 7 Pitting');
GO
INSERT INTO [TheStandardPitChart] ([TheStandardPitChartID],[TheStandardPitChartName]) VALUES (
8,N'Grade 8 Pitting');
GO
INSERT INTO [ReductionInTheMAT] ([ReductionInTheMATID],[ReductionInTheMATName]) VALUES (
1,N'Pressure-Temperature rating basis');
GO
INSERT INTO [ReductionInTheMAT] ([ReductionInTheMATID],[ReductionInTheMATName]) VALUES (
2,N'Stress Basis');
GO
INSERT INTO [ReductionInTheMAT] ([ReductionInTheMATID],[ReductionInTheMATName]) VALUES (
3,N'Thickness Basis');
GO
SET IDENTITY_INSERT [Pitting] OFF;
GO
INSERT INTO [Methodology] ([MethodologyID],[MethodologyName]) VALUES (
1,N'API 579-1/ASME FFS-1 2007 Fitness-For-Service');
GO
INSERT INTO [Methodology] ([MethodologyID],[MethodologyName]) VALUES (
2,N'Other');
GO
INSERT INTO [MaterialType] ([MaterialTypeID],[MaterialTypeName]) VALUES (
1,N'Carbon Steels');
GO
INSERT INTO [MaterialType] ([MaterialTypeID],[MaterialTypeName]) VALUES (
2,N'Low Alloy Steels');
GO
INSERT INTO [MaterialType] ([MaterialTypeID],[MaterialTypeName]) VALUES (
3,N'Other');
GO
INSERT INTO [MaterialSpecification] ([MaterialSpecificationID],[MaterialSpecificationName]) VALUES (
1,N'SS 304');
GO
INSERT INTO [MaterialSpecification] ([MaterialSpecificationID],[MaterialSpecificationName]) VALUES (
2,N'CS 319');
GO
INSERT INTO [MaterialSpecification] ([MaterialSpecificationID],[MaterialSpecificationName]) VALUES (
3,N'Other');
GO
INSERT INTO [MaterialGroup] ([MaterialGroupID],[MaterialGroupName]) VALUES (
1,N'Carbon Steels');
GO
INSERT INTO [MaterialGroup] ([MaterialGroupID],[MaterialGroupName]) VALUES (
2,N'Low Alloy Steels');
GO
INSERT INTO [MaterialGroup] ([MaterialGroupID],[MaterialGroupName]) VALUES (
3,N'Austenitic Stainless Steels');
GO
INSERT INTO [MaterialGroup] ([MaterialGroupID],[MaterialGroupName]) VALUES (
4,N'Duplex Stainless Steels');
GO
INSERT INTO [MaterialGroup] ([MaterialGroupID],[MaterialGroupName]) VALUES (
5,N'Precipitation Hardened Alloy Steels');
GO
INSERT INTO [MaterialGroup] ([MaterialGroupID],[MaterialGroupName]) VALUES (
6,N'Alloy C-276');
GO
INSERT INTO [MaterialGroup] ([MaterialGroupID],[MaterialGroupName]) VALUES (
7,N'Alloy 20');
GO
INSERT INTO [MaterialGroup] ([MaterialGroupID],[MaterialGroupName]) VALUES (
8,N'Alloy 400');
GO
INSERT INTO [MaterialGroup] ([MaterialGroupID],[MaterialGroupName]) VALUES (
9,N'Alloy 600');
GO
INSERT INTO [MaterialGroup] ([MaterialGroupID],[MaterialGroupName]) VALUES (
10,N'Alloy 625');
GO
INSERT INTO [MaterialGroup] ([MaterialGroupID],[MaterialGroupName]) VALUES (
11,N'Alloy 800');
GO
INSERT INTO [MaterialGroup] ([MaterialGroupID],[MaterialGroupName]) VALUES (
12,N'Alloy 800H');
GO
INSERT INTO [MaterialGroup] ([MaterialGroupID],[MaterialGroupName]) VALUES (
13,N'Alloy 825');
GO
INSERT INTO [MaterialGroup] ([MaterialGroupID],[MaterialGroupName]) VALUES (
14,N'Alloy 2205');
GO
INSERT INTO [MaterialGroup] ([MaterialGroupID],[MaterialGroupName]) VALUES (
15,N'Alloy 2207');
GO
INSERT INTO [MaterialGroup] ([MaterialGroupID],[MaterialGroupName]) VALUES (
16,N'Copper Alloys');
GO
INSERT INTO [MaterialGroup] ([MaterialGroupID],[MaterialGroupName]) VALUES (
17,N'Aluminum Alloys');
GO
INSERT INTO [Material] ([MaterialID],[MaterialName],[MaterialCode],[YieldStrength],[TensileStrength],[AllowableStress],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[Curve],[YieldStrengthKSI],[TensileStrengthKSI],[AllowableStressKSI],[YoungModulas],[PossionRatio],[YoungModulasKSI]) VALUES (
1,N'A216 WCA',NULL,205,415,138,NULL,NULL,NULL,NULL,N'2',29733,60191,20015,202000,0.3,29297623);
GO
INSERT INTO [Material] ([MaterialID],[MaterialName],[MaterialCode],[YieldStrength],[TensileStrength],[AllowableStress],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[Curve],[YieldStrengthKSI],[TensileStrengthKSI],[AllowableStressKSI],[YoungModulas],[PossionRatio],[YoungModulasKSI]) VALUES (
2,N'A216 WCB',NULL,250,485,165,NULL,NULL,NULL,NULL,N'1',36259,70343,23931,201000,0.3,29152585);
GO
INSERT INTO [Material] ([MaterialID],[MaterialName],[MaterialCode],[YieldStrength],[TensileStrength],[AllowableStress],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[Curve],[YieldStrengthKSI],[TensileStrengthKSI],[AllowableStressKSI],[YoungModulas],[PossionRatio],[YoungModulasKSI]) VALUES (
3,N'A217 WC1',NULL,240,450,108,NULL,NULL,NULL,NULL,N'2',34809,65267,15664,200000,0.3,29007548);
GO
INSERT INTO [Material] ([MaterialID],[MaterialName],[MaterialCode],[YieldStrength],[TensileStrength],[AllowableStress],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[Curve],[YieldStrengthKSI],[TensileStrengthKSI],[AllowableStressKSI],[YoungModulas],[PossionRatio],[YoungModulasKSI]) VALUES (
4,N'A217 WC6',NULL,275,485,184,NULL,NULL,NULL,NULL,N'2',39885,70343,26687,204000,0.3,29587699);
GO
INSERT INTO [Material] ([MaterialID],[MaterialName],[MaterialCode],[YieldStrength],[TensileStrength],[AllowableStress],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[Curve],[YieldStrengthKSI],[TensileStrengthKSI],[AllowableStressKSI],[YoungModulas],[PossionRatio],[YoungModulasKSI]) VALUES (
5,N'SA387 Gr22',NULL,210,415,138,NULL,NULL,NULL,NULL,N'3',30458,60191,20015,210000,0.3,30457925);
GO
INSERT INTO [Material] ([MaterialID],[MaterialName],[MaterialCode],[YieldStrength],[TensileStrength],[AllowableStress],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[Curve],[YieldStrengthKSI],[TensileStrengthKSI],[AllowableStressKSI],[YoungModulas],[PossionRatio],[YoungModulasKSI]) VALUES (
6,N'SA832 Gr22V',NULL,415,585,244,NULL,NULL,NULL,NULL,N'2',60191,84847,35389,210000,0.3,30457925);
GO
INSERT INTO [Material] ([MaterialID],[MaterialName],[MaterialCode],[YieldStrength],[TensileStrength],[AllowableStress],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[Curve],[YieldStrengthKSI],[TensileStrengthKSI],[AllowableStressKSI],[YoungModulas],[PossionRatio],[YoungModulasKSI]) VALUES (
7,N'SA387 Gr5',NULL,415,210,138,NULL,NULL,NULL,NULL,N'3',60191,30458,20015,213000,0.3,30893038);
GO
INSERT INTO [Material] ([MaterialID],[MaterialName],[MaterialCode],[YieldStrength],[TensileStrength],[AllowableStress],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[Curve],[YieldStrengthKSI],[TensileStrengthKSI],[AllowableStressKSI],[YoungModulas],[PossionRatio],[YoungModulasKSI]) VALUES (
8,N'SA335 P9',NULL,415,205,138,NULL,NULL,NULL,NULL,N'3',60191,29733,20015,213000,0.3,30893038);
GO
INSERT INTO [Material] ([MaterialID],[MaterialName],[MaterialCode],[YieldStrength],[TensileStrength],[AllowableStress],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[Curve],[YieldStrengthKSI],[TensileStrengthKSI],[AllowableStressKSI],[YoungModulas],[PossionRatio],[YoungModulasKSI]) VALUES (
9,N'SA387 Gr91',NULL,585,415,244,NULL,NULL,NULL,NULL,N'3',84847,60191,35389,213000,0.3,30893038);
GO
INSERT INTO [Material] ([MaterialID],[MaterialName],[MaterialCode],[YieldStrength],[TensileStrength],[AllowableStress],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[Curve],[YieldStrengthKSI],[TensileStrengthKSI],[AllowableStressKSI],[YoungModulas],[PossionRatio],[YoungModulasKSI]) VALUES (
10,N'SA240 Gr405',NULL,170,415,115,NULL,NULL,NULL,NULL,NULL,24656,60191,16679,201000,0.3,29152585);
GO
INSERT INTO [Material] ([MaterialID],[MaterialName],[MaterialCode],[YieldStrength],[TensileStrength],[AllowableStress],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[Curve],[YieldStrengthKSI],[TensileStrengthKSI],[AllowableStressKSI],[YoungModulas],[PossionRatio],[YoungModulasKSI]) VALUES (
11,N'AISI Type 304 & 304H',NULL,205,515,138,NULL,NULL,NULL,NULL,NULL,29733,74694,20015,195000,0.31,28282359);
GO
INSERT INTO [Material] ([MaterialID],[MaterialName],[MaterialCode],[YieldStrength],[TensileStrength],[AllowableStress],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[Curve],[YieldStrengthKSI],[TensileStrengthKSI],[AllowableStressKSI],[YoungModulas],[PossionRatio],[YoungModulasKSI]) VALUES (
12,N'AISI Type 316 & 316H',NULL,205,515,138,NULL,NULL,NULL,NULL,NULL,29733,74694,20015,195000,0.31,28282359);
GO
INSERT INTO [Material] ([MaterialID],[MaterialName],[MaterialCode],[YieldStrength],[TensileStrength],[AllowableStress],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[Curve],[YieldStrengthKSI],[TensileStrengthKSI],[AllowableStressKSI],[YoungModulas],[PossionRatio],[YoungModulasKSI]) VALUES (
13,N'AISI Type 321',NULL,205,515,138,NULL,NULL,NULL,NULL,NULL,29733,74694,20015,195000,0.31,28282359);
GO
INSERT INTO [Material] ([MaterialID],[MaterialName],[MaterialCode],[YieldStrength],[TensileStrength],[AllowableStress],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[Curve],[YieldStrengthKSI],[TensileStrengthKSI],[AllowableStressKSI],[YoungModulas],[PossionRatio],[YoungModulasKSI]) VALUES (
14,N'AISI Type 321H',NULL,205,515,138,NULL,NULL,NULL,NULL,NULL,29733,74694,20015,195000,0.31,28282359);
GO
INSERT INTO [Material] ([MaterialID],[MaterialName],[MaterialCode],[YieldStrength],[TensileStrength],[AllowableStress],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[Curve],[YieldStrengthKSI],[TensileStrengthKSI],[AllowableStressKSI],[YoungModulas],[PossionRatio],[YoungModulasKSI]) VALUES (
15,N'AISI Type 347',NULL,205,515,138,NULL,NULL,NULL,NULL,NULL,29733,74694,20015,195000,0.31,28282359);
GO
INSERT INTO [Material] ([MaterialID],[MaterialName],[MaterialCode],[YieldStrength],[TensileStrength],[AllowableStress],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[Curve],[YieldStrengthKSI],[TensileStrengthKSI],[AllowableStressKSI],[YoungModulas],[PossionRatio],[YoungModulasKSI]) VALUES (
16,N'AISI Type 347H',NULL,205,515,138,NULL,NULL,NULL,NULL,NULL,29733,74694,20015,195000,0.31,28282359);
GO
INSERT INTO [Material] ([MaterialID],[MaterialName],[MaterialCode],[YieldStrength],[TensileStrength],[AllowableStress],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[Curve],[YieldStrengthKSI],[TensileStrengthKSI],[AllowableStressKSI],[YoungModulas],[PossionRatio],[YoungModulasKSI]) VALUES (
17,N'Alloy 800, SB407',NULL,170,450,115,NULL,NULL,NULL,NULL,NULL,24656,65267,16679,197000,0.31,28572434);
GO
INSERT INTO [Material] ([MaterialID],[MaterialName],[MaterialCode],[YieldStrength],[TensileStrength],[AllowableStress],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[Curve],[YieldStrengthKSI],[TensileStrengthKSI],[AllowableStressKSI],[YoungModulas],[PossionRatio],[YoungModulasKSI]) VALUES (
18,N'Alloy 800H',NULL,170,450,115,NULL,NULL,NULL,NULL,NULL,24656,65267,16679,197000,0.31,28572434);
GO
INSERT INTO [Material] ([MaterialID],[MaterialName],[MaterialCode],[YieldStrength],[TensileStrength],[AllowableStress],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[Curve],[YieldStrengthKSI],[TensileStrengthKSI],[AllowableStressKSI],[YoungModulas],[PossionRatio],[YoungModulasKSI]) VALUES (
19,N'Alloy 800HT',NULL,170,450,115,NULL,NULL,NULL,NULL,NULL,24656,65267,16679,197000,0.31,28572434);
GO
INSERT INTO [Material] ([MaterialID],[MaterialName],[MaterialCode],[YieldStrength],[TensileStrength],[AllowableStress],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[Curve],[YieldStrengthKSI],[TensileStrengthKSI],[AllowableStressKSI],[YoungModulas],[PossionRatio],[YoungModulasKSI]) VALUES (
20,N'HK-40',NULL,205,485,138,NULL,NULL,NULL,NULL,NULL,29733,70343,20015,197000,0.31,28572434);
GO
INSERT INTO [Material] ([MaterialID],[MaterialName],[MaterialCode],[YieldStrength],[TensileStrength],[AllowableStress],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[Curve],[YieldStrengthKSI],[TensileStrengthKSI],[AllowableStressKSI],[YoungModulas],[PossionRatio],[YoungModulasKSI]) VALUES (
999,N'Other',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL);
GO
SET IDENTITY_INSERT [LocalMetalLoss] ON;
GO
INSERT INTO [LocalMetalLoss] ([LocalMetalLossID],[WidthOfTheLongGrid],[WidthOfTheCirGrid],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[ResultLevel1],[ResultLevel2],[AssessmentID],[ThicknessDataTypeID],[DistanceNearest],[NumberOfInspectionColumn],[NumberOfInspectionRow],[AllowableRSF],[AutomaticallyAllowableRSF]) VALUES (
20000002,NULL,NULL,{ts '2017-07-18 22:15:34.347'},NULL,{ts '2017-07-18 22:15:37.050'},NULL,NULL,NULL,3,2,NULL,NULL,NULL,NULL,NULL);
GO
SET IDENTITY_INSERT [LocalMetalLoss] OFF;
GO
SET IDENTITY_INSERT [LaminationItem] OFF;
GO
SET IDENTITY_INSERT [Lamination] OFF;
GO
SET IDENTITY_INSERT [InspectionGridData] ON;
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
19,1,0,0,1,{ts '2014-03-20 17:12:02.287'},N'admin',{ts '2014-03-20 17:12:02.287'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
20,10000008,0,0,34,{ts '2014-03-20 17:22:36.587'},N'admin',{ts '2014-03-20 17:22:36.587'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
21,10000009,0,0,12,{ts '2014-03-20 19:38:25.547'},N'admin',{ts '2014-03-20 19:38:25.547'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
22,10000001,0,0,0.75,{ts '2014-03-20 19:55:43.483'},N'admin',{ts '2014-03-20 19:55:43.483'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
23,10000001,0,1,0.75,{ts '2014-03-20 19:55:43.497'},N'admin',{ts '2014-03-20 19:55:43.497'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
24,10000001,0,2,0.75,{ts '2014-03-20 19:55:43.497'},N'admin',{ts '2014-03-20 19:55:43.497'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
25,10000001,0,3,0.75,{ts '2014-03-20 19:55:43.500'},N'admin',{ts '2014-03-20 19:55:43.500'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
26,10000001,0,4,0.75,{ts '2014-03-20 19:55:43.500'},N'admin',{ts '2014-03-20 19:55:43.500'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
27,10000001,0,5,0.75,{ts '2014-03-20 19:55:43.503'},N'admin',{ts '2014-03-20 19:55:43.503'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
28,10000001,0,6,0.75,{ts '2014-03-20 19:55:43.503'},N'admin',{ts '2014-03-20 19:55:43.503'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
29,10000001,0,7,0.75,{ts '2014-03-20 19:55:43.507'},N'admin',{ts '2014-03-20 19:55:43.507'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
30,10000001,1,0,0.75,{ts '2014-03-20 19:55:43.507'},N'admin',{ts '2014-03-20 19:55:43.507'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
31,10000001,1,1,0.48,{ts '2014-03-20 19:55:43.510'},N'admin',{ts '2014-03-20 19:55:43.510'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
32,10000001,1,2,0.52,{ts '2014-03-20 19:55:43.510'},N'admin',{ts '2014-03-20 19:55:43.510'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
33,10000001,1,3,0.57,{ts '2014-03-20 19:55:43.513'},N'admin',{ts '2014-03-20 19:55:43.513'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
34,10000001,1,4,0.58,{ts '2014-03-20 19:55:43.513'},N'admin',{ts '2014-03-20 19:55:43.513'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
35,10000001,1,5,0.6,{ts '2014-03-20 19:55:43.517'},N'admin',{ts '2014-03-20 19:55:43.517'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
36,10000001,1,6,0.57,{ts '2014-03-20 19:55:43.517'},N'admin',{ts '2014-03-20 19:55:43.517'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
37,10000001,1,7,0.75,{ts '2014-03-20 19:55:43.517'},N'admin',{ts '2014-03-20 19:55:43.517'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
38,10000001,2,0,0.75,{ts '2014-03-20 19:55:43.520'},N'admin',{ts '2014-03-20 19:55:43.520'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
39,10000001,2,1,0.57,{ts '2014-03-20 19:55:43.523'},N'admin',{ts '2014-03-20 19:55:43.523'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
40,10000001,2,2,0.59,{ts '2014-03-20 19:55:43.523'},N'admin',{ts '2014-03-20 19:55:43.523'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
41,10000001,2,3,0.55,{ts '2014-03-20 19:55:43.527'},N'admin',{ts '2014-03-20 19:55:43.527'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
42,10000001,2,4,0.59,{ts '2014-03-20 19:55:43.527'},N'admin',{ts '2014-03-20 19:55:43.527'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
43,10000001,2,5,0.6,{ts '2014-03-20 19:55:43.527'},N'admin',{ts '2014-03-20 19:55:43.527'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
44,10000001,2,6,0.66,{ts '2014-03-20 19:55:43.530'},N'admin',{ts '2014-03-20 19:55:43.530'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
45,10000001,2,7,0.75,{ts '2014-03-20 19:55:43.533'},N'admin',{ts '2014-03-20 19:55:43.533'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
46,10000001,3,0,0.75,{ts '2014-03-20 19:55:43.533'},N'admin',{ts '2014-03-20 19:55:43.533'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
47,10000001,3,1,0.61,{ts '2014-03-20 19:55:43.537'},N'admin',{ts '2014-03-20 19:55:43.537'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
48,10000001,3,2,0.47,{ts '2014-03-20 19:55:43.537'},N'admin',{ts '2014-03-20 19:55:43.537'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
49,10000001,3,3,0.58,{ts '2014-03-20 19:55:43.537'},N'admin',{ts '2014-03-20 19:55:43.537'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
50,10000001,3,4,0.36,{ts '2014-03-20 19:55:43.540'},N'admin',{ts '2014-03-20 19:55:43.540'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
51,10000001,3,5,0.58,{ts '2014-03-20 19:55:43.540'},N'admin',{ts '2014-03-20 19:55:43.540'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
52,10000001,3,6,0.64,{ts '2014-03-20 19:55:43.547'},N'admin',{ts '2014-03-20 19:55:43.547'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
53,10000001,3,7,0.75,{ts '2014-03-20 19:55:43.550'},N'admin',{ts '2014-03-20 19:55:43.550'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
54,10000001,4,0,0.75,{ts '2014-03-20 19:55:43.550'},N'admin',{ts '2014-03-20 19:55:43.550'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
55,10000001,4,1,0.62,{ts '2014-03-20 19:55:43.553'},N'admin',{ts '2014-03-20 19:55:43.553'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
56,10000001,4,2,0.59,{ts '2014-03-20 19:55:43.553'},N'admin',{ts '2014-03-20 19:55:43.553'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
57,10000001,4,3,0.58,{ts '2014-03-20 19:55:43.557'},N'admin',{ts '2014-03-20 19:55:43.557'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
58,10000001,4,4,0.57,{ts '2014-03-20 19:55:43.557'},N'admin',{ts '2014-03-20 19:55:43.557'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
59,10000001,4,5,0.48,{ts '2014-03-20 19:55:43.560'},N'admin',{ts '2014-03-20 19:55:43.560'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
60,10000001,4,6,0.62,{ts '2014-03-20 19:55:43.560'},N'admin',{ts '2014-03-20 19:55:43.560'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
61,10000001,4,7,0.75,{ts '2014-03-20 19:55:43.563'},N'admin',{ts '2014-03-20 19:55:43.563'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
62,10000001,5,0,0.75,{ts '2014-03-20 19:55:43.563'},N'admin',{ts '2014-03-20 19:55:43.563'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
63,10000001,5,1,0.57,{ts '2014-03-20 19:55:43.567'},N'admin',{ts '2014-03-20 19:55:43.567'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
64,10000001,5,2,0.59,{ts '2014-03-20 19:55:43.567'},N'admin',{ts '2014-03-20 19:55:43.567'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
65,10000001,5,3,0.61,{ts '2014-03-20 19:55:43.567'},N'admin',{ts '2014-03-20 19:55:43.567'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
66,10000001,5,4,0.57,{ts '2014-03-20 19:55:43.570'},N'admin',{ts '2014-03-20 19:55:43.570'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
67,10000001,5,5,0.56,{ts '2014-03-20 19:55:43.573'},N'admin',{ts '2014-03-20 19:55:43.573'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
68,10000001,5,6,0.49,{ts '2014-03-20 19:55:43.573'},N'admin',{ts '2014-03-20 19:55:43.573'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
69,10000001,5,7,0.75,{ts '2014-03-20 19:55:43.577'},N'admin',{ts '2014-03-20 19:55:43.577'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
70,10000001,6,0,0.75,{ts '2014-03-20 19:55:43.577'},N'admin',{ts '2014-03-20 19:55:43.577'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
71,10000001,6,1,0.75,{ts '2014-03-20 19:55:43.577'},N'admin',{ts '2014-03-20 19:55:43.577'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
72,10000001,6,2,0.75,{ts '2014-03-20 19:55:43.580'},N'admin',{ts '2014-03-20 19:55:43.580'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
73,10000001,6,3,0.75,{ts '2014-03-20 19:55:43.580'},N'admin',{ts '2014-03-20 19:55:43.580'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
74,10000001,6,4,0.75,{ts '2014-03-20 19:55:43.583'},N'admin',{ts '2014-03-20 19:55:43.583'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
75,10000001,6,5,0.75,{ts '2014-03-20 19:55:43.583'},N'admin',{ts '2014-03-20 19:55:43.583'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
76,10000001,6,6,0.75,{ts '2014-03-20 19:55:43.587'},N'admin',{ts '2014-03-20 19:55:43.587'},N'admin');
GO
INSERT INTO [InspectionGridData] ([InspectionGridDataID],[ObjectID],[RowNo],[ColumnNo],[Data],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy]) VALUES (
77,10000001,6,7,0.75,{ts '2014-03-20 19:55:43.587'},N'admin',{ts '2014-03-20 19:55:43.587'},N'admin');
GO
SET IDENTITY_INSERT [InspectionGridData] OFF;
GO
SET IDENTITY_INSERT [Hydrogen] OFF;
GO
INSERT INTO [HICDamage] ([HICDamageID],[HICDamageName]) VALUES (
1,N'HIC Damage');
GO
INSERT INTO [HeatExposureZone] ([HeatExposureZoneID],[HeatExposureZoneName]) VALUES (
1,N'Zone I Ambient Temperaute');
GO
INSERT INTO [HeatExposureZone] ([HeatExposureZoneID],[HeatExposureZoneName]) VALUES (
2,N'Zone II Ambient to 65 oC (150oF)');
GO
INSERT INTO [HeatExposureZone] ([HeatExposureZoneID],[HeatExposureZoneName]) VALUES (
3,N'Zone III Over 65 oC (150oF) to 205 oC (400 oF)');
GO
INSERT INTO [HeatExposureZone] ([HeatExposureZoneID],[HeatExposureZoneName]) VALUES (
4,N'Zone IV Over 205 oC (400oF) to 425 oC (800 oF)');
GO
INSERT INTO [HeatExposureZone] ([HeatExposureZoneID],[HeatExposureZoneName]) VALUES (
5,N'Zone V Over 425 oC (800oF) to 730 oC (1350 oF)');
GO
INSERT INTO [HeatExposureZone] ([HeatExposureZoneID],[HeatExposureZoneName]) VALUES (
6,N'Zone VI Over 730 oC (1350 oF)');
GO
SET IDENTITY_INSERT [GeneralMetalLoss] OFF;
GO
SET IDENTITY_INSERT [Fire] OFF;
GO
INSERT INTO [FailureMode] ([FailureModeID],[FailureModeName]) VALUES (
1,N'General Metal Loss');
GO
INSERT INTO [FailureMode] ([FailureModeID],[FailureModeName]) VALUES (
2,N'Local Metal Loss');
GO
INSERT INTO [FailureMode] ([FailureModeID],[FailureModeName]) VALUES (
3,N'Brittle');
GO
INSERT INTO [FailureMode] ([FailureModeID],[FailureModeName]) VALUES (
4,N'Pitting');
GO
INSERT INTO [FailureMode] ([FailureModeID],[FailureModeName]) VALUES (
5,N'Creep');
GO
INSERT INTO [FailureMode] ([FailureModeID],[FailureModeName]) VALUES (
6,N'Crack');
GO
INSERT INTO [FailureMode] ([FailureModeID],[FailureModeName]) VALUES (
7,N'Hydrogen blister');
GO
INSERT INTO [FailureMode] ([FailureModeID],[FailureModeName]) VALUES (
8,N'Weld misalignment and shell distortions');
GO
INSERT INTO [FailureMode] ([FailureModeID],[FailureModeName]) VALUES (
9,N'Fire damage');
GO
INSERT INTO [FailureMode] ([FailureModeID],[FailureModeName]) VALUES (
10,N'Dent, gouges, and dent-gouge combinations');
GO
INSERT INTO [FailureMode] ([FailureModeID],[FailureModeName]) VALUES (
11,N'Laminations');
GO
INSERT INTO [FabricationTolerance] ([FabricationToleranceID],[FabricationToleranceName],[EquipmentTypeID]) VALUES (
1,N'Out-Of-Roundness In Cylindrical Shells Under Internal Pressure',1);
GO
INSERT INTO [FabricationTolerance] ([FabricationToleranceID],[FabricationToleranceName],[EquipmentTypeID]) VALUES (
2,N'Centerline Offset Weld Misalignment – Longitudinal Joints',1);
GO
INSERT INTO [FabricationTolerance] ([FabricationToleranceID],[FabricationToleranceName],[EquipmentTypeID]) VALUES (
3,N'Centerline Offset Weld Misalignment - Circumferential Joints',1);
GO
INSERT INTO [FabricationTolerance] ([FabricationToleranceID],[FabricationToleranceName],[EquipmentTypeID]) VALUES (
4,N'Out-Of-Roundness In Piping Under Internal Pressure',2);
GO
INSERT INTO [FabricationTolerance] ([FabricationToleranceID],[FabricationToleranceName],[EquipmentTypeID]) VALUES (
5,N'Angular Weld Misalignment',2);
GO
INSERT INTO [FabricationTolerance] ([FabricationToleranceID],[FabricationToleranceName],[EquipmentTypeID]) VALUES (
6,N'Out-Of-Plumbness For Tank Shells',3);
GO
INSERT INTO [FabricationTolerance] ([FabricationToleranceID],[FabricationToleranceName],[EquipmentTypeID]) VALUES (
7,N'Out-Of-Roundness For Tank Shells',3);
GO
INSERT INTO [FabricationTolerance] ([FabricationToleranceID],[FabricationToleranceName],[EquipmentTypeID]) VALUES (
8,N'Centerline Offset Weld Radial Misalignment – All Butt Joints',3);
GO
INSERT INTO [EquipmentType] ([EquipmentTypeID],[EquipmentTypeName]) VALUES (
1,N'Pipe Components');
GO
INSERT INTO [EquipmentType] ([EquipmentTypeID],[EquipmentTypeName]) VALUES (
2,N'Pressure Vessel');
GO
INSERT INTO [EquipmentType] ([EquipmentTypeID],[EquipmentTypeName]) VALUES (
3,N'Atmospheric Storage Tank');
GO
INSERT INTO [DesignCode] ([DesignCodeID],[DesignCodeName],[EquipmentTypeID]) VALUES (
1,N'ASME Section VIII Div.1',2);
GO
INSERT INTO [DesignCode] ([DesignCodeID],[DesignCodeName],[EquipmentTypeID]) VALUES (
2,N'ASME Section VIII Div.2',2);
GO
INSERT INTO [DesignCode] ([DesignCodeID],[DesignCodeName],[EquipmentTypeID]) VALUES (
3,N'API 620',3);
GO
INSERT INTO [DesignCode] ([DesignCodeID],[DesignCodeName],[EquipmentTypeID]) VALUES (
4,N'ASME B31.3',1);
GO
INSERT INTO [DesignCode] ([DesignCodeID],[DesignCodeName],[EquipmentTypeID]) VALUES (
5,N'Other',1);
GO
INSERT INTO [DesignCode] ([DesignCodeID],[DesignCodeName],[EquipmentTypeID]) VALUES (
6,N'Other',2);
GO
INSERT INTO [DesignCode] ([DesignCodeID],[DesignCodeName],[EquipmentTypeID]) VALUES (
7,N'Other',3);
GO
SET IDENTITY_INSERT [Dent] OFF;
GO
SET IDENTITY_INSERT [Creep] OFF;
GO
INSERT INTO [CrackType] ([CrackTypeID],[CrackTypeName]) VALUES (
1,N'Flat plate - crack parallel t joint');
GO
INSERT INTO [CrackType] ([CrackTypeID],[CrackTypeName]) VALUES (
2,N'Cylinder - longitudinal joint-crack parallel to joint');
GO
INSERT INTO [CrackType] ([CrackTypeID],[CrackTypeName]) VALUES (
3,N'Cylinder - longitudinal join-crack perpendiculture');
GO
INSERT INTO [CrackType] ([CrackTypeID],[CrackTypeName]) VALUES (
4,N'Cylinder-circumferential joint-crack parallel to joint');
GO
INSERT INTO [CrackType] ([CrackTypeID],[CrackTypeName]) VALUES (
5,N'Cylinder-circumferential joint-crack perpendicular to joint');
GO
INSERT INTO [CrackType] ([CrackTypeID],[CrackTypeName]) VALUES (
6,N'Sphere-circumferential joint-crack parallel to joint');
GO
INSERT INTO [CrackType] ([CrackTypeID],[CrackTypeName]) VALUES (
7,N'Sphere-circumferential joint-crack perpendicular to joint');
GO
INSERT INTO [CrackLocation] ([CrackLocationID],[CrackLocationName]) VALUES (
1,N'Base material');
GO
INSERT INTO [CrackLocation] ([CrackLocationID],[CrackLocationName]) VALUES (
2,N'Weld metal without PWHT');
GO
INSERT INTO [CrackLocation] ([CrackLocationID],[CrackLocationName]) VALUES (
3,N'Weld metal with PWHT');
GO
SET IDENTITY_INSERT [Crack] OFF;
GO
INSERT INTO [ComponentType] ([ComponentTypeID],[ComponentTypeName],[EquipmentTypeID]) VALUES (
1,N'Straight Pipe',1);
GO
INSERT INTO [ComponentType] ([ComponentTypeID],[ComponentTypeName],[EquipmentTypeID]) VALUES (
2,N'Cylindical Shell',2);
GO
INSERT INTO [ComponentType] ([ComponentTypeID],[ComponentTypeName],[EquipmentTypeID]) VALUES (
3,N'Spherical Shell',2);
GO
INSERT INTO [ComponentType] ([ComponentTypeID],[ComponentTypeName],[EquipmentTypeID]) VALUES (
4,N'Hemispherical Head',2);
GO
INSERT INTO [ComponentType] ([ComponentTypeID],[ComponentTypeName],[EquipmentTypeID]) VALUES (
5,N'Storage Tank',3);
GO
INSERT INTO [ComponentShape] ([ComponentShapeID],[ComponentShapeName]) VALUES (
1,N'Cylindical');
GO
INSERT INTO [ComponentShape] ([ComponentShapeID],[ComponentShapeName]) VALUES (
2,N'Spherical');
GO
SET IDENTITY_INSERT [Brittle] ON;
GO
INSERT INTO [Brittle] ([BrittleID],[AssessmentID],[TheCriticalExposureTemperature],[TheMinimumAllowableTemperature],[AutomaticcallyTheMinimumAllowableTemperature],[TheUncorrodedGoverningThickness],[Fabricated],[WallThickness38],[PWHT],[ResultLevel1],[ResultLevel2],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[ReductionInTheMATID]) VALUES (
1,4,123,NULL,1,NULL,0,0,0,N'The Level 1 The Level 1 assessment criteria are  not satisfied.',NULL,{ts '2017-07-22 20:52:32.610'},NULL,{ts '2017-07-22 20:52:35.367'},NULL,NULL);
GO
SET IDENTITY_INSERT [Brittle] OFF;
GO
INSERT INTO [AssessmentMaterial] ([AssessmentMaterialID],[AssessmentMaterialName],[MaterialID]) VALUES (
1,N'Carbon Steel',1);
GO
INSERT INTO [AssessmentMaterial] ([AssessmentMaterialID],[AssessmentMaterialName],[MaterialID]) VALUES (
2,N'Carbon Steel - Graphitized',2);
GO
INSERT INTO [AssessmentMaterial] ([AssessmentMaterialID],[AssessmentMaterialName],[MaterialID]) VALUES (
3,N'C-0.5Mo',3);
GO
INSERT INTO [AssessmentMaterial] ([AssessmentMaterialID],[AssessmentMaterialName],[MaterialID]) VALUES (
4,N'1.25Cr-0.5Mo – N&T',4);
GO
INSERT INTO [AssessmentMaterial] ([AssessmentMaterialID],[AssessmentMaterialName],[MaterialID]) VALUES (
5,N'1.25Cr-0.5Mo – Annealed',4);
GO
INSERT INTO [AssessmentMaterial] ([AssessmentMaterialID],[AssessmentMaterialName],[MaterialID]) VALUES (
6,N'2.25Cr-1Mo – N&T',5);
GO
INSERT INTO [AssessmentMaterial] ([AssessmentMaterialID],[AssessmentMaterialName],[MaterialID]) VALUES (
7,N'2.25Cr-1Mo – Annealed',5);
GO
INSERT INTO [AssessmentMaterial] ([AssessmentMaterialID],[AssessmentMaterialName],[MaterialID]) VALUES (
8,N'2.25Cr-1Mo – Q&T',5);
GO
INSERT INTO [AssessmentMaterial] ([AssessmentMaterialID],[AssessmentMaterialName],[MaterialID]) VALUES (
9,N'2.25Cr-1Mo-V',6);
GO
INSERT INTO [AssessmentMaterial] ([AssessmentMaterialID],[AssessmentMaterialName],[MaterialID]) VALUES (
10,N'5Cr-0.5Mo',7);
GO
INSERT INTO [AssessmentMaterial] ([AssessmentMaterialID],[AssessmentMaterialName],[MaterialID]) VALUES (
11,N'9Cr-1Mo',8);
GO
INSERT INTO [AssessmentMaterial] ([AssessmentMaterialID],[AssessmentMaterialName],[MaterialID]) VALUES (
12,N'9Cr-1Mo-V',9);
GO
INSERT INTO [AssessmentMaterial] ([AssessmentMaterialID],[AssessmentMaterialName],[MaterialID]) VALUES (
13,N'12 Cr',10);
GO
INSERT INTO [AssessmentMaterial] ([AssessmentMaterialID],[AssessmentMaterialName],[MaterialID]) VALUES (
14,N'AISI Type 304 & 304H',11);
GO
INSERT INTO [AssessmentMaterial] ([AssessmentMaterialID],[AssessmentMaterialName],[MaterialID]) VALUES (
15,N'AISI Type 316 & 316H',12);
GO
INSERT INTO [AssessmentMaterial] ([AssessmentMaterialID],[AssessmentMaterialName],[MaterialID]) VALUES (
16,N'AISI Type 321',13);
GO
INSERT INTO [AssessmentMaterial] ([AssessmentMaterialID],[AssessmentMaterialName],[MaterialID]) VALUES (
17,N'AISI Type 321H',14);
GO
INSERT INTO [AssessmentMaterial] ([AssessmentMaterialID],[AssessmentMaterialName],[MaterialID]) VALUES (
18,N'AISI Type 347',15);
GO
INSERT INTO [AssessmentMaterial] ([AssessmentMaterialID],[AssessmentMaterialName],[MaterialID]) VALUES (
19,N'AISI Type 347H',16);
GO
INSERT INTO [AssessmentMaterial] ([AssessmentMaterialID],[AssessmentMaterialName],[MaterialID]) VALUES (
20,N'Alloy 800',17);
GO
INSERT INTO [AssessmentMaterial] ([AssessmentMaterialID],[AssessmentMaterialName],[MaterialID]) VALUES (
21,N'Alloy 800H',18);
GO
INSERT INTO [AssessmentMaterial] ([AssessmentMaterialID],[AssessmentMaterialName],[MaterialID]) VALUES (
22,N'Alloy 800HT',19);
GO
INSERT INTO [AssessmentMaterial] ([AssessmentMaterialID],[AssessmentMaterialName],[MaterialID]) VALUES (
23,N'HK-40',20);
GO
INSERT INTO [AssessmentLevel] ([AssessmentLevelID],[AssessmentLevelName]) VALUES (
1,N'Level 1');
GO
INSERT INTO [AssessmentLevel] ([AssessmentLevelID],[AssessmentLevelName]) VALUES (
2,N'Level 2');
GO
SET IDENTITY_INSERT [Assessment] ON;
GO
INSERT INTO [Assessment] ([AssessmentID],[EquipmentTypeID],[MethodologyID],[UnitID],[AssessmentLevelID],[AnalysisBy],[AnalysisDate],[AnalysisDetail],[ComponentTypeID],[DesignCodeID],[PressureDesign],[TemperatureDesign],[InsideDiameter],[NominalThickness],[AutomaticallyMinRequireThickness],[MinRequireLongitutinalThickness],[MinRequireCircumferentialThickness],[FutureCorrosionAllowance],[UniformMetalLoss],[WeldEfficiency],[AllowableRSF],[MaterialID],[AllowableStress],[AutomaticallyCalculationAllowableStress],[UltimatedTensileStrength],[FailureModeID],[OperatingPressure],[OperatingTemperature],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[AutomaticallyAllowableRSF],[YieldStrength],[EquipmentNumber],[EquipmentImage],[ExternalPressure],[AssessmentResult],[ComponentShapeID],[YearOfFabrication],[ASMEExemptionCurvesID],[MaterialTypeID],[ReferenceTemperature],[AutomaticallyCalculationTheNominalStressOfTheComponent],[TheNominalStressOfTheComponent],[AutomaticCalculationReferenceTemperature],[MaterialName]) VALUES (
3,1,1,1,2,N'',NULL,NULL,1,4,NULL,NULL,NULL,NULL,1,NULL,NULL,NULL,NULL,NULL,NULL,1,13855,0,415,2,NULL,NULL,{ts '2017-07-18 22:15:34.147'},NULL,{ts '2017-07-18 22:15:36.987'},NULL,NULL,205,N'',NULL,NULL,NULL,1,NULL,1,1,NULL,1,NULL,0,NULL);
GO
INSERT INTO [Assessment] ([AssessmentID],[EquipmentTypeID],[MethodologyID],[UnitID],[AssessmentLevelID],[AnalysisBy],[AnalysisDate],[AnalysisDetail],[ComponentTypeID],[DesignCodeID],[PressureDesign],[TemperatureDesign],[InsideDiameter],[NominalThickness],[AutomaticallyMinRequireThickness],[MinRequireLongitutinalThickness],[MinRequireCircumferentialThickness],[FutureCorrosionAllowance],[UniformMetalLoss],[WeldEfficiency],[AllowableRSF],[MaterialID],[AllowableStress],[AutomaticallyCalculationAllowableStress],[UltimatedTensileStrength],[FailureModeID],[OperatingPressure],[OperatingTemperature],[CreatedDate],[CreatedBy],[UpdatedDate],[UpdatedBy],[AutomaticallyAllowableRSF],[YieldStrength],[EquipmentNumber],[EquipmentImage],[ExternalPressure],[AssessmentResult],[ComponentShapeID],[YearOfFabrication],[ASMEExemptionCurvesID],[MaterialTypeID],[ReferenceTemperature],[AutomaticallyCalculationTheNominalStressOfTheComponent],[TheNominalStressOfTheComponent],[AutomaticCalculationReferenceTemperature],[MaterialName]) VALUES (
4,1,1,1,1,N'Pok',{ts '2017-07-22 00:00:00.000'},NULL,1,4,1,2,1,1,1,NULL,NULL,1,1,1,NULL,1,138,0,415,3,1,2,{ts '2017-07-22 20:52:32.460'},NULL,{ts '2017-07-22 20:52:35.307'},NULL,NULL,205,N'1234',NULL,NULL,NULL,NULL,NULL,2,1,NULL,1,NULL,0,NULL);
GO
SET IDENTITY_INSERT [Assessment] OFF;
GO
INSERT INTO [ASMEExemptionCurves] ([ASMEExemptionCurvesID],[ASMEExemptionCurvesName]) VALUES (
1,N'ASME Exemption Curves A');
GO
INSERT INTO [ASMEExemptionCurves] ([ASMEExemptionCurvesID],[ASMEExemptionCurvesName]) VALUES (
2,N'ASME Exemption Curves B');
GO
INSERT INTO [ASMEExemptionCurves] ([ASMEExemptionCurvesID],[ASMEExemptionCurvesName]) VALUES (
3,N'ASME Exemption Curves C');
GO
INSERT INTO [ASMEExemptionCurves] ([ASMEExemptionCurvesID],[ASMEExemptionCurvesName]) VALUES (
4,N'ASME Exemption Curves D');
GO
ALTER TABLE [Weld] ADD CONSTRAINT [PK_Weld] PRIMARY KEY ([WeldID]);
GO
ALTER TABLE [TheStandardPitChart] ADD CONSTRAINT [PK_TheStandardPitChart] PRIMARY KEY ([TheStandardPitChartID]);
GO
ALTER TABLE [Pitting] ADD CONSTRAINT [PK_Pitting] PRIMARY KEY ([PittingID]);
GO
ALTER TABLE [MaterialType] ADD CONSTRAINT [PK_MaterialType] PRIMARY KEY ([MaterialTypeID]);
GO
ALTER TABLE [LocalMetalLoss] ADD CONSTRAINT [PK_LocalMetalLoss] PRIMARY KEY ([LocalMetalLossID]);
GO
ALTER TABLE [LaminationItem] ADD CONSTRAINT [PK_LaminationItem] PRIMARY KEY ([LaminationItemID]);
GO
ALTER TABLE [Lamination] ADD CONSTRAINT [PK_Lamination] PRIMARY KEY ([LaminationID]);
GO
ALTER TABLE [InspectionGridData] ADD CONSTRAINT [PK_InspectionGridData] PRIMARY KEY ([InspectionGridDataID]);
GO
ALTER TABLE [Hydrogen] ADD CONSTRAINT [PK_Hydrogen] PRIMARY KEY ([HydrogenID]);
GO
ALTER TABLE [GeneralMetalLoss] ADD CONSTRAINT [PK__GeneralMetalLoss__0000000000000112] PRIMARY KEY ([GeneralMetalLossID]);
GO
ALTER TABLE [Fire] ADD CONSTRAINT [PK_Fire] PRIMARY KEY ([FireID]);
GO
ALTER TABLE [Dent] ADD CONSTRAINT [PK_Dent] PRIMARY KEY ([DentID]);
GO
ALTER TABLE [Creep] ADD CONSTRAINT [PK_Creep] PRIMARY KEY ([CreepID]);
GO
ALTER TABLE [CrackType] ADD CONSTRAINT [PK_CrackType] PRIMARY KEY ([CrackTypeID]);
GO
ALTER TABLE [CrackLocation] ADD CONSTRAINT [PK_CrackLocation] PRIMARY KEY ([CrackLocationID]);
GO
ALTER TABLE [Crack] ADD CONSTRAINT [PK_Crack] PRIMARY KEY ([CrackID]);
GO
ALTER TABLE [ComponentShape] ADD CONSTRAINT [PK_ComponentShape] PRIMARY KEY ([ComponentShapeID]);
GO
ALTER TABLE [Brittle] ADD CONSTRAINT [PK_Brittle] PRIMARY KEY ([BrittleID]);
GO
ALTER TABLE [AssessmentMaterial] ADD CONSTRAINT [PK_AssessmentMaterial] PRIMARY KEY ([AssessmentMaterialID]);
GO
ALTER TABLE [Assessment] ADD CONSTRAINT [PK_Assessment] PRIMARY KEY ([AssessmentID]);
GO

