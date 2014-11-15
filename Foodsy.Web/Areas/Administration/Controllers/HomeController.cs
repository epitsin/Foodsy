namespace Foodsy.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using Foodsy.Data;
    using Foodsy.Web.Areas.Administration.Controllers.Base;

    public class HomeController : AdminController
    {
        public HomeController(IFoodsyData data)
            : base(data)
        {

        }

        public ActionResult Navigation()
        {
            return View();
        }
    }
}