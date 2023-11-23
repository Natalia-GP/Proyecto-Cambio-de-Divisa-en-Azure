using Microsoft.AspNetCore.Mvc;


namespace ConversionWebMVC.Controllers
{
    public class HistoricoController : Controller
    {
        public IActionResult Historico()
        {
            // List<string> resultados = ObtenerHistorico();

            return View();
        }

        public IActionResult IrAConversor()
        {
            return RedirectToAction("Divisas", "Divisas");
        }

        //public static List<string> ObtenerHistorico()
        //{
        //    string connectionString = @"Server=db4free.net; Database=conversiondivisa; Uid=conversiondivisa; Pwd=conversiondivisa; Port=3306;";
        //    string query = "SELECT * FROM Historico";
        //    List<string> listaResultados = new List<string>();
        //    string objeto;


        //    using (MySqlConnection connection = new MySqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        using (MySqlCommand command = new MySqlCommand(query, connection))
        //        {
        //            using (MySqlDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    objeto = "Fecha:" + reader.GetDateTime("fecha").ToString() + " | Valor y divisa entrante: " + reader.GetString("valor2") +
        //                        " | Valor y divisa saliente: " + reader.GetString("valor1") + " | Email:" + reader.GetString("email");
        //                    listaResultados.Add(objeto);
        //                }
        //            }
        //        }
        //    }
        //    return listaResultados;

        //}


        


    }
}
