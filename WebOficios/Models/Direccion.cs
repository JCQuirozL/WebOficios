using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebOficios.Models
{
    public partial class Direccion
    {
        public Direccion()
        {
            Oficios = new HashSet<Oficio>();
        }

        [Key]
        public int IdDireccion { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage ="El campo {0} es requerido")]
        public string? Nombre { get; set; }

        public virtual ICollection<Oficio> Oficios { get; set; }
    }
}
