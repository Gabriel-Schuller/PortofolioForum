using AutoMapper;
using Forum.Data.Entities;
using Forum.Models;
using Forum.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {

        private readonly IAnswerRepository _repository;
        private readonly IMapper _mapper;

        private readonly LinkGenerator _linkGenerator;

        public AnswersController(IAnswerRepository repository, LinkGenerator linkGenerator, IMapper mapper)
        {
            this._repository = repository;
            this._linkGenerator = linkGenerator;
            this._mapper = mapper;
        }
        // GET: api/<AnswersController>
        [HttpGet]
        public async Task<ActionResult<AnswerModel[]>> GetAllAnswers()
        {
            try
            {
                var answers = await _repository.GetAllAnswersAsync();

                return _mapper.Map<AnswerModel[]>(answers);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        // GET api/<AnswersController>/5
        [HttpGet("{answerID:int}")]
        public async Task<ActionResult<AnswerModel>> Get(int answerID)
        {
            try
            {
                var result = await _repository.GetById(answerID);
                if (result == null)
                {
                    return this.NotFound();
                }
                return _mapper.Map<AnswerModel>(result);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");

            }
        }
        [HttpGet("search/{word}")]
        public async Task<ActionResult<AnswerModel[]>> SearchByWord(string word)
        {
            try
            {
                var results = await _repository.GetAnswersByWord(word);

                if (!results.Any()) return this.NotFound();

                return _mapper.Map<AnswerModel[]>(results);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");

            }
        }

        //POST api/<AnswersController>
        [HttpPost]
        public async Task<ActionResult<AnswerModel>> Post([FromBody] AnswerModel model)
        {

            var existing = await _repository.GetAnswersByWord(model.Message);

            if (existing != null) return BadRequest("Answer already in use");

            try
            {
                var answer = _mapper.Map<Answer>(model);
                _repository.Add(answer);

                if (await _repository.SaveChangesAsync())
                {
                    var location = _linkGenerator.GetPathByAction("Get", "Answers", new { answerID = answer.Id });

                    if (string.IsNullOrWhiteSpace(location))
                    {
                        return BadRequest("Could not use current monicker");
                    }

                    return Created(location, _mapper.Map<AnswerModel>(answer));

                }

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");

            }

            return BadRequest();
        }

        // PUT api/<AnswersController>/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<AnswerModel>> Put(int id, AnswerModel model)
        {
            try
            {
                var oldAnswer = await _repository.GetById(id);
                if (oldAnswer == null)
                {
                    return NotFound("Answer with the specified id does not exist");
                }
                _mapper.Map(model, oldAnswer);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<AnswerModel>(oldAnswer);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }

            return BadRequest();
        }

        // DELETE api/<AnswersController>/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var oldAnswer = await _repository.GetById(id);
                if (oldAnswer == null)
                {
                    return NotFound("There is no answer with the specified id");
                }

                _repository.Delete(oldAnswer);

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
