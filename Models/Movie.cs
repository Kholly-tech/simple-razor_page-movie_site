using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RazorPagesMovie.Models
{
    public class Movie
    {
        public int Id { get; set; }
        
        [Required]
        public string Title { get; set; }
        
        [Required]
        public string Director { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        
        public string Genre { get; set; }
        
        // We'll keep the Cast collection for backward compatibility
        public ICollection<Cast> Cast { get; set; } = new List<Cast>();
        
        // Add the many-to-many relationship
        public List<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
        
        [Range(0, 10)]
        [Column(TypeName = "decimal(3,1)")]
        public decimal IMDbRating { get; set; }
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal BoxOfficeRevenue { get; set; }
        
        public string ReleaseCountry { get; set; }
        
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}