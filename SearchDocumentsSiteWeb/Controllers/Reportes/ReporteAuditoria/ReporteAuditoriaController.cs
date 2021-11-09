using SearchDocuments.Entidades;
using SearchDocuments.Negocio.Auditoria;
using SearchDocumentsSiteWeb.Controllers.Parametro;
using SearchDocumentsSiteWeb.General;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SearchDocumentsSiteWeb.Controllers.Reportes
{
    public class ReporteAuditoriaController : Controller
    {
        #region "Refrescar Cache"
        [SessionExpireFilter]
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

        // GET: ReporteAuditoria
        public ActionResult Index()
        {
            RefrescarCache();
            ViewBag.Usuarios = new ParametroController().SelectListUsuarios();
            ViewBag.Eventos = new ParametroController().SelectListEventos();
            return View();
        }

        [HttpPost]
        public JsonResult GetDatosAuditoria(Tbl_audit_events request)
        {
            request.Event = request.Event == null ? "" : request.Event;
            if (request.EsFechaHabilitada == 1)
            {
                var fechaFinMasUno = Convert.ToDateTime(request.FechaFin).AddDays(1);
                request.FechaFin = fechaFinMasUno;
            }

            IAuditoriaBL objIAuditoriaBL = new AuditoriaBL();

            var listadoAuditoria = objIAuditoriaBL.ObtenerListadoTblAuditEvents(request);

            var listadoAuditoriaExcel = objIAuditoriaBL.ObtenerListadoTblAuditEventsExcel(request);

            this.Session[Constantes.LISTA.Value] = (object)null;
            this.Session[Constantes.LISTA.Value] = (object)listadoAuditoriaExcel;

            return this.Json(new { list = listadoAuditoria, cantidadRegistros = listadoAuditoriaExcel.Count }, JsonRequestBehavior.AllowGet);

        }

        [SessionExpireFilter]
        public ActionResult ExportarExcel()
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Auditoria.xls");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");

            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Charset = "";

            ExportExcel.ExportarExcel<Tbl_audit_events>(TitulosCabeceraExcel.AUDITORIA.Value, (List<Tbl_audit_events>)(Session[Constantes.LISTA.Value]), Response.Output);
            Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());

            Response.Flush();
            Response.End();
            string strRespuesta = "";
            return Json(new { strRespuesta = strRespuesta });
        }
    }
}