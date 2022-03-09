﻿using AutoMapper;
using Forum.Data.Entities;
using Forum.Helpers;
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
        private readonly JwtService _jwtService;
        private readonly LinkGenerator _linkGenerator;

        public UsersController(IUserRepository repository, LinkGenerator linkGenerator, IMapper mapper,
                                IBaseRepository baseRepository, JwtService service)
        {
            this._repository = repository;
            this._linkGenerator = linkGenerator;
            this._mapper = mapper;
            this._baseRepository = baseRepository;
            this._jwtService = service;
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
        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Post([FromBody] RegisterUserModel model)
        {

            var existing = await _repository.GetUserByUsername(model.UserName);

            if (existing != null) return BadRequest("User already in use");

            try
            {
                var user = _mapper.Map<User>(model);
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

                _baseRepository.Add(user);

                if (await _baseRepository.SaveChangesAsync())
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

        [HttpPost("login")]
        public async Task<ActionResult<RegisterUserModel>> Login(UserModel model)
        {
            try
            {
                var user = await _repository.GetUserByUsername(model.UserName);
                if (user == null) return BadRequest(new { message = "Invalid Cridentials" });

                if (!BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                {
                    return BadRequest(new { message = "Invalid Cridentials" });
                }

                var jwt = _jwtService.Generate(user.Id);

                Response.Cookies.Append("jwt", jwt, new CookieOptions
                {
                    Secure = true,
                    HttpOnly = true,
                    IsEssential = true,
                    Expires = DateTime.Now.AddMonths(1),
                    SameSite = SameSiteMode.None

                });

                return Ok(_mapper.Map<RegisterUserModel>(user));
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        [HttpGet("user")]
        public async Task<ActionResult<RegisterUserModel>> UserJwt()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.Verify(jwt);

                int userId = int.Parse(token.Issuer);

                var user =await _repository.GetById(userId);

                return Ok(_mapper.Map<RegisterUserModel>(user));
            }
            catch (Exception e)
            {

                return Unauthorized(e);
            }


        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok();
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

                if (await _baseRepository.SaveChangesAsync())
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

                if (await _baseRepository.SaveChangesAsync())
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
