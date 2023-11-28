using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ConversionWebMVC.Models
{
    public class Contexto : IdentityDbContext<UsuarioModel>
    {
        public Contexto(DbContextOptions<Contexto> opciones)
            : base(opciones)
        {

        }
        //Tabla paises
        public DbSet<UsuarioModel> Usuario { get; set; }

        public DbSet<DivisasModel> Divisa { get; set; }

        public DbSet<HistoricoModel> Historico { get; set; }
    }
}
