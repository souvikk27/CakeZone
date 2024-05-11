using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CakeZone.Services.Product.Controllers
{
    [Route("api/v1/attributes")]
    [ApiController]
    public class AttributeController : ControllerBase
    {
        // GET: api/<AttributeController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AttributeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AttributeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AttributeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AttributeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
