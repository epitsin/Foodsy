namespace Foodsy.Data.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public User Author { get; set; }
    }
}
