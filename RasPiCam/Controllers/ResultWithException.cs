using System;

namespace RasPiCam.Controllers
{
    [Serializable]
    public class ResultWithException
    {
        public bool Result { get; set; }
        public Exception Exception { get; set; }
    }
}