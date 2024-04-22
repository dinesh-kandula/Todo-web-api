using System.Net;
using System.Security.Policy;
using Newtonsoft.Json;

namespace MvcTodoApp.Services
{
    public class WebRequestService : IWebRequestService
    {
        public string WebRequestGet(string url)
        {
            var urlTest = string.Format(url);
            WebRequest request = WebRequest.Create(urlTest);
            request.Method = "GET";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string? result = null;
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                result = sr.ReadToEnd();
                sr.Close();
            }
            return result;
        }
      
        public string WebRequestPost(string url, string data)
        {
            string strUrl = string.Format(url);
            WebRequest request = WebRequest.Create(strUrl);
            request.Method = "POST";
            request.ContentType = "application/json";

            using var streamWriter = new StreamWriter(request.GetRequestStream());
            streamWriter.Write(data);
            streamWriter.Flush();
            streamWriter.Close();

            var response = (HttpWebResponse)request.GetResponse();

            using StreamReader streamReader = new(response.GetResponseStream());
            string result = streamReader.ReadToEnd();
            return result;
        }

        public string WebRequestPut(string url, string data)
        {
            string strUrl = string.Format(url);
            WebRequest request = WebRequest.Create(strUrl);
            request.Method = "PUT";
            request.ContentType = "application/json";

            using var streamWriter = new StreamWriter(request.GetRequestStream());
            streamWriter.Write(data);
            streamWriter.Flush();
            streamWriter.Close();

            var response = (HttpWebResponse)request.GetResponse();

            using StreamReader streamReader = new(response.GetResponseStream());
            string result = streamReader.ReadToEnd();
            return result;
        }

        public string WebRequestDelete(string url)
        {
            string strUrl = string.Format(url);
            WebRequest request = WebRequest.Create(strUrl);
            request.Method = "DELETE";
            HttpWebResponse? response = (HttpWebResponse)request.GetResponse();

            string? result = null;
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                result = sr.ReadToEnd();
                sr.Close();
            }
            return result;
        }
    }
}
