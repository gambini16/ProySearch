using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Entidades
{
    public class PlantillaEL
    {
        private int _PLANTILLA_ID;
        private string _PLANTILLA_NOMBRE;

        [Display(Name = "Id")]
        public int PLANTILLA_ID { get; set; }

        [Display(Name = "Nombre Plantilla")]
        [Required(ErrorMessage = "El nombre de la plantilla es obligatorio.")]
        public string PLANTILLA_NOMBRE { get; set; }

    }
}
