using Microsoft.Owin.Extensions;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ffs.api
{
    public class Startup
    {
        const string dbFile = "db.sdf";

        public void Configuration(IAppBuilder app)
        {
            if (!File.Exists(dbFile))
            {
                var assembly = Assembly.GetExecutingAssembly();
                var resourceName = "ffs.api." + dbFile;

                using (Stream stream = assembly.GetManifestResourceStream(resourceName))
                {
                    using (var fileStream = File.Create(dbFile))
                    {
                        stream.Seek(0, SeekOrigin.Begin);
                        stream.CopyTo(fileStream);
                    }
                }
            }
            //var args = Environment.GetCommandLineArgs();
            //if (args.Length > 0 && !args[0].EndsWith("OwinHost.exe"))
            //{
                const string rootFolder = "public";
                var fileSystem = new PhysicalFileSystem(rootFolder);
                var options = new FileServerOptions
                {
                    EnableDefaultFiles = true,

                    FileSystem = fileSystem
                };

                app.UseFileServer(options);
           // }
            app.UseNancy();
            app.UseStageMarker(PipelineStage.MapHandler);
        }
    }
}
