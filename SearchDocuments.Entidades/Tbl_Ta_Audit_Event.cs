using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Entidades
{
    public class Tbl_Ta_Audit_Event
    {
        public Int32 IN_CODIGO_AUD { get; set; }
        public Int32 IN_CODIGO_FILE { get; set; }
        public String VC_DESCRIP_1 { get; set; }
        public Int32 IN_CODIGO_OPCION { get; set; }
        public Int32 IN_USUREG_AUD { get; set; }
        public DateTime DT_FECREG_AUD { get; set; }
        public String VC_DIREC_IP { get; set; }
        public String VC_NOM_HOST { get; set; }

        public Tbl_Ta_Audit_Event() { }

        public Tbl_Ta_Audit_Event(
            Int32 _IN_CODIGO_AUD,
            Int32 _IN_CODIGO_FILE,
            String _VC_DESCRIP_1,
            Int32 _IN_CODIGO_OPCION,
            Int32 _IN_USUREG_AUD,
            DateTime _DT_FECREG_AUD,
            String _VC_DIREC_IP, String _VC_NOM_HOST
          )
        {
            IN_CODIGO_AUD = _IN_CODIGO_AUD;
            IN_CODIGO_FILE = _IN_CODIGO_FILE;
            VC_DESCRIP_1 = _VC_DESCRIP_1;
            IN_CODIGO_OPCION = _IN_CODIGO_OPCION;
            IN_USUREG_AUD = _IN_USUREG_AUD;
            DT_FECREG_AUD = _DT_FECREG_AUD;
            VC_DIREC_IP = _VC_DIREC_IP;
            VC_NOM_HOST = _VC_NOM_HOST;
        }
    }
}
