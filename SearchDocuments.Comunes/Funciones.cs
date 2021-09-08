using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchDocuments.Comunes
{
    public static class Funciones
    {
        public static string FirstCharToUpper(this Object objeto)
        {
            try
            {
                string input = objeto.ToString().ToLower() ?? throw new Exception();
                return input.First().ToString().ToUpper() + input.Substring(1);
            }
            catch { return string.Empty; }
        }
    }
}
