using SearchDocuments.Entidades;
using SearchDocuments.Negocio.Auditoria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SearchDocumentsSiteWeb.General
{
    public class Util
    {
        public static int RegistrarAuditoria(String strNameForm, String strEvent, String strDesOpe, String strNomUsu, Int32 intCodUsu)
        {
            int intResultado = 0;

            try
            {
                AuditoriaEL objAuditoria = new AuditoriaEL();
                objAuditoria.VC_DIREC_IP = SearchDocumentsSiteWeb.General.Constantes.GetIPAddress();
                objAuditoria.VC_NOM_HOST = SearchDocumentsSiteWeb.General.Constantes.GetNameHost();
                objAuditoria.VC_NOM_NAVEGADOR = SearchDocumentsSiteWeb.General.Constantes.GetNameBrowser();
                objAuditoria.VC_NOM_FORM = strNameForm;
                objAuditoria.VC_DESC_EVENTO = strEvent;
                objAuditoria.VC_DESC_OPERA = strDesOpe;
                objAuditoria.VC_NOM_USUARIO = strNomUsu;
                objAuditoria.IN_COD_USUARIO = intCodUsu;

                IAuditoriaBL objBL = new AuditoriaBL();
                objBL.fn_Insert_Auditoria(objAuditoria);
            }
            catch (Exception)
            {
                intResultado = 1;
            }
            finally
            {

            }
            return intResultado;

        }
        public static int RegistrarAuditoriaDS(int? intTocId,string strNameToc,int? intIdUser,string strNameEvent)
        {
            int intResultado = 0;
            try
            {
                //Auditoria
                Tbl_audit_events oTbl_audit_events = new Tbl_audit_events();
                oTbl_audit_events.IdUsuario = intIdUser;
                oTbl_audit_events.HostName = SearchDocumentsSiteWeb.General.Constantes.GetNameHost();
                oTbl_audit_events.Ip_Address = SearchDocumentsSiteWeb.General.Constantes.GetIPAddress();
                oTbl_audit_events.Event = strNameEvent;
                oTbl_audit_events.TocId = intTocId;
                oTbl_audit_events.Name_Toc = strNameToc;
                oTbl_audit_events.UserName_Domain = SearchDocumentsSiteWeb.General.Constantes.GetNameBrowser();

                IAuditoriaBL objBL = new AuditoriaBL();
                objBL.fn_Insert_Auditoria_DS(oTbl_audit_events);


            }
            catch (Exception)
            {
                intResultado = 1;
            }
            return intResultado;
        }

        public enum EventName
        {
            [StringValue("Importar")]
            Importar = 1,
            [StringValue("Exportar")]
            Exportar = 2,
            [StringValue("Eliminar")]
            Eliminar = 3,
            [StringValue("Escanear")]
            Escanear = 4,
            [StringValue("Indexacion")]
            Indexacion = 5,
            [StringValue("Desindexar")]
            Desindexar = 6,
            [StringValue("VerImagen")]
            VerImagen = 7,
            [StringValue("ImagenActualizada")]
            ImagenActualizada = 8,
            [StringValue("Imprimir")]
            Imprimir = 9,
            [StringValue("IndexacionActualizado")]
            IndexacionActualizado = 10,
            [StringValue("OCR")]
            OCR = 11,
            [StringValue("CopiarPegar")]
            CopiarPegar = 12,
            [StringValue("EliminarPagina")]
            EliminarPagina = 13,
            [StringValue("RotarImagen")]
            RotarImagen = 14,
            [StringValue("InsertarImagen")]
            InsertarImagen = 15,
            [StringValue("EnviarCorreo")]
            EnviarCorreo = 16,
            [StringValue("BathUpdate")]
            BathUpdate = 17,
            [StringValue("ReemplazarImagen")]
            ReemplazarImagen = 18,
            [StringValue("EliminarFolderPermanente")]
            EliminarFolderPermanente = 19,
            [StringValue("EliminarArchivoPermanente")]
            EliminarArchivoPermanente = 20,
            [StringValue("RecuperarObjeto")]
            RecuperarObjeto = 21,
            [StringValue("LoginOk")]
            LoginOk = 22,
            [StringValue("LoginFailed")]
            LoginFailed = 23,
            [StringValue("ActualizarPlantilla")]
            ActualizarPlantilla = 24,
            [StringValue("CargaImgSisDoc")]
            CargaImgSisDoc = 25,
            [StringValue("RenombrarArchivo")]
            RenombrarArchivo = 26,
            [StringValue("FirmaDigital")]
            FirmaDigital = 27,
            [StringValue("AutoRun")]
            AutoRun = 28


        }
        private class StringValue : System.Attribute
        {
            private string _value;

            public StringValue(string value)
            {
                _value = value;
            }

            public string Value
            {
                get { return _value; }
            }

        }
    }
}