using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

public class Imprimir
{
    public static string RutaArchivo = "../../../../Divisas.txt";

    public static void Tabla()
    {
        Console.Clear();
        Console.WriteLine("           |========================================================================================|");
        Console.WriteLine("           |                                                                                        |");
        Console.WriteLine("           |                           Configuraciones de Moneda                                    |");
        Console.WriteLine("           |                                                                                        |");
        Console.WriteLine("           |========================================================================================|");

        List<Divisa> divisas = CargarDatosDesdeArchivo(RutaArchivo);

        if (divisas != null)
        {
            int elementosPorPagina = 10;
            int paginaActual = 1;
            int totalPaginas = (divisas.Count + elementosPorPagina - 1) / elementosPorPagina;

            do
            {
                Console.Clear();
                CultureInfo culturaOriginal = CultureInfo.CurrentCulture;
                CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

                Console.WriteLine("Página {0}/{1}", paginaActual, totalPaginas);
                Console.WriteLine(new string('-', 60));
                int indiceInicio = (paginaActual - 1) * elementosPorPagina;
                int indiceFin = Math.Min(indiceInicio + elementosPorPagina, divisas.Count);

                for (int i = indiceInicio; i < indiceFin; i++)
                {
                    var divisa = divisas[i];
                    Console.WriteLine("{0,-25} {1,-15} {2,-15:F4}", divisa.Pais, divisa.Siglas, divisa.TipoCambio);
                }

                CultureInfo.CurrentCulture = culturaOriginal;

                Console.WriteLine("\nOpciones:");
                Console.WriteLine("1. Presiona Enter para avanzar");
                Console.WriteLine("2. Ingresa un valor de búsqueda");
                Console.WriteLine("3. Modificar el valor de una divisa (nombre o siglas)");
                Console.WriteLine("4. para salir");
                string entradaUsuario = Console.ReadLine().Trim();

                if (entradaUsuario.Equals("4", StringComparison.OrdinalIgnoreCase))
                {
                    try
                    {
                        Console.WriteLine("¿Deseas guardar los cambios? (S/N)");
                        string opcionGuardar = Console.ReadLine().Trim();

                        if (opcionGuardar.Equals("S", StringComparison.OrdinalIgnoreCase))
                        {
                            GuardarDatosEnArchivo(RutaArchivo, divisas);
                        }

                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error al guardar el archivo: " + ex.Message);
                    }
                }
                else if (entradaUsuario.Equals("1"))
                {
                    if (paginaActual < totalPaginas)
                    {
                        paginaActual++;
                    }
                }
                else if (entradaUsuario.Equals("2"))
                {
                    Console.Write("Ingresa una cadena de búsqueda: ");
                    string entradaBusqueda = Console.ReadLine();
                    RealizarBusqueda(divisas, entradaBusqueda);
                }
                else if (entradaUsuario.Equals("3"))
                {
                    Console.Write("Ingresa el nombre o las siglas de la divisa a modificar: ");
                    string entradaDivisa = Console.ReadLine();
                    ModificarValorDivisa(divisas, entradaDivisa);
                }
            } while (true);
        }
        else
        {
            Console.WriteLine("El archivo no existe.");
        }
    }

    public static List<Divisa> CargarDatosDesdeArchivo(string rutaArchivo)
    {
        List<Divisa> divisas = new List<Divisa>();

        if (File.Exists(rutaArchivo))
        {
            string[] lineas = File.ReadAllLines(rutaArchivo);

            foreach (string linea in lineas)
            {
                string[] partes = linea.Split('|');
                if (partes.Length == 3)
                {
                    string pais = partes[0];
                    string siglas = partes[1];
                    double tipoCambio;

                    if (double.TryParse(partes[2], out tipoCambio))

                    {
                        divisas.Add(new Divisa { Pais = pais, Siglas = siglas, TipoCambio = tipoCambio });
                    }
                    else
                    {
                        Console.WriteLine("Error de formato en el tipo de cambio para la línea: " + linea);
                    }
                }
            }
        }

        return divisas;
    }

    public static void RealizarBusqueda(List<Divisa> divisas, string entradaBusqueda)
    {
        var resultadosBusqueda = divisas.Where(d =>
            d.Pais.Contains(entradaBusqueda, StringComparison.OrdinalIgnoreCase) ||
            d.Siglas.Contains(entradaBusqueda, StringComparison.OrdinalIgnoreCase)).ToList();

        if (resultadosBusqueda.Any())
        {
            Console.WriteLine("\nResultados de la búsqueda:");
            Console.WriteLine(new string('-', 60));

            foreach (var divisa in resultadosBusqueda)
            {
                string tipoCambioString = divisa.TipoCambio.ToString("F4", CultureInfo.InvariantCulture).Replace('.', ',');
                Console.WriteLine("{0,-25} {1,-15} {2,-15}", divisa.Pais, divisa.Siglas, tipoCambioString);
            }

            Console.WriteLine(new string('-', 60));
        }
        else
        {
            Console.WriteLine("No se encontraron resultados para la búsqueda.");
        }

        Console.WriteLine("\nPresiona Enter para continuar...");
        Console.ReadLine();
    }



    public static void ModificarValorDivisa(List<Divisa> divisas, string entradaDivisa)
    {
        var divisaAModificar = divisas.FirstOrDefault(d =>
            d.Pais.Contains(entradaDivisa, StringComparison.OrdinalIgnoreCase) ||
            d.Siglas.Contains(entradaDivisa, StringComparison.OrdinalIgnoreCase));

        if (divisaAModificar != null)
        {
            Console.Write("Ingresa el nuevo valor del tipo de cambio: ");
            if (double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out double nuevoTipoCambio))
            {
                divisaAModificar.TipoCambio = nuevoTipoCambio;
                Console.WriteLine("Valor de la divisa actualizado con éxito.");
            }
            else
            {
                Console.WriteLine("El valor ingresado no es válido.");
            }
        }
        else
        {
            Console.WriteLine("La divisa no se encontró en la lista.");
        }

        Console.WriteLine("\nPresiona Enter para continuar...");
        Console.ReadLine();
    }

    public static void GuardarDatosEnArchivo(string rutaArchivo, List<Divisa> divisas)
    {
        try
        {
            using StreamWriter escritor = new StreamWriter(rutaArchivo);

            foreach (var divisa in divisas)
            {
                string tipoCambioString = divisa.TipoCambio.ToString("F4");
                escritor.WriteLine($"{divisa.Pais}|{divisa.Siglas}|{tipoCambioString.Replace('.', ',')}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al escribir en el archivo: " + ex.Message);
        }
    }

}

public class Divisa
{
    public string Pais { get; set; }
    public string Siglas { get; set; }
    public double TipoCambio { get; set; }
}
