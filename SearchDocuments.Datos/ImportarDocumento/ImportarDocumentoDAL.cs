using SearchDocuments.Datos.Helpers;
using SearchDocuments.Entidades.ImportarDocumento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace SearchDocuments.Datos.ImportarDocumento
{
    public class ImportarDocumentoDAL : IImportarDocumentoDAL
    {
        public string fn_Insert_Tbl_toc(TBL_TOCEL objTblToc)
        {
            try
            {
                SqlParameter[] objParameter = new SqlParameter[9];
                objParameter[0] = new SqlParameter("@ParentId", objTblToc.ParentId);
                objParameter[1] = new SqlParameter("@Name", objTblToc.Name);
                objParameter[2] = new SqlParameter("@ElType", objTblToc.ElType);
                objParameter[3] = new SqlParameter("@IsIndexed", objTblToc.IsIndexed);
                objParameter[4] = new SqlParameter("@VolumeId", objTblToc.VolumeId);
                objParameter[5] = new SqlParameter("@TemplateId", objTblToc.TemplateId);
                objParameter[6] = new SqlParameter("@PageCount", objTblToc.PageCount);
                objParameter[7] = new SqlParameter("@Creator", objTblToc.Creator);
                objParameter[8] = new SqlParameter("@Ext_File", objTblToc.Ext_File);

                int resultado = Convert.ToInt32(SqlHelper.ExecuteScalar("usp_Tbl_toc_Insert", objParameter));

                return resultado.ToString();
            }
            catch (Exception ex)
            {
                return "NOOK";
            }
        }

        public string fn_Update_Tbl_toc(TBL_TOCEL objTblToc)
        {
            try
            {
                SqlParameter[] objParameter = new SqlParameter[14];
                objParameter[0] = new SqlParameter("@TocId", objTblToc.TocId);
                objParameter[1] = new SqlParameter("@ParentId", objTblToc.ParentId);
                objParameter[2] = new SqlParameter("@Name", objTblToc.Name);
                objParameter[3] = new SqlParameter("@ElType", objTblToc.ElType);
                objParameter[4] = new SqlParameter("@LastModified", objTblToc.LastModified);
                objParameter[5] = new SqlParameter("@CreateDate", objTblToc.CreateDate);
                objParameter[6] = new SqlParameter("@IsIndexed", objTblToc.IsIndexed);
                objParameter[7] = new SqlParameter("@VolumeId", objTblToc.VolumeId);
                objParameter[8] = new SqlParameter("@TemplateId", objTblToc.TemplateId);
                objParameter[9] = new SqlParameter("@PageCount", objTblToc.PageCount);
                objParameter[10] = new SqlParameter("@Creator", objTblToc.Creator);
                objParameter[11] = new SqlParameter("@Toc_Flags", objTblToc.Toc_Flags);
                objParameter[12] = new SqlParameter("@Toc_Owner", objTblToc.Toc_Owner);
                objParameter[13] = new SqlParameter("@Toc_Comment", objTblToc.Toc_Comment);

                int resultado = Convert.ToInt32(SqlHelper.ExecuteScalar("usp_Tbl_toc_Update", objParameter));

                return resultado.ToString();
            }
            catch (Exception ex)
            {
                return "NOOK";
            }
        }

        public string fn_Insert_tbl_td(TBL_TDEL objTBL_TDEL)
        {
            try
            {
                int longitudDiccionario = objTBL_TDEL.dictImportarDocumento.Count;
                int contador = 0;

                SqlParameter[] objParameter = new SqlParameter[longitudDiccionario + 1];
                objParameter[contador] = new SqlParameter("@TocId", objTBL_TDEL.tocId);

                foreach (var item in objTBL_TDEL.dictImportarDocumento)
                {
                    contador++;
                    objParameter[contador] = new SqlParameter(item.Key, item.Value);
                }

                int resultado = Convert.ToInt32(SqlHelper.ExecuteScalar($"usp_TD{objTBL_TDEL.TemplateId}_insert", objParameter));

                return resultado.ToString();
            }
            catch (Exception ex)
            {
                return "NOOK";
            }
        }

        public string fn_Update_Tbl_Td(TBL_TDEL objTBL_TDEL)
        {
            try
            {
                int longitudDiccionario = objTBL_TDEL.dictImportarDocumento.Count;
                int contador = 0;

                SqlParameter[] objParameter = new SqlParameter[longitudDiccionario + 1];
                objParameter[contador] = new SqlParameter("@TocId", objTBL_TDEL.tocId);

                foreach (var item in objTBL_TDEL.dictImportarDocumento)
                {
                    contador++;
                    objParameter[contador] = new SqlParameter(item.Key, item.Value);
                }

                int resultado = Convert.ToInt32(SqlHelper.ExecuteScalar($"usp_TD{objTBL_TDEL.TemplateId}_update", objParameter));

                return resultado.ToString();
            }
            catch (Exception ex)
            {
                return "NOOK";
            }
        }

        public string fn_Insert_Tbl_doc(TBL_DOCEL objTblDoc)
        {
            try
            {
                SqlParameter[] objParameter = new SqlParameter[9];
                objParameter[0] = new SqlParameter("@TocId", objTblDoc.TocId);
                objParameter[1] = new SqlParameter("@PageNum", objTblDoc.PageNum);
                objParameter[2] = new SqlParameter("@img_size", objTblDoc.img_size);
                objParameter[3] = new SqlParameter("@txt_size", objTblDoc.txt_size);
                objParameter[4] = new SqlParameter("@img_width", objTblDoc.img_width);
                objParameter[5] = new SqlParameter("@img_height", objTblDoc.img_height);
                objParameter[6] = new SqlParameter("@img_xdpi", objTblDoc.img_xdpi);
                objParameter[7] = new SqlParameter("@img_ydpi", objTblDoc.img_ydpi);
                objParameter[8] = new SqlParameter("@img_bpp", objTblDoc.img_bpp);

                int resultado = Convert.ToInt32(SqlHelper.ExecuteScalar("usp_Tbl_doc_Insert", objParameter));

                return resultado.ToString();
            }
            catch (Exception ex)
            {
                return "NOOK";
            }
        }

        public string fn_Update_Tbl_doc(TBL_DOCEL objTblDoc)
        {
            try
            {
                SqlParameter[] objParameter = new SqlParameter[9];
                objParameter[0] = new SqlParameter("@TocId", objTblDoc.TocId);
                objParameter[1] = new SqlParameter("@PageNum", objTblDoc.PageNum);
                objParameter[2] = new SqlParameter("@img_size", objTblDoc.img_size);
                objParameter[3] = new SqlParameter("@txt_size", objTblDoc.txt_size);
                objParameter[4] = new SqlParameter("@img_width", objTblDoc.img_width);
                objParameter[5] = new SqlParameter("@img_height", objTblDoc.img_height);
                objParameter[6] = new SqlParameter("@img_xdpi", objTblDoc.img_xdpi);
                objParameter[7] = new SqlParameter("@img_ydpi", objTblDoc.img_ydpi);
                objParameter[8] = new SqlParameter("@img_bpp", objTblDoc.img_bpp);

                int resultado = Convert.ToInt32(SqlHelper.ExecuteScalar("usp_Tbl_doc_Update", objParameter));

                return resultado.ToString();
            }
            catch (Exception ex)
            {
                return "NOOK";
            }
        }

        public TBL_VOLEL fn_Get_Vol_Get_VolumeId(int VolumeId)
        {
            //List<TBL_VOLEL> listTBL_VOLEL = new List<TBL_VOLEL>();
            TBL_VOLEL objTBL_VOLEL = new TBL_VOLEL();

            DataTable dt = new DataTable();
            SqlParameter[] objParameter = new SqlParameter[1];
            objParameter[0] = new SqlParameter("@VolumeId", VolumeId);
            SqlHelper.Fill(dt, "usp_Tbl_vol_Get_VolumeId", objParameter);

            foreach (DataRow item in dt.Rows)
            {
                objTBL_VOLEL = new TBL_VOLEL();

                objTBL_VOLEL.VolumeId = int.Parse(item["VolumeId"].ToString());
                objTBL_VOLEL.VolumeName = item["VolumeName"].ToString();
                objTBL_VOLEL.Vol_descrip = item["Vol_descrip"].ToString();
                objTBL_VOLEL.FixedPathShare = item["FixedPathShare"].ToString();

                //listTBL_VOLEL.Add(objTBL_VOLEL);
            }

            return objTBL_VOLEL;
        }

    }
}
