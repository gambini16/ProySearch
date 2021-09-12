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

namespace SearchDocuments.Datos.Usuario
{
    public class UsuarioDAL : IUsuarioDAL
    {
        public List<UsuarioListadoEL> fn_Get_Usuario(UsuarioEL objUsuarioEL)
        {
            try
            {
                List<UsuarioListadoEL> lstUsuarios = new List<UsuarioListadoEL>();
                UsuarioListadoEL usuario;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[2];
                objParameter[0] = new SqlParameter("@Usuario", objUsuarioEL.LOGIN);
                objParameter[1] = new SqlParameter("@Datos", objUsuarioEL.NOMBRE_COMPLETO);
                SqlHelper.Fill(dt, "usp_Tbl_users_Get_Datos", objParameter);

                foreach (DataRow item in dt.Rows)
                {
                    usuario = new UsuarioListadoEL();
                    usuario.USUARIO_ID = Convert.ToInt32(item[0].ToString());
                    usuario.LOGIN = item[1].ToString();
                    usuario.NOMBRE_COMPLETO = item[4].ToString();
                    usuario.ROL_DESCRIPCION = item[18].ToString();
                    if (item[10].ToString().Trim().ToUpper().Equals("FALSE"))
                    {
                        usuario.ESTADO_DESCRIPCION = "Activo";
                    }
                    else
                    {
                        usuario.ESTADO_DESCRIPCION = "Inactivo";
                    }
                    lstUsuarios.Add(usuario);
                }
                return lstUsuarios;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public List<UsuarioListadoEL> fn_Get_Usuario()
        {
            try
            {
                List<UsuarioListadoEL> lstUsuarios = new List<UsuarioListadoEL>();
                UsuarioListadoEL usuario;
                DataTable dt = new DataTable();

                SqlHelper.Fill(dt, "usp_Fs_ta_usuario_Listado");

                foreach (DataRow item in dt.Rows)
                {
                    usuario = new UsuarioListadoEL();
                    usuario.USUARIO_ID = Convert.ToInt32(item["IN_CODIGO_USU"].ToString());
                    usuario.LOGIN = item["VC_LOGIN_USU"].ToString();
                    usuario.NOMBRE_COMPLETO = item["Nombre"].ToString();
                    usuario.ROL_DESCRIPCION = item["VC_EMAIL_USU"].ToString();
                    usuario.ESTADO_DESCRIPCION = item["Estado"].ToString();
                    lstUsuarios.Add(usuario);
                }
                return lstUsuarios;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public UsuarioEL fn_GetInfo_Usuario(UsuarioEL objUsuarioEL)
        {
            try
            {
                UsuarioEL usuario = null;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[1];
                objParameter[0] = new SqlParameter("@IdUsuario", objUsuarioEL.USUARIO_ID);
                SqlHelper.Fill(dt, "usp_Tbl_users_GetIdUsuario", objParameter);

                foreach (DataRow item in dt.Rows)
                {
                    usuario = new UsuarioEL();
                    usuario.USUARIO_ID = Convert.ToInt32(item["IdUsuario"].ToString());
                    usuario.LOGIN = item["Usuario"].ToString();
                    usuario.ROL_CODIGO = int.Parse(item["IdGrupo"].ToString());
                    usuario.CLAVE = item["Password"].ToString();
                    usuario.APELLIDO_PATERNO = item["LastName"].ToString();
                    usuario.NOMBRES = item["FirstName"].ToString();
                    usuario.NOMBRE_COMPLETO = item["FirstName"].ToString() + " " + item["LastName"].ToString();
                    if (item["IsDisabled"].ToString().ToUpper().Equals("FALSE"))
                    {
                        usuario.ESTADO_CODIGO = 1;
                    }
                    else
                    {
                        usuario.ESTADO_CODIGO = 0;
                    }
                    usuario.EMAIL = item["UserEmail"].ToString();
                    usuario.AVATAR = item["Avatar"].ToString();
                }
                return usuario;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string fn_Update_Usuario(UsuarioEL objUsuarioEL)
        {
            try
            {
                SqlParameter[] objParameter = new SqlParameter[7];
                objParameter[0] = new SqlParameter("@IdUsuario", objUsuarioEL.USUARIO_ID);
                objParameter[1] = new SqlParameter("@Usuario", objUsuarioEL.LOGIN);
                objParameter[2] = new SqlParameter("@FirstName", objUsuarioEL.NOMBRES);
                objParameter[3] = new SqlParameter("@LastName", objUsuarioEL.APELLIDO_PATERNO);
                objParameter[4] = new SqlParameter("@IdGrupo", objUsuarioEL.ROL_CODIGO);
                objParameter[5] = new SqlParameter("@Correo", objUsuarioEL.EMAIL);
                objParameter[6] = new SqlParameter("@IsDisabled", objUsuarioEL.ESTADO_CODIGO);
                int resultado = Convert.ToInt32(SqlHelper.ExecuteScalar("usp_Tbl_users_Update_Web", objParameter));
                return resultado.ToString();
            }
            catch (Exception ex)
            {
                return "NOOK";
            }
        }

        public string fn_Update_UsuarioAvatar(UsuarioEL objUsuarioEL)
        {
            try
            {
                SqlParameter[] objParameter = new SqlParameter[2];
                objParameter[0] = new SqlParameter("@Usuario", objUsuarioEL.LOGIN);
                objParameter[1] = new SqlParameter("@Avatar", objUsuarioEL.AVATAR);
                int resultado = Convert.ToInt32(SqlHelper.ExecuteScalar("usp_Tbl_users_Update_Avatar", objParameter));
                return resultado.ToString();
            }
            catch (Exception ex)
            {
                return "NOOK";
            }
        }

        public string fn_Insert_Usuario(UsuarioEL objUsuarioEL)
        {
            try
            {
                SqlParameter[] objParameter = new SqlParameter[7];
                objParameter[0] = new SqlParameter("@Usuario", objUsuarioEL.LOGIN);
                objParameter[1] = new SqlParameter("@FirstName", objUsuarioEL.NOMBRES);
                objParameter[2] = new SqlParameter("@LastName", objUsuarioEL.APELLIDO_PATERNO);
                objParameter[3] = new SqlParameter("@Password", objUsuarioEL.CLAVE);
                objParameter[4] = new SqlParameter("@IsDisabled", objUsuarioEL.ESTADO_CODIGO);
                objParameter[5] = new SqlParameter("@IdGrupo", objUsuarioEL.ROL_CODIGO);
                objParameter[6] = new SqlParameter("@Email", objUsuarioEL.EMAIL);
                int resultado = Convert.ToInt32(SqlHelper.ExecuteScalar("usp_Tbl_users_Insert_Web", objParameter));
                return resultado.ToString();
            }
            catch (Exception ex)
            {
                return "NOOK";
            }
        }

        public string fn_Update_Clave(UsuarioEL objUsuarioEL)
        {
            try
            {
                SqlParameter[] objParameter = new SqlParameter[2];
                objParameter[0] = new SqlParameter("@UsuV_Nom_Usu", objUsuarioEL.LOGIN_USUARIO);
                objParameter[1] = new SqlParameter("@UsuV_Pwd_Usu", objUsuarioEL.CLAVE);
                int resultado = Convert.ToInt32(SqlHelper.ExecuteScalar("usp_Ds_tbl_usuario_Update_Password", objParameter));
                return resultado.ToString();
            }
            catch (Exception ex)
            {
                return "NOOK";
            }
        }

        public string fn_Update_Clave(int intCodUser, string strPwd)
        {
            string resultado = "";

            try
            {
                SqlParameter[] objParameter = new SqlParameter[2];
                objParameter[0] = new SqlParameter("@IdUsuario", intCodUser);
                objParameter[1] = new SqlParameter("@Password", strPwd);
                SqlHelper.ExecuteNonQuery("usp_tbl_users_Update_Password", objParameter);
                resultado = "0";


                return resultado.ToString();
            }
            catch (Exception ex)
            {
                return "NOOK";
            }
        }



        public string fn_Update_GenerarClave(UsuarioEL objUsuarioEL)
        {
            try
            {
                SqlParameter[] objParameter = new SqlParameter[2];
                objParameter[0] = new SqlParameter("@Correo", objUsuarioEL.EMAIL);
                objParameter[1] = new SqlParameter("@Password", objUsuarioEL.CLAVE);
                int resultado = Convert.ToInt32(SqlHelper.ExecuteScalar("usp_tbl_users_email_Update_Password", objParameter));
                return resultado.ToString();
            }
            catch (Exception ex)
            {
                return "NOOK";
            }
        }

        public UsuarioEL fn_GetInfo_UsuarioCorreo(UsuarioEL objUsuarioEL)
        {
            try
            {
                UsuarioEL usuario = null;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[1];
                objParameter[0] = new SqlParameter("@Correo", objUsuarioEL.EMAIL);
                SqlHelper.Fill(dt, "usp_Tbl_users_Get_Correo", objParameter);

                foreach (DataRow item in dt.Rows)
                {
                    usuario = new UsuarioEL();
                    usuario.USUARIO_ID = Convert.ToInt32(item[0].ToString());
                    usuario.LOGIN = item[1].ToString();
                    usuario.ROL_CODIGO = int.Parse(item[15].ToString());
                    usuario.CLAVE = item[5].ToString();
                    usuario.APELLIDO_PATERNO = item[3].ToString();
                    usuario.NOMBRES = item[2].ToString();
                    usuario.EMAIL = objUsuarioEL.EMAIL;
                    usuario.AVATAR = item[18].ToString();
                }
                return usuario;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
