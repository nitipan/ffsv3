using Microsoft.Owin.Extensions;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //const string rootFolder = "public";
            //var fileSystem = new PhysicalFileSystem(rootFolder);
            //var options = new FileServerOptions
            //{
            //    EnableDefaultFiles = true,

            //    FileSystem = fileSystem
            //};

         //   app.UseFileServer(options);

            app.UseNancy();
            app.UseStageMarker(PipelineStage.MapHandler);
        }
    }
}
