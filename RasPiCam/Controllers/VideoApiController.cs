using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using RasPiCam.Models;
using System.Linq;
using RasPiCam.AzureBlob;

namespace RasPiCam.Controllers
{
    public class VideoApiController : ApiController
    {
        private readonly IBlobEnumerator m_blobEnumerator;

        public VideoApiController()
        {
            m_blobEnumerator = BlobEnumeratorFactory.CreateInstance();
        }

        [AcceptVerbs("GET")]
        public JsonResult<IEnumerable<IVideo>> Videos()
        {
            return Json(m_blobEnumerator.Videos());
        }

        [AcceptVerbs("GET")]
        public JsonResult<IEnumerable<IVideo>> Videos(DateTime start, DateTime end)
        {
            return Json(m_blobEnumerator.Videos().Where(v => v.Timestamp >= start && v.Timestamp <= end));
        }
    }
}