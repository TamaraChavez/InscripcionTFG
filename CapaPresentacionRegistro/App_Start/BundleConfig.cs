using System.Web;
using System.Web.Optimization;

namespace CapaPresentacionRegistro
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new Bundle("~/bundles/complementos").Include(
                     "~/Scripts/fontawesome/all.min.js", //obtenida de paquetes de nuget
                     "~/Scripts/DataTables/jquery.dataTables.js", //obtenida de paquetes de nuget
                      "~/Scripts/DataTables/dataTables.responsive.js", //obtenida de paquetes de nuget
                      "~/Scripts/loadingoverlay/loadingoverlay.min.js", //copie el cdn de internet
                      "~/Scripts/sweetalert.min.js", //obtenida de paquetes de nuget
                      "~/Scripts/scripts.js"
                       ));

            /*bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información sobre los formularios.  De esta manera estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));*/

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                         "~/Scripts/bootstrap.bundle.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/Site.css",
                         "~/Content/DataTables/css/jquery.dataTables.css", //obtenida de paquetes de nuget
                         "~/Content/DataTables/css/responsive.dataTables.css", //obtenida de paquetes de nuget
                          "~/Content/sweetalert.css", //obtenida de paquetes de nuget
                          "~/Content/EstilosPeriodoInscripcion.css"
                        ));
        }
    }
}
