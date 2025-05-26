// New model for cast members
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Models;


public class Cast
{
    public int Id { get; set; }
    
    [Required]
    public string? Name { get; set; }
    
    public string? Role { get; set; }
    
    public int MovieId { get; set; }
    public Movie? Movie { get; set; }
    [DataType(DataType.Date)]
    public DateTime DateOfBirth { get; set; }
}