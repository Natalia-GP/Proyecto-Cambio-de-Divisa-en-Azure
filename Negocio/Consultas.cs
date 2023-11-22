using MySql.Data.MySqlClient;
using Negocio;


namespace Datos
{
    public class Consultas
    {
        public static MySqlConnection CrearConnection()
        {

            string connectionString = @"Server=db4free.net; Database=conversiondivisa; Uid=conversiondivisa; Pwd=conversiondivisa; Port=3306;";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    return connection;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error al crear conexión: " + ex.Message);
                    return null;
                }
            }

        }

        public static void AgregarUsuario(string email, string PASSWORD, DateTime fechaNacimiento, string nombre_pais)
        {

            string connectionString = @"Server=db4free.net; Database=conversiondivisa; Uid=conversiondivisa; Pwd=conversiondivisa; Port=3306;";


            // Consulta SQL para insertar un nuevo registro
            string query = "INSERT INTO Usuario (email,PASSWORD,fechaNacimiento,nombre_pais) VALUES (@email,@PASSWORD,@fechaNacimiento,@nombre_pais)";

            // Establecer la conexión
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Abrir la conexión
                    connection.Open();

                    // Crear un objeto de comando y establecer los parámetros
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@PASSWORD", PASSWORD);
                        command.Parameters.AddWithValue("@fechaNacimiento", fechaNacimiento);
                        command.Parameters.AddWithValue("@nombre_pais", nombre_pais);

                        // Ejecutar la consulta
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("Usuario insertado correctamente.");
                        }
                        else
                        {
                            Console.WriteLine("No se pudo insertar el usuario.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        public static bool VerificarUsuario(string email, string PASSWORD)
        {
            string connectionString = @"Server=db4free.net; Database=conversiondivisa; Uid=conversiondivisa; Pwd=conversiondivisa; Port=3306;";
            string query = "SELECT * FROM Usuario WHERE email = @email AND PASSWORD = @PASSWORD";

            // Crear conexión
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@email", email);
                        command.Parameters.AddWithValue("@PASSWORD", PASSWORD);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                return true; // Usuario encontrado
                            }
                            else
                            {
                                return false; // Usuario no encontrado
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }

        public static void AgregarRegistroHistorico(string valor1, string valor2, string email)
		{
            DateTime fecha = DateTime.Now;
			string connectionString = @"Server=db4free.net; Database=conversiondivisa; Uid=conversiondivisa; Pwd=conversiondivisa; Port=3306;";


			// Consulta SQL para insertar un nuevo registro
			string query = "INSERT INTO Historico (fecha,valor1,valor2,email) VALUES (@fecha, @valor1,@valor2,@email)";

			// Establecer la conexión
			using (MySqlConnection connection = new MySqlConnection(connectionString))
			{
				try
				{
					// Abrir la conexión
					connection.Open();

					// Crear un objeto de comando y establecer los parámetros
					using (MySqlCommand command = new MySqlCommand(query, connection))
					{
						command.Parameters.AddWithValue("@fecha", fecha);
						command.Parameters.AddWithValue("@valor1", valor2);
						command.Parameters.AddWithValue("@valor2", valor1);
						command.Parameters.AddWithValue("@email", email);

						// Ejecutar la consulta
						int rowsAffected = command.ExecuteNonQuery();

						if (rowsAffected > 0)
						{
							Console.WriteLine("Registro insertado correctamente.");
						}
						else
						{
							Console.WriteLine("No se pudo insertar el registro.");
						}
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Error: " + ex.Message);
				}
			}
		}

		public static void ObtenerUsuarios()
        {
            string connectionString = @"Server=db4free.net; Database=conversiondivisa; Uid=conversiondivisa; Pwd=conversiondivisa; Port=3306;";

            // Consulta SQL para seleccionar datos de una tabla
            string query = "SELECT * FROM Usuario";

            // Establecer la conexión
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Abrir la conexión
                    connection.Open();

                    // Crear un objeto de comando y ejecutar la consulta
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        // Crear un lector de datos
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            // Leer y mostrar los datos
                            Console.WriteLine($"--- LISTA DE USUARIOS ---");
                            while (reader.Read())
                            {
                                string email = reader.GetString("email");
                                string nombre_pais = reader.GetString("nombre_pais");

                                Console.WriteLine($"Email: {email}, pais: {nombre_pais}");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        public static List<Historicos> ObtenerHistorico()
        {
            string connectionString = @"Server=db4free.net; Database=conversiondivisa; Uid=conversiondivisa; Pwd=conversiondivisa; Port=3306;";
            string query = "SELECT * FROM Historico";
            List<Historicos> listaResultados = new List<Historicos>();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand(query, connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Historicos objeto = new Historicos
                            (
                                reader.GetDateTime("fecha"),
                                reader.GetString("valor1"),
                                reader.GetString("valor2"),
                                reader.GetString("email")
                            );
							listaResultados.Add(objeto);
                        }
                    }
                }
            }
            return listaResultados;

        }

			public static void AgregarPais(string nombre_pais)
        {

            string connectionString = @"Server=db4free.net; Database=conversiondivisa; Uid=conversiondivisa; Pwd=conversiondivisa; Port=3306;";


            // Consulta SQL para insertar un nuevo registro
            string query = "INSERT INTO Pais (nombre_pais) VALUES (@nombre_pais)";

            // Establecer la conexión
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    // Abrir la conexión
                    connection.Open();

                    // Crear un objeto de comando y establecer los parámetros
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nombre_pais", nombre_pais);

                        // Ejecutar la consulta
                        int rowsAffected = command.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            Console.WriteLine("País insertado correctamente.");
                        }
                        else
                        {
                            Console.WriteLine("No se pudo insertar el país.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}