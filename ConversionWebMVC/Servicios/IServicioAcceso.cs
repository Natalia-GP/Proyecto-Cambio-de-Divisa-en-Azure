namespace ConversionWebMVC.Servicios
{
    public interface IServicioAcceso
    {
        string usuario { get; set; }
        string pass { get; set; }
        void EnviarValores();
    }
}

