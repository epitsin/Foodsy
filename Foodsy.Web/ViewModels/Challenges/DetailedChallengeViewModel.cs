namespace Foodsy.Web.ViewModels.Challenges
{
    using System;
    using System.Collections.Generic;

    using Foodsy.Data.Models;
    using Foodsy.Web.Infrastructure.Mapping;

    public class DetailedChallengeViewModel : IMapFrom<Challenge>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime? Start { get; set; }

        public DateTime? Finish { get; set; }

        public ChallengeType ChallengeType { get; set; }

        public virtual ICollection<Recipe> Recipes { get; set; }

        public virtual ICollection<User> Participants { get; set; }
    }
}