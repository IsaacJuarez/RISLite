using Fuji.RISLite.Entidades.DataBase;
using Fuji.RISLite.Site.Services.DataContract;
using System.Collections.Generic;
using System.ServiceModel;

namespace Fuji.RISLite.Site.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de interfaz "IRisLiteService" en el código y en el archivo de configuración a la vez.
    [ServiceContract]
    public interface IRisLiteService
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        ValidaUserResponse getUser(ValidaUserRequest request);

        #region catalogo
        List<tbl_CAT_Catalogo> getListCatalogos(CatalogoRequest request);
        #endregion catalogo
    }
}
