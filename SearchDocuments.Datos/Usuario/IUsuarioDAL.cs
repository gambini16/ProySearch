using SearchDocuments.Entidades;
using SearchDocuments.Entidades.Listado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Datos.Usuario
{
    public interface IUsuarioDAL
    {
        List<UsuarioListadoEL> fn_Get_Usuario(UsuarioEL objUsuarioEL);

        List<UsuarioListadoEL> fn_Get_Usuario();

        UsuarioEL fn_GetInfo_Usuario(UsuarioEL objUsuarioEL);

        string fn_Update_Usuario(UsuarioEL objUsuarioEL);

        string fn_Update_UsuarioAvatar(UsuarioEL objUsuarioEL);

        string fn_Insert_Usuario(UsuarioEL objUsuarioEL);

        string fn_Update_Clave(UsuarioEL objUsuarioEL);
        string fn_Update_Clave(int intCodUser, string strPwd);

        string fn_Update_GenerarClave(UsuarioEL objUsuarioEL);

        UsuarioEL fn_GetInfo_UsuarioCorreo(UsuarioEL objUsuarioEL);
    }
}
