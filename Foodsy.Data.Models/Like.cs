namespace Foodsy.Data.Models
{
    public class Like
    {
        public int Id { get; set; }

        public bool IsPositive { get; set; }

        public User Author { get; set; }

        public int RecipeId { get; set; }

        public virtual Recipe Recipe { get; set; }
    }
}
