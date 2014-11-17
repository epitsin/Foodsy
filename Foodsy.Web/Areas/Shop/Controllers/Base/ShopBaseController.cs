namespace Foodsy.Web.Areas.Shop.Controllers.Base
{
    using System.Web.Mvc;

    using Foodsy.Data;
    using Foodsy.Web.Controllers;

    [Authorize]
    public abstract class ShopBaseController : BaseController
    {
        public ShopBaseController(IFoodsyData data)
            : base(data)
        {
        }
    }
}