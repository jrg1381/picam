using System;

namespace RasPiCam.Models
{
    public interface IVideo
    {
        string Filename { get; }
        long Size { get; }
        DateTime Timestamp { get; }
        Uri Url { get; }
    }
}