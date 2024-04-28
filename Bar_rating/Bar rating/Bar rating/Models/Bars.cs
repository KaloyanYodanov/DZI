using System.ComponentModel.DataAnnotations;

namespace Bar_rating.Models
{
    public class Bars
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Bar name is required")]
        public string BarName { get; set; }
        [MaxLength(64)]

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }
        [MaxLength(255)]

        
        public string Image {  get; set; }  

        public List<Reviews> Reviews { get; set; }
    }
}
