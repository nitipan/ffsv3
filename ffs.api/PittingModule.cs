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
    public class PittingModule : Nancy.NancyModule
    {
        public PittingModule() : base("/api/pitting")
        {
            Post["/calculation/level{level}/unit{unit}"] = x =>
            {

                var level = (int)x.level;
                var unit = (int)x.unit;

                PittingCalculator calculator = new PittingCalculator(new GlobalVar(unit));

                var input = this.Bind<BeanPitting>();

                var result = level == 1 ? calculator.calculateLevel1(input) : calculator.calculateLevel2(input);

                return Response.AsJson(result);
            };

        }
    }


    internal class PittingCalculator : CalculatorBase
    {

        public PittingCalculator(GlobalVar var) : base(var)
        {

        }
        public BeanPittingResult calculateLevel1(BeanPitting p_beanPitting)
        {
            BeanPittingResult beanPittingResult = new BeanPittingResult();
            beanPittingResult.d_1 = p_beanPitting.insideDiameter;
            beanPittingResult.fca_1 = p_beanPitting.fca;
            beanPittingResult.tnom_1 = p_beanPitting.nominalThickness;
            beanPittingResult.loss_1 = p_beanPitting.loss;
            BeanPittingResult arg_BC_0 = beanPittingResult;
            double? num = beanPittingResult.tnom_1;
            double? num2 = beanPittingResult.loss_1;
            num = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() - num2.GetValueOrDefault()) : null);
            num2 = beanPittingResult.fca_1;
            arg_BC_0.tc_2 = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() - num2.GetValueOrDefault()) : null);
            beanPittingResult.wmax_4 = p_beanPitting.theMaximumPitDepth;
            BeanPittingResult arg_18D_0 = beanPittingResult;
            num = beanPittingResult.tc_2;
            num2 = beanPittingResult.fca_1;
            num = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() + num2.GetValueOrDefault()) : null);
            num2 = beanPittingResult.wmax_4;
            num = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() - num2.GetValueOrDefault()) : null);
            num2 = beanPittingResult.tc_2;
            arg_18D_0.rwt_5 = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() / num2.GetValueOrDefault()) : null);
            BeanPittingResult arg_1C4_0 = beanPittingResult;
            num = beanPittingResult.rwt_5;
            arg_1C4_0.checkRwt_5 = new bool?(num.GetValueOrDefault() >= 0.2 && num.HasValue);
            num = beanPittingResult.d_1;
            num = (num.HasValue ? new double?(num.GetValueOrDefault() / 2.0) : null);
            num2 = beanPittingResult.fca_1;
            num = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() + num2.GetValueOrDefault()) : null);
            num2 = beanPittingResult.loss_1;
            double? num3 = (num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() + num2.GetValueOrDefault()) : null;
            BeanPittingResult arg_39D_0 = beanPittingResult;
            num = p_beanPitting.allowableStress;
            num2 = p_beanPitting.weldJointEfficiency;
            num = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() * num2.GetValueOrDefault()) : null);
            num2 = beanPittingResult.tc_2;
            num = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() * num2.GetValueOrDefault()) : null);
            num2 = num3;
            double? num4 = beanPittingResult.tc_2;
            num4 = (num4.HasValue ? new double?(0.6 * num4.GetValueOrDefault()) : null);
            num2 = ((num2.HasValue & num4.HasValue) ? new double?(num2.GetValueOrDefault() + num4.GetValueOrDefault()) : null);
            arg_39D_0.mawpC_6 = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() / num2.GetValueOrDefault()) : null);
            BeanPittingResult arg_4F5_0 = beanPittingResult;
            num = p_beanPitting.allowableStress;
            num = (num.HasValue ? new double?(2.0 * num.GetValueOrDefault()) : null);
            num2 = p_beanPitting.weldJointEfficiency;
            num = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() * num2.GetValueOrDefault()) : null);
            num2 = beanPittingResult.tc_2;
            num = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() * num2.GetValueOrDefault()) : null);
            num2 = num3;
            num4 = beanPittingResult.tc_2;
            num4 = (num4.HasValue ? new double?(0.4 * num4.GetValueOrDefault()) : null);
            num2 = ((num2.HasValue & num4.HasValue) ? new double?(num2.GetValueOrDefault() - num4.GetValueOrDefault()) : null);
            arg_4F5_0.mawpL_6 = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() / num2.GetValueOrDefault()) : null);
            beanPittingResult.mawp_6 = new double?(Math.Min(beanPittingResult.mawpC_6.Value, beanPittingResult.mawpL_6.Value));
            double[,] array = new double[4, 3];
            array[0, 0] = 0.8;
            array[0, 1] = 0.97;
            array[0, 2] = 0.96;
            array[1, 0] = 0.6;
            array[1, 1] = 0.95;
            array[1, 2] = 0.91;
            array[2, 0] = 0.4;
            array[2, 1] = 0.92;
            array[2, 2] = 0.87;
            array[3, 0] = 0.2;
            array[3, 1] = 0.89;
            array[3, 2] = 0.83;
            double[,] array2 = new double[4, 3];
            array2[0, 0] = 0.8;
            array2[0, 1] = 0.97;
            array2[0, 2] = 0.96;
            array2[1, 0] = 0.6;
            array2[1, 1] = 0.95;
            array2[1, 2] = 0.91;
            array2[2, 0] = 0.4;
            array2[2, 1] = 0.92;
            array2[2, 2] = 0.87;
            array2[3, 0] = 0.2;
            array2[3, 1] = 0.89;
            array2[3, 2] = 0.83;
            double[,] array3 = new double[4, 3];
            array3[0, 0] = 0.8;
            array3[0, 1] = 0.96;
            array3[0, 2] = 0.95;
            array3[1, 0] = 0.6;
            array3[1, 1] = 0.93;
            array3[1, 2] = 0.89;
            array3[2, 0] = 0.4;
            array3[2, 1] = 0.89;
            array3[2, 2] = 0.84;
            array3[3, 0] = 0.2;
            array3[3, 1] = 0.86;
            array3[3, 2] = 0.79;
            double[,] array4 = new double[4, 3];
            array4[0, 0] = 0.8;
            array4[0, 1] = 0.95;
            array4[0, 2] = 0.93;
            array4[1, 0] = 0.6;
            array4[1, 1] = 0.9;
            array4[1, 2] = 0.86;
            array4[2, 0] = 0.4;
            array4[2, 1] = 0.85;
            array4[2, 2] = 0.79;
            array4[3, 0] = 0.2;
            array4[3, 1] = 0.79;
            array4[3, 2] = 0.72;
            double[,] array5 = new double[4, 3];
            array5[0, 0] = 0.8;
            array5[0, 1] = 0.93;
            array5[0, 2] = 0.91;
            array5[1, 0] = 0.6;
            array5[1, 1] = 0.85;
            array5[1, 2] = 0.81;
            array5[2, 0] = 0.4;
            array5[2, 1] = 0.78;
            array5[2, 2] = 0.72;
            array5[3, 0] = 0.2;
            array5[3, 1] = 0.7;
            array5[3, 2] = 0.62;
            double[,] array6 = new double[4, 3];
            array6[0, 0] = 0.8;
            array6[0, 1] = 0.91;
            array6[0, 2] = 0.89;
            array6[1, 0] = 0.6;
            array6[1, 1] = 0.82;
            array6[1, 2] = 0.78;
            array6[2, 0] = 0.4;
            array6[2, 1] = 0.73;
            array6[2, 2] = 0.67;
            array6[3, 0] = 0.2;
            array6[3, 1] = 0.64;
            array6[3, 2] = 0.56;
            double[,] array7 = new double[4, 3];
            array7[0, 0] = 0.8;
            array7[0, 1] = 0.89;
            array7[0, 2] = 0.88;
            array7[1, 0] = 0.6;
            array7[1, 1] = 0.79;
            array7[1, 2] = 0.76;
            array7[2, 0] = 0.4;
            array7[2, 1] = 0.68;
            array7[2, 2] = 0.63;
            array7[3, 0] = 0.2;
            array7[3, 1] = 0.56;
            array7[3, 2] = 0.51;
            double[,] array8 = new double[4, 3];
            array8[0, 0] = 0.8;
            array8[0, 1] = 0.88;
            array8[0, 2] = 0.87;
            array8[1, 0] = 0.6;
            array8[1, 1] = 0.77;
            array8[1, 2] = 0.74;
            array8[2, 0] = 0.4;
            array8[2, 1] = 0.65;
            array8[2, 2] = 0.6;
            array8[3, 0] = 0.2;
            array8[3, 1] = 0.53;
            array8[3, 2] = 0.47;
            double[,] array9 = new double[4, 3];
            array9[0, 0] = 0.8;
            array9[0, 1] = 0.0;
            array9[0, 2] = 0.0;
            array9[1, 0] = 0.6;
            array9[1, 1] = 0.0;
            array9[1, 2] = 0.0;
            array9[2, 0] = 0.4;
            array9[2, 1] = 0.0;
            array9[2, 2] = 0.0;
            array9[3, 0] = 0.2;
            array9[3, 1] = 0.0;
            array9[3, 2] = 0.0;
            num = p_beanPitting.theStandardPitChart;
            double[,] array10;
            if (num.GetValueOrDefault() == 1.0 && num.HasValue)
            {
                array10 = array;
            }
            else
            {
                num = p_beanPitting.theStandardPitChart;
                if (num.GetValueOrDefault() == 2.0 && num.HasValue)
                {
                    array10 = array2;
                }
                else
                {
                    num = p_beanPitting.theStandardPitChart;
                    if (num.GetValueOrDefault() == 3.0 && num.HasValue)
                    {
                        array10 = array3;
                    }
                    else
                    {
                        num = p_beanPitting.theStandardPitChart;
                        if (num.GetValueOrDefault() == 4.0 && num.HasValue)
                        {
                            array10 = array4;
                        }
                        else
                        {
                            num = p_beanPitting.theStandardPitChart;
                            if (num.GetValueOrDefault() == 5.0 && num.HasValue)
                            {
                                array10 = array5;
                            }
                            else
                            {
                                num = p_beanPitting.theStandardPitChart;
                                if (num.GetValueOrDefault() == 6.0 && num.HasValue)
                                {
                                    array10 = array6;
                                }
                                else
                                {
                                    num = p_beanPitting.theStandardPitChart;
                                    if (num.GetValueOrDefault() == 7.0 && num.HasValue)
                                    {
                                        array10 = array7;
                                    }
                                    else
                                    {
                                        num = p_beanPitting.theStandardPitChart;
                                        if (num.GetValueOrDefault() == 8.0 && num.HasValue)
                                        {
                                            array10 = array8;
                                        }
                                        else
                                        {
                                            array10 = array9;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            int num5 = 2;
            for (int i = 2; i >= 1; i--)
            {
                num = beanPittingResult.rwt_5;
                double num6 = array10[i, 0];
                if (num.GetValueOrDefault() >= num6 && num.HasValue)
                {
                    num5--;
                }
            }
            int num7 = num5 + 1;
            double num8;
            if (p_beanPitting.componentShapeID == 1)
            {
                num8 = array10[num7, 1];
            }
            else
            {
                num8 = array10[num7, 2];
            }
            double num9;
            if (p_beanPitting.componentShapeID == 1)
            {
                num9 = array10[num5, 1];
            }
            else
            {
                num9 = array10[num5, 2];
            }
            beanPittingResult.pitChart_7 = p_beanPitting.theStandardPitChart;
            beanPittingResult.pitChartName_7 = p_beanPitting.theStandardPitChartName;
            double num10 = array10[num7, 0];
            double num11 = array10[num5, 0];
            num = p_beanPitting.theStandardPitChart;
            if (num.GetValueOrDefault() > 8.0 && num.HasValue)
            {
                beanPittingResult.rsf_8 = beanPittingResult.rwt_5;
            }
            else
            {
                BeanPittingResult arg_10A9_0 = beanPittingResult;
                double num6 = num8;
                double num12 = (num9 - num8) / (num11 - num10);
                num = beanPittingResult.rwt_5;
                double num13 = num10;
                num = (num.HasValue ? new double?(num.GetValueOrDefault() - num13) : null);
                num = (num.HasValue ? new double?(num12 * num.GetValueOrDefault()) : null);
                arg_10A9_0.rsf_8 = (num.HasValue ? new double?(num6 + num.GetValueOrDefault()) : null);
            }
            beanPittingResult.rsfa_9 = p_beanPitting.allowRSF;
            BeanPittingResult arg_113E_0 = beanPittingResult;
            num = beanPittingResult.mawp_6;
            num2 = beanPittingResult.rsf_8;
            num4 = beanPittingResult.rsfa_9;
            num2 = ((num2.HasValue & num4.HasValue) ? new double?(num2.GetValueOrDefault() / num4.GetValueOrDefault()) : null);
            arg_113E_0.mawpR_9 = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() * num2.GetValueOrDefault()) : null);
            num = beanPittingResult.rsf_8;
            num2 = beanPittingResult.rsfa_9;
            if ((num.GetValueOrDefault() < num2.GetValueOrDefault() && (num.HasValue & num2.HasValue)) || beanPittingResult.checkRwt_5 == false)
            {
                beanPittingResult.result = "The Level 1 assessment criteria are not satisfied.";
                beanPittingResult.resultBool = false;
            }
            else
            {
                beanPittingResult.result = "The Level 1 assessment criteria are satisfied.";
                beanPittingResult.resultBool = true;
            }
            return beanPittingResult;
        }

        public BeanPittingResult calculateLevel2(BeanPitting p_beanPitting)
        {
            BeanPittingResult beanPittingResult = new BeanPittingResult();
            beanPittingResult.d_1 = p_beanPitting.insideDiameter;
            beanPittingResult.fca_1 = p_beanPitting.fca;
            beanPittingResult.tnom_1 = p_beanPitting.nominalThickness;
            beanPittingResult.loss_1 = p_beanPitting.loss;
            BeanPittingResult arg_BC_0 = beanPittingResult;
            double? num = beanPittingResult.tnom_1;
            double? num2 = beanPittingResult.loss_1;
            num = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() - num2.GetValueOrDefault()) : null);
            num2 = beanPittingResult.fca_1;
            arg_BC_0.tc_2 = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() - num2.GetValueOrDefault()) : null);
            double[,] inspectionGridData = p_beanPitting.inspectionGridData;
            int length = inspectionGridData.GetLength(0);
            int length2 = inspectionGridData.GetLength(1);
            double[] array = new double[length];
            int i;
            for (i = 0; i < length; i++)
            {
                array[i] = inspectionGridData[i, 0];
            }
            double[] array2 = new double[length];
            for (i = 0; i < length; i++)
            {
                array2[i] = inspectionGridData[i, 1];
            }
            double[] array3 = new double[length];
            for (i = 0; i < length; i++)
            {
                array3[i] = inspectionGridData[i, 2];
            }
            double[] array4 = new double[length];
            for (i = 0; i < length; i++)
            {
                array4[i] = inspectionGridData[i, 3];
            }
            double[] array5 = new double[length];
            for (i = 0; i < length; i++)
            {
                array5[i] = inspectionGridData[i, 4];
            }
            double[] array6 = new double[length];
            for (i = 0; i < length; i++)
            {
                array6[i] = inspectionGridData[i, 5];
            }
            double[] array7 = new double[length];
            for (i = 0; i < length; i++)
            {
                array7[i] = inspectionGridData[i, 6];
            }
            double[] array8 = new double[length];
            for (i = 0; i < length; i++)
            {
                array8[i] = (array5[i] + array7[i]) / 2.0;
            }
            num = beanPittingResult.d_1;
            num = (num.HasValue ? new double?(num.GetValueOrDefault() / 2.0) : null);
            num2 = beanPittingResult.fca_1;
            num = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() + num2.GetValueOrDefault()) : null);
            num2 = beanPittingResult.loss_1;
            double? num3 = (num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() + num2.GetValueOrDefault()) : null;
            double num4 = p_beanPitting.designPressure.Value / p_beanPitting.weldJointEfficiency.Value;
            num = num3;
            double num5 = beanPittingResult.tc_2.Value;
            num = (num.HasValue ? new double?(num.GetValueOrDefault() / num5) : null);
            num = (num.HasValue ? new double?(num.GetValueOrDefault() + 0.6) : null);
            double? sigma = num.HasValue ? new double?(num4 * num.GetValueOrDefault()) : null;
            beanPittingResult.sigma1 = sigma;
            num4 = p_beanPitting.designPressure.Value / (p_beanPitting.weldJointEfficiency.Value * 2.0);
            num = num3;
            num5 = beanPittingResult.tc_2.Value;
            num = (num.HasValue ? new double?(num.GetValueOrDefault() / num5) : null);
            num = (num.HasValue ? new double?(num.GetValueOrDefault() - 0.4) : null);
            double? sigma2 = num.HasValue ? new double?(num4 * num.GetValueOrDefault()) : null;
            beanPittingResult.sigma2 = sigma2;
            BeanPittingResult arg_5B0_0 = beanPittingResult;
            num = p_beanPitting.allowableStress;
            num2 = p_beanPitting.weldJointEfficiency;
            num = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() * num2.GetValueOrDefault()) : null);
            num2 = beanPittingResult.tc_2;
            num = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() * num2.GetValueOrDefault()) : null);
            num2 = num3;
            double? num6 = beanPittingResult.tc_2;
            num6 = (num6.HasValue ? new double?(0.6 * num6.GetValueOrDefault()) : null);
            num2 = ((num2.HasValue & num6.HasValue) ? new double?(num2.GetValueOrDefault() + num6.GetValueOrDefault()) : null);
            arg_5B0_0.mawpC_6 = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() / num2.GetValueOrDefault()) : null);
            BeanPittingResult arg_709_0 = beanPittingResult;
            num = p_beanPitting.allowableStress;
            num = (num.HasValue ? new double?(2.0 * num.GetValueOrDefault()) : null);
            num2 = p_beanPitting.weldJointEfficiency;
            num = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() * num2.GetValueOrDefault()) : null);
            num2 = beanPittingResult.tc_2;
            num = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() * num2.GetValueOrDefault()) : null);
            num2 = num3;
            num6 = beanPittingResult.tc_2;
            num6 = (num6.HasValue ? new double?(0.4 * num6.GetValueOrDefault()) : null);
            num2 = ((num2.HasValue & num6.HasValue) ? new double?(num2.GetValueOrDefault() - num6.GetValueOrDefault()) : null);
            arg_709_0.mawpL_6 = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() / num2.GetValueOrDefault()) : null);
            beanPittingResult.mawp_6 = new double?(Math.Min(beanPittingResult.mawpC_6.Value, beanPittingResult.mawpL_6.Value));
            double[] array9 = new double[length];
            for (i = 0; i < length; i++)
            {
                array9[i] = (array4[i] + array6[i]) / 2.0;
            }
            double[] array10 = new double[length];
            for (i = 0; i < length; i++)
            {
                array10[i] = (array2[i] - array9[i]) / array2[i];
            }
            double[] array11 = new double[length];
            for (i = 0; i < length; i++)
            {
                array11[i] = sigma.Value / array10[i];
            }
            double[] array12 = new double[length];
            for (i = 0; i < length; i++)
            {
                array12[i] = sigma2.Value / array10[i];
            }
            double[] array13 = new double[length];
            for (i = 0; i < length; i++)
            {
                array13[i] = (Math.Pow(Math.Cos(array3[i] * 3.1415926535897931 / 180.0), 4.0) + Math.Pow(Math.Sin(2.0 * (array3[i] * 3.1415926535897931 / 180.0)), 2.0)) * Math.Pow(array11[i], 2.0) - 3.0 * Math.Pow(Math.Sin(2.0 * (array3[i] * 3.1415926535897931 / 180.0)), 2.0) * array11[i] * array12[i] / 2.0 + (Math.Pow(Math.Sin(array3[i] * 3.1415926535897931 / 180.0), 4.0) + Math.Pow(Math.Sin(2.0 * (array3[i] * 3.1415926535897931 / 180.0)), 2.0)) * Math.Pow(array12[i], 2.0);
            }
            double[] array14 = new double[length];
            for (i = 0; i < length; i++)
            {
                array14[i] = array10[i] * Math.Max(Math.Abs(array11[i]), Math.Max(Math.Abs(array12[i]), Math.Abs(array11[i] - array12[i])));
            }
            double[] array15 = new double[length];
            for (i = 0; i < length; i++)
            {
                array15[i] = Math.Min(array14[i] / Math.Sqrt(array13[i]), 1.0);
            }
            double[] array16 = new double[length];
            for (i = 0; i < length; i++)
            {
                array16[i] = 1.0 - array8[i] / beanPittingResult.tc_2.Value * (1.0 - array15[i]);
            }
            double[,] array17 = new double[length + 1, 9];
            for (i = 1; i <= length; i++)
            {
                array17[i, 0] = array[i - 1];
            }
            for (i = 1; i <= length; i++)
            {
                array17[i, 1] = array8[i - 1];
            }
            for (i = 1; i <= length; i++)
            {
                array17[i, 2] = array10[i - 1];
            }
            for (i = 1; i <= length; i++)
            {
                array17[i, 3] = array11[i - 1];
            }
            for (i = 1; i <= length; i++)
            {
                array17[i, 4] = array12[i - 1];
            }
            for (i = 1; i <= length; i++)
            {
                array17[i, 5] = array13[i - 1];
            }
            for (i = 1; i <= length; i++)
            {
                array17[i, 6] = array14[i - 1];
            }
            for (i = 1; i <= length; i++)
            {
                array17[i, 7] = array15[i - 1];
            }
            for (i = 1; i <= length; i++)
            {
                array17[i, 8] = array16[i - 1];
            }
            beanPittingResult.rsfTables = array17;
            float num7 = (float)(length - 1);
            double num8 = 0.0;
            i = 0;
            while ((float)i <= num7)
            {
                num8 += array16[i];
                i++;
            }
            double num9 = (double)(1f / (num7 + 1f)) * 1.0 * num8;
            beanPittingResult.rsfPit = new double?(num9);
            beanPittingResult.rsfa_9 = p_beanPitting.allowRSF;
            BeanPittingResult arg_CF9_0 = beanPittingResult;
            num = beanPittingResult.mawp_6;
            num4 = num9;
            num2 = beanPittingResult.rsfa_9;
            num2 = (num2.HasValue ? new double?(num4 / num2.GetValueOrDefault()) : null);
            arg_CF9_0.mawpR_9 = ((num.HasValue & num2.HasValue) ? new double?(num.GetValueOrDefault() * num2.GetValueOrDefault()) : null);
            double[] array18 = new double[length];
            for (i = 1; i < length; i++)
            {
                array18[i] = (beanPittingResult.tc_2.Value + beanPittingResult.fca_1.Value - array5[i]) / beanPittingResult.tc_2.Value;
            }
            double[] array19 = new double[length];
            for (i = 1; i < length; i++)
            {
                array19[i] = (beanPittingResult.tc_2.Value + beanPittingResult.fca_1.Value - array7[i]) / beanPittingResult.tc_2.Value;
            }
            double[] array20 = new double[length];
            for (i = 1; i < length; i++)
            {
                num4 = array18[i];
                num = beanPittingResult.rsfa_9;
                if (num4 < num.GetValueOrDefault() && num.HasValue)
                {
                    array20[i] = 1.123 * Math.Pow(Math.Pow((1.0 - array18[i]) / (1.0 - array18[i] / beanPittingResult.rsfa_9.Value), 2.0) - 1.0, 0.5);
                }
                else
                {
                    array20[i] = 50.0;
                }
            }
            double[] array21 = new double[length];
            for (i = 1; i < length; i++)
            {
                num4 = array19[i];
                num = beanPittingResult.rsfa_9;
                if (num4 < num.GetValueOrDefault() && num.HasValue)
                {
                    array21[i] = 1.123 * Math.Pow(Math.Pow((1.0 - array19[i]) / (1.0 - array19[i] / beanPittingResult.rsfa_9.Value), 2.0) - 1.0, 0.5);
                }
                else
                {
                    array21[i] = 50.0;
                }
            }
            bool[,] array22 = new bool[length + 1, 5];
            for (i = 1; i <= length; i++)
            {
                if (0.2 <= array18[i - 1])
                {
                    array22[i, 1] = true;
                }
                else
                {
                    array22[i, 1] = false;
                }
            }
            for (i = 1; i <= length; i++)
            {
                if (0.2 <= array19[i - 1])
                {
                    array22[i, 2] = true;
                }
                else
                {
                    array22[i, 2] = false;
                }
            }
            for (i = 1; i <= length; i++)
            {
                if (array4[i - 1] <= array20[i - 1] * Math.Sqrt(2.0 * num3.Value * beanPittingResult.tc_2.Value))
                {
                    array22[i, 3] = true;
                }
                else
                {
                    array22[i, 3] = false;
                }
            }
            for (i = 1; i <= length; i++)
            {
                if (array6[i - 1] <= array21[i - 1] * Math.Sqrt(2.0 * num3.Value * beanPittingResult.tc_2.Value))
                {
                    array22[i, 4] = true;
                }
                else
                {
                    array22[i, 4] = false;
                }
            }
            beanPittingResult.checkTable = array22;
            bool flag = true;
            for (i = 1; i <= length; i++)
            {
                flag = (array22[i, 1] && array22[i, 2] && array22[i, 3] && array22[i, 4]);
            }
            num = beanPittingResult.rsf_8;
            num2 = beanPittingResult.rsfa_9;
            if (num.GetValueOrDefault() < num2.GetValueOrDefault() && (num.HasValue & num2.HasValue) && !flag)
            {
                beanPittingResult.result = "The Level 2 assessment criteria are not satisfied.";
                beanPittingResult.resultBool = false;
            }
            else
            {
                beanPittingResult.result = "The Level 2 assessment criteria are satisfied.";
                beanPittingResult.resultBool = true;
            }
            return beanPittingResult;
        }
    }

}
