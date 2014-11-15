namespace Foodsy.Web.ViewModels.Challenges
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    using Foodsy.Data.Models;

    public class CreateChallengeViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Start { get; set; }

        public DateTime Finish { get; set; }

        public ChallengeType ChallengeType { get; set; }

        public IEnumerable<string> SelectedRecipes { get; set; }

        public IEnumerable<SelectListItem> Recipes { get; set; }
    }
}