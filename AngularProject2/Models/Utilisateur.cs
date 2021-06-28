using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularProject2.Models
{
    public class Utilisateur
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="entrer username correcte")]
        public string Username { get; set; }
        [Required(ErrorMessage = "entrer mot de passe correcte")]
        public string Password { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}
