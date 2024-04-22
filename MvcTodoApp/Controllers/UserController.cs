using Microsoft.AspNetCore.Mvc;
using MvcTodoApp.Models;
using MvcTodoApp.Services;
using Newtonsoft.Json;
using TodoModels.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MvcTodoApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IHttpRequestService _httpRequestService;

        public UserController(IHttpRequestService httpRequestService)
        {
            _httpRequestService = httpRequestService;
        }
        public IActionResult Index()
        {
            var responseStream = _httpRequestService.GetRequest("api/Users");

            var streamReader = new StreamReader(responseStream);
            var serializer = new JsonSerializer();
            using var jsonTextReader = new JsonTextReader(streamReader);
            List<User> users = serializer.Deserialize<List<User>>(jsonTextReader)!;
            return View(users);
        }

        public IActionResult Details(Guid Id)
        {
            var responseStream = _httpRequestService.GetRequest($"api/Users/{Id}");

            var streamReader = new StreamReader(responseStream);
            var serializer = new JsonSerializer();
            using var jsonTextReader = new JsonTextReader(streamReader);
            User user = serializer.Deserialize<User>(jsonTextReader)!;

            return View(user);
        }

        public IActionResult Create()
        {
            List<Gender> GenderOptions = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            ViewBag.GenderOptions = GenderOptions;
            return View();
        }

        [HttpPost, ActionName("Create")]
        public async Task<IActionResult> Create([Bind()] Custom custom)
        {
            Guid guid = Guid.NewGuid();
            custom.User.UserId = guid;
            custom.Credential.UserId = guid;
            custom.User.Credential = custom.Credential;
            custom.User.Gender = (Gender)Enum.ToObject(typeof(Gender), custom.User.Gender);
            custom.User.Profile = null;
            

            var response = await _httpRequestService.PostRequest("api/Users", custom.User);
            if (response.IsSuccessStatusCode)
            {
                var responseStream = response.Content.ReadAsStream();
                var streamReader = new StreamReader(responseStream);
                var serializer = new JsonSerializer();
                using var jsonTextReader = new JsonTextReader(streamReader);
                var result = serializer.Deserialize(jsonTextReader)!;
                TempData["success"] = result;
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response.StatusCode.ToString();
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
