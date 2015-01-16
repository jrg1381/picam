using System;
using System.Collections.Generic;
using RasPiCam.Models;

namespace RasPiCam.AzureBlob
{
    class MockBlobEnumerator : IBlobEnumerator
    {
        public IEnumerable<IVideo> Videos()
        {
            var metadata = new Dictionary<string, string>();
            metadata["foo"] = "bar";

            var videosFound = new List<IVideo> {
                new Video("01-20101201121307.avi", 44, metadata), 
                new Video("01-20131201121308.avi", 44, metadata), 
                new Video("01-20150116121308.avi", 44, metadata)};

            return videosFound;
        }


        public Uri TemporaryUrlForBlob(string name)
        {
            return new Uri("http://foo/" + name);
        }
    }
}