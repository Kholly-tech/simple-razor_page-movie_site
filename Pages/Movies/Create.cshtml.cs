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
    public class CreateModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public CreateModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            PopulateActorsDropDown();
            return Page();
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        [BindProperty]
        public int[] SelectedActors { get; set; } = Array.Empty<int>();

        public SelectList ActorOptions { get; set; }

        private void PopulateActorsDropDown()
        {
            var actors = _context.Actor.OrderBy(a => a.Name).ToList();
            ActorOptions = new SelectList(actors, "Id", "Name");
        }

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopulateActorsDropDown();
                return Page();
            }

            _context.Movie.Add(Movie);
            await _context.SaveChangesAsync();

            // Add selected actors to the movie
            if (SelectedActors != null && SelectedActors.Length > 0)
            {
                foreach (var actorId in SelectedActors)
                {
                    _context.MovieActor.Add(new MovieActor
                    {
                        MovieId = Movie.Id,
                        ActorId = actorId
                    });
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}