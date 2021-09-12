using SearchDocuments.Entidades;
using SearchDocuments.Entidades.Listado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Datos.Modulo
{
    public interface IModuloDAL
    {
        List<ModuloListadoEL> fn_Get_Modulo(ModuloEL objOpcionEL);

        ModuloEL fn_GetInfo_Modulo(ModuloEL objOpcionEL);

        string fn_Update_Modulo(ModuloEL objOpcionEL);

        string fn_Insert_Modulo(ModuloEL objOpcionEL);
    }
}
