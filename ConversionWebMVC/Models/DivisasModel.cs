using System.ComponentModel.DataAnnotations;
using ConversionWebMVC.Models;


namespace ConversionWebMVC.Models
{
	public class DivisasModel
    {

        public string acronimo { get; set; }
        public string nombre_divisa { get; set; }
        public int Valor { get; set; }
        public int nombre_pais { get; set; }

    }
}