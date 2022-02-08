using Forum.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Forum.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionRepository repository;
        private readonly LinkGenerator linkGenerator;

        public QuestionsController(IQuestionRepository repository, LinkGenerator linkGenerator )
        {
            this.repository = repository;
            this.linkGenerator = linkGenerator;
        }
        // GET: api/<QuestionsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<QuestionsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<QuestionsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<QuestionsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<QuestionsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
