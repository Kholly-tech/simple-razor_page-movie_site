using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Movies
{
    public class EditModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public EditModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        [BindProperty]
        public int[] SelectedActors { get; set; } = Array.Empty<int>();

        public SelectList ActorOptions { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie
                .Include(m => m.MovieActors)
                .ThenInclude(ma => ma.Actor)
                .FirstOrDefaultAsync(m => m.Id == id);
                
            if (movie == null)
            {
                return NotFound();
            }
            
            Movie = movie;
            
            // Get currently selected actors
            SelectedActors = Movie.MovieActors.Select(ma => ma.ActorId).ToArray();
            
            PopulateActorsDropDown();
            return Page();
        }

        private void PopulateActorsDropDown()
        {
            var actors = _context.Actor.OrderBy(a => a.Name).ToList();
            ActorOptions = new SelectList(actors, "Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateActorsDropDown();
                return Page();
            }

            // Get the existing movie with its relationships
            var movieToUpdate = await _context.Movie
                .Include(m => m.MovieActors)
                .FirstOrDefaultAsync(m => m.Id == Movie.Id);

            if (movieToUpdate == null)
            {
                return NotFound();
            }

            // Update basic movie properties
            _context.Entry(movieToUpdate).CurrentValues.SetValues(Movie);
            
            // Handle actor relationships
            
            // Remove actors that are no longer selected
            foreach (var existingActor in movieToUpdate.MovieActors.ToList())
            {
                if (!SelectedActors.Contains(existingActor.ActorId))
                {
                    _context.MovieActor.Remove(existingActor);
                }
            }
            
            // Add newly selected actors
            foreach (var actorId in SelectedActors)
            {
                if (!movieToUpdate.MovieActors.Any(ma => ma.ActorId == actorId))
                {
                    movieToUpdate.MovieActors.Add(new MovieActor
                    {
                        MovieId = movieToUpdate.Id,
                        ActorId = actorId
                    });
                }
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(Movie.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}