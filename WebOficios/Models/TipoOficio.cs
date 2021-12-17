using System;
using System.Collections.Generic;

namespace WebOficios.Models
{
    public partial class TipoOficio
    {
        public TipoOficio()
        {
            Oficios = new HashSet<Oficio>();
        }

        public int IdTipo { get; set; }
        public string? Nombre { get; set; }

        public virtual ICollection<Oficio> Oficios { get; set; }
    }
}
