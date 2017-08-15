﻿using Fuji.RISLite.Entidades.DataBase;
using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fuji.RISLite.DataAccess
{


    public class RISLiteDataAccess
    {
        RISLiteEntities dbRisDA;

        public bool getUser(string user, ref clsUsuario Usuario)
        {
            bool Success = false;
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Usuario.Any(x => (bool)x.bitActivo && x.vchUsuario.Trim().ToUpper() == user.ToUpper().Trim()))
                    {
                        var query = (from _user in dbRisDA.tbl_CAT_Usuario
                                     join catTipo in dbRisDA.tbl_CAT_TipoUsuario on _user.intTipoUsuario equals catTipo.intTipoUsuario
                                     where _user.vchUsuario.Trim().ToUpper() == user.Trim().ToUpper() && (bool)_user.bitActivo
                                     select new
                                     {
                                         intUsuarioID = _user.intUsuarioID,
                                         intTipoUsuario = _user.intTipoUsuario,
                                         vchNombre = _user.vchNombre,
                                         vchUsuario = _user.vchUsuario,
                                         vchTipoUsuario = catTipo.vchTipoUsuario,
                                         bitActivo = _user.bitActivo,
                                         datFecha = _user.datFecha,
                                         vchUserAdmin = _user.vchUserAdmin
                                     }
                           ).First();
                        if (query != null)
                        {
                            if (Success = query.intUsuarioID > 0 ? true : false)
                            {
                                Usuario.intUsuarioID = query.intUsuarioID;
                                Usuario.intTipoUsuario = (int)query.intTipoUsuario;
                                Usuario.vchTipoUsuario = query.vchTipoUsuario;
                                Usuario.vchNombre = query.vchNombre;
                                Usuario.vchUsuario = query.vchUsuario;
                                Usuario.bitActivo = (bool)query.bitActivo;
                                Usuario.datFecha = (DateTime)query.datFecha;
                                Usuario.vchUserAdmin = query.vchUserAdmin;
                                Usuario.Token = Security.Encrypt(query.intUsuarioID + "-" + query.vchUsuario);
                            }
                        }
                    }
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getUser: " + egU.Message, 3, user);
            }
            return Success;
        }

        #region catalogos

        public List<tbl_CAT_Catalogo> getListCatalogos(string user)
        {
            List<tbl_CAT_Catalogo> lst = new List<tbl_CAT_Catalogo>();
            try
            {
                using(dbRisDA = new RISLiteEntities())
                {
                    if (dbRisDA.tbl_CAT_Catalogo.Any())
                    {
                        var query = dbRisDA.tbl_CAT_Catalogo.ToList();
                        if(query != null)
                        {
                            if(query.Count > 0)
                            {
                                lst = query;
                            }
                        }
                    }
                }
            }
           catch(Exception egLC)
            {
                Log.EscribeLog("Existe un error en getListCatalogos: " + egLC.Message, 3, user);
            }
            return lst;
        }

        public List<stp_getListCatalogo_Result> getListCatalogo(int intCatalogoId, string user)
        {
            List<stp_getListCatalogo_Result> list = new List<stp_getListCatalogo_Result>();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                   var query =   dbRisDA.stp_getListCatalogo(intCatalogoId).ToList();
                    if(query != null)
                    {
                        if (query.Count > 0)
                        {
                            list = query;
                        }
                    }
                }
            }
            catch(Exception egLC)
            {
                Log.EscribeLog("Existe un error en getListCatalogos: " + egLC.Message, 3, user);
            }
            return list;
        }

        public stp_updateCatEstatus_Result updateCatalogoEstatus(int intCatalogoID,bool biActivo,int intPrimaryKey, string user)
        {
            stp_updateCatEstatus_Result result = new stp_updateCatEstatus_Result();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_updateCatEstatus(intCatalogoID, biActivo, intPrimaryKey);
                    if (query != null)
                    {
                        result = query.First();
                    }
                }
            }
            catch(Exception eUCE)
            {
                result = null;
                Log.EscribeLog("Existe un error en getListCatalogos: " + eUCE.Message, 3, user);
            }
            return result;
        }

        public stp_updateCatalogo_Result updateCatalogo(int intCatalogoID, int intPrimaryKey, string vchValor, string user)
        {
            stp_updateCatalogo_Result response = new stp_updateCatalogo_Result();
            try
            {
                using(dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_updateCatalogo(intCatalogoID, intPrimaryKey, vchValor);
                    if(query != null)
                    {
                        response = query.First();
                    }
                }
            }
            catch(Exception eUC)
            {
                response = null;
                Log.EscribeLog("Existe un error en updateCatalogo: " + eUC.Message, 3, user);
            }
            return response;
        }

        public stp_setItemCatalogo_Result setItemCatalogo(int intCatalogoID, string vchValor, string user)
        {
            stp_setItemCatalogo_Result response = new stp_setItemCatalogo_Result();
            try
            {
                using (dbRisDA = new RISLiteEntities())
                {
                    var query = dbRisDA.stp_setItemCatalogo(intCatalogoID, vchValor, user);
                    if (query != null)
                    {
                        response = query.First();
                    }
                }
            }
            catch (Exception eUC)
            {
                response = null;
                Log.EscribeLog("Existe un error en setItemCatalogo: " + eUC.Message, 3, user);
            }
            return response;
        }
        #endregion catalogos

    }
}
