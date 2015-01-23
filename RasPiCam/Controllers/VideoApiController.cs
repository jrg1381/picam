using System;
using System.Collections.Generic;
using System.Web;
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

        [System.Web.Http.HttpPost]
        public JsonResult<ResultWithException> Delete(FormModel model)
        {
            try
            {
               // var name = collection["name"];
                var realName = Conversions.Base64ToUtf8String(model.Name);
                m_blobEnumerator.Delete(realName);
                return Json(ResultWithException.Success);
            }
            catch (Exception e)
            {
                return Json(new ResultWithException(e));
            }
        }
    }

    public class FormModel
    {
       public string Name { get; set; }
    }
}