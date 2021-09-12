using SearchDocuments.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Negocio.SeguridadSistema
{
    public interface ISeguridadBL
    {
        Tbl_users ObtenerNombreUsuario(string strNombreUsuario);

        string ActualizarIntentosUsuario(int intCodigoUsuario, int intNumeroIntentos);

        string InsertarDatosAuditoria(Tbl_Ta_Audit_Event entity);

        void Update_CH_ESTADO_USU(int intCodigoUsuario, string strEstado);

        Tbl_users ObtenerCodigoUsuario(int intCodUser);

        /*Agregado por José Palomino*/
        List<OpcionEL> obtenerOpcionesPerfil(int perfilId);
    }
}
