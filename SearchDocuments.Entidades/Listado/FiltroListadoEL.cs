using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Entidades.Listado
{
    public class FiltroListadoEL
    {
        private string _CODIGO;
        private string _POSICION;
        private string _NOMBRE_CAMPO;
        private string _TIPO_DATO;
        private string _LONGITUD_CAMPO;
        private string _DATO_COLUMNA;
        private string _TIPO_CONTROL;
        private List<ValorControlEL> _CONTROL;
        public string NOMBRE_CAMPO { get; set; }
        public string CODIGO { get; set; }
        public string TIPO_DATO { get; set; }
        public string LONGITUD_CAMPO { get; set; }
        public string DATO_COLUMNA { get; set; }
        public string POSICION { get; set; }
        public string TIPO_CONTROL { get; set; }
        public List<ValorControlEL> CONTROL { get; set; }
    }
}
