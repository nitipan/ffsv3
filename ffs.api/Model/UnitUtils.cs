﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api.Model
{

    public static class UnitUtils
    {
        public static double getCalculateValue(string p_dblValue)
        {
            return UnitUtils.getCalculateValue(Convert.ToDouble(p_dblValue));
        }

        public static double getCalculateValue(double p_dblValue)
        {
            return p_dblValue;
        }

        public static double? getCalculateValue(double? p_dblValue)
        {
            double? result;
            if (p_dblValue.HasValue)
            {
                result = new double?(UnitUtils.getCalculateValue(p_dblValue.Value));
            }
            else
            {
                result = null;
            }
            return result;
        }

        public static string getDisplayValue(double? p_dblValue)
        {
            string result;
            if (p_dblValue.HasValue)
            {
                result = string.Format("{0:#,##0.##}", UnitUtils.roundToSignificantDigits(p_dblValue.Value, 3));
            }
            else
            {
                result = "";
            }
            return result;
        }

        public static double roundToSignificantDigits(double d, int digits)
        {
            double result;
            if (d == 0.0 || double.IsNaN(d))
            {
                result = 0.0;
            }
            else
            {
                double num = Math.Floor(Math.Log10(Math.Abs(d))) + 1.0;
                double num2 = Math.Pow(10.0, num);
                double value = num2 * Math.Round(d / num2, digits, MidpointRounding.AwayFromZero);
                if ((int)num >= digits)
                {
                    result = Math.Round(value, 0, MidpointRounding.AwayFromZero);
                }
                else
                {
                    result = Math.Round(value, digits - (int)num, MidpointRounding.AwayFromZero);
                }
            }
            return result;
        }

        public static string getDistanceUnitString()
        {
            string result;
            if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_SI)
            {
                result = "mm";
            }
            else
            {
                result = "in";
            }
            return result;
        }

        public static string getDegreeUnitString()
        {
            string result;
            if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_SI)
            {
                result = "° C";
            }
            else
            {
                result = "° F";
            }
            return result;
        }

        public static string getPressureUnitString()
        {
            string result;
            if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_SI)
            {
                result = "MPa";
            }
            else
            {
                result = "psi";
            }
            return result;
        }

        public static string getPressureDistanceUnitString()
        {
            string result;
            if (GlobalVar.UNIT_CURRENT == GlobalVar.UNIT_SI)
            {
                result = "MPa*√m";
            }
            else
            {
                result = "psi*√psi";
            }
            return result;
        }
    }

}
