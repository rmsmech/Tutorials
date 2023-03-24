using Microsoft.AspNetCore.Mvc;


namespace WebApplication1.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class GAuthController : ControllerBase {
        // GET: api/<GAuthController>
        [HttpGet]
        public IEnumerable<string> Get() {
            return new string[] { "value1", "value2" };
        }

        // GET api/<GAuthController>/5
        [HttpGet("{id}")]
        public string Get(int id) {
            return "value";
        }

        // POST api/<GAuthController>
        [HttpPost]
        public void Post([FromBody] string value) {
        }

        // PUT api/<GAuthController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value) {
        }

        // DELETE api/<GAuthController>/5
        [HttpDelete("{id}")]
        public void Delete(int id) {
        }
    }
}
