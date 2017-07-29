using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api
{
    public class RootModule : NancyModule
    {
        public RootModule()
        {
            Get["/{any}"] = x =>
            {
                return Response.AsFile("public/index.html");
            };
        }
    }
}
