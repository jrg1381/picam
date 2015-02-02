using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace ProcessVideos
{
    public class Functions
    {
        // This function will get triggered/executed when a new blob is uploaded
        public static void ProcessQueueMessage([BlobTrigger("data/{name}")] Stream blob)
        {
            
        }
    }
}
