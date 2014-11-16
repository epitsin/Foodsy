namespace Foodsy.Web.Infrastructure.Populators
{

    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    using Foodsy.Data;
    using Foodsy.Web.Infrastructure.Caching;

    public class DropDownListPopulator : IDropDownListPopulator
    {
        private IFoodsyData data;
        private ICacheService cache;

        public DropDownListPopulator(IFoodsyData data, ICacheService cache)
        {
            this.data = data;
            this.cache = cache;
        }

        public IEnumerable<SelectListItem> GetIngredients()
        {
            var ingredients = this.cache.Get<IEnumerable<SelectListItem>>("ingredients",
                () =>
                {
                    return this.data.Ingredients
                       .All()
                       .Select(c => new SelectListItem
                       {
                           Value = c.Id.ToString(),
                           Text = c.Name
                       })
                       .ToList();
                });

            return ingredients;
        }
    }
}
