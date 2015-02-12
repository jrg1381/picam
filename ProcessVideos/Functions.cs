using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Newtonsoft.Json;

namespace ProcessVideos
{
    public class Functions
    {
        // This function will get triggered/executed when a new blob is uploaded
        public static void ProcessQueueMessage([BlobTrigger(MetadataModifier.BlobContainer + "/{name}")] Stream blob, string name)
        {
            using (var meta = new MetadataModifier(name))
            {
                meta["processed"] = true;
                meta["lastModified"] = LastModifiedTimeUtc(name);
            }
        }

        private static DateTime LastModifiedTimeUtc(string blobName)
        {
            var cloudStorageConnectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");
            var storageAccount = CloudStorageAccount.Parse(cloudStorageConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(MetadataModifier.BlobContainer);
            var blob = container.GetBlobReferenceFromServer(blobName);
            return blob.Properties.LastModified.HasValue ? blob.Properties.LastModified.Value.UtcDateTime : DateTime.MinValue;
        }
    }
}
