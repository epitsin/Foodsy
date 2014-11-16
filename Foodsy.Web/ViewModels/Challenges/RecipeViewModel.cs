namespace Foodsy.Web.ViewModels.Challenges
{
    using System.ComponentModel.DataAnnotations;

    using Foodsy.Data.Models;
    using Foodsy.Web.Infrastructure.Mapping;

    public class RecipeViewModel : IMapFrom<Recipe>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }
    }
}