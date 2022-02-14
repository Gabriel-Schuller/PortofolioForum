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
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionRepository _repository;
        private readonly LinkGenerator _linkGenerator;
        private readonly IMapper _mapper;
        private readonly IBaseRepository _baseRepository;

        public QuestionsController(IQuestionRepository repository,
            LinkGenerator linkGenerator, IMapper mapper, IBaseRepository baseRepository)
        {
            this._repository = repository;
            this._linkGenerator = linkGenerator;
            this._mapper = mapper;
            this._baseRepository = baseRepository;
        }
        // GET: api/<QuestionsController>
        [HttpGet]
        public async Task<ActionResult<QuestionModel[]>> GetAllAnswers()
        {
            try
            {
                var answers = await _repository.GetAllQuestionsAsync();

                return _mapper.Map<QuestionModel[]>(answers);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }
        }

        // GET api/<QuestionsController>/5
        [HttpGet("{questionID:int}")]
        public async Task<ActionResult<QuestionModel>> Get(int questionID)
        {
            try
            {
                var result = await _repository.GetById(questionID);
                if (result == null)
                {
                    return this.NotFound();
                }
                return _mapper.Map<QuestionModel>(result);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");

            }
        }

        [HttpGet("search/{word}")]
        public async Task<ActionResult<QuestionModel[]>> SearchByWord(string word)
        {
            try
            {
                var results = await _repository.GetQuestionsByWord(word);

                if (!results.Any()) return this.NotFound();

                return _mapper.Map<QuestionModel[]>(results);
            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");

            }
        }

        // POST api/<QuestionsController>
        [HttpPost]
        public async Task<ActionResult<QuestionModel>> Post([FromBody] QuestionModel model)
        {
            //On future sprints, we should add the userID, taken from the jwt Token.
            var existing = await _repository.CheckForDuplicate(model);

            if (existing) return BadRequest("Question already in use");

            try
            {
                var question = _mapper.Map<Question>(model);
                _baseRepository.Add<Question>(question);

                if (await _repository.SaveChangesAsync())
                {
                    var location = _linkGenerator.GetPathByAction("Get", "Questions", new { questionID = question.Id });

                    if (string.IsNullOrWhiteSpace(location))
                    {
                        return BadRequest("Could not use current id");
                    }

                    return Created(location, _mapper.Map<QuestionModel>(question));

                }

            }
            catch (Exception)
            {

                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");

            }

            return BadRequest();
        }

        // PUT api/<QuestionsController>/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult<QuestionModel>> Put(int id, QuestionModel model)
        {
            try
            {
                var oldQuestion = await _repository.GetById(id);
                if (oldQuestion == null)
                {
                    return NotFound("Question with the specified id does not exist");
                }

                var existing = await _repository.CheckForDuplicate(model);

                if (existing) return BadRequest("Question already in use");

                _mapper.Map(model, oldQuestion);

                if (await _repository.SaveChangesAsync())
                {
                    return _mapper.Map<QuestionModel>(oldQuestion);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Server error");
            }

            return BadRequest();
        }

        // DELETE api/<QuestionsController>/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var oldQuestion = await _repository.GetById(id);
                if (oldQuestion == null)
                {
                    return NotFound("There is no question with the specified id");
                }

                _baseRepository.Delete(oldQuestion);

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
