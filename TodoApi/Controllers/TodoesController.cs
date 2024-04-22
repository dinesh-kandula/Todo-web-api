using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoModels.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;
using TodoModels.Services;

namespace TodoApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TodoesController : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;

        public TodoesController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        // GET: /Todoes
        [HttpGet]
        public async Task<List<Todo>> GetTodos()
        {
           return await _unitofWork.TodoRepository.GetAllAsync();
        }

        // GET: /Todoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(Guid id)
        {
            Todo _data = await _unitofWork.TodoRepository.GetAsync(id);

            if (_data == null)
                return NotFound();

            var todo = new {_data.TodoId, _data.Title, _data.Description, _data.Priority, _data.Status, _data.CreatedDate, _data.UpdatedDate, _data.UserId};

            return Ok(todo);
        }

        // GET: api/Users/Todoes/5
        [HttpGet("/Users/Todoes/{userId}")]
        public async Task<ActionResult<Todo>> GetUsersTodo(Guid userId)
        {
            List<Todo>_data = await _unitofWork.TodoRepository.GetUsersTodoAsync(userId);

            if (_data.Count == 0)
                return NotFound();

            List<object> todoList = [];

            foreach (var item in _data)
            {
                var todoItem = new {item.TodoId, item.Title, item.Description, item.Priority, item.Status, item.CreatedDate, item.UpdatedDate };
                todoList.Add(todoItem);
            }

            return Ok(todoList);
        }

        // PUT: /Todoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(Guid id, [FromBody] Todo todo)
        {
            var _data = await _unitofWork.TodoRepository.UpdateEntity(id, todo);

            if (_data)
            {
                await _unitofWork.CompleteAsync();
                return Ok(201);
            }
            else
                return BadRequest(204);
        }

        // POST: /Todoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo([FromBody] Todo todo)
        {
            User user = await _unitofWork.UserRepository.GetAsync(todo.UserId);
            if (user == null) return StatusCode(404, "No User Found with user Id");

            todo.User = user;
            todo.Status = (Status) Enum.ToObject(typeof(Status), todo.Status);
            todo.Priority = (Priority) Enum.ToObject(typeof(Priority), todo.Priority);
            todo.TodoId = new Guid();

            todo.CreatedDate = DateTime.Now;
            todo.UpdatedDate = DateTime.Now;

            var _data = _unitofWork.TodoRepository.AddEntity(todo);
            if (_data.IsCompleted)
            {
                await _unitofWork.CompleteAsync();
                return Ok(todo.TodoId);
            }
            
            return BadRequest(400);
        }

        // DELETE: /Todoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            var todo = await _unitofWork.TodoRepository.GetAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            var _data = _unitofWork.TodoRepository.DeleteEntity(todo);
            if (_data.IsCompleted)
            {
                await _unitofWork.CompleteAsync();
                return StatusCode(200, "Successly Deleted");
            }
            return StatusCode(502, "DB Error");
        }
    }
}
