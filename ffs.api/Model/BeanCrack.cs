using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api.Model
{
    public class BeanCrack : BeanCalculateCommon
    {
        public int? CrackTypeID
        {
            get;
            set;
        }

        public int? CrackLocationID
        {
            get;
            set;
        }

        public string CrackType
        {
            get;
            set;
        }

        public string CrackLocation
        {
            get;
            set;
        }

        public double? CrackLength
        {
            get;
            set;
        }

        public double? CrackDepth
        {
            get;
            set;
        }
    }
}
