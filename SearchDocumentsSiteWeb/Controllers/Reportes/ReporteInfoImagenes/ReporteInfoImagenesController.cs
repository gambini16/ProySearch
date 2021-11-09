using SearchDocuments.Entidades.InfoImagenes;
using SearchDocuments.Negocio.InfoImagenes;
using SearchDocumentsSiteWeb.Controllers.Parametro;
using SearchDocumentsSiteWeb.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SearchDocumentsSiteWeb.Controllers.Reportes.ReporteInfoImagenes
{
    public class ReporteInfoImagenesController : Controller
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

        // GET: ReporteInfoImagenes
        public ActionResult Index()
        {
            RefrescarCache();
            int codPerfil = Convert.ToInt32(ViewData["LOGUEO_PERFIL"].ToString());
            ViewBag.TipoDocumentoInfoImagenes = new ParametroController().SelectListTiposDocumentosporPerfilParaRepo(codPerfil);

            return View();
        }

        [HttpPost]
        public JsonResult GetDatosInfoImagenes(int templateId)
        {

            IInfoImagenesBL objInfoImagenesBL = new InfoImagenesBL();

            var listInfoImagenes = objInfoImagenesBL.ObtenerLIstadoTblToc(templateId);

            this.Session[Constantes.LISTA.Value] = (object)null;
            this.Session[Constantes.LISTA.Value] = (object)listInfoImagenes;

            return this.Json((object)listInfoImagenes, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public ActionResult ExportarExcel()
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=InfoImagenes.xls");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");

            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Charset = "";
            ExportExcel.ExportarExcel<InfoImagenesEL>(TitulosCabeceraExcel.IMAGENES.Value, (List<InfoImagenesEL>)(Session[Constantes.LISTA.Value]), Response.Output);
            Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());

            Response.Flush();
            Response.End();
            string strRespuesta = "";
            return Json(new { strRespuesta = strRespuesta });
        }
    }
}