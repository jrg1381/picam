using System;
using System.Collections;
using System.Collections.Generic;

namespace RasPiCam.Models
{
    public interface IVideo
    {
        long Size { get; }
        DateTime Timestamp { get; }
        IDictionary<string,string> Metadata { get; }
        string EncodedFilename { get; }
    }
}