using System;
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
        public System.Web.Http.Results.JsonResult<List<IVideo>> Videos()
        {
            var videosFound = new List<IVideo>();

            // Retrieve storage account from connection string.
            var cloudStorageConnectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");
            if (cloudStorageConnectionString == null)
            {
                videosFound.Add(new Video("01-20101201121307.avi",44,new Uri("http://foo/")));
                videosFound.Add(new Video("01-20131201121308.avi", 44, new Uri("http://foo/")));
                videosFound.Add(new Video("01-20111201121308.avi", 44, new Uri("http://foo/")));
                return Json(videosFound);
            }

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(cloudStorageConnectionString);
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