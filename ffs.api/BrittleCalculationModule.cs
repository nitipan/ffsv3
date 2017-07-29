using ffs.api.Model;
using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Extensions;
using Nancy.ModelBinding;

namespace ffs.api
{
    public class BrittleCalculationModule : NancyModule
    {

        public BrittleCalculationModule() : base("/api/brittle")
        {
            Post["/calculation/level{level}/unit{unit}"] = x =>
            {

                var level = (int)x.level;
                var unit = (int)x.unit;

                BrittleCalculator calculator = new BrittleCalculator(new GlobalVar(unit));

                var beanBrittle = this.Bind<BeanBrittle>();

                var result = level == 1 ? calculator.calculateLevel1(beanBrittle) : calculator.calculateLevel2(beanBrittle);

                return Response.AsJson(result);
            };
        }

        internal class BrittleCalculator : CalculatorBase
        {
            public BrittleCalculator(GlobalVar var) : base(var)
            {

            }
            private object[,] resultDataGrid = new object[17, 4];

            public BeanBrittleResult calculateLevel1(BeanBrittle p_beanBrittle)
            {
                BeanBrittleResult beanBrittleResult = new BeanBrittleResult();
                beanBrittleResult.tg = p_beanBrittle.TheUncorrodedGoverningThickness;
                double? num;
                if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_SI)
                {
                    BeanBrittleResult arg_5C_0 = beanBrittleResult;
                    num = beanBrittleResult.tg;
                    arg_5C_0.t = (num.HasValue ? new double?(num.GetValueOrDefault() * 0.039) : null);
                }
                else
                {
                    beanBrittleResult.t = beanBrittleResult.tg;
                }
                bool arg_E2_0;
                if (p_beanBrittle.asmeExemptionCurvesID == 1)
                {
                    num = beanBrittleResult.t;
                    if (num.GetValueOrDefault() > 0.0 && num.HasValue)
                    {
                        num = beanBrittleResult.t;
                        arg_E2_0 = (num.GetValueOrDefault() > 0.394 || !num.HasValue);
                        goto IL_E2;
                    }
                }
                arg_E2_0 = true;
                IL_E2:
                double? num3;
                if (!arg_E2_0)
                {
                    beanBrittleResult.MAT = new double?(18.0);
                }
                else
                {
                    bool arg_170_0;
                    if (p_beanBrittle.asmeExemptionCurvesID == 1)
                    {
                        num = beanBrittleResult.t;
                        if (num.GetValueOrDefault() > 0.394 && num.HasValue)
                        {
                            num = beanBrittleResult.t;
                            arg_170_0 = (num.GetValueOrDefault() > 6.0 || !num.HasValue);
                            goto IL_170;
                        }
                    }
                    arg_170_0 = true;
                    IL_170:
                    if (!arg_170_0)
                    {
                        BeanBrittleResult arg_312_0 = beanBrittleResult;
                        num = beanBrittleResult.t;
                        num = (num.HasValue ? new double?(284.85 * num.GetValueOrDefault()) : null);
                        num = (num.HasValue ? new double?(-76.911 + num.GetValueOrDefault()) : null);
                        double num2 = 27.56 * Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 2.0);
                        num = (num.HasValue ? new double?(num.GetValueOrDefault() - num2) : null);
                        num3 = beanBrittleResult.t;
                        num3 = (num3.HasValue ? new double?(1.797 * num3.GetValueOrDefault()) : null);
                        num3 = (num3.HasValue ? new double?(1.0 + num3.GetValueOrDefault()) : null);
                        num2 = 0.17887 * Math.Pow(beanBrittleResult.t.Value, 2.0);
                        num3 = (num3.HasValue ? new double?(num3.GetValueOrDefault() - num2) : null);
                        arg_312_0.MAT = ((num.HasValue & num3.HasValue) ? new double?(num.GetValueOrDefault() / num3.GetValueOrDefault()) : null);
                    }
                    else
                    {
                        bool arg_38C_0;
                        if (p_beanBrittle.asmeExemptionCurvesID == 2)
                        {
                            num = beanBrittleResult.t;
                            if (num.GetValueOrDefault() > 0.0 && num.HasValue)
                            {
                                num = beanBrittleResult.t;
                                arg_38C_0 = (num.GetValueOrDefault() > 0.394 || !num.HasValue);
                                goto IL_38C;
                            }
                        }
                        arg_38C_0 = true;
                        IL_38C:
                        if (!arg_38C_0)
                        {
                            beanBrittleResult.MAT = new double?(-20.0);
                        }
                        else
                        {
                            bool arg_41A_0;
                            if (p_beanBrittle.asmeExemptionCurvesID == 2)
                            {
                                num = beanBrittleResult.t;
                                if (num.GetValueOrDefault() > 0.394 && num.HasValue)
                                {
                                    num = beanBrittleResult.t;
                                    arg_41A_0 = (num.GetValueOrDefault() > 6.0 || !num.HasValue);
                                    goto IL_41A;
                                }
                            }
                            arg_41A_0 = true;
                            IL_41A:
                            if (!arg_41A_0)
                            {
                                BeanBrittleResult arg_59F_0 = beanBrittleResult;
                                double num2 = -135.79 + 171.56 * Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 0.5);
                                num = beanBrittleResult.t;
                                num = (num.HasValue ? new double?(103.63 * num.GetValueOrDefault()) : null);
                                num = (num.HasValue ? new double?(num2 + num.GetValueOrDefault()) : null);
                                num2 = 172.0 * Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 1.5);
                                num = (num.HasValue ? new double?(num.GetValueOrDefault() - num2) : null);
                                num2 = 73.737 * Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 2.0);
                                num = (num.HasValue ? new double?(num.GetValueOrDefault() + num2) : null);
                                num2 = 10.535 * Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 2.5);
                                arg_59F_0.MAT = (num.HasValue ? new double?(num.GetValueOrDefault() - num2) : null);
                            }
                            else
                            {
                                bool arg_619_0;
                                if (p_beanBrittle.asmeExemptionCurvesID == 3)
                                {
                                    num = beanBrittleResult.t;
                                    if (num.GetValueOrDefault() > 0.0 && num.HasValue)
                                    {
                                        num = beanBrittleResult.t;
                                        arg_619_0 = (num.GetValueOrDefault() > 0.394 || !num.HasValue);
                                        goto IL_619;
                                    }
                                }
                                arg_619_0 = true;
                                IL_619:
                                if (!arg_619_0)
                                {
                                    beanBrittleResult.MAT = new double?(-55.0);
                                }
                                else
                                {
                                    bool arg_6A7_0;
                                    if (p_beanBrittle.asmeExemptionCurvesID == 3)
                                    {
                                        num = beanBrittleResult.t;
                                        if (num.GetValueOrDefault() > 0.394 && num.HasValue)
                                        {
                                            num = beanBrittleResult.t;
                                            arg_6A7_0 = (num.GetValueOrDefault() > 6.0 || !num.HasValue);
                                            goto IL_6A7;
                                        }
                                    }
                                    arg_6A7_0 = true;
                                    IL_6A7:
                                    if (!arg_6A7_0)
                                    {
                                        BeanBrittleResult arg_7DB_0 = beanBrittleResult;
                                        num = beanBrittleResult.t;
                                        num = (num.HasValue ? new double?(255.5 / num.GetValueOrDefault()) : null);
                                        num = (num.HasValue ? new double?(101.29 - num.GetValueOrDefault()) : null);
                                        double num2 = 287.86 / Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 2.0) - (196.42 / Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 3.0) + 69.457 / Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 4.0) - 9.8082 / Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 5.0));
                                        arg_7DB_0.MAT = (num.HasValue ? new double?(num.GetValueOrDefault() + num2) : null);
                                    }
                                    else
                                    {
                                        bool arg_855_0;
                                        if (p_beanBrittle.asmeExemptionCurvesID == 4)
                                        {
                                            num = beanBrittleResult.tg;
                                            if (num.GetValueOrDefault() > 0.0 && num.HasValue)
                                            {
                                                num = beanBrittleResult.tg;
                                                arg_855_0 = (num.GetValueOrDefault() > 0.5 || !num.HasValue);
                                                goto IL_855;
                                            }
                                        }
                                        arg_855_0 = true;
                                        IL_855:
                                        if (!arg_855_0)
                                        {
                                            beanBrittleResult.MAT = new double?(-55.0);
                                        }
                                        else
                                        {
                                            bool arg_8E3_0;
                                            if (p_beanBrittle.asmeExemptionCurvesID == 4)
                                            {
                                                num = beanBrittleResult.t;
                                                if (num.GetValueOrDefault() > 0.5 && num.HasValue)
                                                {
                                                    num = beanBrittleResult.t;
                                                    arg_8E3_0 = (num.GetValueOrDefault() > 6.0 || !num.HasValue);
                                                    goto IL_8E3;
                                                }
                                            }
                                            arg_8E3_0 = true;
                                            IL_8E3:
                                            if (!arg_8E3_0)
                                            {
                                                BeanBrittleResult arg_A8C_0 = beanBrittleResult;
                                                num = beanBrittleResult.t;
                                                num = (num.HasValue ? new double?(94.065 * num.GetValueOrDefault()) : null);
                                                num = (num.HasValue ? new double?(-92.965 + num.GetValueOrDefault()) : null);
                                                double num2 = 39.812 * Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 2.0);
                                                num = (num.HasValue ? new double?(num.GetValueOrDefault() - num2) : null);
                                                num2 = 9.6838 * Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 3.0);
                                                num = (num.HasValue ? new double?(num.GetValueOrDefault() + num2) : null);
                                                num2 = 1.1698 * Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 4.0);
                                                num = (num.HasValue ? new double?(num.GetValueOrDefault() - num2) : null);
                                                num2 = 0.054687 * Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 5.0);
                                                arg_A8C_0.MAT = (num.HasValue ? new double?(num.GetValueOrDefault() + num2) : null);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (p_beanBrittle.Fabricated.GetValueOrDefault() && p_beanBrittle.WallThickness38.GetValueOrDefault() && p_beanBrittle.PWHT.GetValueOrDefault())
                {
                    BeanBrittleResult arg_B03_0 = beanBrittleResult;
                    num = beanBrittleResult.MAT;
                    arg_B03_0.MATreduce = (num.HasValue ? new double?(num.GetValueOrDefault() - 30.0) : null);
                }
                else
                {
                    beanBrittleResult.MATreduce = beanBrittleResult.MAT;
                }
                if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_MATRIC && p_beanBrittle.AutomaticcallyTheMinimumAllowableTemperature.GetValueOrDefault())
                {
                    beanBrittleResult.MATlv = beanBrittleResult.MATreduce;
                }
                else if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_SI && p_beanBrittle.AutomaticcallyTheMinimumAllowableTemperature.GetValueOrDefault())
                {
                    BeanBrittleResult arg_BDB_0 = beanBrittleResult;
                    num = beanBrittleResult.MATreduce;
                    num = (num.HasValue ? new double?(num.GetValueOrDefault() - 32.0) : null);
                    arg_BDB_0.MATlv = (num.HasValue ? new double?(num.GetValueOrDefault() * 0.0) : null);
                }
                else if (p_beanBrittle.AutomaticcallyTheMinimumAllowableTemperature.GetValueOrDefault())
                {
                    beanBrittleResult.MATlv = new double?(1.0);
                }
                else
                {
                    beanBrittleResult.MATlv = p_beanBrittle.TheMinimumAllowableTemperature;
                }
                num = beanBrittleResult.MATlv;
                num3 = p_beanBrittle.TheCriticalExposureTemperature;
                if (num.GetValueOrDefault() <= num3.GetValueOrDefault() && (num.HasValue & num3.HasValue))
                {
                    beanBrittleResult.result = "The Level 1 assessment criteria are satisfied.";
                    beanBrittleResult.resultBool = true;
                }
                else
                {
                    beanBrittleResult.result = "The Level 1 assessment criteria are  not satisfied.";
                    beanBrittleResult.resultBool = false;
                }
                return beanBrittleResult;
            }

            public BeanBrittleResult calculateLevel2(BeanBrittle p_beanBrittle)
            {
                BeanBrittleResult beanBrittleResult = new BeanBrittleResult();
                beanBrittleResult.tg = p_beanBrittle.TheUncorrodedGoverningThickness;
                if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_SI)
                {
                    BeanBrittleResult arg_5F_0 = beanBrittleResult;
                    double? num = beanBrittleResult.tg;
                    arg_5F_0.t = (num.HasValue ? new double?(num.GetValueOrDefault() * 0.039) : null);
                }
                else
                {
                    beanBrittleResult.t = beanBrittleResult.tg;
                }
                bool arg_E7_0;
                if (p_beanBrittle.asmeExemptionCurvesID == 1)
                {
                    double? num = beanBrittleResult.t;
                    if (num.GetValueOrDefault() > 0.0 && num.HasValue)
                    {
                        num = beanBrittleResult.t;
                        arg_E7_0 = (num.GetValueOrDefault() > 0.394 || !num.HasValue);
                        goto IL_E7;
                    }
                }
                arg_E7_0 = true;
                IL_E7:
                if (!arg_E7_0)
                {
                    beanBrittleResult.MAT = new double?(18.0);
                }
                else
                {
                    bool arg_179_0;
                    if (p_beanBrittle.asmeExemptionCurvesID == 1)
                    {
                        double? num = beanBrittleResult.t;
                        if (num.GetValueOrDefault() > 0.394 && num.HasValue)
                        {
                            num = beanBrittleResult.t;
                            arg_179_0 = (num.GetValueOrDefault() > 6.0 || !num.HasValue);
                            goto IL_179;
                        }
                    }
                    arg_179_0 = true;
                    IL_179:
                    if (!arg_179_0)
                    {
                        BeanBrittleResult arg_321_0 = beanBrittleResult;
                        double? num = beanBrittleResult.t;
                        num = (num.HasValue ? new double?(284.85 * num.GetValueOrDefault()) : null);
                        num = (num.HasValue ? new double?(-76.911 + num.GetValueOrDefault()) : null);
                        double num2 = 27.56 * Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 2.0);
                        num = (num.HasValue ? new double?(num.GetValueOrDefault() - num2) : null);
                        double? num3 = beanBrittleResult.t;
                        num3 = (num3.HasValue ? new double?(1.797 * num3.GetValueOrDefault()) : null);
                        num3 = (num3.HasValue ? new double?(1.0 + num3.GetValueOrDefault()) : null);
                        num2 = 0.17887 * Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 2.0);
                        num3 = (num3.HasValue ? new double?(num3.GetValueOrDefault() - num2) : null);
                        arg_321_0.MAT = ((num.HasValue & num3.HasValue) ? new double?(num.GetValueOrDefault() / num3.GetValueOrDefault()) : null);
                    }
                    else
                    {
                        bool arg_39D_0;
                        if (p_beanBrittle.asmeExemptionCurvesID == 2)
                        {
                            double? num = beanBrittleResult.t;
                            if (num.GetValueOrDefault() > 0.0 && num.HasValue)
                            {
                                num = beanBrittleResult.t;
                                arg_39D_0 = (num.GetValueOrDefault() > 0.394 || !num.HasValue);
                                goto IL_39D;
                            }
                        }
                        arg_39D_0 = true;
                        IL_39D:
                        if (!arg_39D_0)
                        {
                            beanBrittleResult.MAT = new double?(-20.0);
                        }
                        else
                        {
                            bool arg_42F_0;
                            if (p_beanBrittle.asmeExemptionCurvesID == 2)
                            {
                                double? num = beanBrittleResult.t;
                                if (num.GetValueOrDefault() > 0.394 && num.HasValue)
                                {
                                    num = beanBrittleResult.t;
                                    arg_42F_0 = (num.GetValueOrDefault() > 6.0 || !num.HasValue);
                                    goto IL_42F;
                                }
                            }
                            arg_42F_0 = true;
                            IL_42F:
                            if (!arg_42F_0)
                            {
                                BeanBrittleResult arg_5BC_0 = beanBrittleResult;
                                double num2 = -135.79 + 171.56 * Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 0.5);
                                double? num = beanBrittleResult.t;
                                num = (num.HasValue ? new double?(103.63 * num.GetValueOrDefault()) : null);
                                num = (num.HasValue ? new double?(num2 + num.GetValueOrDefault()) : null);
                                num2 = 172.0 * Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 1.5);
                                num = (num.HasValue ? new double?(num.GetValueOrDefault() - num2) : null);
                                num2 = 73.737 * Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 2.0);
                                num = (num.HasValue ? new double?(num.GetValueOrDefault() + num2) : null);
                                num2 = 10.535 * Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 2.5);
                                arg_5BC_0.MAT = (num.HasValue ? new double?(num.GetValueOrDefault() - num2) : null);
                            }
                            else
                            {
                                bool arg_638_0;
                                if (p_beanBrittle.asmeExemptionCurvesID == 3)
                                {
                                    double? num = beanBrittleResult.t;
                                    if (num.GetValueOrDefault() > 0.0 && num.HasValue)
                                    {
                                        num = beanBrittleResult.t;
                                        arg_638_0 = (num.GetValueOrDefault() > 0.394 || !num.HasValue);
                                        goto IL_638;
                                    }
                                }
                                arg_638_0 = true;
                                IL_638:
                                if (!arg_638_0)
                                {
                                    beanBrittleResult.MAT = new double?(-55.0);
                                }
                                else
                                {
                                    bool arg_6CA_0;
                                    if (p_beanBrittle.asmeExemptionCurvesID == 3)
                                    {
                                        double? num = beanBrittleResult.t;
                                        if (num.GetValueOrDefault() > 0.394 && num.HasValue)
                                        {
                                            num = beanBrittleResult.t;
                                            arg_6CA_0 = (num.GetValueOrDefault() > 6.0 || !num.HasValue);
                                            goto IL_6CA;
                                        }
                                    }
                                    arg_6CA_0 = true;
                                    IL_6CA:
                                    if (!arg_6CA_0)
                                    {
                                        BeanBrittleResult arg_803_0 = beanBrittleResult;
                                        double? num = beanBrittleResult.t;
                                        num = (num.HasValue ? new double?(255.5 / num.GetValueOrDefault()) : null);
                                        num = (num.HasValue ? new double?(101.29 - num.GetValueOrDefault()) : null);
                                        double num2 = 287.86 / Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 2.0) - (196.42 / Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 3.0) + 69.457 / Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 4.0) - 9.8082 / Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 5.0));
                                        arg_803_0.MAT = (num.HasValue ? new double?(num.GetValueOrDefault() + num2) : null);
                                    }
                                    else
                                    {
                                        bool arg_87F_0;
                                        if (p_beanBrittle.asmeExemptionCurvesID == 4)
                                        {
                                            double? num = beanBrittleResult.tg;
                                            if (num.GetValueOrDefault() > 0.0 && num.HasValue)
                                            {
                                                num = beanBrittleResult.tg;
                                                arg_87F_0 = (num.GetValueOrDefault() > 0.5 || !num.HasValue);
                                                goto IL_87F;
                                            }
                                        }
                                        arg_87F_0 = true;
                                        IL_87F:
                                        if (!arg_87F_0)
                                        {
                                            beanBrittleResult.MAT = new double?(-55.0);
                                        }
                                        else
                                        {
                                            bool arg_911_0;
                                            if (p_beanBrittle.asmeExemptionCurvesID == 4)
                                            {
                                                double? num = beanBrittleResult.t;
                                                if (num.GetValueOrDefault() > 0.5 && num.HasValue)
                                                {
                                                    num = beanBrittleResult.t;
                                                    arg_911_0 = (num.GetValueOrDefault() > 6.0 || !num.HasValue);
                                                    goto IL_911;
                                                }
                                            }
                                            arg_911_0 = true;
                                            IL_911:
                                            if (!arg_911_0)
                                            {
                                                BeanBrittleResult arg_AC2_0 = beanBrittleResult;
                                                double? num = beanBrittleResult.t;
                                                num = (num.HasValue ? new double?(94.065 * num.GetValueOrDefault()) : null);
                                                num = (num.HasValue ? new double?(-92.965 + num.GetValueOrDefault()) : null);
                                                double num2 = 39.812 * Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 2.0);
                                                num = (num.HasValue ? new double?(num.GetValueOrDefault() - num2) : null);
                                                num2 = 9.6838 * Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 3.0);
                                                num = (num.HasValue ? new double?(num.GetValueOrDefault() + num2) : null);
                                                num2 = 1.1698 * Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 4.0);
                                                num = (num.HasValue ? new double?(num.GetValueOrDefault() - num2) : null);
                                                num2 = 0.054687 * Math.Pow(beanBrittleResult.t.GetValueOrDefault(), 5.0);
                                                arg_AC2_0.MAT = (num.HasValue ? new double?(num.GetValueOrDefault() + num2) : null);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (p_beanBrittle.Fabricated.GetValueOrDefault() && p_beanBrittle.WallThickness38.GetValueOrDefault() && p_beanBrittle.PWHT.GetValueOrDefault())
                {
                    BeanBrittleResult arg_B3C_0 = beanBrittleResult;
                    double? num = beanBrittleResult.MAT;
                    arg_B3C_0.MATreduce = (num.HasValue ? new double?(num.GetValueOrDefault() - 30.0) : null);
                }
                else
                {
                    beanBrittleResult.MATreduce = beanBrittleResult.MAT;
                }
                beanBrittleResult.Loss = p_beanBrittle.loss;
                beanBrittleResult.FCA = p_beanBrittle.fca;
                beanBrittleResult.E = p_beanBrittle.weldJointEfficiency;
                beanBrittleResult.Estar = new double?(Math.Max(beanBrittleResult.E.GetValueOrDefault(), 0.8));
                double[] p_dblP_ratios = new double[]
                {
                1.0,
                0.95,
                0.9,
                0.85,
                0.8,
                0.75,
                0.7,
                0.65,
                0.6,
                0.55,
                0.5,
                0.45,
                0.4,
                0.35,
                0.3,
                0.25,
                0.2
                };
                bool[] resultLV = this.getResultLV2(p_dblP_ratios, p_beanBrittle, beanBrittleResult);
                beanBrittleResult.resultDataGrid = this.resultDataGrid;
                return beanBrittleResult;
            }

            private bool[] getResultLV2(double[] p_dblP_ratios, BeanBrittle p_beanBrittle, BeanBrittleResult p_beanBrittleResult)
            {
                bool[] array = new bool[17];
                for (int i = 0; i < p_dblP_ratios.Length; i++)
                {
                    double mATLV = this.getMATLV2(i, p_dblP_ratios[i] * p_beanBrittle.designPressure.GetValueOrDefault(), p_beanBrittle, p_beanBrittleResult);
                    double? theCriticalExposureTemperature = p_beanBrittle.TheCriticalExposureTemperature;
                    if (mATLV <= theCriticalExposureTemperature.GetValueOrDefault() && theCriticalExposureTemperature.HasValue)
                    {
                        array[i] = true;
                        this.resultDataGrid[i, 3] = "Satisfied.";
                    }
                    else
                    {
                        array[i] = false;
                        this.resultDataGrid[i, 3] = "Not Satisfied.";
                    }
                }
                return array;
            }

            private double getMATLV2(int rowIndex, double p, BeanBrittle p_beanBrittle, BeanBrittleResult p_beanBrittleResult)
            {
                double num = 0.0;
                double num2 = Math.Pow(10.0, 6.0);
                double num3 = 6.895 * Math.Pow(10.0, 3.0);
                double num4 = 6.895 * Math.Pow(10.0, 6.0);
                this.resultDataGrid[rowIndex, 0] = this.getDisplayValue(new double?(p));
                double num5;
                if (p_beanBrittle.ReductionInTheMATID == 1)
                {
                    num5 = p / p_beanBrittle.designPressure.GetValueOrDefault();
                }
                else if (p_beanBrittle.ReductionInTheMATID == 2)
                {
                    double? num6 = p_beanBrittle.insideDiameter;
                    num6 = (num6.HasValue ? new double?(num6.GetValueOrDefault() / 2.0) : null);
                    double? num7 = p_beanBrittle.fca;
                    num6 = ((num6.HasValue & num7.HasValue) ? new double?(num6.GetValueOrDefault() + num7.GetValueOrDefault()) : null);
                    num7 = p_beanBrittle.loss;
                    double num8 = ((num6.HasValue & num7.HasValue) ? new double?(num6.GetValueOrDefault() + num7.GetValueOrDefault()) : null).GetValueOrDefault();
                    num6 = p_beanBrittle.nominalThickness;
                    num7 = p_beanBrittle.fca;
                    num6 = ((num6.HasValue & num7.HasValue) ? new double?(num6.GetValueOrDefault() - num7.GetValueOrDefault()) : null);
                    num7 = p_beanBrittle.loss;
                    double num9 = ((num6.HasValue & num7.HasValue) ? new double?(num6.GetValueOrDefault() - num7.GetValueOrDefault()) : null).GetValueOrDefault();
                    double num10 = p * (num8 / num9 + 0.6);
                    double num11 = num10;
                    num6 = p_beanBrittleResult.Estar;
                    num6 = (num6.HasValue ? new double?(num11 * num6.GetValueOrDefault()) : null);
                    num7 = p_beanBrittle.allowableStress;
                    double? e = p_beanBrittleResult.E;
                    num7 = ((num7.HasValue & e.HasValue) ? new double?(num7.GetValueOrDefault() * e.GetValueOrDefault()) : null);
                    num5 = ((num6.HasValue & num7.HasValue) ? new double?(num6.GetValueOrDefault() / num7.GetValueOrDefault()) : null).GetValueOrDefault();
                }
                else
                {
                    double? num6 = p_beanBrittle.insideDiameter;
                    num6 = (num6.HasValue ? new double?(num6.GetValueOrDefault() / 2.0) : null);
                    double? num7 = p_beanBrittle.fca;
                    num6 = ((num6.HasValue & num7.HasValue) ? new double?(num6.GetValueOrDefault() + num7.GetValueOrDefault()) : null);
                    num7 = p_beanBrittle.loss;
                    double num8 = ((num6.HasValue & num7.HasValue) ? new double?(num6.GetValueOrDefault() + num7.GetValueOrDefault()) : null).GetValueOrDefault();
                    num6 = p_beanBrittle.nominalThickness;
                    num7 = p_beanBrittle.fca;
                    num6 = ((num6.HasValue & num7.HasValue) ? new double?(num6.GetValueOrDefault() - num7.GetValueOrDefault()) : null);
                    num7 = p_beanBrittle.loss;
                    double num9 = ((num6.HasValue & num7.HasValue) ? new double?(num6.GetValueOrDefault() - num7.GetValueOrDefault()) : null).GetValueOrDefault();
                    double num11 = p * num8;
                    num6 = p_beanBrittle.allowableStress;
                    num7 = p_beanBrittleResult.E;
                    num6 = ((num6.HasValue & num7.HasValue) ? new double?(num6.GetValueOrDefault() * num7.GetValueOrDefault()) : null);
                    double num12 = 0.6 * p;
                    num6 = (num6.HasValue ? new double?(num6.GetValueOrDefault() - num12) : null);
                    double val = (num6.HasValue ? new double?(num11 / num6.GetValueOrDefault()) : null).GetValueOrDefault();
                    num11 = p * num8;
                    num6 = p_beanBrittle.allowableStress;
                    num6 = (num6.HasValue ? new double?(2.0 * num6.GetValueOrDefault()) : null);
                    num7 = p_beanBrittleResult.E;
                    num6 = ((num6.HasValue & num7.HasValue) ? new double?(num6.GetValueOrDefault() * num7.GetValueOrDefault()) : null);
                    num12 = 0.4 * p;
                    num6 = (num6.HasValue ? new double?(num6.GetValueOrDefault() + num12) : null);
                    double val2 = (num6.HasValue ? new double?(num11 / num6.GetValueOrDefault()) : null).GetValueOrDefault();
                    double num13 = Math.Max(val, val2);
                    num11 = num13;
                    num6 = p_beanBrittleResult.Estar;
                    num6 = (num6.HasValue ? new double?(num11 * num6.GetValueOrDefault()) : null);
                    num7 = p_beanBrittle.allowableStress;
                    double? e = p_beanBrittleResult.E;
                    num7 = ((num7.HasValue & e.HasValue) ? new double?(num7.GetValueOrDefault() * e.GetValueOrDefault()) : null);
                    num5 = ((num6.HasValue & num7.HasValue) ? new double?(num6.GetValueOrDefault() / num7.GetValueOrDefault()) : null).GetValueOrDefault();
                }
                double num14;
                if (p_beanBrittle.unitID == GlobalVar.UNIT_SI)
                {
                    double? num6 = p_beanBrittle.allowableStress;
                    double num11 = num2;
                    num14 = (num6.HasValue ? new double?(num6.GetValueOrDefault() * num11) : null).GetValueOrDefault();
                }
                else
                {
                    double? num6 = p_beanBrittle.allowableStress;
                    double num11 = num3;
                    num14 = (num6.HasValue ? new double?(num6.GetValueOrDefault() * num11) : null).GetValueOrDefault();
                }
                double num15;
                double num16;
                if (num14 <= 17.5 * num4)
                {
                    num15 = 0.4;
                    num16 = 105.0;
                }
                else if (17.5 * num4 < num14 && num14 <= 20.0 * num4)
                {
                    num15 = 0.35;
                    num16 = 140.0;
                }
                else if (20.0 * num4 < num14 && num14 <= 25.0 * num4)
                {
                    num15 = 0.35;
                    num16 = 200.0;
                }
                else
                {
                    num15 = 0.0;
                    num16 = 0.0;
                }
                double num17;
                if (num5 <= num15)
                {
                    num17 = num16;
                }
                else if (num15 < num5 && num5 < 0.6)
                {
                    num17 = -9979.57 - 14125.0 * Math.Pow(num5, 1.5) + 9088.11 * Math.Exp(num5) - 17.3893 * (Math.Log(num5) / Math.Pow(num5, 2.0));
                }
                else if (num5 >= 0.6)
                {
                    num17 = 100.0 * (1.0 - num5);
                }
                else
                {
                    num17 = 0.0;
                }
                if (p_beanBrittle.unitID == GlobalVar.UNIT_SI)
                {
                    this.resultDataGrid[rowIndex, 1] = this.getDisplayValue(new double?((num17 - 32.0) * 0.55555555555555558));
                }
                else
                {
                    this.resultDataGrid[rowIndex, 1] = this.getDisplayValue(new double?(num17));
                }
                if (num5 <= num15)
                {
                    num = -155.0;
                }
                else if (num5 > num15)
                {
                    double? num6 = p_beanBrittleResult.MATreduce;
                    double num11 = num17;
                    num = Math.Max((num6.HasValue ? new double?(num6.GetValueOrDefault() - num11) : null).GetValueOrDefault(), -55.0);
                }
                if (p_beanBrittle.unitID == GlobalVar.UNIT_SI)
                {
                    this.resultDataGrid[rowIndex, 2] = this.getDisplayValue(new double?((num - 32.0) * 0.55555555555555558));
                }
                else
                {
                    this.resultDataGrid[rowIndex, 2] = this.getDisplayValue(new double?(num));
                }
                double result;
                if (p_beanBrittle.unitID == GlobalVar.UNIT_MATRIC)
                {
                    result = num;
                }
                else
                {
                    result = (num - 32.0) * 0.55555555555555558;
                }
                return result;
            }
        }

    }
}
