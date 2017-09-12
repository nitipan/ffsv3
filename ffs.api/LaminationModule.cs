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
    public class LaminationModule : Nancy.NancyModule
    {
        public LaminationModule() : base("/api/lamination")
        {
            Post["/calculation/level{level}/unit{unit}"] = x =>
            {

                var level = (int)x.level;
                var unit = (int)x.unit;

                LaminationCalculator calculator = new LaminationCalculator(new GlobalVar(unit));

                var input = this.Bind<BeanLamination>();

                var result = calculator.calculateLevel1(input);

                return Response.AsJson(result);
            };

        }

        internal class LaminationCalculator : CalculatorBase
        {
            public LaminationCalculator(GlobalVar var) : base(var) { }
            public BeanLaminationResult calculateLevel1(BeanLamination p_beanLamination)
            {
                BeanLaminationResult beanLaminationResult = new BeanLaminationResult();
                beanLaminationResult.CheckDamage = p_beanLamination.Damage;
                beanLaminationResult.D = p_beanLamination.insideDiameter.Value;
                beanLaminationResult.FCA = p_beanLamination.fca.Value;
                beanLaminationResult.LOSS = p_beanLamination.loss.Value;
                beanLaminationResult.tnom = p_beanLamination.nominalThickness.Value;
                beanLaminationResult.NF = p_beanLamination.NumberOfFlow.Value;
                BeanLaminationItem[] laminationItems = p_beanLamination.LaminationItems;
                beanLaminationResult.Lh = new double[laminationItems.Length];
                beanLaminationResult.c = new double[laminationItems.Length];
                beanLaminationResult.s = new double[laminationItems.Length];
                beanLaminationResult.tmm = new double[laminationItems.Length];
                beanLaminationResult.Lw = new double[laminationItems.Length];
                beanLaminationResult.Ls = new double[laminationItems.Length];
                beanLaminationResult.Lmsd = new double[laminationItems.Length];
                for (int i = 0; i < laminationItems.Length; i++)
                {
                    beanLaminationResult.Lh[i] = laminationItems[i].LaminationHeight.Value;
                    beanLaminationResult.c[i] = laminationItems[i].FlawDimensionCircumferentialDirection.Value;
                    beanLaminationResult.s[i] = laminationItems[i].FlawDimensionLongituidinalDirection.Value;
                    beanLaminationResult.tmm[i] = laminationItems[i].MinimumMeasuredThickness.Value;
                    beanLaminationResult.Lw[i] = laminationItems[i].SpacingToNearestWeldJoint.Value;
                    beanLaminationResult.Lmsd[i] = laminationItems[i].SpacingToNearestMajorStructuralDiscontinuity.Value;
                    beanLaminationResult.Ls[i] = laminationItems[i].EdgeToEdgeSpacingToNearestLamination.Value;
                }
                beanLaminationResult.tc = beanLaminationResult.tnom - beanLaminationResult.FCA - beanLaminationResult.LOSS;
                beanLaminationResult.Lscheck = true;
                double[] array = beanLaminationResult.Ls;
                for (int j = 0; j < array.Length; j++)
                {
                    double num = array[j];
                    if (num <= 2.0 * beanLaminationResult.tc)
                    {
                        beanLaminationResult.Lscheck = false;
                        break;
                    }
                }
                beanLaminationResult.Lhcheck = true;
                int num2 = 0;
                array = beanLaminationResult.Lh;
                for (int j = 0; j < array.Length; j++)
                {
                    double num3 = array[j];
                    if (num3 > 0.09 * Math.Max(beanLaminationResult.s[num2], beanLaminationResult.c[num2]))
                    {
                        beanLaminationResult.Lhcheck = false;
                        break;
                    }
                    num2++;
                }
                beanLaminationResult.tmmCheck = true;
                num2 = 0;
                array = beanLaminationResult.tmm;
                for (int j = 0; j < array.Length; j++)
                {
                    double num4 = array[j];
                    if (num4 > 0.19 * beanLaminationResult.tc)
                    {
                        beanLaminationResult.tmmCheck = false;
                        break;
                    }
                    num2++;
                }
                beanLaminationResult.LwCheck = true;
                num2 = 0;
                array = beanLaminationResult.Lw;
                for (int j = 0; j < array.Length; j++)
                {
                    double num5 = array[j];
                    if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_SI && num5 < Math.Max(2.0 * beanLaminationResult.tc, 25.0))
                    {
                        beanLaminationResult.LwCheck = false;
                        break;
                    }
                    if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_MATRIC && num5 < Math.Max(2.0 * beanLaminationResult.tc, 1.0))
                    {
                        beanLaminationResult.LwCheck = false;
                        break;
                    }
                    num2++;
                }
                beanLaminationResult.LmsdCheck = true;
                num2 = 0;
                array = beanLaminationResult.Lmsd;
                for (int j = 0; j < array.Length; j++)
                {
                    double num6 = array[j];
                    if (num6 < 1.8 * Math.Sqrt(beanLaminationResult.D * beanLaminationResult.tc))
                    {
                        beanLaminationResult.LmsdCheck = false;
                        break;
                    }
                    num2++;
                }
                beanLaminationResult.sCheck = true;
                num2 = 0;
                array = beanLaminationResult.s;
                for (int j = 0; j < array.Length; j++)
                {
                    double num7 = array[j];
                    if (num7 > 0.6 * Math.Sqrt(beanLaminationResult.D * beanLaminationResult.tc))
                    {
                        beanLaminationResult.sCheck = false;
                        break;
                    }
                    num2++;
                }
                beanLaminationResult.cCheck = true;
                num2 = 0;
                array = beanLaminationResult.c;
                for (int j = 0; j < array.Length; j++)
                {
                    double num8 = array[j];
                    if (num8 > 0.6 * Math.Sqrt(beanLaminationResult.D * beanLaminationResult.tc))
                    {
                        beanLaminationResult.cCheck = false;
                        break;
                    }
                    num2++;
                }
                double num9 = beanLaminationResult.D / 2.0 + beanLaminationResult.FCA + beanLaminationResult.LOSS;
                beanLaminationResult.Pdesign = p_beanLamination.designPressure.Value;
                beanLaminationResult.MAWPC = p_beanLamination.allowableStress.Value * p_beanLamination.weldJointEfficiency.Value * beanLaminationResult.tc / (num9 + 0.6 * beanLaminationResult.tc);
                beanLaminationResult.MAWPL = 2.0 * p_beanLamination.allowableStress.Value * p_beanLamination.weldJointEfficiency.Value * beanLaminationResult.tc / (num9 - 0.4 * beanLaminationResult.tc);
                beanLaminationResult.MAWP = Math.Min(beanLaminationResult.MAWPC, beanLaminationResult.MAWPL);
                if (!beanLaminationResult.CheckDamage || !beanLaminationResult.Lscheck || !beanLaminationResult.Lhcheck || !beanLaminationResult.tmmCheck || !beanLaminationResult.LwCheck || !beanLaminationResult.LmsdCheck || !beanLaminationResult.sCheck || !beanLaminationResult.cCheck || beanLaminationResult.Pdesign > beanLaminationResult.MAWP)
                {
                    beanLaminationResult.Result = "The Level 1 assessment criteria are not satisfied.";
                    beanLaminationResult.ResultBool = false;
                }
                else
                {
                    beanLaminationResult.Result = "The Level 1 assessment criteria are satisfied.";
                    beanLaminationResult.ResultBool = true;
                }
                return beanLaminationResult;
            }

            public BeanLaminationResult calculateLevel2(BeanLamination p_beanLamination)
            {
                BeanLaminationResult beanLaminationResult = new BeanLaminationResult();
                beanLaminationResult.CheckDamage = p_beanLamination.Damage;
                beanLaminationResult.D = p_beanLamination.insideDiameter.Value;
                beanLaminationResult.FCA = p_beanLamination.fca.Value;
                beanLaminationResult.LOSS = p_beanLamination.loss.Value;
                beanLaminationResult.tnom = p_beanLamination.nominalThickness.Value;
                beanLaminationResult.NF = p_beanLamination.NumberOfFlow.Value;
                BeanLaminationItem[] laminationItems = p_beanLamination.LaminationItems;
                beanLaminationResult.Lh = new double[laminationItems.Length];
                beanLaminationResult.c = new double[laminationItems.Length];
                beanLaminationResult.s = new double[laminationItems.Length];
                beanLaminationResult.tmm = new double[laminationItems.Length];
                beanLaminationResult.Lw = new double[laminationItems.Length];
                beanLaminationResult.Ls = new double[laminationItems.Length];
                beanLaminationResult.Lmsd = new double[laminationItems.Length];
                for (int i = 0; i < laminationItems.Length; i++)
                {
                    beanLaminationResult.Lh[i] = laminationItems[i].LaminationHeight.Value;
                    beanLaminationResult.c[i] = laminationItems[i].FlawDimensionCircumferentialDirection.Value;
                    beanLaminationResult.s[i] = laminationItems[i].FlawDimensionLongituidinalDirection.Value;
                    beanLaminationResult.tmm[i] = laminationItems[i].MinimumMeasuredThickness.Value;
                    beanLaminationResult.Lw[i] = laminationItems[i].SpacingToNearestWeldJoint.Value;
                    beanLaminationResult.Ls[i] = laminationItems[i].EdgeToEdgeSpacingToNearestLamination.Value;
                    beanLaminationResult.Lmsd[i] = laminationItems[i].SpacingToNearestMajorStructuralDiscontinuity.Value;
                }
                beanLaminationResult.tc = beanLaminationResult.tnom - beanLaminationResult.FCA - beanLaminationResult.LOSS;
                beanLaminationResult.Lscheck = true;
                double[] array = beanLaminationResult.Ls;
                for (int j = 0; j < array.Length; j++)
                {
                    double num = array[j];
                    if (num <= 2.0 * beanLaminationResult.tc)
                    {
                        beanLaminationResult.Lscheck = false;
                        break;
                    }
                }
                beanLaminationResult.Lhcheck = true;
                int num2 = 0;
                array = beanLaminationResult.Lh;
                for (int j = 0; j < array.Length; j++)
                {
                    double num3 = array[j];
                    if (num3 > 0.09 * Math.Max(beanLaminationResult.s[num2], beanLaminationResult.c[num2]))
                    {
                        beanLaminationResult.Lhcheck = false;
                        break;
                    }
                    num2++;
                }
                beanLaminationResult.tmmCheck = true;
                num2 = 0;
                array = beanLaminationResult.tmm;
                for (int j = 0; j < array.Length; j++)
                {
                    double num4 = array[j];
                    if (num4 > 0.19 * beanLaminationResult.tc)
                    {
                        beanLaminationResult.tmmCheck = false;
                        break;
                    }
                    num2++;
                }
                beanLaminationResult.LwCheck = true;
                num2 = 0;
                array = beanLaminationResult.Lw;
                for (int j = 0; j < array.Length; j++)
                {
                    double num5 = array[j];
                    if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_SI && num5 < Math.Max(2.0 * beanLaminationResult.tc, 25.0))
                    {
                        beanLaminationResult.LwCheck = false;
                        break;
                    }
                    if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_MATRIC && num5 < Math.Max(2.0 * beanLaminationResult.tc, 1.0))
                    {
                        beanLaminationResult.LwCheck = false;
                        break;
                    }
                    num2++;
                }
                beanLaminationResult.LmsdCheck = true;
                num2 = 0;
                array = beanLaminationResult.Lmsd;
                for (int j = 0; j < array.Length; j++)
                {
                    double num6 = array[j];
                    if (num6 < 1.8 * Math.Sqrt(beanLaminationResult.D * beanLaminationResult.tc))
                    {
                        beanLaminationResult.LmsdCheck = false;
                        break;
                    }
                    num2++;
                }
                beanLaminationResult.sCheck = true;
                num2 = 0;
                array = beanLaminationResult.s;
                for (int j = 0; j < array.Length; j++)
                {
                    double num7 = array[j];
                    if (num7 > 0.6 * Math.Sqrt(beanLaminationResult.D * beanLaminationResult.tc))
                    {
                        beanLaminationResult.sCheck = false;
                        break;
                    }
                    num2++;
                }
                beanLaminationResult.cCheck = true;
                num2 = 0;
                array = beanLaminationResult.c;
                for (int j = 0; j < array.Length; j++)
                {
                    double num8 = array[j];
                    if (num8 > 0.6 * Math.Sqrt(beanLaminationResult.D * beanLaminationResult.tc))
                    {
                        beanLaminationResult.cCheck = false;
                        break;
                    }
                    num2++;
                }
                double num9 = beanLaminationResult.D / 2.0 + beanLaminationResult.FCA + beanLaminationResult.LOSS;
                beanLaminationResult.Pdesign = p_beanLamination.designPressure.Value;
                beanLaminationResult.MAWPC = p_beanLamination.allowableStress.Value * p_beanLamination.weldJointEfficiency.Value * beanLaminationResult.tc / (num9 + 0.6 * beanLaminationResult.tc);
                beanLaminationResult.MAWPL = 2.0 * p_beanLamination.allowableStress.Value * p_beanLamination.weldJointEfficiency.Value * beanLaminationResult.tc / (num9 - 0.4 * beanLaminationResult.tc);
                beanLaminationResult.MAWP = Math.Min(beanLaminationResult.MAWPC, beanLaminationResult.MAWPL);
                if (!beanLaminationResult.CheckDamage || !beanLaminationResult.Lscheck || !beanLaminationResult.Lhcheck || !beanLaminationResult.tmmCheck || !beanLaminationResult.LwCheck || !beanLaminationResult.LmsdCheck || !beanLaminationResult.sCheck || !beanLaminationResult.cCheck || beanLaminationResult.Pdesign > beanLaminationResult.MAWP)
                {
                    beanLaminationResult.Result = "The Level 2 assessment criteria are not satisfied.";
                    beanLaminationResult.ResultBool = false;
                }
                else
                {
                    beanLaminationResult.Result = "The Level 2 assessment criteria are satisfied.";
                    beanLaminationResult.ResultBool = true;
                }
                return beanLaminationResult;
            }
        }
    }

}

