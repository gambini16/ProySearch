using Ionic.Zip;
using SearchDocuments.Comunes;
using SearchDocuments.Criptografia;
using SearchDocuments.Entidades;
using SearchDocuments.Entidades.ImportarDocumento;
using SearchDocuments.Entidades.Listado;
using SearchDocuments.Negocio.Documento;
using SearchDocuments.Negocio.Filtro;
using SearchDocuments.Negocio.ImportarDocumento;
using SearchDocumentsSiteWeb.Controllers.Parametro;
using SearchDocumentsSiteWeb.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Dynamic;
using System.Text;
using System.Web.Mvc;
using ZipFile = Ionic.Zip.ZipFile;

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
            filtro = "where 1=1 ";
            int contador = 0;
            string[] words = parametros.Split('|');
            string cantidadRegistros = string.Empty;

            if (!parametros.Trim().Equals(""))
            {
                foreach (var item in vLstfiltro)
                {
                    string strValue = "";
                    int intIndeOf = 0;

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
                            filtro = filtro + " AND (convert(varchar,t." + item.NOMBRE_CAMPO + ",103)='" + words[contador] + "' or '" + words[contador] + "'='')";
                        }
                        else
                        {
                            if (item.TIPO_CONTROL.Trim().Equals("0"))
                            {
                                //txt
                                strValue = words[contador];
                                strValue = strValue.Replace("'", "''");
                                strValue = strValue.Replace("--Seleccione--", "");
                                intIndeOf = strValue.IndexOf("*");

                                if (strValue.Length != 0)
                                {
                                    if (intIndeOf >= 0)
                                    {
                                        filtro = filtro + " AND (t." + item.NOMBRE_CAMPO + " like '" + strValue.Replace("*", "%") + "')";
                                    }
                                    else
                                    {
                                        filtro = filtro + " AND (t." + item.NOMBRE_CAMPO + "='" + strValue + "')";
                                    }
                                }
                            }
                            else
                            {
                                //combo                              
                                strValue = words[contador];
                                strValue = strValue.Replace("'", "''");
                                strValue = strValue.Replace("--Seleccione--", "");
                                if (strValue.Length != 0)
                                {
                                    filtro = filtro + " AND (t." + item.NOMBRE_CAMPO + "='" + strValue + "')";
                                }
                            }
                        }
                        contador = contador + 1;
                    }
                    else
                    {
                        if (item.TIPO_DATO.ToUpper().Equals("DATETIME"))
                        {
                            strValue = words[contador];
                            if (strValue.Length != 0)
                            {
                                filtro = filtro + " AND (convert(varchar,t." + item.NOMBRE_CAMPO + ",103)='" + strValue + "')";
                            }

                            //filtro = filtro + " AND (convert(varchar,t." + item.NOMBRE_CAMPO + ",103)='" + words[contador] + "' or '" + words[contador] + "'='')";
                        }
                        else
                        {

                            if (item.TIPO_CONTROL.Trim().Equals("0"))
                            {
                                //txt
                                strValue = words[contador];
                                strValue = strValue.Replace("'", "''");
                                strValue = strValue.Replace("--Seleccione--", "");
                                intIndeOf = strValue.IndexOf("*");

                                if (strValue.Length != 0)
                                {
                                    if (intIndeOf >= 0)
                                    {
                                        filtro = filtro + " AND (t." + item.NOMBRE_CAMPO + " like '" + strValue.Replace("*", "%") + "')";
                                    }
                                    else
                                    {
                                        filtro = filtro + " AND (t." + item.NOMBRE_CAMPO + "='" + strValue + "')";
                                    }
                                }
                            }
                            else
                            {
                                //combo
                                strValue = words[contador];
                                strValue = strValue.Replace("'", "''");
                                strValue = strValue.Replace("--Seleccione--", "");
                                if (strValue.Length != 0)
                                {
                                    filtro = filtro + " AND (t." + item.NOMBRE_CAMPO + "='" + strValue + "')";
                                }
                            }
                        }
                        contador = contador + 1;
                    }
                }

                if (filtro == "where ") filtro = "where 1=1 ";

                DataTable dt = new DataTable();
                DataTable dtExcel = new DataTable();
                dt = objDocumentoBL.ObtenerListadoDinamico(select, filtro, objDocumentoEL.TABLA, true);
                dtExcel = objDocumentoBL.ObtenerListadoDinamico(select, filtro, objDocumentoEL.TABLA, false);

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
                cantidadRegistros = objDocumentoBL.ObtenerCantidadRegistros(filtro, objDocumentoEL.TABLA);
                Session["DataTableDocumento"] = dtExcel;

                //return Json(listado, JsonRequestBehavior.AllowGet);
                return this.Json(new
                {
                    listado = listado,
                    cantidadRegistros = cantidadRegistros,
                    //CodigoFile = 1
                }, JsonRequestBehavior.AllowGet);
            }
            Session[Constantes.LISTA.Value] = null;
            Session[Constantes.LISTA.Value] = vLstTD1;
            //return Json(vLstTD1, JsonRequestBehavior.AllowGet);
            return this.Json((object)new
            {
                listado = vLstTD1,
                cantidadRegistros = cantidadRegistros,
                //CodigoFile = 1
            }, JsonRequestBehavior.AllowGet);
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

            PermisosUsuario();
            RefrescarCache();
            int codPerfil = Convert.ToInt32(ViewData["LOGUEO_PERFIL"].ToString());
            //ViewBag.TipoDocumento = new ParametroController().SelectListTiposDocumentos();
            ViewBag.TipoDocumento = new ParametroController().SelectListTiposDocumentosporPerfil(codPerfil);
            ViewBag.Sexo = new ParametroController().SelectListSexo();
            return View();
        }

        private void PermisosUsuario()
        {
            ViewData["hdnVerDocumento"] = SesionActual.Current.IN_ESTA_VIEW;
            ViewData["hdnEliminarDocumento"] = SesionActual.Current.IN_ESTA_ELI;
            ViewData["hdnEditarDocumento"] = SesionActual.Current.IN_ESTA_EDIT;
            ViewData["hdnExportarPDF"] = SesionActual.Current.IN_ESTA_EXPORT;
            ViewData["hdnCantidadMaxImportarPdfs"] = ConfigurationManager.AppSettings["keyCantidadMaxImportarPdfs"].ToString();
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

                    //Auditoria
                    string strNameToc = this.Obtener_Name_Toc(intCodigoFile);
                    Util.RegistrarAuditoriaDS(intCodigoFile, strNameToc, Funciones.CheckInt(strIdUser), Funciones.CheckStr(Util.EventName.VerImagen));
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
        private string Obtener_Name_Toc(int tocId)
        {
            string strName = "";
            IDocumentoBL objDocumentoBL = new DocumentoBL();
            List<Tbl_Toc> list = new List<Tbl_Toc>();
            list = objDocumentoBL.Tbl_toc_Get_TocId(tocId);
            if (list.Count > 0)
                strName = list[0].Name;
            else
                strName = "";

            return strName;
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
            int intTocId = 0;
            int intDocId = 0;

            var resultDoc = "0";

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
                    IDocumentoBL objDocumentoBL = new DocumentoBL();
                    List<TBL_DOCEL> listDoc = objDocumentoBL.Tbl_doc_Get_TocId(tocId, 1);
                    if (listDoc.Count == 1)
                    {
                        intTocId = listDoc[0].TocId;
                        intDocId = listDoc[0].PageId;
                    }

                    //Auditoria
                    string strNameToc = this.Obtener_Name_Toc(tocId);
                    Util.RegistrarAuditoriaDS(tocId, strNameToc, Funciones.CheckInt(SesionActual.Current.IN_CODIGO_USU), Funciones.CheckStr(Util.EventName.IndexacionActualizado));

                    var resultData = new { PageId = intDocId, TocId = intTocId };
                    return Json(resultData, JsonRequestBehavior.AllowGet);


                    /*
                    return this.Json((object)new
                    {
                        result,
                    }, JsonRequestBehavior.AllowGet);
                    */
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

        [HttpPost]
        public JsonResult ElminarRegistro(int tocId)
        {
            try
            {
                IDocumentoBL objDocumentoBL = new DocumentoBL();

                var result = objDocumentoBL.ActualizarTocFlagsTblToc(tocId);

                //Auditoria
                string strNameToc = this.Obtener_Name_Toc(tocId);
                Util.RegistrarAuditoriaDS(tocId, strNameToc, Funciones.CheckInt(SesionActual.Current.IN_CODIGO_USU), Funciones.CheckStr(Util.EventName.Eliminar));

                return this.Json((object)new
                {
                    Value = result
                }, JsonRequestBehavior.AllowGet); ;
            }
            catch (Exception)
            {

                return this.Json((object)new
                {
                    Value = "NOOK",
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [SessionExpireFilter]
        public ActionResult ExportarExcelDocumento()
        {

            var dataValue = Session["DataTableDocumento"];

            this.Response.ClearContent();
            this.Response.Buffer = true;
            this.Response.AddHeader("content-disposition", "attachment;filename=documento.xls");
            this.Response.AddHeader("Content-Type", "application/vnd.ms-excel");
            this.Response.ContentEncoding = Encoding.UTF8;
            this.Response.Charset = "";
            ExportExcel.ExportarExcelDimanico(TitulosCabeceraExcel.DOCUMENTO.Value, (DataTable)dataValue, this.Response.Output);
            this.Response.BinaryWrite(Encoding.UTF8.GetPreamble());
            this.Response.Flush();
            this.Response.End();
            //Util.RegistrarAuditoria("Documento", "Click", "Exportar excel correctamente", SesionActual.Current.NOMBRE_COMPLETO, SesionActual.Current.IN_CODIGO_USU);

            string strRespuesta = "";
            return Json(new { strRespuesta = strRespuesta });
        }

        public ActionResult ValidateRequestDownload(string paramKey, List<int> data)
        {
            string strEncode64User = string.Empty;
            string strCodRpta = "0";
            object objData;

            try
            {
                strEncode64User = EncodingForBase64.Base64Encode(paramKey);
                string strIdsFile = "";
                foreach (var item in data) strIdsFile += item + "&";
                string strEncodeIdFile = EncodingForBase64.Base64Encode(strIdsFile);

                string stUrlPdf = ConfigurationManager.AppSettings["RutaPrincipal"] + "Documento/DescargarPDFMasivo?";
                objData = new LineData { CodRpta = strCodRpta, MsgRpta = "OK", IdUrl = strEncode64User, UrlPdf = stUrlPdf, CodigoFile = strEncodeIdFile };

            }
            catch (Exception ex)
            {
                strCodRpta = "1";
                objData = new LineData { CodRpta = strCodRpta, MsgRpta = ex.Message, IdUrl = strEncode64User, UrlPdf = string.Empty, CodigoFile = string.Empty };
            }

            return Json(objData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DescargarPDFMasivo()
        {

            try
            {
                if (Request.QueryString["IdFileKey"] == null || Request.QueryString["IdUserKey"] == null)
                {
                    TempData["yourMessage"] = "Problema al abrir el documento electrónico PDF. (Codigo de error = 1)";
                    return RedirectToAction("ViewErrorPDF", "BusquedaDocumento");
                }

                string strIdFileRequest = Request.QueryString["IdFileKey"];
                string strIdUserRequest = Request.QueryString["IdUserKey"];

                if (EncodingForBase64.Base64Decode(strIdFileRequest) == null || EncodingForBase64.Base64Decode(strIdUserRequest) == null)
                {
                    TempData["yourMessage"] = "Problema al abrir el documento electrónico PDF. (Codigo de error = 2)";
                    return RedirectToAction("ViewErrorPDF", "BusquedaDocumento");
                }

                string strCadenaIds = EncodingForBase64.Base64Decode(strIdFileRequest);
                string strIdUserEncy = EncodingForBase64.Base64Decode(strIdUserRequest);
                string strIdUser = EncryptionHelper.Decrypt(strIdUserEncy);

                if (strIdUser != SesionActual.Current.IN_CODIGO_USU.ToString())
                {
                    TempData["yourMessage"] = "Problema al abrir el documento electrónico PDF. (Codigo de error = 3)";
                    return RedirectToAction("ViewErrorPDF", "BusquedaDocumento");
                }

                List<string> listPdf = new List<string>();
                string[] arrIds = strCadenaIds.Split('&');

                for (int i = 0; i < arrIds.Length; i++)
                {
                    if (arrIds[i] != "")
                    {
                        int intCodigoFile = Convert.ToInt32(arrIds[i]);

                        DocumentoBL objDocBL = new DocumentoBL();
                        var objDocumentoEL = new DocumentoEL();
                        objDocumentoEL = objDocBL.ObtenerInfoDoc(intCodigoFile, 1, 1);

                        string strNombreArchivo = objDocumentoEL.NOMBRE_DOCUMENTO.Substring(objDocumentoEL.NOMBRE_DOCUMENTO.LastIndexOf(@"\") + 1);
                        string path = objDocumentoEL.NOMBRE_DOCUMENTO;
                        string filePath = path;

                        if (System.IO.File.Exists(filePath))
                        {
                            listPdf.Add(filePath);
                        }
                    }
                }

                if (listPdf.Count > 0)
                {
                    using (ZipFile zip = new ZipFile())
                    {
                        zip.AlternateEncodingUsage = ZipOption.AsNecessary;
                        zip.AddDirectoryByName("Files");
                        foreach (string strPdf in listPdf)
                        {
                            zip.AddFile(strPdf, "Files");
                        }

                        Response.Clear();
                        Response.BufferOutput = false;
                        string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
                        Response.ContentType = "application/zip";
                        Response.AddHeader("content-disposition", "attachment; filename=" + zipName);
                        zip.Save(Response.OutputStream);
                        Response.End();
                    }
                }
                else
                {
                    TempData["yourMessage"] = "Error al acceder al documento. (Codigo de error = 6)";
                    return RedirectToAction("ViewErrorPDF", "BusquedaDocumento");
                }
            }
            catch (Exception ex)
            {
                Documentos_Log.Log_WriteLog(ex.Message, typeof(DocumentoController).ToString());
                Documentos_Log.Log_WriteLog(ex.Source, typeof(Controller).ToString());
                Documentos_Log.Log_WriteLog(ex.StackTrace, typeof(DocumentoController).ToString());

                TempData["yourMessage"] = "Acceso denegado al abrir el documento. (Codigo de error = 5)";
                return RedirectToAction("ViewErrorPDF", "BusquedaDocumento");
            }

            Util.RegistrarAuditoria("Consulta Documento", "DescargarPDFMasivo", "Descarga de archivos correctamente", SesionActual.Current.NOMBRE_COMPLETO, SesionActual.Current.IN_CODIGO_USU);
            string strRespuesta = "";
            return Json(new { strRespuesta = strRespuesta });
        }
    }
}

