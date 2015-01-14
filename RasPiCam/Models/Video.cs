using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace RasPiCam.Models
{
    public class Video
    {
        private readonly string m_name;
        private readonly long m_filesize;

        public Video(string name, long filesize)
        {
            m_name = name;
            m_filesize = filesize;
        }

        public string Filename
        {
            get { return m_name; }
        }

        public long Size
        {
            get {  return m_filesize; }
        }
    }
}