using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TodoApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TodoesController : ControllerBase
    {
        private readonly TodoDbContext _context;

        public TodoesController(TodoDbContext context)
        {
            _context = context;
        }

        // GET: /Todoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodos()
        {
            return await _context.Todos.ToListAsync();
        }

        // GET: /Todoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);

            var todo1 = from x in _context.Todos where x.TodoId == id select x;

            if (todo == null)
            {
                return NotFound();
            }

            return todo;
        }

        // PUT: /Todoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(Guid id, Todo todo)
        {
            if (id != todo.TodoId)
            {
                return BadRequest();
            }

            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: /Todoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(Todo todo)
        {
            todo.Status = (Status) Enum.ToObject(typeof(Status), todo.Status);
            todo.Priority = (Priority) Enum.ToObject(typeof(Priority), todo.Priority);

            todo.User = await _context.Users.FirstOrDefaultAsync(e => e.UserId == todo.UserId);


            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodo), new { id = todo.TodoId }, todo);
        }

        // DELETE: /Todoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoExists(Guid id)
        {
            return _context.Todos.Any(e => e.TodoId == id);
        }
    }
}
