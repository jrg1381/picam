﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.WindowsAzure;
using RasPiCam.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;

namespace RasPiCam.Controllers
{
    public class DefaultController : Controller
    {
        private const string c_blobContainer = "data";

        // GET: Default
        public ActionResult Index()
        {
            return View(VideosFromBlobStore().ToList());
        }

        private IEnumerable<Video> VideosFromBlobStore()
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(c_blobContainer);

            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                var blockBlob = item as CloudBlockBlob;
                if (blockBlob == null) continue;
                yield return new Video(blockBlob.Name, blockBlob.Properties.Length);
            }
        }
    }
}