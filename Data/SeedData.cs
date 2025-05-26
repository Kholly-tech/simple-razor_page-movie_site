using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using System;
using System.Linq;
using Bogus;

namespace RazorPagesMovie.Data
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new RazorPagesMovieContext(
                serviceProvider.GetRequiredService<DbContextOptions<RazorPagesMovieContext>>()))
            {
                // Look for any movies or actors
                if (context.Movie.Any() && context.Actor.Any())
                {
                    return;   // DB has been seeded
                }

                // Seed actors
                SeedActors(context);

                // Seed movies if needed
                if (!context.Movie.Any())
                {
                    SeedMovies(context);
                }
            }
        }

        private static void SeedActors(RazorPagesMovieContext context)
        {
            // Check if there are already actors
            if (context.Actor.Any())
            {
                return;
            }

            // Use Bogus to generate fake actor data
            var faker = new Faker<Actor>()
                .RuleFor(a => a.Name, f => f.Name.FullName())
                .RuleFor(a => a.DateOfBirth, f => f.Date.Past(80, DateTime.Now.AddYears(-18)));

            var actors = faker.Generate(100);

            context.Actor.AddRange(actors);
            context.SaveChanges();
        }

        private static void SeedMovies(RazorPagesMovieContext context)
        {
            // Sample movies for seeding
            context.Movie.AddRange(
                new Movie
                {
                    Title = "The Shawshank Redemption",
                    Director = "Frank Darabont",
                    ReleaseDate = DateTime.Parse("1994-10-14"),
                    Genre = "Drama",
                    IMDbRating = 9.3M,
                    BoxOfficeRevenue = 28341469M,
                    ReleaseCountry = "USA",
                    Price = 14.99M
                },
                new Movie
                {
                    Title = "The Godfather",
                    Director = "Francis Ford Coppola",
                    ReleaseDate = DateTime.Parse("1972-03-24"),
                    Genre = "Crime, Drama",
                    IMDbRating = 9.2M,
                    BoxOfficeRevenue = 134966411M,
                    ReleaseCountry = "USA",
                    Price = 12.99M
                },
                new Movie
                {
                    Title = "Pulp Fiction",
                    Director = "Quentin Tarantino",
                    ReleaseDate = DateTime.Parse("1994-10-14"),
                    Genre = "Crime, Drama",
                    IMDbRating = 8.9M,
                    BoxOfficeRevenue = 107928762M,
                    ReleaseCountry = "USA",
                    Price = 13.99M
                }
            );
            context.SaveChanges();

            // Assign random actors to movies
            var movies = context.Movie.ToList();
            var actors = context.Actor.ToList();
            var random = new Random();

            foreach (var movie in movies)
            {
                // Select 3-8 random actors for each movie
                var actorCount = random.Next(3, 9);
                var selectedActors = actors.OrderBy(x => Guid.NewGuid()).Take(actorCount).ToList();

                foreach (var actor in selectedActors)
                {
                    context.MovieActor.Add(new MovieActor
                    {
                        MovieId = movie.Id,
                        ActorId = actor.Id
                    });
                }
            }
            context.SaveChanges();
        }
    }
}