using Fuji.RISLite.AccesoDatos;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class ValidaUserResponse
    {
        public bool Success { get; set; }
        public clsUsuario mdlUser;
        

        public ValidaUserResponse()
        {
            Success = false;
            mdlUser = new clsUsuario();
        }
    }
}