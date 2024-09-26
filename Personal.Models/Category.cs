using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Personal.Models
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
