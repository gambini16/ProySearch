using SearchDocuments.Datos.Usuario;
using SearchDocuments.Entidades;
using SearchDocuments.Entidades.Listado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Negocio.Usuario
{
    public class UsuarioBL : IUsuarioBL
    {
        private readonly IUsuarioDAL _objUsuarioDAL;

        #region Constructores

        public UsuarioBL()
        {
            _objUsuarioDAL = new UsuarioDAL();
        }
        public UsuarioBL(IUsuarioDAL ObjUsuarioDAL)
        {
            _objUsuarioDAL = ObjUsuarioDAL;
        }
        #endregion

        public List<UsuarioListadoEL> fn_Get_Usuario(UsuarioEL objUsuarioEL)
        {
            return _objUsuarioDAL.fn_Get_Usuario(objUsuarioEL);
        }

        public List<UsuarioListadoEL> fn_Get_Usuario()
        {
            return _objUsuarioDAL.fn_Get_Usuario();

        }

        public UsuarioEL fn_GetInfo_Usuario(UsuarioEL objUsuarioEL)
        {
            return _objUsuarioDAL.fn_GetInfo_Usuario(objUsuarioEL);
        }

        public string fn_Update_Usuario(UsuarioEL objUsuarioEL)
        {
            return _objUsuarioDAL.fn_Update_Usuario(objUsuarioEL);
        }

        public string fn_Update_Usuario(int intCodUser, string strPwd)
        {
            return _objUsuarioDAL.fn_Update_Clave(intCodUser, strPwd);
        }

        public string fn_Update_UsuarioAvatar(UsuarioEL objUsuarioEL)
        {
            return _objUsuarioDAL.fn_Update_UsuarioAvatar(objUsuarioEL);
        }

        public string fn_Insert_Usuario(UsuarioEL objUsuarioEL)
        {
            return _objUsuarioDAL.fn_Insert_Usuario(objUsuarioEL);
        }

        public string fn_Update_Clave(UsuarioEL objUsuarioEL)
        {
            return _objUsuarioDAL.fn_Update_Clave(objUsuarioEL);
        }

        public string fn_Update_GenerarClave(UsuarioEL objUsuarioEL)
        {
            return _objUsuarioDAL.fn_Update_GenerarClave(objUsuarioEL);
        }

        public UsuarioEL fn_GetInfo_UsuarioCorreo(UsuarioEL objUsuarioEL)
        {
            return _objUsuarioDAL.fn_GetInfo_UsuarioCorreo(objUsuarioEL);
        }
    }
}
