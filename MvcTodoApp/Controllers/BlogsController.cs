using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Azure;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcTodoApp.Models;
using MvcTodoApp.Services;
using Newtonsoft.Json;

namespace MvcTodoApp.Controllers { 

    public class DinnuObject
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Body { get; set; }
    }

    public class MyViewModel
    {
        public required List<Blog> BlogList { get; set; }
        public required List<DinnuObject> DinnuList { get; set; }
    }

    public class BlogsController : Controller
    {
        private readonly MvcDbContext _context;
        private readonly IFileService _fileService;
        private readonly IWebRequestService _webRequestService;

        public BlogsController(MvcDbContext context, IFileService fileService, IWebRequestService webRequestService)
        {
            _context = context;
            _fileService = fileService;
            _webRequestService = webRequestService;
        }

        // GET: Blogs
        public async Task<IActionResult> Index()
        {
            string urlTest = string.Format("https://jsonplaceholder.typicode.com/posts");
            WebRequest request = WebRequest.Create(urlTest);
            request.Method = "GET";
            HttpWebResponse response = null;
            response = (HttpWebResponse) request.GetResponse();

            string result = null;
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                result = sr.ReadToEnd();
                sr.Close();
            }

            List<DinnuObject> dinnuList = JsonConvert.DeserializeObject<List<DinnuObject>>(result)!;

            List<Blog> blogList = await _context.Blogs.ToListAsync();

            MyViewModel viewModel = new MyViewModel
            {
                BlogList = blogList, 
                DinnuList =  dinnuList
            };


            return View(viewModel);
        }


        public ActionResult Orders_Read([DataSourceRequest] DataSourceRequest request)
        {
            string urlTest = string.Format("https://jsonplaceholder.typicode.com/posts");

            string result = _webRequestService.WebRequestGet(urlTest);

            List<DinnuObject> dinnuList = JsonConvert.DeserializeObject<List<DinnuObject>>(result)!;


            var dsResult = dinnuList.ToDataSourceResult(request);

            return Json(dsResult);
        }


        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: Blogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind()] Blog blog)
        {
            if (ModelState.IsValid)
            {

                if (blog.ImageFile != null)
                {
                    var serviceResult = _fileService.SaveImage(blog.ImageFile);
                    if (serviceResult.Item1 == 1)
                    {
                        blog.Cover = serviceResult.Item2;
                    }
                }

                blog.CreatedAt = DateTime.Now;
                blog.UpdatedAt = DateTime.Now;

                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            
            return View(blog);
        }

        // POST: Blogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Content,CreatedAt,UpdatedAt,Cover,Author")] Blog blog)
        {
            if (id != blog.Id)
            {
                return NotFound();
            }

            var preBlog = await _context.Blogs.FindAsync(id);

            if (ModelState.IsValid)
            {
                try
                {
                    preBlog.Title = blog.Title ?? preBlog.Title;
                    preBlog.Content = blog.Content ?? preBlog.Content;
                    preBlog.CreatedAt = preBlog.CreatedAt;
                    preBlog.Author = blog.Author ?? preBlog.Author;
                    preBlog.UpdatedAt = DateTime.Now;

                    //_context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog != null)
            {
                _context.Blogs.Remove(blog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.Id == id);
        }
    }
}
