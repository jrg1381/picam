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
        private readonly long m_filesize;
        private readonly DateTime m_timestamp;
        private readonly string m_encodedFilename;
        private readonly Dictionary<string,string> m_metadata;

        public Video(string name, long filesize, IDictionary<string,string> metadata, DateTime lastModifiedUtc)
        {
            m_filesize = filesize;
            m_metadata = new Dictionary<string, string>(metadata);
            m_timestamp = lastModifiedUtc;
            m_encodedFilename = Conversions.Utf8StringToBase64(name);
        }

        public string EncodedFilename
        {
            get { return m_encodedFilename; }
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