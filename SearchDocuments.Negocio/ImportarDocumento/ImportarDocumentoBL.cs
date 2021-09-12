using SearchDocuments.Datos.ImportarDocumento;
using SearchDocuments.Entidades.ImportarDocumento;

namespace SearchDocuments.Negocio.ImportarDocumento
{
    public class ImportarDocumentoBL : IImportarDocumentoBL
    {
        private readonly IImportarDocumentoDAL _objImportarDocumentoDAL;

        public ImportarDocumentoBL()
        {
            _objImportarDocumentoDAL = new ImportarDocumentoDAL();
        }

        public ImportarDocumentoBL(IImportarDocumentoDAL objImportarDocumentoDAL)
        {
            _objImportarDocumentoDAL = objImportarDocumentoDAL;
        }

        public string fn_Insert_Update_Tbl_toc(TBL_TOCEL objTblToc)
        {
            if (objTblToc.IsNew)
            {
                return _objImportarDocumentoDAL.fn_Insert_Tbl_toc(objTblToc);
            }
            else
            {
                return _objImportarDocumentoDAL.fn_Update_Tbl_toc(objTblToc);
            }
        }

        public string fn_Insert_Update_Tbl_doc(TBL_DOCEL objTblDoc)
        {
            if (objTblDoc.IsNew) {
                return _objImportarDocumentoDAL.fn_Insert_Tbl_doc(objTblDoc);
            }
            else
            {
                return _objImportarDocumentoDAL.fn_Update_Tbl_doc(objTblDoc);
            }
            
        }

        public TBL_VOLEL fn_Get_Vol_Get_VolumeId(int VolumeId)
        {
            return _objImportarDocumentoDAL.fn_Get_Vol_Get_VolumeId(VolumeId);
        }

        public string fn_Insert_Update_tbl_td(TBL_TDEL objTBL_TDEL)
        {
            if (objTBL_TDEL.IsNew)
            {
                return _objImportarDocumentoDAL.fn_Insert_tbl_td(objTBL_TDEL);
            }
            else
            {
                return _objImportarDocumentoDAL.fn_Update_Tbl_Td(objTBL_TDEL);
            }
        }
    }
}
