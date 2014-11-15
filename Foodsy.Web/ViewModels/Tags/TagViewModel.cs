namespace Foodsy.Web.ViewModels.Tags
{
    using System.ComponentModel.DataAnnotations;

    using Foodsy.Data.Models;
    using Foodsy.Web.Infrastructure.Mapping;

    public class TagViewModel : IMapFrom<Tag>
    {
        [Required]
        public string Name { get; set; }
    }
}