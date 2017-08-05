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
    public class WeldCalculationModule : NancyModule
    {

        public WeldCalculationModule() : base("/api/weld")
        {
            Post["/calculation/level{level}/unit{unit}"] = x =>
            {

                var level = (int)x.level;
                var unit = (int)x.unit;

                WeldCalculator calculator = new WeldCalculator(new GlobalVar(unit));

                var input = this.Bind<BeanWeld>();

                var result = level == 1 ? calculator.calculateLevel1(input) : calculator.calculateLevel2(input);

                return Response.AsJson(result);

            };
        }


        internal class WeldCalculator : CalculatorBase
        {
            private static object[,] resultDataGrid = new object[17, 4];

            public WeldCalculator(GlobalVar var) : base(var)
            {

            }
            public BeanWeldResult calculateLevel1(BeanWeld p_beanWeld)
            {
                BeanWeldResult beanWeldResult = new BeanWeldResult();
                if (p_beanWeld.FabricationTolerance == 2 || p_beanWeld.FabricationTolerance == 3 || p_beanWeld.FabricationTolerance == 5 || p_beanWeld.FabricationTolerance == 8)
                {
                    beanWeldResult.e = p_beanWeld.CenterlineOffset.Value;
                    if (p_beanWeld.WeldOrientarion == 1)
                    {
                        beanWeldResult.WO = "Longitudinal Joints";
                    }
                    else if (p_beanWeld.WeldOrientarion == 2)
                    {
                        beanWeldResult.WO = "Circumferential Joints";
                    }
                    beanWeldResult.t = p_beanWeld.nominalThickness.Value;
                    if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_SI)
                    {
                        if (p_beanWeld.WeldOrientarion == 1)
                        {
                            if (beanWeldResult.t <= 13.0 && beanWeldResult.e <= beanWeldResult.t / 4.0)
                            {
                                beanWeldResult.CheckOffset = true;
                            }
                            else if (13.0 < beanWeldResult.t && beanWeldResult.t <= 51.0 && beanWeldResult.e <= 3.0)
                            {
                                beanWeldResult.CheckOffset = true;
                            }
                            else if (beanWeldResult.t > 51.0 && beanWeldResult.e <= Math.Min(beanWeldResult.t / 16.0, 10.0))
                            {
                                beanWeldResult.CheckOffset = true;
                            }
                            else
                            {
                                beanWeldResult.CheckOffset = false;
                            }
                        }
                        else if (p_beanWeld.WeldOrientarion == 2)
                        {
                            if (beanWeldResult.t <= 19.0 && beanWeldResult.e <= beanWeldResult.t / 4.0)
                            {
                                beanWeldResult.CheckOffset = true;
                            }
                            else if (19.0 < beanWeldResult.t && beanWeldResult.t < 38.0 && beanWeldResult.e <= 5.0)
                            {
                                beanWeldResult.CheckOffset = true;
                            }
                            else if (38.0 < beanWeldResult.t && beanWeldResult.t <= 51.0 && beanWeldResult.e <= beanWeldResult.t / 8.0)
                            {
                                beanWeldResult.CheckOffset = true;
                            }
                            else if (beanWeldResult.t > 51.0 && beanWeldResult.e <= Math.Min(beanWeldResult.t / 8.0, 19.0))
                            {
                                beanWeldResult.CheckOffset = true;
                            }
                            else
                            {
                                beanWeldResult.CheckOffset = false;
                            }
                        }
                    }
                    else if (p_beanWeld.WeldOrientarion == 1)
                    {
                        if (beanWeldResult.t <= 0.5 && beanWeldResult.e <= beanWeldResult.t / 4.0)
                        {
                            beanWeldResult.CheckOffset = true;
                        }
                        else if (0.5 < beanWeldResult.t && beanWeldResult.t <= 2.0 && beanWeldResult.e <= beanWeldResult.t / 8.0)
                        {
                            beanWeldResult.CheckOffset = true;
                        }
                        else if (beanWeldResult.t > 2.0 && beanWeldResult.e <= Math.Min(beanWeldResult.t / 16.0, 0.375))
                        {
                            beanWeldResult.CheckOffset = true;
                        }
                        else
                        {
                            beanWeldResult.CheckOffset = false;
                        }
                    }
                    else if (p_beanWeld.WeldOrientarion == 2)
                    {
                        if (beanWeldResult.t <= 0.75 && beanWeldResult.e <= beanWeldResult.t / 4.0)
                        {
                            beanWeldResult.CheckOffset = true;
                        }
                        else if (0.75 < beanWeldResult.t && beanWeldResult.t < 1.5 && beanWeldResult.e <= 0.1875)
                        {
                            beanWeldResult.CheckOffset = true;
                        }
                        else if (1.5 < beanWeldResult.t && beanWeldResult.t <= 2.0 && beanWeldResult.e <= beanWeldResult.t / 8.0)
                        {
                            beanWeldResult.CheckOffset = true;
                        }
                        else if (beanWeldResult.t > 2.0 && beanWeldResult.e <= Math.Min(beanWeldResult.t / 8.0, 0.75))
                        {
                            beanWeldResult.CheckOffset = true;
                        }
                        else
                        {
                            beanWeldResult.CheckOffset = false;
                        }
                    }
                    if (!beanWeldResult.CheckOffset)
                    {
                        beanWeldResult.Result = "The Level 1 assessment criteria are not satisfied.";
                        beanWeldResult.ResultBool = false;
                    }
                    else
                    {
                        beanWeldResult.Result = "The Level 1 assessment criteria are satisfied.";
                        beanWeldResult.ResultBool = true;
                    }
                }
                else
                {
                    beanWeldResult.D = p_beanWeld.insideDiameter.Value;
                    beanWeldResult.Dmax = p_beanWeld.MaxInternalDiameter.Value;
                    beanWeldResult.Dmin = p_beanWeld.MinInternalDiameter.Value;
                    if (beanWeldResult.Dmax - beanWeldResult.Dmin <= 0.01 * beanWeldResult.D)
                    {
                        beanWeldResult.CheckD = true;
                    }
                    else
                    {
                        beanWeldResult.CheckD = false;
                    }
                    if (!beanWeldResult.CheckD)
                    {
                        beanWeldResult.Result = "The Level 1 assessment criteria are not satisfied.";
                        beanWeldResult.ResultBool = false;
                    }
                    else
                    {
                        beanWeldResult.Result = "The Level 1 assessment criteria are satisfied.";
                        beanWeldResult.ResultBool = true;
                    }
                }
                return beanWeldResult;
            }

            public BeanWeldResult calculateLevel2(BeanWeld p_beanWeld)
            {
                BeanWeldResult beanWeldResult = new BeanWeldResult();
                if (p_beanWeld.FabricationTolerance == 2 || p_beanWeld.FabricationTolerance == 3 || p_beanWeld.FabricationTolerance == 5 || p_beanWeld.FabricationTolerance == 8)
                {
                    beanWeldResult.D = p_beanWeld.insideDiameter.Value;
                    beanWeldResult.FCA = p_beanWeld.fca.Value;
                    beanWeldResult.LOSS = p_beanWeld.loss.Value;
                    beanWeldResult.tnom = p_beanWeld.nominalThickness.Value;
                    if (p_beanWeld.WeldOrientarion == 1)
                    {
                        beanWeldResult.WO = "Longitudinal Joints";
                    }
                    else if (p_beanWeld.WeldOrientarion == 2)
                    {
                        beanWeldResult.WO = "Circumferential Joints";
                    }
                    beanWeldResult.tc = beanWeldResult.tnom - beanWeldResult.FCA - beanWeldResult.LOSS;
                    beanWeldResult.t1c = p_beanWeld.TComponent1.Value - beanWeldResult.FCA - beanWeldResult.LOSS;
                    beanWeldResult.t2c = p_beanWeld.TComponent2.Value - beanWeldResult.FCA - beanWeldResult.LOSS;
                    beanWeldResult.P = p_beanWeld.designPressure.Value;
                    double num = p_beanWeld.insideDiameter.Value / 2.0;
                    beanWeldResult.SigmaC = beanWeldResult.P / p_beanWeld.weldJointEfficiency.Value * (num / beanWeldResult.tc + 0.6);
                    beanWeldResult.SigmaL = beanWeldResult.P / (p_beanWeld.weldJointEfficiency.Value * 2.0) * (num / beanWeldResult.tc - 0.4);
                    if (!p_beanWeld.automaticcallyPrimaryStress.Value)
                    {
                        beanWeldResult.SigmaM = p_beanWeld.primaryStress.Value;
                    }
                    else
                    {
                        beanWeldResult.SigmaM = Math.Max(beanWeldResult.SigmaC, beanWeldResult.SigmaL);
                    }
                    if (p_beanWeld.supplementalLoad.Value)
                    {
                        beanWeldResult.SigmaMs = p_beanWeld.supplementalStress.Value;
                    }
                    else
                    {
                        beanWeldResult.SigmaMs = 0.0;
                    }
                    beanWeldResult.Ra = (p_beanWeld.RComponent1.Value + p_beanWeld.RComponent2.Value) / 2.0;
                    beanWeldResult.e = 0.31;
                    if (beanWeldResult.t2c >= beanWeldResult.t1c)
                    {
                        beanWeldResult.p = beanWeldResult.t2c / beanWeldResult.t1c;
                    }
                    else
                    {
                        beanWeldResult.p = beanWeldResult.t1c / beanWeldResult.t2c;
                    }
                    beanWeldResult.C1c = (beanWeldResult.p - 1.0) * (Math.Pow(beanWeldResult.p, 2.0) - 1.0);
                    beanWeldResult.C2c = Math.Pow(beanWeldResult.p, 2.0) + 2.0 * Math.Pow(beanWeldResult.p, 1.5) + 1.0;
                    beanWeldResult.C3c = Math.Pow(Math.Pow(beanWeldResult.p, 2.0) + 1.0, 2.0) + 2.0 * Math.Pow(beanWeldResult.p, 1.5) + (beanWeldResult.p + 1.0);
                    beanWeldResult.Rbccjc = Math.Abs(12.0 / (p_beanWeld.RComponent1.Value * beanWeldResult.t1c) * (0.25672 * p_beanWeld.RComponent2.Value * beanWeldResult.t2c * (beanWeldResult.C1c / beanWeldResult.C2c) + beanWeldResult.e * beanWeldResult.Ra / 2.0 * (beanWeldResult.C1c / beanWeldResult.C2c)));
                    beanWeldResult.Rbccja = 0.0;
                    beanWeldResult.Rbc = beanWeldResult.Rbccjc + beanWeldResult.Rbccja;
                    beanWeldResult.Rbsccjc = 1.0 + 6.0 * beanWeldResult.e / beanWeldResult.t1c * Math.Pow(1.0 + Math.Pow(beanWeldResult.p, 1.5), -1.0);
                    beanWeldResult.Rbsccja = 0.0;
                    beanWeldResult.Rbsc = beanWeldResult.Rbsccjc + beanWeldResult.Rbsccja;
                    beanWeldResult.Sp = Math.Sqrt(12.0 * (1.0 - Math.Pow(p_beanWeld.poissonRatio.Value, 2.0)) * beanWeldResult.P * Math.Pow(num, 3.0) / (p_beanWeld.youngModulus.Value * Math.Pow(beanWeldResult.tc, 3.0)));
                    beanWeldResult.C11 = 3.8392 * Math.Pow(10.0, -3.0) + 3.1636 * (beanWeldResult.e / beanWeldResult.tc) + 1.2377 * Math.Pow(beanWeldResult.e / beanWeldResult.tc, 2.0) - 4.0582 * Math.Pow(10.0, -3.0) * beanWeldResult.Sp + 3.4647 * Math.Pow(10.0, -4.0) * Math.Pow(beanWeldResult.Sp, 2.0) + 3.1205 * Math.Pow(10.0, -6.0) * Math.Pow(beanWeldResult.Sp, 3.0);
                    beanWeldResult.C21 = 1.0 + 0.41934 * (beanWeldResult.e / beanWeldResult.tc) + 9.739 * Math.Pow(10.0, -3.0) * Math.Pow(beanWeldResult.e / beanWeldResult.tc, 2.0);
                    beanWeldResult.Rbcljc = beanWeldResult.C11 / beanWeldResult.C21;
                    beanWeldResult.Rbclja = 0.0;
                    beanWeldResult.Rbl = beanWeldResult.Rbcljc + beanWeldResult.Rbclja;
                    beanWeldResult.Rbsl = -1.0;
                    if (p_beanWeld.WeldOrientarion == 2)
                    {
                        beanWeldResult.Rb = beanWeldResult.Rbc;
                    }
                    else
                    {
                        beanWeldResult.Rb = beanWeldResult.Rbl;
                    }
                    if (p_beanWeld.WeldOrientarion == 2)
                    {
                        beanWeldResult.Rbs = beanWeldResult.Rbsc;
                    }
                    else
                    {
                        beanWeldResult.Rbs = beanWeldResult.Rbsl;
                    }
                    beanWeldResult.Hf = 1.5;
                    beanWeldResult.Sa = p_beanWeld.allowableStress.Value;
                    beanWeldResult.SigmaMs = 0.0;
                    beanWeldResult.RSF = Math.Min(Math.Abs(beanWeldResult.Hf * beanWeldResult.Sa / (beanWeldResult.SigmaM * (1.0 + beanWeldResult.Rb) + beanWeldResult.SigmaMs * (beanWeldResult.SigmaMs * (1.0 + beanWeldResult.Rbs)))), 1.0);
                    double rSF = beanWeldResult.RSF;
                    double? allowRSF = p_beanWeld.allowRSF;
                    if (rSF < allowRSF.GetValueOrDefault() && allowRSF.HasValue)
                    {
                        beanWeldResult.Result = "The Level 2 assessment criteria are not satisfied.";
                        beanWeldResult.ResultBool = false;
                    }
                    else
                    {
                        beanWeldResult.Result = "The Level 2 assessment criteria are satisfied.";
                        beanWeldResult.ResultBool = true;
                    }
                }
                else
                {
                    beanWeldResult.D = p_beanWeld.insideDiameter.Value;
                    beanWeldResult.FCA = p_beanWeld.fca.Value;
                    beanWeldResult.LOSS = p_beanWeld.loss.Value;
                    beanWeldResult.tnom = p_beanWeld.nominalThickness.Value;
                    beanWeldResult.Dmax = p_beanWeld.MaxInternalDiameter.Value;
                    beanWeldResult.Dmin = p_beanWeld.MinInternalDiameter.Value;
                    beanWeldResult.tc = beanWeldResult.tnom - beanWeldResult.FCA - beanWeldResult.LOSS;
                    beanWeldResult.P = p_beanWeld.designPressure.Value;
                    double num = p_beanWeld.insideDiameter.Value / 2.0;
                    beanWeldResult.SigmaC = beanWeldResult.P / p_beanWeld.weldJointEfficiency.Value * (num / beanWeldResult.tc + 0.6);
                    beanWeldResult.SigmaL = beanWeldResult.P / (p_beanWeld.weldJointEfficiency.Value * 2.0) * (num / beanWeldResult.tc - 0.4);
                    if (!p_beanWeld.automaticcallyPrimaryStress.Value)
                    {
                        beanWeldResult.SigmaM = p_beanWeld.primaryStress.Value;
                    }
                    else
                    {
                        beanWeldResult.SigmaM = Math.Max(beanWeldResult.SigmaC, beanWeldResult.SigmaL);
                    }
                    if (p_beanWeld.supplementalLoad.Value)
                    {
                        beanWeldResult.SigmaMs = p_beanWeld.supplementalStress.Value;
                    }
                    else
                    {
                        beanWeldResult.SigmaMs = 0.0;
                    }
                    beanWeldResult.v = p_beanWeld.poissonRatio.Value;
                    beanWeldResult.Cs = 0.1;
                    beanWeldResult.Dm = (beanWeldResult.Dmax + beanWeldResult.Dmin) / 2.0;
                    beanWeldResult.Rbor = 1.5 * (beanWeldResult.Dmax - beanWeldResult.Dmin) * Math.Cos(2.0 * p_beanWeld.AngleToDefineToStress.Value) / (beanWeldResult.tc * (1.0 + beanWeldResult.Cs * beanWeldResult.P * (1.0 + Math.Pow(beanWeldResult.v, 2.0)) / p_beanWeld.youngModulus.Value * Math.Pow(beanWeldResult.Dm / beanWeldResult.tc, 3.0)));
                    beanWeldResult.Rb = Math.Abs(beanWeldResult.Rbor);
                    beanWeldResult.Hf = 1.5;
                    beanWeldResult.Sa = p_beanWeld.allowableStress.Value;
                    beanWeldResult.SigmaMs = 0.0;
                    beanWeldResult.RSF = Math.Min(Math.Abs(beanWeldResult.Hf * beanWeldResult.Sa / (beanWeldResult.SigmaM * (1.0 + beanWeldResult.Rb) + beanWeldResult.SigmaMs * (beanWeldResult.SigmaMs * (1.0 + beanWeldResult.Rbs)))), 1.0);
                    double rSF = beanWeldResult.RSF;
                    double? allowRSF = p_beanWeld.allowRSF;
                    if (rSF < allowRSF.GetValueOrDefault() && allowRSF.HasValue)
                    {
                        beanWeldResult.Result = "The Level 2 assessment criteria are not satisfied.";
                        beanWeldResult.ResultBool = false;
                    }
                    else
                    {
                        beanWeldResult.Result = "The Level 2 assessment criteria are satisfied.";
                        beanWeldResult.ResultBool = true;
                    }
                }
                return beanWeldResult;
            }
        }
    }
}
