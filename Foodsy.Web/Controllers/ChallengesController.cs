using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Foodsy.Web.Controllers
{
    public class ChallengesController : Controller
    {
        // GET: Challenges
        public ActionResult AllChallenges()
        {
            return View();
        }
    }
}