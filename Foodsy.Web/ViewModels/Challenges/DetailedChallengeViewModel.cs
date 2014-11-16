namespace Foodsy.Web.ViewModels.Challenges
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;

    using Foodsy.Data.Models;
    using Foodsy.Web.Infrastructure.Mapping;

    public class DetailedChallengeViewModel : IMapFrom<Challenge>, IHaveCustomMappings
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
        public DateTime Start { get; set; }

        [Required]
        public DateTime Finish { get; set; }

        [Required]
        public ChallengeType ChallengeType { get; set; }

        public virtual ICollection<RecipeViewModel> Recipes { get; set; }

        public virtual ICollection<string> Participants { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<Challenge, DetailedChallengeViewModel>()
                .ForMember(m => m.Participants, opt => opt.MapFrom(t => t.Participants.Select(x => x.UserName)));
        }
    }
}