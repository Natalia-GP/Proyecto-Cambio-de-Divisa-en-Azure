using Microsoft.VisualBasic;
using Newtonsoft.Json;

namespace Negocio
{
    public class Divisas
    {

        public static async Task<double> ConvertirMonedaAPI(string Divisa1, string Divisa2, double cantidad, string usuario)
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

                    //Json.JsonCurrencyLayer json = JsonConvert.DeserializeObject<Json.JsonCurrencyLayer>(jsonResponse);                                       


                    posicionInicial = jsonResponse.IndexOf($"{Divisa2}{Divisa1}") + 8;
                    posicionFinal = jsonResponse.IndexOf("\n  }\n}");

                    jsonResponse = jsonResponse.Substring(posicionInicial, posicionFinal- posicionInicial);
                    jsonResponse = jsonResponse.Replace(".", ",");
                    Datos.Consultas.AgregarRegistroHistorico(cantidad.ToString() + " " + Divisa1, 
                                                            (double.Parse(jsonResponse) * cantidad).ToString() + " " + Divisa2, 
                                                            usuario);
                    return double.Parse(jsonResponse) * cantidad;
                }
                else
                {
                    Console.WriteLine("Error al hacer la solicitud a la API de Fixer: " + response.StatusCode);
                    return 0;
                }
            }
        }
    }
}