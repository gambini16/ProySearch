using SearchDocuments.Comunes;
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
            ViewBag.UrlRedirectContrasena = ConfigurationManager.AppSettings["ketUrlRedirectContrasena"].ToString();
            return View();
        }
        [HttpPost]
        public JsonResult RecuperarClave()
        {
            IUsuarioBL objUsuarioBL = new UsuarioBL();

            string userEmail = Request.Form["correo"].ToString();
            var objUsuario = objUsuarioBL.ObtenerUsuarioPorEmail(userEmail);

            if (objUsuario.USUARIO_ID == 0)
            {
                return Json(new { strRespuesta = "NOOK" }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                string password = Util.CrearPassword(10);

                var requestUpdatePassword = new UsuarioEL
                {
                    USUARIO_ID = objUsuario.USUARIO_ID,
                    CLAVE = Cryptography.Encrypt(password, objUsuario.USUARIO_ID.ToString())
                };

                var passwordEncriptado = objUsuarioBL.Update_Tbl_usersPassword(requestUpdatePassword);

                //var strRespuesta = passwordEncriptado == "0" ? "OK" : "NOOK";


                if (passwordEncriptado == "0")
                {
                    fn_enviar_correo_usuario(objUsuario, userEmail, password);
                }

                return Json(new { strRespuesta = passwordEncriptado }, JsonRequestBehavior.AllowGet);
            }
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
            body = body.Replace("{nombres}", usuario.NOMBRES + " " + usuario.APELLIDO_PATERNO);
            body = body.Replace("{usuario}", usuario.LOGIN.ToString());
            body = body.Replace("{clave}", nuevaClave);
            body = body.Replace("{urlImagen}", urlImagen);
            var smtp = new SmtpClient
            {
                Host = ConfigurationManager.AppSettings["ServidorSMTP"],
                //Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = sub,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(mess);
            }
            return "OK";
        }

        //posible borrar
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

        //posible borrar
        [SessionExpireFilter]
        public ActionResult Recuperar()
        {
            return View();
        }

        //posible borrar
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
                string strPwd = Cryptography.Decrypt(oUsuario.CLAVE, oUsuario.USUARIO_ID.ToString());

                if (!strPwd.Equals(PwdOld))
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

                    string passwordEncriptado = Cryptography.Encrypt(PwdNew, oUsuario.USUARIO_ID.ToString());
                    strRespuesta = objUsuarioBL.fn_Update_Usuario(CodUser, passwordEncriptado);
                    return Json(new { strRespuesta = strRespuesta }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                strRespuesta = "1";
                return Json(new { strRespuesta = strRespuesta }, JsonRequestBehavior.AllowGet);

            }
        }
    }
}
