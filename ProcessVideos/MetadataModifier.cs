using System;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ProcessVideos
{
    class MetadataModifier : IDisposable
    {
        public const string BlobContainer = "data";
        private readonly ICloudBlob m_blob;

        public MetadataModifier(string blobName)
        {
            var cloudStorageConnectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");
            var storageAccount = CloudStorageAccount.Parse(cloudStorageConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(BlobContainer);
            m_blob = container.GetBlobReferenceFromServer(blobName);
        }

        public string this[string key]
        {
            get { return m_blob.Metadata[key]; }
            set { m_blob.Metadata[key] = value; }
        }

        public void Dispose()
        {
            m_blob.SetMetadata();
        }
    }
}