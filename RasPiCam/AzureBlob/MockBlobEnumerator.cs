using System;
using System.Collections.Generic;
using RasPiCam.Models;

namespace RasPiCam.AzureBlob
{
    class MockBlobEnumerator : IBlobEnumerator
    {
        public IEnumerable<IVideo> Videos()
        {
            var videosFound = new List<IVideo> {
                new Video("01-20101201121307.avi", 44, new Uri("http://foo/")), 
                new Video("01-20131201121308.avi", 44, new Uri("http://foo/")), 
                new Video("01-20150116121308.avi", 44, new Uri("http://foo/"))};

            return videosFound;
        }
    }
}