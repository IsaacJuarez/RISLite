namespace Fuji.RISLite.Site.Services.DataContract
{
    public class EquipoResponse
    {
        public bool Success { get; set; }
        public string Mensaje { get; set; }

        public EquipoResponse()
        {
            Success = false;
            Mensaje = string.Empty;
        }
    }
}