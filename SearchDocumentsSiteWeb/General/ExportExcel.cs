using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SearchDocumentsSiteWeb.General
{
    public class ExportExcel
    {
        public static void ExportarExcel<T>(string strTitulo, IEnumerable<T> data, TextWriter output)
        {
            //Writes markup characters and text to an ASP.NET server control output stream. This class provides formatting capabilities that ASP.NET server controls use when rendering markup to clients.
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
                    //  Create a form to contain the List
                    Table table = new Table();
                    TableRow row = new TableRow();
                    /*FILAS EN BLANCO*/
                    row = new TableRow();
                    table.Rows.Add(row);
                    row = new TableRow();
                    table.Rows.Add(row);
                    /*CABECERA*/

                    TableHeaderCell hcell2 = new TableHeaderCell();
                    //hcell2.Text = string.Format("<img src='{0}' width='130' height='37' />", KeysWebConfig.URLLogoSD.Value);
                    //row.Cells.Add(hcell2);
                    hcell2 = new TableHeaderCell();
                    hcell2.Text = strTitulo;
                    hcell2.Font.Size = 18;
                    hcell2.Font.Bold = true;
                    //hcell2.ColumnSpan = props.Count - 1;
                    hcell2.ColumnSpan = props.Count;
                    row.Cells.Add(hcell2);
                    table.Rows.Add(row);
                    /*DATOS DE USUARIO*/
                    row = new TableRow();
                    TableHeaderCell hcell3 = new TableHeaderCell();
                    hcell3.Text = "Usuario: " + SesionActual.Current.USUARIO_LOGIN;
                    hcell3.HorizontalAlign = HorizontalAlign.Left;
                    hcell3.Font.Bold = false;
                    hcell3.ColumnSpan = props.Count;
                    row.Cells.Add(hcell3);
                    table.Rows.Add(row);
                    /*DATOS DE FECHA*/
                    row = new TableRow();
                    hcell3 = new TableHeaderCell();
                    hcell3.Text = "Fecha: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                    hcell3.HorizontalAlign = HorizontalAlign.Left;
                    hcell3.Font.Bold = false;
                    hcell3.ColumnSpan = props.Count;
                    row.Cells.Add(hcell3);
                    table.Rows.Add(row);
                    /*FILAS EN BLANCO*/
                    row = new TableRow();
                    table.Rows.Add(row);

                    foreach (PropertyDescriptor prop in props)
                    {
                        TableHeaderCell hcell = new TableHeaderCell();
                        hcell.Text = prop.DisplayName;
                        hcell.BackColor = System.Drawing.Color.FromArgb(224, 220, 220);
                        hcell.Font.Bold = true;
                        row.Cells.Add(hcell);
                    }

                    table.Rows.Add(row);
                    foreach (T item in data)
                    {
                        row = new TableRow();
                        foreach (PropertyDescriptor prop in props)
                        {
                            TableCell cell = new TableCell();
                            cell.Text = prop.Converter.ConvertToString(prop.GetValue(item));
                            row.Cells.Add(cell);
                        }
                        table.Rows.Add(row);
                    }
                    table.Attributes.Add("border", "1");
                    table.RenderControl(htw);
                    output.Write(sw.ToString());
                }
            }

        }

        public static void ExportarExcel<T>(string strTitulo, IEnumerable<T> data, TextWriter output, string ruta)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(typeof(T));
                    Table table = new Table();
                    TableRow row = new TableRow();
                    /*FILAS EN BLANCO*/
                    row = new TableRow();
                    table.Rows.Add(row);
                    row = new TableRow();
                    table.Rows.Add(row);
                    /*CABECERA*/
                    TableHeaderCell hcell2 = new TableHeaderCell();
                    //hcell2.Text = string.Format("<img src='{0}' width='130' height='37' />", ruta + KeysWebConfig.URLLogoSD.Value);
                    //row.Cells.Add(hcell2);
                    hcell2 = new TableHeaderCell();
                    hcell2.Text = strTitulo;
                    hcell2.Font.Size = 18;
                    hcell2.Font.Bold = true;
                    hcell2.ColumnSpan = props.Count - 1;
                    row.Cells.Add(hcell2);
                    table.Rows.Add(row);
                    /*DATOS DE USUARIO*/
                    row = new TableRow();
                    TableHeaderCell hcell3 = new TableHeaderCell();
                    hcell3.Text = "Usuario: " + SesionActual.Current.USUARIO_LOGIN;
                    hcell3.HorizontalAlign = HorizontalAlign.Left;
                    hcell3.Font.Bold = false;
                    hcell3.ColumnSpan = props.Count;
                    row.Cells.Add(hcell3);
                    table.Rows.Add(row);
                    /*DATOS DE FECHA*/
                    row = new TableRow();
                    hcell3 = new TableHeaderCell();
                    hcell3.Text = "Fecha: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
                    hcell3.HorizontalAlign = HorizontalAlign.Left;
                    hcell3.Font.Bold = false;
                    hcell3.ColumnSpan = props.Count;
                    row.Cells.Add(hcell3);
                    table.Rows.Add(row);
                    /*FILAS EN BLANCO*/
                    row = new TableRow();
                    table.Rows.Add(row);

                    foreach (PropertyDescriptor prop in props)
                    {
                        TableHeaderCell hcell = new TableHeaderCell();
                        hcell.Text = prop.DisplayName;
                        hcell.BackColor = System.Drawing.Color.FromArgb(224, 220, 220);
                        hcell.Font.Bold = true;
                        row.Cells.Add(hcell);
                    }
                    table.Rows.Add(row);
                    foreach (T item in data)
                    {
                        row = new TableRow();
                        foreach (PropertyDescriptor prop in props)
                        {
                            TableCell cell = new TableCell();
                            cell.Text = prop.Converter.ConvertToString(prop.GetValue(item));
                            row.Cells.Add(cell);
                        }
                        table.Rows.Add(row);
                    }
                    table.Attributes.Add("border", "1");
                    table.RenderControl(htw);
                    output.Write(sw.ToString());
                }
            }

        }
    }
}