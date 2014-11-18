namespace Foodsy.Web
{
    using System.Globalization;
    using System.Reflection;
    using System.Threading;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Foodsy.Web.App_Start;
    using Foodsy.Web.Infrastructure.Mapping;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            ViewEnginesConfig.RegisterViewEngines(ViewEngines.Engines);

            ConfigureAutoMapper();

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-US");

            AntiForgeryConfig.SuppressXFrameOptionsHeader = false;
        }

        public static void ConfigureAutoMapper()
        {
            AutoMapperConfig.Execute(Assembly.GetExecutingAssembly());
        }
    }
}
