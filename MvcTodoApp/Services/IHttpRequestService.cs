using TodoModels.Models;

namespace MvcTodoApp.Services
{
    public interface IHttpRequestService
    {

        Stream GetRequest(string endPoint);

        Task<HttpResponseMessage> PostRequest(string endPoint, User data);
    }
}
