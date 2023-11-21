namespace ConversionWebMVC.Servicios
{
    public interface IServicioDivisa
    {
        string acronimo { get; set; }
        string nombre_divisa { get; set; }
        int Valor { get; set; }
        int nombre_pais { get; set; }
        void  EnviarDivisa();
    }
}
