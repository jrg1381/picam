using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using RasPiCam.Models;
using System.Linq;
using RasPiCam.AzureBlob;

namespace RasPiCam.Controllers
{
    [System.Web.Http.Authorize]
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

        [System.Web.Http.AcceptVerbs("GET")]
        public JsonResult<IEnumerable<IVideo>> Videos()
        {
            return Json(m_blobEnumerator.Videos());
        }

        [System.Web.Http.AcceptVerbs("GET")]
        public JsonResult<IEnumerable<IVideo>> Videos(int start, int end)
        {
            var startTime = Conversions.UnixTimestampToDateTime(start);
            var endTime = Conversions.UnixTimestampToDateTime(end);

            return Json(m_blobEnumerator.Videos().Where(v => v.Timestamp >= startTime && v.Timestamp <= endTime));
        }

        [System.Web.Mvc.AcceptVerbs(HttpVerbs.Post)]
        public JsonResult<ResultWithException> Delete(string name)
        {
            try
            {
                var realName = Conversions.Base64ToUtf8String(name);
                m_blobEnumerator.Delete(realName);
                return Json(new ResultWithException { Result = true, Exception = null });
            }
            catch (Exception e)
            {
                return Json(new ResultWithException { Result = false, Exception = e });
            }
        }
    }
}