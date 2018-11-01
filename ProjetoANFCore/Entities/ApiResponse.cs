using ProjetoANFCore.Contracts;
using System.Net;
using System.Net.Http;

namespace ProjetoANFCore.Entities
{
    public class ApiResponse<T> : IApiResponse<T>
    {
        public HttpRequestMessage Request { get; set; }
        public string ErrorMessage { get; set; }
        public string ReasonPhrase { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public T ResponseBody { get; set; }
        public bool IsSuccessStatusCode { get; set; }
    }
}
