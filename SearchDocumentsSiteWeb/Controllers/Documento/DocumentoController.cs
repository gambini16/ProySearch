using SearchDocuments.Comunes;
using SearchDocuments.Criptografia;
using SearchDocuments.Entidades;
using SearchDocuments.Entidades.ImportarDocumento;
using SearchDocuments.Entidades.Listado;
using SearchDocuments.Negocio.Documento;
using SearchDocuments.Negocio.Filtro;
using SearchDocuments.Negocio.ImportarDocumento;
using SearchDocumentsSiteWeb.Controllers.Modulo;
using SearchDocumentsSiteWeb.Controllers.Parametro;
using SearchDocumentsSiteWeb.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace SearchDocumentsSiteWeb.Controllers.Documento
{
    public class DocumentoController : Controller
    {
        #region "Refrescar Cache"
        [SessionExpireFilter]
        public void RefrescarCache()
        {
            if (SesionActual.Current.PERFIL == "")
            {
                Session.Abandon();
                Response.Redirect("~");
            }
            else
            {
                ViewData["LOGUEO_NOMBRE_COMPLETO"] = SesionActual.Current.NOMBRE_COMPLETO;
                ViewData["LOGUEO_PERFIL"] = SesionActual.Current.PERFIL;
                ViewData["OPCIONES_USUARIO"] = SesionActual.Current.OPCIONES_USUARIO;
                ViewData["LOGUEO_PERFIL_NAME"] = SesionActual.Current.PERFIL_NOMBRE;
                ViewData["USER_ID"] = SesionActual.Current.IN_CODIGO_USU;


                Response.Cache.AppendCacheExtension("no-store, must-revalidate");
                Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.0.
                Response.AppendHeader("Expires", "0"); // Proxies.
            }
        }
        #endregion

        [HttpGet]
        public JsonResult Leer_Documento(string parametros, string intTipoPlantilla)
        {
            var vLstDocumento = new List<DocumentoListadoEL>();
            var vLstTD1 = new List<TD1ListadoEL>();
            IDocumentoBL objDocumentoBL = new DocumentoBL();
            DocumentoEL objDocumentoEL = new DocumentoEL();
            /*Se obtiene el nombre de la tabla*/
            objDocumentoEL.TIPO_DOCUMENTO = intTipoPlantilla;
            objDocumentoEL = objDocumentoBL.fn_GetTabla_Plantilla(objDocumentoEL);

            /*Los nombre de los campos del filtro*/
            var vLstfiltro = new List<FiltroListadoEL>();
            IFiltroBL objFiltroBL = new FiltroBL();
            vLstfiltro = objFiltroBL.fn_Get_Campos(intTipoPlantilla);
            string select = String.Empty;
            string filtro = String.Empty;
            filtro = "where ";
            int contador = 0;
            string[] words = parametros.Split('|');

            if (!parametros.Trim().Equals(""))
            {
                foreach (var item in vLstfiltro)
                {
                    if (item.TIPO_DATO.ToUpper().Equals("DATETIME"))
                    {
                        select = select + "convert(varchar,t." + item.NOMBRE_CAMPO + ",103) as " + item.NOMBRE_CAMPO + ",";
                    }
                    else
                    {
                        select = select + "t." + item.NOMBRE_CAMPO + ",";
                    }
                    if (contador == 0)
                    {
                        if (item.TIPO_DATO.ToUpper().Equals("DATETIME"))
                        {
                            filtro = filtro + " (convert(varchar,t." + item.NOMBRE_CAMPO + ",103)='" + words[contador] + "' or '" + words[contador] + "'='')";
                        }
                        else
                        {
                            if (item.TIPO_CONTROL.Trim().Equals("0"))
                            {
                                filtro = filtro + " (t." + item.NOMBRE_CAMPO + "='" + words[contador] + "' or '" + words[contador] + "'='')";
                            }
                            else
                            {
                                filtro = filtro + " (t." + item.NOMBRE_CAMPO + "='" + words[contador] + "' or '" + words[contador] + "'='--Seleccione--')";
                            }
                        }
                        contador = contador + 1;
                    }
                    else
                    {
                        if (item.TIPO_DATO.ToUpper().Equals("DATETIME"))
                        {
                            filtro = filtro + " AND (convert(varchar,t." + item.NOMBRE_CAMPO + ",103)='" + words[contador] + "' or '" + words[contador] + "'='')";
                        }
                        else
                        {
                            if (item.TIPO_CONTROL.Trim().Equals("0"))
                            {
                                filtro = filtro + " AND (t." + item.NOMBRE_CAMPO + "='" + words[contador] + "' or '" + words[contador] + "'='')";
                            }
                            else
                            {
                                filtro = filtro + " AND (t." + item.NOMBRE_CAMPO + "='" + words[contador] + "' or '" + words[contador] + "'='--Seleccione--')";
                            }
                        }
                        contador = contador + 1;
                    }
                }
                DataTable dt = new DataTable();
                dt = objDocumentoBL.ObtenerListadoDinamico(select, filtro, objDocumentoEL.TABLA);
                dynamic listado = new ExpandoObject();
                var dictionary = (IDictionary<string, object>)listado;
                if (dt != null)
                {
                    foreach (DataColumn dataColumn in dt.Columns)
                    {
                        var columnValueList = new List<object>();

                        foreach (DataRow dataRow in dt.Rows)
                        {
                            columnValueList.Add(dataRow[dataColumn.ColumnName]);
                        }

                        dictionary.Add(dataColumn.ColumnName, columnValueList);
                    }
                }
                else
                {
                    var columnaExtra = new List<object>();
                    columnaExtra.Add("Tabla no existe");
                    dictionary.Add("Resultado", columnaExtra);
                }

                Session[Constantes.LISTA.Value] = null;
                Session[Constantes.LISTA.Value] = listado;
                return Json(listado, JsonRequestBehavior.AllowGet);
            }
            Session[Constantes.LISTA.Value] = null;
            Session[Constantes.LISTA.Value] = vLstTD1;
            return Json(vLstTD1, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewErrorPDF()
        {
            return View();
        }

        public ActionResult Index()
        {
            if (SesionActual.Current.VC_DATAENCRY == "" || SesionActual.Current.VC_DATAENCRY == null)
            {
                ViewBag.hdnViewPdf = string.Empty;
                ViewBag.hidIdUser = string.Empty;
            }
            else
            {
                ViewBag.hdnViewPdf = ConfigurationManager.AppSettings["ViewPdfOpcion"].ToString();
                ViewBag.hidIdUser = SesionActual.Current.VC_DATAENCRY.ToString();
            }


            RefrescarCache();
            int codPerfil = Convert.ToInt32(ViewData["LOGUEO_PERFIL"].ToString());
            //ViewBag.TipoDocumento = new ParametroController().SelectListTiposDocumentos();
            ViewBag.TipoDocumento = new ParametroController().SelectListTiposDocumentosporPerfil(codPerfil);
            ViewBag.Sexo = new ParametroController().SelectListSexo();
            return View();
        }

        [SessionExpireFilter]
        public ActionResult MostrarPDF(int intCodigoFile)
        {
            DocumentoBL objDocBL = new DocumentoBL();
            var objDocumentoEL = new DocumentoEL();

            //objDocumentoEL = objDocBL.ObtenerInformacionDocumento(intCodigoFile);

            objDocumentoEL = objDocBL.ObtenerInfoDoc(intCodigoFile, 1, 0);




            //string strRutaArchivo = objDocumentoEL.CARPETA;  //"DIVISION MINERA/459297107/BOLETAS DE PAGO EMPLEADOS LIMA-RAURA MAR-1993";
            string strNombreArchivo = objDocumentoEL.NOMBRE_DOCUMENTO;//"/2_00000001.pdf";"2_00000001.pdf"; 
            strNombreArchivo = strNombreArchivo.Replace(@"\", @"/");
            string path = string.Format("{0}/{1}", ConfigurationManager.AppSettings["RutaArchivosPDF"], strNombreArchivo);

            string strRespuesta = path;

            return Json(new { strRespuesta = strRespuesta }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListarFiltros(string codTipoDocumento)
        {
            var vLstfiltro = new List<FiltroListadoEL>();
            IFiltroBL objFiltroBL = new FiltroBL();
            vLstfiltro = objFiltroBL.fn_Get_Campos(codTipoDocumento);
            vLstfiltro = objFiltroBL.fn_Get_Valores_Select_List(vLstfiltro);
            Session[Constantes.LISTA.Value] = null;
            Session[Constantes.LISTA.Value] = vLstfiltro;
            return Json(vLstfiltro, JsonRequestBehavior.AllowGet);
        }

        private class LineData
        {
            public string IdUrl { get; set; }
            public string CodRpta { get; set; }
            public string MsgRpta { get; set; }
            public string message { get; set; }
            public string UrlPdf { get; set; }
            public string CodigoFile { get; set; }
            public List<LineData> list { get; set; }
        }
        public ActionResult ValidateRequestUrl(string paramKey, string paramOpc, string paramPerfil, string paramIdFile)
        {
            string strEncode64User = string.Empty;
            string strEncodeIdFile = string.Empty;
            string strCodRpta = "0";
            object objData = null;

            try
            {
                strEncode64User = EncodingForBase64.Base64Encode(paramKey);
                strEncodeIdFile = EncodingForBase64.Base64Encode(paramIdFile);
                string stUrlPdf = ConfigurationManager.AppSettings["RutaPrincipal"] + "Documento/GetPdf?";
                objData = new LineData { CodRpta = strCodRpta, MsgRpta = "OK", IdUrl = strEncode64User, UrlPdf = stUrlPdf, CodigoFile = strEncodeIdFile };
            }
            catch (Exception ex)
            {
                strCodRpta = "1";
                objData = new LineData { CodRpta = strCodRpta, MsgRpta = ex.Message, IdUrl = strEncode64User, UrlPdf = string.Empty, CodigoFile = string.Empty };

            }
            finally
            {

            }
            return Json(objData, JsonRequestBehavior.AllowGet);
        }
       
        public ActionResult GetPdf()
        {
            try
            {
                if (Request.QueryString["IdFileKey"] == null || Request.QueryString["IdUserKey"] == null)
                {
                    TempData["yourMessage"] = "Problema al abrir el documento electrónico PDF. (Codigo de error = 1)";
                    return RedirectToAction("ViewErrorPDF", "Documento");
                }
                string strIdFileRequest = Request.QueryString["IdFileKey"];
                string strIdUserRequest = Request.QueryString["IdUserKey"];

                if (EncodingForBase64.Base64Decode(strIdFileRequest) == null || EncodingForBase64.Base64Decode(strIdUserRequest) == null)
                {
                    TempData["yourMessage"] = "Problema al abrir el documento electrónico PDF. (Codigo de error = 2)";
                    return RedirectToAction("ViewErrorPDF", "Documento");
                }

                int intCodigoFile = Int32.Parse(EncodingForBase64.Base64Decode(strIdFileRequest));
                string strIdUserEncy = EncodingForBase64.Base64Decode(strIdUserRequest);
                string strIdUser = EncryptionHelper.Decrypt(strIdUserEncy);

                if (strIdUser != SesionActual.Current.IN_CODIGO_USU.ToString())
                {
                    TempData["yourMessage"] = "Problema al abrir el documento electrónico PDF. (Codigo de error = 3)";
                    return RedirectToAction("ViewErrorPDF", "Documento");
                }

                DocumentoBL objDocBL = new DocumentoBL();
                var objDocumentoEL = new DocumentoEL();
                objDocumentoEL = objDocBL.ObtenerInfoDoc(intCodigoFile, 1, 1);







                //string strRutaArchivo = objTbl_Ta_File.VC_FOLDER_IMG.Replace("/", @"\");
                string strNombreArchivo = objDocumentoEL.NOMBRE_DOCUMENTO.Substring(objDocumentoEL.NOMBRE_DOCUMENTO.LastIndexOf(@"\") + 1);
                string path = objDocumentoEL.NOMBRE_DOCUMENTO;


                string filePath = path;

                if (!System.IO.File.Exists(filePath))
                {
                    TempData["yourMessage"] = "Problema al abrir el documento electrónico PDF. (Codigo de error = 4)";
                    return RedirectToAction("ViewErrorPDF", "Documento");
                }
                else
                {
                    string strCadena = string.Format("{0}_{1}", intCodigoFile.ToString(), strNombreArchivo);
                    //Util.RegistrarAuditoria("Consulta Documento", "Abrir_Documento", strCadena, SesionActual.Current.VC_LOGIN_USU, SesionActual.Current.IN_CODIGO_USU);
                }

                byte[] FileBytes = System.IO.File.ReadAllBytes(filePath);
                Response.AddHeader("Content-Disposition", "inline; filename=" + strNombreArchivo);
                return File(FileBytes, "application/pdf");

            }
            catch (Exception ex)
            {
                Documentos_Log.Log_WriteLog(ex.Message, typeof(DocumentoController).ToString());
                Documentos_Log.Log_WriteLog(ex.Source, typeof(DocumentoController).ToString());
                Documentos_Log.Log_WriteLog(ex.StackTrace, typeof(DocumentoController).ToString());

                TempData["yourMessage"] = ex.Message + "--" + ex.StackTrace;

                //TempData["yourMessage"] = "Acceso denegado al abrir el documento. (Codigo de error = 5)";
                return RedirectToAction("ViewErrorPDF", "Documento");
            }
        }

        [HttpGet]
        public JsonResult Obtener_Datos_Tabla_Td(int templateId, int tocId)
        {
            IDocumentoBL objDocumentoBL = new DocumentoBL();
            DataTable dt = new DataTable();
            dt = objDocumentoBL.ObtenerDatoTablaTd(templateId, tocId);

            dynamic listado = new ExpandoObject();
            var dictionary = (IDictionary<string, object>)listado;
            if (dt != null)
            {
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    var columnValueList = new List<object>();

                    foreach (DataRow dataRow in dt.Rows)
                    {
                        columnValueList.Add(dataRow[dataColumn.ColumnName]);
                    }

                    dictionary.Add(dataColumn.ColumnName, columnValueList);
                }
            }
            else
            {
                var columnaExtra = new List<object>();
                columnaExtra.Add("Tabla no existe");
                dictionary.Add("Resultado", columnaExtra);
            }

            return Json(listado, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ActualizarEnBaseDatos(Dictionary<string, string> dictImportarDocumento, int intTipoPlantilla, string nombreArchivo, int tocId)
        {
            IImportarDocumentoBL objImportarDocumentoBL = new ImportarDocumentoBL();

            var objTBL_TOCEL = new TBL_TOCEL
            {
                TocId = tocId,
                ParentId = int.Parse(ConfigurationManager.AppSettings["consParentId"]),
                Name = nombreArchivo,
                ElType = int.Parse(ConfigurationManager.AppSettings["consElType"]),
                LastModified = DateTime.Now,
                CreateDate = DateTime.Now,
                IsIndexed = 0,
                VolumeId = int.Parse(ConfigurationManager.AppSettings["consVolumeId"]),
                TemplateId = intTipoPlantilla,
                PageCount = int.Parse(ConfigurationManager.AppSettings["consPageCount"]),
                Creator = SesionActual.Current.NOMBRE_COMPLETO,
                Toc_Flags = 1,
                Toc_Owner = string.Empty,
                Toc_Comment = string.Empty
            };

            var resultDoc = objImportarDocumentoBL.fn_Insert_Update_Tbl_toc(objTBL_TOCEL);

            if (resultDoc == "0")
            {

                var objTBL_TDEL = new TBL_TDEL
                {
                    tocId = tocId,
                    TemplateId = intTipoPlantilla,
                    dictImportarDocumento = dictImportarDocumento
                };

                var resultTd = objImportarDocumentoBL.fn_Insert_Update_tbl_td(objTBL_TDEL);

                if (resultTd == "0")
                {
                    var objTBL_DOCEL = new TBL_DOCEL
                    {
                        TocId = tocId,
                        PageNum = int.Parse(ConfigurationManager.AppSettings["consPageNum"]),
                        img_size = 0,
                        txt_size = int.Parse(ConfigurationManager.AppSettings["consTxt_size"]),
                        img_width = int.Parse(ConfigurationManager.AppSettings["consImg_width"]),
                        img_height = int.Parse(ConfigurationManager.AppSettings["consImg_height"]),
                        img_xdpi = int.Parse(ConfigurationManager.AppSettings["consImg_xdpi"]),
                        img_ydpi = int.Parse(ConfigurationManager.AppSettings["consImg_ydpi"]),
                        img_bpp = ConfigurationManager.AppSettings["consImg_bpp"].ToString()
                    };

                    var result = objImportarDocumentoBL.fn_Insert_Update_Tbl_doc(objTBL_DOCEL);

                    return this.Json((object)new
                    {
                        Value = int.Parse(result) > 0 ? result : "NOOK",
                    }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return this.Json((object)new
                    {
                        Value = resultTd,
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return this.Json((object)new
                {
                    Value = tocId,
                    //Message = "Subido con éxito",
                    //CodigoFile = 1
                }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
