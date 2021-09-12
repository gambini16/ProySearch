using SearchDocuments.Datos.Modulo;
using SearchDocuments.Entidades;
using SearchDocuments.Entidades.Listado;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Negocio.Modulo
{
    public class ModuloBL : IModuloBL
    {
        private readonly IModuloDAL _objModuloDAL;

        #region Constructores
        public ModuloBL()
        {
            _objModuloDAL = new ModuloDAL();
        }
        public ModuloBL(IModuloDAL ObjModuloDAL)
        {
            _objModuloDAL = ObjModuloDAL;
        }
        #endregion

        public List<ModuloListadoEL> fn_Get_Modulo(ModuloEL objOpcionEL)
        {
            return _objModuloDAL.fn_Get_Modulo(objOpcionEL);
        }

        public ModuloEL fn_GetInfo_Modulo(ModuloEL objOpcionEL)
        {
            return _objModuloDAL.fn_GetInfo_Modulo(objOpcionEL);
        }

        public string fn_Update_Modulo(ModuloEL objOpcionEL)
        {
            return _objModuloDAL.fn_Update_Modulo(objOpcionEL);
        }

        public string fn_Insert_Modulo(ModuloEL objOpcionEL)
        {
            return _objModuloDAL.fn_Insert_Modulo(objOpcionEL);
        }
    }
}
