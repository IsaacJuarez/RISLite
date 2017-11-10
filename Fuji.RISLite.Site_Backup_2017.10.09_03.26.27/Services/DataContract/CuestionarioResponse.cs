namespace Fuji.RISLite.Site.Services.DataContract
{
    public class CuestionarioResponse
    {
        public bool Success { get; set; }
        public string Mensaje { get; set; }

        public CuestionarioResponse()
        {
            Success = false;
            Mensaje = string.Empty;
        }
    }
}