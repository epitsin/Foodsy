namespace Foodsy.Web.Areas.Shop.Controllers.Base
{
    using System;
    using System.Linq;
    using System.Collections;
    using System.Data.Entity;
    using System.Web.Mvc;

    using AutoMapper;

    using Foodsy.Data;
    using Foodsy.Data.Contracts.Models;
    using Foodsy.Web.Areas.Administration.Models.Base;
    using Kendo.Mvc.Extensions;
    using Kendo.Mvc.UI;

    public abstract class KendoGridShopController : ShopBaseController
    {
        public KendoGridShopController(IFoodsyData data)
            : base(data)
        {
        }

        protected abstract IEnumerable GetData();

        protected abstract T GetById<T>(object id) where T : class;

        [HttpPost]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var data =
                this.GetData().AsQueryable().ToDataSourceResult(request);

            return this.Json(data);
        }

        [NonAction]
        protected virtual void Update<TModel, TViewModel>(TViewModel model, object id)
            where TModel : AuditInfo
            where TViewModel : AdministrationViewModel
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = this.GetById<TModel>(id);
                Mapper.Map<TViewModel, TModel>(model, dbModel);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Modified);
                model.ModifiedOn = dbModel.ModifiedOn;
            }
        }

        protected JsonResult GridOperation<T>(T model, [DataSourceRequest]DataSourceRequest request)
        {
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        private void ChangeEntityStateAndSave(object dbModel, EntityState state)
        {
            var entry = this.Data.Context.Entry(dbModel);
            entry.State = state;
            this.Data.SaveChanges();
        }
    }
}