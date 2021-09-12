using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Entidades
{
    public class PerfilEL
    {
        private int _PERFIL_ID;
        private string _PERFIL_NOMBRE;
        private string _DESCRIPCION;
        private string _USUARIO_CREACION;

        [Display(Name = "Id")]
        public int PERFIL_ID { get; set; }

        [Display(Name = "Nombre Perfil")]
        [StringLength(50)]
        [Required(ErrorMessage = "El campo nombre Perfil es obligatorio.")]
        public string PERFIL_NOMBRE { get; set; }

        [Display(Name = "Descripción")]
        public string DESCRIPCION { get; set; }

        public string USUARIO_CREACION { get; set; }

    }
}
