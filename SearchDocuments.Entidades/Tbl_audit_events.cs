using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Entidades
{
    public class Tbl_audit_events
    {
        public Int64 IdAudit { get; set; }
        public Int32? IdUsuario { get; set; }
        public DateTime? CreateDate { get; set; }
        public String HostName { get; set; }
        public String Ip_Address { get; set; }
        public String Event { get; set; }
        public Int32? TocId { get; set; }
        public String Name_Toc { get; set; }
        public String UserName_Domain { get; set; }
        public Tbl_audit_events() { }
        public Tbl_audit_events(Int64 _IdAudit, Int32 _IdUsuario, DateTime _CreateDate, String _HostName, String _Ip_Address, String _Event, Int32 _TocId, String _Name_Toc, String _UserName_Domain)
        {
            IdAudit = _IdAudit;
            IdUsuario = _IdUsuario;
            CreateDate = _CreateDate;
            HostName = _HostName;
            Ip_Address = _Ip_Address;
            Event = _Event;
            TocId = _TocId;
            Name_Toc = _Name_Toc;
            UserName_Domain = _UserName_Domain;
        }
    }
}
