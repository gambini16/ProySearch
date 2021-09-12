using SearchDocuments.Datos.Perfil;
using SearchDocuments.Entidades;
using SearchDocuments.Entidades.Listado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Negocio.Perfil
{
    public class PerfilBL : IPerfilBL
    {
        private readonly IPerfilDAL _objPerfilDAL;

        #region Constructores
        public PerfilBL()
        {
            _objPerfilDAL = new PerfilDAL();
        }
        public PerfilBL(IPerfilDAL ObjPerfilDAL)
        {
            _objPerfilDAL = ObjPerfilDAL;
        }
        #endregion

        public List<PerfilListadoEL> fn_Get_Perfil(PerfilEL objPerfilEL)
        {
            return _objPerfilDAL.fn_Get_Perfil(objPerfilEL);
        }

        public List<PerfilListadoEL> fn_Get_Groups_All()
        {
            return _objPerfilDAL.fn_Get_Groups_All();
        }

        public PerfilEL fn_GetInfo_Perfil(PerfilEL objRolEL)
        {
            return _objPerfilDAL.fn_GetInfo_Perfil(objRolEL);
        }

        public string fn_Update_Perfil(PerfilEL objRolEL)
        {
            return _objPerfilDAL.fn_Update_Perfil(objRolEL);
        }

        public string fn_Insert_Perfil(PerfilEL objRolEL)
        {
            return _objPerfilDAL.fn_Insert_Perfil(objRolEL);
        }

        public List<ModuloEL> fn_Get_Perfil_Modulo_Actual(int intTipoConsulta, PerfilEL objRolEL)
        {
            return _objPerfilDAL.fn_Get_Perfil_Modulo_Actual(intTipoConsulta, objRolEL);
        }

        public string fn_Insert_PerfilModulo(List<ModuloEL> lstOpciones, PerfilEL objRolEL)
        {
            return _objPerfilDAL.fn_Insert_PerfilModulo(lstOpciones, objRolEL);
        }

        public List<PlantillaEL> fn_Get_Plantilla_Perfil(int intTipoConsulta, PerfilEL objRolEL)
        {
            return _objPerfilDAL.fn_Get_Plantilla_Perfil(intTipoConsulta, objRolEL);
        }

        public string fn_Insert_PerfilPlantilla(List<PlantillaEL> lstOpciones, PerfilEL objRolEL)
        {
            return _objPerfilDAL.fn_Insert_PerfilPlantilla(lstOpciones, objRolEL);
        }
    }
}
