using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Permissions;
using System.Text;
using RasPiCam.Controllers;

namespace RasPiCam.Models
{
    public class Video : IVideo
    {
        private readonly string m_name;
        private readonly long m_filesize;
        private readonly DateTime m_timestamp;
        private readonly string m_encodedFilename;
        private readonly IDictionary<string,string> m_metadata;

        public Video(string name, long filesize, IDictionary<string,string> metadata)
        {
            m_name = name;
            m_filesize = filesize;
            var timestampText = name.Substring(name.IndexOf('-') + 1, 14);
            m_timestamp = DateTime.ParseExact(timestampText, "yyyyMMddHHmmss", CultureInfo.InvariantCulture);
            m_metadata = metadata;
            m_encodedFilename = Conversions.Utf8StringToBase64(m_name);
        }

        public string EncodedFilename
        {
            get { return m_encodedFilename; }
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

        public IDictionary<string,string> Metadata
        {
            get { return m_metadata; }
        }
    }
}