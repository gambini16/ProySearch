using SearchDocuments.Entidades.Listado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Negocio.Filtro
{
    public interface IFiltroBL
    {
        List<FiltroListadoEL> fn_Get_Campos(string codTipoDocumento);

        List<FiltroListadoEL> fn_Get_Valores_Select_List(List<FiltroListadoEL> lstFiltro);
    }
}
