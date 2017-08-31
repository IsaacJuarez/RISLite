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

        [OperationContract]
        List<clsCatalogo> getTipoUsuario(CatalogoRequest request);

        [OperationContract]
        List<clsCatalogo> getListaBoton(CatalogoRequest request);

        [OperationContract]
        List<clsCatalogo> getListaVista(CatalogoRequest request);

        [OperationContract]
        List<stp_getListaPaginas_Result> getListVistas(CatalogoRequest request);

        [OperationContract]
        List<clsUsuario> getListaUsuarios(TecnicoRequest request);

        #endregion catalogo

        #region equipo
        List<clsEquipo> getListaEquipos(EquipoRequest request);
        #endregion equipo

        #region Tecnico
        List<clsUsuario> getListTecnico(TecnicoRequest request);
        #endregion Tecnico

        #region ConfigEmail
        [OperationContract]
        ConfigEmailResponse getConfigEmail(ConfigEmailRequest request);

        [OperationContract]
        ConfigEmailResponse setConfigEmail(ConfigEmailRequest request);

        [OperationContract]
        ConfigEmailResponse setActualizarConfigEmail(ConfigEmailRequest request);
        #endregion ConfigEmail

        #region ConfigSitio
        [OperationContract]
        ConfigSitioResponse getConfigSitio(ConfigSitioRequest request);
        [OperationContract]
        ConfigSitioResponse setConfigSitio(ConfigSitioRequest request);
        [OperationContract]
        ConfigSitioResponse setActualizarConfigSitio(ConfigSitioRequest request);

        #endregion ConfigSitio

        #region varAdicionales
        [OperationContract]
        List<clsVarAcicionales> getVariablesAdicionalPaciente(VarAdicionalRequest request);

        [OperationContract]
        VarAdicionalResponse setAgregarVariable(VarAdicionalRequest request);

        [OperationContract]
        VarAdicionalResponse setActualizarVariable(VarAdicionalRequest request);

        [OperationContract]
        VarAdicionalResponse setEstatusVariable(VarAdicionalRequest request);
        #endregion varAdicionales

    }
}
