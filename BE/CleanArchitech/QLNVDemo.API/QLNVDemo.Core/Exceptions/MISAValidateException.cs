using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMISDemo.Core.Exceptions
{
    public class MISAValidateException:Exception
    {
        public string MsgError = string.Empty;
        public HttpStatusCode StatusCode;
        public Dictionary<string, string[]>? Errors;
        public MISAValidateException( string error)
        { 
            this.MsgError = error;
        }
        public MISAValidateException(HttpStatusCode statusCode, string error)
        { 
            this.StatusCode = statusCode;
            this.MsgError = error;
        }
        public MISAValidateException(HttpStatusCode statusCode, Dictionary<string, string[]> error)
        {
            this.StatusCode = statusCode;
            this.Errors = error;
        }
        public MISAValidateException(HttpStatusCode statusCode,string message, Dictionary<string, string[]> error)
        {
            this.StatusCode = statusCode;
            this.Errors = error;
            this.MsgError = message;
        }
        public override string Message => this.MsgError;

    }
}
