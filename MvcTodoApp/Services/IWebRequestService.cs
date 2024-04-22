namespace MvcTodoApp.Services
{
    public interface IWebRequestService
    {

        string WebRequestGet(string url);

        string WebRequestPost(string url, string data);

        string WebRequestPut(string url, string data);

        string WebRequestDelete(string url);

    }
}
