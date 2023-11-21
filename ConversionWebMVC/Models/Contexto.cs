using Microsoft.EntityFrameworkCore;

namespace ConversionWebMVC.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> opciones)
            : base(opciones)
        {

        }
        //Tabla paises
        public DbSet<UsuarioModel> Usuario { get; set; }

        public DbSet<DivisasModel> Divisa { get; set; }

    }
}
