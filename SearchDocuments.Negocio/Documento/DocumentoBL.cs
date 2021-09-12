using SearchDocuments.Datos.Documento;
using SearchDocuments.Entidades;
using SearchDocuments.Entidades.Listado;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Negocio.Documento
{
    public class DocumentoBL : IDocumentoBL
    {
        private readonly IDocumentoDAL _objDocumentoDAL;

        #region Constructores

        public DocumentoBL()
        {
            _objDocumentoDAL = new DocumentoDAL();
        }
        public DocumentoBL(IDocumentoDAL ObjDocumentoDAL)
        {
            _objDocumentoDAL = ObjDocumentoDAL;
        }
        #endregion

        public DocumentoEL fn_GetTabla_Plantilla(DocumentoEL objDocumentoEL)
        {
            return _objDocumentoDAL.fn_GetTabla_Plantilla(objDocumentoEL);
        }

        public List<TD1ListadoEL> ObtenerListadoTD1(TD1EL oTD1)
        {
            return _objDocumentoDAL.ObtenerListadoTD1(oTD1);
        }

        public List<TD1ListadoEL> ObtenerListadoTD2(TD2EL oTD2)
        {
            return _objDocumentoDAL.ObtenerListadoTD2(oTD2);
        }

        public List<TD1ListadoEL> ObtenerListadoTD3(TD3EL oTD3)
        {
            return _objDocumentoDAL.ObtenerListadoTD3(oTD3);
        }

        public List<TD1ListadoEL> ObtenerListadoTD5(TD5EL oTD5)
        {
            return _objDocumentoDAL.ObtenerListadoTD5(oTD5);
        }

        public List<TD1ListadoEL> ObtenerListadoTD6(TD6EL oTD6)
        {
            return _objDocumentoDAL.ObtenerListadoTD6(oTD6);
        }

        public List<TD1ListadoEL> ObtenerListadoTD7(TD7EL oTD7)
        {
            return _objDocumentoDAL.ObtenerListadoTD7(oTD7);
        }

        public List<TD1ListadoEL> ObtenerListadoTD8(TD8EL oTD8)
        {
            return _objDocumentoDAL.ObtenerListadoTD8(oTD8);
        }

        public DocumentoEL ObtenerInformacionDocumento(int codigoDocumento)
        {
            return _objDocumentoDAL.ObtenerInformacionDocumento(codigoDocumento);
        }

        public DataTable ObtenerListadoDinamico(string selects, string where, string nombreTabla)
        {
            return _objDocumentoDAL.ObtenerListadoDinamico(selects, where, nombreTabla);
        }
        public DocumentoEL ObtenerInfoDoc(int intTocid, int intFlag, int intOpcion)
        {
            return _objDocumentoDAL.ObtenerInfoDoc(intTocid, intFlag, intOpcion);
        }

        public DataTable ObtenerDatoTablaTd(int templateId, int tocId)
        {
            return _objDocumentoDAL.ObtenerDatoTablaTd(templateId, tocId);
        }
    }
}
