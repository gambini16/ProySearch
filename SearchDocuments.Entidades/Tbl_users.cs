using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Entidades
{
    public class Tbl_users
    {
        public Int32 IN_CODIGO_USU { get; set; }
        public String VC_NOMBRE_USU { get; set; }
        public String VC_APELLIDO_USU { get; set; }
        public Int32 IN_CODIGO_PRF { get; set; }
        public String VC_EMAIL_USU { get; set; }
        public String VC_USUARIO_USU { get; set; }
        public String VC_CONTRASE_USU { get; set; }
        public String CH_INGNUE_USU { get; set; }
        public String CH_ESTADO_USU { get; set; }
        public DateTime? DT_FECREG_USU { get; set; }
        public Int32 IN_USUREG_USU { get; set; }
        public Nullable<DateTime> DT_FECACT_USU { get; set; }
        public Int32 IN_USUACT_USU { get; set; }
        public Int32 IN_INTENTOS_USU { get; set; }
        public String CH_USUGEN_USU { get; set; }
        public String VC_DIRECCION_USU { get; set; }
        public String CH_TIPO_USU { get; set; }

        public Int32 IN_ESTA_VIEW { get; set; }
        public Int32 IN_ESTA_EDIT { get; set; }
        public Int32 IN_ESTA_ELI { get; set; }

        public string VC_PWD_USU { get; set; }

        public string NOM_PERFIL { get; set; }


        public Tbl_users() { }
        public Tbl_users(Int32 _IN_CODIGO_USU, String _VC_NOMBRE_USU,
          String _VC_APELLIDO_USU, Int32 _IN_CODIGO_PRF,
          String _VC_EMAIL_USU, String _VC_USUARIO_USU, String _VC_CONTRASE_USU, String _CH_INGNUE_USU,
          String _CH_ESTADO_USU, DateTime _DT_FECREG_USU, Int32 _IN_USUREG_USU,
          DateTime _DT_FECACT_USU, Int32 _IN_USUACT_USU, Int32 _IN_INTENTOS_USU,
          String _CH_USUGEN_USU, String _VC_DIRECCION_USU, String _CH_TIPO_USU,
          Int32 _IN_ESTA_VIEW, Int32 _IN_ESTA_EDIT, Int32 _IN_ESTA_ELI, String _VC_PWD_USU, String _NOM_PERFIL
          )
        {
            IN_CODIGO_USU = _IN_CODIGO_USU;
            VC_NOMBRE_USU = _VC_NOMBRE_USU;
            VC_APELLIDO_USU = _VC_APELLIDO_USU;
            IN_CODIGO_PRF = _IN_CODIGO_PRF;
            VC_EMAIL_USU = _VC_EMAIL_USU;
            VC_USUARIO_USU = _VC_USUARIO_USU;
            VC_CONTRASE_USU = _VC_CONTRASE_USU;
            CH_INGNUE_USU = _CH_INGNUE_USU;
            CH_ESTADO_USU = _CH_ESTADO_USU;
            DT_FECREG_USU = _DT_FECREG_USU;
            IN_USUREG_USU = _IN_USUREG_USU;
            DT_FECACT_USU = _DT_FECACT_USU;
            IN_USUACT_USU = _IN_USUACT_USU;
            IN_INTENTOS_USU = _IN_INTENTOS_USU;
            CH_USUGEN_USU = _CH_USUGEN_USU;
            VC_DIRECCION_USU = _VC_DIRECCION_USU;
            CH_TIPO_USU = _CH_TIPO_USU;

            IN_ESTA_VIEW = _IN_ESTA_VIEW;
            IN_ESTA_EDIT = _IN_ESTA_EDIT;
            IN_ESTA_ELI = _IN_ESTA_ELI;
            VC_PWD_USU = _VC_PWD_USU;
            NOM_PERFIL = _NOM_PERFIL;

        }
    }
}
