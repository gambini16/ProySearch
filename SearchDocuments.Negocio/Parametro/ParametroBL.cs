using SearchDocuments.Datos.Parametro;
using SearchDocuments.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Negocio.Parametro
{
    public class ParametroBL : IParametroBL
    {
        private readonly IParametroDAL _objParametroDAL;

        #region Constructores

        public ParametroBL()
        {
            _objParametroDAL = new ParametroDAL();
        }
        public ParametroBL(IParametroDAL ObjParametroDAL)
        {
            _objParametroDAL = ObjParametroDAL;
        }
        #endregion

        public List<ParametroEL> fn_Get_Roles(string strNombre)
        {
            return _objParametroDAL.fn_Get_Roles(strNombre);
        }

        public List<ParametroEL> fn_Get_Plantillas()
        {
            return _objParametroDAL.fn_Get_Plantillas();
        }

        public List<ParametroEL> fn_Get_PlantillasPorPerfil(int codigoPerfil)
        {
            return _objParametroDAL.fn_Get_PlantillasPorPerfil(codigoPerfil);
        }

    }
}
