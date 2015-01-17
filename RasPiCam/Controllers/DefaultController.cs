using System.Web.Mvc;
using RasPiCam.AzureBlob;

namespace RasPiCam.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Http.Authorize]
        public ActionResult Video(string id)
        {
            var realName = Conversions.Base64ToUtf8String(id);
            var blobEnumerator = BlobEnumeratorFactory.CreateInstance();
            return Redirect(blobEnumerator.TemporaryUrlForBlob(realName).ToString());
        }
    }
}