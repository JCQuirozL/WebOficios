using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebOficios.Models
{
    public partial class Oficio
    {
        [Key]
        [Required]
        public long IdOficio { get; set; }
        public string? NOficio { get; set; }


        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? Fecha { get; set; }


        [Required(ErrorMessage = "Debe Seleccionar un Tipo de Oficio")]
        public int IdTipo { get; set; }
        public string? Asunto { get; set; }


        [Required(ErrorMessage = "Debe indicar la Dirección")]
        public int IdDireccion { get; set; }


        [Required(ErrorMessage = "No puede quedar en blanco")]
        public string? FolioSolicitud { get; set; }
        public byte[]? PdfArchivo { get; set; }
        public string? Realname { get; set; }
        public int? IdUsuario { get; set; }
        public string? Capturo { get; set; }

        public virtual Direccion? IdDireccionNavigation { get; set; }
        public virtual TipoOficio? IdTipoNavigation { get; set; }
        
    }
}
