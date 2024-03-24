using System.ComponentModel.DataAnnotations;
using MongoDB.Entities;

namespace CategoryService.Models;

public class Category : Entity
{
    public string Name { get; set; }
}