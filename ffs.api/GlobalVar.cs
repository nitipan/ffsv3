using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api
{

    // TO DO - link with Calling session
    public class GlobalVar
    {
        public const int UNIT_SI = 1;

        public const int UNIT_MATRIC = 2;

        public int UNIT_CURRENT { get; private set; }

        public GlobalVar(int unit)
        {
            this.UNIT_CURRENT = unit;
        }
    }
}
