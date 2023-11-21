using ConversionWebMVC.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace ConversionWebMVC.Controllers
{
    public class AccesoController : Controller
    {
        private readonly IServicioAcceso servicioAcceso;

        public AccesoController(IServicioAcceso servicioAcceso) 
		{
            this.servicioAcceso = servicioAcceso;
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
			Datos.Consultas.AgregarUsuario(modelo.email, modelo.password, modelo.fechaNacimiento, modelo.nombre_pais);
			//TempData["email"] = modelo.UsuarioModel.email;
            return RedirectToAction("Divisas", "Divisas", modelo);
        }

        public IActionResult VerificarUsuario(ConversionWebMVC.Models.UsuarioModel modelo)
		{

			bool usuarioValido = Datos.Consultas.VerificarUsuario(modelo.email, modelo.password);

			if (usuarioValido)
			{
				//TempData["email"] = modelo.UsuarioModel.email;
				return RedirectToAction("Divisas", "Divisas", modelo);
            }
			else
			{
				TempData["ErrorMessage"] = "Usuario no válido. Por favor, verifique sus credenciales.";
				return RedirectToAction("Registro");
			}
		}
	}
}
