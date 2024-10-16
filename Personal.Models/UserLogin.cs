using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal.Models
{
    public class UserLogin
    {
        [Required]
        public string nameEmail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [AllowNull]
        public bool RememberMe { get; set; }
    }
}
