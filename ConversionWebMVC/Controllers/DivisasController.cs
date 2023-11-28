using Microsoft.AspNetCore.Mvc;
using ConversionWebMVC.Models;
using ConversionWebMVC.ViewModels;
using Microsoft.DotNet.Scaffolding.Shared.Project;

namespace ConversionWebMVC.Controllers
{
	public class DivisasController : Controller
	{

        private readonly Contexto contexto;


        public DivisasController(Contexto contexto)
        {

            this.contexto = contexto;

        }

        public IActionResult Divisas(ConversionWebMVC.Models.UsuarioModel modelo)
		{

            var modelovista = new UsuarioDivisasViewModel();

            ViewBag.email = modelo.email;

            modelovista.valorFinal = 0;

            return View(modelovista);
        }

        public IActionResult IrAHistorico()
        {
            return RedirectToAction("Historico", "Historico");
        }

        public IActionResult IrAHistorico2(UsuarioDivisasViewModel modelo)
        {
            string a = modelo.email;

            ViewBag.email = modelo.email;

            return RedirectToAction("Index", "HistoricoModels", a);
        }











        [HttpPost]
        public async Task<ActionResult> ConvertirMoneda(UsuarioDivisasViewModel modelo)
        {
            try
            {
                double res = await ConvertirMonedaAPIAsync(modelo.divisa1, modelo.divisa2, modelo.valorInicial, modelo.email);
               

                modelo.valorFinal = res;
                return View("Divisas", modelo);
            }
            catch (Exception ex)
            {
                   Console.WriteLine("Error al convertir moneda: " + ex.Message);
                return StatusCode(500, "Error interno del servidor");
            }
        }

        public async Task<double> ConvertirMonedaAPIAsync(string Divisa1, string Divisa2, double cantidad, string usuario)
        {
            string apiKey = "16bdfe57c1175f87ac5e35b62db1ba7a";

            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"http://apilayer.net/api/live?access_key={apiKey}&currencies={Divisa1}&source={Divisa2}&format=1";

                int posicionInicial;
                int posicionFinal;

                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    posicionInicial = jsonResponse.IndexOf($"{Divisa2}{Divisa1}") + 8;
                    posicionFinal = jsonResponse.IndexOf("\n  }\n}");

                    jsonResponse = jsonResponse.Substring(posicionInicial, posicionFinal - posicionInicial);
                    jsonResponse = jsonResponse.Replace(".", ",");

                    var historico = new HistoricoModel()
                    {
                        ValorInicial = cantidad.ToString() + " " + Divisa1,
                        ValorFinal = (double.Parse(jsonResponse) * cantidad).ToString() + " " + Divisa2,
                        tiempo = DateTime.Now,
                        NombreUsuario = usuario

                    };

                    guardarHistorico(historico);
                    return double.Parse(jsonResponse) * cantidad;
                }
                else
                {
                    Console.WriteLine("Error al hacer la solicitud a la API de Fixer: " + response.StatusCode);
                    return 0;
                }
            }
        }

        public void guardarHistorico(HistoricoModel modelo)
        {
            contexto.Historico.Add(modelo);
            contexto.SaveChanges();
        }





    }
}
