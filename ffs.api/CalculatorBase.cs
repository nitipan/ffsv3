using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api
{
    public abstract class CalculatorBase
    {
        protected GlobalVar GlobalVar;

        public CalculatorBase(GlobalVar var)
        {
            this.GlobalVar = var;
        }

        public double getCalculateValue(string p_dblValue)
        {
            return this.getCalculateValue(Convert.ToDouble(p_dblValue));
        }

        public double getCalculateValue(double p_dblValue)
        {
            return p_dblValue;
        }

        public double? getCalculateValue(double? p_dblValue)
        {
            double? result;
            if (p_dblValue.HasValue)
            {
                result = new double?(this.getCalculateValue(p_dblValue.Value));
            }
            else
            {
                result = null;
            }
            return result;
        }

        public string getDisplayValue(double? p_dblValue)
        {
            string result;
            if (p_dblValue.HasValue)
            {
                result = string.Format("{0:#,##0.##}", this.roundToSignificantDigits(p_dblValue.Value, 3));
            }
            else
            {
                result = "";
            }
            return result;
        }

        public double roundToSignificantDigits(double d, int digits)
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

        public string getDistanceUnitString()
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

        public string getDegreeUnitString()
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

        public string getPressureUnitString()
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

        public string getPressureDistanceUnitString()
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

        public double getMinData(double[,] p_inspectionGridData, int p_intNumberOfColumn, int p_intNumberOfRow)
        {
            IEnumerable<double> source = p_inspectionGridData.Cast<double>();
            return source.Min();
        }

        public double[] getMinLongitudinalDataUpperLower(double[,] p_inspectionGridData, int p_intNumberOfColumn, int p_intNumberOfRow)
        {
            double[] array = new double[p_intNumberOfColumn];
            for (int i = 0; i < p_intNumberOfColumn; i++)
            {
                array[i] = 100.0;
                for (int j = 0; j < p_intNumberOfRow; j++)
                {
                    if (p_inspectionGridData[j, i] < array[i])
                    {
                        array[i] = p_inspectionGridData[j, i];
                    }
                }
            }
            int num = 1;
            double num2 = 100.0;
            for (int i = 0; i < p_intNumberOfColumn; i++)
            {
                if (array[i] < num2)
                {
                    num2 = array[i];
                    num = i;
                }
            }
            double[] array2 = new double[3];
            array2[0] = array[num];
            if (num - 1 >= 0)
            {
                array2[1] = array[num - 1];
            }
            else
            {
                array2[1] = 0.0;
            }
            if (num + 1 < p_intNumberOfColumn)
            {
                array2[2] = array[num + 1];
            }
            else
            {
                array2[2] = array[p_intNumberOfColumn - 1];
            }
            return array2;
        }

        public double[] getMinLongitudinal(double[,] p_inspectionGridData, int p_intNumberOfColumn, int p_intNumberOfRow)
        {
            double[] array = new double[p_intNumberOfColumn];
            for (int i = 0; i < p_intNumberOfColumn; i++)
            {
                array[i] = 100.0;
                for (int j = 0; j < p_intNumberOfRow; j++)
                {
                    if (p_inspectionGridData[j, i] < array[i])
                    {
                        array[i] = p_inspectionGridData[j, i];
                    }
                }
            }
            return array;
        }

        public double getMinDataPTR(double[,] p_inspectionGridData, int p_intNumberOfRow)
        {
            double num = 100.0;
            for (int i = 0; i < p_intNumberOfRow; i++)
            {
                if (num > p_inspectionGridData[i, 1])
                {
                    num = p_inspectionGridData[i, 1];
                }
            }
            return num;
        }

        public double getAVGDataPTR(double[,] p_inspectionGridData, int p_intNumberOfRow)
        {
            double[] array = new double[p_intNumberOfRow];
            for (int i = 0; i < p_intNumberOfRow; i++)
            {
                array[i] = p_inspectionGridData[i, 1];
            }
            return array.Average();
        }

        public double[] getMinCircumferentialData(double[,] p_inspectionGridData, int p_intNumberOfColumn, int p_intNumberOfRow)
        {
            double[] array = new double[p_intNumberOfColumn];
            for (int i = 0; i < p_intNumberOfRow; i++)
            {
                array[i] = 100.0;
                for (int j = 0; j < p_intNumberOfColumn; j++)
                {
                    if (p_inspectionGridData[i, j] < array[i])
                    {
                        array[i] = p_inspectionGridData[i, j];
                    }
                }
            }
            int num = 1;
            double num2 = 100.0;
            for (int i = 0; i < p_intNumberOfRow; i++)
            {
                if (array[i] < num2)
                {
                    num2 = array[i];
                    num = i;
                }
            }
            return new double[]
            {
                array[num],
                array[num - 1],
                array[num + 1]
            };
        }

        public double[] getMinLongitudinalDataArray(double[,] p_inspectionGridData, int p_intNumberOfColumn, int p_intNumberOfRow)
        {
            double[] array = new double[p_intNumberOfColumn];
            for (int i = 0; i < p_intNumberOfColumn; i++)
            {
                array[i] = 100.0;
                for (int j = 0; j < p_intNumberOfRow; j++)
                {
                    if (p_inspectionGridData[j, i] < array[i])
                    {
                        array[i] = p_inspectionGridData[j, i];
                    }
                }
            }
            return array;
        }

        public double[] getMinCircumferentialDataArray(double[,] p_inspectionGridData, int p_intNumberOfColumn, int p_intNumberOfRow)
        {
            double[] array = new double[p_intNumberOfRow];
            for (int i = 0; i < p_intNumberOfRow; i++)
            {
                array[i] = 100.0;
                for (int j = 0; j < p_intNumberOfColumn; j++)
                {
                    if (p_inspectionGridData[i, j] < array[i])
                    {
                        array[i] = p_inspectionGridData[i, j];
                    }
                }
            }
            return array;
        }

    }

}
