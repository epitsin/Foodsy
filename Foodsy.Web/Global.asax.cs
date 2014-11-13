namespace Foodsy.Web
{
    using System.Reflection;
    using System.Web.Helpers;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;

    using Foodsy.Web.Infrastructure.Mapping;
    using Foodsy.Web.App_Start;
    using System.Web;
    using System.Threading;
    using System.Globalization;

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

            GlobalFilters.Filters.Add(new HandleErrorAttribute());

            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en-US");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture("en-US");

            //AntiForgeryConfig.SuppressXFrameOptionsHeader = true;

        }

        //protected void Application_Error()
        //{
        //    HttpContext httpContext = HttpContext.Current;
        //    if (httpContext != null)
        //    {
        //        RequestContext requestContext = ((MvcHandler)httpContext.CurrentHandler).RequestContext;
        //        /* when the request is ajax the system can automatically handle a mistake with a JSON response. then overwrites the default response */
        //        if (requestContext.HttpContext.Request.IsAjaxRequest())
        //        {
        //            httpContext.Response.Clear();
        //            string controllerName = requestContext.RouteData.GetRequiredString("controller");
        //            IControllerFactory factory = ControllerBuilder.Current.GetControllerFactory();
        //            IController controller = factory.CreateController(requestContext, controllerName);
        //            ControllerContext controllerContext = new ControllerContext(requestContext, (ControllerBase)controller);

        //            JsonResult jsonResult = new JsonResult();
        //            jsonResult.Data = new { success = false, serverError = "500" };
        //            jsonResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
        //            jsonResult.ExecuteResult(controllerContext);
        //            httpContext.Response.End();
        //        }
        //        else
        //        {
        //            httpContext.Response.Redirect("~/Views/Shared/Error");
        //        }
        //    }
        //}
    }
}
