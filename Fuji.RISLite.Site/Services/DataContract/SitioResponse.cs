namespace Fuji.RISLite.Site.Services.DataContract
{
    public class SitioResponse
    {
        public bool Success { get; set; }
        public string Mensaje { get; set; }

        public SitioResponse()
        {
            Success = false;
            Mensaje = string.Empty;
        }
    }
}