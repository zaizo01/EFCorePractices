using EFCoreWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GenderController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public GenderController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<Gender>> Get()
        {
            return await context.Genders
                .OrderBy(g => g.Name)
                .ToListAsync();
        }


        [HttpGet("id:int")]
        public async Task<ActionResult<Gender>> GetGenderById(int id)
        {
            var gender = await context.Genders.FirstOrDefaultAsync(x => x.Identificador == id);
            if (gender is null) return NotFound("Gender not exist");
            return gender;
        }


        [HttpGet("First")]
        public async Task<ActionResult<Gender>> GetFirst()
        {
            var gender = await context.Genders.FirstOrDefaultAsync(g => g.Name.StartsWith("C"));
            if (gender is null) return NotFound("Gender not exist");
            return gender;
        }

        [HttpGet("Filter")]
        public async Task<IEnumerable<Gender>> GetFilter(string filter)
        {
          return await context.Genders
                .Where(g => g.Name.Contains(filter))
                .OrderBy(g => g.Name)
                .ToListAsync();
        }

        [HttpGet("Pagination")]
        public async Task<IEnumerable<Gender>> Pagination(int page = 1)
        {
            var quantityOfRecordsByPage = 2;
            var genders = await context.Genders
                    .Skip((page - 1) * quantityOfRecordsByPage)  
                    .Take(quantityOfRecordsByPage)
                    .ToListAsync();
            return genders;
        }

        [HttpPost]
        public async Task<ActionResult> Post(Gender gender)
        {
            var status1 = context.Entry(gender).State;
            context.Add(gender);
            var status2 = context.Entry(gender).State;
            await context.SaveChangesAsync();
            var status3 = context.Entry(gender).State;
            return Ok();
        }

        [HttpPost("Alot")]
        public async Task<ActionResult> Post(Gender[] genders)
        {
            context.AddRange(genders);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
