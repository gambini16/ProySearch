using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Entidades.ImportarDocumento
{
    public class TBL_VOLEL
    {
        public int VolumeId { get; set; }
        public string VolumeName { get; set; }
        public string Vol_descrip { get; set; }
        public string FixedPath { get; set; }
        public string FixedPathShare { get; set; }
        public int MaxSize { get; set; }
        public int CurSize { get; set; }
        public int CurDiskSize { get; set; }
        public int Vol_Flag { get; set; }
        public DateTime Vol_Created { get; set; }
        public int Encrypt_alg { get; set; }
    }
}
