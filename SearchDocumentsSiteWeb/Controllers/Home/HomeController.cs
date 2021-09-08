using SearchDocumentsSiteWeb.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SearchDocumentsSiteWeb.Controllers.Home
{
    public class HomeController : Controller
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
                ViewData["NOMBRE_SISTEMA"] = SesionActual.Current.APLICACION;
                ViewData["AVATAR"] = SesionActual.Current.AVATAR;
                ViewData["FORZAR_CLAVE"] = SesionActual.Current.FORZAR_CLAVE;
                ViewData["LOGUEO_PERFIL_NAME"] = SesionActual.Current.PERFIL_NOMBRE;
                ViewData["USER_ID"] = SesionActual.Current.IN_CODIGO_USU;
                Response.Cache.AppendCacheExtension("no-store, must-revalidate");
                Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
                Response.AppendHeader("Expires", "0"); // Proxies.
            }
        }

        public ActionResult Index()
        {
            RefrescarCache();
            return View();
        }

        [SessionExpireFilter]
        public ActionResult NavBar()
        {
            RefrescarCache();
            string strRuta = String.Empty;
            string strRutaServicio = String.Empty;
            if (SesionActual.Current.AVATAR == null || SesionActual.Current.AVATAR.ToString().Equals(""))
            {
                strRutaServicio = System.Configuration.ConfigurationManager.AppSettings["RutaFotoUsuario"].ToString();
                strRuta = Request.ApplicationPath + strRutaServicio + "userDefault.jpg";
            }
            else
            {
                strRutaServicio = System.Configuration.ConfigurationManager.AppSettings["RutaAvatar"].ToString();
                strRuta = Request.ApplicationPath + strRutaServicio + SesionActual.Current.AVATAR;
            }
            ViewData["urlUsuarioFoto"] = strRuta;
            string strRutaLogo = System.Configuration.ConfigurationManager.AppSettings["RutaLogo"].ToString();
            string strRutaFinalLogo = Request.ApplicationPath + strRutaLogo + SesionActual.Current.SOCIEDAD_LOGO;
            ViewData["urlLogo"] = strRutaFinalLogo;
            return PartialView("_NavBar");
        }

        [SessionExpireFilter]
        public ActionResult EncriptarParametroJS(string strParametro)
        {
            return Json(new { strEncriptado = Encriptador.Encriptar(strParametro) });
        }
    }
}
