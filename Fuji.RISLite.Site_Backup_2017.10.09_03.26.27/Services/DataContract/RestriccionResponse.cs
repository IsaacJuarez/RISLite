namespace Fuji.RISLite.Site.Services.DataContract
{
    public class RestriccionResponse
    {
        public bool Success { get; set; }
        public string Mensaje { get; set; }

        public RestriccionResponse()
        {
            Success = false;
            Mensaje = string.Empty;
        }
    }
}