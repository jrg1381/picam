using System.Web.Optimization;

namespace RasPiCam
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/ui")
                .Include("~/Scripts/jquery-{version}.js")
                .Include("~/Scripts/bootstrap.js"));
        }
    }
}