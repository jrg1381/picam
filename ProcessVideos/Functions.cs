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
        public static void ProcessQueueMessage([BlobTrigger(MetadataModifier.BlobContainerInput + "/{name}")] Stream blob, 
            [Blob(MetadataModifier.BlobContainerOutput + "/{name}", FileAccess.Write)] Stream blobOutput,
            string name)
        {
            blob.CopyTo(blobOutput);
            blobOutput.Flush();
            blobOutput.Close();
            blob.Seek(0, SeekOrigin.Begin);

            using (var meta = new MetadataModifier(MetadataModifier.BlobContainerOutput, name))
            {
                if (meta["processed"] != null && (bool) meta["processed"]) return;

                try
                {
                    using (var mediaFile = new MediaInfoDotNet.MediaFile(blob))
                    {
                        var video = mediaFile.Video.First().Value;
                        meta["duration"] = video.duration;
                        meta["resolution"] = String.Format("{0}x{1}", video.width, video.height);
                    }
                }
                catch (Exception e)
                {
                    meta["exception"] = e;
                }

                meta["processed"] = true;
                meta["lastModified"] = LastModifiedTimeUtc(MetadataModifier.BlobContainerInput, name);
            }

            DeleteBlob(MetadataModifier.BlobContainerInput, name);
        }

        private static void DeleteBlob(string containerName, string blobName)
        {
            var cloudStorageConnectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");
            var storageAccount = CloudStorageAccount.Parse(cloudStorageConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);

            var blob = container.GetBlobReferenceFromServer(blobName);
            blob.Delete();
        }

        private static DateTime LastModifiedTimeUtc(string containerName, string blobName)
        {
            var cloudStorageConnectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");
            var storageAccount = CloudStorageAccount.Parse(cloudStorageConnectionString);
            var blobClient = storageAccount.CreateCloudBlobClient();
            var container = blobClient.GetContainerReference(containerName);
            var blob = container.GetBlobReferenceFromServer(blobName);
            return blob.Properties.LastModified.HasValue ? blob.Properties.LastModified.Value.UtcDateTime : DateTime.MinValue;
        }
    }
}
