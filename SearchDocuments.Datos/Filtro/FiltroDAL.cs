using SearchDocuments.Comunes;
using SearchDocuments.Datos.Helpers;
using SearchDocuments.Entidades.Listado;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Datos.Filtro
{
    public class FiltroDAL:IFiltroDAL
    {
        public List<FiltroListadoEL> fn_Get_Campos(string codTipoDocumento)
        {
            try
            {
                List<FiltroListadoEL> lstFiltros = new List<FiltroListadoEL>();
                FiltroListadoEL filtro;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[1];
                objParameter[0] = new SqlParameter("@TemplateId", codTipoDocumento);
                SqlHelper.Fill(dt, "usp_Tbl_tfields_Get_TemplateId", objParameter);

                foreach (DataRow item in dt.Rows)
                {
                    filtro = new FiltroListadoEL();
                    filtro.CODIGO = item[0].ToString();
                    filtro.POSICION = item[2].ToString();
                    filtro.NOMBRE_CAMPO = Funciones.FirstCharToUpper(item["Name_Fields"]);
                    filtro.TIPO_DATO = item[4].ToString();
                    filtro.LONGITUD_CAMPO = item[5].ToString();
                    filtro.DATO_COLUMNA = item["DataColumn"].ToString();
                    filtro.TIPO_CONTROL = item[9].ToString();
                    lstFiltros.Add(filtro);
                }
                return lstFiltros;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<FiltroListadoEL> fn_Get_Valores_Select_List(List<FiltroListadoEL> lstFiltro)
        {
            try
            {
                foreach (FiltroListadoEL item in lstFiltro)
                {
                    if (item.TIPO_CONTROL.Equals("1")) //Si es un combo, se carga valores para el control
                    {
                        DataTable dt = new DataTable();
                        SqlParameter[] objParameter = new SqlParameter[1];
                        objParameter[0] = new SqlParameter("@TFieldId", item.CODIGO);
                        SqlHelper.Fill(dt, "usp_Tbl_lup_Get_TFieldId", objParameter);
                        if (dt!=null)
                        {
                            if (dt.Rows.Count>0)
                            {
                                item.CONTROL = new List<ValorControlEL>();
                                ValorControlEL o = new ValorControlEL();
                                o.VALOR = "0";
                                o.DESCRIPCION = "--Seleccione--";
                                item.CONTROL.Add(o);
                                foreach (DataRow row in dt.Rows)
                                {
                                    ValorControlEL valor = new ValorControlEL();
                                    valor.VALOR = row[1].ToString();
                                    valor.DESCRIPCION = row[2].ToString();
                                    item.CONTROL.Add(valor);
                                }        
                            }
                        }
                    }
                }
                return lstFiltro;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
