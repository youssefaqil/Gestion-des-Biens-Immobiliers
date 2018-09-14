using System.Web;
using System.Web.Optimization;

namespace Omni.Agence.WEB
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/plugins/jquery-migrate-1.2.1.min.js",
                        "~/plugins/jquery-ui/jquery-ui-1.10.3.custom.min.js",
                //"~/plugins/bootstrap/js/bootstrap.min.js",
                ////"~/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                        "~/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                        "~/plugins/jquery.blockui.min.js",
                        "~/plugins/jquery.cokie.min.js",
                        "~/plugins/uniform/jquery.uniform.min.js",
                //"~/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                        "~/plugins/select2/select2.min.js",
                        "~/plugins/datatables/media/js/jquery.dataTables.min.js",
                //"~/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js",
                        "~/scripts/metronic.js",
                        "~/admin/layout/scripts/layout.js",
                        "~/admin/layout/scripts/quick-sidebar.js",
                        "~/admin/layout/scripts/demo.js",
                        "~/admin/pages/scripts/table-editable.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));



            bundles.Add(new ScriptBundle("~/Scripts/DatePicker").Include(
                        "~/plugins/bootstrap-datepicker/js/bootstrap-datepicker.js"));
            // Utilisez la version de développement de Modernizr pour le développement et l'apprentissage. Puis, une fois
            // prêt pour la production, utilisez l'outil de génération (bluid) sur http://modernizr.com pour choisir uniquement les tests dont vous avez besoin.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                //"~/Scripts/bootstrap.js",
                      "~/plugins/bootstrap/js/bootstrap.min.js",
                      "~/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                      "~/plugins/bootstrap-switch/js/bootstrap-switch.min.js",
                      "~/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/MetronicM").Include(
                        "~/scripts/metronic.js",
                        "~/admin/layout/scripts/layout.js",
                        "~/admin/layout/scripts/quick-sidebar.js",
                        "~/admin/layout/scripts/demo.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryM").Include(
              "~/plugins/jquery-1.11.0.min.js",
              "~/plugins/jquery-migrate-1.2.1.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/DataTableM").Include(
                "~/admin/pages/scripts/table-managed.js",
                "~/plugins/select2/select2.min.js",
                "~/plugins/datatables/media/js/jquery.dataTables.min.js",
                "~/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryModal").Include(
               "~/plugins/bootstrap-modal/js/bootstrap-modalmanager.js",
               "~/plugins/bootstrap-modal/js/bootstrap-modal.js",
               "~/admin/pages/scripts/ui-extended-modals.js"
               ));


            bundles.Add(new StyleBundle("~/bundles/DatePicker").Include(
                      "~/plugins/bootstrap-datepicker/css/datepicker.css"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/components.css",
                      "~/Content/plugins.css",
                      "~/admin/layout/css/layout.css",
                      "~/admin/layout/css/themes/default.css",
                      "~/admin/layout/css/custom.css"
                      ));

            bundles.Add(new StyleBundle("~/Plugins/css").Include(
                      "~/plugins/font-awesome/css/font-awesome.min.css",
                      "~/plugins/bootstrap/css/bootstrap.min.css",
                      "~/plugins/uniform/css/uniform.default.css",
                      "~/plugins/bootstrap-switch/css/bootstrap-switch.min.css",
                      "~/plugins/select2/select2.css",
                      "~/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css"
                ));

            //bundles.Add(new StyleBundle("~/Plugins/simple-line-icons").Include(
            //      "~/plugins/simple-line-icons/simple-line-icons.min.css"
            // ));


            bundles.Add(new StyleBundle("~/Content/DataTableM").Include(
                "~/plugins/select2/select2.css",
                "~/plugins/datatables/plugins/bootstrap/dataTables.bootstrap.css"));

            bundles.Add(new StyleBundle("~/Content/cssTheme").Include(
                "~/Content/components.css",
                "~/Content/plugins.css",
                "~/admin/layout/css/layout.css",
                "~/admin/layout/css/themes/default.css",
                "~/admin/layout/css/custom.css"));
            bundles.Add(new ScriptBundle("~/bundles/bootstrapM").Include(
                      "~/plugins/jquery-ui/jquery-ui-1.10.3.custom.min.js",
                      "~/plugins/bootstrap/js/bootstrap.min.js",
                      "~/plugins/bootstrap-hover-dropdown/bootstrap-hover-dropdown.min.js",
                      "~/plugins/jquery-slimscroll/jquery.slimscroll.min.js",
                      "~/plugins/jquery.blockui.min.js",
                      "~/plugins/jquery.cokie.min.js",
                      "~/plugins/uniform/jquery.uniform.min.js",
                      "~/plugins/bootstrap-switch/js/bootstrap-switch.min.js"));


            bundles.Add(new StyleBundle("~/Content/cssModal").Include(
                "~/plugins/bootstrap-modal/css/bootstrap-modal-bs3patch.css",
                "~/plugins/bootstrap-modal/css/bootstrap-modal.css"));

        }
    }
}
