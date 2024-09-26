using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace PersonalWeb_Razor.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage = "Can not exceed 15 characters")]
        [DisplayName("Category Name")]
        public string Name { get; set; }

    }
}
