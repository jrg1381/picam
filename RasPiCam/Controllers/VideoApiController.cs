using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using RasPiCam.Models;
using System.Linq;
using RasPiCam.AzureBlob;

namespace RasPiCam.Controllers
{
    [Authorize]
    public class VideoApiController : ApiController
    {
        private readonly IBlobEnumerator m_blobEnumerator;

        public VideoApiController()
        {
            m_blobEnumerator = BlobEnumeratorFactory.CreateInstance();
        }

        public VideoApiController(IBlobEnumerator blobEnumerator)
        {
            m_blobEnumerator = blobEnumerator;
        }

        [AcceptVerbs("GET")]
        [Authorize]
        public JsonResult<IEnumerable<IVideo>> Videos()
        {
            return Json(m_blobEnumerator.Videos());
        }

        [AcceptVerbs("GET")]
        [Authorize]
        public JsonResult<IEnumerable<IVideo>> Videos(int start, int end)
        {
            var startTime = Conversions.UnixTimestampToDateTime(start);
            var endTime = Conversions.UnixTimestampToDateTime(end);

            return Json(m_blobEnumerator.Videos().Where(v => v.Timestamp >= startTime && v.Timestamp <= endTime));
        }
    }
}