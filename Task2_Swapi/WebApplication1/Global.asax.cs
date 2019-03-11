using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using WebApplication1.Application;
using WebApplication1.Controllers;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            var builder = new ContainerBuilder();
            var controllersAssembly = Assembly.GetAssembly(typeof(HomeController));
            builder.RegisterInstance(new DataStoreHelper()).AsSelf();
            builder.RegisterControllers(controllersAssembly);
           // builder.Build();
         //   builder.RegisterModule(new PerRequestModule());
          
           var containerBuilder = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(containerBuilder));

          

        }

       

    }
}
