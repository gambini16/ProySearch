using SearchDocuments.Datos.Auditoria;
using SearchDocuments.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Negocio.Auditoria
{
    public class AuditoriaBL : IAuditoriaBL
    {
        private readonly IAuditoriaDAL _objAuditoriaDAL;
        #region Constructores

        public AuditoriaBL()
        {
            _objAuditoriaDAL = new AuditoriaDAL();
        }
        public AuditoriaBL(IAuditoriaDAL ObjAuditoriaDAL)
        {
            _objAuditoriaDAL = ObjAuditoriaDAL;
        }
        #endregion

        public void fn_Insert_Auditoria(AuditoriaEL obj)
        {
            _objAuditoriaDAL.fn_Insert_Auditoria(obj);
        }

        public void fn_Insert_Auditoria_DS(Tbl_audit_events obj)
        {
            _objAuditoriaDAL.fn_Insert_Auditoria_DS(obj);
        }


    }
}
