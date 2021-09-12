using SearchDocuments.Datos.Filtro;
using SearchDocuments.Entidades.Listado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Negocio.Filtro
{
    public class FiltroBL:IFiltroBL
    {
        private readonly IFiltroDAL _objFiltroDAL;

        #region Constructores

        public FiltroBL()
        {
            _objFiltroDAL = new FiltroDAL();
        }
        public FiltroBL(IFiltroDAL ObjFiltroDAL)
        {
            _objFiltroDAL = ObjFiltroDAL;
        }
        #endregion

        public List<FiltroListadoEL> fn_Get_Campos(string codTipoDocumento)
        {
            return _objFiltroDAL.fn_Get_Campos(codTipoDocumento);
        }

        public List<FiltroListadoEL> fn_Get_Valores_Select_List(List<FiltroListadoEL> lstFiltro)
        {
            return _objFiltroDAL.fn_Get_Valores_Select_List(lstFiltro);
        }
    }
}
