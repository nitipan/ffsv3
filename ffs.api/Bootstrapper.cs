﻿using Nancy;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nancy.Bootstrapper;
using Nancy.Session;
using System.Configuration;

namespace ffs.api
{

    public interface ICurrentRequest
    {
        NancyContext Context { get; }
    }
    public class CurrentRequest : ICurrentRequest
    {
        public CurrentRequest(NancyContext context)
        {
            this.Context = context;
        }

        public NancyContext Context { get; internal set; }

    }

    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {


            container.Register<IDbContext>((c, n) =>
            {
                return new FFSDbContext(container.Resolve<IRootPathProvider>().GetRootPath(), ConfigurationManager.AppSettings["connectionString"]);
            });

            Nancy.Json.JsonSettings.MaxJsonLength = int.MaxValue;
            Nancy.Json.JsonSettings.MaxRecursions = 100;


            CookieBasedSessions.Enable(pipelines);
        }
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);
            container.Register<JsonSerializer, CustomJsonSerializer>();
        }

        protected override void ConfigureRequestContainer(TinyIoCContainer container, NancyContext context)
        {
            base.ConfigureRequestContainer(container, context);
            container.Register<CurrentRequest>(new CurrentRequest(context));
        }


        protected override void RequestStartup(TinyIoCContainer container, IPipelines pipelines, NancyContext context)
        {
            base.RequestStartup(container, pipelines, context);
            var req = container.Resolve<CurrentRequest>();
            req.Context = context;
        }




    }

}
