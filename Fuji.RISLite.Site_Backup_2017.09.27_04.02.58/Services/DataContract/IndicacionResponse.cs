namespace Fuji.RISLite.Site.Services.DataContract
{
    public class IndicacionResponse
    {
        public bool Success { get; set; }
        public string Mensaje { get; set; }

        public IndicacionResponse()
        {
            Success = false;
            Mensaje = string.Empty;
        }
    }
}