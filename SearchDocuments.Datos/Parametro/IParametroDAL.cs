using SearchDocuments.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Datos.Parametro
{
    public interface IParametroDAL
    {
        List<ParametroEL> fn_Get_Roles(string strNombre);

        List<ParametroEL> fn_Get_Plantillas();

        List<ParametroEL> fn_Get_PlantillasPorPerfil(int codigoPerfil);
    }
}
