using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api.Model
{
    public class BeanHydrogenResult
    {
        public double D
        {
            get;
            set;
        }

        public double FCA
        {
            get;
            set;
        }

        public double LOSS
        {
            get;
            set;
        }

        public double tnom
        {
            get;
            set;
        }

        public double tc
        {
            get;
            set;
        }

        public bool[] Planar
        {
            get;
            set;
        }

        public bool[] Flaw
        {
            get;
            set;
        }

        public string[] HICType
        {
            get;
            set;
        }

        public bool[] Facility
        {
            get;
            set;
        }

        public double MAWP
        {
            get;
            set;
        }

        public double DH
        {
            get;
            set;
        }

        public double[] Lampda
        {
            get;
            set;
        }

        public double[] Mt
        {
            get;
            set;
        }

        public double[] RSF
        {
            get;
            set;
        }

        public bool CheckFlaw
        {
            get;
            set;
        }

        public bool CheckFacility
        {
            get;
            set;
        }

        public bool CheckPlanar
        {
            get;
            set;
        }

        public bool CheckHICType
        {
            get;
            set;
        }

        public bool CheckRSF
        {
            get;
            set;
        }

        public string Result
        {
            get;
            set;
        }

        public bool ResultBool
        {
            get;
            set;
        }
    }
}
