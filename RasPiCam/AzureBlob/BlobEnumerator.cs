using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using RasPiCam.Controllers;
using RasPiCam.Models;

namespace RasPiCam.AzureBlob
{
    class BlobEnumerator : IBlobEnumerator
    {
        private const string c_blobContainer = "processeddata";
        private readonly CloudStorageAccount m_storageAccount;

        internal BlobEnumerator()
        {
            var cloudStorageConnectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");
            m_storageAccount = CloudStorageAccount.Parse(cloudStorageConnectionString);
        }

        public IEnumerable<IVideo> Videos()
        {
            var videosFound = new List<IVideo>();

            var blobClient = m_storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(c_blobContainer);

            foreach (IListBlobItem item in container.ListBlobs(null, false, BlobListingDetails.None | BlobListingDetails.Metadata))
            {
                var blockBlob = item as CloudBlockBlob;
                if (blockBlob == null) continue;
                var lastModifiedUtc = blockBlob.Properties.LastModified.HasValue ? blockBlob.Properties.LastModified.Value.UtcDateTime : DateTime.MinValue;
                videosFound.Add(new Video(blockBlob.Name, blockBlob.Properties.Length, blockBlob.Metadata, lastModifiedUtc));
            }

            return videosFound;
        }

        public Uri TemporaryUrlForBlob(string name)
        {
            var blobClient = m_storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(c_blobContainer);

            var blob = container.GetBlobReferenceFromServer(name);

            var policy = new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.Now.AddMinutes(1)
            };

            var signature = blob.GetSharedAccessSignature(policy);

            return new Uri(blob.Uri, signature);
        }

        public void Delete(string name)
        {
                var blobClient = m_storageAccount.CreateCloudBlobClient();
                var container = blobClient.GetContainerReference(c_blobContainer);

                var blob = container.GetBlobReferenceFromServer(name);
                blob.Delete();
        }
    }
}