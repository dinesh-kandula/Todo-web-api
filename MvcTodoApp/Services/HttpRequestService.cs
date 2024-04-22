using Newtonsoft.Json;
using System.Text;
using TodoModels.Models;

namespace MvcTodoApp.Services
{
    public class HttpRequestService : IHttpRequestService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpRequestService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public Stream GetRequest(string endPoint)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, endPoint);
            var response = _httpClient.Send(request);
            response.EnsureSuccessStatusCode();
            var responseStream = response.Content.ReadAsStream();
            return responseStream;
        }

        public async Task<HttpResponseMessage> PostRequest(string endPoint, User data)
        {
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(data);

            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_configuration["API:URL"]}/{endPoint}"),
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            var response = await _httpClient.SendAsync(request);
            return response;
        }
    }
}
