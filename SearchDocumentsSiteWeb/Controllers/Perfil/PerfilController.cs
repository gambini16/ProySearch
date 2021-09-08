using SearchDocuments.Entidades;
using SearchDocuments.Entidades.Listado;
using SearchDocuments.Negocio.Perfil;
using SearchDocumentsSiteWeb.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SearchDocumentsSiteWeb.Controllers.Perfil
{
    public class PerfilController : Controller
    {
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

        [SessionExpireFilter]
        public JsonResult Leer_Perfil(string strNombrePerfil)
        {
            var vLstRole = new List<PerfilListadoEL>();
            IPerfilBL objRolBL = new PerfilBL();
            PerfilEL objRolEL = new PerfilEL();
            objRolEL.PERFIL_NOMBRE = strNombrePerfil;
            if (strNombrePerfil.Trim().Length == 0)
                vLstRole = objRolBL.fn_Get_Groups_All();
            else
                vLstRole = objRolBL.fn_Get_Perfil(objRolEL);



            Session[Constantes.LISTA.Value] = null;
            Session[Constantes.LISTA.Value] = vLstRole;
            return Json(vLstRole, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public ActionResult Ver(string intIdCodigoPerfil)
        {
            RefrescarCache();
            var vRol = new PerfilEL();
            IPerfilBL objRolBL = new PerfilBL();
            PerfilEL objRolEL = new PerfilEL();
            objRolEL.PERFIL_ID = int.Parse(intIdCodigoPerfil);
            vRol = objRolBL.fn_GetInfo_Perfil(objRolEL);
            return View(vRol);
        }

        [SessionExpireFilter]
        public ActionResult Editar(int intIdCodigoPerfil)
        {
            RefrescarCache();
            var vPerfil = new PerfilEL();
            IPerfilBL objRolBL = new PerfilBL();
            PerfilEL objRolEL = new PerfilEL();
            objRolEL.PERFIL_ID = intIdCodigoPerfil;
            vPerfil = objRolBL.fn_GetInfo_Perfil(objRolEL);
            return View(vPerfil);
        }

        [HttpPost]
        [SessionExpireFilter]
        public ActionResult Actualizar(string intIdPerfil, string strNombrePerfil, string strDescripcion)
        {
            string strRespuesta = string.Empty;
            IPerfilBL objRolBL = new PerfilBL();
            PerfilEL objRolEL = new PerfilEL();
            objRolEL.PERFIL_NOMBRE = strNombrePerfil.Trim();
            objRolEL.DESCRIPCION = strDescripcion.Trim();
            objRolEL.PERFIL_ID = int.Parse(intIdPerfil);
            objRolEL.USUARIO_CREACION = string.Empty;
            strRespuesta = objRolBL.fn_Update_Perfil(objRolEL);
            return Json(new { strRespuesta = strRespuesta }, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public ActionResult Crear()
        {
            RefrescarCache();
            return View();
        }

        [HttpPost]
        [SessionExpireFilter]
        public ActionResult Guardar(string strNombrePerfil, string strDescripcion)
        {
            string strRespuesta = string.Empty;
            IPerfilBL objRolBL = new PerfilBL();
            PerfilEL objRolEL = new PerfilEL();
            objRolEL.PERFIL_NOMBRE = strNombrePerfil.ToUpper();
            objRolEL.DESCRIPCION = strDescripcion.ToUpper();
            objRolEL.USUARIO_CREACION = SesionActual.Current.USUARIO_LOGIN;
            strRespuesta = objRolBL.fn_Insert_Perfil(objRolEL);
            return Json(new { strRespuesta = strRespuesta }, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public ActionResult ExportarExcel()
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Perfil.xls");
            //Response.AddHeader("Content-Type", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");

            //Response.ContentEncoding = System.Text.Encoding.Default;
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Charset = "";
            ExportExcel.ExportarExcel<PerfilListadoEL>(TitulosCabeceraExcel.PERFIL.Value, (List<PerfilListadoEL>)(Session[Constantes.LISTA.Value]), Response.Output);
            Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());

            Response.Flush();
            Response.End();
            string strRespuesta = "";
            return Json(new { strRespuesta = strRespuesta });
        }

        // GET: Perfil
        public ActionResult Index()
        {
            RefrescarCache();
            return View();
        }

        [SessionExpireFilter]
        public ActionResult IndexModulo()
        {
            RefrescarCache();
            return View();
        }

        [SessionExpireFilter]
        public ActionResult IndexPlantillaPerfil()
        {
            RefrescarCache();
            return View();
        }

        [SessionExpireFilter]
        public ActionResult PerfilModulo(int intIdCodigoPerfil, string strNombrePerfil)
        {
            RefrescarCache();
            ViewData["intIdCodigoPerfil"] = intIdCodigoPerfil;
            ViewData["strNombrePerfil"] = strNombrePerfil;
            return View();
        }

        [SessionExpireFilter]
        public ActionResult PlantillaPerfil(int intIdCodigoPerfil, string strNombrePerfil)
        {
            RefrescarCache();
            ViewData["intIdCodigoPerfil"] = intIdCodigoPerfil;
            ViewData["strNombrePerfil"] = strNombrePerfil;
            return View();
        }

        [SessionExpireFilter]
        public JsonResult Leer_Modulo(int intPerfilId, int intTipoConsulta)
        {
            var vLstRol = new List<ModuloEL>();
            IPerfilBL objRolBL = new PerfilBL();
            PerfilEL objRolEL = new PerfilEL();
            objRolEL.PERFIL_ID = intPerfilId;
            vLstRol = objRolBL.fn_Get_Perfil_Modulo_Actual(intTipoConsulta, objRolEL);
            return Json(vLstRol, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public JsonResult Leer_Plantilla(int intPerfilId, int intTipoConsulta)
        {
            var vLstRol = new List<PlantillaEL>();
            IPerfilBL objRolBL = new PerfilBL();
            PerfilEL objRolEL = new PerfilEL();
            objRolEL.PERFIL_ID = intPerfilId;
            vLstRol = objRolBL.fn_Get_Plantilla_Perfil(intTipoConsulta, objRolEL);
            return Json(vLstRol, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public ActionResult ExportarExcelModuloPerfil()
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ModuloPerfil.xls");
            //Response.AddHeader("Content-Type", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");

            //Response.ContentEncoding = System.Text.Encoding.Default;
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Charset = "";
            ExportExcel.ExportarExcel<PerfilListadoEL>(TitulosCabeceraExcel.MODULO_PERFIL.Value, (List<PerfilListadoEL>)(Session[Constantes.LISTA.Value]), Response.Output);
            Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());

            Response.Flush();
            Response.End();
            string strRespuesta = "";
            return Json(new { strRespuesta = strRespuesta });
        }

        [HttpPost]
        [SessionExpireFilter]
        public ActionResult GuardarPerfilModulo(ModuloModel model)
        {
            int intPerfilID = 0;
            intPerfilID = model.MODULOS[0].PERFIL_ID;
            List<ModuloEL> lstOpciones = new List<ModuloEL>();
            ModuloEL opcion;
            foreach (var fila in model.MODULOS)
            {
                opcion = new ModuloEL();
                opcion.MODULO_ID = fila.MODULO_ID;
                lstOpciones.Add(opcion);
            }
            IPerfilBL objRolBL = new PerfilBL();
            PerfilEL objRolEL = new PerfilEL();
            objRolEL.PERFIL_ID = intPerfilID;
            objRolEL.USUARIO_CREACION = SesionActual.Current.USUARIO_LOGIN;
            string strRespuesta = objRolBL.fn_Insert_PerfilModulo(lstOpciones, objRolEL);
            return Json(new { strRespuesta = strRespuesta });
        }

        [HttpPost]
        [SessionExpireFilter]
        public ActionResult GuardarPerfilPlantilla(PlantillaModel model)
        {
            int intPerfilID = 0;
            intPerfilID = model.PLANTILLAS[0].PERFIL_ID;
            List<PlantillaEL> lstOpciones = new List<PlantillaEL>();
            PlantillaEL opcion;
            foreach (var fila in model.PLANTILLAS)
            {
                opcion = new PlantillaEL();
                opcion.PLANTILLA_ID = fila.PLANTILLA_ID;
                lstOpciones.Add(opcion);
            }
            IPerfilBL objRolBL = new PerfilBL();
            PerfilEL objRolEL = new PerfilEL();
            objRolEL.PERFIL_ID = intPerfilID;
            objRolEL.USUARIO_CREACION = SesionActual.Current.USUARIO_LOGIN;
            string strRespuesta = objRolBL.fn_Insert_PerfilPlantilla(lstOpciones, objRolEL);
            return Json(new { strRespuesta = strRespuesta });
        }

        public class ModuloModel
        {
            public List<DatosModel> MODULOS { get; set; }
        }

        public class DatosModel
        {
            public int PERFIL_ID { get; set; }
            public int MODULO_ID { get; set; }
            public string MODULO_NAME { get; set; }
        }

        public class PlantillaModel
        {
            public List<InformacionModel> PLANTILLAS { get; set; }
        }

        public class InformacionModel
        {
            public int PERFIL_ID { get; set; }
            public int PLANTILLA_ID { get; set; }
            public string PLANTILLA_NOMBRE { get; set; }
        }
    }
}
