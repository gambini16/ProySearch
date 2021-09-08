using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Comunes
{
    public class Documentos_Log
    {
        public static void Log_WriteLog(String strTexto, String strController)
        {
            log4net.ILog log = LogManager.GetLogger(strController);
            log.Info(strTexto);
        }
    }
}
