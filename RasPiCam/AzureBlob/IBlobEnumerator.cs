using System.Collections.Generic;
using RasPiCam.Models;

namespace RasPiCam.AzureBlob
{
    internal interface IBlobEnumerator
    {
        IEnumerable<IVideo> Videos();
    }
}