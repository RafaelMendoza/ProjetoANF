using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace ProjetoANFCore.Entities
{
    [Serializable]
    public class ApiException : Exception
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public ApiException()
        {
        }

        public ApiException(string message) : base(message)
        {
        }

        public ApiException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public ApiException(string title, string description) : base(title)
        {
            Title = title;
            Description = description;
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        protected ApiException(SerializationInfo info, StreamingContext context)
        {
            Title = info.GetString("Title");
            Description = info.GetString("Description");
        }

        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue("Title", Title);
            info.AddValue("Description", Description);

            base.GetObjectData(info, context);
        }
    }
}
