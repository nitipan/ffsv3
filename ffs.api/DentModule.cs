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
    public class DentModule : Nancy.NancyModule
    {
        public DentModule() : base("/api/dent")
        {
            Post["/calculation/level{level}/unit{unit}"] = x =>
            {

                var level = (int)x.level;
                var unit = (int)x.unit;

                DentCalculator calculator = new DentCalculator(new GlobalVar(unit));

                var input = this.Bind<BeanDent>();

                var result = level == 1 ? calculator.calculateLevel1(input) : calculator.calculateLevel2(input);

                return Response.AsJson(result);
            };

        }
    }


    internal class DentCalculator : CalculatorBase

    {

        public DentCalculator(GlobalVar var) : base(var)
        {

        }

        public BeanDentResult calculateLevel1(BeanDent p_beanDent)
        {
            BeanDentResult beanDentResult = new BeanDentResult();
            beanDentResult.D = p_beanDent.insideDiameter.Value;
            beanDentResult.FCA = p_beanDent.fca.Value;
            beanDentResult.LOSS = p_beanDent.loss.Value;
            beanDentResult.tnom = p_beanDent.nominalThickness.Value;
            beanDentResult.tc = beanDentResult.tnom - beanDentResult.FCA - beanDentResult.LOSS;
            double? num = p_beanDent.DentDepth;
            double num2 = 1.8 * Math.Sqrt(beanDentResult.D * beanDentResult.tc);
            if (num.GetValueOrDefault() <= num2 && num.HasValue)
            {
                beanDentResult.Flaw = true;
            }
            else
            {
                beanDentResult.Flaw = false;
            }
            bool arg_160_0;
            if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_SI)
            {
                num = p_beanDent.MajorStructuralDiscontinuity;
                num2 = 1.8 * Math.Sqrt(beanDentResult.D * beanDentResult.tc);
                if (num.GetValueOrDefault() >= num2 && num.HasValue)
                {
                    num = p_beanDent.WeldJoint;
                    num2 = Math.Max(2.0 * beanDentResult.tc, 25.0);
                    arg_160_0 = (num.GetValueOrDefault() <= num2 || !num.HasValue);
                    goto IL_160;
                }
            }
            arg_160_0 = true;
            IL_160:
            if (!arg_160_0)
            {
                beanDentResult.Facility = true;
            }
            else
            {
                bool arg_201_0;
                if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_MATRIC)
                {
                    num = p_beanDent.MajorStructuralDiscontinuity;
                    num2 = 1.8 * Math.Sqrt(beanDentResult.D * beanDentResult.tc);
                    if (num.GetValueOrDefault() >= num2 && num.HasValue)
                    {
                        num = p_beanDent.WeldJoint;
                        num2 = Math.Max(2.0 * beanDentResult.tc, 1.0);
                        arg_201_0 = (num.GetValueOrDefault() <= num2 || !num.HasValue);
                        goto IL_201;
                    }
                }
                arg_201_0 = true;
                IL_201:
                if (!arg_201_0)
                {
                    beanDentResult.Facility = true;
                }
                else
                {
                    beanDentResult.Facility = false;
                }
            }
            double num3 = beanDentResult.D / 2.0 + beanDentResult.FCA + beanDentResult.LOSS;
            beanDentResult.Pdesign = p_beanDent.designPressure.Value;
            beanDentResult.MAWPC = p_beanDent.allowableStress.Value * p_beanDent.weldJointEfficiency.Value * beanDentResult.tc / (num3 + 0.6 * beanDentResult.tc);
            beanDentResult.MAWPL = 2.0 * p_beanDent.allowableStress.Value * p_beanDent.weldJointEfficiency.Value * beanDentResult.tc / (num3 - 0.4 * beanDentResult.tc);
            beanDentResult.MAWP = Math.Min(beanDentResult.MAWPC, beanDentResult.MAWPL);
            if (!beanDentResult.Facility || !beanDentResult.Flaw || beanDentResult.Pdesign > beanDentResult.MAWP)
            {
                beanDentResult.Result = "The Level 1 assessment criteria are not satisfied.";
                beanDentResult.ResultBool = false;
            }
            else
            {
                beanDentResult.Result = "The Level 1 assessment criteria are satisfied.";
                beanDentResult.ResultBool = true;
            }
            return beanDentResult;
        }

        public BeanDentResult calculateLevel2(BeanDent p_beanDent)
        {
            BeanDentResult beanDentResult = new BeanDentResult();
            beanDentResult.D = p_beanDent.insideDiameter.Value;
            beanDentResult.FCA = p_beanDent.fca.Value;
            beanDentResult.LOSS = p_beanDent.loss.Value;
            beanDentResult.tnom = p_beanDent.nominalThickness.Value;
            beanDentResult.tc = beanDentResult.tnom - beanDentResult.FCA - beanDentResult.LOSS;
            double? num = p_beanDent.DentDepth;
            double num2 = 1.8 * Math.Sqrt(beanDentResult.D * beanDentResult.tc);
            if (num.GetValueOrDefault() <= num2 && num.HasValue)
            {
                beanDentResult.Flaw = true;
            }
            else
            {
                beanDentResult.Flaw = false;
            }
            bool arg_160_0;
            if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_SI)
            {
                num = p_beanDent.MajorStructuralDiscontinuity;
                num2 = 1.8 * Math.Sqrt(beanDentResult.D * beanDentResult.tc);
                if (num.GetValueOrDefault() >= num2 && num.HasValue)
                {
                    num = p_beanDent.WeldJoint;
                    num2 = Math.Max(2.0 * beanDentResult.tc, 25.0);
                    arg_160_0 = (num.GetValueOrDefault() <= num2 || !num.HasValue);
                    goto IL_160;
                }
            }
            arg_160_0 = true;
            IL_160:
            if (!arg_160_0)
            {
                beanDentResult.Facility = true;
            }
            else
            {
                bool arg_201_0;
                if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_MATRIC)
                {
                    num = p_beanDent.MajorStructuralDiscontinuity;
                    num2 = 1.8 * Math.Sqrt(beanDentResult.D * beanDentResult.tc);
                    if (num.GetValueOrDefault() >= num2 && num.HasValue)
                    {
                        num = p_beanDent.WeldJoint;
                        num2 = Math.Max(2.0 * beanDentResult.tc, 1.0);
                        arg_201_0 = (num.GetValueOrDefault() <= num2 || !num.HasValue);
                        goto IL_201;
                    }
                }
                arg_201_0 = true;
                IL_201:
                if (!arg_201_0)
                {
                    beanDentResult.Facility = true;
                }
                else
                {
                    beanDentResult.Facility = false;
                }
            }
            double num3 = beanDentResult.D / 2.0 + beanDentResult.FCA + beanDentResult.LOSS;
            beanDentResult.Pdesign = p_beanDent.designPressure.Value;
            beanDentResult.MAWPC = p_beanDent.allowableStress.Value * p_beanDent.weldJointEfficiency.Value * beanDentResult.tc / (num3 + 0.6 * beanDentResult.tc);
            beanDentResult.MAWPL = 2.0 * p_beanDent.allowableStress.Value * p_beanDent.weldJointEfficiency.Value * beanDentResult.tc / (num3 - 0.4 * beanDentResult.tc);
            beanDentResult.MAWP = Math.Min(beanDentResult.MAWPC, beanDentResult.MAWPL);
            beanDentResult.SigmaMax = p_beanDent.MaxOperatingPressure.Value / p_beanDent.weldJointEfficiency.Value * (num3 / beanDentResult.tc + 0.6);
            beanDentResult.SigmaMin = p_beanDent.MinOperatingPressure.Value / p_beanDent.weldJointEfficiency.Value * (num3 / beanDentResult.tc + 0.6);
            beanDentResult.Sigmaa = (beanDentResult.SigmaMax - beanDentResult.SigmaMin) / 2.0;
            num = p_beanDent.DentDepth;
            num = (num.HasValue ? new double?(num.GetValueOrDefault() / 2.0) : null);
            num2 = 5.0 * beanDentResult.tc;
            if (num.GetValueOrDefault() >= num2 && num.HasValue)
            {
                beanDentResult.Cs = 2.0;
            }
            else
            {
                beanDentResult.Cs = 1.0;
            }
            if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_MATRIC)
            {
                beanDentResult.Cul = 25.4;
            }
            else
            {
                beanDentResult.Cul = 1.0;
            }
            beanDentResult.dd0 = p_beanDent.DentDepth.Value;
            beanDentResult.Kg = 1.0;
            beanDentResult.Kd = 1.0 + beanDentResult.Cs * Math.Sqrt(beanDentResult.tc / beanDentResult.D * Math.Pow(beanDentResult.dd0 * beanDentResult.Cul, 1.5));
            beanDentResult.SigmaA = beanDentResult.Sigmaa * (1.0 - Math.Pow((beanDentResult.SigmaMax - beanDentResult.Sigmaa) / p_beanDent.ultimatedTensileStrength.Value, 2.0));
            beanDentResult.Nc = 562.2 * (p_beanDent.ultimatedTensileStrength.Value / (2.0 * beanDentResult.SigmaA * beanDentResult.Kd * beanDentResult.Kg));
            bool arg_584_0;
            if (beanDentResult.Facility && beanDentResult.Flaw)
            {
                if (beanDentResult.Pdesign <= beanDentResult.MAWP)
                {
                    num2 = beanDentResult.Nc;
                    num = p_beanDent.NumberOfCycle;
                    arg_584_0 = (num2 >= num.GetValueOrDefault() || !num.HasValue);
                }
                else
                {
                    arg_584_0 = false;
                }
            }
            else
            {
                arg_584_0 = false;
            }
            if (!arg_584_0)
            {
                beanDentResult.Result = "The Level 2 assessment criteria are not satisfied.";
                beanDentResult.ResultBool = false;
            }
            else
            {
                beanDentResult.Result = "The Level 2 assessment criteria are satisfied.";
                beanDentResult.ResultBool = true;
            }
            return beanDentResult;
        }
    }
}


