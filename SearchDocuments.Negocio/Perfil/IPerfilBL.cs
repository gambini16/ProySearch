using SearchDocuments.Entidades;
using SearchDocuments.Entidades.Listado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Negocio.Perfil
{
    public interface IPerfilBL
    {
        List<PerfilListadoEL> fn_Get_Perfil(PerfilEL objPerfilEL);

        List<PerfilListadoEL> fn_Get_Groups_All();

        PerfilEL fn_GetInfo_Perfil(PerfilEL objRolEL);

        string fn_Update_Perfil(PerfilEL objRolEL);

        string fn_Insert_Perfil(PerfilEL objRolEL);

        List<ModuloEL> fn_Get_Perfil_Modulo_Actual(int intTipoConsulta, PerfilEL objRolEL);

        string fn_Insert_PerfilModulo(List<ModuloEL> lstOpciones, PerfilEL objRolEL);

        List<PlantillaEL> fn_Get_Plantilla_Perfil(int intTipoConsulta, PerfilEL objRolEL);

        string fn_Insert_PerfilPlantilla(List<PlantillaEL> lstOpciones, PerfilEL objRolEL);
    }
}
