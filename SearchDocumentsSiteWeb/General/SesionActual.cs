using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SearchDocumentsSiteWeb.General
{
    public class SesionActual
    {
        private SesionActual()
        {
            //VALIDAR SI SE BORRA
            APLICACION_ID = 0;
            USUARIO_ID = 0;
            IP = "";
            BROWSER = "";
            LOG_SESION_ID = 0;
            NOMBRE_COMPLETO = "";
            USUARIO_LOGIN = "";
            OPCIONES_USUARIO = "";
            CANTIDAD_FILAS_DATATABLE = "0";
            LONGITUD_MINIMA_CLAVE = "0";
            TIPO_USUARIO = 0;
            PERFIL = "";
            APLICACION = "";
            AVATAR = "";
            SOCIEDAD_ID = 0;
            SOCIEDAD_NOMBRE = "";
            SOCIEDAD_CONNECTION = "";
            SOCIEDAD_LOGO = "";
            FORZAR_CLAVE = 0;
            //VALIDAR SI SE BORRA

            //ADD MGM-INICIO
            IN_CODIGO_USU = 0;
            VC_NOMBRE_USU = "";
            VC_APELLIDO_USU = "";
            VC_USUARIO_USU = "";
            IN_CODIGO_PRF = 0;
            CH_TIPO_USU = "";
            VC_EMAIL_USU = "";
            VC_DIRECCION_USU = "";
            IN_ESTA_VIEW = 0;
            IN_ESTA_EDIT = 0;
            IN_ESTA_ELI = 0;
            //ADD MGM-FIN

            EXCEL = null;

        }
        public static SesionActual Current
        {
            get
            {
                SesionActual session = (SesionActual)HttpContext.Current.Session["USUARIO_ACTUAL"];
                if (session == null)
                {
                    session = new SesionActual();
                    HttpContext.Current.Session["USUARIO_ACTUAL"] = session;
                }
                return session;
            }
        }
        //VALIDAR SI SE BORRA
        public int APLICACION_ID { get; set; }
        public int USUARIO_ID { get; set; }
        public string IP { get; set; }
        public string BROWSER { get; set; }
        public int LOG_SESION_ID { get; set; }
        public int SOCIEDAD_ID { get; set; }
        public string SOCIEDAD_NOMBRE { get; set; }
        public string SOCIEDAD_CONNECTION { get; set; }
        public string NOMBRE_COMPLETO { get; set; }

        public string VC_DATAENCRY { get; set; }
        public string USUARIO_LOGIN { get; set; }
        public string OPCIONES_USUARIO { get; set; }
        public string CANTIDAD_FILAS_DATATABLE { get; set; }
        public string LONGITUD_MINIMA_CLAVE { get; set; }
        public int TIPO_USUARIO { get; set; }
        public string PERFIL { get; set; }
        public string APLICACION { get; set; }
        public string AVATAR { get; set; }
        public string SOCIEDAD_LOGO { get; set; }
        public int FORZAR_CLAVE { get; set; }
        //VALIDAR SI SE BORRA
        public int IN_CODIGO_USU { get; set; }
        public string VC_NOMBRE_USU { get; set; }
        public string VC_APELLIDO_USU { get; set; }
        public string VC_USUARIO_USU { get; set; }
        public int IN_CODIGO_PRF { get; set; }
        public string CH_TIPO_USU { get; set; }
        public string VC_EMAIL_USU { get; set; }
        public string VC_DIRECCION_USU { get; set; }
        public int IN_ESTA_VIEW { get; set; }
        public int IN_ESTA_EDIT { get; set; }
        public int IN_ESTA_ELI { get; set; }

        public string PERFIL_NOMBRE { get; set; }

        public DataTable EXCEL { get; set; }
    }
}