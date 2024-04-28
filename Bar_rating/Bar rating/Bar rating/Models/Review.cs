using System.ComponentModel.DataAnnotations;

namespace Bar_rating.Models
{
    public class Reviews
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Bar reviw is required")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Witch users left the review?")]
        public Users user { get; set; }
        
    }
}
