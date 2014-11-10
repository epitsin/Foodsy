﻿namespace Foodsy.Web
{
    using System.Reflection;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Foodsy.Web.Infrastructure.Mapping;
    using Foodsy.Web.App_Start;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ViewEnginesConfig.RegisterViewEngines(ViewEngines.Engines);

            AutoMapperConfig.Execute(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //AntiForgeryConfig.SuppressXFrameOptionsHeader = true;

        }
    }
}
