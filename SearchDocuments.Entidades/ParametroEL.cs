using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Entidades
{
    public class ParametroEL
    {
        private string _CODIGO_VALOR;
        private string _GRUPO_PARAMETRO;
        private string _DESCRIPCION;
        private string _VALOR;

        public string CODIGO_VALOR { get; set; }
        public string GRUPO_PARAMETRO { get; set; }
        public string DESCRIPCION { get; set; }
        public string VALOR { get; set; }
    }
}
