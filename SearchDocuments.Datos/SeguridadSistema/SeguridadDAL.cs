using SearchDocuments.Datos.Helpers;
using SearchDocuments.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchDocuments.Datos.SeguridadSistema
{
    public class SeguridadDAL : ISeguridadDAL
    {
        public Tbl_users ObtenerNombreUsuario(string strNombreUsuario)
        {
            try
            {
                Tbl_users usuario = null;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[1];
                objParameter[0] = new SqlParameter("@Usuario", strNombreUsuario);
                SqlHelper.Fill(dt, "usp_Tbl_users_Get_Usuario", objParameter);

                foreach (DataRow item in dt.Rows)
                {
                    usuario = new Tbl_users();
                    usuario.IN_CODIGO_USU = Convert.ToInt32(item["IdUsuario"].ToString());
                    usuario.VC_NOMBRE_USU = item["FirstName"].ToString();
                    usuario.VC_APELLIDO_USU = item["LastName"].ToString();
                    usuario.VC_USUARIO_USU = item["Usuario"].ToString();
                    usuario.IN_CODIGO_PRF = Convert.ToInt32(item["IdGrupo"].ToString());
                    usuario.VC_EMAIL_USU = item["UserEmail"].ToString();
                    usuario.VC_PWD_USU = item["Password"].ToString();
                    //usuario.NOM_PERFIL = item["NameGroup"].ToString();
                    usuario.NOM_PERFIL = "";
                    if (item["IsDisabled"].ToString().ToUpper().Equals("FALSE"))
                    {
                        usuario.CH_ESTADO_USU = "0";
                    }
                    else
                    {
                        usuario.CH_ESTADO_USU = "1";
                    }
                }
                return usuario;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public Tbl_users ObtenerCodigoUsuario(int intCodUser)
        {
            try
            {
                Tbl_users usuario = null;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[1];
                objParameter[0] = new SqlParameter("@IN_CODIGO_USU", intCodUser);
                SqlHelper.Fill(dt, "usp_Fs_ta_usuario_GetAll_IN_CODIGO_USU", objParameter);

                foreach (DataRow item in dt.Rows)
                {
                    usuario = new Tbl_users();
                    usuario.IN_CODIGO_USU = Convert.ToInt32(item["IN_CODIGO_USU"].ToString());
                    usuario.VC_NOMBRE_USU = item["VC_NOM_USU"].ToString();
                    usuario.VC_APELLIDO_USU = item["VC_APE_USU"].ToString();
                    usuario.VC_USUARIO_USU = item["VC_LOGIN_USU"].ToString();
                    usuario.IN_CODIGO_PRF = Convert.ToInt32(item["IN_CODIGO_PRF"].ToString());
                    usuario.VC_EMAIL_USU = item["VC_EMAIL_USU"].ToString();
                    usuario.VC_PWD_USU = item["VC_PWD_USU"].ToString();
                    usuario.CH_ESTADO_USU = item["CH_ESTADO_USU"].ToString();
                    usuario.VC_DIRECCION_USU = item["VC_DIRECCION_USU"].ToString();
                    usuario.IN_ESTA_VIEW = Convert.ToInt32(item["IN_ESTA_VIEW"].ToString());
                    usuario.IN_ESTA_EDIT = Convert.ToInt32(item["IN_ESTA_EDIT"].ToString());
                    usuario.IN_ESTA_ELI = Convert.ToInt32(item["IN_ESTA_ELI"].ToString());
                }
                return usuario;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string ActualizarIntentosUsuario(int intCodigoUsuario, int intNumeroIntentos)
        {
            try
            {
                SqlParameter[] objParameter = new SqlParameter[1];
                objParameter[0] = new SqlParameter("@IN_CODIGO_USU", intCodigoUsuario);
                objParameter[1] = new SqlParameter("@IN_INTENTOS_USU", intNumeroIntentos);

                int intResultado = Convert.ToInt32(SqlHelper.ExecuteScalar("usp_Ds_tbl_usuario_Update_Sesion", objParameter));
                return intResultado.ToString();
            }
            catch (Exception ex)
            {
                return "NOOK";
            }

        }
        public void Update_CH_ESTADO_USU(int intCodigoUsuario, string strEstado)
        {
            try
            {
                SqlParameter[] objParameter = new SqlParameter[2];
                objParameter[0] = new SqlParameter("@IN_CODIGO_USU", intCodigoUsuario);
                objParameter[1] = new SqlParameter("@CH_ESTADO_USU", strEstado);
                int intResultado = Convert.ToInt32(SqlHelper.ExecuteScalar("usp_Fs_ta_usuario_Update_CH_ESTADO_USU", objParameter));

            }
            catch (Exception ex)
            {

            }
        }

        public string InsertarDatosAuditoria(Tbl_Ta_Audit_Event entity)
        {
            try
            {
                SqlParameter[] objParameter = new SqlParameter[6];
                objParameter[0] = new SqlParameter("@IN_CODIGO_FILE", entity.IN_CODIGO_FILE);
                objParameter[1] = new SqlParameter("@VC_DESCRIP_1", entity.VC_DESCRIP_1);
                objParameter[2] = new SqlParameter("@IN_CODIGO_OPCION", entity.IN_CODIGO_OPCION);
                objParameter[3] = new SqlParameter("@IN_USUREG_AUD", entity.IN_USUREG_AUD);
                objParameter[4] = new SqlParameter("@VC_DIREC_IP", entity.VC_DIREC_IP);
                objParameter[5] = new SqlParameter("@VC_NOM_HOST", entity.VC_NOM_HOST);

                int intResultado = Convert.ToInt32(SqlHelper.ExecuteNonQuery("usp_tbl_ta_Audit_Insert", objParameter));
                return intResultado.ToString();
            }
            catch (Exception ex)
            {
                return "NOOK";
            }
        }

        /*Agregado por José Palomino*/
        public List<OpcionEL> obtenerOpcionesPerfil(int perfilId)
        {
            try
            {
                List<OpcionEL> lstOpciones = new List<OpcionEL>();
                OpcionEL opcion;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[1];
                objParameter[0] = new SqlParameter("@IdGroup", perfilId);
                SqlHelper.Fill(dt, "usp_tbl_module_group_Get", objParameter);
                foreach (DataRow item in dt.Rows)
                {
                    opcion = new OpcionEL();
                    opcion.OPCION_ID = Convert.ToInt32(item[0].ToString());
                    opcion.OPCION_NOMBRE = item[1].ToString();
                    opcion.SIGLA = item[2].ToString();
                    opcion.ESTADO = item[3].ToString();
                    lstOpciones.Add(opcion);
                }
                return lstOpciones;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
