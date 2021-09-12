using System;

namespace SearchDocuments.Entidades.ImportarDocumento
{
    public class TBL_TOCEL
    {
        public bool IsNew { get; set; }
        public int ParentId { get; set; }
        public string Name { get; set; }
        public int ElType { get; set; }
        public int IsIndexed { get; set; }
        public int VolumeId { get; set; }
        public int TemplateId { get; set; }
        public int PageCount { get; set; }
        public string Creator { get; set; }
        public string Ext_File { get; set; }
        public int TocId { get; set; }
        public DateTime LastModified { get; set; }
        public DateTime CreateDate { get; set; }
        public int Toc_Flags { get; set; }
        public string Toc_Owner { get; set; }
        public string Toc_Comment { get; set; }
    }
}
