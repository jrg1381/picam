using System.Collections.Generic;
using System.Web.Http;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using RasPiCam.Models;

namespace RasPiCam.Controllers
{
    public class VideoApiController : ApiController
    {
        private const string c_blobContainer = "data";

        [AcceptVerbs("GET")]
        public System.Web.Http.Results.JsonResult<List<Video>> Videos()
        {
            var videosFound = new List<Video>();

            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(c_blobContainer);

            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                var blockBlob = item as CloudBlockBlob;
                if (blockBlob == null) continue;
                videosFound.Add(new Video(blockBlob.Name, blockBlob.Properties.Length, blockBlob.SnapshotQualifiedUri));
            }

            return Json(videosFound);
        }
    }
}