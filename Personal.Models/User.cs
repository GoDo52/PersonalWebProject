using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [ValidateNever]
        public string PasswordHash { get; set; }
        [ValidateNever]
        public string PasswordSalt { get; set; }

        [DefaultValue(1)]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        [ValidateNever]
        public Role Role { get; set; }
    }
}
