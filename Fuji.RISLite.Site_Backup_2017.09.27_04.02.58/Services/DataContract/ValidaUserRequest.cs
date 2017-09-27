using Fuji.RISLite.Entities;

namespace Fuji.RISLite.Site.Services.DataContract
{
    public class ValidaUserRequest
    {
        public string  user { get; set; }
        public clsUsuario mdlUser;

        public ValidaUserRequest()
        {
            user = string.Empty;
            mdlUser = new clsUsuario();
        }
    }
}