namespace ConversionWebMVC.Servicios
{
    public class ServicioAcceso : IServicioAcceso
    {
        public string usuario { get; set; }
        public string pass { get; set; }

        public void EnviarValores()
        {
            usuario = "";
            pass = "";
        }
    }
}


