using SearchDocuments.Entidades.ImportarDocumento;
using System.Collections.Generic;

namespace SearchDocuments.Negocio.ImportarDocumento
{
    public interface IImportarDocumentoBL
    {
        string fn_Insert_Update_Tbl_toc(TBL_TOCEL objTblToc);
        string fn_Insert_Update_Tbl_doc(TBL_DOCEL objTblDoc);
        TBL_VOLEL fn_Get_Vol_Get_VolumeId(int VolumeId);
        string fn_Insert_Update_tbl_td(TBL_TDEL objTBL_TDEL);
    }
}
