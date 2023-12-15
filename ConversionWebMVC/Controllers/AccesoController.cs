using ConversionWebMVC.Models;
using ConversionWebMVC.Servicios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ConversionWebMVC.Controllers
{
    public class AccesoController : Controller
    {
        private readonly IServicioAcceso servicioAcceso;
        private readonly Contexto contexto;

        public AccesoController(IServicioAcceso servicioAcceso, Contexto contexto) 
		{
            this.servicioAcceso = servicioAcceso;
            this.contexto = contexto;
        }
        public IActionResult Acceso()
        {
			servicioAcceso.EnviarValores();
            ViewBag.usuario = servicioAcceso.usuario;
            ViewBag.pass = servicioAcceso.pass;
            return View();
        }

		public IActionResult ProcesarFormulario(ConversionWebMVC.Models.UsuarioModel modelo)
        {
            string? pruebaconexion = contexto.Database.GetConnectionString();

            TempData["email"] = modelo.email;

			contexto.Usuario.Add(modelo);
            contexto.SaveChanges();
            return RedirectToAction("Divisas", "Divisas", modelo);
        }

        public IActionResult VerificarUsuario(ConversionWebMVC.Models.UsuarioModel modelo)
		{

            //bool usuarioValido = Datos.Consultas.VerificarUsuario(modelo.email, modelo.password);

            bool total = contexto.Usuario.Any(t => t.email == modelo.email && t.password == modelo.password);

            if (total)
			{
                TempData["email"] = modelo.email;


                
                
                return RedirectToAction("Divisas", "Divisas", modelo);




            }
			else
			{
				TempData["ErrorMessage"] = "Usuario no válido. Por favor, verifique sus credenciales.";
				return RedirectToAction("Acceso");
			}
		}



	}
}
