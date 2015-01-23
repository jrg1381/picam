using System;
using System.Runtime.Serialization;
using System.Text;

namespace RasPiCam.Controllers
{
    [Serializable]
    public class ResultWithException
    {
        public bool Result { get; private set; }
        public Type ExceptionClass { get; private set; }
        public string ExceptionMessage { get; private set; }
        private static readonly ResultWithException s_success = new ResultWithException {Result = true};

        public static ResultWithException Success
        {
            get { return s_success; }
        }

        private ResultWithException()
        {
            
        }

        public ResultWithException(Exception ex)
        {
            Result = false;
            if (ex == null)
                return;
            ExceptionClass = ex.GetType();
            ExceptionMessage = AllMessages(ex);
        }

        private static string AllMessages(Exception ex)
        {
            var sb = new StringBuilder(ex.Message);

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;
                sb.Append(",");
                sb.Append(ex.Message);
            }

            return sb.ToString();
        }
    }
}