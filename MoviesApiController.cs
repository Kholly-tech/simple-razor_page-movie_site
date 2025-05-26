using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace RazorPagesMovie.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesApiController : ControllerBase
    {
        private readonly RazorPagesMovieContext _context;
        private readonly JsonSerializerOptions _jsonOptions;

        public MoviesApiController(RazorPagesMovieContext context)
        {
            _context = context;
            _jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve,
                MaxDepth = 64 // Increase max depth if needed
            };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            var movies = await _context.Movie
                .Include(m => m.Cast)
                .Include(m => m.MovieActors)
                .ThenInclude(ma => ma.Actor)
                .AsNoTracking()
                .ToListAsync();

            return new JsonResult(movies, _jsonOptions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> GetMovie(int id)
        {
            var movie = await _context.Movie
                .Include(m => m.Cast)
                .Include(m => m.MovieActors)
                .ThenInclude(ma => ma.Actor)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (movie == null)
            {
                return NotFound();
            }

            return new JsonResult(movie, _jsonOptions);
        }
    }
}