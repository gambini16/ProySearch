using SearchDocuments.Entidades;
using SearchDocuments.Entidades.Listado;
using SearchDocuments.Negocio.Modulo;
using SearchDocumentsSiteWeb.Controllers.Parametro;
using SearchDocumentsSiteWeb.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SearchDocumentsSiteWeb.Controllers.Modulo
{
    public class ModuloController : Controller
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
                ViewData["LOGUEO_PERFIL_NAME"] = SesionActual.Current.PERFIL_NOMBRE;
                ViewData["USER_ID"] = SesionActual.Current.IN_CODIGO_USU;
                Response.Cache.AppendCacheExtension("no-store, must-revalidate");
                Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
                Response.AppendHeader("Expires", "0"); // Proxies.
            }
        }
        #endregion

        [SessionExpireFilter]
        public JsonResult Leer_Modulo(string strNombreModulo)
        {
            var vLstOpcion = new List<ModuloListadoEL>();
            IModuloBL objOpcionBL = new ModuloBL();
            ModuloEL objOpcionEL = new ModuloEL();
            objOpcionEL.MODULO_NOMBRE = strNombreModulo;
            vLstOpcion = objOpcionBL.fn_Get_Modulo(objOpcionEL);
            Session[Constantes.LISTA.Value] = null;
            Session[Constantes.LISTA.Value] = vLstOpcion;
            return Json(vLstOpcion, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public ActionResult Ver(int intIdCodigoModulo)
        {
            RefrescarCache();
            var vOpcion = new ModuloEL();
            IModuloBL objOpcionBL = new ModuloBL();
            ModuloEL objOpcionEL = new ModuloEL();
            objOpcionEL.MODULO_ID = intIdCodigoModulo;
            ViewBag.EstadoModulo = new ParametroController().Estados();
            vOpcion = objOpcionBL.fn_GetInfo_Modulo(objOpcionEL);
            return View(vOpcion);
        }

        [SessionExpireFilter]
        public ActionResult Editar(int intIdCodigoModulo)
        {
            RefrescarCache();
            var vOpcion = new ModuloEL();
            IModuloBL objOpcionBL = new ModuloBL();
            ModuloEL objOpcionEL = new ModuloEL();
            objOpcionEL.MODULO_ID = intIdCodigoModulo;
            ViewBag.EstadoModulo = new ParametroController().Estados();
            vOpcion = objOpcionBL.fn_GetInfo_Modulo(objOpcionEL);
            return View(vOpcion);
        }

        [HttpPost]
        [SessionExpireFilter]
        public ActionResult Actualizar(string strNombreModulo, string strSiglas, string strEstado, string strIdModulo)
        {
            string strRespuesta = string.Empty;
            IModuloBL objOpcionBL = new ModuloBL();
            ModuloEL objOpcionEL = new ModuloEL();
            objOpcionEL.MODULO_ID = int.Parse(strIdModulo);
            objOpcionEL.MODULO_NOMBRE = strNombreModulo.ToUpper();
            objOpcionEL.SIGLA = strSiglas.ToUpper();
            objOpcionEL.ESTADO = int.Parse(strEstado);
            objOpcionEL.USUARIO_CREACION = SesionActual.Current.USUARIO_LOGIN;
            strRespuesta = objOpcionBL.fn_Update_Modulo(objOpcionEL);
            return Json(new { strRespuesta = strRespuesta }, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public ActionResult Crear()
        {
            RefrescarCache();
            ViewBag.EstadoModulo = new ParametroController().Estados();
            return View();
        }

        [HttpPost]
        [SessionExpireFilter]
        public ActionResult Guardar(string strNombreModulo, string strSiglas, string strEstado)
        {
            string strRespuesta = string.Empty;
            IModuloBL objOpcionBL = new ModuloBL();
            ModuloEL objOpcionEL = new ModuloEL();
            objOpcionEL.MODULO_NOMBRE = strNombreModulo.ToUpper();
            objOpcionEL.SIGLA = strSiglas.ToUpper();
            objOpcionEL.USUARIO_CREACION = SesionActual.Current.USUARIO_LOGIN;
            objOpcionEL.ESTADO = Convert.ToInt32(strEstado);
            strRespuesta = objOpcionBL.fn_Insert_Modulo(objOpcionEL);
            return Json(new { strRespuesta = strRespuesta }, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public ActionResult ExportarExcel()
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Modulo.xls");
            //Response.AddHeader("Content-Type", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");

            //Response.ContentEncoding = System.Text.Encoding.Default;
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Charset = "";
            ExportExcel.ExportarExcel<ModuloListadoEL>(TitulosCabeceraExcel.MODULO.Value, (List<ModuloListadoEL>)(Session[Constantes.LISTA.Value]), Response.Output);
            Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());

            Response.Flush();
            Response.End();
            string strRespuesta = "";
            return Json(new { strRespuesta = strRespuesta });
        }

        // GET: Modulo
        public ActionResult Index()
        {
            RefrescarCache();
            return View();
        }
    }
}
