namespace Foodsy.Web.Areas.Administration.ViewModels
{
    using Foodsy.Data.Models;
    using Foodsy.Web.Areas.Administration.Models.Base;
    using Foodsy.Web.Infrastructure.Mapping;

    public class ArticleViewModel : AdministrationViewModel, IMapFrom<Article>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string ImageUrl { get; set; }
    }
}