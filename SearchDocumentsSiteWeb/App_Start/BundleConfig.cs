using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace SearchDocumentsSiteWeb.App_Start
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*", "~/Scripts/jquery.unobtrusive*"
                        ));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/Scripts/layout")
                                                .Include("~/Scripts/Documentos/General.js")
                                                .Include("~/Scripts/layout/metronic.js")
                                                .Include("~/Scripts/layout/layout.js")
                                                .Include("~/Scripts/layout/principal.js")
                                                .Include("~/Scripts/Datatable/jquery.dataTables.js")
                                                .Include("~/Scripts/Datatable/dataTables.tableTools.js")
                                                .Include("~/Scripts/jquery.loading.js")
                                                .Include("~/Scripts/tooltipster.bundle.min.js")
                                                );

            bundles.Add(new ScriptBundle("~/bundles/Scripts/layoutLogin")
                                    .Include("~/Scripts/layout/metronic.js")
                                    .Include("~/Scripts/layout/layout.js")
                                    .Include("~/Scripts/layout/principal.js")
                                    .Include("~/Scripts/Datatable/jquery.dataTables.js")
                                    .Include("~/Scripts/Datatable/dataTables.tableTools.js")
                                    .Include("~/Scripts/jquery.loading.js")
                                    .Include("~/Scripts/Documentos/UsuarioInfoPC.js")
                                    );

            bundles.Add(new StyleBundle("~/bundles/Content/cssEstandar").Include("~/Content/estilos.css")
                                            .Include("~/Content/css/custom.css")
                                            .Include("~/Content/css/layout.css")
                                            .Include("~/Content/css/light2.css")
                                            .Include("~/Content/css/plugins.css")
                                            .Include("~/Content/css/select2.css")
                                            .Include("~/Content/css/font-awesome.min.css")
                                            .Include("~/Content/css/uniform.default.css")
                                            .Include("~/Content/css/jquery.dataTables.css")
                                            .Include("~/Content/css/dataTables.tableTools.css")
                                            );

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            BundleTable.EnableOptimizations = true;
        }
    }
}