using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Entidades
{
    public class UsuarioActualEL
    {
        private string _LOGIN_USUARIO;
        private string _LOGIN_PASSWORD;
        private int _USUARIO_ID;
        private int _ROL_ID;
        private string _ROL_NOMBRE;
        private string _APELLIDO_PATERNO;
        private string _APELLIDO_MATERNO;
        private string _NOMBRES;
        private string _AVATAR;
        private string _CAPTCHA;
        private int _TIPO_USUARIO;//ADD MGAMBINI

        [Required(ErrorMessage = "El campo Usuario es obligatorio.")]
        public string LOGIN_USUARIO { get; set; }
        [Required(ErrorMessage = "El campo Contraseña es obligatorio.")]
        public string LOGIN_PASSWORD { get; set; }
        public int USUARIO_ID { get; set; }
        public int ROL_ID { get; set; }
        public string ROL_NOMBRE { get; set; }
        public string APELLIDO_PATERNO { get; set; }
        public string APELLIDO_MATERNO { get; set; }
        public string NOMBRES { get; set; }
        public string AVATAR { get; set; }

        public string CAPTCHA { get; set; }

        public int TIPO_USUARIO { get; set; } //ADD MGGAMBINI

        public int CONTADOR_INTENTOS { get; set; }

    }
}
