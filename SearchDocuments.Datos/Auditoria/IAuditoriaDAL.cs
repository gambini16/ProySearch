using SearchDocuments.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Datos.Auditoria
{
    public interface IAuditoriaDAL
    {
        void fn_Insert_Auditoria(AuditoriaEL obj);
        void fn_Insert_Auditoria_DS(Tbl_audit_events obj);
    }
}
