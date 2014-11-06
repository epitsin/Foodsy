namespace Foodsy.Data.Models
{
    public class Action
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public int StepLevel { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
