using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace MvcTodoApp.Controllers
{
    public class HelloworldController : Controller
    {
        // GET: /HelloWorld/
        public IActionResult Index()
        {
            return View();
        }
        // 
        // GET: /HelloWorld/Welcome/ 
        public IActionResult Welcome(string name, int ID = 1)
        {
            List<string> list = [];
            list.Add(name);
            list.Add("Dinesh");
            list.Add("Kumar");
            list.Add("Kandula");


            ViewData["Message"] = "Hello " + name;
            ViewData["NumTimes"] = ID;
            ViewData["NameList"] = list;
           
            return View();
        }
    }
}
