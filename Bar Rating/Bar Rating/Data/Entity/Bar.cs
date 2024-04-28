using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace BarRating.Data.Entity
{
    public class Bar
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MaxLength(64)]
        public string Name { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [MaxLength(255)]
        public string Description { get; set; }
        [Required(ErrorMessage ="Image path is required")]
        public string ImagePath { get; set; }
        public List<Review> Reviews { get; set; }
    }
}
