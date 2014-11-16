namespace Foodsy.Web.Areas.Administration.ViewModels
{
    using System.Web.Mvc;
    using System.ComponentModel.DataAnnotations;

    using Foodsy.Data.Models;
    using Foodsy.Web.Areas.Administration.Models.Base;
    using Foodsy.Web.Infrastructure.Mapping;

    public class ArticleViewModel : AdministrationViewModel, IMapFrom<Article>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 10)]
        public string Summary { get; set; }

        [Required]
        [StringLength(15000, MinimumLength = 20)]
        public string Text { get; set; }

        [Required]
        public string ImageUrl { get; set; }
    }
}