using SearchDocuments.Entidades;
using SearchDocuments.Negocio.SeguridadSistema;
using SearchDocumentsSiteWeb.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CaptchaMvc.HtmlHelpers;
using Newtonsoft.Json;
using SearchDocuments.Criptografia;

namespace SearchDocumentsSiteWeb.Controllers
{
    public class LoginController : Controller
    {
        #region "Refrescar Cache"
        public void RefrescarCache()
        {
            ViewData["LOGUEO_NOMBRE_COMPLETO"] = SesionActual.Current.NOMBRE_COMPLETO;
            ViewData["LOGUEO_PERFIL"] = SesionActual.Current.PERFIL;
            ViewData["OPCIONES_USUARIO"] = SesionActual.Current.OPCIONES_USUARIO;
            ViewData["NOMBRE_SISTEMA"] = SesionActual.Current.APLICACION;
            ViewData["AVATAR"] = SesionActual.Current.AVATAR;
            ViewData["NOMBRE_SOCIEDAD"] = SesionActual.Current.SOCIEDAD_NOMBRE;
            ViewData["LOGUEO_PERFIL_NAME"] = SesionActual.Current.PERFIL_NOMBRE;
            ViewData["USER_ID"] = SesionActual.Current.IN_CODIGO_USU;

            Response.Cache.AppendCacheExtension("no-store, must-revalidate");
            Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
            Response.AppendHeader("Expires", "0"); // Proxies.
        }
        #endregion

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult LoginEstandar()
        {
            int intRegAudit = Util.RegistrarAuditoria("Login", "Load", "Ingreso al sistema", "", 0);
            RefrescarCache();            
            var usuario = new UsuarioActualEL();
            ViewBag.CountFail = 0;
            ViewBag.ErrMessage = "Validation Messgae";
            return View("LoginEstandar", usuario);
        }

        [HttpPost]
        public ActionResult LoginEstandar(UsuarioActualEL users, FormCollection Form)
        {
            string strRespuesta = string.Empty;
            string strUsuario = users.LOGIN_USUARIO;
            int intContador = 0;
            int intMaxFailLogin = Convert.ToInt32(ConfigurationManager.AppSettings["MaxFailLogin"]);
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("LoginEstandar", users);
                }
                else
                {
                    ISeguridadBL objBL = new SeguridadBL();
                    Tbl_users objUsuario = new Tbl_users();
                    objUsuario = objBL.ObtenerNombreUsuario(strUsuario);

                    if (objUsuario != null)
                    {
                        switch (objUsuario.CH_ESTADO_USU)
                        {
                            case "1":
                                ViewBag.ErrorMessage = "El usuario :" + objUsuario.VC_USUARIO_USU + " se encuentra inactivo.";
                                intContador = 0;
                                ViewBag.CountFail = intContador;
                                break;
                            case "0"://Usuario activo
                                //string strClaveEncriptada = Encriptador.Encriptar(users.LOGIN_PASSWORD);

                                string strPwd = Cryptography.Decrypt(objUsuario.VC_PWD_USU, objUsuario.IN_CODIGO_USU.ToString());
                                if (strPwd == users.LOGIN_PASSWORD)
                                {
                                    SesionActual.Current.IN_CODIGO_PRF = objUsuario.IN_CODIGO_PRF;
                                    SesionActual.Current.IN_CODIGO_USU = objUsuario.IN_CODIGO_USU;

                                    SesionActual.Current.VC_DATAENCRY = EncryptionHelper.Encrypt(objUsuario.IN_CODIGO_USU.ToString());

                                    SesionActual.Current.NOMBRE_COMPLETO = string.Format("{0} {1}", objUsuario.VC_NOMBRE_USU, objUsuario.VC_APELLIDO_USU);
                                    SesionActual.Current.PERFIL = objUsuario.IN_CODIGO_PRF.ToString();
                                    SesionActual.Current.OPCIONES_USUARIO = "";
                                    SesionActual.Current.AVATAR = "";
                                    SesionActual.Current.SOCIEDAD_NOMBRE = "";
                                    SesionActual.Current.PERFIL_NOMBRE = objUsuario.NOM_PERFIL;

                                    List<OpcionEL> lstOpciones = objBL.obtenerOpcionesPerfil(objUsuario.IN_CODIGO_PRF);
                                    var json = JsonConvert.SerializeObject(lstOpciones);
                                    SesionActual.Current.OPCIONES_USUARIO = json;
                                    ViewData["OPCIONES_USUARIO"] = SesionActual.Current.OPCIONES_USUARIO;
                                    /************************************/
                                    Util.RegistrarAuditoria("Login", "Click", "Ingreso al sistema Clave Correcta", objUsuario.VC_USUARIO_USU, objUsuario.IN_CODIGO_USU);
                                    return RedirectToAction("Index", "Home");
                                }
                                else
                                {
                                    users.CONTADOR_INTENTOS++;
                                    ViewBag.ErrorMessage = "La contraseña ingresada es incorrecta.";
                                    intContador = Convert.ToInt32(Form["hidCountFail"]);
                                    intContador++;
                                    ViewBag.CountFail = intContador;

                                    Util.RegistrarAuditoria("Login", "Click", "Ingreso al sistema Clave Incorrecta", objUsuario.VC_USUARIO_USU, objUsuario.IN_CODIGO_USU);


                                    if (intContador == intMaxFailLogin)
                                    {
                                        ViewBag.CountFail = 0;
                                        if (objUsuario.IN_CODIGO_USU != 1)
                                        {
                                            this.bloquear_usuario(objUsuario.IN_CODIGO_USU);
                                        }
                                    }
                                    //return Json(new { strRespuesta = 2 }, JsonRequestBehavior.AllowGet);//Credenciales incorrectas
                                }
                                break;
                            default:
                                ViewBag.ErrorMessage = "El usuario ingresado es incorrecto.";
                                intContador = 0;
                                ViewBag.CountFail = intContador;
                                break;
                        }
                    }
                    else
                    {
                        intContador = 0;
                        ViewBag.CountFail = intContador;
                        ViewBag.ErrorMessage = "El usuario ingresado es incorrecto.";
                        Util.RegistrarAuditoria("Login", "Click", "El usuario ingresado es incorrecto.", users.LOGIN_USUARIO, 0);
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            return View("LoginEstandar", users);
        }

        private void bloquear_usuario(int codUser)
        {
            ISeguridadBL objBL = new SeguridadBL();
            Tbl_users objUsuario = new Tbl_users();
            objBL.Update_CH_ESTADO_USU(codUser, "2");
        }

        [HttpPost]
        public ActionResult UsuarioAutenticacion(string strLogin, string strPwd)
        {
            return null;

        }
    }
}
