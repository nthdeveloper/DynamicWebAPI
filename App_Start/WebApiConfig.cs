using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using DynamicWebAPI.Operations;

namespace DynamicWebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            ContainerBuilder builder = new ContainerBuilder();

            //Register controllers
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            //Register all operations
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(t=> t.IsAssignableTo<IOperation>()).As<IOperation>();

            var container = builder.Build();

            //Set Autofac container as the dependency resolver
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{operationName}",
                defaults: new { controller = "operations", action = "process", operationName = "help" }
            );
        }
    }
}
