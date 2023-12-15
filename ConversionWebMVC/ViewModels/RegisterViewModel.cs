using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ConversionWebMVC.ViewModels
{

    public class RegisterViewModel

    {
        [Required(ErrorMessage = "El correo electronico no puede quedar en blanco")]
        [EmailAddress]
        [Display(Name = "Correo Electrónico")]
        public string Email { get; set; }
        [Required(ErrorMessage = "La contraseña no puede quedar en blanco")]
        [DataType(DataType.Password)]
        [Display(Name = "Introduzca password")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Este campo no puede quedar en blanco")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme password")]
        [Compare("Password", ErrorMessage = "La contraseña y confirmación no coinciden")]
        public string ConfirmPassword { get; set; }

        [Required]

        public DateTime Birthday { get; set; }

    }
}
