using SearchDocuments.Entidades;
using SearchDocuments.Entidades.Listado;
using SearchDocuments.Negocio.Usuario;
using SearchDocumentsSiteWeb.Controllers.Parametro;
using SearchDocumentsSiteWeb.General;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SearchDocumentsSiteWeb.Controllers.Usuario
{
    public class UsuarioController : Controller
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

        [HttpGet]
        public JsonResult Leer_Usuario(string strLogin, string strApellidosNombres)
        {
            var vLstUsuario = new List<UsuarioListadoEL>();
            IUsuarioBL objUsuarioBL = new UsuarioBL();
            UsuarioEL objUsuarioEL = new UsuarioEL();
            objUsuarioEL.LOGIN = strLogin;
            objUsuarioEL.NOMBRE_COMPLETO = strApellidosNombres;
            vLstUsuario = objUsuarioBL.fn_Get_Usuario(objUsuarioEL);
            Session[Constantes.LISTA.Value] = null;
            Session[Constantes.LISTA.Value] = vLstUsuario;
            return Json(vLstUsuario, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public ActionResult Crear()
        {
            RefrescarCache();
            ViewBag.Estado = new ParametroController().Estados();
            ViewBag.Rol = new ParametroController().SelectListRoles();
            return View();
        }

        [HttpPost]
        [SessionExpireFilter]
        public ActionResult Guardar()
        {
            string strRespuesta = string.Empty;
            IUsuarioBL objUsuarioBL = new UsuarioBL();
            UsuarioEL objUsuarioEL = new UsuarioEL();
            objUsuarioEL.EMAIL = Request.Form["correo"].ToString().ToUpper();
            objUsuarioEL.LOGIN = Request.Form["login"].ToString().ToUpper();
            objUsuarioEL.APELLIDO_PATERNO = Request.Form["apellidoPaterno"].ToString().ToUpper();
            objUsuarioEL.NOMBRES = Request.Form["nombres"].ToString().ToUpper();
            objUsuarioEL.ROL_CODIGO = Convert.ToInt32(Request.Form["rol"].ToString());
            objUsuarioEL.ESTADO_CODIGO = Convert.ToInt32(Request.Form["estado"].ToString());
            if (objUsuarioEL.ESTADO_CODIGO==1)
            {
                objUsuarioEL.ESTADO_CODIGO = 0;
            }
            else
            {
                objUsuarioEL.ESTADO_CODIGO = 1;
            }
            objUsuarioEL.CLAVE = Encriptador.Encriptar(Request.Form["clave"].ToString());
            objUsuarioEL.USUARIO_REGISTRO = SesionActual.Current.USUARIO_LOGIN;
            strRespuesta = objUsuarioBL.fn_Insert_Usuario(objUsuarioEL);
            if (Request.Files.Count > 0)
            {
                HttpFileCollectionBase files = Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    HttpPostedFileBase file = files[i];
                    string fname;
                    if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                    {
                        string[] testfiles = file.FileName.Split(new char[] { '\\' });
                        fname = testfiles[testfiles.Length - 1];
                    }
                    else
                    {
                        fname = file.FileName;
                        fname = objUsuarioEL.LOGIN + Path.GetExtension(file.FileName);
                        objUsuarioEL.AVATAR = fname;
                        objUsuarioEL.USUARIO_REGISTRO = SesionActual.Current.USUARIO_LOGIN;
                    }
                    fname = Path.Combine(Server.MapPath("~/Avatar/"), fname);
                    file.SaveAs(fname);
                    objUsuarioBL.fn_Update_UsuarioAvatar(objUsuarioEL);
                }
            }
            else
            {
                objUsuarioEL.AVATAR = "userDefault.jpg";
                objUsuarioEL.USUARIO_REGISTRO = SesionActual.Current.USUARIO_LOGIN;
                objUsuarioBL.fn_Update_UsuarioAvatar(objUsuarioEL);
            }
            return Json(new { strRespuesta = (int.Parse(strRespuesta) > 0 ? "1" : strRespuesta) }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [SessionExpireFilter]
        public ActionResult Actualizar()
        {
            string strRespuesta = string.Empty;
            IUsuarioBL objUsuarioBL = new UsuarioBL();
            UsuarioEL objUsuarioEL = new UsuarioEL();

            objUsuarioEL.USUARIO_ID = Convert.ToInt32(Request.Form["id"].ToString());
            objUsuarioEL.EMAIL = Request.Form["correo"].ToString().ToUpper();
            objUsuarioEL.LOGIN = Request.Form["login"].ToString().ToUpper();
            objUsuarioEL.APELLIDO_PATERNO = Request.Form["apellidoPaterno"].ToString().ToUpper();
            objUsuarioEL.NOMBRES = Request.Form["nombres"].ToString().ToUpper();
            objUsuarioEL.ESTADO_CODIGO = Convert.ToInt32(Request.Form["estado"].ToString());
            if (objUsuarioEL.ESTADO_CODIGO == 1)
            {
                objUsuarioEL.ESTADO_CODIGO = 0;
            }
            else
            {
                objUsuarioEL.ESTADO_CODIGO = 1;
            }
            objUsuarioEL.ROL_CODIGO = Convert.ToInt32(Request.Form["rol"].ToString());
            objUsuarioEL.CLAVE = Encriptador.Encriptar(Request.Form["clave"].ToString());
            objUsuarioEL.USUARIO_REGISTRO = SesionActual.Current.USUARIO_LOGIN;
            strRespuesta = objUsuarioBL.fn_Update_Usuario(objUsuarioEL);
            HttpFileCollectionBase files = Request.Files;
            for (int i = 0; i < files.Count; i++)
            {
                HttpPostedFileBase file = files[i];
                string fname;
                if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                {
                    string[] testfiles = file.FileName.Split(new char[] { '\\' });
                    fname = testfiles[testfiles.Length - 1];
                }
                else
                {
                    fname = file.FileName;
                    fname = objUsuarioEL.LOGIN + Path.GetExtension(file.FileName);
                    objUsuarioEL.AVATAR = fname;
                    objUsuarioEL.USUARIO_REGISTRO = SesionActual.Current.USUARIO_LOGIN;
                }
                fname = Path.Combine(Server.MapPath("~/Avatar/"), fname);
                file.SaveAs(fname);
                objUsuarioBL.fn_Update_UsuarioAvatar(objUsuarioEL);
            }
            return Json(new { strRespuesta = strRespuesta }, JsonRequestBehavior.AllowGet);
        }

        [SessionExpireFilter]
        public ActionResult Editar(int strIdCodigoUsuario)
        {
            RefrescarCache();
            var vusuario = new UsuarioEL();
            IUsuarioBL objUsuarioBL = new UsuarioBL();
            UsuarioEL objUsuarioEL = new UsuarioEL();
            objUsuarioEL.USUARIO_ID = strIdCodigoUsuario;
            ViewBag.Estado = new ParametroController().Estados();
            ViewBag.Rol = new ParametroController().SelectListRoles();
            vusuario = objUsuarioBL.fn_GetInfo_Usuario(objUsuarioEL);
            vusuario.CLAVE = Desencriptador.Desencriptar(vusuario.CLAVE);
            string strRuta = String.Empty;
            var strRutaServicio = System.Configuration.ConfigurationManager.AppSettings["RutaAvatar"].ToString();
            strRuta = Request.ApplicationPath + strRutaServicio + vusuario.AVATAR;
            ViewData["urlAvatar"] = strRuta;
            return View("Editar", vusuario);
        }

        [SessionExpireFilter]
        public ActionResult Ver(int strIdCodigoUsuario)
        {
            RefrescarCache();
            var vusuario = new UsuarioEL();
            IUsuarioBL objUsuarioBL = new UsuarioBL();
            UsuarioEL objUsuarioEL = new UsuarioEL();
            objUsuarioEL.USUARIO_ID = strIdCodigoUsuario;
            ViewBag.Estado = new ParametroController().Estados();
            ViewBag.Rol = new ParametroController().SelectListRoles();
            vusuario = objUsuarioBL.fn_GetInfo_Usuario(objUsuarioEL);
            string strRuta = String.Empty;
            var strRutaServicio = System.Configuration.ConfigurationManager.AppSettings["RutaAvatar"].ToString();
            strRuta = Request.ApplicationPath + strRutaServicio + vusuario.AVATAR;
            ViewData["urlAvatar"] = strRuta;
            return View("Ver", vusuario);
        }

        [SessionExpireFilter]
        public ActionResult ExportarExcel()
        {
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=Usuario.xls");
            Response.AddHeader("Content-Type", "application/vnd.ms-excel");

            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Charset = "";
            ExportExcel.ExportarExcel<UsuarioListadoEL>(TitulosCabeceraExcel.USUARIO.Value, (List<UsuarioListadoEL>)(Session[Constantes.LISTA.Value]), Response.Output);
            Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());

            Response.Flush();
            Response.End();
            string strRespuesta = "";
            return Json(new { strRespuesta = strRespuesta });
        }

        // GET: Usuario
        public ActionResult Index()
        {
            RefrescarCache();
            return View();
        }
    }
}
