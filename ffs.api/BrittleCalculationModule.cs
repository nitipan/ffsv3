using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api
{
    public class BrittleCalculationModule : NancyModule
    {
        public BrittleCalculationModule() : base("/api/brittle")
        {
            Post["/calculation"] = x =>
            {
                
                return null;
            };
        }
    }
}
