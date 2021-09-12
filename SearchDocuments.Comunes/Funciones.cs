using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
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

        public static int CountPagePdf(string strRuta)
        {
            int intCant = 0;
            try
            {
                if (File.Exists(strRuta))
                {
                    PdfReader pdfReader = new PdfReader(strRuta);
                    intCant = pdfReader.NumberOfPages;
                    pdfReader.Close();
                    pdfReader = null;
                }
            }
            catch (Exception)
            {
                intCant = 0;
            }
            return intCant;
        }


    }
}
