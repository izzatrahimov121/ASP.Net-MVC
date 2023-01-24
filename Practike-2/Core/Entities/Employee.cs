using Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class Employee : IEntity
{
	public int Id { get; set; }
	[Required,MaxLength(100)]
	public string? Fullname { get; set; }
    [Required, MaxLength(150)]
    public string? Position { get; set; }
    [Required, MaxLength(250)]
    public string? Description { get; set; }
    [Required, MaxLength(100)]
    public string? Image { get; set; }
}
