using SearchDocuments.Comunes;
using SearchDocuments.Entidades;
using SearchDocuments.Negocio.Usuario;
using SearchDocumentsSiteWeb.Controllers.Parametro;
using SearchDocumentsSiteWeb.General;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Mail;
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
        // GET: Usuario
        public ActionResult Index()
        {
            RefrescarCache();

            ViewBag.ListGrupo = new ParametroController().SelectListGrupos();
            ViewBag.EstadoUsuario = new ParametroController().EstadosUsuario();
            return View();
        }

        [HttpPost]
        public JsonResult GetListadoUsuario(UsuarioEL objUsuarioRequest)
        {
            IUsuarioBL objUsuarioBL = new UsuarioBL();
            var listUsuario = objUsuarioBL.ObtenerListadoUsuario(objUsuarioRequest);

            this.Session[Constantes.LISTA.Value] = (object)null;
            this.Session[Constantes.LISTA.Value] = (object)listUsuario;

            return this.Json((object)listUsuario, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetUsuarioPorId(int IdUsuario)
        {
            IUsuarioBL objUsuarioBL = new UsuarioBL();
            var objUsuario = objUsuarioBL.ObtenerUsuarioPorId(IdUsuario);

            return this.Json((object)objUsuario, JsonRequestBehavior.AllowGet);
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
            ExportExcel.ExportarExcel<UsuarioEL>(TitulosCabeceraExcel.AUDITORIA.Value, (List<UsuarioEL>)(Session[Constantes.LISTA.Value]), Response.Output);
            Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble());

            Response.Flush();
            Response.End();
            string strRespuesta = "";
            return Json(new { strRespuesta = strRespuesta });
        }

        [HttpPost]
        public JsonResult GuadarEnBaseDatos(UsuarioEL request)
        {
            string resultado = string.Empty;
            IUsuarioBL objUsuarioBL = new UsuarioBL();

            if (request.USUARIO_ID == 0)
            {
                string strPwd = Util.CrearPassword(10);

                //request.CLAVE = strPwd;

                var result = objUsuarioBL.Insert_Tbl_users(request);

                var requestUpdatePassword = new UsuarioEL
                {
                    USUARIO_ID = Funciones.CheckInt(result),
                    CLAVE = Cryptography.Encrypt(strPwd, result)
                };

                var passwordEncriptado = objUsuarioBL.Update_Tbl_usersPassword(requestUpdatePassword);

                resultado = passwordEncriptado == "0" ? "OK" : "NOOK";

                if (resultado == "OK")
                {
                    fn_enviar_correo_usuario(request, request.EMAIL, strPwd);
                }
            }
            else
            {
                var result = objUsuarioBL.Update_Tbl_users(request);
                resultado = result == "0" ? "OK" : "NOOK";
            }

            return this.Json((object)new
            {
                Value = resultado,
            }, JsonRequestBehavior.AllowGet);
        }

        private string fn_enviar_correo_usuario(UsuarioEL usuario, string correo, string nuevaClave)
        {
            string usuarioEnvio = ConfigurationManager.AppSettings["UsuarioCorreo"].ToString();
            string contrasena = ConfigurationManager.AppSettings["ContrasenaCorreo"].ToString();
            string urlImagen = ConfigurationManager.AppSettings["urlImagenCorreo"].ToString();
            var senderEmail = new MailAddress(usuarioEnvio, ConfigurationManager.AppSettings["TituloEmail2"]);
            var receiverEmail = new MailAddress(correo, usuario.LOGIN.ToUpper().ToString());
            var password = contrasena;
            var sub = "Bienvenido a Document Security";

            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/Plantillas/TemplateRegistroUser.html")))
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

    }
}
