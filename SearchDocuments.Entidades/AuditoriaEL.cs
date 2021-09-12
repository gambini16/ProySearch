using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Entidades
{
    public class AuditoriaEL
    {
        private Int64 _IN_CODIGO_AUD;
        private String _VC_DIREC_IP;
        private String _VC_NOM_HOST;
        private String _VC_NOM_NAVEGADOR;
        private String _VC_NOM_FORM;
        private String _VC_DESC_EVENTO;
        private String _VC_DESC_OPERA;
        private String _VC_NOM_USUARIO;
        private Int32 _IN_COD_USUARIO;
        private DateTime _DT_FECREG_AUD;


        public Int64 IN_CODIGO_AUD
        {
            get { return _IN_CODIGO_AUD; }
            set { _IN_CODIGO_AUD = value; }
        }

        public String VC_DIREC_IP
        {
            get { return _VC_DIREC_IP; }
            set { _VC_DIREC_IP = value; }
        }

        public String VC_NOM_HOST
        {
            get { return _VC_NOM_HOST; }
            set { _VC_NOM_HOST = value; }
        }

        public String VC_NOM_NAVEGADOR
        {
            get { return _VC_NOM_NAVEGADOR; }
            set { _VC_NOM_NAVEGADOR = value; }
        }

        public String VC_NOM_FORM
        {
            get { return _VC_NOM_FORM; }
            set { _VC_NOM_FORM = value; }
        }

        public String VC_DESC_EVENTO
        {
            get { return _VC_DESC_EVENTO; }
            set { _VC_DESC_EVENTO = value; }
        }

        public String VC_DESC_OPERA
        {
            get { return _VC_DESC_OPERA; }
            set { _VC_DESC_OPERA = value; }
        }

        public String VC_NOM_USUARIO
        {
            get { return _VC_NOM_USUARIO; }
            set { _VC_NOM_USUARIO = value; }
        }

        public Int32 IN_COD_USUARIO
        {
            get { return _IN_COD_USUARIO; }
            set { _IN_COD_USUARIO = value; }
        }

        public DateTime DT_FECREG_AUD
        {
            get { return _DT_FECREG_AUD; }
            set { _DT_FECREG_AUD = value; }
        }
    }
}
