using SearchDocuments.Datos.Helpers;
using SearchDocuments.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Datos.Parametro
{
    public class ParametroDAL : IParametroDAL
    {
        public List<ParametroEL> fn_Get_Roles(string strNombre)
        {
            try
            {
                List<ParametroEL> lstParametros = new List<ParametroEL>();
                ParametroEL parametro;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[1];
                objParameter[0] = new SqlParameter("@NOMBRE", strNombre);
                SqlHelper.Fill(dt, "usp_Tbl_groups_PorNombre", objParameter);
                foreach (DataRow item in dt.Rows)
                {
                    parametro = new ParametroEL();
                    parametro.CODIGO_VALOR = item["IdGroup"].ToString();
                    parametro.VALOR = item["IdGroup"].ToString();
                    parametro.DESCRIPCION = item["NameGroup"].ToString();
                    lstParametros.Add(parametro);
                }
                return lstParametros;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ParametroEL> fn_Get_Plantillas()
        {
            try
            {
                List<ParametroEL> lstParametros = new List<ParametroEL>();
                ParametroEL parametro;
                DataTable dt = new DataTable();
                SqlHelper.Fill(dt, "usp_Tbl_tstr_Get_Active");
                foreach (DataRow item in dt.Rows)
                {
                    parametro = new ParametroEL();
                    parametro.CODIGO_VALOR = item["TemplateId"].ToString();
                    parametro.VALOR = item["TemplateId"].ToString();
                    parametro.DESCRIPCION = item["TemplateName"].ToString();
                    lstParametros.Add(parametro);
                }
                return lstParametros;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ParametroEL> fn_Get_PlantillasPorPerfil(int codigoPerfil)
        {
            try
            {
                List<ParametroEL> lstParametros = new List<ParametroEL>();
                ParametroEL parametro;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[1];
                objParameter[0] = new SqlParameter("@GroupId", codigoPerfil);
                SqlHelper.Fill(dt, "usp_Tbl_tstr_Get_GroupId", objParameter);
                foreach (DataRow item in dt.Rows)
                {
                    parametro = new ParametroEL();
                    parametro.CODIGO_VALOR = item["TemplateId"].ToString();
                    parametro.VALOR = item["TemplateId"].ToString();
                    parametro.DESCRIPCION = item["TemplateName"].ToString();
                    lstParametros.Add(parametro);
                }
                return lstParametros;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

    }
}
