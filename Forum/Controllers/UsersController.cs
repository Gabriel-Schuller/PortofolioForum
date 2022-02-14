using AutoMapper;
using Forum.Data.Entities;
using Forum.Models;
using Forum.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IBaseRepository _baseRepository;
        private readonly LinkGenerator _linkGenerator;

        public UsersController(IUserRepository repository, LinkGenerator linkGenerator, IMapper mapper,
                                IBaseRepository baseRepository)
        {
            this._repository = repository;
            this._linkGenerator = linkGenerator;
            this._mapper = mapper;
            this._baseRepository = baseRepository;
        }
        // GET: api/<UserController>
        [HttpGet]
        public async Task<ActionResult<UserModel[]>> GetAllUsers()
        {
            try
            {
                var users = await _repository.GetAllUsersAsync();

                return _mapper.Map<UserModel[]>(users);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{userID:int}")]
        public async Task<ActionResult<UserModel>> Get(int userID)
        {
            try
            {
                var result = await _repository.GetById(userID);
                if (result == null)
                {
                    return this.NotFound();
                }
                return _mapper.Map<UserModel>(result);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");

            }
        }

        [HttpGet("search/{word}")]
        public async Task<ActionResult<UserModel[]>> SearchByWord(string word)
        {
            try
            {
                var results = await _repository.GetUsersByWord(word);

                if (!results.Any()) return this.NotFound();

                return _mapper.Map<UserModel[]>(results);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");

            }
        }

        //POST api/<UserController>
        [HttpPost]
        public async Task<ActionResult<UserModel>> Post([FromBody] UserModel model)
        {

            var existing = await _repository.GetUsersByWord(model.UserName);

            if (existing.Length != 0) return BadRequest("User already in use");

            try
            {
                var user = _mapper.Map<User>(model);
                _baseRepository.Add(user);

                if (await _repository.SaveChangesAsync())
                {
                    var location = _linkGenerator.GetPathByAction("Get", "Users", new { userID = user.Id });

                    if (string.IsNullOrWhiteSpace(location))
                    {
                        return BadRequest("Could not use current id");
                    }

                    return Created(location, _mapper.Map<UserModel>(user));

                }

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");

            }

            return BadRequest();
        }

        // PUT api/<UserController>/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserModel>> Put(int id, UserModel model)
        {
            try
            {
                var oldUser = await _repository.GetById(id);
                if (oldUser == null)
                {
                    return NotFound("User with the specified id does not exist");
                }
                _mapper.Map(model, oldUser);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<UserModel>(oldUser);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }

            return BadRequest();
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var oldUser = await _repository.GetById(id);
                if (oldUser == null)
                {
                    return NotFound("There is no user with the specified id");
                }

                _baseRepository.Delete(oldUser);

                if (await _repository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
            return BadRequest("Failed to delete the camp!");
        }
    }
}
