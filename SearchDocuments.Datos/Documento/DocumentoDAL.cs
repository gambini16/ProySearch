using SearchDocuments.Datos.Helpers;
using SearchDocuments.Entidades;
using SearchDocuments.Entidades.Listado;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Datos.Documento
{
    public class DocumentoDAL:IDocumentoDAL
    {
        public DocumentoEL fn_GetTabla_Plantilla(DocumentoEL objDocumentoEL)
        {
            try
            {
                DocumentoEL documento = null;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[1];
                objParameter[0] = new SqlParameter("@TemplateId", objDocumentoEL.TIPO_DOCUMENTO);
                SqlHelper.Fill(dt, "usp_Tbl_tstr_Get_TemplateId", objParameter);
                foreach (DataRow item in dt.Rows)
                {
                    documento = new DocumentoEL();
                    documento.TABLA = item["TableName"].ToString();
                }
                return documento;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<TD1ListadoEL> ObtenerListadoTD1(TD1EL oTD1)
        {
            try
            {
                List<TD1ListadoEL> lstListado = new List<TD1ListadoEL>();
                TD1ListadoEL lista;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[9];
                objParameter[0] = new SqlParameter("@Campo01", oTD1.CAMPO01);
                objParameter[1] = new SqlParameter("@Campo02", oTD1.CAMPO02);
                objParameter[2] = new SqlParameter("@Campo03", oTD1.CAMPO03);
                objParameter[3] = new SqlParameter("@Campo04", oTD1.CAMPO04);
                objParameter[4] = new SqlParameter("@Campo05", oTD1.CAMPO05);
                objParameter[5] = new SqlParameter("@Campo06", oTD1.CAMPO06);
                objParameter[6] = new SqlParameter("@Campo07", oTD1.CAMPO07);
                objParameter[7] = new SqlParameter("@Campo08", oTD1.CAMPO08);
                objParameter[8] = new SqlParameter("@Campo09", oTD1.CAMPO09);
                SqlHelper.Fill(dt, "usp_td1_get_filters", objParameter);

                foreach (DataRow item in dt.Rows)
                {
                    lista = new TD1ListadoEL();
                    lista.TIPO_TABLA = "TD1";
                    lista.TOC_ID = Convert.ToInt32(item[0].ToString());
                    lista.NOMBRE = item[10].ToString();
                    lista.VOLUMEN = item[11].ToString();
                    if (item[12].ToString().ToUpper().Equals("FALSE"))
                    {
                        lista.INDEXADO = "No";
                    }
                    else
                    {
                        lista.INDEXADO = "Si";
                    }
                    lista.CAMPO01 = item[1].ToString();
                    lista.CAMPO02 = item[2].ToString();
                    lista.CAMPO03 = item[3].ToString();
                    lista.CAMPO04 = item[4].ToString();
                    lista.CAMPO05 = item[5].ToString();
                    lista.CAMPO06 = item[6].ToString();
                    lista.CAMPO07 = item[7].ToString();
                    lista.CAMPO08 = item[8].ToString();
                    lista.CAMPO09 = item[9].ToString();
                    lista.PAGINAS = item[13].ToString();
                    lstListado.Add(lista);
                }
                return lstListado;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<TD1ListadoEL> ObtenerListadoTD2(TD2EL oTD2)
        {
            try
            {
                List<TD1ListadoEL> lstListado = new List<TD1ListadoEL>();
                TD1ListadoEL lista;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[3];
                objParameter[0] = new SqlParameter("@NroExpediente", oTD2.NROEXPEDIENTE);
                objParameter[1] = new SqlParameter("@FechaDoc", oTD2.FECHADOC);
                objParameter[2] = new SqlParameter("@DNI", oTD2.DNI);
                SqlHelper.Fill(dt, "usp_td2_get_filters", objParameter);

                foreach (DataRow item in dt.Rows)
                {
                    lista = new TD1ListadoEL();
                    lista.TIPO_TABLA = "TD2";
                    lista.TOC_ID = Convert.ToInt32(item[0].ToString());
                    lista.NOMBRE = item[4].ToString();
                    lista.VOLUMEN = item[5].ToString();
                    lista.INDEXADO = item[6].ToString();
                    lista.NRO_EXPEDIENTE = item[1].ToString();
                    lista.FECHADOC = item[2].ToString();
                    lista.DNI = item[3].ToString();
                    lista.PAGINAS = item[7].ToString();
                    lstListado.Add(lista);
                }
                return lstListado;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<TD1ListadoEL> ObtenerListadoTD3(TD3EL oTD3)
        {
            try
            {
                List<TD1ListadoEL> lstListado = new List<TD1ListadoEL>();
                TD1ListadoEL lista;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[5];
                objParameter[0] = new SqlParameter("@DNI", oTD3.DNI);
                objParameter[1] = new SqlParameter("@Paterno", oTD3.PATERNO);
                objParameter[2] = new SqlParameter("@Materno", oTD3.MATERNO);
                objParameter[3] = new SqlParameter("@Nombres", oTD3.NOMBRES);
                objParameter[4] = new SqlParameter("@Sexo", oTD3.SEXO);
                SqlHelper.Fill(dt, "usp_td3_get_filters", objParameter);

                foreach (DataRow item in dt.Rows)
                {
                    lista = new TD1ListadoEL();
                    lista.TIPO_TABLA = "TD3";
                    lista.TOC_ID = Convert.ToInt32(item[0].ToString());
                    lista.NOMBRE = item[6].ToString();
                    lista.VOLUMEN = item[7].ToString();
                    lista.INDEXADO = item[8].ToString();
                    lista.DNI = item[1].ToString();
                    lista.PATERNO = item[2].ToString();
                    lista.MATERNO = item[3].ToString();
                    lista.NOMBRES = item[4].ToString();
                    lista.SEXO = item[5].ToString();
                    lista.PAGINAS = item[9].ToString();
                    lstListado.Add(lista);
                }
                return lstListado;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<TD1ListadoEL> ObtenerListadoTD5(TD5EL oTD5)
        {
            try
            {
                List<TD1ListadoEL> lstListado = new List<TD1ListadoEL>();
                TD1ListadoEL lista;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[1];
                objParameter[0] = new SqlParameter("@Indice01", oTD5.INDICE_01);
                SqlHelper.Fill(dt, "usp_td5_get_filters", objParameter);

                foreach (DataRow item in dt.Rows)
                {
                    lista = new TD1ListadoEL();
                    lista.TIPO_TABLA = "TD5";
                    lista.TOC_ID = Convert.ToInt32(item[0].ToString());
                    lista.NOMBRE = item[2].ToString();
                    lista.VOLUMEN = item[3].ToString();
                    lista.INDEXADO = item[4].ToString();
                    lista.INDICE_01 = item[1].ToString();
                    lista.PAGINAS = item[5].ToString();
                    lstListado.Add(lista);
                }
                return lstListado;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<TD1ListadoEL> ObtenerListadoTD6(TD6EL oTD6)
        {
            try
            {
                List<TD1ListadoEL> lstListado = new List<TD1ListadoEL>();
                TD1ListadoEL lista;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[4];
                objParameter[0] = new SqlParameter("@IdZarpe", oTD6.ID_ZARPE);
                objParameter[1] = new SqlParameter("@Puerto", oTD6.PUERTO);
                objParameter[2] = new SqlParameter("@Puerto2", oTD6.PUERTO2);
                objParameter[3] = new SqlParameter("@Matricula", oTD6.MATRICULA);
                SqlHelper.Fill(dt, "usp_td6_get_filters", objParameter);

                foreach (DataRow item in dt.Rows)
                {
                    lista = new TD1ListadoEL();
                    lista.TIPO_TABLA = "TD6";
                    lista.TOC_ID = Convert.ToInt32(item[0].ToString());
                    lista.NOMBRE = item[5].ToString();
                    lista.VOLUMEN = item[6].ToString();
                    lista.INDEXADO = item[7].ToString();
                    lista.PAGINAS = item[8].ToString();
                    lista.ID_ZARPE = item[1].ToString();
                    lista.PUERTO = item[2].ToString();
                    lista.PUERTO2 = item[3].ToString();
                    lista.MATRICULA = item[4].ToString();
                    lstListado.Add(lista);
                }
                return lstListado;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<TD1ListadoEL> ObtenerListadoTD7(TD7EL oTD7)
        {
            try
            {
                List<TD1ListadoEL> lstListado = new List<TD1ListadoEL>();
                TD1ListadoEL lista;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[3];
                objParameter[0] = new SqlParameter("@RUC", oTD7.RUC);
                objParameter[1] = new SqlParameter("@Matricula", oTD7.MATRICULA);
                objParameter[2] = new SqlParameter("@DNI", oTD7.DNI);
                SqlHelper.Fill(dt, "usp_td7_get_filters", objParameter);

                foreach (DataRow item in dt.Rows)
                {
                    lista = new TD1ListadoEL();
                    lista.TIPO_TABLA = "TD7";
                    lista.TOC_ID = Convert.ToInt32(item[0].ToString());
                    lista.NOMBRE = item[4].ToString();
                    lista.VOLUMEN = item[5].ToString();
                    lista.INDEXADO = item[6].ToString();
                    lista.PAGINAS = item[7].ToString();
                    lista.DNI = item[1].ToString();
                    lista.MATRICULA = item[2].ToString();
                    lista.RUC = item[3].ToString();
                    lstListado.Add(lista);
                }
                return lstListado;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<TD1ListadoEL> ObtenerListadoTD8(TD8EL oTD8)
        {
            try
            {
                List<TD1ListadoEL> lstListado = new List<TD1ListadoEL>();
                TD1ListadoEL lista;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[15];
                objParameter[0] = new SqlParameter("@CAMPO_01", oTD8.CAMPO01);
                objParameter[1] = new SqlParameter("@CAMPO_02", oTD8.CAMPO02);
                objParameter[2] = new SqlParameter("@CAMPO_03", oTD8.CAMPO03);
                objParameter[3] = new SqlParameter("@CAMPO_04", oTD8.CAMPO04);
                objParameter[4] = new SqlParameter("@CAMPO_05", oTD8.CAMPO05);
                objParameter[5] = new SqlParameter("@CAMPO_06", oTD8.CAMPO06);
                objParameter[6] = new SqlParameter("@CAMPO_07", oTD8.CAMPO07);
                objParameter[7] = new SqlParameter("@CAMPO_08", oTD8.CAMPO08);
                objParameter[8] = new SqlParameter("@CAMPO_09", oTD8.CAMPO09);
                objParameter[9] = new SqlParameter("@CAMPO_10", oTD8.CAMPO10);
                objParameter[10] = new SqlParameter("@CAMPO_11", oTD8.CAMPO11);
                objParameter[11] = new SqlParameter("@CAMPO_12", oTD8.CAMPO12);
                objParameter[12] = new SqlParameter("@CAMPO_13", oTD8.CAMPO13);
                objParameter[13] = new SqlParameter("@CAMPO_14", oTD8.CAMPO14);
                objParameter[14] = new SqlParameter("@CAMPO_15", oTD8.CAMPO15);
                SqlHelper.Fill(dt, "usp_td8_get_filters", objParameter);

                foreach (DataRow item in dt.Rows)
                {
                    lista = new TD1ListadoEL();
                    lista.TIPO_TABLA = "TD8";
                    lista.TOC_ID = Convert.ToInt32(item[0].ToString());
                    lista.NOMBRE = item[16].ToString();
                    lista.VOLUMEN = item[17].ToString();
                    lista.INDEXADO = item[18].ToString();
                    lista.PAGINAS = item[19].ToString();
                    lista.CAMPO01 = item[1].ToString();
                    lista.CAMPO02 = item[2].ToString();
                    lista.CAMPO03 = item[3].ToString();
                    lista.CAMPO04 = item[4].ToString();
                    lista.CAMPO05 = item[5].ToString();
                    lista.CAMPO06 = item[6].ToString();
                    lista.CAMPO07 = item[7].ToString();
                    lista.CAMPO08 = item[8].ToString();
                    lista.CAMPO09 = item[9].ToString();
                    lista.CAMPO10 = item[10].ToString();
                    lista.CAMPO11 = item[11].ToString();
                    lista.CAMPO12 = item[12].ToString();
                    lista.CAMPO13 = item[13].ToString();
                    lista.CAMPO14 = item[14].ToString();
                    lista.CAMPO15 = item[15].ToString();
                    lstListado.Add(lista);
                }
                return lstListado;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DocumentoEL ObtenerInformacionDocumento(int codigoDocumento)
        {
            try
            {
                DocumentoEL lista = new DocumentoEL(); ;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[1];
                objParameter[0] = new SqlParameter("@TocId", codigoDocumento);
                SqlHelper.Fill(dt, "usp_Tbl_doc_Get_Doc", objParameter);

                foreach (DataRow item in dt.Rows)
                {
                    lista = new DocumentoEL();
                    lista.CARPETA = item[16].ToString();
                    lista.NOMBRE_DOCUMENTO = item[1].ToString().PadLeft(8,'0') + ".pdf";
                }
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DocumentoEL ObtenerInfoDoc(int intTocid,int intFlag,int intOpcion)
        {
            string strNameSp = "";

            try
            {
                if (intOpcion == 1)
                    strNameSp = "usp_Tbl_doc_FilterList_TocId";
                else
                    strNameSp = "usp_Tbl_doc_FilterList_TocId_2";


                DocumentoEL lista = new DocumentoEL(); ;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[2];
                objParameter[0] = new SqlParameter("@TocId", intTocid);
                objParameter[1] = new SqlParameter("@page_flags", intFlag);

                SqlHelper.Fill(dt, strNameSp, objParameter);

                foreach (DataRow item in dt.Rows)
                {
                    lista = new DocumentoEL();
                    lista.CARPETA = item[16].ToString();
                    lista.NOMBRE_DOCUMENTO = item["FullPath"].ToString();
                }
                return lista;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable ObtenerListadoDinamico(string selects,string where, string nombreTabla)
        {
            try
            {
                string sentencia = "";
                sentencia = sentencia + "SELECT  t.TocId AS Id,";
                sentencia = sentencia + selects;
                //sentencia = sentencia + "tc.Name,v.VolumeName,tc.IsIndexed,ISNULL(tc.PageCount,0) AS PAGINAS ";
                sentencia = sentencia + "ISNULL(tc.PageCount,0) AS Cant_Pag ";
                sentencia = sentencia + " FROM  dbo." + nombreTabla+ " t ";
                sentencia = sentencia + " INNER JOIN tbl_toc tc on tc.TocId=t.TocId and tc.Toc_Flags=1 ";
                sentencia = sentencia + " INNER JOIN tbl_vol v on v.VolumeId= tc.VolumeId ";
                sentencia = sentencia + where;

                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[1];
                objParameter[0] = new SqlParameter("@sentencia", sentencia);
                SqlHelper.Fill(dt, "usp_dinamic_sentence", objParameter);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable ObtenerDatoTablaTd(int templateId, int tocId)
        {
            try
            {
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[2];
                objParameter[0] = new SqlParameter("@TemplateId", templateId);
                objParameter[1] = new SqlParameter("@TocId", tocId);
                SqlHelper.Fill(dt, "usp_tbl_td_templateId_tocId", objParameter);
                return dt;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
