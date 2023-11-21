namespace ConversionWebMVC.Servicios
{
    public class ServicioDivisa
    {
        string acronimo { get; set; }
        string nombre_divisa { get; set; }
        int Valor { get; set; }
        string nombre_pais { get; set; }
        public void EnviarDivisa() 
        {
            acronimo = "";
            nombre_divisa = "";
            Valor= 0;
            nombre_pais = "";

        }
    }
}
