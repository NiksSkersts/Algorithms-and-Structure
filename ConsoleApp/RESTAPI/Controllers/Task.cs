using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using RESTAPI.Model;

namespace RESTAPI.Controllers
{
    [ApiController]
    public class Task : ControllerBase
    {
        private Storage _startup;
        public Task(Storage startup)
        {
            _startup = startup;
        }
        [HttpGet]
        [Route("get")]
        public ActionResult Get()
        {
            if (_startup.Persons.Any())
                return Ok(_startup.Persons);
            return NotFound();
        }

        [HttpPut]
        [Route("put")]
        public void Post([FromBody] Person person)
        {
            _startup.Persons.Add(person);
        }
        [HttpPost( "{id:int}" )]  
        public void Put( int  id, [FromBody] Person person )
        {
            _startup.Persons[id] = person;
        } 
        [HttpDelete( "{id:int}" )]  
        public void Delete( int id )
        {
            _startup.Persons.RemoveAt(id);
        }  
        
    }
}