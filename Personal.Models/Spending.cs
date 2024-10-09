using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Models
{
    public class Spending
    {
        [Key]
        public int Id { get; set; }
        [Required]
		//TODO: [ForeignKey]
		public int UserId { get; set; }
        [Required]
        public int CategoryId { get; set; }

        [Required]
        [DisplayName("Spending Amount")]
        public decimal Amount { get; set; }
        [Required]
        public DateTime DateTime { get; set; }
        [AllowNull]
        [DisplayName("Optional Description")]
        public string? Description { get; set; }

        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        [ForeignKey("UserId")]
        [ValidateNever]
        public User User { get; set; }
    }
}
