using Foodsy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Foodsy.Web.Controllers
{
    public abstract class BaseController : Controller
    {
        public BaseController(IFoodsyData data)
        {
            this.Data = data;
        }

        protected IFoodsyData Data { get; set; }
    }
}