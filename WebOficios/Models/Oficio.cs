using System;
using System.Collections.Generic;

namespace WebOficios.Models
{
    public partial class Oficio
    {
        public long IdOficio { get; set; }
        public string? NOficio { get; set; }
        public DateTime? Fecha { get; set; }
        public int IdTipo { get; set; }
        public string? Asunto { get; set; }
        public int IdDireccion { get; set; }
        public string? FolioSolicitud { get; set; }
        public byte[]? PdfArchivo { get; set; }
        public string? Realname { get; set; }
        public int IdUsuario { get; set; }

        public virtual Direccion IdDireccionNavigation { get; set; } = null!;
        public virtual TipoOficio IdTipoNavigation { get; set; } = null!;
        public virtual Usuario IdUsuarioNavigation { get; set; } = null!;
    }
}
