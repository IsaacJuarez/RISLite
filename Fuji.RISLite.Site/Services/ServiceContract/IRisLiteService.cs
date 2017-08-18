using Fuji.RISLite.Entidades.DataBase;
using Fuji.RISLite.Entities;
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
        [OperationContract]
        List<tbl_CAT_Catalogo> getListCatalogos(CatalogoRequest request);

        [OperationContract]
        List<stp_getListCatalogo_Result> getListCatalogo(CatalogoRequest request);

        [OperationContract]
        stp_updateCatEstatus_Result updateCatalogoEstatus(CatalogoRequest request);

        [OperationContract]
        stp_updateCatalogo_Result updateCatalogo(CatalogoRequest request);

        [OperationContract]
        stp_setItemCatalogo_Result setItemCatalogo(CatalogoRequest request);


        #endregion catalogo

        #region equipo
        List<clsEquipo> getListaEquipos(EquipoRequest request);
        #endregion equipo

        #region Tecnico
        List<clsUsuario> getListTecnico(TecnicoRequest request);
        #endregion Tecnico
    }
}
