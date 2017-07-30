using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nancy.Extensions;
using Nancy.ModelBinding;
using ffs.api.Model;
using System.Globalization;

namespace ffs.api
{
    public class LocalMetalLossModule : NancyModule
    {
        public LocalMetalLossModule() : base("/api/localmetalloss")
        {
            Post["/calculation/level{level}/unit{unit}"] = x =>
            {
                var level = (int)x.level;
                var unit = (int)x.unit;
                
                var input = this.Bind<BeanLocalMetalLoss>();

                LocalMetalLossCalculator calculator = new api.LocalMetalLossModule.LocalMetalLossCalculator(new GlobalVar(unit));

                var result = level == 1 ? calculator.calculateLevel1(input) : calculator.calculateLevel2(input);

                return Response.AsJson(result);
            };
        }
        
        internal class LocalMetalLossCalculator : CalculatorBase
        {
            public LocalMetalLossCalculator(GlobalVar var) : base(var)
            {

            }
            public  BeanLocalMetalLossResult calculateLevel1(BeanLocalMetalLoss p_beanLocalMetalLoss)
            {
                BeanLocalMetalLossResult beanLocalMetalLossResult = new BeanLocalMetalLossResult();
                beanLocalMetalLossResult.tmm = new double?(this.getMinData(p_beanLocalMetalLoss.inspectionGridData, p_beanLocalMetalLoss.numberOfInspectionColumn.Value, p_beanLocalMetalLoss.numberOfInspectionRow.Value));
                double[] minLongitudinalDataUpperLower = this.getMinLongitudinalDataUpperLower(p_beanLocalMetalLoss.inspectionGridData, p_beanLocalMetalLoss.numberOfInspectionColumn.Value, p_beanLocalMetalLoss.numberOfInspectionRow.Value);
                beanLocalMetalLossResult.Xi = new double?(minLongitudinalDataUpperLower[0]);
                BeanLocalMetalLossResult arg_F2_0 = beanLocalMetalLossResult;
                double num = p_beanLocalMetalLoss.nominalThickness.Value;
                double? num2 = p_beanLocalMetalLoss.loss;
                num2 = (num2.HasValue ? new double?(num - num2.GetValueOrDefault()) : null);
                double? num3 = p_beanLocalMetalLoss.fca;
                arg_F2_0.tc = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() - num3.GetValueOrDefault()) : null);
                BeanLocalMetalLossResult arg_13A_0 = beanLocalMetalLossResult;
                num = (double)(p_beanLocalMetalLoss.numberOfInspectionRow.Value - 1);
                num2 = p_beanLocalMetalLoss.widthOfTheLongGrid;
                arg_13A_0.s = (num2.HasValue ? new double?(num * num2.GetValueOrDefault()) : null);
                BeanLocalMetalLossResult arg_1C1_0 = beanLocalMetalLossResult;
                num2 = beanLocalMetalLossResult.tmm;
                num3 = p_beanLocalMetalLoss.fca;
                num2 = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() - num3.GetValueOrDefault()) : null);
                num3 = beanLocalMetalLossResult.tc;
                arg_1C1_0.rt = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() / num3.GetValueOrDefault()) : null);
                BeanLocalMetalLossResult arg_24B_0 = beanLocalMetalLossResult;
                num2 = beanLocalMetalLossResult.s;
                num2 = (num2.HasValue ? new double?(1.285 * num2.GetValueOrDefault()) : null);
                num = Math.Sqrt(p_beanLocalMetalLoss.insideDiameter.Value * beanLocalMetalLossResult.tc.Value);
                arg_24B_0.lampda = (num2.HasValue ? new double?(num2.GetValueOrDefault() / num) : null);
                num2 = beanLocalMetalLossResult.lampda;
                if (num2.GetValueOrDefault() >= 20.0 && num2.HasValue)
                {
                    beanLocalMetalLossResult.lampda = new double?(20.0);
                }
                BeanLocalMetalLossResult arg_2C7_0 = beanLocalMetalLossResult;
                num2 = beanLocalMetalLossResult.rt;
                arg_2C7_0.CheckRt = new bool?(num2.GetValueOrDefault() >= 0.2 && num2.HasValue);
                BeanLocalMetalLossResult arg_329_0 = beanLocalMetalLossResult;
                num2 = p_beanLocalMetalLoss.lmsd;
                num = 1.8 * Math.Sqrt(p_beanLocalMetalLoss.insideDiameter.Value * beanLocalMetalLossResult.tc.Value);
                arg_329_0.CheckLmsd = new bool?(num2.GetValueOrDefault() >= num && num2.HasValue);
                bool arg_3A3_0;
                if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_SI)
                {
                    num2 = beanLocalMetalLossResult.tmm;
                    num3 = p_beanLocalMetalLoss.fca;
                    num2 = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() - num3.GetValueOrDefault()) : null);
                    arg_3A3_0 = (num2.GetValueOrDefault() < 2.5 || !num2.HasValue);
                }
                else
                {
                    arg_3A3_0 = true;
                }
                if (!arg_3A3_0)
                {
                    beanLocalMetalLossResult.CheckTmm = new bool?(true);
                }
                else
                {
                    bool arg_431_0;
                    if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_MATRIC)
                    {
                        num2 = beanLocalMetalLossResult.tmm;
                        num3 = p_beanLocalMetalLoss.fca;
                        num2 = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() - num3.GetValueOrDefault()) : null);
                        arg_431_0 = (num2.GetValueOrDefault() < 0.1 || !num2.HasValue);
                    }
                    else
                    {
                        arg_431_0 = true;
                    }
                    if (!arg_431_0)
                    {
                        beanLocalMetalLossResult.CheckTmm = new bool?(true);
                    }
                    else
                    {
                        beanLocalMetalLossResult.CheckTmm = new bool?(false);
                    }
                }
                beanLocalMetalLossResult.CheckFlawLong = new bool?(beanLocalMetalLossResult.CheckRt.Value && beanLocalMetalLossResult.CheckLmsd.Value && beanLocalMetalLossResult.CheckTmm.Value);
                double num4 = p_beanLocalMetalLoss.insideDiameter.Value / 2.0;
                BeanLocalMetalLossResult arg_5CB_0 = beanLocalMetalLossResult;
                num2 = p_beanLocalMetalLoss.allowableStress;
                num3 = p_beanLocalMetalLoss.weldJointEfficiency;
                num2 = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() * num3.GetValueOrDefault()) : null);
                num3 = beanLocalMetalLossResult.tc;
                num2 = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() * num3.GetValueOrDefault()) : null);
                num = num4;
                num3 = beanLocalMetalLossResult.tc;
                num3 = (num3.HasValue ? new double?(0.6 * num3.GetValueOrDefault()) : null);
                num3 = (num3.HasValue ? new double?(num + num3.GetValueOrDefault()) : null);
                arg_5CB_0.MAWPc = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() / num3.GetValueOrDefault()) : null);
                BeanLocalMetalLossResult arg_715_0 = beanLocalMetalLossResult;
                num2 = p_beanLocalMetalLoss.allowableStress;
                num2 = (num2.HasValue ? new double?(2.0 * num2.GetValueOrDefault()) : null);
                num3 = p_beanLocalMetalLoss.weldJointEfficiency;
                num2 = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() * num3.GetValueOrDefault()) : null);
                num3 = beanLocalMetalLossResult.tc;
                num2 = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() * num3.GetValueOrDefault()) : null);
                num = num4;
                num3 = beanLocalMetalLossResult.tc;
                num3 = (num3.HasValue ? new double?(0.4 * num3.GetValueOrDefault()) : null);
                num3 = (num3.HasValue ? new double?(num - num3.GetValueOrDefault()) : null);
                arg_715_0.MAWPl = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() / num3.GetValueOrDefault()) : null);
                BeanLocalMetalLossResult arg_760_0 = beanLocalMetalLossResult;
                num2 = beanLocalMetalLossResult.MAWPc;
                num3 = beanLocalMetalLossResult.MAWPl;
                arg_760_0.MAWP = ((num2.GetValueOrDefault() < num3.GetValueOrDefault() && (num2.HasValue & num3.HasValue)) ? beanLocalMetalLossResult.MAWPc : beanLocalMetalLossResult.MAWPl);
                beanLocalMetalLossResult.RSFa = p_beanLocalMetalLoss.allowRSF;
                if (p_beanLocalMetalLoss.componentShapeID == 1)
                {
                    BeanLocalMetalLossResult arg_B50_0 = beanLocalMetalLossResult;
                    num2 = beanLocalMetalLossResult.lampda;
                    num2 = (num2.HasValue ? new double?(0.014195 * num2.GetValueOrDefault()) : null);
                    num2 = (num2.HasValue ? new double?(1.001 - num2.GetValueOrDefault()) : null);
                    num = 0.2909 * Math.Pow(beanLocalMetalLossResult.lampda.Value, 2.0);
                    num2 = (num2.HasValue ? new double?(num2.GetValueOrDefault() + num) : null);
                    num = 0.09642 * Math.Pow(beanLocalMetalLossResult.lampda.Value, 3.0);
                    num2 = (num2.HasValue ? new double?(num2.GetValueOrDefault() - num) : null);
                    num = 0.02089 * Math.Pow(beanLocalMetalLossResult.lampda.Value, 4.0);
                    num2 = (num2.HasValue ? new double?(num2.GetValueOrDefault() + num) : null);
                    num = 0.003054 * Math.Pow(beanLocalMetalLossResult.lampda.Value, 5.0);
                    num2 = (num2.HasValue ? new double?(num2.GetValueOrDefault() - num) : null);
                    num = 2.957 * Math.Pow(10.0, -4.0) * Math.Pow(beanLocalMetalLossResult.lampda.Value, 6.0);
                    num2 = (num2.HasValue ? new double?(num2.GetValueOrDefault() + num) : null);
                    num = 1.8462 * Math.Pow(10.0, -5.0) * Math.Pow(beanLocalMetalLossResult.lampda.Value, 7.0);
                    num2 = (num2.HasValue ? new double?(num2.GetValueOrDefault() - num) : null);
                    num = 7.1553 * Math.Pow(10.0, -7.0) * Math.Pow(beanLocalMetalLossResult.lampda.Value, 8.0);
                    num2 = (num2.HasValue ? new double?(num2.GetValueOrDefault() + num) : null);
                    num = 1.5631 * Math.Pow(10.0, -8.0) * Math.Pow(beanLocalMetalLossResult.lampda.Value, 9.0);
                    num2 = (num2.HasValue ? new double?(num2.GetValueOrDefault() - num) : null);
                    num = 1.4656 * Math.Pow(10.0, -10.0) * Math.Pow(beanLocalMetalLossResult.lampda.Value, 10.0);
                    arg_B50_0.Mt = (num2.HasValue ? new double?(num2.GetValueOrDefault() + num) : null);
                }
                else
                {
                    BeanLocalMetalLossResult arg_CFB_0 = beanLocalMetalLossResult;
                    num2 = beanLocalMetalLossResult.lampda;
                    num2 = (num2.HasValue ? new double?(0.49001 * num2.GetValueOrDefault()) : null);
                    num2 = (num2.HasValue ? new double?(1.0005 + num2.GetValueOrDefault()) : null);
                    num = 0.32409 * Math.Pow(beanLocalMetalLossResult.lampda.Value, 2.0);
                    num2 = (num2.HasValue ? new double?(num2.GetValueOrDefault() + num) : null);
                    num3 = beanLocalMetalLossResult.lampda;
                    num3 = (num3.HasValue ? new double?(0.50144 * num3.GetValueOrDefault()) : null);
                    num3 = (num3.HasValue ? new double?(1.0 + num3.GetValueOrDefault()) : null);
                    num = 0.011067 * Math.Pow(beanLocalMetalLossResult.lampda.Value, 2.0);
                    num3 = (num3.HasValue ? new double?(num3.GetValueOrDefault() - num) : null);
                    arg_CFB_0.Mt = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() / num3.GetValueOrDefault()) : null);
                }
                BeanLocalMetalLossResult arg_DDF_0 = beanLocalMetalLossResult;
                num2 = beanLocalMetalLossResult.rt;
                num3 = beanLocalMetalLossResult.rt;
                num3 = (num3.HasValue ? new double?(1.0 - num3.GetValueOrDefault()) : null);
                double? num5 = beanLocalMetalLossResult.Mt;
                num3 = ((num3.HasValue & num5.HasValue) ? new double?(num3.GetValueOrDefault() / num5.GetValueOrDefault()) : null);
                num3 = (num3.HasValue ? new double?(1.0 - num3.GetValueOrDefault()) : null);
                arg_DDF_0.RSF = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() / num3.GetValueOrDefault()) : null);
                BeanLocalMetalLossResult arg_E66_0 = beanLocalMetalLossResult;
                num2 = beanLocalMetalLossResult.MAWP;
                num3 = beanLocalMetalLossResult.RSF;
                num5 = beanLocalMetalLossResult.RSFa;
                num3 = ((num3.HasValue & num5.HasValue) ? new double?(num3.GetValueOrDefault() / num5.GetValueOrDefault()) : null);
                arg_E66_0.MAWPr = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() * num3.GetValueOrDefault()) : null);
                bool arg_EB5_0;
                if (p_beanLocalMetalLoss.componentShapeID == 1)
                {
                    num2 = beanLocalMetalLossResult.lampda;
                    arg_EB5_0 = (num2.GetValueOrDefault() > 0.354 || !num2.HasValue);
                }
                else
                {
                    arg_EB5_0 = true;
                }
                if (!arg_EB5_0)
                {
                    beanLocalMetalLossResult.RtLimit = new double?(0.2);
                }
                else
                {
                    bool arg_F47_0;
                    if (p_beanLocalMetalLoss.componentShapeID == 1)
                    {
                        num2 = beanLocalMetalLossResult.lampda;
                        if (num2.GetValueOrDefault() > 0.354 && num2.HasValue)
                        {
                            num2 = beanLocalMetalLossResult.lampda;
                            arg_F47_0 = (num2.GetValueOrDefault() >= 20.0 || !num2.HasValue);
                            goto IL_F47;
                        }
                    }
                    arg_F47_0 = true;
                    IL_F47:
                    if (!arg_F47_0)
                    {
                        BeanLocalMetalLossResult arg_1034_0 = beanLocalMetalLossResult;
                        num2 = beanLocalMetalLossResult.RSFa;
                        num3 = beanLocalMetalLossResult.RSFa;
                        num5 = beanLocalMetalLossResult.Mt;
                        num3 = ((num3.HasValue & num5.HasValue) ? new double?(num3.GetValueOrDefault() / num5.GetValueOrDefault()) : null);
                        num2 = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() - num3.GetValueOrDefault()) : null);
                        num = Math.Pow(1.0 - beanLocalMetalLossResult.RSFa.Value / beanLocalMetalLossResult.Mt.Value, -1.0);
                        arg_1034_0.RtLimit = (num2.HasValue ? new double?(num2.GetValueOrDefault() * num) : null);
                    }
                    else
                    {
                        bool arg_1089_0;
                        if (p_beanLocalMetalLoss.componentShapeID == 1)
                        {
                            num2 = beanLocalMetalLossResult.lampda;
                            arg_1089_0 = (num2.GetValueOrDefault() < 20.0 || !num2.HasValue);
                        }
                        else
                        {
                            arg_1089_0 = true;
                        }
                        if (!arg_1089_0)
                        {
                            beanLocalMetalLossResult.RtLimit = new double?(0.9);
                        }
                        else
                        {
                            bool arg_10F4_0;
                            if (p_beanLocalMetalLoss.componentShapeID == 2)
                            {
                                num2 = beanLocalMetalLossResult.lampda;
                                arg_10F4_0 = (num2.GetValueOrDefault() > 0.33 || !num2.HasValue);
                            }
                            else
                            {
                                arg_10F4_0 = true;
                            }
                            if (!arg_10F4_0)
                            {
                                beanLocalMetalLossResult.RtLimit = new double?(0.2);
                            }
                            else
                            {
                                bool arg_1186_0;
                                if (p_beanLocalMetalLoss.componentShapeID == 2)
                                {
                                    num2 = beanLocalMetalLossResult.lampda;
                                    if (num2.GetValueOrDefault() > 0.33 && num2.HasValue)
                                    {
                                        num2 = beanLocalMetalLossResult.lampda;
                                        arg_1186_0 = (num2.GetValueOrDefault() >= 20.0 || !num2.HasValue);
                                        goto IL_1186;
                                    }
                                }
                                arg_1186_0 = true;
                                IL_1186:
                                if (!arg_1186_0)
                                {
                                    BeanLocalMetalLossResult arg_1273_0 = beanLocalMetalLossResult;
                                    num2 = beanLocalMetalLossResult.RSFa;
                                    num3 = beanLocalMetalLossResult.RSFa;
                                    num5 = beanLocalMetalLossResult.Mt;
                                    num3 = ((num3.HasValue & num5.HasValue) ? new double?(num3.GetValueOrDefault() / num5.GetValueOrDefault()) : null);
                                    num2 = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() - num3.GetValueOrDefault()) : null);
                                    num = Math.Pow(1.0 - beanLocalMetalLossResult.RSFa.Value / beanLocalMetalLossResult.Mt.Value, -1.0);
                                    arg_1273_0.RtLimit = (num2.HasValue ? new double?(num2.GetValueOrDefault() * num) : null);
                                }
                                else
                                {
                                    bool arg_12C5_0;
                                    if (p_beanLocalMetalLoss.componentShapeID == 2)
                                    {
                                        num2 = beanLocalMetalLossResult.lampda;
                                        arg_12C5_0 = (num2.GetValueOrDefault() < 20.0 || !num2.HasValue);
                                    }
                                    else
                                    {
                                        arg_12C5_0 = true;
                                    }
                                    if (!arg_12C5_0)
                                    {
                                        beanLocalMetalLossResult.RtLimit = new double?(0.9);
                                    }
                                }
                            }
                        }
                    }
                }
                bool arg_132D_0;
                if (beanLocalMetalLossResult.CheckFlawLong.Value)
                {
                    num2 = beanLocalMetalLossResult.rt;
                    num3 = beanLocalMetalLossResult.RtLimit;
                    arg_132D_0 = (num2.GetValueOrDefault() < num3.GetValueOrDefault() || !(num2.HasValue & num3.HasValue));
                }
                else
                {
                    arg_132D_0 = true;
                }
                if (!arg_132D_0)
                {
                    beanLocalMetalLossResult.CheckLongitudinalFlaw = new bool?(true);
                }
                else
                {
                    bool arg_13C8_0;
                    if (beanLocalMetalLossResult.CheckFlawLong.Value)
                    {
                        num2 = beanLocalMetalLossResult.rt;
                        num3 = beanLocalMetalLossResult.RtLimit;
                        if (num2.GetValueOrDefault() < num3.GetValueOrDefault() && (num2.HasValue & num3.HasValue))
                        {
                            num2 = beanLocalMetalLossResult.RSF;
                            num3 = beanLocalMetalLossResult.RSFa;
                            arg_13C8_0 = (num2.GetValueOrDefault() < num3.GetValueOrDefault() || !(num2.HasValue & num3.HasValue));
                            goto IL_13C8;
                        }
                    }
                    arg_13C8_0 = true;
                    IL_13C8:
                    if (!arg_13C8_0)
                    {
                        beanLocalMetalLossResult.CheckLongitudinalFlaw = new bool?(true);
                    }
                    else
                    {
                        beanLocalMetalLossResult.CheckLongitudinalFlaw = new bool?(false);
                    }
                }
                if (p_beanLocalMetalLoss.componentShapeID == 1)
                {
                    BeanLocalMetalLossResult arg_1480_0 = beanLocalMetalLossResult;
                    int? num6 = p_beanLocalMetalLoss.numberOfInspectionColumn - 1;
                    num2 = p_beanLocalMetalLoss.widthOfTheCirGrid;
                    arg_1480_0.c = ((num6.HasValue & num2.HasValue) ? new double?((double)num6.GetValueOrDefault() * num2.GetValueOrDefault()) : null);
                    num2 = beanLocalMetalLossResult.c;
                    if (num2.GetValueOrDefault() == 0.0 && num2.HasValue && p_beanLocalMetalLoss.componentShapeID == 1)
                    {
                        beanLocalMetalLossResult.c = beanLocalMetalLossResult.s;
                    }
                    BeanLocalMetalLossResult arg_1568_0 = beanLocalMetalLossResult;
                    num2 = beanLocalMetalLossResult.c;
                    num2 = (num2.HasValue ? new double?(1.285 * num2.GetValueOrDefault()) : null);
                    num = Math.Sqrt(p_beanLocalMetalLoss.insideDiameter.Value * beanLocalMetalLossResult.tc.Value);
                    arg_1568_0.lampdaC = (num2.HasValue ? new double?(num2.GetValueOrDefault() / num) : null);
                    num2 = beanLocalMetalLossResult.lampdaC;
                    if (num2.GetValueOrDefault() <= 9.0 && num2.HasValue)
                    {
                        beanLocalMetalLossResult.CheckLampdaC = new bool?(true);
                    }
                    else
                    {
                        beanLocalMetalLossResult.CheckLampdaC = new bool?(false);
                    }
                    num2 = p_beanLocalMetalLoss.insideDiameter;
                    num3 = beanLocalMetalLossResult.tc;
                    num2 = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() / num3.GetValueOrDefault()) : null);
                    if (num2.GetValueOrDefault() >= 20.0 && num2.HasValue)
                    {
                        beanLocalMetalLossResult.CheckDtc = new bool?(true);
                    }
                    else
                    {
                        beanLocalMetalLossResult.CheckDtc = new bool?(false);
                    }
                    num2 = beanLocalMetalLossResult.RSF;
                    bool arg_1699_0;
                    if (num2.GetValueOrDefault() >= 0.7 && num2.HasValue)
                    {
                        num2 = beanLocalMetalLossResult.RSF;
                        arg_1699_0 = (num2.GetValueOrDefault() > 1.0 || !num2.HasValue);
                    }
                    else
                    {
                        arg_1699_0 = true;
                    }
                    if (!arg_1699_0)
                    {
                        beanLocalMetalLossResult.CheckRSF = new bool?(true);
                    }
                    else
                    {
                        beanLocalMetalLossResult.CheckRSF = new bool?(false);
                    }
                    BeanLocalMetalLossResult arg_17FC_0 = beanLocalMetalLossResult;
                    num2 = p_beanLocalMetalLoss.weldJointEfficiency;
                    num3 = beanLocalMetalLossResult.RSF;
                    num3 = (num3.HasValue ? new double?(2.0 * num3.GetValueOrDefault()) : null);
                    num2 = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() / num3.GetValueOrDefault()) : null);
                    num = Math.Sqrt(4.0 - 3.0 * Math.Pow(p_beanLocalMetalLoss.weldJointEfficiency.Value, 2.0));
                    num3 = p_beanLocalMetalLoss.weldJointEfficiency;
                    num3 = (num3.HasValue ? new double?(num / num3.GetValueOrDefault()) : null);
                    num3 = (num3.HasValue ? new double?(1.0 + num3.GetValueOrDefault()) : null);
                    arg_17FC_0.TSF = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() * num3.GetValueOrDefault()) : null);
                    num2 = beanLocalMetalLossResult.RSF;
                    bool arg_1854_0;
                    if (num2.GetValueOrDefault() >= 0.7 && num2.HasValue)
                    {
                        num2 = beanLocalMetalLossResult.RSF;
                        arg_1854_0 = (num2.GetValueOrDefault() > 2.3 || !num2.HasValue);
                    }
                    else
                    {
                        arg_1854_0 = true;
                    }
                    if (!arg_1854_0)
                    {
                        beanLocalMetalLossResult.CheckTSF = new bool?(true);
                    }
                    else
                    {
                        beanLocalMetalLossResult.CheckTSF = new bool?(false);
                    }
                    if (beanLocalMetalLossResult.CheckLampdaC.Value && beanLocalMetalLossResult.CheckDtc.Value && beanLocalMetalLossResult.CheckRSF.Value && beanLocalMetalLossResult.CheckTSF.Value)
                    {
                        beanLocalMetalLossResult.CheckFlawCir = new bool?(true);
                    }
                    else
                    {
                        beanLocalMetalLossResult.CheckFlawCir = new bool?(false);
                    }
                    double[,] array = new double[8, 9];
                    array[0, 0] = this.getExpFromString("7.0000E-01");
                    array[0, 1] = this.getExpFromString("7.5000E-01");
                    array[0, 2] = this.getExpFromString("8.0000E-01");
                    array[0, 3] = this.getExpFromString("9.0000E-01");
                    array[0, 4] = this.getExpFromString("1.0000E+00");
                    array[0, 5] = this.getExpFromString("1.2000E+00");
                    array[0, 6] = this.getExpFromString("1.4000E+00");
                    array[0, 7] = this.getExpFromString("1.8000E+00");
                    array[0, 8] = this.getExpFromString("2.3000E+00");
                    array[1, 0] = this.getExpFromString("2.1000E-01");
                    array[1, 1] = this.getExpFromString("4.8000E-01");
                    array[1, 2] = this.getExpFromString("6.7000E-01");
                    array[1, 3] = this.getExpFromString("9.8000E-01");
                    array[1, 4] = this.getExpFromString("1.2300E+00");
                    array[1, 5] = this.getExpFromString("1.6600E+00");
                    array[1, 6] = this.getExpFromString("2.0300E+00");
                    array[1, 7] = this.getExpFromString("2.6600E+00");
                    array[1, 8] = this.getExpFromString("3.3500E+00");
                    array[2, 0] = this.getExpFromString("9.9221E-01");
                    array[2, 1] = this.getExpFromString("9.6801E-01");
                    array[2, 2] = this.getExpFromString("9.4413E-01");
                    array[2, 3] = this.getExpFromString("8.9962E-01");
                    array[2, 4] = this.getExpFromString("8.5947E-01");
                    array[2, 5] = this.getExpFromString("7.8654E-01");
                    array[2, 6] = this.getExpFromString("7.2335E-01");
                    array[2, 7] = this.getExpFromString("6.0737E-01");
                    array[2, 8] = this.getExpFromString("4.9304E-01");
                    array[3, 0] = this.getExpFromString("-1.1959E-01");
                    array[3, 1] = this.getExpFromString("-2.3780E-01");
                    array[3, 2] = this.getExpFromString("-3.1256E-01");
                    array[3, 3] = this.getExpFromString("-3.8860E-01");
                    array[3, 4] = this.getExpFromString("-4.0012E-01");
                    array[3, 5] = this.getExpFromString("-2.5322E-01");
                    array[3, 6] = this.getExpFromString("1.1528E-02");
                    array[3, 7] = this.getExpFromString("9.3796E-01");
                    array[3, 8] = this.getExpFromString("2.1692E+00");
                    array[4, 0] = this.getExpFromString("-5.7333E-02");
                    array[4, 1] = this.getExpFromString("-3.2678E-01");
                    array[4, 2] = this.getExpFromString("-6.9968E-01");
                    array[4, 3] = this.getExpFromString("-1.6485E+00");
                    array[4, 4] = this.getExpFromString("-2.7979E+00");
                    array[4, 5] = this.getExpFromString("-5.7982E+00");
                    array[4, 6] = this.getExpFromString("-9.3536E+00");
                    array[4, 7] = this.getExpFromString("-1.9239E+01");
                    array[4, 8] = this.getExpFromString("-3.2459E+01");
                    array[5, 0] = this.getExpFromString("1.6948E-02");
                    array[5, 1] = this.getExpFromString("2.0684E-01");
                    array[5, 2] = this.getExpFromString("6.5020E-01");
                    array[5, 3] = this.getExpFromString("2.3445E+00");
                    array[5, 4] = this.getExpFromString("5.0729E+00");
                    array[5, 5] = this.getExpFromString("1.3858E+01");
                    array[5, 6] = this.getExpFromString("2.6031E+01");
                    array[5, 7] = this.getExpFromString("6.4267E+01");
                    array[5, 8] = this.getExpFromString("1.2245E+02");
                    array[6, 0] = this.getExpFromString("-1.7976E-03");
                    array[6, 1] = this.getExpFromString("-4.6537E-02");
                    array[6, 2] = this.getExpFromString("-2.2102E-01");
                    array[6, 3] = this.getExpFromString("-1.2534E+00");
                    array[6, 4] = this.getExpFromString("-3.5217E+00");
                    array[6, 5] = this.getExpFromString("-1.3118E+01");
                    array[6, 6] = this.getExpFromString("-2.9372E+01");
                    array[6, 7] = this.getExpFromString("-9.1307E+01");
                    array[6, 8] = this.getExpFromString("-2.0243E+02");
                    array[7, 0] = this.getExpFromString("6.9114E-05");
                    array[7, 1] = this.getExpFromString("3.9436E-03");
                    array[7, 2] = this.getExpFromString("2.8799E-02");
                    array[7, 3] = this.getExpFromString("2.5331E-01");
                    array[7, 4] = this.getExpFromString("9.1877E-01");
                    array[7, 5] = this.getExpFromString("4.6436E+00");
                    array[7, 6] = this.getExpFromString("1.2387E+01");
                    array[7, 7] = this.getExpFromString("4.8962E+01");
                    array[7, 8] = this.getExpFromString("1.2727E+02");
                    beanLocalMetalLossResult.UpperIndex = 1;
                    for (int i = 1; i <= 7; i++)
                    {
                        num2 = beanLocalMetalLossResult.TSF;
                        num = array[0, i];
                        if (num2.GetValueOrDefault() >= num && num2.HasValue)
                        {
                            beanLocalMetalLossResult.UpperIndex++;
                        }
                    }
                    beanLocalMetalLossResult.LowerIndex = beanLocalMetalLossResult.UpperIndex - 1;
                    num2 = beanLocalMetalLossResult.lampdaC;
                    num = array[1, beanLocalMetalLossResult.UpperIndex];
                    if (num2.GetValueOrDefault() <= num && num2.HasValue)
                    {
                        beanLocalMetalLossResult.RtUp = new double?(0.2);
                    }
                    else
                    {
                        double num7 = 0.0;
                        for (int i = 1; i <= 7; i++)
                        {
                            num7 += array[i, beanLocalMetalLossResult.UpperIndex] / Math.Pow(beanLocalMetalLossResult.lampdaC.Value, (double)i);
                        }
                        beanLocalMetalLossResult.RtUp = new double?(num7);
                    }
                    num2 = beanLocalMetalLossResult.lampdaC;
                    num = array[1, beanLocalMetalLossResult.LowerIndex];
                    if (num2.GetValueOrDefault() <= num && num2.HasValue)
                    {
                        beanLocalMetalLossResult.RtLow = new double?(0.2);
                    }
                    else
                    {
                        double num7 = 0.0;
                        for (int i = 1; i <= 7; i++)
                        {
                            num7 += array[i, beanLocalMetalLossResult.LowerIndex] / Math.Pow(beanLocalMetalLossResult.lampdaC.Value, (double)i);
                        }
                        beanLocalMetalLossResult.RtLow = new double?(num7);
                    }
                    BeanLocalMetalLossResult arg_2110_0 = beanLocalMetalLossResult;
                    num2 = beanLocalMetalLossResult.RtLow;
                    num3 = beanLocalMetalLossResult.TSF;
                    num = array[0, beanLocalMetalLossResult.LowerIndex];
                    num3 = (num3.HasValue ? new double?(num3.GetValueOrDefault() - num) : null);
                    num = array[0, beanLocalMetalLossResult.UpperIndex] - array[0, beanLocalMetalLossResult.LowerIndex];
                    num3 = (num3.HasValue ? new double?(num3.GetValueOrDefault() / num) : null);
                    num5 = beanLocalMetalLossResult.RtUp;
                    double? rtLow = beanLocalMetalLossResult.RtLow;
                    num5 = ((num5.HasValue & rtLow.HasValue) ? new double?(num5.GetValueOrDefault() - rtLow.GetValueOrDefault()) : null);
                    num3 = ((num3.HasValue & num5.HasValue) ? new double?(num3.GetValueOrDefault() * num5.GetValueOrDefault()) : null);
                    arg_2110_0.RtCir = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() + num3.GetValueOrDefault()) : null);
                    bool arg_2161_0;
                    if (beanLocalMetalLossResult.CheckFlawCir.Value)
                    {
                        num2 = beanLocalMetalLossResult.rt;
                        num3 = beanLocalMetalLossResult.RtCir;
                        arg_2161_0 = (num2.GetValueOrDefault() < num3.GetValueOrDefault() || !(num2.HasValue & num3.HasValue));
                    }
                    else
                    {
                        arg_2161_0 = true;
                    }
                    if (!arg_2161_0)
                    {
                        beanLocalMetalLossResult.CheckCircumferentialFlaw = new bool?(true);
                    }
                    else
                    {
                        beanLocalMetalLossResult.CheckCircumferentialFlaw = new bool?(false);
                    }
                }
                else
                {
                    beanLocalMetalLossResult.CheckCircumferentialFlaw = new bool?(true);
                }
                if (p_beanLocalMetalLoss.componentShapeID == 1 && beanLocalMetalLossResult.CheckLongitudinalFlaw.Value && beanLocalMetalLossResult.CheckCircumferentialFlaw.Value)
                {
                    beanLocalMetalLossResult.result = "The Level 1 assessment criteria are satisfied.";
                    beanLocalMetalLossResult.resultBool = true;
                }
                else if (p_beanLocalMetalLoss.componentShapeID == 1 && !beanLocalMetalLossResult.CheckLongitudinalFlaw.Value && beanLocalMetalLossResult.CheckCircumferentialFlaw.Value)
                {
                    beanLocalMetalLossResult.result = "The Level 1 assessment criteria are  not satisfied. ";
                    beanLocalMetalLossResult.resultBool = false;
                }
                else if (p_beanLocalMetalLoss.componentShapeID == 1 && beanLocalMetalLossResult.CheckLongitudinalFlaw.Value && !beanLocalMetalLossResult.CheckCircumferentialFlaw.Value)
                {
                    beanLocalMetalLossResult.result = "The Level 1 assessment criteria are  not satisfied.";
                    beanLocalMetalLossResult.resultBool = false;
                }
                else if (p_beanLocalMetalLoss.componentShapeID == 2 && beanLocalMetalLossResult.CheckLongitudinalFlaw.Value)
                {
                    beanLocalMetalLossResult.result = "The Level 1 assessment criteria are satisfied.";
                    beanLocalMetalLossResult.resultBool = true;
                }
                else
                {
                    beanLocalMetalLossResult.result = "The Level 1 assessment criteria are not satisfied.";
                    beanLocalMetalLossResult.resultBool = false;
                }
                return beanLocalMetalLossResult;
            }

            public  BeanLocalMetalLossResult calculateLevel2(BeanLocalMetalLoss p_beanLocalMetalLoss)
            {
                BeanLocalMetalLossResult beanLocalMetalLossResult = new BeanLocalMetalLossResult();
                beanLocalMetalLossResult.tmm = new double?(this.getMinData(p_beanLocalMetalLoss.inspectionGridData, p_beanLocalMetalLoss.numberOfInspectionColumn.Value, p_beanLocalMetalLoss.numberOfInspectionRow.Value));
                double[] minLongitudinalDataUpperLower = this.getMinLongitudinalDataUpperLower(p_beanLocalMetalLoss.inspectionGridData, p_beanLocalMetalLoss.numberOfInspectionColumn.Value, p_beanLocalMetalLoss.numberOfInspectionRow.Value);
                beanLocalMetalLossResult.Xi = new double?(minLongitudinalDataUpperLower[0]);
                BeanLocalMetalLossResult arg_F2_0 = beanLocalMetalLossResult;
                double num = p_beanLocalMetalLoss.nominalThickness.Value;
                double? num2 = p_beanLocalMetalLoss.loss;
                num2 = (num2.HasValue ? new double?(num - num2.GetValueOrDefault()) : null);
                double? num3 = p_beanLocalMetalLoss.fca;
                arg_F2_0.tc = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() - num3.GetValueOrDefault()) : null);
                BeanLocalMetalLossResult arg_13A_0 = beanLocalMetalLossResult;
                num = (double)(p_beanLocalMetalLoss.numberOfInspectionColumn.Value - 1);
                num2 = p_beanLocalMetalLoss.widthOfTheLongGrid;
                arg_13A_0.s = (num2.HasValue ? new double?(num * num2.GetValueOrDefault()) : null);
                BeanLocalMetalLossResult arg_1C1_0 = beanLocalMetalLossResult;
                num2 = beanLocalMetalLossResult.tmm;
                num3 = p_beanLocalMetalLoss.fca;
                num2 = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() - num3.GetValueOrDefault()) : null);
                num3 = beanLocalMetalLossResult.tc;
                arg_1C1_0.rt = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() / num3.GetValueOrDefault()) : null);
                BeanLocalMetalLossResult arg_24B_0 = beanLocalMetalLossResult;
                num2 = beanLocalMetalLossResult.s;
                num2 = (num2.HasValue ? new double?(1.285 * num2.GetValueOrDefault()) : null);
                num = Math.Sqrt(p_beanLocalMetalLoss.insideDiameter.Value * beanLocalMetalLossResult.tc.Value);
                arg_24B_0.lampda = (num2.HasValue ? new double?(num2.GetValueOrDefault() / num) : null);
                num2 = beanLocalMetalLossResult.lampda;
                if (num2.GetValueOrDefault() >= 20.0 && num2.HasValue)
                {
                    beanLocalMetalLossResult.lampda = new double?(20.0);
                }
                beanLocalMetalLossResult.RSFa = p_beanLocalMetalLoss.allowRSF;
                BeanLocalMetalLossResult arg_2D4_0 = beanLocalMetalLossResult;
                num2 = beanLocalMetalLossResult.rt;
                arg_2D4_0.CheckRt = new bool?(num2.GetValueOrDefault() >= 0.2 && num2.HasValue);
                BeanLocalMetalLossResult arg_336_0 = beanLocalMetalLossResult;
                num2 = p_beanLocalMetalLoss.lmsd;
                num = 1.8 * Math.Sqrt(p_beanLocalMetalLoss.insideDiameter.Value * beanLocalMetalLossResult.tc.Value);
                arg_336_0.CheckLmsd = new bool?(num2.GetValueOrDefault() >= num && num2.HasValue);
                bool arg_3B0_0;
                if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_SI)
                {
                    num2 = beanLocalMetalLossResult.tmm;
                    num3 = p_beanLocalMetalLoss.fca;
                    num2 = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() - num3.GetValueOrDefault()) : null);
                    arg_3B0_0 = (num2.GetValueOrDefault() < 2.5 || !num2.HasValue);
                }
                else
                {
                    arg_3B0_0 = true;
                }
                if (!arg_3B0_0)
                {
                    beanLocalMetalLossResult.CheckTmm = new bool?(true);
                }
                else
                {
                    bool arg_43E_0;
                    if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_MATRIC)
                    {
                        num2 = beanLocalMetalLossResult.tmm;
                        num3 = p_beanLocalMetalLoss.fca;
                        num2 = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() - num3.GetValueOrDefault()) : null);
                        arg_43E_0 = (num2.GetValueOrDefault() < 0.1 || !num2.HasValue);
                    }
                    else
                    {
                        arg_43E_0 = true;
                    }
                    if (!arg_43E_0)
                    {
                        beanLocalMetalLossResult.CheckTmm = new bool?(true);
                    }
                    else
                    {
                        beanLocalMetalLossResult.CheckTmm = new bool?(false);
                    }
                }
                beanLocalMetalLossResult.CheckFlawLong = new bool?(beanLocalMetalLossResult.CheckRt.Value && beanLocalMetalLossResult.CheckLmsd.Value && beanLocalMetalLossResult.CheckTmm.Value);
                double num4 = p_beanLocalMetalLoss.insideDiameter.Value / 2.0;
                BeanLocalMetalLossResult arg_5D8_0 = beanLocalMetalLossResult;
                num2 = p_beanLocalMetalLoss.allowableStress;
                num3 = p_beanLocalMetalLoss.weldJointEfficiency;
                num2 = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() * num3.GetValueOrDefault()) : null);
                num3 = beanLocalMetalLossResult.tc;
                num2 = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() * num3.GetValueOrDefault()) : null);
                num = num4;
                num3 = beanLocalMetalLossResult.tc;
                num3 = (num3.HasValue ? new double?(0.6 * num3.GetValueOrDefault()) : null);
                num3 = (num3.HasValue ? new double?(num + num3.GetValueOrDefault()) : null);
                arg_5D8_0.MAWPc = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() / num3.GetValueOrDefault()) : null);
                BeanLocalMetalLossResult arg_722_0 = beanLocalMetalLossResult;
                num2 = p_beanLocalMetalLoss.allowableStress;
                num2 = (num2.HasValue ? new double?(2.0 * num2.GetValueOrDefault()) : null);
                num3 = p_beanLocalMetalLoss.weldJointEfficiency;
                num2 = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() * num3.GetValueOrDefault()) : null);
                num3 = beanLocalMetalLossResult.tc;
                num2 = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() * num3.GetValueOrDefault()) : null);
                num = num4;
                num3 = beanLocalMetalLossResult.tc;
                num3 = (num3.HasValue ? new double?(0.4 * num3.GetValueOrDefault()) : null);
                num3 = (num3.HasValue ? new double?(num - num3.GetValueOrDefault()) : null);
                arg_722_0.MAWPl = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() / num3.GetValueOrDefault()) : null);
                BeanLocalMetalLossResult arg_76D_0 = beanLocalMetalLossResult;
                num2 = beanLocalMetalLossResult.MAWPc;
                num3 = beanLocalMetalLossResult.MAWPl;
                arg_76D_0.MAWP = ((num2.GetValueOrDefault() < num3.GetValueOrDefault() && (num2.HasValue & num3.HasValue)) ? beanLocalMetalLossResult.MAWPc : beanLocalMetalLossResult.MAWPl);
                beanLocalMetalLossResult.RSFmin = new double?(999999.0);
                double[] minLongitudinal = this.getMinLongitudinal(p_beanLocalMetalLoss.inspectionGridData, p_beanLocalMetalLoss.numberOfInspectionColumn.Value, p_beanLocalMetalLoss.numberOfInspectionRow.Value);
                int i = 0;
                while (true)
                {
                    int num5 = i;
                    if (!(num5 < p_beanLocalMetalLoss.numberOfInspectionColumn - 1))
                    {
                        break;
                    }
                    for (int num6 = i + 1; num6 < p_beanLocalMetalLoss.numberOfInspectionColumn; num6++)
                    {
                        double num7 = p_beanLocalMetalLoss.widthOfTheLongGrid.Value * (double)i;
                        double num8 = p_beanLocalMetalLoss.widthOfTheLongGrid.Value * (double)num6;
                        double num9 = num8 - num7;
                        double num10 = p_beanLocalMetalLoss.nominalThickness.Value * num9;
                        double num11 = 0.0;
                        for (int j = i; j <= num6 - 1; j++)
                        {
                            num11 += 0.5 * (p_beanLocalMetalLoss.widthOfTheLongGrid.Value * (double)(j + 1) - p_beanLocalMetalLoss.widthOfTheLongGrid.Value * (double)j) * (minLongitudinal[j + 1] + minLongitudinal[j]);
                        }
                        num11 = num10 - num11;
                        double num12 = 1.285 * num9 / Math.Sqrt(p_beanLocalMetalLoss.insideDiameter.Value * beanLocalMetalLossResult.tc.Value);
                        double num13 = 1.001 - 0.014195 * num12 + 0.2909 * Math.Pow(num12, 2.0) - 0.09642 * Math.Pow(num12, 3.0) + 0.02089 * Math.Pow(num12, 4.0) - 0.003054 * Math.Pow(num12, 5.0) + 2.957 * Math.Pow(10.0, -4.0) * Math.Pow(num12, 6.0) - 1.8462 * Math.Pow(10.0, -5.0) * Math.Pow(num12, 7.0) + 7.1553 * Math.Pow(10.0, -7.0) * Math.Pow(num12, 8.0) - 1.5631 * Math.Pow(10.0, -8.0) * Math.Pow(num12, 9.0) + 1.4656 * Math.Pow(10.0, -10.0) * Math.Pow(num12, 10.0);
                        double num14 = (1.0 - num11 / num10) / (1.0 - 1.0 / num13 * (num11 / num10));
                        num2 = beanLocalMetalLossResult.RSFmin;
                        num = num14;
                        if (num2.GetValueOrDefault() > num && num2.HasValue)
                        {
                            beanLocalMetalLossResult.RSFmin = new double?(num14);
                        }
                    }
                    i++;
                }
                beanLocalMetalLossResult.RSF = beanLocalMetalLossResult.RSFmin;
                BeanLocalMetalLossResult arg_BC5_0 = beanLocalMetalLossResult;
                num2 = beanLocalMetalLossResult.MAWP;
                num3 = beanLocalMetalLossResult.RSF;
                double? num15 = beanLocalMetalLossResult.RSFa;
                num3 = ((num3.HasValue & num15.HasValue) ? new double?(num3.GetValueOrDefault() / num15.GetValueOrDefault()) : null);
                arg_BC5_0.MAWPr = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() * num3.GetValueOrDefault()) : null);
                if (beanLocalMetalLossResult.CheckFlawLong.Value)
                {
                    beanLocalMetalLossResult.CheckLongitudinalFlaw = new bool?(true);
                }
                else
                {
                    bool arg_C3F_0;
                    if (beanLocalMetalLossResult.CheckFlawLong.Value)
                    {
                        num2 = beanLocalMetalLossResult.RSF;
                        num3 = beanLocalMetalLossResult.RSFa;
                        arg_C3F_0 = (num2.GetValueOrDefault() < num3.GetValueOrDefault() || !(num2.HasValue & num3.HasValue));
                    }
                    else
                    {
                        arg_C3F_0 = true;
                    }
                    if (!arg_C3F_0)
                    {
                        beanLocalMetalLossResult.CheckLongitudinalFlaw = new bool?(true);
                    }
                    else
                    {
                        beanLocalMetalLossResult.CheckLongitudinalFlaw = new bool?(false);
                    }
                }
                if (p_beanLocalMetalLoss.componentShapeID == 1)
                {
                    BeanLocalMetalLossResult arg_CF7_0 = beanLocalMetalLossResult;
                    int? num16 = p_beanLocalMetalLoss.numberOfInspectionRow - 1;
                    num2 = p_beanLocalMetalLoss.widthOfTheCirGrid;
                    arg_CF7_0.c = ((num16.HasValue & num2.HasValue) ? new double?((double)num16.GetValueOrDefault() * num2.GetValueOrDefault()) : null);
                    num2 = beanLocalMetalLossResult.c;
                    if (num2.GetValueOrDefault() == 0.0 && num2.HasValue && p_beanLocalMetalLoss.componentShapeID == 1)
                    {
                        beanLocalMetalLossResult.c = beanLocalMetalLossResult.s;
                    }
                    BeanLocalMetalLossResult arg_DDF_0 = beanLocalMetalLossResult;
                    num2 = beanLocalMetalLossResult.c;
                    num2 = (num2.HasValue ? new double?(1.285 * num2.GetValueOrDefault()) : null);
                    num = Math.Sqrt(p_beanLocalMetalLoss.insideDiameter.Value * beanLocalMetalLossResult.tc.Value);
                    arg_DDF_0.lampdaC = (num2.HasValue ? new double?(num2.GetValueOrDefault() / num) : null);
                    num2 = beanLocalMetalLossResult.lampdaC;
                    if (num2.GetValueOrDefault() <= 9.0 && num2.HasValue)
                    {
                        beanLocalMetalLossResult.CheckLampdaC = new bool?(true);
                    }
                    else
                    {
                        beanLocalMetalLossResult.CheckLampdaC = new bool?(false);
                    }
                    num2 = p_beanLocalMetalLoss.insideDiameter;
                    num3 = beanLocalMetalLossResult.tc;
                    num2 = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() / num3.GetValueOrDefault()) : null);
                    if (num2.GetValueOrDefault() >= 20.0 && num2.HasValue)
                    {
                        beanLocalMetalLossResult.CheckDtc = new bool?(true);
                    }
                    else
                    {
                        beanLocalMetalLossResult.CheckDtc = new bool?(false);
                    }
                    num2 = beanLocalMetalLossResult.RSF;
                    bool arg_F10_0;
                    if (num2.GetValueOrDefault() >= 0.7 && num2.HasValue)
                    {
                        num2 = beanLocalMetalLossResult.RSF;
                        arg_F10_0 = (num2.GetValueOrDefault() > 1.0 || !num2.HasValue);
                    }
                    else
                    {
                        arg_F10_0 = true;
                    }
                    if (!arg_F10_0)
                    {
                        beanLocalMetalLossResult.CheckRSF = new bool?(true);
                    }
                    else
                    {
                        beanLocalMetalLossResult.CheckRSF = new bool?(false);
                    }
                    BeanLocalMetalLossResult arg_1073_0 = beanLocalMetalLossResult;
                    num2 = p_beanLocalMetalLoss.weldJointEfficiency;
                    num3 = beanLocalMetalLossResult.RSF;
                    num3 = (num3.HasValue ? new double?(2.0 * num3.GetValueOrDefault()) : null);
                    num2 = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() / num3.GetValueOrDefault()) : null);
                    num = Math.Sqrt(4.0 - 3.0 * Math.Pow(p_beanLocalMetalLoss.weldJointEfficiency.Value, 2.0));
                    num3 = p_beanLocalMetalLoss.weldJointEfficiency;
                    num3 = (num3.HasValue ? new double?(num / num3.GetValueOrDefault()) : null);
                    num3 = (num3.HasValue ? new double?(1.0 + num3.GetValueOrDefault()) : null);
                    arg_1073_0.TSF = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() * num3.GetValueOrDefault()) : null);
                    num2 = beanLocalMetalLossResult.RSF;
                    bool arg_10CB_0;
                    if (num2.GetValueOrDefault() >= 0.7 && num2.HasValue)
                    {
                        num2 = beanLocalMetalLossResult.RSF;
                        arg_10CB_0 = (num2.GetValueOrDefault() > 2.3 || !num2.HasValue);
                    }
                    else
                    {
                        arg_10CB_0 = true;
                    }
                    if (!arg_10CB_0)
                    {
                        beanLocalMetalLossResult.CheckTSF = new bool?(true);
                    }
                    else
                    {
                        beanLocalMetalLossResult.CheckTSF = new bool?(false);
                    }
                    if (beanLocalMetalLossResult.CheckLampdaC.Value && beanLocalMetalLossResult.CheckDtc.Value && beanLocalMetalLossResult.CheckRSF.Value && beanLocalMetalLossResult.CheckTSF.Value)
                    {
                        beanLocalMetalLossResult.CheckFlawCir = new bool?(true);
                    }
                    else
                    {
                        beanLocalMetalLossResult.CheckFlawCir = new bool?(false);
                    }
                    double[,] array = new double[8, 9];
                    array[0, 0] = this.getExpFromString("7.0000E-01");
                    array[0, 1] = this.getExpFromString("7.5000E-01");
                    array[0, 2] = this.getExpFromString("8.0000E-01");
                    array[0, 3] = this.getExpFromString("9.0000E-01");
                    array[0, 4] = this.getExpFromString("1.0000E+00");
                    array[0, 5] = this.getExpFromString("1.2000E+00");
                    array[0, 6] = this.getExpFromString("1.4000E+00");
                    array[0, 7] = this.getExpFromString("1.8000E+00");
                    array[0, 8] = this.getExpFromString("2.3000E+00");
                    array[1, 0] = this.getExpFromString("2.1000E-01");
                    array[1, 1] = this.getExpFromString("4.8000E-01");
                    array[1, 2] = this.getExpFromString("6.7000E-01");
                    array[1, 3] = this.getExpFromString("9.8000E-01");
                    array[1, 4] = this.getExpFromString("1.2300E+00");
                    array[1, 5] = this.getExpFromString("1.6600E+00");
                    array[1, 6] = this.getExpFromString("2.0300E+00");
                    array[1, 7] = this.getExpFromString("2.6600E+00");
                    array[1, 8] = this.getExpFromString("3.3500E+00");
                    array[2, 0] = this.getExpFromString("9.9221E-01");
                    array[2, 1] = this.getExpFromString("9.6801E-01");
                    array[2, 2] = this.getExpFromString("9.4413E-01");
                    array[2, 3] = this.getExpFromString("8.9962E-01");
                    array[2, 4] = this.getExpFromString("8.5947E-01");
                    array[2, 5] = this.getExpFromString("7.8654E-01");
                    array[2, 6] = this.getExpFromString("7.2335E-01");
                    array[2, 7] = this.getExpFromString("6.0737E-01");
                    array[2, 8] = this.getExpFromString("4.9304E-01");
                    array[3, 0] = this.getExpFromString("-1.1959E-01");
                    array[3, 1] = this.getExpFromString("-2.3780E-01");
                    array[3, 2] = this.getExpFromString("-3.1256E-01");
                    array[3, 3] = this.getExpFromString("-3.8860E-01");
                    array[3, 4] = this.getExpFromString("-4.0012E-01");
                    array[3, 5] = this.getExpFromString("-2.5322E-01");
                    array[3, 6] = this.getExpFromString("1.1528E-02");
                    array[3, 7] = this.getExpFromString("9.3796E-01");
                    array[3, 8] = this.getExpFromString("2.1692E+00");
                    array[4, 0] = this.getExpFromString("-5.7333E-02");
                    array[4, 1] = this.getExpFromString("-3.2678E-01");
                    array[4, 2] = this.getExpFromString("-6.9968E-01");
                    array[4, 3] = this.getExpFromString("-1.6485E+00");
                    array[4, 4] = this.getExpFromString("-2.7979E+00");
                    array[4, 5] = this.getExpFromString("-5.7982E+00");
                    array[4, 6] = this.getExpFromString("-9.3536E+00");
                    array[4, 7] = this.getExpFromString("-1.9239E+01");
                    array[4, 8] = this.getExpFromString("-3.2459E+01");
                    array[5, 0] = this.getExpFromString("1.6948E-02");
                    array[5, 1] = this.getExpFromString("2.0684E-01");
                    array[5, 2] = this.getExpFromString("6.5020E-01");
                    array[5, 3] = this.getExpFromString("2.3445E+00");
                    array[5, 4] = this.getExpFromString("5.0729E+00");
                    array[5, 5] = this.getExpFromString("1.3858E+01");
                    array[5, 6] = this.getExpFromString("2.6031E+01");
                    array[5, 7] = this.getExpFromString("6.4267E+01");
                    array[5, 8] = this.getExpFromString("1.2245E+02");
                    array[6, 0] = this.getExpFromString("-1.7976E-03");
                    array[6, 1] = this.getExpFromString("-4.6537E-02");
                    array[6, 2] = this.getExpFromString("-2.2102E-01");
                    array[6, 3] = this.getExpFromString("-1.2534E+00");
                    array[6, 4] = this.getExpFromString("-3.5217E+00");
                    array[6, 5] = this.getExpFromString("-1.3118E+01");
                    array[6, 6] = this.getExpFromString("-2.9372E+01");
                    array[6, 7] = this.getExpFromString("-9.1307E+01");
                    array[6, 8] = this.getExpFromString("-2.0243E+02");
                    array[7, 0] = this.getExpFromString("6.9114E-05");
                    array[7, 1] = this.getExpFromString("3.9436E-03");
                    array[7, 2] = this.getExpFromString("2.8799E-02");
                    array[7, 3] = this.getExpFromString("2.5331E-01");
                    array[7, 4] = this.getExpFromString("9.1877E-01");
                    array[7, 5] = this.getExpFromString("4.6436E+00");
                    array[7, 6] = this.getExpFromString("1.2387E+01");
                    array[7, 7] = this.getExpFromString("4.8962E+01");
                    array[7, 8] = this.getExpFromString("1.2727E+02");
                    beanLocalMetalLossResult.UpperIndex = 1;
                    for (i = 1; i <= 7; i++)
                    {
                        num2 = beanLocalMetalLossResult.TSF;
                        num = array[0, i];
                        if (num2.GetValueOrDefault() >= num && num2.HasValue)
                        {
                            beanLocalMetalLossResult.UpperIndex++;
                        }
                    }
                    beanLocalMetalLossResult.LowerIndex = beanLocalMetalLossResult.UpperIndex - 1;
                    num2 = beanLocalMetalLossResult.lampdaC;
                    num = array[1, beanLocalMetalLossResult.UpperIndex];
                    if (num2.GetValueOrDefault() <= num && num2.HasValue)
                    {
                        beanLocalMetalLossResult.RtUp = new double?(0.2);
                    }
                    else
                    {
                        double num17 = 0.0;
                        for (i = 1; i <= 7; i++)
                        {
                            num17 += array[i, beanLocalMetalLossResult.UpperIndex] / Math.Pow(beanLocalMetalLossResult.lampdaC.Value, (double)i);
                        }
                        beanLocalMetalLossResult.RtUp = new double?(num17);
                    }
                    num2 = beanLocalMetalLossResult.lampdaC;
                    num = array[1, beanLocalMetalLossResult.LowerIndex];
                    if (num2.GetValueOrDefault() <= num && num2.HasValue)
                    {
                        beanLocalMetalLossResult.RtUp = new double?(0.2);
                    }
                    else
                    {
                        double num17 = 0.0;
                        for (i = 1; i <= 7; i++)
                        {
                            num17 += array[i, beanLocalMetalLossResult.LowerIndex] / Math.Pow(beanLocalMetalLossResult.lampdaC.Value, (double)i);
                        }
                        beanLocalMetalLossResult.RtLow = new double?(num17);
                    }
                    BeanLocalMetalLossResult arg_19D8_0 = beanLocalMetalLossResult;
                    num2 = beanLocalMetalLossResult.RtLow;
                    num3 = beanLocalMetalLossResult.TSF;
                    num = array[0, beanLocalMetalLossResult.LowerIndex];
                    num3 = (num3.HasValue ? new double?(num3.GetValueOrDefault() - num) : null);
                    num = array[0, beanLocalMetalLossResult.UpperIndex] - array[0, beanLocalMetalLossResult.LowerIndex];
                    num3 = (num3.HasValue ? new double?(num3.GetValueOrDefault() / num) : null);
                    num15 = beanLocalMetalLossResult.RtUp;
                    double? rtLow = beanLocalMetalLossResult.RtLow;
                    num15 = ((num15.HasValue & rtLow.HasValue) ? new double?(num15.GetValueOrDefault() - rtLow.GetValueOrDefault()) : null);
                    num3 = ((num3.HasValue & num15.HasValue) ? new double?(num3.GetValueOrDefault() * num15.GetValueOrDefault()) : null);
                    arg_19D8_0.RtCir = ((num2.HasValue & num3.HasValue) ? new double?(num2.GetValueOrDefault() + num3.GetValueOrDefault()) : null);
                    bool arg_1A29_0;
                    if (beanLocalMetalLossResult.CheckFlawCir.Value)
                    {
                        num2 = beanLocalMetalLossResult.rt;
                        num3 = beanLocalMetalLossResult.RtCir;
                        arg_1A29_0 = (num2.GetValueOrDefault() < num3.GetValueOrDefault() || !(num2.HasValue & num3.HasValue));
                    }
                    else
                    {
                        arg_1A29_0 = true;
                    }
                    if (!arg_1A29_0)
                    {
                        beanLocalMetalLossResult.CheckCircumferentialFlaw = new bool?(true);
                    }
                    else
                    {
                        beanLocalMetalLossResult.CheckCircumferentialFlaw = new bool?(false);
                    }
                }
                else
                {
                    beanLocalMetalLossResult.CheckCircumferentialFlaw = new bool?(true);
                }
                if (p_beanLocalMetalLoss.componentShapeID == 1 && beanLocalMetalLossResult.CheckLongitudinalFlaw.Value && beanLocalMetalLossResult.CheckCircumferentialFlaw.Value)
                {
                    beanLocalMetalLossResult.result = "The Level 2 assessment criteria are satisfied.";
                    beanLocalMetalLossResult.resultBool = true;
                }
                else if (p_beanLocalMetalLoss.componentShapeID == 1 && !beanLocalMetalLossResult.CheckLongitudinalFlaw.Value && beanLocalMetalLossResult.CheckCircumferentialFlaw.Value)
                {
                    beanLocalMetalLossResult.result = "The Level 2 assessment criteria are  not satisfied.";
                    beanLocalMetalLossResult.resultBool = false;
                }
                else if (p_beanLocalMetalLoss.componentShapeID == 1 && beanLocalMetalLossResult.CheckLongitudinalFlaw.Value && !beanLocalMetalLossResult.CheckCircumferentialFlaw.Value)
                {
                    beanLocalMetalLossResult.result = "The Level 2 assessment criteria are  not satisfied.";
                    beanLocalMetalLossResult.resultBool = false;
                }
                else if (p_beanLocalMetalLoss.componentShapeID == 2 && beanLocalMetalLossResult.CheckLongitudinalFlaw.Value)
                {
                    beanLocalMetalLossResult.result = "The Level 2 assessment criteria are satisfied.";
                    beanLocalMetalLossResult.resultBool = true;
                }
                else
                {
                    beanLocalMetalLossResult.result = "The Level 2 assessment criteria are not satisfied.";
                    beanLocalMetalLossResult.resultBool = false;
                }
                return beanLocalMetalLossResult;
            }

            private  double getExpFromString(string p_strNumber)
            {
                return double.Parse(p_strNumber, NumberStyles.Float, CultureInfo.InvariantCulture);
            }
        }

    }
}
