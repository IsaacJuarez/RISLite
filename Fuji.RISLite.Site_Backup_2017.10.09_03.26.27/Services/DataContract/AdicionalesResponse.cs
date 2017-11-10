namespace Fuji.RISLite.Site.Services.DataContract
{
    public class AdicionalesResponse
    {
        public bool Success { get; set; }
        public string Mensaje { get; set; }

        public AdicionalesResponse()
        {
            Success = false;
            Mensaje = string.Empty;
        }
    }
}