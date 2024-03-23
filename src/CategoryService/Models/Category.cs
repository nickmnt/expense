using System.ComponentModel.DataAnnotations;
using MongoDB.Entities;

namespace CategoryService.Models;

public class Category : Entity
{
    [Key]
    [Required]
    public int Id { get; set; }
    
    [Required]
    public string Name { get; set; }
}