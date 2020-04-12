using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [Authorize]
    //http://localhost:5000/api/values/
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context=context;
        }
        #region Important Points
        //http://localhost:5000/api/values/dot
        /*Important Commands*/
        // dotnet run - to run local server
        // dotnet run watch -- to autosync change,not to rerun above code again and again // this is not working as expected in my case
        // GET api/values
        //[HttpGet]
        /*public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }*/
        // Repositry Pattern
        #endregion
         [AllowAnonymous]
        [HttpGet]
        //[EnableCors("CorsPolicy")]
        public async Task<IActionResult> GetValues()
        {
           var values= await _context.Values.ToListAsync();
           return Ok(values); 
            //return new string[] { "value1", "value2" };
        }

        [AllowAnonymous]
        // GET api/values/5
        [HttpGet("{id}")]
        public  async Task<IActionResult> GetValue(int id)
        {
            var record= await _context.Values.FirstOrDefaultAsync(_vl=>_vl.id==id);
            return Ok(record);
            //return "value";
        }
        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
