using Foodsy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Foodsy.Data.Models;

namespace Foodsy.Web.Controllers
{
    using Microsoft.AspNet.Identity;

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