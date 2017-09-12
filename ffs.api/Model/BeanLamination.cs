using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api.Model
{
    public class BeanLamination : BeanCalculateCommon
    {
        public int? NumberOfFlow
        {
            get;
            set;
        }

        public bool Damage
        {
            get;
            set;
        }

        public BeanLaminationItem[] LaminationItems
        {
            get;
            set;
        }
    }
}
