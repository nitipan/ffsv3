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
    public class HydrogenModule : Nancy.NancyModule
    {
        public HydrogenModule() : base("/api/hydrogen")
        {
            Post["/calculation/level{level}/unit{unit}"] = x =>
            {

                var level = (int)x.level;
                var unit = (int)x.unit;

                HydrogenCalculator calculator = new HydrogenCalculator(new GlobalVar(unit));

                var input = this.Bind<BeanHydrogen>();

                var result = level == 1 ? calculator.calculateLevel1(input) : calculator.calculateLevel2(input);

                return Response.AsJson(result);
            };

        }

        internal class HydrogenCalculator : CalculatorBase
        {
            private static object[,] resultDataGrid = new object[17, 4];
            public HydrogenCalculator(GlobalVar var) : base(var)
            {

            }
            public BeanHydrogenResult calculateLevel1(BeanHydrogen p_beanHydrogen)
            {
                BeanHydrogenResult beanHydrogenResult = new BeanHydrogenResult();
                beanHydrogenResult.D = p_beanHydrogen.insideDiameter.Value;
                beanHydrogenResult.FCA = p_beanHydrogen.fca.Value;
                beanHydrogenResult.LOSS = p_beanHydrogen.loss.Value;
                beanHydrogenResult.tnom = p_beanHydrogen.nominalThickness.Value;
                beanHydrogenResult.tc = beanHydrogenResult.tnom - beanHydrogenResult.FCA - beanHydrogenResult.LOSS;
                beanHydrogenResult.Planar = new bool[p_beanHydrogen.HydrogenItem.Length];
                beanHydrogenResult.Flaw = new bool[p_beanHydrogen.HydrogenItem.Length];
                beanHydrogenResult.HICType = new string[p_beanHydrogen.HydrogenItem.Length];
                beanHydrogenResult.Facility = new bool[p_beanHydrogen.HydrogenItem.Length];
                for (int i = 0; i < p_beanHydrogen.HydrogenItem.Length; i++)
                {
                    BeanHydrogenItem beanHydrogenItem = p_beanHydrogen.HydrogenItem[i];
                    double num = beanHydrogenItem.DirectionCircumferential.Value;
                    double num2 = beanHydrogenItem.DirectionLongitudinal.Value;
                    if (num2 <= 0.6 * Math.Sqrt(beanHydrogenResult.D * beanHydrogenResult.tc) && num <= 0.6 * Math.Sqrt(beanHydrogenResult.D * beanHydrogenResult.tc))
                    {
                        beanHydrogenResult.Planar[i] = true;
                    }
                    else
                    {
                        beanHydrogenResult.Planar[i] = false;
                    }
                    double? num3;
                    double num4;
                    bool arg_1B5_0;
                    if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_SI)
                    {
                        num3 = beanHydrogenItem.HICDepth;
                        num4 = Math.Min(beanHydrogenResult.tc / 3.0, 13.0);
                        arg_1B5_0 = (num3.GetValueOrDefault() > num4 || !num3.HasValue);
                    }
                    else
                    {
                        arg_1B5_0 = true;
                    }
                    if (!arg_1B5_0)
                    {
                        beanHydrogenResult.Flaw[i] = true;
                    }
                    else
                    {
                        bool arg_219_0;
                        if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_MATRIC)
                        {
                            num3 = beanHydrogenItem.HICDepth;
                            num4 = Math.Min(beanHydrogenResult.tc / 3.0, 0.5);
                            arg_219_0 = (num3.GetValueOrDefault() > num4 || !num3.HasValue);
                        }
                        else
                        {
                            arg_219_0 = true;
                        }
                        if (!arg_219_0)
                        {
                            beanHydrogenResult.Flaw[i] = true;
                        }
                        else
                        {
                            beanHydrogenResult.Flaw[i] = false;
                        }
                    }
                    num3 = beanHydrogenItem.InternalSurface;
                    num4 = 0.2 * beanHydrogenResult.tc;
                    bool arg_29F_0;
                    if (num3.GetValueOrDefault() >= num4 && num3.HasValue)
                    {
                        num3 = beanHydrogenItem.ExternalSurface;
                        num4 = 0.2 * beanHydrogenResult.tc;
                        arg_29F_0 = (num3.GetValueOrDefault() < num4 || !num3.HasValue);
                    }
                    else
                    {
                        arg_29F_0 = true;
                    }
                    if (!arg_29F_0)
                    {
                        beanHydrogenResult.HICType[i] = "Subsurface HIC";
                    }
                    else
                    {
                        beanHydrogenResult.HICType[i] = "Surface Breaking HIC";
                    }
                    bool arg_359_0;
                    if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_SI)
                    {
                        num3 = beanHydrogenItem.WeldJoint;
                        num4 = Math.Max(2.0 * beanHydrogenResult.tc, 25.0);
                        if (num3.GetValueOrDefault() > num4 && num3.HasValue)
                        {
                            num3 = beanHydrogenItem.MajorStructuralDiscontinuity;
                            num4 = 1.8 * Math.Sqrt(beanHydrogenResult.D * beanHydrogenResult.tc);
                            arg_359_0 = (num3.GetValueOrDefault() < num4 || !num3.HasValue);
                        }
                        else
                        {
                            arg_359_0 = true;
                        }
                    }
                    else
                    {
                        arg_359_0 = true;
                    }
                    if (!arg_359_0)
                    {
                        beanHydrogenResult.Facility[i] = true;
                    }
                    else
                    {
                        bool arg_403_0;
                        if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_MATRIC)
                        {
                            num3 = beanHydrogenItem.WeldJoint;
                            num4 = Math.Max(2.0 * beanHydrogenResult.tc, 1.0);
                            if (num3.GetValueOrDefault() > num4 && num3.HasValue)
                            {
                                num3 = beanHydrogenItem.MajorStructuralDiscontinuity;
                                num4 = 1.8 * Math.Sqrt(beanHydrogenResult.D * beanHydrogenResult.tc);
                                arg_403_0 = (num3.GetValueOrDefault() < num4 || !num3.HasValue);
                            }
                            else
                            {
                                arg_403_0 = true;
                            }
                        }
                        else
                        {
                            arg_403_0 = true;
                        }
                        if (!arg_403_0)
                        {
                            beanHydrogenResult.Facility[i] = true;
                        }
                        else
                        {
                            beanHydrogenResult.Facility[i] = false;
                        }
                    }
                }
                beanHydrogenResult.CheckFlaw = true;
                beanHydrogenResult.CheckFacility = true;
                beanHydrogenResult.CheckPlanar = true;
                beanHydrogenResult.CheckHICType = true;
                for (int i = 0; i < p_beanHydrogen.HydrogenItem.Length; i++)
                {
                    if (!beanHydrogenResult.Flaw[i])
                    {
                        beanHydrogenResult.CheckFlaw = false;
                        break;
                    }
                }
                for (int i = 0; i < p_beanHydrogen.HydrogenItem.Length; i++)
                {
                    if (!beanHydrogenResult.Facility[i])
                    {
                        beanHydrogenResult.CheckFacility = false;
                        break;
                    }
                }
                for (int i = 0; i < p_beanHydrogen.HydrogenItem.Length; i++)
                {
                    if (!beanHydrogenResult.Planar[i])
                    {
                        beanHydrogenResult.CheckPlanar = false;
                        break;
                    }
                }
                for (int i = 0; i < p_beanHydrogen.HydrogenItem.Length; i++)
                {
                    if (beanHydrogenResult.HICType[i].CompareTo("Surface Breaking HIC") == 0)
                    {
                        beanHydrogenResult.CheckHICType = false;
                        break;
                    }
                }
                if (!beanHydrogenResult.CheckFlaw || !beanHydrogenResult.CheckFacility || !beanHydrogenResult.CheckPlanar || !beanHydrogenResult.CheckHICType)
                {
                    beanHydrogenResult.Result = "The Level 1 assessment criteria are not satisfied.";
                    beanHydrogenResult.ResultBool = false;
                }
                else
                {
                    beanHydrogenResult.Result = "The Level 1 assessment criteria are satisfied.";
                    beanHydrogenResult.ResultBool = true;
                }
                return beanHydrogenResult;
            }

            public BeanHydrogenResult calculateLevel2(BeanHydrogen p_beanHydrogen)
            {
                BeanHydrogenResult beanHydrogenResult = new BeanHydrogenResult();
                beanHydrogenResult.D = p_beanHydrogen.insideDiameter.Value;
                beanHydrogenResult.FCA = p_beanHydrogen.fca.Value;
                beanHydrogenResult.LOSS = p_beanHydrogen.loss.Value;
                beanHydrogenResult.tnom = p_beanHydrogen.nominalThickness.Value;
                beanHydrogenResult.tc = beanHydrogenResult.tnom - beanHydrogenResult.FCA - beanHydrogenResult.LOSS;
                beanHydrogenResult.Planar = new bool[p_beanHydrogen.HydrogenItem.Length];
                beanHydrogenResult.Flaw = new bool[p_beanHydrogen.HydrogenItem.Length];
                beanHydrogenResult.HICType = new string[p_beanHydrogen.HydrogenItem.Length];
                beanHydrogenResult.Facility = new bool[p_beanHydrogen.HydrogenItem.Length];
                beanHydrogenResult.Lampda = new double[p_beanHydrogen.HydrogenItem.Length];
                beanHydrogenResult.Mt = new double[p_beanHydrogen.HydrogenItem.Length];
                beanHydrogenResult.RSF = new double[p_beanHydrogen.HydrogenItem.Length];
                for (int i = 0; i < p_beanHydrogen.HydrogenItem.Length; i++)
                {
                    BeanHydrogenItem beanHydrogenItem = p_beanHydrogen.HydrogenItem[i];
                    double? num;
                    double num2;
                    bool arg_1AF_0;
                    if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_SI)
                    {
                        num = beanHydrogenItem.WeldJoint;
                        num2 = Math.Max(2.0 * beanHydrogenResult.tc, 25.0);
                        if (num.GetValueOrDefault() > num2 && num.HasValue)
                        {
                            num = beanHydrogenItem.MajorStructuralDiscontinuity;
                            num2 = 1.8 * Math.Sqrt(beanHydrogenResult.D * beanHydrogenResult.tc);
                            arg_1AF_0 = (num.GetValueOrDefault() < num2 || !num.HasValue);
                        }
                        else
                        {
                            arg_1AF_0 = true;
                        }
                    }
                    else
                    {
                        arg_1AF_0 = true;
                    }
                    if (!arg_1AF_0)
                    {
                        beanHydrogenResult.Facility[i] = true;
                    }
                    else
                    {
                        bool arg_259_0;
                        if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_MATRIC)
                        {
                            num = beanHydrogenItem.WeldJoint;
                            num2 = Math.Max(2.0 * beanHydrogenResult.tc, 1.0);
                            if (num.GetValueOrDefault() > num2 && num.HasValue)
                            {
                                num = beanHydrogenItem.MajorStructuralDiscontinuity;
                                num2 = 1.8 * Math.Sqrt(beanHydrogenResult.D * beanHydrogenResult.tc);
                                arg_259_0 = (num.GetValueOrDefault() < num2 || !num.HasValue);
                            }
                            else
                            {
                                arg_259_0 = true;
                            }
                        }
                        else
                        {
                            arg_259_0 = true;
                        }
                        if (!arg_259_0)
                        {
                            beanHydrogenResult.Facility[i] = true;
                        }
                        else
                        {
                            beanHydrogenResult.Facility[i] = false;
                        }
                    }
                    bool arg_2C8_0;
                    if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_SI)
                    {
                        num = beanHydrogenItem.HICDepth;
                        num2 = Math.Min(beanHydrogenResult.tc / 3.0, 13.0);
                        arg_2C8_0 = (num.GetValueOrDefault() > num2 || !num.HasValue);
                    }
                    else
                    {
                        arg_2C8_0 = true;
                    }
                    if (!arg_2C8_0)
                    {
                        beanHydrogenResult.Flaw[i] = true;
                    }
                    else
                    {
                        bool arg_32C_0;
                        if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_MATRIC)
                        {
                            num = beanHydrogenItem.HICDepth;
                            num2 = Math.Min(beanHydrogenResult.tc / 3.0, 0.5);
                            arg_32C_0 = (num.GetValueOrDefault() > num2 || !num.HasValue);
                        }
                        else
                        {
                            arg_32C_0 = true;
                        }
                        if (!arg_32C_0)
                        {
                            beanHydrogenResult.Flaw[i] = true;
                        }
                        else
                        {
                            beanHydrogenResult.Flaw[i] = false;
                        }
                    }
                    num = beanHydrogenItem.InternalSurface;
                    num2 = 0.2 * beanHydrogenResult.tc;
                    bool arg_3B2_0;
                    if (num.GetValueOrDefault() >= num2 && num.HasValue)
                    {
                        num = beanHydrogenItem.ExternalSurface;
                        num2 = 0.2 * beanHydrogenResult.tc;
                        arg_3B2_0 = (num.GetValueOrDefault() < num2 || !num.HasValue);
                    }
                    else
                    {
                        arg_3B2_0 = true;
                    }
                    if (!arg_3B2_0)
                    {
                        beanHydrogenResult.HICType[i] = "Subsurface HIC";
                    }
                    else
                    {
                        beanHydrogenResult.HICType[i] = "Surface Breaking HIC";
                    }
                    double num3;
                    if (!p_beanHydrogen.automaticallyCalculationAllowableStress.Value)
                    {
                        num3 = p_beanHydrogen.allowableStress.Value;
                    }
                    else
                    {
                        num3 = Math.Min(0.24285714285714285 * p_beanHydrogen.ultimatedTensileStrength.Value, 0.56666666666666665 * p_beanHydrogen.yieldStrength.Value);
                    }
                    double num4 = p_beanHydrogen.insideDiameter.Value / 2.0;
                    beanHydrogenResult.MAWP = num3 * p_beanHydrogen.weldJointEfficiency.Value * beanHydrogenResult.tc / (num4 + 0.6 * beanHydrogenResult.tc);
                    beanHydrogenResult.DH = 0.8;
                    double num5 = beanHydrogenItem.DirectionLongitudinal.Value;
                    beanHydrogenResult.Lampda[i] = 1.285 * num5 / Math.Sqrt(beanHydrogenResult.D * beanHydrogenResult.tc);
                    double num6;
                    if (p_beanHydrogen.componentTypeID == 1 || p_beanHydrogen.componentTypeID == 4)
                    {
                        num6 = 1.0;
                    }
                    else
                    {
                        num6 = 2.0;
                    }
                    if (num6 == 1.0)
                    {
                        beanHydrogenResult.Mt[i] = 1.001 - 0.014195 * beanHydrogenResult.Lampda[i] + 0.2909 * Math.Pow(beanHydrogenResult.Lampda[i], 2.0) - 0.09642 * Math.Pow(beanHydrogenResult.Lampda[i], 3.0) + 0.02089 * Math.Pow(beanHydrogenResult.Lampda[i], 4.0) - 0.003054 * Math.Pow(beanHydrogenResult.Lampda[i], 5.0) + 2.957 * Math.Pow(10.0, -4.0) * Math.Pow(beanHydrogenResult.Lampda[i], 6.0) - 1.8462 * Math.Pow(10.0, -5.0) * Math.Pow(beanHydrogenResult.Lampda[i], 7.0) + 7.1553 * Math.Pow(10.0, -7.0) * Math.Pow(beanHydrogenResult.Lampda[i], 8.0) - 1.5631 * Math.Pow(10.0, -8.0) * Math.Pow(beanHydrogenResult.Lampda[i], 9.0) + 1.4656 * Math.Pow(10.0, -10.0) * Math.Pow(beanHydrogenResult.Lampda[i], 10.0);
                    }
                    else if (num6 == 2.0)
                    {
                        beanHydrogenResult.Mt[i] = (1.0005 + 0.49001 * beanHydrogenResult.Lampda[i] + 0.32409 * Math.Pow(beanHydrogenResult.Lampda[i], 2.0)) / (1.0 + 0.50144 * beanHydrogenResult.Lampda[i] - 0.011067 * Math.Pow(beanHydrogenResult.Lampda[i], 2.0));
                    }
                    if (beanHydrogenResult.Mt[i] >= 20.0)
                    {
                        beanHydrogenResult.Mt[i] = 20.0;
                    }
                    if (beanHydrogenResult.HICType[i].CompareTo("Subsurface HIC") == 0)
                    {
                        beanHydrogenResult.RSF[i] = (1.0 - beanHydrogenItem.HICDepth.Value * beanHydrogenResult.DH / beanHydrogenResult.tc) / (1.0 - 1.0 / beanHydrogenResult.Mt[i] * (beanHydrogenItem.HICDepth.Value * beanHydrogenResult.DH / beanHydrogenResult.tc));
                    }
                    else
                    {
                        beanHydrogenResult.RSF[i] = (2.0 * Math.Min(beanHydrogenItem.SpacingToNearestHIC.Value / 2.0, 8.0 * beanHydrogenResult.tc) + num5 * (1.0 - beanHydrogenItem.HICDepth.Value * beanHydrogenResult.DH / beanHydrogenResult.tc)) / (2.0 * Math.Min(beanHydrogenItem.SpacingToNearestHIC.Value / 2.0, 8.0 * beanHydrogenResult.tc) + num5);
                    }
                    beanHydrogenResult.RSF[i] = Math.Min(beanHydrogenResult.RSF[i], 1.0);
                }
                beanHydrogenResult.CheckFlaw = true;
                beanHydrogenResult.CheckFacility = true;
                beanHydrogenResult.CheckRSF = true;
                for (int i = 0; i < p_beanHydrogen.HydrogenItem.Length; i++)
                {
                    if (!beanHydrogenResult.Flaw[i])
                    {
                        beanHydrogenResult.CheckFlaw = false;
                        break;
                    }
                }
                for (int i = 0; i < p_beanHydrogen.HydrogenItem.Length; i++)
                {
                    if (!beanHydrogenResult.Facility[i])
                    {
                        beanHydrogenResult.CheckFacility = false;
                        break;
                    }
                }
                for (int i = 0; i < p_beanHydrogen.HydrogenItem.Length; i++)
                {
                    double num2 = beanHydrogenResult.RSF[i];
                    double? num = p_beanHydrogen.allowRSF;
                    if (num2 >= num.GetValueOrDefault() && num.HasValue)
                    {
                        beanHydrogenResult.CheckRSF = false;
                        break;
                    }
                }
                if (!beanHydrogenResult.CheckFlaw || !beanHydrogenResult.CheckFacility || !beanHydrogenResult.CheckRSF)
                {
                    beanHydrogenResult.Result = "The Level 2 assessment criteria are not satisfied.";
                    beanHydrogenResult.ResultBool = false;
                }
                else
                {
                    beanHydrogenResult.Result = "The Level 2 assessment criteria are satisfied.";
                    beanHydrogenResult.ResultBool = true;
                }
                return beanHydrogenResult;
            }
        }

    }
}
