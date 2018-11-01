using ProjetoANFCore.Contracts;
using ProjetoANFCore.Entities;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ProjetoANFCore.Helpers
{
    public class ApiCaller
    {
        private readonly string _requestUri;
        private readonly HttpClient _httpClient;

        private ApiCaller(string serviceBaseUri, string requestUri)
        {
            _httpClient = new HttpClient();
            _requestUri = requestUri;

            InitializeClient(_httpClient, serviceBaseUri);
        }

        public static ApiCaller FromServiceEndpoint(string serviceBaseUri, string requestUri)
        {
            return new ApiCaller(serviceBaseUri, requestUri);
        }

        private static void InitializeClient(HttpClient httpClient, string serviceBaseUri)
        {
            httpClient.BaseAddress = new Uri(serviceBaseUri);
        }

        private ApiResponse<T> GetApiResponse<T>(HttpResponseMessage response)
        {
            if(response.IsSuccessStatusCode)
            {
                var apiResponse = new ApiResponse<T>
                {
                    Request = response.RequestMessage,
                    StatusCode = response.StatusCode,
                    ReasonPhrase = response.ReasonPhrase,
                    IsSuccessStatusCode = response.IsSuccessStatusCode,
                    ResponseBody = (typeof(T) == typeof(string))
                   ? (T)Convert.ChangeType(response.Content.ReadAsStringAsync().Result, typeof(T))
                   : response.Content.ReadAsAsync<T>().Result
                };

                return apiResponse;
            }
            else
            {
                var apiResponse = new ApiResponse<T>
                {
                    Request = response.RequestMessage,
                    StatusCode = response.StatusCode,
                    ReasonPhrase = response.ReasonPhrase,
                    ErrorMessage = response.Content.ReadAsStringAsync().Result
                };

                return apiResponse;
            }
        }

        public ApiCaller WithJsonContent()
        {
            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return this;
        }

        public async Task<ApiResponse<T>> GetAsync<T>()
        {
            var response = await _httpClient.GetAsync(_requestUri);

            return GetApiResponse<T>(response);
        }

        public async Task<ApiResponse<T>> PostAsync<T>(T entity)
        {
            var response = await _httpClient.PostAsJsonAsync(_requestUri, entity);

            return GetApiResponse<T>(response);
        }

        public async Task<ApiResponse<T>> PutAsync<T>(T entity)
        {
            var response = await _httpClient.PutAsJsonAsync(_requestUri, entity);

            return GetApiResponse<T>(response);
        }
    }
}
