using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoANFCore.Entities
{
    [Serializable]
    public class ViewException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ReasonPhrase { get; set; }
        public string ErrorMessage { get; set; }

        public ViewException() { }

        public ViewException(string message) : base(message) { }

        public ViewException(string message, Exception innerException) : base(message, innerException) { }

        public ViewException(string format, params object[] args) : base (string.Format(format, args)) { }

        public ViewException(string format, Exception innerException,params object[] args) : base(string.Format(format, args), innerException) { }

        public ViewException(HttpStatusCode statusCode, string reasonPhare, string errorMessage) : base(reasonPhare)
        {
            StatusCode = statusCode;
            ReasonPhrase = reasonPhare;
            ErrorMessage = errorMessage;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected ViewException(SerializationInfo info, StreamingContext context)
        {
            StatusCode = (HttpStatusCode)info.GetValue("StatusCode", typeof(HttpStatusCode));
            ReasonPhrase = info.GetString("ReasonPhrase");
            ErrorMessage = info.GetString("ErrorMessage");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if(info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue("StatuCode", StatusCode, typeof(HttpStatusCode));
            info.AddValue("ReasonPhrase", ReasonPhrase);
            info.AddValue("ErrorMessage", ErrorMessage);

            base.GetObjectData(info, context);
        }
    }
}
