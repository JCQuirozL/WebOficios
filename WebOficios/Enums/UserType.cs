using System.ComponentModel.DataAnnotations;

namespace WebOficios.Enums
{
    public enum UserType
    {
        [Display(Name = "Administrador")]
        Admin,
        [Display(Name = "Usuario")]
        User
        
    }
}
