using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api.Model
{
    public class BeanHydrogen : BeanCalculateCommon
    {
        public int? NumberOfFlow
        {
            get;
            set;
        }

        public BeanHydrogenItem[] HydrogenItem
        {
            get;
            set;
        }
    }
}
