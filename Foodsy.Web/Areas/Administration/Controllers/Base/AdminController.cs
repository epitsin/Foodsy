namespace Foodsy.Web.Areas.Administration.Controllers.Base
{
    using System.Web.Mvc;

    using Foodsy.Data;
    using Foodsy.Web.Controllers;

    [Authorize(Roles = "Admin")]
    public abstract class AdminController : BaseController
    {
        public AdminController(IFoodsyData data)
            : base(data)
        {
        }
    }
}