using ffs.api.Model;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api
{
    public class CreepModule : Nancy.NancyModule
    {
        public CreepModule() : base("/api/creep")
        {
            Post["/calculation/level{level}/unit{unit}"] = x =>
            {

                var level = (int)x.level;
                var unit = (int)x.unit;

                CreepCalculator calculator = new CreepCalculator(new GlobalVar(unit));

                var input = this.Bind<BeanCreep>();

                var result = calculator.calculateLevel1(input);

                return Response.AsJson(result);
            };

        }

        internal class CreepCalculator : CalculatorBase
        {
            public CreepCalculator(GlobalVar var) : base(var)
            {

            }
            public  BeanCreepResult calculateLevel1(BeanCreep p_beanCreep)
            {
                BeanCreepResult beanCreepResult = new BeanCreepResult();
                double? num;
                if (p_beanCreep.TheComponentContainsAWeldJoint == true)
                {
                    if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_SI)
                    {
                        BeanCreepResult arg_7A_0 = beanCreepResult;
                        num = p_beanCreep.operatingTemperature;
                        arg_7A_0.tmax_1 = (num.HasValue ? new double?(num.GetValueOrDefault() + 14.0) : null);
                    }
                    else
                    {
                        BeanCreepResult arg_B9_0 = beanCreepResult;
                        num = p_beanCreep.operatingTemperature;
                        arg_B9_0.tmax_1 = (num.HasValue ? new double?(num.GetValueOrDefault() + 25.0) : null);
                    }
                }
                else
                {
                    beanCreepResult.tmax_1 = p_beanCreep.operatingTemperature;
                }
                beanCreepResult.pmax_1 = p_beanCreep.operatingPressure;
                beanCreepResult.toper_1 = p_beanCreep.ExcursionDuration;
                BeanCreepResult arg_121_0 = beanCreepResult;
                num = p_beanCreep.insideDiameter;
                arg_121_0.r_2 = (num.HasValue ? new double?(num.GetValueOrDefault() / 2.0) : null);
                BeanCreepResult arg_1A8_0 = beanCreepResult;
                num = p_beanCreep.nominalThickness;
                double? num2 = p_beanCreep.fca;
                num = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() - num2.GetValueOrDefault()) : null);
                num2 = p_beanCreep.loss;
                arg_1A8_0.tc_2 = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() - num2.GetValueOrDefault()) : null);
                BeanCreepResult arg_29A_0 = beanCreepResult;
                num = beanCreepResult.pmax_1;
                num2 = p_beanCreep.weldJointEfficiency;
                num = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() / num2.GetValueOrDefault()) : null);
                num2 = beanCreepResult.r_2;
                double? tc_ = beanCreepResult.tc_2;
                num2 = ((num2.HasValue & tc_.HasValue) ? new double?(num2.GetValueOrDefault() / tc_.GetValueOrDefault()) : null);
                num2 = (num2.HasValue ? new double?(num2.GetValueOrDefault() + 0.6) : null);
                arg_29A_0.omc_2 = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() * num2.GetValueOrDefault()) : null);
                BeanCreepResult arg_3BA_0 = beanCreepResult;
                num = beanCreepResult.pmax_1;
                num2 = p_beanCreep.weldJointEfficiency;
                num2 = (num2.HasValue ? new double?(2.0 * num2.GetValueOrDefault()) : null);
                num = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() / num2.GetValueOrDefault()) : null);
                num2 = beanCreepResult.r_2;
                tc_ = beanCreepResult.tc_2;
                num2 = ((num2.HasValue & tc_.HasValue) ? new double?(num2.GetValueOrDefault() / tc_.GetValueOrDefault()) : null);
                num2 = (num2.HasValue ? new double?(num2.GetValueOrDefault() - 0.4) : null);
                arg_3BA_0.oml_2 = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() * num2.GetValueOrDefault()) : null);
                if (p_beanCreep.automaticallyCalculationTheNominalStressOfTheComponent.Value)
                {
                    beanCreepResult.omax_2 = new double?(Math.Max(beanCreepResult.omc_2.Value, beanCreepResult.oml_2.Value));
                }
                else
                {
                    beanCreepResult.omax_2 = p_beanCreep.theNominalStressOfTheComponent;
                }
                if (p_beanCreep.AssessmentMaterialID == 1)
                {
                    beanCreepResult.figure_3 = "Figure 10.3";
                }
                else if (p_beanCreep.AssessmentMaterialID == 2)
                {
                    beanCreepResult.figure_3 = "Figure 10.4";
                }
                else if (p_beanCreep.AssessmentMaterialID == 3)
                {
                    beanCreepResult.figure_3 = "Figure 10.5";
                }
                else if (p_beanCreep.AssessmentMaterialID == 4)
                {
                    beanCreepResult.figure_3 = "Figure 10.6";
                }
                else if (p_beanCreep.AssessmentMaterialID == 5)
                {
                    beanCreepResult.figure_3 = "Figure 10.7";
                }
                else if (p_beanCreep.AssessmentMaterialID == 6)
                {
                    beanCreepResult.figure_3 = "Figure 10.8";
                }
                else if (p_beanCreep.AssessmentMaterialID == 7)
                {
                    beanCreepResult.figure_3 = "Figure 10.9";
                }
                else if (p_beanCreep.AssessmentMaterialID == 8)
                {
                    beanCreepResult.figure_3 = "Figure 10.10";
                }
                else if (p_beanCreep.AssessmentMaterialID == 9)
                {
                    beanCreepResult.figure_3 = "Figure 10.11";
                }
                else if (p_beanCreep.AssessmentMaterialID == 10)
                {
                    beanCreepResult.figure_3 = "Figure 10.12";
                }
                else if (p_beanCreep.AssessmentMaterialID == 11)
                {
                    beanCreepResult.figure_3 = "Figure 10.13";
                }
                else if (p_beanCreep.AssessmentMaterialID == 12)
                {
                    beanCreepResult.figure_3 = "Figure 10.14";
                }
                else if (p_beanCreep.AssessmentMaterialID == 13)
                {
                    beanCreepResult.figure_3 = "Figure 10.15";
                }
                else if (p_beanCreep.AssessmentMaterialID == 14)
                {
                    beanCreepResult.figure_3 = "Figure 10.16";
                }
                else if (p_beanCreep.AssessmentMaterialID == 15)
                {
                    beanCreepResult.figure_3 = "Figure 10.17";
                }
                else if (p_beanCreep.AssessmentMaterialID == 16)
                {
                    beanCreepResult.figure_3 = "Figure 10.18";
                }
                else if (p_beanCreep.AssessmentMaterialID == 17)
                {
                    beanCreepResult.figure_3 = "Figure 10.19";
                }
                else if (p_beanCreep.AssessmentMaterialID == 18)
                {
                    beanCreepResult.figure_3 = "Figure 10.20";
                }
                else if (p_beanCreep.AssessmentMaterialID == 19)
                {
                    beanCreepResult.figure_3 = "Figure 10.21";
                }
                else if (p_beanCreep.AssessmentMaterialID == 20)
                {
                    beanCreepResult.figure_3 = "Figure 10.22";
                }
                else if (p_beanCreep.AssessmentMaterialID == 21)
                {
                    beanCreepResult.figure_3 = "Figure 10.23";
                }
                else if (p_beanCreep.AssessmentMaterialID == 22)
                {
                    beanCreepResult.figure_3 = "Figure 10.24";
                }
                else if (p_beanCreep.AssessmentMaterialID == 23)
                {
                    beanCreepResult.figure_3 = "Figure 10.25";
                }
                else
                {
                    beanCreepResult.figure_3 = "Other";
                }
                double[,] array = new double[,]
                {
                {
                    1873.3548,
                    1793.7914,
                    1556.6622,
                    1469.2414,
                    1160.8094
                },
                {
                    1293.7004,
                    1379.5783,
                    1421.6155,
                    1357.2764,
                    1276.9889
                },
                {
                    187125.057,
                    81434.5143,
                    33658.4616,
                    7353.9466,
                    1980.1276
                },
                {
                    18303.3005,
                    45825.9314,
                    43314.0493,
                    76564.9528,
                    75697.8932
                },
                {
                    1965.9876,
                    3552.7302,
                    3051.3128,
                    3991.1766,
                    3946.7876
                },
                {
                    499.9115,
                    547.4882,
                    594.707,
                    639.9928,
                    628.8028
                },
                {
                    1335.6512,
                    1588.3182,
                    2323.678,
                    7612.6754,
                    24308.0294
                },
                {
                    622.2131,
                    603.5627,
                    598.9685,
                    649.0881,
                    643.5433
                },
                {
                    728.2102,
                    1299.3713,
                    1572.9472,
                    1478.5045,
                    2078.508
                },
                {
                    5192.9956,
                    5599.7801,
                    6367.8569,
                    11881.2203,
                    18904.706
                },
                {
                    435.2756,
                    528.5429,
                    598.2394,
                    1071.6384,
                    1712.8038
                },
                {
                    24779.6574,
                    22821.512,
                    30683.944,
                    70193.0749,
                    315408.7209
                },
                {
                    133060.9947,
                    242534.5398,
                    279688.8785,
                    321743.5027,
                    229268.8258
                },
                {
                    108464.1926,
                    124988.3524,
                    135395.2398,
                    215453.5608,
                    353356.2272
                },
                {
                    129517.4279,
                    137249.0281,
                    181378.7512,
                    258538.5821,
                    279992.0056
                },
                {
                    38082.4785,
                    50612.0871,
                    57881.392,
                    86276.744,
                    216138.236
                },
                {
                    41781.1878,
                    68324.9618,
                    112174.3199,
                    212149.6821,
                    748461.5335
                },
                {
                    55559.4422,
                    118047.5655,
                    210239.0233,
                    607072.0627,
                    2200219.6838
                },
                {
                    20644.766,
                    44140.4306,
                    77651.7212,
                    172795.1449,
                    699773.8189
                },
                {
                    2609.5214,
                    2323.5859,
                    2537.6112,
                    2619.8135,
                    2152.6089
                },
                {
                    1902.0519,
                    1702.866,
                    1764.7446,
                    2338.7351,
                    1704.4219
                },
                {
                    1384.6196,
                    1474.2218,
                    1406.4351,
                    1649.3306,
                    2400.2608
                },
                {
                    2097.0274,
                    2615.4895,
                    3223.7027,
                    3336.7877,
                    10336.8662
                }
                };
                double[,] array2 = new double[,]
                {
                {
                    -0.005348,
                    -0.005508,
                    -0.005633,
                    -0.005976,
                    -0.006158
                },
                {
                    -0.005313,
                    -0.005595,
                    -0.005888,
                    -0.00627,
                    -0.006659
                },
                {
                    -0.009076,
                    -0.008519,
                    -0.007957,
                    -0.006847,
                    -0.00585
                },
                {
                    -0.006649,
                    -0.007818,
                    -0.008082,
                    -0.009217,
                    -0.009764
                },
                {
                    -0.004781,
                    -0.005479,
                    -0.005553,
                    -0.006134,
                    -0.006475
                },
                {
                    -0.003111,
                    -0.00332,
                    -0.003557,
                    -0.003856,
                    -0.004058
                },
                {
                    -0.003968,
                    -0.004272,
                    -0.004834,
                    -0.006376,
                    -0.007975
                },
                {
                    -0.003379,
                    -0.00346,
                    -0.003583,
                    -0.003887,
                    -0.0041
                },
                {
                    -0.003176,
                    -0.003914,
                    -0.004261,
                    -0.004431,
                    -0.005075
                },
                {
                    -0.005376,
                    -0.005614,
                    -0.005947,
                    -0.005947,
                    -0.007732
                },
                {
                    -0.003649,
                    -0.003954,
                    -0.004245,
                    -0.005067,
                    -0.005837
                },
                {
                    -0.006018,
                    -0.006082,
                    -0.006526,
                    -0.007568,
                    -0.00932
                },
                {
                    -0.008052,
                    -0.008817,
                    -0.009228,
                    -0.009763,
                    -0.009843
                },
                {
                    -0.006645,
                    -0.006972,
                    -0.007321,
                    -0.008142,
                    -0.009018
                },
                {
                    -0.006166,
                    -0.006446,
                    -0.006973,
                    -0.007709,
                    -0.008298
                },
                {
                    -0.00647,
                    -0.006952,
                    -0.007357,
                    -0.008136,
                    -0.009453
                },
                {
                    -0.006443,
                    -0.007062,
                    -0.00778,
                    -0.008781,
                    -0.010364
                },
                {
                    -0.006992,
                    -0.007872,
                    -0.008674,
                    -0.010101,
                    -0.011789
                },
                {
                    -0.005831,
                    -0.006673,
                    -0.007432,
                    -0.008595,
                    -0.01032
                },
                {
                    -0.004301,
                    -0.004368,
                    -0.004622,
                    -0.004937,
                    -0.005104
                },
                {
                    -0.003831,
                    -0.003907,
                    -0.004124,
                    -0.004594,
                    -0.004671
                },
                {
                    -0.003566,
                    -0.003744,
                    -0.003889,
                    -0.004253,
                    -0.004802
                },
                {
                    -0.003817,
                    -0.004125,
                    -0.004479,
                    -0.004839,
                    -0.005975
                }
                };
                int num3;
                if (p_beanCreep.AssessmentMaterialID == 1)
                {
                    num3 = 0;
                }
                else if (p_beanCreep.AssessmentMaterialID == 2)
                {
                    num3 = 1;
                }
                else if (p_beanCreep.AssessmentMaterialID == 3)
                {
                    num3 = 2;
                }
                else if (p_beanCreep.AssessmentMaterialID == 4)
                {
                    num3 = 3;
                }
                else if (p_beanCreep.AssessmentMaterialID == 5)
                {
                    num3 = 4;
                }
                else if (p_beanCreep.AssessmentMaterialID == 6)
                {
                    num3 = 5;
                }
                else if (p_beanCreep.AssessmentMaterialID == 7)
                {
                    num3 = 6;
                }
                else if (p_beanCreep.AssessmentMaterialID == 8)
                {
                    num3 = 7;
                }
                else if (p_beanCreep.AssessmentMaterialID == 9)
                {
                    num3 = 8;
                }
                else if (p_beanCreep.AssessmentMaterialID == 10)
                {
                    num3 = 9;
                }
                else if (p_beanCreep.AssessmentMaterialID == 11)
                {
                    num3 = 10;
                }
                else if (p_beanCreep.AssessmentMaterialID == 12)
                {
                    num3 = 11;
                }
                else if (p_beanCreep.AssessmentMaterialID == 13)
                {
                    num3 = 12;
                }
                else if (p_beanCreep.AssessmentMaterialID == 14)
                {
                    num3 = 13;
                }
                else if (p_beanCreep.AssessmentMaterialID == 15)
                {
                    num3 = 14;
                }
                else if (p_beanCreep.AssessmentMaterialID == 16)
                {
                    num3 = 15;
                }
                else if (p_beanCreep.AssessmentMaterialID == 17)
                {
                    num3 = 16;
                }
                else if (p_beanCreep.AssessmentMaterialID == 18)
                {
                    num3 = 17;
                }
                else if (p_beanCreep.AssessmentMaterialID == 19)
                {
                    num3 = 18;
                }
                else if (p_beanCreep.AssessmentMaterialID == 20)
                {
                    num3 = 19;
                }
                else if (p_beanCreep.AssessmentMaterialID == 21)
                {
                    num3 = 20;
                }
                else if (p_beanCreep.AssessmentMaterialID == 22)
                {
                    num3 = 21;
                }
                else if (p_beanCreep.AssessmentMaterialID == 23)
                {
                    num3 = 22;
                }
                else
                {
                    num3 = -1;
                }
                double num4;
                if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_SI)
                {
                    num4 = p_beanCreep.operatingTemperature.Value * 18.0 + 32.0;
                }
                else
                {
                    num4 = p_beanCreep.operatingTemperature.Value;
                }
                double num5;
                if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_SI)
                {
                    num = beanCreepResult.omax_2;
                    num = (num.HasValue ? new double?(num.GetValueOrDefault() * 145.038) : null);
                    num5 = (num.HasValue ? new double?(num.GetValueOrDefault() / 1000.0) : null).Value;
                }
                else
                {
                    num = beanCreepResult.omax_2;
                    num5 = (num.HasValue ? new double?(num.GetValueOrDefault() / 1000.0) : null).Value;
                }
                double[] array3 = new double[]
                {
                Math.Log(num5 / array[num3, 4]) / array2[num3, 4],
                Math.Log(num5 / array[num3, 3]) / array2[num3, 3],
                Math.Log(num5 / array[num3, 2]) / array2[num3, 2],
                Math.Log(num5 / array[num3, 1]) / array2[num3, 1],
                Math.Log(num5 / array[num3, 0]) / array2[num3, 0]
                };
                int num6 = 1;
                for (int i = 0; i <= 3; i++)
                {
                    if (num4 > array3[i] && num4 <= array3[i + 1])
                    {
                        num6 = i + 1;
                        break;
                    }
                    num6 = i + 1;
                }
                int num7 = num6 - 1;
                double num8 = array3[num7];
                int num9;
                if (num7 == 0)
                {
                    num9 = 250000;
                }
                else if (num7 == 1)
                {
                    num9 = 25000;
                }
                else if (num7 == 2)
                {
                    num9 = 2500;
                }
                else if (num7 == 3)
                {
                    num9 = 250;
                }
                else
                {
                    num9 = 25;
                }
                double num10 = array3[num6];
                int num11;
                if (num6 == 0)
                {
                    num11 = 250000;
                }
                else if (num6 == 1)
                {
                    num11 = 25000;
                }
                else if (num6 == 2)
                {
                    num11 = 2500;
                }
                else if (num6 == 3)
                {
                    num11 = 250;
                }
                else
                {
                    num11 = 25;
                }
                if (p_beanCreep.AutomaticallyCalculationTheMaximumPermissibleTime == false)
                {
                    beanCreepResult.tpermit_3 = p_beanCreep.TheMaximumPermissibleTime;
                }
                else
                {
                    beanCreepResult.tpermit_3 = new double?((double)num9 + (double)(num11 - num9) / (num10 - num8) * (num4 - num8));
                }
                num = beanCreepResult.tpermit_3;
                num2 = beanCreepResult.toper_1;
                if (num.GetValueOrDefault() >= num2.GetValueOrDefault() && (num.HasValue & num2.HasValue))
                {
                    beanCreepResult.checkTPermit_3 = new bool?(true);
                }
                else
                {
                    beanCreepResult.checkTPermit_3 = new bool?(false);
                }
                int num12;
                if (p_beanCreep.AssessmentMaterialID == 1)
                {
                    num12 = 0;
                }
                else if (p_beanCreep.AssessmentMaterialID == 2)
                {
                    num12 = 2;
                }
                else if (p_beanCreep.AssessmentMaterialID == 3)
                {
                    num12 = 4;
                }
                else if (p_beanCreep.AssessmentMaterialID == 4)
                {
                    num12 = 6;
                }
                else if (p_beanCreep.AssessmentMaterialID == 5)
                {
                    num12 = 8;
                }
                else if (p_beanCreep.AssessmentMaterialID == 6)
                {
                    num12 = 10;
                }
                else if (p_beanCreep.AssessmentMaterialID == 7)
                {
                    num12 = 12;
                }
                else if (p_beanCreep.AssessmentMaterialID == 8)
                {
                    num12 = 14;
                }
                else if (p_beanCreep.AssessmentMaterialID == 9)
                {
                    num12 = 16;
                }
                else if (p_beanCreep.AssessmentMaterialID == 10)
                {
                    num12 = 18;
                }
                else if (p_beanCreep.AssessmentMaterialID == 11)
                {
                    num12 = 20;
                }
                else if (p_beanCreep.AssessmentMaterialID == 12)
                {
                    num12 = 22;
                }
                else if (p_beanCreep.AssessmentMaterialID == 13)
                {
                    num12 = 24;
                }
                else if (p_beanCreep.AssessmentMaterialID == 14)
                {
                    num12 = 26;
                }
                else if (p_beanCreep.AssessmentMaterialID == 15)
                {
                    num12 = 28;
                }
                else if (p_beanCreep.AssessmentMaterialID == 16)
                {
                    num12 = 30;
                }
                else if (p_beanCreep.AssessmentMaterialID == 17)
                {
                    num12 = 32;
                }
                else if (p_beanCreep.AssessmentMaterialID == 18)
                {
                    num12 = 34;
                }
                else if (p_beanCreep.AssessmentMaterialID == 19)
                {
                    num12 = 36;
                }
                else if (p_beanCreep.AssessmentMaterialID == 20)
                {
                    num12 = 38;
                }
                else if (p_beanCreep.AssessmentMaterialID == 21)
                {
                    num12 = 40;
                }
                else if (p_beanCreep.AssessmentMaterialID == 22)
                {
                    num12 = 42;
                }
                else if (p_beanCreep.AssessmentMaterialID == 23)
                {
                    num12 = 44;
                }
                else
                {
                    num12 = -1;
                }
                double[,] array4 = new double[,]
                {
                {
                    700.0,
                    735.0,
                    770.0,
                    805.0,
                    840.0,
                    875.0,
                    910.0,
                    945.0,
                    980.0,
                    1015.0,
                    1050.0,
                    1085.0,
                    1120.0,
                    1055.0
                },
                {
                    2.322094206,
                    2.265854763,
                    2.188189292,
                    2.084977322,
                    1.896603448,
                    1.629845616,
                    1.375572785,
                    1.209125069,
                    0.995357675,
                    0.843625353,
                    0.594929622,
                    0.509873115,
                    0.473209792,
                    0.425092004
                },
                {
                    700.0,
                    735.0,
                    770.0,
                    805.0,
                    840.0,
                    875.0,
                    910.0,
                    945.0,
                    980.0,
                    1015.0,
                    1050.0,
                    1085.0,
                    1120.0,
                    1055.0
                },
                {
                    2.180064199,
                    2.129941775,
                    1.918655934,
                    1.799211475,
                    1.548103454,
                    1.287969322,
                    1.146865988,
                    0.876840611,
                    0.830625869,
                    0.566517616,
                    0.501504062,
                    0.443418353,
                    0.408271129,
                    0.359708366
                },
                {
                    800.0,
                    835.0,
                    870.0,
                    905.0,
                    940.0,
                    975.0,
                    1010.0,
                    1045.0,
                    1080.0,
                    1115.0,
                    1150.0,
                    0.0,
                    0.0,
                    0.0
                },
                {
                    5.927210952,
                    5.556785981,
                    4.958620746,
                    5.140950013,
                    4.126701412,
                    3.470107592,
                    2.688874594,
                    1.880265996,
                    0.689193469,
                    0.451221645,
                    0.464091746,
                    0.0,
                    0.0,
                    0.0
                },
                {
                    850.0,
                    865.0,
                    880.0,
                    895.0,
                    910.0,
                    925.0,
                    940.0,
                    955.0,
                    970.0,
                    985.0,
                    1000.0,
                    1015.0,
                    1030.0,
                    1045.0
                },
                {
                    7.794182075,
                    8.024284162,
                    8.08648685,
                    8.278031844,
                    8.166069836,
                    8.372810058,
                    8.191260185,
                    8.58965225,
                    8.56851213,
                    8.778528081,
                    8.761743963,
                    9.239580582,
                    9.393939609,
                    9.10410268
                },
                {
                    800.0,
                    835.0,
                    870.0,
                    905.0,
                    940.0,
                    975.0,
                    1010.0,
                    1045.0,
                    1080.0,
                    1115.0,
                    1150.0,
                    1185.0,
                    1220.0,
                    1255.0
                },
                {
                    1.682411539,
                    1.640931644,
                    1.425671057,
                    1.377184865,
                    1.2981444,
                    1.256807602,
                    1.176849538,
                    1.134828252,
                    0.930335347,
                    0.814124718,
                    0.733587786,
                    0.679140734,
                    0.628763434,
                    0.565275896
                },
                {
                    800.0,
                    820.0,
                    840.0,
                    860.0,
                    880.0,
                    900.0,
                    920.0,
                    940.0,
                    960.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0
                },
                {
                    3.00494083,
                    3.046006397,
                    3.221245859,
                    3.27453544,
                    3.326413326,
                    3.412521802,
                    3.591251254,
                    3.613873678,
                    3.620372822,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0
                },
                {
                    800.0,
                    835.0,
                    870.0,
                    905.0,
                    940.0,
                    975.0,
                    1010.0,
                    1045.0,
                    1080.0,
                    1115.0,
                    1150.0,
                    1185.0,
                    1220.0,
                    1255.0
                },
                {
                    2.828546677,
                    2.652963826,
                    2.51682445,
                    2.414001104,
                    2.35767027,
                    2.15262784,
                    2.103576593,
                    1.962102719,
                    1.781798287,
                    1.772048781,
                    1.652175241,
                    1.556070922,
                    1.540248381,
                    1.503923339
                },
                {
                    800.0,
                    820.0,
                    840.0,
                    860.0,
                    880.0,
                    900.0,
                    920.0,
                    940.0,
                    960.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0
                },
                {
                    3.002381408,
                    3.051133382,
                    3.228313004,
                    3.288457583,
                    3.358438044,
                    3.439776672,
                    3.547056269,
                    3.575441971,
                    3.615106832,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0
                },
                {
                    800.0,
                    820.0,
                    840.0,
                    860.0,
                    880.0,
                    900.0,
                    920.0,
                    940.0,
                    960.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0
                },
                {
                    3.217353436,
                    3.313581739,
                    3.474392913,
                    3.549982659,
                    3.624480144,
                    3.751142682,
                    3.834353715,
                    3.934682381,
                    4.056104358,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0
                },
                {
                    800.0,
                    835.0,
                    870.0,
                    905.0,
                    940.0,
                    975.0,
                    1010.0,
                    1045.0,
                    1080.0,
                    1115.0,
                    1150.0,
                    1185.0,
                    1220.0,
                    1255.0
                },
                {
                    3.99250715,
                    4.025899877,
                    3.469481287,
                    2.85866136,
                    2.760070296,
                    2.194329087,
                    1.855169432,
                    1.800473666,
                    1.493552811,
                    1.24030738,
                    0.871368518,
                    0.876594693,
                    0.748565391,
                    0.640365949
                },
                {
                    800.0,
                    835.0,
                    870.0,
                    905.0,
                    940.0,
                    975.0,
                    1010.0,
                    1045.0,
                    1080.0,
                    1115.0,
                    1150.0,
                    1185.0,
                    1220.0,
                    1255.0
                },
                {
                    1.008738225,
                    0.944976517,
                    0.918691159,
                    0.87678069,
                    0.829166775,
                    0.778410068,
                    0.735545081,
                    0.695118608,
                    0.635281537,
                    0.620030032,
                    0.580455919,
                    0.540985462,
                    0.53990687,
                    0.532647799
                },
                {
                    925.0,
                    950.0,
                    975.0,
                    1000.0,
                    1025.0,
                    1050.0,
                    1075.0,
                    1100.0,
                    1125.0,
                    1150.0,
                    1175.0,
                    1200.0,
                    1225.0,
                    1250.0
                },
                {
                    5.158170773,
                    4.677788123,
                    4.284720477,
                    4.045811793,
                    3.740018627,
                    3.376340651,
                    3.060222955,
                    2.713143701,
                    2.62155954,
                    2.222181763,
                    2.16032084,
                    1.754199796,
                    1.623848204,
                    1.479347577
                },
                {
                    800.0,
                    835.0,
                    870.0,
                    905.0,
                    940.0,
                    975.0,
                    1010.0,
                    1045.0,
                    1080.0,
                    1115.0,
                    1150.0,
                    1185.0,
                    1220.0,
                    0.0
                },
                {
                    7.20240813,
                    7.10502581,
                    6.866953663,
                    6.304668485,
                    5.542277564,
                    4.647738909,
                    3.709480239,
                    3.064755603,
                    2.672349749,
                    1.218620133,
                    1.04037481,
                    0.791756734,
                    0.544073571,
                    0.0
                },
                {
                    1050.0,
                    1085.0,
                    1120.0,
                    1155.0,
                    1190.0,
                    1225.0,
                    1260.0,
                    1295.0,
                    1330.0,
                    1400.0,
                    1435.0,
                    1470.0,
                    1505.0,
                    0.0
                },
                {
                    5.817684104,
                    5.645353251,
                    5.291032997,
                    5.087448476,
                    4.268615394,
                    3.840908985,
                    3.01888751,
                    2.767856506,
                    1.844476562,
                    1.741343454,
                    1.562196652,
                    1.096223151,
                    0.886965541,
                    0.0
                },
                {
                    1025.0,
                    1095.0,
                    1140.0,
                    1185.0,
                    1230.0,
                    1275.0,
                    1320.0,
                    1365.0,
                    1410.0,
                    1455.0,
                    1500.0,
                    1545.0,
                    1590.0,
                    1635.0
                },
                {
                    9.095751469,
                    8.551094742,
                    7.762010099,
                    7.63766594,
                    6.481447052,
                    5.094984889,
                    3.862431409,
                    3.363207388,
                    2.7,
                    2.370965547,
                    1.97802937,
                    1.544240823,
                    1.418419435,
                    1.19560351
                },
                {
                    1025.0,
                    1060.0,
                    1095.0,
                    1130.0,
                    1165.0,
                    1200.0,
                    1235.0,
                    1270.0,
                    1305.0,
                    1340.0,
                    1375.0,
                    1410.0,
                    1445.0,
                    1480.0
                },
                {
                    2.61248419,
                    2.472723086,
                    2.425323679,
                    2.407152364,
                    2.118099953,
                    1.815981117,
                    1.681162357,
                    1.538041365,
                    1.413650476,
                    1.325625642,
                    1.066039395,
                    1.021953525,
                    0.876600201,
                    0.825634496
                },
                {
                    1025.0,
                    1060.0,
                    1095.0,
                    1130.0,
                    1165.0,
                    1200.0,
                    1235.0,
                    1270.0,
                    1305.0,
                    1340.0,
                    1375.0,
                    1410.0,
                    1445.0,
                    1480.0
                },
                {
                    2.871638154,
                    2.739841056,
                    2.62615098,
                    2.715380969,
                    2.598363132,
                    2.431484393,
                    2.051401728,
                    1.947924063,
                    1.807265896,
                    1.594101884,
                    1.479316683,
                    1.317471122,
                    1.276002595,
                    1.01008413
                },
                {
                    1025.0,
                    1060.0,
                    1095.0,
                    1130.0,
                    1165.0,
                    1200.0,
                    1235.0,
                    1270.0,
                    1305.0,
                    1340.0,
                    1375.0,
                    1410.0,
                    1445.0,
                    1480.0
                },
                {
                    2.859409325,
                    2.721381854,
                    2.634907253,
                    2.49578563,
                    2.364359552,
                    2.022275605,
                    1.843688711,
                    1.70416928,
                    1.572756392,
                    1.418324301,
                    1.278357024,
                    1.049684476,
                    1.005780782,
                    0.922797062
                },
                {
                    1025.0,
                    1060.0,
                    1095.0,
                    1130.0,
                    1165.0,
                    1200.0,
                    1235.0,
                    1270.0,
                    1305.0,
                    1340.0,
                    1375.0,
                    1410.0,
                    1445.0,
                    1480.0
                },
                {
                    2.697517269,
                    2.57750307,
                    2.55026866,
                    2.50093655,
                    2.404157162,
                    2.36906068,
                    1.949704111,
                    1.850903414,
                    1.711413169,
                    1.585306908,
                    1.429664582,
                    1.220765553,
                    1.177805155,
                    1.04084346
                },
                {
                    1050.0,
                    1100.0,
                    1150.0,
                    1200.0,
                    1250.0,
                    1300.0,
                    1350.0,
                    1400.0,
                    1450.0,
                    1500.0,
                    1550.0,
                    1600.0,
                    1650.0,
                    0.0
                },
                {
                    1.836648591,
                    1.679985338,
                    1.594020028,
                    1.453557231,
                    1.132060479,
                    1.030250961,
                    0.830387147,
                    0.628344371,
                    0.555006482,
                    0.526317789,
                    0.484593861,
                    0.394170997,
                    0.197386732,
                    0.0
                },
                {
                    1050.0,
                    1100.0,
                    1150.0,
                    1200.0,
                    1250.0,
                    1300.0,
                    1350.0,
                    1400.0,
                    1450.0,
                    1500.0,
                    1550.0,
                    1600.0,
                    1650.0,
                    0.0
                },
                {
                    1.960535561,
                    1.86939824,
                    1.758777155,
                    1.776935895,
                    1.591830201,
                    1.195458146,
                    1.082641791,
                    0.929510502,
                    0.681732103,
                    0.608320347,
                    0.563299242,
                    0.482692993,
                    0.440966546,
                    0.0
                },
                {
                    1050.0,
                    1100.0,
                    1150.0,
                    1200.0,
                    1250.0,
                    1300.0,
                    1350.0,
                    1400.0,
                    1450.0,
                    1500.0,
                    1550.0,
                    1600.0,
                    1650.0,
                    0.0
                },
                {
                    1.730525041,
                    1.612360981,
                    1.396382392,
                    1.344454768,
                    1.224417697,
                    1.029029003,
                    0.873655306,
                    0.739224516,
                    0.56919576,
                    0.525125213,
                    0.463278991,
                    0.332129127,
                    0.314658571,
                    0.0
                },
                {
                    1400.0,
                    1435.0,
                    1470.0,
                    1505.0,
                    1540.0,
                    1575.0,
                    1610.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0
                },
                {
                    0.937214632,
                    0.957412676,
                    0.783740626,
                    0.738927609,
                    0.69347159,
                    0.569649506,
                    0.527656202,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0
                }
                };
                double[,] array5 = new double[,]
                {
                {
                    700.0,
                    735.0,
                    770.0,
                    805.0,
                    840.0,
                    875.0,
                    910.0,
                    945.0,
                    980.0,
                    1015.0,
                    1050.0,
                    1085.0,
                    1120.0,
                    1055.0
                },
                {
                    51.01169792,
                    47.27992712,
                    43.00438115,
                    34.61067855,
                    34.61067855,
                    29.23334923,
                    24.53115023,
                    20.8651919,
                    17.27796095,
                    14.28190986,
                    10.84187742,
                    9.013595264,
                    8.042917076,
                    6.965620651
                },
                {
                    700.0,
                    735.0,
                    770.0,
                    805.0,
                    840.0,
                    875.0,
                    910.0,
                    945.0,
                    980.0,
                    1015.0,
                    1050.0,
                    1085.0,
                    1120.0,
                    1055.0
                },
                {
                    46.27374169,
                    42.82002004,
                    37.27137082,
                    33.51462123,
                    28.33861106,
                    23.40544372,
                    20.08657765,
                    15.76281719,
                    14.11871228,
                    10.42873363,
                    8.930471195,
                    7.585980568,
                    6.714134494,
                    5.720721826
                },
                {
                    800.0,
                    835.0,
                    870.0,
                    905.0,
                    940.0,
                    975.0,
                    1010.0,
                    1045.0,
                    1080.0,
                    1115.0,
                    1150.0,
                    0.0,
                    0.0,
                    0.0
                },
                {
                    115.1412386,
                    101.4547678,
                    85.78297011,
                    81.24681719,
                    64.96812259,
                    52.02165147,
                    39.39355119,
                    27.78246267,
                    14.27570883,
                    10.10117791,
                    9.73381108,
                    0.0,
                    0.0,
                    0.0
                },
                {
                    850.0,
                    865.0,
                    880.0,
                    895.0,
                    910.0,
                    925.0,
                    940.0,
                    955.0,
                    970.0,
                    985.0,
                    1000.0,
                    1015.0,
                    1030.0,
                    1045.0
                },
                {
                    149.6594286,
                    147.9089644,
                    143.0713062,
                    140.496535,
                    134.376906,
                    131.4670535,
                    124.6902956,
                    124.2754802,
                    118.8491136,
                    115.8006141,
                    110.9766865,
                    110.6312701,
                    106.995258,
                    99.66498053
                },
                {
                    800.0,
                    835.0,
                    870.0,
                    905.0,
                    940.0,
                    975.0,
                    1010.0,
                    1045.0,
                    1080.0,
                    1115.0,
                    1150.0,
                    1185.0,
                    1220.0,
                    1255.0
                },
                {
                    44.34647592,
                    40.86497069,
                    34.52710647,
                    31.79022825,
                    28.56129373,
                    26.12904523,
                    23.57110249,
                    21.45965822,
                    17.5608854,
                    14.96030765,
                    12.9518958,
                    11.2305992,
                    9.865889819,
                    8.417121457
                },
                {
                    800.0,
                    820.0,
                    840.0,
                    860.0,
                    880.0,
                    900.0,
                    920.0,
                    940.0,
                    960.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0
                },
                {
                    88.83308598,
                    86.54189541,
                    85.3573551,
                    83.51876769,
                    81.08151639,
                    79.08773032,
                    78.31369045,
                    75.60495657,
                    72.81448334,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0
                },
                {
                    800.0,
                    835.0,
                    870.0,
                    905.0,
                    940.0,
                    975.0,
                    1010.0,
                    1045.0,
                    1080.0,
                    1115.0,
                    1150.0,
                    1185.0,
                    1220.0,
                    1255.0
                },
                {
                    72.37950599,
                    66.02778329,
                    59.70721693,
                    54.80790117,
                    50.75532368,
                    44.66230344,
                    41.1919032,
                    36.783144,
                    31.89940854,
                    29.30784525,
                    25.52623178,
                    22.23367633,
                    20.29574613,
                    18.05805329
                },
                {
                    800.0,
                    820.0,
                    840.0,
                    860.0,
                    880.0,
                    900.0,
                    920.0,
                    940.0,
                    960.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0
                },
                {
                    88.8606206,
                    86.72156178,
                    85.54563763,
                    83.94308124,
                    81.5700094,
                    79.58769404,
                    78.05931195,
                    75.44915998,
                    72.73369353,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0
                },
                {
                    800.0,
                    820.0,
                    840.0,
                    860.0,
                    880.0,
                    900.0,
                    920.0,
                    940.0,
                    960.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0
                },
                {
                    106.1644993,
                    104.1078968,
                    102.3982512,
                    100.0893479,
                    97.51183282,
                    95.55963666,
                    93.05709161,
                    90.76161287,
                    88.66471952,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0
                },
                {
                    800.0,
                    835.0,
                    870.0,
                    905.0,
                    940.0,
                    975.0,
                    1010.0,
                    1045.0,
                    1080.0,
                    1115.0,
                    1150.0,
                    1185.0,
                    1220.0,
                    1255.0
                },
                {
                    89.44698692,
                    84.18967426,
                    70.80826475,
                    59.05410227,
                    53.53058341,
                    42.67914723,
                    35.88768608,
                    32.69854451,
                    27.07355252,
                    21.71610536,
                    16.15872065,
                    15.07179241,
                    12.29131698,
                    10.04063041
                },
                {
                    800.0,
                    835.0,
                    870.0,
                    905.0,
                    940.0,
                    975.0,
                    1010.0,
                    1045.0,
                    1080.0,
                    1115.0,
                    1150.0,
                    1185.0,
                    1220.0,
                    1255.0
                },
                {
                    23.67490672,
                    21.91594,
                    20.50723745,
                    19.13845639,
                    17.7948108,
                    16.32995059,
                    15.14348222,
                    14.03100771,
                    12.6641454,
                    11.85328839,
                    10.88818179,
                    9.773125433,
                    9.317091476,
                    8.720490201
                },
                {
                    925.0,
                    950.0,
                    975.0,
                    1000.0,
                    1025.0,
                    1050.0,
                    1075.0,
                    1100.0,
                    1125.0,
                    1150.0,
                    1175.0,
                    1200.0,
                    1225.0,
                    1250.0
                },
                {
                    124.716376,
                    111.62186,
                    99.39617145,
                    90.73664738,
                    80.75590031,
                    71.22488706,
                    63.13359223,
                    54.7883205,
                    49.94276591,
                    42.06453541,
                    38.29810658,
                    30.94197198,
                    26.90697284,
                    23.06261463
                },
                {
                    800.0,
                    835.0,
                    870.0,
                    905.0,
                    940.0,
                    975.0,
                    1010.0,
                    1045.0,
                    1080.0,
                    1115.0,
                    1150.0,
                    1185.0,
                    1220.0,
                    0.0
                },
                {
                    179.7044895,
                    163.1331359,
                    145.8116896,
                    125.9767085,
                    104.9972383,
                    84.19880643,
                    66.20228253,
                    51.45387574,
                    41.67152419,
                    22.75388064,
                    18.37346761,
                    13.53380985,
                    9.341351573,
                    0.0
                },
                {
                    1050.0,
                    1085.0,
                    1120.0,
                    1155.0,
                    1190.0,
                    1225.0,
                    1260.0,
                    1295.0,
                    1330.0,
                    1400.0,
                    1435.0,
                    1470.0,
                    1505.0,
                    0.0
                },
                {
                    113.0509876,
                    104.7857438,
                    94.48060783,
                    87.44108384,
                    73.00151607,
                    63.05395409,
                    50.25860034,
                    44.19844365,
                    31.22976924,
                    28.19590987,
                    24.24813697,
                    16.10815303,
                    12.67764296,
                    0.0
                },
                {
                    1025.0,
                    1095.0,
                    1140.0,
                    1185.0,
                    1230.0,
                    1275.0,
                    1320.0,
                    1365.0,
                    1410.0,
                    1455.0,
                    1500.0,
                    1545.0,
                    1590.0,
                    1635.0
                },
                {
                    178.4903806,
                    159.3425896,
                    137.8359794,
                    128.1338333,
                    105.0932718,
                    81.62949245,
                    62.28920056,
                    51.57482603,
                    39.80280589,
                    33.20718954,
                    26.39424843,
                    19.75157593,
                    17.02843,
                    13.64428917
                },
                {
                    1025.0,
                    1060.0,
                    1095.0,
                    1130.0,
                    1165.0,
                    1200.0,
                    1235.0,
                    1270.0,
                    1305.0,
                    1340.0,
                    1375.0,
                    1410.0,
                    1445.0,
                    1480.0
                },
                {
                    51.89667578,
                    47.27449661,
                    43.8027278,
                    41.46028375,
                    35.64877377,
                    30.06248392,
                    26.60844559,
                    23.1934253,
                    20.37837991,
                    18.06331153,
                    14.13600543,
                    12.74569982,
                    10.46106146,
                    9.223162011
                },
                {
                    1025.0,
                    1060.0,
                    1095.0,
                    1130.0,
                    1165.0,
                    1200.0,
                    1235.0,
                    1270.0,
                    1305.0,
                    1340.0,
                    1375.0,
                    1410.0,
                    1445.0,
                    1480.0
                },
                {
                    59.236613,
                    54.44902091,
                    49.95242053,
                    48.82635281,
                    44.92414818,
                    40.5304806,
                    34.09630984,
                    30.78279228,
                    27.12329529,
                    23.17777945,
                    20.55174158,
                    17.44057234,
                    16.05051643,
                    12.42369778
                },
                {
                    1025.0,
                    1060.0,
                    1095.0,
                    1130.0,
                    1165.0,
                    1200.0,
                    1235.0,
                    1270.0,
                    1305.0,
                    1340.0,
                    1375.0,
                    1410.0,
                    1445.0,
                    1480.0
                },
                {
                    54.48704474,
                    50.08008854,
                    46.19474965,
                    42.27622084,
                    38.12493889,
                    32.25517003,
                    28.25756524,
                    24.85006121,
                    21.90793622,
                    18.89693271,
                    16.293461,
                    12.8810898,
                    11.72895372,
                    10.07228101
                },
                {
                    1025.0,
                    1060.0,
                    1095.0,
                    1130.0,
                    1165.0,
                    1200.0,
                    1235.0,
                    1270.0,
                    1305.0,
                    1340.0,
                    1375.0,
                    1410.0,
                    1445.0,
                    1480.0
                },
                {
                    55.9166786,
                    51.53986482,
                    48.30746985,
                    45.32140391,
                    42.11299255,
                    39.15157194,
                    32.55225232,
                    29.5369861,
                    26.18040379,
                    23.18640013,
                    20.06126481,
                    16.54378096,
                    15.1217199,
                    12.81767578
                },
                {
                    1050.0,
                    1100.0,
                    1150.0,
                    1200.0,
                    1250.0,
                    1300.0,
                    1350.0,
                    1400.0,
                    1450.0,
                    1500.0,
                    1550.0,
                    1600.0,
                    1650.0,
                    0.0
                },
                {
                    39.07073834,
                    34.38203782,
                    30.66326746,
                    26.92230739,
                    21.23926549,
                    18.25436192,
                    14.57883714,
                    11.42955995,
                    9.739299791,
                    8.540086181,
                    7.456659608,
                    5.90194564,
                    3.716302801,
                    0.0
                },
                {
                    1050.0,
                    1100.0,
                    1150.0,
                    1200.0,
                    1250.0,
                    1300.0,
                    1350.0,
                    1400.0,
                    1450.0,
                    1500.0,
                    1550.0,
                    1600.0,
                    1650.0,
                    0.0
                },
                {
                    43.64144633,
                    39.6164931,
                    35.03466643,
                    33.05282868,
                    28.73607394,
                    22.06404988,
                    19.12969896,
                    15.76091755,
                    12.13837244,
                    10.35703895,
                    9.142218475,
                    7.570514497,
                    6.543399185,
                    0.0
                },
                {
                    1050.0,
                    1100.0,
                    1150.0,
                    1200.0,
                    1250.0,
                    1300.0,
                    1350.0,
                    1400.0,
                    1450.0,
                    1500.0,
                    1550.0,
                    1600.0,
                    1650.0,
                    0.0
                },
                {
                    41.24545077,
                    36.50165796,
                    30.64345132,
                    27.93026902,
                    24.37455905,
                    20.24751973,
                    17.10179209,
                    14.06781462,
                    11.08375235,
                    9.691352938,
                    8.224558112,
                    6.139914362,
                    5.479356899,
                    0.0
                },
                {
                    1400.0,
                    1435.0,
                    1470.0,
                    1505.0,
                    1540.0,
                    1575.0,
                    1610.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0
                },
                {
                    16.17736925,
                    15.77769489,
                    13.24890345,
                    12.21603936,
                    11.17727678,
                    9.420183893,
                    8.525903176,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0,
                    0.0
                }
                };
                double[] array6 = new double[array4.GetLength(1)];
                for (int i = 0; i < array4.GetLength(1); i++)
                {
                    array6[i] = array4[num12, i];
                }
                double[] array7 = new double[array4.GetLength(1)];
                for (int i = 0; i < array4.GetLength(1); i++)
                {
                    array7[i] = array4[num12 + 1, i];
                }
                double[] array8 = new double[array4.GetLength(1)];
                for (int i = 0; i < array4.GetLength(1); i++)
                {
                    array8[i] = array5[num12 + 1, i];
                }
                int num13 = 0;
                for (int i = 0; i <= array4.GetLength(1) - 2; i++)
                {
                    if (num4 > array6[i] && num4 <= array6[i + 1])
                    {
                        num13 = i + 1;
                        break;
                    }
                    num13 = i + 1;
                }
                int num14 = num13 - 1;
                double num15 = array6[num14];
                double num16 = array6[num13];
                double num17 = Math.Exp((num5 - array8[num14]) / array7[num14]);
                double num18 = Math.Exp((num5 - array8[num13]) / array7[num13]);
                if (!p_beanCreep.AutomaticallyCalculationTheCreepDamageRate.Value)
                {
                    beanCreepResult.rc_5 = p_beanCreep.TheCreepDamageRate;
                }
                else
                {
                    beanCreepResult.rc_5 = new double?(num17 + (num18 - num17) / (num16 - num15) * (num4 - num15));
                }
                BeanCreepResult arg_179D_0 = beanCreepResult;
                num = beanCreepResult.rc_5;
                num2 = beanCreepResult.toper_1;
                arg_179D_0.dc_5 = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() * num2.GetValueOrDefault()) : null);
                num = beanCreepResult.dc_5;
                if (num.GetValueOrDefault() <= 0.25 && num.HasValue)
                {
                    beanCreepResult.checkDc_6 = new bool?(true);
                }
                else
                {
                    beanCreepResult.checkDc_6 = new bool?(false);
                }
                if (beanCreepResult.checkTPermit_3.Value && beanCreepResult.checkDc_6.Value)
                {
                    beanCreepResult.result = "The Level 1 assessment criteria are satisfied.";
                    beanCreepResult.resultBool = true;
                }
                else
                {
                    beanCreepResult.result = "The Level 1 assessment criteria are not satisfied.";
                    beanCreepResult.resultBool = false;
                }
                return beanCreepResult;
            }
        }


    }
}
