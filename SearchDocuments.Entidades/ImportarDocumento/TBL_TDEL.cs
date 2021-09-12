using System.Collections.Generic;

namespace SearchDocuments.Entidades.ImportarDocumento
{
    public class TBL_TDEL
    {
        public bool IsNew { get; set; }
        public int TemplateId { get; set; }
        public int tocId { get; set; }
        public Dictionary<string, string> dictImportarDocumento { get; set; }
    }
}
