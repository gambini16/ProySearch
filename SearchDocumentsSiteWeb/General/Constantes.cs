using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SearchDocumentsSiteWeb.General
{
    public class Constantes
    {
        public static string GetIPAddress()
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (!string.IsNullOrEmpty(ipAddress))
            {
                string[] addresses = ipAddress.Split(',');
                if (addresses.Length != 0)
                {
                    return addresses[0];
                }
            }
            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        public static string GetNameHost()
        {
            return System.Web.HttpContext.Current.Request.UserHostName;
        }

        public static string GetNameBrowser()
        {
            string browserDetails = string.Empty;
            System.Web.HttpBrowserCapabilities browser = HttpContext.Current.Request.Browser;
            return browser.Browser;
        }

        private Constantes(string value) { Value = value; }
        private Constantes(int value) { ValueInt = value; }
        public string Value { get; set; }
        public int ValueInt { get; set; }
        public static Constantes LISTA { get { return new Constantes("LISTA"); } }
        public static Constantes IDENTIFICADOR_APLICACION { get { return new Constantes(1); } }
        public static Constantes SUFIJO_USUARIOEXTERNO { get { return new Constantes(".EXT"); } }
    }

    public class ListaValores
    {
        private ListaValores(string value) { Value = value; }
        public string Value { get; set; }
        public static ListaValores TIPO_MODELO { get { return new ListaValores("TIPO_MODELO"); } }
        public static ListaValores ESTADO_USUARIO { get { return new ListaValores("ESTADO_USUARIO"); } }
        public static ListaValores ESTADO_REGISTRO { get { return new ListaValores("ESTADO_REGISTRO"); } }
        public static ListaValores TIPO_DOCUMENTO { get { return new ListaValores("1"); } }
        public static ListaValores CONDICIONAL { get { return new ListaValores("011"); } }
        public static ListaValores MENU { get { return new ListaValores("017"); } }
        public static ListaValores TIPO_LOGUEO { get { return new ListaValores("014"); } }
        public static ListaValores CANTIDAD_INTENTOS_DEFECTO { get { return new ListaValores("032"); } }
        public static ListaValores MAIL_FORMATO { get { return new ListaValores("033"); } }
        public static ListaValores MAIL_VALORES { get { return new ListaValores("039"); } }
        public static ListaValores VALORES_SISTEMA { get { return new ListaValores("050"); } }
        public static ListaValores VALORES_SEGURIDAD { get { return new ListaValores("031"); } }
        public static ListaValores DURACION_SESION { get { return new ListaValores("051"); } }
        public static ListaValores CANTIDAD_FILAS_DATATABLE { get { return new ListaValores("052"); } }
        public static ListaValores LONGITUD_MINIMA_CLAVE { get { return new ListaValores("062"); } }
        public static ListaValores REGLAS_CLAVE { get { return new ListaValores("068"); } }
        public static ListaValores ESTADO_REGISTRADO { get { return new ListaValores("1"); } }
        public static ListaValores ESTADO_ASIGNADO { get { return new ListaValores("2"); } }
        public static ListaValores ESTADO_EN_ATENCION { get { return new ListaValores("3"); } }
        public static ListaValores ESTADO_ATENDIDO { get { return new ListaValores("4"); } }
        public static ListaValores ESTADO_CONFORME { get { return new ListaValores("5"); } }
    }
    public class MailParametros
    {
        private MailParametros(string value) { Value = value; }
        public string Value { get; set; }
        public static MailParametros HTML { get { return new MailParametros("034"); } }
        public static MailParametros HEADER { get { return new MailParametros("035"); } }
        public static MailParametros BODY { get { return new MailParametros("036"); } }
        public static MailParametros BODY2 { get { return new MailParametros("037"); } }
        public static MailParametros FOOTER { get { return new MailParametros("038"); } }
        public static MailParametros ASUNTO_CORREO { get { return new MailParametros("063"); } }
        public static MailParametros ASUNTO_CORREO_APLICACION { get { return new MailParametros("077"); } }
        public static MailParametros ASUNTO_CORREO_RESETEO { get { return new MailParametros("078"); } }
        public static MailParametros TITULO { get { return new MailParametros("040"); } }
        public static MailParametros SALUDO { get { return new MailParametros("041"); } }
        public static MailParametros MENSAJE_NUEVA_CLAVE { get { return new MailParametros("042"); } }
        public static MailParametros DATOS_PARTICULARES { get { return new MailParametros("043"); } }
        public static MailParametros MENSAJE_FINAL { get { return new MailParametros("044"); } }
        public static MailParametros PIE_MENSAJE { get { return new MailParametros("045"); } }
        public static MailParametros MENSAJE_NORESPONDER { get { return new MailParametros("064"); } }
        public static MailParametros MENSAJE_RESETEO_CLAVE { get { return new MailParametros("046"); } }
        public static MailParametros MENSAJE_APLICACION { get { return new MailParametros("065"); } }
        public static MailParametros DATOS_APLICACIONES { get { return new MailParametros("066"); } }
        public static MailParametros MENSAJE_FINAL_APLICACION_INTERNO { get { return new MailParametros("067"); } }
        public static MailParametros MENSAJE_FINAL_APLICACION_EXTERNO { get { return new MailParametros("079"); } }
    }

    public class CamposDDL
    {
        private CamposDDL(string value) { Value = value; }
        private CamposDDL(int value) { ValueInt = value; }
        public string Value { get; set; }
        public int ValueInt { get; set; }
        public static CamposDDL TODOS { get { return new CamposDDL("TODOS"); } }
        public static CamposDDL SELECCIONE { get { return new CamposDDL("SELECCIONE"); } }
        public static CamposDDL CERO { get { return new CamposDDL(0); } }
        public static CamposDDL CEROCAD { get { return new CamposDDL("0"); } }
        public static CamposDDL DESCRIPCION { get { return new CamposDDL("DESCRIPCION"); } }
        public static CamposDDL VALOR { get { return new CamposDDL("VALOR"); } }
        public static CamposDDL APLICACION_ID { get { return new CamposDDL("APLICACION_ID"); } }
        public static CamposDDL NOMBRE { get { return new CamposDDL("NOMBRE"); } }
        public static CamposDDL PERFIL_ID { get { return new CamposDDL("PERFIL_ID"); } }
        public static CamposDDL NOMBRE_PERFIL { get { return new CamposDDL("NOMBRE_PERFIL"); } }
    }

    public class TablasBD
    {
        private TablasBD(string value) { Value = value; }
        public string Value { get; set; }
        public static TablasBD USUARIO { get { return new TablasBD("USUARIO"); } }
        public static TablasBD APLICACION { get { return new TablasBD("APLICACION"); } }
        public static TablasBD APLICACION_VERSION { get { return new TablasBD("VERSION"); } }
        public static TablasBD OPCION { get { return new TablasBD("OPCION"); } }
        public static TablasBD PERFIL { get { return new TablasBD("PERFIL"); } }
        public static TablasBD LISTA_VALORES { get { return new TablasBD("LISTA_VALORES"); } }
    }
    public class CampoBD
    {
        private CampoBD(string value) { Value = value; }
        public string Value { get; set; }
        public static CampoBD USUARIO_ID { get { return new CampoBD("USUARIO_ID"); } }
        public static CampoBD APLICACION_ID { get { return new CampoBD("APLICACION_ID"); } }
        public static CampoBD VERSION_ID { get { return new CampoBD("VERSION_ID"); } }
        public static CampoBD OPCION_ID { get { return new CampoBD("OPCION_ID"); } }
        public static CampoBD PERFIL_ID { get { return new CampoBD("PERFIL_ID"); } }
        public static CampoBD CODIGO_VALOR { get { return new CampoBD("CODIGO_VALOR"); } }
    }
    public class TitulosCabeceraExcel
    {
        private TitulosCabeceraExcel(string value) { Value = value; }
        public string Value { get; set; }
        public static TitulosCabeceraExcel CAJA { get { return new TitulosCabeceraExcel("LISTA DE CAJAS"); } }
        public static TitulosCabeceraExcel CENTRO_COSTO { get { return new TitulosCabeceraExcel("LISTA DE CENTRO DE COSTOS"); } }
        public static TitulosCabeceraExcel CLIENTE { get { return new TitulosCabeceraExcel("LISTA DE CLIENTES"); } }
        public static TitulosCabeceraExcel DETALLE_CAJA { get { return new TitulosCabeceraExcel("LISTA DE DETALLE DE CAJAS"); } }
        public static TitulosCabeceraExcel MODULO { get { return new TitulosCabeceraExcel("LISTA DE MODULOS"); } }
        public static TitulosCabeceraExcel PERFIL { get { return new TitulosCabeceraExcel("LISTA DE PERFILES"); } }
        public static TitulosCabeceraExcel MODULO_PERFIL { get { return new TitulosCabeceraExcel("LISTA DE MODULOS POR PERFIL"); } }
        public static TitulosCabeceraExcel SOLICITUD { get { return new TitulosCabeceraExcel("LISTA DE SOLICITUDES"); } }
        public static TitulosCabeceraExcel TIPO_SOLICITUD { get { return new TitulosCabeceraExcel("LISTA DE TIPO DE SOLICITUDES"); } }
        public static TitulosCabeceraExcel USUARIO { get { return new TitulosCabeceraExcel("LISTA DE USUARIOS"); } }
        public static TitulosCabeceraExcel BITACORA_ERROR_APLICACIONES { get { return new TitulosCabeceraExcel("BITÁCORA DE ERROR DE APLICACIONES"); } }
        public static TitulosCabeceraExcel OPCIONES_PERFIL { get { return new TitulosCabeceraExcel("LISTA DE OPCIONES POR PERFIL"); } }
        public static TitulosCabeceraExcel PERFIL_USUARIO { get { return new TitulosCabeceraExcel("LISTA DE PERFILES POR USUARIO"); } }
    }

    public class KeysWebConfig
    {
        private KeysWebConfig(string value) { Value = ConfigurationManager.AppSettings[value].ToString(); }
        public string Value { get; set; }
        public static KeysWebConfig URLLogoSD { get { return new KeysWebConfig("URLLogoSD"); } }
        public static KeysWebConfig MailServer { get { return new KeysWebConfig("mailServer"); } }
        public static KeysWebConfig MailPort { get { return new KeysWebConfig("mailPort"); } }
        public static KeysWebConfig MailFrom { get { return new KeysWebConfig("mailFrom"); } }
        public static KeysWebConfig MailPass { get { return new KeysWebConfig("mailPass"); } }
    }

    public class ParametrosClave
    {
        private ParametrosClave(string value) { Value = value; }
        public string Value { get; set; }
        public static ParametrosClave CLAVE_NUNCA_EXPIRA { get { return new ParametrosClave("069"); } }
        public static ParametrosClave VALIDAR_LONGITUD_CLAVE { get { return new ParametrosClave("070"); } }
        public static ParametrosClave MANTENER_HISTORIAL { get { return new ParametrosClave("071"); } }
        public static ParametrosClave CARACTERES_CONSECUTIVOS { get { return new ParametrosClave("072"); } }
        public static ParametrosClave BLOQUEO_USUARIO { get { return new ParametrosClave("073"); } }
        public static ParametrosClave CAMBIO_INMEDIATAMENTE { get { return new ParametrosClave("074"); } }
        public static ParametrosClave VALIDAR_CLAVE_ALFANUMERICA { get { return new ParametrosClave("075"); } }
        public static ParametrosClave CLAVE_IGUAL_LOGIN { get { return new ParametrosClave("076"); } }
    }
}