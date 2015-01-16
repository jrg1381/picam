using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure;
using RasPiCam.AzureBlob;
using RasPiCam.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace RasPiCam.Controllers
{
    public class DefaultController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Video(string id)
        {
            var realName = Conversions.Base64ToUtf8String(id);
            var blobEnumerator = BlobEnumeratorFactory.CreateInstance();
            return Redirect(blobEnumerator.TemporaryUrlForBlob(realName).ToString());
        }
    }
}