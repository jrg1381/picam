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
                meta["processed"] = JsonConvert.SerializeObject(true);
            }
        }
    }
}
