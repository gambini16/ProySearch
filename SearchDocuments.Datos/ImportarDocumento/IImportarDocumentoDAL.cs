using SearchDocuments.Entidades.ImportarDocumento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Datos.ImportarDocumento
{
    public interface IImportarDocumentoDAL
    {
        string fn_Insert_Tbl_toc(TBL_TOCEL objTblToc);
        string fn_Update_Tbl_toc(TBL_TOCEL objTblToc);
        string fn_Insert_Tbl_doc(TBL_DOCEL objTblDoc);
        string fn_Update_Tbl_doc(TBL_DOCEL objTblDoc);
        string fn_Update_Tbl_Td(TBL_TDEL objTBL_TDEL);
        string fn_Insert_tbl_td(TBL_TDEL objTBL_TDEL);
        TBL_VOLEL fn_Get_Vol_Get_VolumeId(int VolumeId);

    }
}
