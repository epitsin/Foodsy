using Foodsy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Foodsy.Web.Controllers
{
    public class CommentsController : BaseController
    {

        public CommentsController(IFoodsyData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}