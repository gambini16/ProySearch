using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Entidades.Listado
{
    public class UsuarioListadoEL
    {
        private int _USUARIO_ID;
        private string _LOGIN;
        private string _NOMBRE_COMPLETO;
        private string _ESTADO_DESCRIPCION;
        private string _ROL_DESCRIPCION;

        [DisplayName("Id")]
        public int USUARIO_ID { get; set; }

        [DisplayName("Login")]
        public string LOGIN { get; set; }

        [DisplayName("Nombre Completo")]
        public string NOMBRE_COMPLETO { get; set; }

        [DisplayName("Estado")]
        public string ESTADO_DESCRIPCION { get; set; }

        [DisplayName("Perfil")]
        public string ROL_DESCRIPCION { get; set; }
    }
}
