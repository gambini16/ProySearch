using SearchDocuments.Entidades.ImportarDocumento;
using SearchDocuments.Negocio.ImportarDocumento;
using SearchDocumentsSiteWeb.Controllers.Parametro;
using SearchDocumentsSiteWeb.General;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace SearchDocumentsSiteWeb.Controllers.Documento
{
    public class ImportarDocumentoController : Controller
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

        // GET: ImportarDocumento
        public ActionResult Index()
        {
            RefrescarCache();

            int codPerfil = Convert.ToInt32(ViewData["LOGUEO_PERFIL"].ToString());
            ViewBag.TipoDocumento = new ParametroController().SelectListTiposDocumentosporPerfil(codPerfil);
            return View();
        }

        [HttpPost]
        public JsonResult GuadarEnBaseDatos(Dictionary<string, string> dictImportarDocumento, int intTipoPlantilla, string nombreArchivo)
        {
            IImportarDocumentoBL objImportarDocumentoBL = new ImportarDocumentoBL();

            var objTBL_TOCEL = new TBL_TOCEL
            {
                ParentId = int.Parse(ConfigurationManager.AppSettings["consParentId"]),
                Name = nombreArchivo,
                ElType = int.Parse(ConfigurationManager.AppSettings["consElType"]),
                VolumeId = int.Parse(ConfigurationManager.AppSettings["consVolumeId"]),
                TemplateId = intTipoPlantilla,
                PageCount = int.Parse(ConfigurationManager.AppSettings["consPageCount"]),
                Creator = SesionActual.Current.NOMBRE_COMPLETO,
                Ext_File = ConfigurationManager.AppSettings["consExt_File"].ToString()
            };

            var tocId = objImportarDocumentoBL.fn_Insert_Tbl_toc(objTBL_TOCEL);

            if (int.Parse(tocId) > 0)
            {

                var objTBL_TDEL = new TBL_TDEL
                {
                    tocId = int.Parse(tocId),
                    TemplateId = intTipoPlantilla,
                    dictImportarDocumento = dictImportarDocumento
                };

                var resultTd = objImportarDocumentoBL.fn_insert_tbl_td(objTBL_TDEL);

                if (resultTd == "0")
                {
                    var objTBL_DOCEL = new TBL_DOCEL
                    {
                        TocId = int.Parse(tocId),
                        PageNum = int.Parse(ConfigurationManager.AppSettings["consPageNum"]),
                        img_size = 0,
                        txt_size = int.Parse(ConfigurationManager.AppSettings["consTxt_size"]),
                        img_width = int.Parse(ConfigurationManager.AppSettings["consImg_width"]),
                        img_height = int.Parse(ConfigurationManager.AppSettings["consImg_height"]),
                        img_xdpi = int.Parse(ConfigurationManager.AppSettings["consImg_xdpi"]),
                        img_ydpi = int.Parse(ConfigurationManager.AppSettings["consImg_ydpi"]),
                        img_bpp = ConfigurationManager.AppSettings["consImg_bpp"].ToString()
                    };

                    var result = objImportarDocumentoBL.fn_Insert_Tbl_doc(objTBL_DOCEL);

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

        public JsonResult SubirPDF(HttpPostedFileBase fileUpload, int pageId)
        {
            try
            {
                IImportarDocumentoBL objImportarDocumentoBL = new ImportarDocumentoBL();

                var objVol = objImportarDocumentoBL.fn_Get_Vol_Get_VolumeId(1);

                string volPath = $"{objVol.FixedPathShare}\\{objVol.VolumeName}";

                if (!Directory.Exists(volPath)) Directory.CreateDirectory(volPath);
                string hex = Convert.ToInt32(pageId).ToString("X8");
                string strFolderImage = Generar_Path_Img(hex, volPath);
                if (!Directory.Exists(strFolderImage)) Directory.CreateDirectory(strFolderImage);
                string strFileImage = strFolderImage + hex + ".pdf";

                fileUpload.SaveAs(strFileImage);

            }
            catch (Exception ex)
            {
                int num2 = 0;
                Console.Write(ex.Message);
                return this.Json((object)new
                {
                    Value = false,
                    Message = ex.Message,
                    CodigoFile = num2
                }, JsonRequestBehavior.AllowGet);
            }

            return this.Json((object)new
            {
                Value = true,
                Message = "Subido con éxito",
                CodigoFile = 0
            }, JsonRequestBehavior.AllowGet);

        }

        public static string Generar_Path_Img(string _PageHex, string strFolder)
        {
            string strMyPath = "";

            string[] strArreglo = new string[4];
            strArreglo[0] = _PageHex.Substring(0, 2);
            strArreglo[1] = _PageHex.Substring(2, 2);
            strArreglo[2] = _PageHex.Substring(4, 2);
            strArreglo[3] = _PageHex.Substring(6, 2);


            string strRuta = strFolder + @"\";
            int intContador = 0;
            string strCarpeta = "";

            foreach (string strValores in strArreglo)
            {
                if (intContador != 3)
                {
                    strCarpeta += strValores + @"\";
                    strMyPath = strRuta + strCarpeta + @"\";
                    strMyPath = strMyPath.Substring(0, strMyPath.Length - 1);
                    intContador += 1;
                }
            }

            return strMyPath;
        }

        [HttpGet]
        public JsonResult Get_Data_Td_Table(string parametros, string intTipoPlantilla)
        {
            string[] value = new string[4];

            return this.Json((object)new
            {
                Value = value,
            }, JsonRequestBehavior.AllowGet);
        }
    }
}