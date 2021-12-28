using System;
using System.Collections.Generic;

namespace WebOficios.Models
{
    public partial class Direccion
    {
        public Direccion()
        {
            Oficios = new HashSet<Oficio>();
        }

        public int IdDireccion { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Oficio> Oficios { get; set; }
    }
}
