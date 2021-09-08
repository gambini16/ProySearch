using SearchDocuments.Entidades;
using SearchDocuments.Negocio.SeguridadSistema;
using SearchDocuments.Negocio.Usuario;
using SearchDocumentsSiteWeb.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace SearchDocumentsSiteWeb.Controllers.Contrasena
{
    public class ContrasenaController : Controller
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

        // GET: Contrasena
        public ActionResult Index()
        {
            return View();
        }

        [SessionExpireFilter]
        public ActionResult Editar()
        {
            this.RefrescarCache();
            int intCodUser = Convert.ToInt32(ViewData["USER_ID"]);
            IUsuarioBL objBL = new UsuarioBL();
            UsuarioEL objUsuario = new UsuarioEL();
            UsuarioEL objUser = new UsuarioEL();
            objUser.USUARIO_ID = intCodUser;
            objUser = objBL.fn_GetInfo_Usuario(objUser);
            return View("Editar", objUser);
        }

        [SessionExpireFilter]
        public ActionResult Recuperar()
        {
            return View();
        }

        public ActionResult Actualizar(int CodUser, string PwdOld, string PwdNew)
        {
            string strRespuesta = string.Empty;
            IUsuarioBL objBL = new UsuarioBL();
            Tbl_users objUsuario = new Tbl_users();
            UsuarioEL oUsuario = new UsuarioEL();
            oUsuario.USUARIO_ID = CodUser;
            //objUsuario = objBL.ObtenerCodigoUsuario(CodUser);
            oUsuario = objBL.fn_GetInfo_Usuario(oUsuario);
            if (oUsuario != null)
            {
                if (!oUsuario.CLAVE.Equals(PwdOld))
                {
                    //Contraseña incorrecta
                    strRespuesta = "2";
                    return Json(new { strRespuesta = strRespuesta }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    //Contraseña correcta
                    IUsuarioBL objUsuarioBL = new UsuarioBL();
                    UsuarioEL objUsuarioEL = new UsuarioEL();
                    strRespuesta = objUsuarioBL.fn_Update_Usuario(CodUser, PwdNew);
                    return Json(new { strRespuesta = strRespuesta }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                strRespuesta = "1";
                return Json(new { strRespuesta = strRespuesta }, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult RecuperarClave()
        {
            string strRespuesta = string.Empty;
            IUsuarioBL objUsuarioBL = new UsuarioBL();
            UsuarioEL objUsuarioEL = new UsuarioEL();
            objUsuarioEL.EMAIL = Request.Form["correo"].ToString();
            string nuevaClave = CrearPassword(10);
            objUsuarioEL.CLAVE = Encriptador.Encriptar(nuevaClave);
            strRespuesta = objUsuarioBL.fn_Update_GenerarClave(objUsuarioEL);
            UsuarioEL usuario = objUsuarioBL.fn_GetInfo_UsuarioCorreo(objUsuarioEL);
            if (usuario == null)
            {
                strRespuesta = "NOOK";
            }
            else
            {
                fn_enviar_correo_usuario(usuario, objUsuarioEL.EMAIL, nuevaClave);
            }
            return Json(new { strRespuesta = strRespuesta }, JsonRequestBehavior.AllowGet);
        }

        public string fn_enviar_correo_usuario(UsuarioEL usuario, string correo, string nuevaClave)
        {
            string usuarioEnvio = ConfigurationManager.AppSettings["usuarioCorreo"].ToString();
            string contrasena = ConfigurationManager.AppSettings["contrasenaCorreo"].ToString();
            string urlImagen = ConfigurationManager.AppSettings["urlImagenCorreo"].ToString();
            var senderEmail = new MailAddress(usuarioEnvio, "Gestor Documentos");
            var receiverEmail = new MailAddress(correo, usuario.LOGIN.ToUpper().ToString());
            var password = contrasena;
            var sub = "Recuperación de Contraseña de usuario : " + usuario.LOGIN.ToUpper().ToString();
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/Plantillas/TemplateRecuperar.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{nombres}", usuario.NOMBRES + " " + usuario.APELLIDO_PATERNO );
            body = body.Replace("{usuario}", usuario.LOGIN.ToUpper().ToString());
            body = body.Replace("{clave}", nuevaClave);
            body = body.Replace("{urlImagen}", urlImagen);
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = "Recuperación de Contraseña de usuario : " + usuario.LOGIN_USUARIO,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(mess);
            }
            return "OK";
        }

        public string CrearPassword(int longitud)
        {
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < longitud--)
            {
                res.Append(caracteres[rnd.Next(caracteres.Length)]);
            }
            return res.ToString();
        }
    }
}
