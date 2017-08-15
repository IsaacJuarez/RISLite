using Fuji.RISLite.DataAccess;
using Fuji.RISLite.Entidades.DataBase;
using Fuji.RISLite.Entidades.Extensions;
using Fuji.RISLite.Entities;
using Fuji.RISLite.Site.Services.DataContract;
using System;
using System.Collections.Generic;

namespace Fuji.RISLite.Site.Services
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "RisLiteService" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione RisLiteService.svc o RisLiteService.svc.cs en el Explorador de soluciones e inicie la depuración.
    public class RisLiteService : IRisLiteService
    {
        public void DoWork()
        {
        }

        public ValidaUserResponse getUser(ValidaUserRequest request)
        {
            ValidaUserResponse response = new ValidaUserResponse();
            try
            {
                clsUsuario mdlUser = new clsUsuario();
                RISLiteDataAccess controller = new RISLiteDataAccess();
                response.Success = controller.getUser(request.user, ref mdlUser);
                response.mdlUser = mdlUser;
            }
            catch(Exception egU)
            {
                Log.EscribeLog("Existe un error en getUser: " + egU.Message, 3, "");
            }
            return response;
        }

        #region catalogo
        public List<tbl_CAT_Catalogo> getListCatalogos(CatalogoRequest request)
        {
            List<tbl_CAT_Catalogo> response = new List<tbl_CAT_Catalogo>();
            try
            {
                if(Security.ValidateToken(request.mdlUser.Token,request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListCatalogos(request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getUser: " + egU.Message, 3, "");
            }
            return response;
        }

        public List<stp_getListCatalogo_Result> getListCatalogo(CatalogoRequest request)
        {
            List<stp_getListCatalogo_Result> response = new List<stp_getListCatalogo_Result>();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.getListCatalogo(request.mdlCat.intCatalogoID, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getUser: " + egU.Message, 3, "");
            }
            return response;
        }

        public stp_updateCatEstatus_Result updateCatalogoEstatus(CatalogoRequest request)
        {
            stp_updateCatEstatus_Result response = new stp_updateCatEstatus_Result();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.updateCatalogoEstatus(request.mdlCat.intCatalogoID, request.mdlCat.bitActivo,request.mdlCat.intPrimaryKey, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getUser: " + egU.Message, 3, "");
            }
            return response;
        }

        public stp_updateCatalogo_Result updateCatalogo(CatalogoRequest request)
        {
            stp_updateCatalogo_Result response = new stp_updateCatalogo_Result();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.updateCatalogo(request.mdlCat.intCatalogoID, request.mdlCat.intPrimaryKey, request.mdlCat.vchValor, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getUser: " + egU.Message, 3, "");
            }
            return response;
        }

        public stp_setItemCatalogo_Result setItemCatalogo(CatalogoRequest request)
        {
            stp_setItemCatalogo_Result response = new stp_setItemCatalogo_Result();
            try
            {
                if (Security.ValidateToken(request.mdlUser.Token, request.mdlUser.intUsuarioID.ToString(), request.mdlUser.vchUsuario))
                {
                    RISLiteDataAccess controller = new RISLiteDataAccess();
                    response = controller.setItemCatalogo(request.mdlCat.intCatalogoID, request.mdlCat.vchValor, request.mdlUser.vchUsuario);
                }
            }
            catch (Exception egU)
            {
                Log.EscribeLog("Existe un error en getUser: " + egU.Message, 3, "");
            }
            return response;
        }
        #endregion catalogo
    }
}
