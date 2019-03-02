using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context;
        public ValuesController(DataContext context)
        {
            _context = context;

        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get() => Ok(await _context.Values.ToListAsync());

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)=>Ok(await _context.Values.FirstOrDefaultAsync(v=>v.Id==id));

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] string name)
        {
            await _context.Values.AddAsync(new Value(){Name=name});
            await _context.SaveChangesAsync();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] string name)
        {
            var updatedValue=_context.Values.FirstOrDefault(v=>v.Id==id);
            updatedValue.Name=name;
            if(updatedValue!=null)
            {
                 _context.Values.Update(updatedValue);
              await   _context.SaveChangesAsync();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            var deletedValue=_context.Values.FirstOrDefault(v=>v.Id==id);
            if(deletedValue!=null)
            {
                _context.Values.Remove(deletedValue);
                await _context.SaveChangesAsync();
            }
        }
    }
}
