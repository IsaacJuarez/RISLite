﻿//------------------------------------------------------------------------------
// <auto-generated>
//    Este código se generó a partir de una plantilla.
//
//    Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//    Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Fuji.RISLite.Entidades.DataBase
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class RISLiteEntities : DbContext
    {
        public RISLiteEntities()
            : base("name=RISLiteEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<tbl_CAT_TipoMensaje> tbl_CAT_TipoMensaje { get; set; }
        public DbSet<tbl_MST_Bitacora> tbl_MST_Bitacora { get; set; }
        public DbSet<tbl_CAT_TipoUsuario> tbl_CAT_TipoUsuario { get; set; }
        public DbSet<tbl_CAT_Catalogo> tbl_CAT_Catalogo { get; set; }
        public DbSet<tbl_CAT_Modalidad> tbl_CAT_Modalidad { get; set; }
        public DbSet<tbl_CAT_EstatusCita> tbl_CAT_EstatusCita { get; set; }
        public DbSet<tbl_CAT_EstatusEstudio> tbl_CAT_EstatusEstudio { get; set; }
        public DbSet<tbl_CAT_Genero> tbl_CAT_Genero { get; set; }
        public DbSet<tbl_CAT_InstitucionProcedencia> tbl_CAT_InstitucionProcedencia { get; set; }
        public DbSet<tbl_CAT_TipoMovimiento> tbl_CAT_TipoMovimiento { get; set; }
        public DbSet<tbl_CAT_Botones> tbl_CAT_Botones { get; set; }
        public DbSet<tbl_CAT_Vistas> tbl_CAT_Vistas { get; set; }
        public DbSet<tbl_REL_BotonVista> tbl_REL_BotonVista { get; set; }
        public DbSet<tbl_REL_TipoUsuarioBoton> tbl_REL_TipoUsuarioBoton { get; set; }
        public DbSet<tbl_MST_Paciente> tbl_MST_Paciente { get; set; }
        public DbSet<tbl_DET_PacienteDinamico> tbl_DET_PacienteDinamico { get; set; }
        public DbSet<tbl_CAT_Prestacion> tbl_CAT_Prestacion { get; set; }
        public DbSet<tbl_REL_ModalidadPrestacion> tbl_REL_ModalidadPrestacion { get; set; }
        public DbSet<tbl_CAT_CodigoPostal> tbl_CAT_CodigoPostal { get; set; }
        public DbSet<tbl_CAT_Estado> tbl_CAT_Estado { get; set; }
        public DbSet<tbl_CAT_Municipio> tbl_CAT_Municipio { get; set; }
        public DbSet<tbl_DET_DireccionPaciente> tbl_DET_DireccionPaciente { get; set; }
        public DbSet<tbl_DET_Paciente> tbl_DET_Paciente { get; set; }
        public DbSet<tbl_REL_IdentificacionPaciente> tbl_REL_IdentificacionPaciente { get; set; }
        public DbSet<tbl_REL_PacienteCita> tbl_REL_PacienteCita { get; set; }
        public DbSet<tbl_CAT_MedicoTratante> tbl_CAT_MedicoTratante { get; set; }
        public DbSet<tbl_DET_Cuestionario> tbl_DET_Cuestionario { get; set; }
        public DbSet<tbl_DET_Restriccion> tbl_DET_Restriccion { get; set; }
        public DbSet<tbl_DET_IndicacionPrestacion> tbl_DET_IndicacionPrestacion { get; set; }
        public DbSet<tbl_CAT_TipoAdicional> tbl_CAT_TipoAdicional { get; set; }
        public DbSet<tbl_CAT_TipoBoton> tbl_CAT_TipoBoton { get; set; }
        public DbSet<tbl_MST_Estudio> tbl_MST_Estudio { get; set; }
        public DbSet<tbl_REL_CitaEstudio> tbl_REL_CitaEstudio { get; set; }
        public DbSet<tbl_CAT_DiaFeriado> tbl_CAT_DiaFeriado { get; set; }
        public DbSet<tbl_CAT_DiaSemana> tbl_CAT_DiaSemana { get; set; }
        public DbSet<tbl_CAT_Eventos> tbl_CAT_Eventos { get; set; }
        public DbSet<tbl_CAT_Sitio> tbl_CAT_Sitio { get; set; }
        public DbSet<tbl_CONFIG_VariablesAdiPaciente> tbl_CONFIG_VariablesAdiPaciente { get; set; }
        public DbSet<tbl_CAT_Identificacion> tbl_CAT_Identificacion { get; set; }
        public DbSet<tbl_CAT_Usuario> tbl_CAT_Usuario { get; set; }
        public DbSet<tbl_MST_ConfiguracionSistema> tbl_MST_ConfiguracionSistema { get; set; }
        public DbSet<tbl_Conf_CorreoSitio> tbl_Conf_CorreoSitio { get; set; }
        public DbSet<tbl_CONFIG_VariablesAdiCita> tbl_CONFIG_VariablesAdiCita { get; set; }
        public DbSet<tbl_CAT_Equipo> tbl_CAT_Equipo { get; set; }
        public DbSet<tbl_MST_Adicionales> tbl_MST_Adicionales { get; set; }
        public DbSet<tbl_REL_SitioPaciente> tbl_REL_SitioPaciente { get; set; }
        public DbSet<tbl_CONFIG_Agenda> tbl_CONFIG_Agenda { get; set; }
        public DbSet<tbl_CAT_HoraMuerta> tbl_CAT_HoraMuerta { get; set; }
        public DbSet<tbl_CAT_DuracionModalidad> tbl_CAT_DuracionModalidad { get; set; }
        public DbSet<tbl_REL_DiaSemana> tbl_REL_DiaSemana { get; set; }
        public DbSet<tbl_DET_CitaDinamico> tbl_DET_CitaDinamico { get; set; }
        public DbSet<tbl_REL_ModalidadesTecnico> tbl_REL_ModalidadesTecnico { get; set; }
        public DbSet<tbl_REL_EstudioTecnico> tbl_REL_EstudioTecnico { get; set; }
        public DbSet<tbl_REL_AdicionalEspecificaciones> tbl_REL_AdicionalEspecificaciones { get; set; }
        public DbSet<tbl_CAT_AdicionalEspecifico> tbl_CAT_AdicionalEspecifico { get; set; }
        public DbSet<tbl_MST_Cita> tbl_MST_Cita { get; set; }
        public DbSet<tbl_DET_Cita> tbl_DET_Cita { get; set; }
    
        public virtual ObjectResult<stp_updateCatEstatus_Result> stp_updateCatEstatus(Nullable<int> intCatalogoID, Nullable<bool> bitActivo, Nullable<int> intPrimaryKey)
        {
            var intCatalogoIDParameter = intCatalogoID.HasValue ?
                new ObjectParameter("intCatalogoID", intCatalogoID) :
                new ObjectParameter("intCatalogoID", typeof(int));
    
            var bitActivoParameter = bitActivo.HasValue ?
                new ObjectParameter("bitActivo", bitActivo) :
                new ObjectParameter("bitActivo", typeof(bool));
    
            var intPrimaryKeyParameter = intPrimaryKey.HasValue ?
                new ObjectParameter("intPrimaryKey", intPrimaryKey) :
                new ObjectParameter("intPrimaryKey", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<stp_updateCatEstatus_Result>("stp_updateCatEstatus", intCatalogoIDParameter, bitActivoParameter, intPrimaryKeyParameter);
        }
    
        public virtual ObjectResult<stp_updateCatalogo_Result> stp_updateCatalogo(Nullable<int> intCatalogoID, Nullable<int> intPrimaryKey, string vchValor)
        {
            var intCatalogoIDParameter = intCatalogoID.HasValue ?
                new ObjectParameter("intCatalogoID", intCatalogoID) :
                new ObjectParameter("intCatalogoID", typeof(int));
    
            var intPrimaryKeyParameter = intPrimaryKey.HasValue ?
                new ObjectParameter("intPrimaryKey", intPrimaryKey) :
                new ObjectParameter("intPrimaryKey", typeof(int));
    
            var vchValorParameter = vchValor != null ?
                new ObjectParameter("vchValor", vchValor) :
                new ObjectParameter("vchValor", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<stp_updateCatalogo_Result>("stp_updateCatalogo", intCatalogoIDParameter, intPrimaryKeyParameter, vchValorParameter);
        }
    
        public virtual ObjectResult<stp_getListaPaginas_Result> stp_getListaPaginas(Nullable<int> intTipoUsuarioID)
        {
            var intTipoUsuarioIDParameter = intTipoUsuarioID.HasValue ?
                new ObjectParameter("intTipoUsuarioID", intTipoUsuarioID) :
                new ObjectParameter("intTipoUsuarioID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<stp_getListaPaginas_Result>("stp_getListaPaginas", intTipoUsuarioIDParameter);
        }
    
        public virtual ObjectResult<stp_getBusquedaEstudio_Result> stp_getBusquedaEstudio(string estudio)
        {
            var estudioParameter = estudio != null ?
                new ObjectParameter("estudio", estudio) :
                new ObjectParameter("estudio", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<stp_getBusquedaEstudio_Result>("stp_getBusquedaEstudio", estudioParameter);
        }
    
        public virtual ObjectResult<stp_getListCatalogo_Result> stp_getListCatalogo(Nullable<int> intCatalogoID, Nullable<int> intSitioID)
        {
            var intCatalogoIDParameter = intCatalogoID.HasValue ?
                new ObjectParameter("intCatalogoID", intCatalogoID) :
                new ObjectParameter("intCatalogoID", typeof(int));
    
            var intSitioIDParameter = intSitioID.HasValue ?
                new ObjectParameter("intSitioID", intSitioID) :
                new ObjectParameter("intSitioID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<stp_getListCatalogo_Result>("stp_getListCatalogo", intCatalogoIDParameter, intSitioIDParameter);
        }
    
        public virtual ObjectResult<stp_setItemCatalogo_Result> stp_setItemCatalogo(Nullable<int> intCatalogoID, Nullable<int> intSitioID, string vchValor, string vchUserAdmin)
        {
            var intCatalogoIDParameter = intCatalogoID.HasValue ?
                new ObjectParameter("intCatalogoID", intCatalogoID) :
                new ObjectParameter("intCatalogoID", typeof(int));
    
            var intSitioIDParameter = intSitioID.HasValue ?
                new ObjectParameter("intSitioID", intSitioID) :
                new ObjectParameter("intSitioID", typeof(int));
    
            var vchValorParameter = vchValor != null ?
                new ObjectParameter("vchValor", vchValor) :
                new ObjectParameter("vchValor", typeof(string));
    
            var vchUserAdminParameter = vchUserAdmin != null ?
                new ObjectParameter("vchUserAdmin", vchUserAdmin) :
                new ObjectParameter("vchUserAdmin", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<stp_setItemCatalogo_Result>("stp_setItemCatalogo", intCatalogoIDParameter, intSitioIDParameter, vchValorParameter, vchUserAdminParameter);
        }
    
        public virtual ObjectResult<stp_getPrestacionModalidad_Result> stp_getPrestacionModalidad(Nullable<int> intModalidadId, Nullable<int> intSitioID)
        {
            var intModalidadIdParameter = intModalidadId.HasValue ?
                new ObjectParameter("intModalidadId", intModalidadId) :
                new ObjectParameter("intModalidadId", typeof(int));
    
            var intSitioIDParameter = intSitioID.HasValue ?
                new ObjectParameter("intSitioID", intSitioID) :
                new ObjectParameter("intSitioID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<stp_getPrestacionModalidad_Result>("stp_getPrestacionModalidad", intModalidadIdParameter, intSitioIDParameter);
        }
    
        public virtual ObjectResult<stp_getBusquedaCita_Sitio_Result> stp_getBusquedaCita_Sitio(Nullable<int> intIdModalidad, Nullable<int> intIdSitio)
        {
            var intIdModalidadParameter = intIdModalidad.HasValue ?
                new ObjectParameter("intIdModalidad", intIdModalidad) :
                new ObjectParameter("intIdModalidad", typeof(int));
    
            var intIdSitioParameter = intIdSitio.HasValue ?
                new ObjectParameter("intIdSitio", intIdSitio) :
                new ObjectParameter("intIdSitio", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<stp_getBusquedaCita_Sitio_Result>("stp_getBusquedaCita_Sitio", intIdModalidadParameter, intIdSitioParameter);
        }
    
        public virtual ObjectResult<stp_getCitaDisponible_Result> stp_getCitaDisponible(Nullable<System.DateTime> datFechaInicio, Nullable<System.DateTime> datFechaFinal, Nullable<int> intModalidad, string vchDias, string vchHoras, Nullable<int> intSitioID)
        {
            var datFechaInicioParameter = datFechaInicio.HasValue ?
                new ObjectParameter("datFechaInicio", datFechaInicio) :
                new ObjectParameter("datFechaInicio", typeof(System.DateTime));
    
            var datFechaFinalParameter = datFechaFinal.HasValue ?
                new ObjectParameter("datFechaFinal", datFechaFinal) :
                new ObjectParameter("datFechaFinal", typeof(System.DateTime));
    
            var intModalidadParameter = intModalidad.HasValue ?
                new ObjectParameter("intModalidad", intModalidad) :
                new ObjectParameter("intModalidad", typeof(int));
    
            var vchDiasParameter = vchDias != null ?
                new ObjectParameter("vchDias", vchDias) :
                new ObjectParameter("vchDias", typeof(string));
    
            var vchHorasParameter = vchHoras != null ?
                new ObjectParameter("vchHoras", vchHoras) :
                new ObjectParameter("vchHoras", typeof(string));
    
            var intSitioIDParameter = intSitioID.HasValue ?
                new ObjectParameter("intSitioID", intSitioID) :
                new ObjectParameter("intSitioID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<stp_getCitaDisponible_Result>("stp_getCitaDisponible", datFechaInicioParameter, datFechaFinalParameter, intModalidadParameter, vchDiasParameter, vchHorasParameter, intSitioIDParameter);
        }
    
        public virtual ObjectResult<stp_getBusquedaPaciente_Result> stp_getBusquedaPaciente(string vchCadena, Nullable<int> intSitioID)
        {
            var vchCadenaParameter = vchCadena != null ?
                new ObjectParameter("vchCadena", vchCadena) :
                new ObjectParameter("vchCadena", typeof(string));
    
            var intSitioIDParameter = intSitioID.HasValue ?
                new ObjectParameter("intSitioID", intSitioID) :
                new ObjectParameter("intSitioID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<stp_getBusquedaPaciente_Result>("stp_getBusquedaPaciente", vchCadenaParameter, intSitioIDParameter);
        }
    
        public virtual ObjectResult<stp_getEstudiosPaciente_Result> stp_getEstudiosPaciente(Nullable<int> intPacienteID)
        {
            var intPacienteIDParameter = intPacienteID.HasValue ?
                new ObjectParameter("intPacienteID", intPacienteID) :
                new ObjectParameter("intPacienteID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<stp_getEstudiosPaciente_Result>("stp_getEstudiosPaciente", intPacienteIDParameter);
        }
    
        public virtual ObjectResult<stp_getBusquedaCita_SoloSitio_Result> stp_getBusquedaCita_SoloSitio(Nullable<int> intIdSitio)
        {
            var intIdSitioParameter = intIdSitio.HasValue ?
                new ObjectParameter("intIdSitio", intIdSitio) :
                new ObjectParameter("intIdSitio", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<stp_getBusquedaCita_SoloSitio_Result>("stp_getBusquedaCita_SoloSitio", intIdSitioParameter);
        }
    
        public virtual ObjectResult<stp_getDetalleCita_Result> stp_getDetalleCita(Nullable<int> intCitaID)
        {
            var intCitaIDParameter = intCitaID.HasValue ?
                new ObjectParameter("intCitaID", intCitaID) :
                new ObjectParameter("intCitaID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<stp_getDetalleCita_Result>("stp_getDetalleCita", intCitaIDParameter);
        }
    
        public virtual ObjectResult<stp_getRELModalidadTecnico_Result> stp_getRELModalidadTecnico(Nullable<int> intUsuarioID)
        {
            var intUsuarioIDParameter = intUsuarioID.HasValue ?
                new ObjectParameter("intUsuarioID", intUsuarioID) :
                new ObjectParameter("intUsuarioID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<stp_getRELModalidadTecnico_Result>("stp_getRELModalidadTecnico", intUsuarioIDParameter);
        }
    
        public virtual ObjectResult<stp_getListaTrabajo_Sitio_Result> stp_getListaTrabajo_Sitio(Nullable<int> intIdSitio)
        {
            var intIdSitioParameter = intIdSitio.HasValue ?
                new ObjectParameter("intIdSitio", intIdSitio) :
                new ObjectParameter("intIdSitio", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<stp_getListaTrabajo_Sitio_Result>("stp_getListaTrabajo_Sitio", intIdSitioParameter);
        }
    
        public virtual ObjectResult<stp_getGraficaModalidad_Result> stp_getGraficaModalidad(Nullable<int> intIdSitio, Nullable<int> intModalidadID, Nullable<System.DateTime> datFechaInicio, Nullable<System.DateTime> datFechaFin, string intEstatusSitio)
        {
            var intIdSitioParameter = intIdSitio.HasValue ?
                new ObjectParameter("intIdSitio", intIdSitio) :
                new ObjectParameter("intIdSitio", typeof(int));
    
            var intModalidadIDParameter = intModalidadID.HasValue ?
                new ObjectParameter("intModalidadID", intModalidadID) :
                new ObjectParameter("intModalidadID", typeof(int));
    
            var datFechaInicioParameter = datFechaInicio.HasValue ?
                new ObjectParameter("datFechaInicio", datFechaInicio) :
                new ObjectParameter("datFechaInicio", typeof(System.DateTime));
    
            var datFechaFinParameter = datFechaFin.HasValue ?
                new ObjectParameter("datFechaFin", datFechaFin) :
                new ObjectParameter("datFechaFin", typeof(System.DateTime));
    
            var intEstatusSitioParameter = intEstatusSitio != null ?
                new ObjectParameter("intEstatusSitio", intEstatusSitio) :
                new ObjectParameter("intEstatusSitio", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<stp_getGraficaModalidad_Result>("stp_getGraficaModalidad", intIdSitioParameter, intModalidadIDParameter, datFechaInicioParameter, datFechaFinParameter, intEstatusSitioParameter);
        }
    
        public virtual ObjectResult<stp_getGraficaUsuario_Result> stp_getGraficaUsuario(Nullable<int> intIdSitio, Nullable<int> intUsuarioID, Nullable<System.DateTime> datFechaInicio, Nullable<System.DateTime> datFechaFin, string intEstatusSitio)
        {
            var intIdSitioParameter = intIdSitio.HasValue ?
                new ObjectParameter("intIdSitio", intIdSitio) :
                new ObjectParameter("intIdSitio", typeof(int));
    
            var intUsuarioIDParameter = intUsuarioID.HasValue ?
                new ObjectParameter("intUsuarioID", intUsuarioID) :
                new ObjectParameter("intUsuarioID", typeof(int));
    
            var datFechaInicioParameter = datFechaInicio.HasValue ?
                new ObjectParameter("datFechaInicio", datFechaInicio) :
                new ObjectParameter("datFechaInicio", typeof(System.DateTime));
    
            var datFechaFinParameter = datFechaFin.HasValue ?
                new ObjectParameter("datFechaFin", datFechaFin) :
                new ObjectParameter("datFechaFin", typeof(System.DateTime));
    
            var intEstatusSitioParameter = intEstatusSitio != null ?
                new ObjectParameter("intEstatusSitio", intEstatusSitio) :
                new ObjectParameter("intEstatusSitio", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<stp_getGraficaUsuario_Result>("stp_getGraficaUsuario", intIdSitioParameter, intUsuarioIDParameter, datFechaInicioParameter, datFechaFinParameter, intEstatusSitioParameter);
        }
    
        public virtual ObjectResult<stp_getBusquedaPacienteList_Result> stp_getBusquedaPacienteList(string vchCadena, Nullable<int> intSitioID)
        {
            var vchCadenaParameter = vchCadena != null ?
                new ObjectParameter("vchCadena", vchCadena) :
                new ObjectParameter("vchCadena", typeof(string));
    
            var intSitioIDParameter = intSitioID.HasValue ?
                new ObjectParameter("intSitioID", intSitioID) :
                new ObjectParameter("intSitioID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<stp_getBusquedaPacienteList_Result>("stp_getBusquedaPacienteList", vchCadenaParameter, intSitioIDParameter);
        }
    
        public virtual ObjectResult<string> stp_updateCita()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<string>("stp_updateCita");
        }
    
        public virtual ObjectResult<stp_getBusquedaPacienteMod_Result> stp_getBusquedaPacienteMod(string vchCadena, Nullable<int> intSitioID)
        {
            var vchCadenaParameter = vchCadena != null ?
                new ObjectParameter("vchCadena", vchCadena) :
                new ObjectParameter("vchCadena", typeof(string));
    
            var intSitioIDParameter = intSitioID.HasValue ?
                new ObjectParameter("intSitioID", intSitioID) :
                new ObjectParameter("intSitioID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<stp_getBusquedaPacienteMod_Result>("stp_getBusquedaPacienteMod", vchCadenaParameter, intSitioIDParameter);
        }
    
        public virtual ObjectResult<stp_getCitas_Result> stp_getCitas(string vchNombre, Nullable<int> intModalidadID, Nullable<System.DateTime> datFechaInicio, Nullable<System.DateTime> datFechaFin, Nullable<int> intSitioID)
        {
            var vchNombreParameter = vchNombre != null ?
                new ObjectParameter("vchNombre", vchNombre) :
                new ObjectParameter("vchNombre", typeof(string));
    
            var intModalidadIDParameter = intModalidadID.HasValue ?
                new ObjectParameter("intModalidadID", intModalidadID) :
                new ObjectParameter("intModalidadID", typeof(int));
    
            var datFechaInicioParameter = datFechaInicio.HasValue ?
                new ObjectParameter("datFechaInicio", datFechaInicio) :
                new ObjectParameter("datFechaInicio", typeof(System.DateTime));
    
            var datFechaFinParameter = datFechaFin.HasValue ?
                new ObjectParameter("datFechaFin", datFechaFin) :
                new ObjectParameter("datFechaFin", typeof(System.DateTime));
    
            var intSitioIDParameter = intSitioID.HasValue ?
                new ObjectParameter("intSitioID", intSitioID) :
                new ObjectParameter("intSitioID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<stp_getCitas_Result>("stp_getCitas", vchNombreParameter, intModalidadIDParameter, datFechaInicioParameter, datFechaFinParameter, intSitioIDParameter);
        }
    
        public virtual ObjectResult<stp_getDetalleCitaPaciente_Result> stp_getDetalleCitaPaciente(Nullable<int> intCitaID)
        {
            var intCitaIDParameter = intCitaID.HasValue ?
                new ObjectParameter("intCitaID", intCitaID) :
                new ObjectParameter("intCitaID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<stp_getDetalleCitaPaciente_Result>("stp_getDetalleCitaPaciente", intCitaIDParameter);
        }
    
        public virtual ObjectResult<stp_getAdicionalesPac_Result> stp_getAdicionalesPac(Nullable<int> intSitioID, Nullable<int> mASCULINO, Nullable<int> fEMENINO, Nullable<int> mAYOR, Nullable<int> mENOR)
        {
            var intSitioIDParameter = intSitioID.HasValue ?
                new ObjectParameter("intSitioID", intSitioID) :
                new ObjectParameter("intSitioID", typeof(int));
    
            var mASCULINOParameter = mASCULINO.HasValue ?
                new ObjectParameter("MASCULINO", mASCULINO) :
                new ObjectParameter("MASCULINO", typeof(int));
    
            var fEMENINOParameter = fEMENINO.HasValue ?
                new ObjectParameter("FEMENINO", fEMENINO) :
                new ObjectParameter("FEMENINO", typeof(int));
    
            var mAYORParameter = mAYOR.HasValue ?
                new ObjectParameter("MAYOR", mAYOR) :
                new ObjectParameter("MAYOR", typeof(int));
    
            var mENORParameter = mENOR.HasValue ?
                new ObjectParameter("MENOR", mENOR) :
                new ObjectParameter("MENOR", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<stp_getAdicionalesPac_Result>("stp_getAdicionalesPac", intSitioIDParameter, mASCULINOParameter, fEMENINOParameter, mAYORParameter, mENORParameter);
        }
    
        public virtual ObjectResult<stp_getCitaReporte_Result> stp_getCitaReporte(Nullable<int> intCitaID)
        {
            var intCitaIDParameter = intCitaID.HasValue ?
                new ObjectParameter("intCitaID", intCitaID) :
                new ObjectParameter("intCitaID", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<stp_getCitaReporte_Result>("stp_getCitaReporte", intCitaIDParameter);
        }
    }
}
