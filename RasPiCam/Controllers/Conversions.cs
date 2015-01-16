using System;
using System.Text;

namespace RasPiCam.Controllers
{
    static class Conversions
    {
        static readonly DateTime s_epoch = new DateTime(1970,1,1,0,0,0,0,DateTimeKind.Utc);

        public static DateTime UnixTimestampToDateTime(int timestamp)
        {
            return s_epoch.AddSeconds(timestamp);
        }

        public static string Utf8StringToBase64(string s)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
        }

        public static string Base64ToUtf8String(string data)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(data));
        }
    }
}