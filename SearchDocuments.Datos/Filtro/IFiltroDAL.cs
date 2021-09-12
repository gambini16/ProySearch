using SearchDocuments.Entidades.Listado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Datos.Filtro
{
    public interface IFiltroDAL
    {
        List<FiltroListadoEL> fn_Get_Campos(string codTipoDocumento);

        List<FiltroListadoEL> fn_Get_Valores_Select_List(List<FiltroListadoEL> lstFiltro);
    }
}
