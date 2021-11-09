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

        static public string CheckStr(object value)
        {
            string salida = "";
            if (value == null || value == System.DBNull.Value)
                salida = "";
            else
                salida = value.ToString();
            return salida.Trim();
        }
        static public string CheckStr2(object value)
        {
            string salida = "";
            if (value == null || value == System.DBNull.Value)
                salida = "";
            else
                salida = value.ToString();
            return salida;
        }

        static public Int64 CheckInt64(object value)
        {
            Int64 salida = 0;
            if (value == null || value == System.DBNull.Value)
            {
                salida = 0;
            }
            else
            {
                if (Convert.ToString(value) == "")
                    salida = 0;
                else
                    salida = Convert.ToInt64(value);
            }
            return salida;
        }

        static public float CheckFloat(object value)
        {
            float salida = 0;
            if (value == null || value == System.DBNull.Value)
            {
                salida = 0;
            }
            else
            {
                if (Convert.ToString(value) == "")
                    salida = 0;
                else
                    salida = Convert.ToInt64(value);//.ToString("#,##0.00");
            }
            return salida;
        }


        static public int CheckInt(object value)
        {
            int salida = 0;
            if (value == null || value == System.DBNull.Value)
            {
                salida = 0;
            }
            else
            {
                if (Convert.ToString(value) == "" || Convert.ToString(value) == "&nbsp;" || Convert.ToString(value) == "&nbsp")
                    salida = 0;
                else
                    salida = Convert.ToInt32(value);
            }
            return salida;
        }

        static public double CheckDbl(object value)
        {
            double salida = 0;
            if (value == null || value == System.DBNull.Value)
            {
                salida = 0;
            }
            else
            {
                if (Convert.ToString(value) == "" || Convert.ToString(value) == "&nbsp;" || Convert.ToString(value) == "&nbsp")
                    salida = 0;
                else
                    salida = Convert.ToDouble(value);
            }
            return salida;
        }

        static public double CheckDbl(object value, int nroDecimales)
        {
            double salida = CheckDbl(value);
            if (salida == 0) return salida;
            return redondearMontos(salida, nroDecimales);
        }

        static public decimal CheckDecimal(object value)
        {
            decimal salida = 0;
            if (value == null || value == System.DBNull.Value)
            {
                salida = 0;
            }
            else
            {
                if (Convert.ToString(value) == "" || Convert.ToString(value) == "&nbsp;" || Convert.ToString(value) == "&nbsp")
                    salida = 0;
                else
                    salida = Convert.ToDecimal(value);
            }
            return salida;
        }

        static public double redondearMontos(double value, int nroDecimales)
        {
            return Math.Round(value, nroDecimales);
        }

        static public DateTime CheckDate(object value)
        {
            if (value == null || value == System.DBNull.Value)
                return new DateTime(1, 1, 1);

            if (value.ToString() == "")
                return new DateTime(1, 1, 1);

            if (value.ToString() == "00000000")
                return new DateTime(1, 1, 1);

            return Convert.ToDateTime(value);
        }

        static public  string FormatBytes(long bytes)
        {
            string[] Suffix = { "B", "KB", "MB", "GB", "TB" };
            int i;
            double dblSByte = bytes;
            for (i = 0; i < Suffix.Length && bytes >= 1024; i++, bytes /= 1024)
            {
                dblSByte = bytes / 1024.0;
            }

            return String.Format("{0:0.##} {1}", dblSByte, Suffix[i]);
        }
    }
}
