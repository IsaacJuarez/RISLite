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

        [OperationContract]
        ValidaUserResponse getLoginUser(ValidaUserRequest request);

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


        #region Sitio
        [OperationContract]
        List<tbl_CAT_Sitio> getListSitios(SitioRequest request);

        [OperationContract]
        SitioResponse setSitio(SitioRequest request);

        [OperationContract]
        SitioResponse setActualizaSitio(SitioRequest request);

        [OperationContract]
        SitioResponse setEstatusSitio(SitioRequest request);
        #endregion Sitio

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

        [OperationContract]
        List<tbl_CAT_Modalidad> getModalidadTecnico(ModTecnicoRequest request);

        [OperationContract]
        List<stp_getRELModalidadTecnico_Result> getModalidadTecnicoList(ModTecnicoRequest request);

        [OperationContract]
        ModTecnicoResponse setModalidadTecnico(ModTecnicoRequest request);

        [OperationContract]
        ModTecnicoResponse setEstatusModalidadTecnico(ModTecnicoRequest request);
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
        PacienteResponse getPacienteAdicionales(PacienteRequest request);

        [OperationContract]
        CitaNuevaResponse getCitaAdicionales(CitaNuevaRequest request);

        [OperationContract]
        PacienteResponse getBusquedaPacientes(PacienteRequest request);

        [OperationContract]
        PacienteResponse getBusquedaPacientesMod(PacienteRequest request);

        [OperationContract]
        PacienteResponse getBusquedaPacientesList(PacienteRequest request);

        [OperationContract]
        PacienteResponse getBusquedaEstudio(PacienteRequest request);

        [OperationContract]
        EstudioResponse getEstudioDetalle(EstudioRequest request);

        [OperationContract]
        AsignacionModalidadNuevaCita_Response getEstudioDetalle_citaNueva(AsignacionModalidadNuevaCita_Request request, int id_tabla_modalidad);

        [OperationContract]
        PacienteResponse setActualizaPaciente(PacienteRequest request);
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

        #region Estudios
        [OperationContract]
        List<clsEstudioCita> getEstudiosPaciente(EstudioRequest request);
        #endregion Estudios


        #region Adicionales
        [OperationContract]
        List<clsAdicionales> getAdicionales(AdicionalesRequest request);

        [OperationContract]
        AdicionalesResponse setAdicionales(AdicionalesRequest request);

        [OperationContract]
        AdicionalesResponse setActualizarAdicionales(AdicionalesRequest request);

        [OperationContract]
        List<tbl_CAT_TipoBoton> getCATTipoBoton(AdicionalesRequest request);

        [OperationContract]
        List<tbl_CAT_TipoAdicional> getCATTipoAdicional(AdicionalesRequest request);

        [OperationContract]
        AdicionalesResponse setEstatusAdicional(AdicionalesRequest request);

        [OperationContract]
        List<clsAdicionales> getAdicionalesREL(AdicionalesRequest request);

        [OperationContract]
        AdicionalesResponse setAdicionalesREL(AdicionalesRequest request);
        #endregion Adicionales

        #region SugerenciasCita
        [OperationContract]
        List<stp_getCitaDisponible_Result> getSugerenciasCita(SugerenciasRequest request);
        #endregion SugerenciasCita

        [OperationContract]
        List<clsEquipo> getCitaEquipo_Sitio(CitaNumEquipos request);

        [OperationContract]
        string getDescripcionModalidad_sitio(AgendaRequest request);

        [OperationContract]
        int getListDuracionGen_Sitio(CitaModalidad request);

        [OperationContract]
        string getListColorModalidad_Sitio(AgendaRequest request);

        [OperationContract]
        List<clsEventoCita> getListCitas_en_agenda_Sitio(CitasRequest request);

        [OperationContract]
        List<clsConfScheduler> getConfScheduler_Sitio(ConfigSchedulerRequest request);

        [OperationContract]
        List<clsHoraMuerta> getHoraMuertaConfScheduler_Sitio(ConfigScheduler_HoraMuertaRequest request);

        #region InsertCita
        CitaNuevaResponse setCitaNueva(CitaNuevaRequest request);
        #endregion

        #region CitaReporte
        [OperationContract]
        List<stp_getCitaReporte_Result> getCitaReporte(CitaReporteRequest request);

        [OperationContract]
        List<clsRepIndicacion> getIndicaciones(CitaReporteRequest request);

        [OperationContract]
        List<clsRepRestriccion> getRestricciones(CitaReporteRequest request);

        [OperationContract]
        List<stp_getCitas_Result> getCitas(CitaReporteRequest request);

        [OperationContract]
        CitaReporteResponse setEstatusEstudio(CitaReporteRequest request);

        [OperationContract]
        void updateEstatusCitaAutomatica(string user);
        #endregion CitaReporte

        #region Perfil
        [OperationContract]
        PerfilResponse setPerfil(PerfilRequest request);
        #endregion Perfil

    }
}
