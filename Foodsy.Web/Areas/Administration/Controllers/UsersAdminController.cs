namespace Foodsy.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.UI;

    using Foodsy.Data;
    using Foodsy.Web.Areas.Administration.Controllers.Base;
    using Foodsy.Web.Areas.Administration.ViewModels;

    using Model = Foodsy.Data.Models.User;
    using ViewModel = Foodsy.Web.Areas.Administration.ViewModels.UserViewModel;

    public class UsersAdminController : KendoGridAdministrationController
    {
        public UsersAdminController(IFoodsyData data)
            : base(data)
        {
        }

        public ActionResult AllUsers()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.Data.Users.All().Project().To<UserViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Users.Find(id) as T;
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var dbModel = base.Create<Model>(model);
            if (dbModel != null)
                model.Id = dbModel.Id;
            return this.GridOperation(model, request);
        }

        //[HttpPost]
        //public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        //{
        //    base.Update<Model, ViewModel>(model, model.Id);
        //    return this.GridOperation(model, request);
        //}

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.Data.Users.Delete(model.Id);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}