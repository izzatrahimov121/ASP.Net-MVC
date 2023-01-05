using Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class SlideItem : IEntity
{
	public int? Id { get; set; }
	[Required,MaxLength(200)]
	public string? Title { get; set;}
	[Required, MaxLength(400)]
	public string? Description { get; set;}
	[Required]
	public string? ImageSignature { get; set;}
	[Required]
	public string? Image { get; set;}

}
