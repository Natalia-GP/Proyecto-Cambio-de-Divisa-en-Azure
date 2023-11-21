namespace ConversionWebMVC.Servicios
{
    public class ServicioDivisaPruebas
    {
        string acronimo { get; set; }
        string nombre_divisa { get; set; }
        int Valor { get; set; }
        string nombre_pais { get; set; }
        public void EnviarDivisa()
        {
            acronimo = "USD";
            nombre_divisa = "Dolar";
            Valor = 0 ;
            nombre_pais = "";

        }
    }
}
