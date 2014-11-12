namespace Foodsy.Web.Areas.Administration.Controllers
{
    using System.Web.Mvc;

    using Foodsy.Data;
    using Foodsy.Web.Controllers;

    public class HomeController : BaseController
    {
        public HomeController(IFoodsyData data)
            :base(data)
        {

        }

        public ActionResult Navigation()
        {
            return View();
        }
    }
}