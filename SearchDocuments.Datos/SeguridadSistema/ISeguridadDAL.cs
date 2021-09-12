using SearchDocuments.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Datos.SeguridadSistema
{
    public interface ISeguridadDAL
    {
        Tbl_users ObtenerNombreUsuario(string strNombreUsuario);

        Tbl_users ObtenerCodigoUsuario(int intCodUser);

        string ActualizarIntentosUsuario(int intCodigoUsuario, int intNumeroIntentos);

        string InsertarDatosAuditoria(Tbl_Ta_Audit_Event entity);
     
        void Update_CH_ESTADO_USU(int intCodigoUsuario, string strEstado);

        List<OpcionEL> obtenerOpcionesPerfil(int perfilId);
    }
}
