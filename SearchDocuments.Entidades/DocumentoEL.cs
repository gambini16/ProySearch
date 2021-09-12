using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Entidades
{
    public class DocumentoEL
    {
        private string _TIPO_DOCUMENTO;
        private string _TABLA;
        private string _CARPETA;
        private string _NOMBRE_DOCUMENTO;
        public string TIPO_DOCUMENTO { get; set; }
        public string TABLA { get; set; }
        public string CARPETA { get; set; }
        public string NOMBRE_DOCUMENTO { get; set; }
    }
}
