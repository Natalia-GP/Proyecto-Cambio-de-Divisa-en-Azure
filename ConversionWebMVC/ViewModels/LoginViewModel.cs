using System.ComponentModel.DataAnnotations;


namespace ConversionWebMVC.ViewModels

{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo Electronico")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Introduzca Password")]
        public string Password { get; set; }
        [Display(Name = "Recordar")]
        public bool RememberMe { get; set;}
    }
}
