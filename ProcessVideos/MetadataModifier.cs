using System;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;

namespace ProcessVideos
{
    class MetadataModifier : IDisposable
    {
        public const string BlobContainerInput = "data";
        public const string BlobContainerOutput = "processeddata";
        private readonly ICloudBlob m_blob;
        private bool m_wasModified;

        public MetadataModifier(string blobName)
        {
            var cloudStorageConnectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");
            var storageAccount = CloudStorageAccount.Parse(cloudStorageConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(BlobContainerInput);
            m_blob = container.GetBlobReferenceFromServer(blobName);
        }

        public object this[string key]
        {
            get
            {
                return m_blob.Metadata.ContainsKey(key) ? JsonConvert.DeserializeObject(m_blob.Metadata[key]) : null;
            }
            set
            {
                m_blob.Metadata[key] = JsonConvert.SerializeObject(value);
                m_wasModified = true;
            }
        }

        public void Dispose()
        {
            if(m_wasModified)
                m_blob.SetMetadata();
        }
    }
}