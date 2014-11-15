namespace Foodsy.Data.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Message
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Email { get; set; }

        [Required]
        [StringLength(200, MinimumLength = 10)]
        public string Subject { get; set; }

        [Required]
        [StringLength(2000, MinimumLength = 10)]
        public string Text { get; set; }
    }
}
