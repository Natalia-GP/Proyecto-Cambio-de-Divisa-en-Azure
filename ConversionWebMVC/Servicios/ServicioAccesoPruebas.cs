namespace ConversionWebMVC.Servicios
{
    public class ServicioAccesoPruebas : IServicioAcceso
    {
        public string usuario { get; set; }
        public string pass { get; set; }
        public void EnviarValores()
        {
            usuario = "hola@hola.com";
            pass = "hola";
        }
    }
}


