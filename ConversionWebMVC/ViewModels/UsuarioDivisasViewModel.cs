using System.ComponentModel.DataAnnotations;
using ConversionWebMVC.Models;

namespace ConversionWebMVC.ViewModels
{
    public class UsuarioDivisasViewModel
    {


        public string divisa1 { get; set; }
        public string divisa2 { get; set; }
        public double valorInicial { get; set; }
        public double valorFinal { get; set; }
        public string email { get; set; }
    }
}