using System.ComponentModel.DataAnnotations;

namespace BarRating.Data.Entity
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "UserId is required")]
        public string UserId { get; set; }
        [Required(ErrorMessage = "Text is required")]
        public string Text { get; set; }
    }
}
