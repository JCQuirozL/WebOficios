using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using WebOficios.Enums;

namespace WebOficios.Models
{
    public partial class Usuario: IdentityUser
    {
        public Usuario()
        {
            Oficios = new HashSet<Oficio>();
        }

        public int IdUsuario { get; set; }

        [Display(Name = "Nombre")]
        [MaxLength(100)]
        [Required]
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Password { get; set; }

        [Display(Name = "Tipo de usuario")]
        public UserType UserType { get; set; }
        public string? Estado { get; set; }

       
        public virtual ICollection<Oficio> Oficios { get; set; }
    }
}
