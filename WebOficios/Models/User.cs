
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace WebOficios.Models
{
    public class User:IdentityUser
    {
        
        [Display(Name="Nombre")]
        [MaxLength(50, ErrorMessage = "El campo {0} no  puede tener mas de {1} caracteres")]
        [Required(ErrorMessage ="El campo {0} es obligatorio")]
        public string Nombre { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0} no  puede tener mas de {1} caracteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Apellidos { get; set; }
    }
}
