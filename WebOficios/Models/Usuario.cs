using System;
using System.Collections.Generic;

namespace WebOficios.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Oficios = new HashSet<Oficio>();
        }

        public int IdUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Correo { get; set; }
        public string? Password { get; set; }
        public string? Estado { get; set; }

        public virtual ICollection<Oficio> Oficios { get; set; }
    }
}
