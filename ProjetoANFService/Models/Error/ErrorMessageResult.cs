using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace ProjetoANFService.Models.Error
{
    public class ErrorMessageResult : IHttpActionResult
    {

        #region Attributes
        #endregion

        #region Properties

        public readonly HttpRequestMessage request;
        public readonly string errorMessage;
        public readonly string reasonPhrase;
        public readonly HttpStatusCode statusCode;


        #endregion

        #region Constants
        #endregion

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_request"></param>
        /// <param name="_errorMessage"></param>
        /// <param name="_reasonPhrase"></param>
        /// <param name="_statusCode"></param>
        public ErrorMessageResult(HttpRequestMessage _request, string _errorMessage, string _reasonPhrase, HttpStatusCode _statusCode)
        {
            request = _request;
            errorMessage = _errorMessage;
            reasonPhrase = _reasonPhrase;
            statusCode = _statusCode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_errorMessage"></param>
        /// <param name="_reasonPhrase"></param>
        /// <param name="_statusCode"></param>
        public ErrorMessageResult(string _errorMessage, string _reasonPhrase, HttpStatusCode _statusCode)
        {
            errorMessage = _errorMessage;
            reasonPhrase = _reasonPhrase;
            statusCode = _statusCode;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="_errorMessage"></param>
        /// <param name="_reasonPhrase"></param>
        /// <param name="_statusCode"></param>
        public ErrorMessageResult(HttpRequestMessage _request, string _errorMessage, string _reasonPhrase)
        {
            errorMessage = _errorMessage;
            reasonPhrase = _reasonPhrase;
            request = _request;
        }

        #endregion

        #region Actions

        #region Get
        #endregion

        #region Post
        #endregion

        #region Put
        #endregion

        #endregion

        #region Methods

        #region Private
        #endregion

        #region Public
        #endregion

        #region Protected

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage()
            {
                Content = new StringContent(errorMessage, Encoding.UTF8, "application/json"),
                ReasonPhrase = reasonPhrase,
                RequestMessage = request,
                StatusCode = statusCode,
            };
            return Task.FromResult(response);
        }

        #endregion

        #endregion

        #region Events
        #endregion
    }
}