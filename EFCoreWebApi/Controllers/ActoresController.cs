using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreWebApi.DTOs;
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
    public class ActoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public ActoresController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var actores = await context.Actors
                .ProjectTo<ActorDTO>(mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(actores);
        }
    }
}
