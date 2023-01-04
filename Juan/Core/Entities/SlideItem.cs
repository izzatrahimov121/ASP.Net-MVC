using Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class SlideItem : IEntity
{
	public int Id { get; set; }
	[Required, MaxLength(100)]
	public string? Category { get; set; }
	[Required]
	public string? Image { get; set; }
	[Required, MaxLength(150)]
	public string? Title { get; set; }
	[Required, MaxLength(250)]
	public string? Description { get; set; }
}
