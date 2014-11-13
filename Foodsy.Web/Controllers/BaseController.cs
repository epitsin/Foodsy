namespace Foodsy.Web.Controllers
{
    using System.Web.Mvc;

    using Foodsy.Data;
    using Foodsy.Data.Models;

    using Microsoft.AspNet.Identity;

    [HandleError]
    public abstract class BaseController : Controller
    {
        private User currentUser;

        public BaseController(IFoodsyData data)
        {
            this.Data = data;
        }

        protected IFoodsyData Data { get; set; }

        public User CurrentUser
        {
            get
            {
                if(this.currentUser == null)
                {
                    var userId = User.Identity.GetUserId();
                    var user = this.Data.Users.Find(userId);
                    this.currentUser = user;
                }

                return this.currentUser;
            }
            set
            {
                this.currentUser = value;
            }
        }
    }
}