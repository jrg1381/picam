using Microsoft.WindowsAzure;
using RasPiCam.Controllers;

namespace RasPiCam.AzureBlob
{
    static class BlobEnumeratorFactory
    {
        public static IBlobEnumerator CreateInstance()
        {
            var cloudStorageConnectionString = CloudConfigurationManager.GetSetting("StorageConnectionString");

            if (cloudStorageConnectionString == null)
            {
                return new MockBlobEnumerator();
            }
            else
            {
                return new BlobEnumerator();
            }
        }
    }
}