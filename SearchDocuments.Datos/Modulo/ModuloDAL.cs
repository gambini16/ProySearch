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

namespace SearchDocuments.Datos.Modulo
{
    public class ModuloDAL : IModuloDAL
    {
        public List<ModuloListadoEL> fn_Get_Modulo(ModuloEL objOpcionEL)
        {
            try
            {
                List<ModuloListadoEL> lstOpciones = new List<ModuloListadoEL>();
                ModuloListadoEL opcion;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[1];
                objParameter[0] = new SqlParameter("@NOMBRE", objOpcionEL.MODULO_NOMBRE);
                SqlHelper.Fill(dt, "usp_tbl_module_Get", objParameter);
                foreach (DataRow item in dt.Rows)
                {
                    opcion = new ModuloListadoEL();
                    opcion.MODULO_ID = Convert.ToInt32(item[0].ToString());
                    opcion.MODULO_NOMBRE = item[1].ToString();
                    opcion.ESTADO_DESCRIPCION = item[3].ToString();
                    opcion.SIGLA = item[2].ToString();
                    lstOpciones.Add(opcion);
                }
                return lstOpciones;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public ModuloEL fn_GetInfo_Modulo(ModuloEL objOpcionEL)
        {
            try
            {
                ModuloEL opcion = null;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[1];
                objParameter[0] = new SqlParameter("@IdModule", objOpcionEL.MODULO_ID);
                SqlHelper.Fill(dt, "usp_tbl_module_Get_IdModule", objParameter);

                foreach (DataRow item in dt.Rows)
                {
                    opcion = new ModuloEL();
                    opcion.MODULO_ID = Convert.ToInt32(item[0].ToString());
                    opcion.MODULO_NOMBRE = item[1].ToString();
                    opcion.ESTADO = Convert.ToInt32(item[2].ToString());
                    opcion.SIGLA = item[3].ToString();
                }
                return opcion;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string fn_Update_Modulo(ModuloEL objOpcionEL)
        {
            try
            {
                SqlParameter[] objParameter = new SqlParameter[5];
                objParameter[0] = new SqlParameter("@IdModule", objOpcionEL.MODULO_ID);
                objParameter[1] = new SqlParameter("@NameModule", objOpcionEL.MODULO_NOMBRE);
                objParameter[2] = new SqlParameter("@Url", objOpcionEL.SIGLA);
                objParameter[3] = new SqlParameter("@Estado", objOpcionEL.ESTADO);
                objParameter[4] = new SqlParameter("@UsuarioCreacion", objOpcionEL.USUARIO_CREACION);
                int resultado = Convert.ToInt32(SqlHelper.ExecuteScalar("usp_tbl_module_Update", objParameter));
                return resultado.ToString();
            }
            catch (Exception ex)
            {
                return "NOOK";
            }
        }

        public string fn_Insert_Modulo(ModuloEL objOpcionEL)
        {
            try
            {
                SqlParameter[] objParameter = new SqlParameter[4];
                objParameter[0] = new SqlParameter("@Name", objOpcionEL.MODULO_NOMBRE);
                objParameter[1] = new SqlParameter("@Estado", objOpcionEL.ESTADO);
                objParameter[2] = new SqlParameter("@Sigla", objOpcionEL.SIGLA);
                objParameter[3] = new SqlParameter("@UsuarioCreacion", objOpcionEL.USUARIO_CREACION);
                int resultado = Convert.ToInt32(SqlHelper.ExecuteScalar("usp_tbl_module_Insert", objParameter));
                return resultado.ToString();
            }
            catch (Exception ex)
            {
                return "NOOK";
            }
        }

    }
}
