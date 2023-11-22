namespace Negocio
{
    public class Usuarios
    {
        public static string filePath = "../../../../usuarios.txt";

        public static bool IniciarSesion(string correo, string contrasena)
        {
            bool usuarioEncontrado = false;

            if (File.Exists(filePath))
            {
                string[] lineas = File.ReadAllLines(filePath);

                foreach (string linea in lineas)
                {
                    string[] elementos = linea.Split(',');

                    if (elementos.Length == 3 && elementos[0] == correo && elementos[1] == contrasena)
                    {
                        usuarioEncontrado = true;
                        break;
                    }
                }

                if (!usuarioEncontrado)
                {
                    Console.WriteLine("Inicio de sesión fallido. Correo o contraseña incorrectos.");
                }
            }
            else
            {
                Console.WriteLine("Inicio de sesión fallido. El archivo de usuarios no existe.");
            }

            return usuarioEncontrado;
        }

        public static void Registrarse( string correo, string contrasena, string pais )
        {
            using (StreamWriter sf = File.AppendText(filePath))
            {
                sf.WriteLine(correo + "," + contrasena + "," + pais);
            }
        }


        public static bool ValidarInicioSesion(string correo, string contrasena)
        {
            bool usuarioEncontrado = false;

            if (File.Exists(filePath))
            {
                string[] lineas = File.ReadAllLines(filePath);

                foreach (string linea in lineas)
                {
                    string[] elementos = linea.Split(',');

                    if (elementos.Length == 3 && elementos[0] == correo && elementos[1] == contrasena)
                    {
                        usuarioEncontrado = true;
                        break;
                    }
                }
            }
            else
            {
                Console.WriteLine("Inicio de sesión fallido. El archivo de usuarios no existe.");
            }

            return usuarioEncontrado;
        }


    }
}