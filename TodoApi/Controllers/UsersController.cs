using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoModels.Models;
using System.Runtime.InteropServices;
using TodoModels.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUnitofWork _unitofWork;

        public UsersController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<object>>> GetUsers()
        {
            var _data = await _unitofWork.UserRepository.GetAllAsync();

            List<object> users = [];
            foreach (var item in _data)
            {
                var userObject = new { item.UserId, item.Name, item.DOB, item.Gender, item.Profile };
                users.Add(userObject);
            }
            return Ok(users);
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(Guid id)
        {
            var _data = await _unitofWork.UserRepository.GetAsync(id);

            if (_data == null)
                return NotFound();

            var user = new {_data.UserId, _data.Name, _data.DOB, _data.Gender, _data.Profile };

            return Ok(user);
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostUser([FromBody] User userData)
        {
            // Access model properties directly

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(userData.Credential!.Password);
            userData.Credential.Password = passwordHash;
            Guid id = Guid.NewGuid();
            userData.UserId = id;
            userData.Gender = (Gender)Enum.ToObject(typeof(Gender), userData.Gender!);

            var user = new User
            {
                UserId = userData.UserId,
                Name = userData.Name,
                DOB = userData.DOB,
                Gender = userData.Gender,
                Profile = userData.Profile,
                Credential = new Credential
                {
                    Username = userData.Credential.Username,
                    EmailId = userData.Credential.EmailId,
                    Password = userData.Credential.Password,
                    UserId = userData.Credential.UserId
                }
            };

            var _data = _unitofWork.UserRepository.AddEntity(user);
            if (_data.IsCompleted)
            {
                await _unitofWork.CompleteAsync();
                return CreatedAtAction(nameof(GetUser), new { id = user.UserId }, user);
            }
            else
                return BadRequest(400);
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(Guid id, [FromBody] User user)
        {
            var _data = await _unitofWork.UserRepository.UpdateEntity(id, user);

            if (_data)
            {
                await _unitofWork.CompleteAsync();
                return Ok(201);
            }
            else
                return BadRequest(204);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _unitofWork.UserRepository.GetAsync(id);
            var credential = await _unitofWork.Credentials.GetByUserId(user.UserId);
            if (user == null)
            {
                if(credential == null)
                {
                    return NotFound("User Id Not Found");
                }
            }

           
            var _data = _unitofWork.UserRepository.DeleteEntity(user);
            var _data1 = _unitofWork.Credentials.DeleteEntity(credential);
            
            if (_data.IsCompletedSuccessfully && _data1.IsCompletedSuccessfully)
            {
                await _unitofWork.CompleteAsync();
                return StatusCode(200, "Successly Deleted");
            }
            return StatusCode(502, "DB Error");
        }
    }
}
