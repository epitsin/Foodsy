namespace Foodsy.Web.ViewModels.Actions
{
    using Foodsy.Data.Models;
    using Foodsy.Web.Infrastructure.Mapping;

    public class ActionViewModel : IMapFrom<Action>
    {
        public string Text { get; set; }

        public string ParentActions { get; set; }

    }
}