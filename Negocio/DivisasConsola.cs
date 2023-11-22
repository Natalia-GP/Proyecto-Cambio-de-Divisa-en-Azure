using System; 
using System.Collections.Generic;
using System.IO;

namespace Negocio
{
    public static class DivisasConsola
    {
        private static readonly string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "../../../../Divisas.txt");

        public static void ConvertirMoneda()
        {
            try
            {
                double cantidad = ObtenerCantidad();
                string monedaOrigen = ObtenerIniciales("moneda de origen");
                string monedaDestino = ObtenerIniciales("moneda de destino");

                var divisas = ObtenerDivisasDesdeArchivo();

                // Encontrar las divisas de origen y destino
                var divisaOrigen = ObtenerDivisa(divisas, monedaOrigen);
                var divisaDestino = ObtenerDivisa(divisas, monedaDestino);

                // Almacenar tipos de cambio en variables
                double tipoCambioOrigen = divisaOrigen.TipoCambio;
                double tipoCambioDestino = divisaDestino.TipoCambio;

                // Mostrar tipos de cambio antes de realizar la conversión
                Console.WriteLine($"El Tipo de cambio del {divisaOrigen} es {tipoCambioOrigen}");
                Console.WriteLine($"El Tipo de cambio del {divisaDestino} es {tipoCambioDestino}");

                // Realizar la conversión y mostrar el resultado
                double resultado = RealizarConversion(cantidad, tipoCambioOrigen, tipoCambioDestino);
                Console.WriteLine($"Resultado: {cantidad} {monedaOrigen} equivale a {resultado} {monedaDestino}");

                // Solicitar al usuario que registre su nombre para la operación
                Console.WriteLine("Registra el usuario que ha realizado la operación:");
                string usuario = Console.ReadLine();


                // Crear cadenas de texto para el historial
                string divisa1 = $"{cantidad} {monedaOrigen}";
                string divisa2 = $"{resultado} {monedaDestino}";

                // Agregar la operación al historial
                Historicos.AgregarLinea("../../../../historial.txt", divisa1, divisa2, usuario);
            }
            catch (Exception ex)
            {
                // Manejar excepciones generales
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static double ObtenerCantidad()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Ingrese la cantidad a convertir:");
                    return Convert.ToDouble(Console.ReadLine());
                }
                catch (FormatException)
                {
                    // Mostrar un mensaje de error si la entrada no puede convertirse a decimal
                    Console.WriteLine("Error: Ingrese un valor válido para la cantidad.");
                }
            }
        }

        private static string ObtenerIniciales(string tipo)
        {
            while (true)
            {
                Console.WriteLine($"Introduce iniciales de la {tipo}:");
                string iniciales = Console.ReadLine().ToUpper();

                if (!string.IsNullOrWhiteSpace(iniciales) && iniciales.Length == 3)
                {
                    return iniciales;
                }

                // Mostrar un mensaje de error si las iniciales no son válidas
                Console.WriteLine($"Debes introducir iniciales válidas (exactamente 3 caracteres).");
            }
        }

        private static List<(string Nombre, string Iniciales, double TipoCambio)> ObtenerDivisasDesdeArchivo()
        {
            var divisas = new List<(string Nombre, string Iniciales, double TipoCambio)>();

            try
            {
                // Leer las líneas del archivo
                string[] lineas = File.ReadAllLines(filePath);

                // Procesar cada línea y agregar las divisas a la lista
                foreach (var linea in lineas)
                {
                    string[] campos = linea.Split('|');
                    if (campos.Length == 3)
                    {
                        string nombre = campos[0].Trim();
                        string iniciales = campos[1].Trim();
                        if (double.TryParse(campos[2].Trim(), out double tipoCambio) && tipoCambio > 0)
                        {
                            divisas.Add((nombre, iniciales, tipoCambio));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al leer el archivo: {ex.Message}");
            }

            return divisas;
        }



        private static (string Nombre, string Iniciales, double TipoCambio) ObtenerDivisa(List<(string Nombre, string Iniciales, double TipoCambio)> divisas, string iniciales)
        {
            var divisa = divisas.Find(d => string.Equals(d.Iniciales, iniciales, StringComparison.OrdinalIgnoreCase));

            if (divisa == default)
            {
                throw new Exception($"Divisa con iniciales '{iniciales}' no encontrada.");
            }

            return divisa;
        }

        private static double RealizarConversion(double cantidad, double tipoCambioOrigen, double tipoCambioDestino)
        {
            double resultado;
            // Validar tasas de cambio
            if (tipoCambioOrigen == 0 || tipoCambioDestino == 0)
            {
                throw new Exception("Error: Tasa de cambio no válida.");
            }

            if (tipoCambioOrigen == 1) 
            {
                resultado = cantidad * tipoCambioDestino;
            }
            else
            {
                resultado = cantidad * (tipoCambioDestino / tipoCambioOrigen);
                resultado = Math.Round(resultado, 2);
            }
            return resultado;
        }




    }
}


