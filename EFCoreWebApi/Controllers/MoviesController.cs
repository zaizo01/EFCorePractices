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
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public MoviesController(ApplicationDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MovieDTO>> Get(int id)
        {
            var movie = await context.Movies
                .Include(m => m.Genders.OrderByDescending(g => g.Name))
                .Include(m => m.CinemaRooms)
                    .ThenInclude(s => s.Cinema)
                .Include(m => m.MovieActors.Where(ma => ma.Actor.DateOfBirth.Value.Year > 1980))
                    .ThenInclude(ma => ma.Actor)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (movie is null) return NotFound();

            var movieDTO = mapper.Map<MovieDTO>(movie);

            movieDTO.Cinemas = movieDTO.Cinemas.DistinctBy(m => m.Id).ToList();

            return movieDTO;
        }

        [HttpGet("withProjectTo/{id:int}")]
        public async Task<ActionResult<MovieDTO>> GetWithProjectTo(int id)
        {
            var movie = await context.Movies
                .ProjectTo<MovieDTO>(mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (movie is null) return NotFound();

            var movieDTO = mapper.Map<MovieDTO>(movie);

            movieDTO.Cinemas = movieDTO.Cinemas.DistinctBy(m => m.Id).ToList();

            return movieDTO;
        }

        [HttpGet("selectiveLoad/{id:int}")]
        public async Task<ActionResult> GetLoadSelective(int id)
        {
            var movie = await context.Movies.Select(p =>
                new
                {
                    Id = p.Id,
                    Title = p.Title,
                    Genders = p.Genders.OrderByDescending(g => g.Name).Select(g => g.Name).ToList(),
                    ActorQuantity = p.MovieActors.Count(),
                    CinemasQuantity = p.CinemaRooms.Select(c => c.CinemaId).Distinct().Count()
                }).FirstOrDefaultAsync(p => p.Id == id);
                

            if (movie is null) return NotFound();
            return Ok(movie);
        }

        [HttpGet("explicitLoad/{id:int}")]
        public async Task<ActionResult<MovieDTO>> GetLoadExplicit(int id)
        {
            var movie = await context.Movies.AsTracking().FirstOrDefaultAsync(m => m.Id == id);

           // await context.Entry(movie).Collection(m => m.Genders).LoadAsync();
            
            var gendersQuatity = await context.Entry(movie).Collection(m => m.Genders).Query().CountAsync();

            if (movie is null) return NotFound();

            return Ok(mapper.Map<MovieDTO>(movie));
        }

        [HttpGet("LazyLoading/{id:int}")]
        public async Task<ActionResult<List<MovieDTO>>> GetLazyLoading(int id)
        {
            var movies = await context.Movies.AsTracking().ToListAsync();

            foreach (var movie in movies)
            {
                // cargar los generos de la pelicula
                movie.Genders.ToList();
            }

            var moviesDTOs = mapper.Map<List<MovieDTO>>(movies);
            return Ok(moviesDTOs);
        }

        [HttpGet("AgrupadaPorEstreno")]
        public async Task<ActionResult> GetAgrupadaPorCartelera()
        {
            var movies = await context.Movies.GroupBy(m => m.IsAvailable)
                                             .Select(g => new
                                             {
                                                 IsAviable = g.Key,
                                                 Quantity = g.Count(),
                                                 Movies = g.ToList()
                                             }).ToListAsync();
            return Ok(movies);
        }

        [HttpGet("AgrupadaPorCantidadDeGeneros")]
        public async Task<ActionResult> GetAgrupadaPorCantidadDeGeneros()
        {
            var movies = await context.Movies.GroupBy(m => m.Genders.Count())
                                             .Select(g => new
                                             {
                                               Conteo = g.Key,
                                               Titles = g.Select(x => x.Title),
                                               Genders = g.Select(m => m.Genders).SelectMany(gen => gen).Select(gen => gen).Distinct()
                                             }).ToListAsync();
            return Ok(movies);
        }

        [HttpGet("filtrar")]
        public async Task<ActionResult<List<MovieDTO>>> filtrar([FromQuery] MoviesFilterDTO moviesFilterDTO)
        {
            var moviesQueryable = context.Movies.AsQueryable();

            if (!string.IsNullOrEmpty(moviesFilterDTO.Title))
            {
                moviesQueryable = moviesQueryable.Where(p => p.Title.Contains(moviesFilterDTO.Title));
            }

            if (moviesFilterDTO.onBillBoard)
            {
                moviesQueryable = moviesQueryable.Where(x => x.IsAvailable);
            }

            if (moviesFilterDTO.nextPremiere)
            {
                var today = DateTime.Today;
                moviesQueryable = moviesQueryable.Where(x => x.PremiereDate > today);
            }

            if (moviesFilterDTO.GenderId != 0)
            {
                moviesQueryable = moviesQueryable.Where(m => m.Genders.Select(g => g.Identificador)
                                                 .Contains(moviesFilterDTO.GenderId));
            }


            var movies = await moviesQueryable.Include(m => m.Genders).ToListAsync();

            return mapper.Map<List<MovieDTO>>(movies);
        }

    }
}
