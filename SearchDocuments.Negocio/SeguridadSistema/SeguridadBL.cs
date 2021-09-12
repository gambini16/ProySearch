using SearchDocuments.Datos.SeguridadSistema;
using SearchDocuments.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Negocio.SeguridadSistema
{

    /*------------------------------Clase creada MGM----------------------*/
    public class SeguridadBL : ISeguridadBL
    {
        private readonly ISeguridadDAL _objSeguridadDAL;

        #region Constructores
        public SeguridadBL()
        {
            _objSeguridadDAL = new SeguridadDAL();
        }

        public SeguridadBL(ISeguridadDAL ObjSeguridadDAL)
        {
            _objSeguridadDAL = ObjSeguridadDAL;
        }
        #endregion

        public Tbl_users ObtenerNombreUsuario(string strNombreUsuario)
        {
            return _objSeguridadDAL.ObtenerNombreUsuario(strNombreUsuario);
        }


        public Tbl_users ObtenerCodigoUsuario(int intCodUser)
        {
            return _objSeguridadDAL.ObtenerCodigoUsuario(intCodUser);
        }

        public string ActualizarIntentosUsuario(int intCodigoUsuario, int intNumeroIntentos)
        {
            return _objSeguridadDAL.ActualizarIntentosUsuario(intCodigoUsuario, intNumeroIntentos);
        }

        public string InsertarDatosAuditoria(Tbl_Ta_Audit_Event entity)
        {
            return _objSeguridadDAL.InsertarDatosAuditoria(entity);
        }

        public void Update_CH_ESTADO_USU(int intCodigoUsuario, string strEstado)
        {
            _objSeguridadDAL.Update_CH_ESTADO_USU(intCodigoUsuario, strEstado);
        }

        /*Agregado por José Palomino*/
        public List<OpcionEL> obtenerOpcionesPerfil(int perfilId)
        {
            return _objSeguridadDAL.obtenerOpcionesPerfil(perfilId);
        }
    }
}
