using SearchDocuments.Entidades;
using SearchDocuments.Negocio.Parametro;
using SearchDocumentsSiteWeb.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SearchDocumentsSiteWeb.Controllers.Parametro
{
    public class ParametroController : Controller
    {
        #region "Refrescar Cache"
        public void RefrescarCache()
        {
            if (SesionActual.Current.PERFIL == "")
            {
                Session.Abandon();
                Response.Redirect("~");
            }
            else
            {
                ViewData["LOGUEO_NOMBRE_COMPLETO"] = SesionActual.Current.NOMBRE_COMPLETO;
                ViewData["LOGUEO_PERFIL"] = SesionActual.Current.PERFIL;
                ViewData["OPCIONES_USUARIO"] = SesionActual.Current.OPCIONES_USUARIO;
                Response.Cache.AppendCacheExtension("no-store, must-revalidate");
                Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
                Response.AppendHeader("Expires", "0"); // Proxies.
            }
        }
        #endregion

        [SessionExpireFilter]
        public SelectList Estados()
        {
            List<ParametroEL> lstParametros = new List<ParametroEL>();
            ParametroEL parametro;
            parametro = new ParametroEL();
            parametro.CODIGO_VALOR = "1";
            parametro.DESCRIPCION = "ACTIVO";
            parametro.VALOR = "1";
            lstParametros.Add(parametro);

            parametro = new ParametroEL();
            parametro.CODIGO_VALOR = "0";
            parametro.DESCRIPCION = "INACTIVO";
            parametro.VALOR = "0";
            lstParametros.Add(parametro);

            return new SelectList(lstParametros, CamposDDL.VALOR.Value, CamposDDL.DESCRIPCION.Value);
        }

        [SessionExpireFilter]
        public SelectList SelectListRoles()
        {
            var vLstParametro = new List<ParametroEL>();
            IParametroBL objParametroBL = new ParametroBL();
            ParametroEL objParametroEL = new ParametroEL();
            vLstParametro = objParametroBL.fn_Get_Roles("");
            return new SelectList(vLstParametro, CamposDDL.VALOR.Value, CamposDDL.DESCRIPCION.Value);
        }

        [SessionExpireFilter]
        public SelectList SelectListTiposDocumentos()
        {
            var vLstParametro = new List<ParametroEL>();
            IParametroBL objParametroBL = new ParametroBL();
            ParametroEL objParametroEL = new ParametroEL();
            vLstParametro = objParametroBL.fn_Get_Plantillas();
            return new SelectList(vLstParametro, CamposDDL.VALOR.Value, CamposDDL.DESCRIPCION.Value);
        }

        [SessionExpireFilter]
        public SelectList SelectListTiposDocumentosporPerfil(int codigoPerfil)
        {
            var vLstParametro = new List<ParametroEL>();
            IParametroBL objParametroBL = new ParametroBL();
            ParametroEL objParametroEL = new ParametroEL();
            vLstParametro = objParametroBL.fn_Get_PlantillasPorPerfil(codigoPerfil);
            return new SelectList(vLstParametro, CamposDDL.VALOR.Value, CamposDDL.DESCRIPCION.Value);
        }

        [SessionExpireFilter]
        public SelectList SelectListSexo()
        {
            List<ParametroEL> lstParametros = new List<ParametroEL>();
            ParametroEL parametro;
            parametro = new ParametroEL();
            parametro.CODIGO_VALOR = "0";
            parametro.DESCRIPCION = "--Seleccione--";
            parametro.VALOR = "0";
            lstParametros.Add(parametro);

            parametro = new ParametroEL();
            parametro.CODIGO_VALOR = "Masculino";
            parametro.DESCRIPCION = "Masculino";
            parametro.VALOR = "1";
            lstParametros.Add(parametro);

            parametro = new ParametroEL();
            parametro.CODIGO_VALOR = "Femenino";
            parametro.DESCRIPCION = "Femenino";
            parametro.VALOR = "2";
            lstParametros.Add(parametro);

            return new SelectList(lstParametros, CamposDDL.VALOR.Value, CamposDDL.DESCRIPCION.Value);
        }
        [SessionExpireFilter]
        public SelectList SelectListUsuarios()
        {

            IParametroBL objParametroBL = new ParametroBL();
            ParametroEL objParametroEL = new ParametroEL();
            var LstParametroBuscar = new List<ParametroEL>();
            objParametroEL.CODIGO_VALOR = "0";
            objParametroEL.VALOR = "0";
            objParametroEL.DESCRIPCION = "--TODOS--";
            LstParametroBuscar.Add(objParametroEL);
            var vLstParametro = objParametroBL.fn_Get_Usuarios();
            foreach (var item in vLstParametro)
            {
                LstParametroBuscar.Add(item);
            }
            return new SelectList(LstParametroBuscar, CamposDDL.VALOR.Value, CamposDDL.DESCRIPCION.Value);
        }

        [SessionExpireFilter]
        public SelectList SelectListEventos()
        {
            IParametroBL objParametroBL = new ParametroBL();
            ParametroEL objParametroEL = new ParametroEL();
            var LstParametroBuscar = new List<ParametroEL>();
            objParametroEL.CODIGO_VALOR = "0";
            objParametroEL.VALOR = "0";
            objParametroEL.DESCRIPCION = "--TODOS--";
            LstParametroBuscar.Add(objParametroEL);
            var vLstParametro = objParametroBL.fn_Get_Eventos();
            foreach (var item in vLstParametro)
            {
                LstParametroBuscar.Add(item);
            }
            return new SelectList(LstParametroBuscar, CamposDDL.VALOR.Value, CamposDDL.DESCRIPCION.Value);
        }

        [SessionExpireFilter]
        public SelectList SelectListTiposDocumentosporPerfilParaRepo(int codigoPerfil)
        {
            //var vLstParametro = new List<ParametroEL>();
            //IParametroBL objParametroBL = new ParametroBL();
            //ParametroEL objParametroEL = new ParametroEL();
            //vLstParametro = objParametroBL.fn_Get_PlantillasPorPerfil(codigoPerfil);

            IParametroBL objParametroBL = new ParametroBL();
            ParametroEL objParametroEL = new ParametroEL();
            var LstParametroBuscar = new List<ParametroEL>();
            objParametroEL.CODIGO_VALOR = "0";
            objParametroEL.VALOR = "0";
            objParametroEL.DESCRIPCION = "--TODOS--";
            LstParametroBuscar.Add(objParametroEL);

            var vLstParametro = objParametroBL.fn_Get_PlantillasPorPerfil(codigoPerfil);

            foreach (var item in vLstParametro)
            {
                LstParametroBuscar.Add(item);
            }

            return new SelectList(LstParametroBuscar, CamposDDL.VALOR.Value, CamposDDL.DESCRIPCION.Value);
        }

        [SessionExpireFilter]
        public SelectList SelectListGrupos()
        {
            IParametroBL objParametroBL = new ParametroBL();
            ParametroEL objParametroEL = new ParametroEL();
            var LstParametroBuscar = new List<ParametroEL>();
            objParametroEL.CODIGO_VALOR = "0";
            objParametroEL.VALOR = "0";
            objParametroEL.DESCRIPCION = ConfigurationManager.AppSettings["keyConstSeleccione"];
            LstParametroBuscar.Add(objParametroEL);
            var vLstParametro = objParametroBL.fn_GetGruposAll();
            foreach (var item in vLstParametro)
            {
                LstParametroBuscar.Add(item);
            }
            return new SelectList(LstParametroBuscar, CamposDDL.VALOR.Value, CamposDDL.DESCRIPCION.Value);
        }
        [SessionExpireFilter]
        public SelectList EstadosUsuario()
        {
            //0 : TODOS | 1:ACTIVO | 2: INACTIVO
            List<ParametroEL> lstParametros = new List<ParametroEL>();
            ParametroEL parametro;

            parametro = new ParametroEL();
            parametro.CODIGO_VALOR = "0";
            parametro.DESCRIPCION = ConfigurationManager.AppSettings["keyConstTodos"];
            parametro.VALOR = "0";
            lstParametros.Add(parametro);

            parametro = new ParametroEL();
            parametro.CODIGO_VALOR = "1";
            parametro.DESCRIPCION = "ACTIVO";
            parametro.VALOR = "1";
            lstParametros.Add(parametro);

            parametro = new ParametroEL();
            parametro.CODIGO_VALOR = "2";
            parametro.DESCRIPCION = "INACTIVO";
            parametro.VALOR = "2";
            lstParametros.Add(parametro);


            return new SelectList(lstParametros, CamposDDL.VALOR.Value, CamposDDL.DESCRIPCION.Value);
        }
    }
}
