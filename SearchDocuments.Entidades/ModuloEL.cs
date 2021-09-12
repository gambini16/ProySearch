using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Entidades
{
    public class ModuloEL
    {
        private int _MODULO_ID;
        private string _MODULO_NOMBRE;
        private string _SIGLA;
        private int _ESTADO;
        private string _USUARIO_CREACION;

        [Display(Name = "Id")]
        public int MODULO_ID { get; set; }

        [Display(Name = "Nombre Módulo")]
        [Required(ErrorMessage = "El nombre del módulo es obligatorio.")]
        public string MODULO_NOMBRE { get; set; }

        [Display(Name = "Sigla")]
        [Required(ErrorMessage = "La Sigla es obligatoria.")]
        public string SIGLA { get; set; }

        [Display(Name = "Estado")]
        public int ESTADO { get; set; }
        public string USUARIO_CREACION { get; set; }
    }
}
