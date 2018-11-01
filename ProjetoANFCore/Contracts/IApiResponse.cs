using System.Net;
using System.Net.Http;

namespace ProjetoANFCore.Contracts
{
    public interface IApiResponse<T>
    {
      HttpRequestMessage Request { get; set; }
      string ErrorMessage { get; set; }
      string ReasonPhrase { get; set; }
      HttpStatusCode StatusCode { get; set; }
      T ResponseBody { get; set; }
      bool IsSuccessStatusCode { get; }
    }
}
