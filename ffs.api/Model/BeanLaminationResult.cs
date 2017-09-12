using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api.Model
{
    public class BeanLaminationResult
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

        public int NF
        {
            get;
            set;
        }

        public double[] Lh
        {
            get;
            set;
        }

        public double[] c
        {
            get;
            set;
        }

        public double[] s
        {
            get;
            set;
        }

        public double[] tmm
        {
            get;
            set;
        }

        public double[] Lw
        {
            get;
            set;
        }

        public double[] Ls
        {
            get;
            set;
        }

        public double[] Lmsd
        {
            get;
            set;
        }

        public bool Lscheck
        {
            get;
            set;
        }

        public bool Lhcheck
        {
            get;
            set;
        }

        public bool CheckDamage
        {
            get;
            set;
        }

        public bool tmmCheck
        {
            get;
            set;
        }

        public bool LwCheck
        {
            get;
            set;
        }

        public bool LmsdCheck
        {
            get;
            set;
        }

        public bool sCheck
        {
            get;
            set;
        }

        public bool cCheck
        {
            get;
            set;
        }

        public double Pdesign
        {
            get;
            set;
        }

        public double MAWPC
        {
            get;
            set;
        }

        public double MAWPL
        {
            get;
            set;
        }

        public double MAWP
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
