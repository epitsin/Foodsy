namespace Foodsy.Web.Areas.Administration.ViewModels
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class UserViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 6)]
        public string UserName { get; set; }
    }
}