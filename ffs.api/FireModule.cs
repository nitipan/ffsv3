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
    public class FireModule : Nancy.NancyModule
    {
        public FireModule() : base("/api/fire")
        {
            Post["/calculation/level{level}/unit{unit}"] = x =>
            {

                var level = (int)x.level;
                var unit = (int)x.unit;

                FireCalculator calculator = new FireCalculator(new GlobalVar(unit));

                var input = this.Bind<BeanFire>();

                var result = calculator.calculateLevel1(input);

                return Response.AsJson(result);
            };

        }


        internal class FireCalculator : CalculatorBase
        {
            public FireCalculator(GlobalVar var) : base(var) { }

            public BeanFireResult calculateLevel1(BeanFire p_beanFire)
            {
                BeanFireResult beanFireResult = new BeanFireResult();
                if (p_beanFire.HeatExposureZoneID == 1)
                {
                    beanFireResult.HEZValue = 1.0;
                }
                else if (p_beanFire.HeatExposureZoneID == 2)
                {
                    beanFireResult.HEZValue = 2.0;
                }
                else if (p_beanFire.HeatExposureZoneID == 3)
                {
                    beanFireResult.HEZValue = 3.0;
                }
                else if (p_beanFire.HeatExposureZoneID == 4)
                {
                    beanFireResult.HEZValue = 4.0;
                }
                else if (p_beanFire.HeatExposureZoneID == 5)
                {
                    beanFireResult.HEZValue = 5.0;
                }
                else
                {
                    beanFireResult.HEZValue = 6.0;
                }
                if (p_beanFire.MaterialGroupID == 1)
                {
                    beanFireResult.AllowableHEZ = "Zone IV Over 205 oC (400oF) to 425 oC (800 oF)";
                }
                else if (p_beanFire.MaterialGroupID == 2)
                {
                    beanFireResult.AllowableHEZ = "Zone IV Over 205 oC (400oF) to 425 oC (800 oF)";
                }
                else if (p_beanFire.MaterialGroupID == 3)
                {
                    beanFireResult.AllowableHEZ = "Zone IV Over 205 oC (400oF) to 425 oC (800 oF)";
                }
                else if (p_beanFire.MaterialGroupID == 4)
                {
                    beanFireResult.AllowableHEZ = "Zone II Ambient to 65 oC (150oF)";
                }
                else if (p_beanFire.MaterialGroupID == 5)
                {
                    beanFireResult.AllowableHEZ = "Zone III Over 65 oC (150oF) to 205 oC (400 oF)";
                }
                else if (p_beanFire.MaterialGroupID == 6)
                {
                    beanFireResult.AllowableHEZ = "Zone IV Over 205 oC (400oF) to 425 oC (800 oF)";
                }
                else if (p_beanFire.MaterialGroupID == 7)
                {
                    beanFireResult.AllowableHEZ = "Zone IV Over 205 oC (400oF) to 425 oC (800 oF)";
                }
                else if (p_beanFire.MaterialGroupID == 8)
                {
                    beanFireResult.AllowableHEZ = "Zone III Over 65 oC (150oF) to 205 oC (400 oF)";
                }
                else if (p_beanFire.MaterialGroupID == 9)
                {
                    beanFireResult.AllowableHEZ = "Zone IV Over 205 oC (400oF) to 425 oC (800 oF)";
                }
                else if (p_beanFire.MaterialGroupID == 10)
                {
                    beanFireResult.AllowableHEZ = "Zone IV Over 205 oC (400oF) to 425 oC (800 oF)";
                }
                else if (p_beanFire.MaterialGroupID == 11)
                {
                    beanFireResult.AllowableHEZ = "Zone IV Over 205 oC (400oF) to 425 oC (800 oF)";
                }
                else if (p_beanFire.MaterialGroupID == 12)
                {
                    beanFireResult.AllowableHEZ = "Zone IV Over 205 oC (400oF) to 425 oC (800 oF)";
                }
                else if (p_beanFire.MaterialGroupID == 13)
                {
                    beanFireResult.AllowableHEZ = "Zone IV Over 205 oC (400oF) to 425 oC (800 oF)";
                }
                else if (p_beanFire.MaterialGroupID == 14)
                {
                    beanFireResult.AllowableHEZ = "Zone II Ambient to 65 oC (150oF)";
                }
                else if (p_beanFire.MaterialGroupID == 15)
                {
                    beanFireResult.AllowableHEZ = "Zone II Ambient to 65 oC (150oF)";
                }
                else if (p_beanFire.MaterialGroupID == 16)
                {
                    beanFireResult.AllowableHEZ = "Zone II Ambient to 65 oC (150oF)";
                }
                else
                {
                    beanFireResult.AllowableHEZ = "Zone II Ambient to 65 oC (150oF)";
                }
                if (beanFireResult.AllowableHEZ.CompareTo("Zone I Ambient Temperaute") == 0)
                {
                    beanFireResult.AllowableHEZValue = 1.0;
                }
                else if (beanFireResult.AllowableHEZ.CompareTo("Zone II Ambient to 65 oC (150oF)") == 0)
                {
                    beanFireResult.AllowableHEZValue = 2.0;
                }
                else if (beanFireResult.AllowableHEZ.CompareTo("Zone III Over 65 oC (150oF) to 205 oC (400 oF)") == 0)
                {
                    beanFireResult.AllowableHEZValue = 3.0;
                }
                else if (beanFireResult.AllowableHEZ.CompareTo("Zone IV Over 205 oC (400oF) to 425 oC (800 oF)") == 0)
                {
                    beanFireResult.AllowableHEZValue = 4.0;
                }
                else if (beanFireResult.AllowableHEZ.CompareTo("Zone IV Over 205 oC (400oF) to 425 oC (800 oF)") == 0)
                {
                    beanFireResult.AllowableHEZValue = 5.0;
                }
                else
                {
                    beanFireResult.AllowableHEZValue = 6.0;
                }
                beanFireResult.CheckLeak = p_beanFire.Leak;
                beanFireResult.CheckCoat = p_beanFire.Coat;
                beanFireResult.CheckDamage = p_beanFire.Damage;
                if (!beanFireResult.CheckLeak || !beanFireResult.CheckCoat || !beanFireResult.CheckDamage || beanFireResult.HEZValue >= beanFireResult.AllowableHEZValue)
                {
                    beanFireResult.Result = "The Level 1 assessment criteria are not satisfied.";
                    beanFireResult.ResultBool = false;
                }
                else
                {
                    beanFireResult.Result = "The Level 1 assessment criteria are satisfied.";
                    beanFireResult.ResultBool = true;
                }
                return beanFireResult;
            }

            public BeanFireResult calculateLevel2(BeanFire p_beanFire)
            {
                BeanFireResult beanFireResult = new BeanFireResult();
                beanFireResult.VH = p_beanFire.VickersHardnessNo.Value;
                if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_SI)
                {
                    beanFireResult.Suts_ht = 3.3056123079 * beanFireResult.VH - 27.633825863;
                }
                else
                {
                    beanFireResult.Suts_ht = (3.3056123079 * beanFireResult.VH - 27.633825863) * 145.038;
                }
                beanFireResult.Cism = 4.0;
                beanFireResult.SaA = p_beanFire.AllowableStressFlaw.Value;
                beanFireResult.SaT = p_beanFire.allowableStress.Value;
                beanFireResult.Safd = Math.Min(beanFireResult.Suts_ht / beanFireResult.Cism * (beanFireResult.SaT / beanFireResult.SaA), beanFireResult.SaT);
                beanFireResult.D = p_beanFire.insideDiameter.Value;
                beanFireResult.FCA = p_beanFire.fca.Value;
                beanFireResult.LOSS = p_beanFire.loss.Value;
                beanFireResult.tnom = p_beanFire.nominalThickness.Value;
                beanFireResult.tc = beanFireResult.tnom - beanFireResult.FCA - beanFireResult.LOSS;
                double num = beanFireResult.D / 2.0 + beanFireResult.FCA + beanFireResult.LOSS;
                beanFireResult.Pdesign = p_beanFire.designPressure.Value;
                beanFireResult.MAWPC = beanFireResult.Safd * p_beanFire.weldJointEfficiency.Value * beanFireResult.tc / (num + 0.6 * beanFireResult.tc);
                beanFireResult.MAWPL = 2.0 * beanFireResult.Safd * p_beanFire.weldJointEfficiency.Value * beanFireResult.tc / (num - 0.4 * beanFireResult.tc);
                beanFireResult.MAWP = Math.Min(beanFireResult.MAWPC, beanFireResult.MAWPL);
                if (!beanFireResult.CheckDamage && beanFireResult.Pdesign > beanFireResult.MAWP)
                {
                    beanFireResult.Result = "The Level 2 assessment criteria are satisfied.";
                    beanFireResult.ResultBool = true;
                }
                else
                {
                    beanFireResult.Result = "The Level 2 assessment criteria are not satisfied.";
                    beanFireResult.ResultBool = false;
                }
                return beanFireResult;
            }
        }
    }
}
