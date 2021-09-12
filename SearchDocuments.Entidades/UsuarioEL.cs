using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Entidades
{
    public class UsuarioEL
    {
        private int _USUARIO_ID;
        private string _LOGIN;
        private string _APELLIDO_PATERNO;
        private string _NOMBRES;
        private string _NOMBRE_COMPLETO;
        private int _ESTADO_CODIGO;
        private string _ESTADO_DESCRIPCION;
        private string _USUARIO_REGISTRO;
        private string _LOGIN_USUARIO;
        private string _VALIDAR_CONTRASENA;
        private int _ROL_CODIGO;
        private string _EMAIL;
        private string _CLAVE;
        private string _AVATAR;
        private int _IN_ESTA_VIEW;
        private int _IN_ESTA_EDIT;
        private int _IN_ESTA_ELI;

        [Display(Name = "ID")]
        public int USUARIO_ID { get; set; }

        [Display(Name = "Login")]
        [StringLength(50)]
        [Required(ErrorMessage = "El campo Login es obligatorio.")]
        public string LOGIN { get; set; }

        [Display(Name = "Apellido Paterno")]
        [StringLength(50)]
        [Required(ErrorMessage = "El campo Apellido es obligatorio.")]
        public string APELLIDO_PATERNO { get; set; }


        [Display(Name = "Nombres")]
        [StringLength(50)]
        [Required(ErrorMessage = "El campo Nombres es obligatorio.")]
        public string NOMBRES { get; set; }

        [Display(Name = "Nombre")]
        [StringLength(120)]
        public string NOMBRE_COMPLETO { get; set; }

        [Display(Name = "Celular")]
        [StringLength(20)]
        public string CELULAR { get; set; }

        [Display(Name = "Dirección")]
        [StringLength(200)]
        public string DIRECCION { get; set; }

        [Display(Name = "Estado")]
        public int ESTADO_CODIGO { get; set; }

        public string ESTADO_DESCRIPCION { get; set; }
        public string USUARIO_REGISTRO { get; set; }
        public string LOGIN_USUARIO { get; set; }

        [Display(Name = "Repetir Contraseña")]
        [Required(ErrorMessage = "El campo Repetir Contraseña es obligatorio.")]
        public string VALIDAR_CONTRASENA { get; set; }

        [Display(Name = "Perfil")]
        public int ROL_CODIGO { get; set; }

        [Display(Name = "Email")]
        [StringLength(100)]
        [Required(ErrorMessage = "El campo Email es obligatorio.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "El campo Email no tiene el formato correcto.")]
        public string EMAIL { get; set; }

        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo Clave es obligatorio.")]
        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "La contraseña debe tener al menos 8 caracteres y contener al menos: una letra mayúscula, una letra minúscula (a-z), un número (0-9) y un caracter especial (e.g. !@#$%^&*)")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        public string CLAVE { get; set; }


        [Display(Name = "Contraseña")]
        [Required(ErrorMessage = "El campo Clave es obligatorio.")]
        public string CLAVE_OLD { get; set; }

        [Display(Name = "Avatar")]
        public string AVATAR { get; set; }
        public int IN_ESTA_VIEW { get; set; }
        public int IN_ESTA_EDIT { get; set; }
        public int IN_ESTA_ELI { get; set; }
    }
}
