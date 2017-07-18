using Nancy;
using Nancy.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ffs.api
{
    public class HomeModule : NancyModule
    {
        public HomeModule()
        {           
            Get["/ping"] = x =>
            {
                return Response.AsJson(new { project = "FFS" });
            };
        }
    }
}
