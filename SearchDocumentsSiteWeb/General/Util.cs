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
    }
}