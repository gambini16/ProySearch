using SearchDocuments.Datos.Helpers;
using SearchDocuments.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Datos.Auditoria
{
    public class AuditoriaDAL : IAuditoriaDAL
    {
        public void fn_Insert_Auditoria(AuditoriaEL obj)
        {
            try
            {
                //Registrar nuevo cupons
                SqlParameter[] objParameter = new SqlParameter[8];
                objParameter[0] = new SqlParameter("@VC_DIREC_IP", obj.VC_DIREC_IP);
                objParameter[1] = new SqlParameter("@VC_NOM_HOST", obj.VC_NOM_HOST);
                objParameter[2] = new SqlParameter("@VC_NOM_NAVEGADOR", obj.VC_NOM_NAVEGADOR);
                objParameter[3] = new SqlParameter("@VC_NOM_FORM", obj.VC_NOM_FORM);
                objParameter[4] = new SqlParameter("@VC_DESC_EVENTO", obj.VC_DESC_EVENTO);
                objParameter[5] = new SqlParameter("@VC_DESC_OPERA", obj.VC_DESC_OPERA);
                objParameter[6] = new SqlParameter("@VC_NOM_USUARIO", obj.VC_NOM_USUARIO);
                objParameter[7] = new SqlParameter("@IN_COD_USUARIO", obj.IN_COD_USUARIO);

                SqlHelper.ExecuteNonQuery("usp_Tbl_Ta_Auditoria_Registro", objParameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void fn_Insert_Auditoria_DS(Tbl_audit_events obj)
        {
            try
            {
                SqlParameter[] objParameter = new SqlParameter[7];
                objParameter[0] = new SqlParameter("@IdUsuario", obj.IdUsuario);
                objParameter[1] = new SqlParameter("@HostName", obj.HostName);
                objParameter[2] = new SqlParameter("@Ip_Address", obj.Ip_Address);
                objParameter[3] = new SqlParameter("@Event", obj.Event);
                if(obj.TocId==null)
                    objParameter[4] = new SqlParameter("@TocId", DBNull.Value);
                else
                    objParameter[4] = new SqlParameter("@TocId", obj.TocId);

                objParameter[5] = new SqlParameter("@Name_Toc", obj.Name_Toc);
                objParameter[6] = new SqlParameter("@UserName_Domain", obj.UserName_Domain);
                

                SqlHelper.ExecuteNonQuery("usp_Tbl_audit_events_Insert", objParameter);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
