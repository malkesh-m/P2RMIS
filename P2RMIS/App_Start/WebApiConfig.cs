using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Microsoft.Practices.Unity;
using Sra.P2rmis.Web.Common;
using Sra.P2rmis.Bll.Setup;

namespace Sra.P2rmis.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            // Web API registration with unity
            var container = new UnityContainer();
            container.RegisterType<ISetupService, SetupService>(new HierarchicalLifetimeManager());
            config.DependencyResolver = new UnityDependencyApiResolver(container);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
