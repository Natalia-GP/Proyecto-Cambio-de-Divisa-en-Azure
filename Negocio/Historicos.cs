using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class Historicos
    {

		public DateTime Fecha { get; set; }
		public string Valor1 { get; set; }

		public string Valor2 { get; set; }

		public string Email { get; set; }

		public Historicos(DateTime fecha, string valor1, string valor2, string email)
		{
			Fecha = fecha;
			Valor1 = valor1;
			Valor2 = valor2;
            Email = email;
		}
		//MUESTRA EL HISTORIAL
		public static void MostrarHistorial(string filePath = "../../../../historial.txt")
        {
            if (!File.Exists(filePath))
            {
                CrearArchivo(filePath);
            }

            List<string> lineas = LeerArchivo(filePath);

            foreach (string linea in lineas)
            {
                Console.WriteLine(linea);
            }
            Console.WriteLine("\nPresiona Enter para continuar...");
            Console.ReadLine();
        }


        //LEER EL .TXT
        public static List<string> LeerArchivo(string filePath)
        {
            List<string> lineas = new List<string>();

            if (!File.Exists(filePath))
            {
                CrearArchivo(filePath);
            }

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string linea;
                    while ((linea = sr.ReadLine()) != null)
                    {
                        lineas.Add(linea);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al leer el archivo: " + e.Message);
            }

            return lineas;
        }

        //CREA EL .TXT
        public static void CrearArchivo(string filePath)
        {
            File.Create(filePath).Close();
            Console.WriteLine($"Archivo '{filePath}' creado.");
        }

        //AÑADE UNA NUEVA ENTRADA AL .TXT
        public static void AgregarLinea(string filePath, string divisa1, string divisa2, string usuario)
        {

            //A LA HORA DE USAR EL METODO HAY QUE DECLARAR divisa1, divisa2 y usuario    

            try
            {
                using (StreamWriter sw = File.AppendText(filePath))
                {
                    sw.WriteLine(DateTime.Now.ToString("dd/MM/yyyy") + "|" + DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second + "|" + divisa1 + "|" + divisa2 + "|" + usuario);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al escribir en el archivo: " + e.Message);
            }
        }



        public static void MostrarLineasConPalabra(string filePath, string palabra)
        {
            List<string> lineasFiltradas = FiltrarLineasConPalabra(filePath, palabra);

            foreach (string linea in lineasFiltradas)
            {
                Console.WriteLine(linea);
            }
        }

        public static List<string> FiltrarLineasConPalabra(string filePath, string palabra)
        {
            List<string> lineasFiltradas = new List<string>();

            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string linea;
                    while ((linea = sr.ReadLine()) != null)
                    {
                        if (linea.Contains(palabra))
                        {
                            lineasFiltradas.Add(linea);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al leer el archivo: " + e.Message);
            }

            return lineasFiltradas;
        }


    }
}
