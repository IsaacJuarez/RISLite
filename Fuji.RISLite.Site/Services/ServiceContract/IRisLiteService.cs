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
        List<clsVarAcicionales> getVariablesAdicionalCita(VarAdicionalRequest request);

        [OperationContract]
        VarAdicionalResponse setAgregarVariable(VarAdicionalRequest request);

        [OperationContract]
        VarAdicionalResponse setActualizarVariable(VarAdicionalRequest request);

        [OperationContract]
        VarAdicionalResponse setEstatusVariable(VarAdicionalRequest request);
        #endregion varAdicionales

        #region AdminUser
        [OperationContract]
        List<clsUsuario> getListaUsuarios(TecnicoRequest request);

        [OperationContract]
        AdminUserResponse setUsuario(AdminUserRequest request);

        [OperationContract]
        AdminUserResponse setActualizaUsuario(AdminUserRequest request);

        [OperationContract]
        AdminUserResponse setEstatusUsuario(AdminUserRequest request);
        #endregion AdminUser

        #region Prestacion
        [OperationContract]
        List<tbl_CAT_Modalidad> getListModalidades(CatalogoRequest request);

        [OperationContract]
        List<clsPrestacion> getListPrestacion(PrestacionRequest request);

        [OperationContract]
        PrestacionResponse setPrestacion(PrestacionRequest request);

        [OperationContract]
        PrestacionResponse setActualizaPrestacion(PrestacionRequest request);

        [OperationContract]
        PrestacionResponse setEstatusPrestacion(PrestacionRequest request);
        #endregion Prestacion;

        #region Equipo
        [OperationContract]
        List<tbl_CAT_Equipo> getListEquipo(EquipoRequest request);

        [OperationContract]
        EquipoResponse setEquipo(EquipoRequest request);

        [OperationContract]
        EquipoResponse setActualizaEquipo(EquipoRequest request);

        [OperationContract]
        EquipoResponse setEstatusEquipo(EquipoRequest request);
        #endregion Equipo

        #region Paciente
        List<tbl_CAT_Genero> getListaGenero(CatalogoRequest request);

        [OperationContract]
        DireccionResponse getDireccionPaciente(DireccionRequest request);

        [OperationContract]
        PacienteResponse setPaciente(PacienteRequest request);

        [OperationContract]
        PacienteResponse getPacienteDetalle(PacienteRequest request);

        [OperationContract]
        PacienteResponse getBusquedaPacientes(PacienteRequest request);

        [OperationContract]
        PacienteResponse getBusquedaEstudio(PacienteRequest request);

        [OperationContract]
        EstudioResponse getEstudioDetalle(EstudioRequest request);
        #endregion Paciente

        #region Indicacion
        [OperationContract]
        List<tbl_DET_IndicacionPrestacion> getListIndicacion(IndicacionRequest request);

        [OperationContract]
        IndicacionResponse setIndicacion(IndicacionRequest request);

        [OperationContract]
        IndicacionResponse setActualizaIndicacion(IndicacionRequest request);

        [OperationContract]
        IndicacionResponse setEstatusIndicacion(IndicacionRequest request);
        #endregion Indicacion

        #region Restriccion
        [OperationContract]
        List<tbl_DET_Restriccion> getListRestriccion(RestriccionRequest request);

        [OperationContract]
        RestriccionResponse setRestriccion(RestriccionRequest request);

        [OperationContract]
        RestriccionResponse setActualizaRestriccion(RestriccionRequest request);

        [OperationContract]
        RestriccionResponse setEstatusRestriccion(RestriccionRequest request);
        #endregion Restriccion

        #region Cuestionario
        [OperationContract]
        List<tbl_DET_Cuestionario> getListCuestionario(CuestionarioRequest request);

        [OperationContract]
        CuestionarioResponse setCuestionario(CuestionarioRequest request);

        [OperationContract]
        CuestionarioResponse setActualizaCuestionario(CuestionarioRequest request);

        [OperationContract]
        CuestionarioResponse setEstatusCuestionario(CuestionarioRequest request);
        #endregion Cuestionario

    }
}
