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
    public interface IDocumentoBL
    {
        DocumentoEL fn_GetTabla_Plantilla(DocumentoEL objDocumentoEL);

        List<TD1ListadoEL> ObtenerListadoTD1(TD1EL oTD1);

        List<TD1ListadoEL> ObtenerListadoTD2(TD2EL oTD2);

        List<TD1ListadoEL> ObtenerListadoTD3(TD3EL oTD3);

        List<TD1ListadoEL> ObtenerListadoTD5(TD5EL oTD5);

        List<TD1ListadoEL> ObtenerListadoTD6(TD6EL oTD6);

        List<TD1ListadoEL> ObtenerListadoTD7(TD7EL oTD7);

        List<TD1ListadoEL> ObtenerListadoTD8(TD8EL oTD8);

        DocumentoEL ObtenerInformacionDocumento(int codigoDocumento);

        DataTable ObtenerListadoDinamico(string selects, string where, string nombreTabla);

        DocumentoEL ObtenerInfoDoc(int intTocid, int intFlag, int intOpcion);

        DataTable ObtenerDatoTablaTd(int templateId, int tocId);
    }
}
