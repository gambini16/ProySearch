using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Entidades.Listado
{
    public class PerfilListadoEL
    {
        private int _PERFIL_ID;
        private string _PERFIL_NOMBRE;
        private string _DESCRIPCION;


        [DisplayName("Id Perfil")]
        public int PERFIL_ID { get; set; }

        [DisplayName("Nombre Perfil")]
        public string PERFIL_NOMBRE { get; set; }

        [DisplayName("Descripción")]
        public string DESCRIPCION { get; set; }
    }
}
