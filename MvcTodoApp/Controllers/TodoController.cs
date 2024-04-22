using Azure;
using Microsoft.AspNetCore.Mvc;
using MvcTodoApp.Models;
using MvcTodoApp.Services;
using Newtonsoft.Json;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata;
using TodoModels.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Numerics;
using System.Reflection;

namespace MvcTodoApp.Controllers
{

    public class TodoController : Controller
    {
        public IWebRequestService _webRequestService;
        private readonly IConfiguration _configuration;

        public TodoController(IWebRequestService webRequestService, IConfiguration configuration)
        {
            _webRequestService = webRequestService;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            string result = _webRequestService.WebRequestGet($"{_configuration["API:URL"]}/Todoes");
            List<Todo> todos = DeseralizeObjectToList(result);
            List<string> autoComplete = new List<string>();
            foreach (var each in todos)
            {
                autoComplete.Add(each.Title);
            }
            ViewBag.TodoTitle = autoComplete;
            return View(todos);
        }

        public ActionResult FilterMenuCustomization_Status()
        {            
            return Json(Enum.GetValues(typeof(TodoModels.Models.Status)).Cast<Status>().ToList());
        }

        public ActionResult Orders_Read([DataSourceRequest] DataSourceRequest request)
        {
            string result = _webRequestService.WebRequestGet($"{_configuration["API:URL"]}/Todoes");
            List<Todo> todos = DeseralizeObjectToList(result);
            
            var dsResult = todos.ToDataSourceResult(request);
            return Json(dsResult);
        }

        public IActionResult Details(Guid Id)
        {
            string result = _webRequestService.WebRequestGet($"{_configuration["API:URL"]}/Todoes/{Id}");
            Todo todo = DeseralizeObject(result)!;
            return View(todo);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind()] Todo todo)
        {
            string postData = JsonConvert.SerializeObject(todo);
            string result = _webRequestService.WebRequestPost($"{_configuration["API:URL"]}/Todoes", postData);
            Console.WriteLine(result);
            return RedirectToAction(nameof(Index));
        }

        [AcceptVerbs("POST")]
        public ActionResult Filter_Multi_Editing_Create([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<Todo> todos)
        {
            var results = new List<Todo>();


            if (todos != null) { 
                foreach (var todo in todos)
                {
                    string postData = JsonConvert.SerializeObject(todo);
                    string todoId = _webRequestService.WebRequestPost($"{_configuration["API:URL"]}/Todoes", postData);
                    if (todoId != null)
                    {
                        ModelState.Clear();
                        todoId = todoId.Substring(1, todoId.Length - 2);
                        string result = _webRequestService.WebRequestGet($"{_configuration["API:URL"]}/Todoes/{todoId}");
                        Todo newTodo = DeseralizeObject(result)!;
                        results.Add(newTodo);
                    }
                }
            }

            return Json(results.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs("POST")]
        public ActionResult Filter_Multi_Editing_Update([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<Todo> todos)
        {
            if (todos != null && ModelState.IsValid)
            {
                foreach (var todo in todos)
                {
                    string updateData = JsonConvert.SerializeObject(todo);
                    _webRequestService.WebRequestPut($"{_configuration["API:URL"]}/Todoes/{todo.TodoId}", updateData);
                }
            }

            return Json(todos.ToDataSourceResult(request, ModelState));
        }

       

    public IActionResult Edit(Guid Id)
        {
            string result = _webRequestService.WebRequestGet($"{_configuration["API:URL"]}/Todoes/{Id}");
            Todo todo = DeseralizeObject(result)!;
            return View(todo);
        }

        [HttpPost, ActionName("Edit")]
        public IActionResult Edit([Bind()] Todo todo)
        {
            Guid Id = todo.TodoId;
            string updateData = JsonConvert.SerializeObject(todo);
            string result = _webRequestService.WebRequestPut($"{_configuration["API:URL"]}/Todoes/{Id}", updateData);
            Console.WriteLine(result);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(Guid Id)
        {
            string result = _webRequestService.WebRequestGet($"{_configuration["API:URL"]}/Todoes/{Id}");
            Todo todo = DeseralizeObject(result)!;
            return View(todo);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(Guid Id)
        {
            string result = _webRequestService.WebRequestDelete($"{_configuration["API:URL"]}/Todoes/{Id}");
            Console.WriteLine(result);
            return RedirectToAction(nameof(Index));
        }

        [AcceptVerbs("POST")]
        public ActionResult Filter_Multi_Editing_Destroy([DataSourceRequest] DataSourceRequest request, [Bind(Prefix = "models")] IEnumerable<Todo> todos)
        {
            if (todos.Any())
            {
                foreach (var todo in todos)
                {
                    _webRequestService.WebRequestDelete($"{_configuration["API:URL"]}/Todoes/{todo.TodoId}");
                }
            }
            return Json(todos.ToDataSourceResult(request, ModelState));
        }
    
    //--------------------------------------------------------
    //JsonConvert 
    public List<Todo> DeseralizeObjectToList(string result)
        {
            List<Todo> t = JsonConvert.DeserializeObject<List<Todo>>(result)!;
            return t;
        }

        public Todo DeseralizeObject(string result)
        {
            Todo t = JsonConvert.DeserializeObject<Todo>(result)!;
            return t;
        }

        public string SeralizeObject(Todo entity)
        {
            string data = JsonConvert.SerializeObject(entity);
            return data;
        }


    }
}
