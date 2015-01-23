using System;
using System.Collections.Generic;
using RasPiCam.Models;

namespace RasPiCam.AzureBlob
{
    public interface IBlobEnumerator
    {
        IEnumerable<IVideo> Videos();
        Uri TemporaryUrlForBlob(string name);

        void Delete(string id);
    }
}