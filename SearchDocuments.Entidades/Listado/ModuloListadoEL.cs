using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Entidades.Listado
{
    public class ModuloListadoEL
    {
        private int _MODULO_ID;
        private string _MODULO_NOMBRE;
        private string _ESTADO_DESCRIPCION;
        private string _SIGLA;

        public int MODULO_ID { get; set; }
        public string MODULO_NOMBRE { get; set; }
        public string ESTADO_DESCRIPCION { get; set; }
        public string SIGLA { get; set; }
    }
}
