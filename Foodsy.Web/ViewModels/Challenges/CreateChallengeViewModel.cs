﻿namespace Foodsy.Web.ViewModels.Challenges
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Foodsy.Common.CustomAttributes;
    using Foodsy.Data.Models;

    public class CreateChallengeViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 20)]
        public string Description { get; set; }

        [Required]
        [DateTimeRangeAttribute(0, 1000)]
        public DateTime Start { get; set; }

        [Required]
        [DateTimeRangeAttribute(1, 1000)]
        public DateTime Finish { get; set; }

        [Required]
        public ChallengeType ChallengeType { get; set; }

        public IEnumerable<string> SelectedRecipes { get; set; }

        public IEnumerable<SelectListItem> Recipes { get; set; }
    }
}