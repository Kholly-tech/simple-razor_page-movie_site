using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RazorPagesMovie.Models
{
    public class Actor
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }
        
        public List<MovieActor> MovieActors { get; set; } = new List<MovieActor>();
    }
}