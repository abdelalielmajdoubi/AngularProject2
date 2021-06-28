using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProject2.Models
{
    public class RoleUtlisateur

    {
        public int Id { get; set; }

        [Required]
        public string Username { get; set; }

        public string Role { get; set; }
    }
}
