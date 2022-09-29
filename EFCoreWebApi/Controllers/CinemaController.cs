using AutoMapper;
using AutoMapper.QueryableExtensions;
using EFCoreWebApi.DTOs;
using EFCoreWebApi.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreWebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CinemaController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public CinemaController(ApplicationDbContext context, IMapper mapper) 
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<CinemaDTO>> Get()
        {
            return await context.Cinemas
                .ProjectTo<CinemaDTO>(mapper.ConfigurationProvider)
                .ToListAsync();
        }

        [HttpGet("Near")]
        public async Task<ActionResult> GetCinemas(double latitude , double longitud)
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            var myCurrentLocation = geometryFactory.CreatePoint(new Coordinate(longitud, latitude));

            var maxDistance = 2000;

            var cinemas = await context.Cinemas
                .OrderBy(c => c.Location.Distance(myCurrentLocation))
                .Where(c => c.Location.IsWithinDistance(myCurrentLocation, maxDistance))
                .Select(c => new
                {
                    Name = c.Name,
                    Distance = Math.Round(c.Location.Distance(myCurrentLocation))
                }).ToListAsync();

            return Ok(cinemas);
        }

        [HttpPost]
        public async Task<ActionResult> Post()
        {
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);

            var cinemaLocation = geometryFactory.CreatePoint(new Coordinate(18.5021343, -69.8991133));

            var cinema = new Cinema()
            {
                Name = "Cine test",
                Location = cinemaLocation,
                CinemaOffer = new CinemaOffer()
                {
                    PercentageDiscount = 5,
                    StartDate = DateTime.Today,
                    EndDate = DateTime.Today.AddDays(7)
                },
                CinemaRooms = new HashSet<CinemaRoom>()
                {
                   new CinemaRoom()
                   {
                       Price = 200,
                       TypeOfCinemaRoom = TypeOfCinemaRoom.TwoDimensions
                   },
                   new CinemaRoom()
                   {
                       Price = 350,
                       TypeOfCinemaRoom = TypeOfCinemaRoom.ThreeDimensions
                   }
                }
            };

            context.Add(cinema);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("withDTO")]
        public async Task<ActionResult> Post(CinemaCreateDTO cinemaCreateDTO)
        {
            var cinema = mapper.Map<Cinema>(cinemaCreateDTO);
            context.Add(cinema);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
