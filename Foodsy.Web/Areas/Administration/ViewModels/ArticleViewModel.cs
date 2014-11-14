namespace Foodsy.Web.Areas.Administration.ViewModels
{
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Foodsy.Data.Models;
    using Foodsy.Web.Areas.Administration.Models.Base;
    using Foodsy.Web.Infrastructure.Mapping;

    public class ArticleViewModel : AdministrationViewModel, IMapFrom<Article>
    {
        public ArticleViewModel()
        {
            //this.Tags = new List<Tag>();
        }

        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(15000, MinimumLength = 20)]
        public string Text { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        //public ICollection<Tag> Tags { get; set; }
    }
}