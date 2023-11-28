using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;


namespace ConversionWebMVC.Models
{
	public class UsuarioModel: IdentityUser
	{
        public int id { get; set; }        [DataType(DataType.EmailAddress)]
        public string email { get; set; }
		public string password { get; set; }
		public DateTime fechaNacimiento { get; set; }
		public string nombre_pais { get; set; }
    }
}