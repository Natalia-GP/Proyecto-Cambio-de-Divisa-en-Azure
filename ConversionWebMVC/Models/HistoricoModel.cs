using System.ComponentModel.DataAnnotations;
using ConversionWebMVC.Models;


namespace ConversionWebMVC.Models
{
	public class HistoricoModel
    {

        public int id { get; set; }
        public DateTime tiempo { get; set; }
        public string ValorInicial { get; set; }
        public string ValorFinal { get; set; }
        public string NombreUsuario { get; set; }

    }
}