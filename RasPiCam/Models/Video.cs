using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Permissions;
using System.Text;

namespace RasPiCam.Models
{
    public class Video
    {
        private readonly string m_name;
        private readonly long m_filesize;
        private readonly DateTime m_timestamp;
        private readonly Uri m_url;

        public Video(string name, long filesize, Uri url)
        {
            m_name = name;
            m_filesize = filesize;
            var timestampText = name.Substring(name.IndexOf('-') + 1, 14);
            m_timestamp = DateTime.ParseExact(timestampText, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            m_url = url;
        }

        public string Filename
        {
            get { return m_name; }
        }

        public long Size
        {
            get {  return m_filesize; }
        }

        public DateTime Timestamp
        {
            get { return m_timestamp; }
        }

        public Uri Url
        {
            get { return m_url; }
        }
    }
}