using System.Web.Mvc;
using RasPiCam.AzureBlob;

namespace RasPiCam.Controllers
{
    public class DefaultController : Controller
    {
        private readonly IBlobEnumerator m_blobEnumerator;

        public DefaultController()
        {
            m_blobEnumerator = BlobEnumeratorFactory.CreateInstance();
        }

        public DefaultController(IBlobEnumerator blobEnumerator)
        {
            m_blobEnumerator = blobEnumerator;
        }

        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        [System.Web.Http.Authorize]
        public ActionResult Video(string id)
        {
            var realName = Conversions.Base64ToUtf8String(id);
            return Redirect(m_blobEnumerator.TemporaryUrlForBlob(realName).ToString());
        }
    }
}