namespace Foodsy.Web.ViewModels.Challenges
{
    using Foodsy.Data.Models;
    using Foodsy.Web.Infrastructure.Mapping;

    public class AllChallengesViewModel : IMapFrom<Challenge>
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}