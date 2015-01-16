using System;

namespace RasPiCam.Controllers
{
    static class Conversions
    {
        static readonly DateTime s_epoch = new DateTime(1970,1,1,0,0,0,0,DateTimeKind.Utc);

        public static DateTime UnixTimestampToDateTime(int timestamp)
        {
            return s_epoch.AddSeconds(timestamp);
        }
    }
}