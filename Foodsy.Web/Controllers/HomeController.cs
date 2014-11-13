namespace Foodsy.Web.Controllers
{
    using System.Web.Mvc;

    using Foodsy.Data;

    public class HomeController : BaseController
    {
        public HomeController(IFoodsyData data)
            :base(data)
        {

        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}