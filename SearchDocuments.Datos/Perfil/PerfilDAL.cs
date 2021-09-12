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

namespace SearchDocuments.Datos.Perfil
{
    public class PerfilDAL : IPerfilDAL
    {
        public List<PerfilListadoEL> fn_Get_Perfil(PerfilEL objPerfilEL)
        {
            try
            {
                List<PerfilListadoEL> lstRoles = new List<PerfilListadoEL>();
                PerfilListadoEL rol;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[1];
                objParameter[0] = new SqlParameter("@NameGroup", objPerfilEL.PERFIL_NOMBRE);
                SqlHelper.Fill(dt, "[usp_Tbl_groups_Get_NameGroup]", objParameter);

                foreach (DataRow item in dt.Rows)
                {
                    rol = new PerfilListadoEL();
                    rol.PERFIL_ID = Convert.ToInt32(item[0].ToString());
                    rol.PERFIL_NOMBRE = item[1].ToString();
                    rol.DESCRIPCION = item[2].ToString();
                    lstRoles.Add(rol);
                }
                return lstRoles;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }
        public List<PerfilListadoEL> fn_Get_Groups_All()
        {
            try
            {
                List<PerfilListadoEL> lstRoles = new List<PerfilListadoEL>();
                PerfilListadoEL rol;
                DataTable dt = new DataTable();
                
                SqlHelper.Fill(dt, "[usp_Tbl_groups_Get_All]");

                foreach (DataRow item in dt.Rows)
                {
                    rol = new PerfilListadoEL();
                    rol.PERFIL_ID = Convert.ToInt32(item[0].ToString());
                    rol.PERFIL_NOMBRE = item[1].ToString();
                    rol.DESCRIPCION = item[2].ToString();
                    lstRoles.Add(rol);
                }
                return lstRoles;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return null;
            }
        }

        public PerfilEL fn_GetInfo_Perfil(PerfilEL objRolEL)
        {
            try
            {
                PerfilEL rol = null;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[1];
                objParameter[0] = new SqlParameter("@IdGroup", objRolEL.PERFIL_ID);
                SqlHelper.Fill(dt, "usp_Tbl_groups_Get_IdGroup", objParameter);

                foreach (DataRow item in dt.Rows)
                {
                    rol = new PerfilEL();
                    rol.PERFIL_ID = Convert.ToInt32(item[0].ToString());
                    rol.PERFIL_NOMBRE = item[1].ToString();
                    rol.DESCRIPCION = item[2].ToString();
                }
                return rol;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string fn_Update_Perfil(PerfilEL objRolEL)
        {
            try
            {
                SqlParameter[] objParameter = new SqlParameter[3];
                objParameter[0] = new SqlParameter("@IdGroup", objRolEL.PERFIL_ID);
                objParameter[1] = new SqlParameter("@NameGroup", objRolEL.PERFIL_NOMBRE);
                objParameter[2] = new SqlParameter("@DescriGroup", objRolEL.DESCRIPCION);
                int resultado = Convert.ToInt32(SqlHelper.ExecuteScalar("usp_Tbl_groups_Update", objParameter));
                return resultado.ToString();
            }
            catch (Exception ex)
            {
                return "NOOK";
            }
        }

        public string fn_Insert_Perfil(PerfilEL objRolEL)
        {
            try
            {
                SqlParameter[] objParameter = new SqlParameter[2];
                objParameter[0] = new SqlParameter("@NameGroup", objRolEL.PERFIL_NOMBRE);
                objParameter[1] = new SqlParameter("@DescriGroup", objRolEL.DESCRIPCION);
                int resultado = Convert.ToInt32(SqlHelper.ExecuteScalar("usp_Tbl_groups_Insert", objParameter));
                return resultado.ToString();
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                return "NOOK";
            }
        }

        public List<ModuloEL> fn_Get_Perfil_Modulo_Actual(int intTipoConsulta, PerfilEL objRolEL)
        {
            try
            {
                List<ModuloEL> lstOpciones = new List<ModuloEL>();
                ModuloEL opcion;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[2];
                objParameter[0] = new SqlParameter("@ROL", objRolEL.PERFIL_ID);
                objParameter[1] = new SqlParameter("@TIPO", intTipoConsulta);
                SqlHelper.Fill(dt, "usp_tbl_ModulexGroup_GetTipo", objParameter);
                foreach (DataRow item in dt.Rows)
                {
                    opcion = new ModuloEL();
                    opcion.MODULO_ID = Convert.ToInt32(item[0].ToString());
                    opcion.MODULO_NOMBRE = item[1].ToString();
                    opcion.SIGLA = item[2].ToString();
                    opcion.ESTADO = int.Parse(item[3].ToString());
                    lstOpciones.Add(opcion);
                }
                return lstOpciones;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<PlantillaEL> fn_Get_Plantilla_Perfil(int intTipoConsulta, PerfilEL objRolEL)
        {
            try
            {
                List<PlantillaEL> lstPlantillas = new List<PlantillaEL>();
                PlantillaEL plantilla;
                DataTable dt = new DataTable();
                SqlParameter[] objParameter = new SqlParameter[2];
                objParameter[0] = new SqlParameter("@ROL", objRolEL.PERFIL_ID);
                objParameter[1] = new SqlParameter("@TIPO", intTipoConsulta);
                SqlHelper.Fill(dt, "usp_tbl_TemplateGroup_GetTipo", objParameter);
                foreach (DataRow item in dt.Rows)
                {
                    plantilla = new PlantillaEL();
                    plantilla.PLANTILLA_ID = Convert.ToInt32(item[0].ToString());
                    plantilla.PLANTILLA_NOMBRE = item[1].ToString();
                    lstPlantillas.Add(plantilla);
                }
                return lstPlantillas;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string fn_Insert_PerfilModulo(List<ModuloEL> lstOpciones, PerfilEL objRolEL)
        {
            try
            {
                fn_Eliminar_PerfilModulo(objRolEL);
                if (lstOpciones.Count == 1)
                {
                    if (lstOpciones[0].MODULO_ID != 0)
                    {
                        foreach (ModuloEL item in lstOpciones)
                        {
                            SqlParameter[] objParameter = new SqlParameter[3];
                            objParameter[0] = new SqlParameter("@IdGroup", objRolEL.PERFIL_ID);
                            objParameter[1] = new SqlParameter("@IdModule", item.MODULO_ID);
                            objParameter[2] = new SqlParameter("@UserCreation", objRolEL.USUARIO_CREACION);
                            Convert.ToInt32(SqlHelper.ExecuteScalar("usp_tbl_module_group_Ins", objParameter));
                        }
                    }
                }
                else
                {
                    foreach (ModuloEL item in lstOpciones)
                    {
                        SqlParameter[] objParameter = new SqlParameter[3];
                        objParameter[0] = new SqlParameter("@IdGroup", objRolEL.PERFIL_ID);
                        objParameter[1] = new SqlParameter("@IdModule", item.MODULO_ID);
                        objParameter[2] = new SqlParameter("@UserCreation", objRolEL.USUARIO_CREACION);
                        Convert.ToInt32(SqlHelper.ExecuteScalar("usp_tbl_module_group_Ins", objParameter));
                    }
                }

                return "1";
            }
            catch (Exception ex)
            {
                return "NOOK";
            }
        }

        public string fn_Insert_PerfilPlantilla(List<PlantillaEL> lstOpciones, PerfilEL objRolEL)
        {
            try
            {
                fn_Eliminar_PerfilPlantilla(objRolEL);
                if (lstOpciones.Count == 1)
                {
                    if (lstOpciones[0].PLANTILLA_ID != 0)
                    {
                        foreach (PlantillaEL item in lstOpciones)
                        {
                            SqlParameter[] objParameter = new SqlParameter[3];
                            objParameter[0] = new SqlParameter("@IdGroup", objRolEL.PERFIL_ID);
                            objParameter[1] = new SqlParameter("@IdTemplate", item.PLANTILLA_ID);
                            objParameter[2] = new SqlParameter("@UserCreation", objRolEL.USUARIO_CREACION);
                            Convert.ToInt32(SqlHelper.ExecuteScalar("usp_tbl_template_group_Ins", objParameter));
                        }
                    }
                }
                else
                {
                    foreach (PlantillaEL item in lstOpciones)
                    {
                        SqlParameter[] objParameter = new SqlParameter[3];
                        objParameter[0] = new SqlParameter("@IdGroup", objRolEL.PERFIL_ID);
                        objParameter[1] = new SqlParameter("@IdTemplate", item.PLANTILLA_ID);
                        objParameter[2] = new SqlParameter("@UserCreation", objRolEL.USUARIO_CREACION);
                        Convert.ToInt32(SqlHelper.ExecuteScalar("usp_tbl_template_group_Ins", objParameter));
                    }
                }

                return "1";
            }
            catch (Exception ex)
            {
                return "NOOK";
            }
        }

        public string fn_Eliminar_PerfilModulo(PerfilEL objRolEL)
        {
            try
            {
                SqlParameter[] objParameter = new SqlParameter[1];
                objParameter[0] = new SqlParameter("@IdGroup", objRolEL.PERFIL_ID);
                int resultado = Convert.ToInt32(SqlHelper.ExecuteScalar("usp_tbl_module_group_Del", objParameter));
                return resultado.ToString();
            }
            catch (Exception ex)
            {
                return "NOOK";
            }
        }

        public string fn_Eliminar_PerfilPlantilla(PerfilEL objRolEL)
        {
            try
            {
                SqlParameter[] objParameter = new SqlParameter[1];
                objParameter[0] = new SqlParameter("@IdGroup", objRolEL.PERFIL_ID);
                int resultado = Convert.ToInt32(SqlHelper.ExecuteScalar("usp_tbl_template_group_Del", objParameter));
                return resultado.ToString();
            }
            catch (Exception ex)
            {
                return "NOOK";
            }
        }

    }
}
