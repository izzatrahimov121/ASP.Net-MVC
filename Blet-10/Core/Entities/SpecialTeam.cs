using Core.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Core.Entities;

public class SpecialTeam : IEntity
{
	public int Id { get; set; }
	[Required(ErrorMessage ="Boş buraxma"),MaxLength(150)]
	public string? Fullname { get; set; }
	[Required(ErrorMessage = "Boş buraxma"), MaxLength(150)]
	public string? Position { get; set; }
	[Required(ErrorMessage = "Boş buraxma"), MaxLength(150)]
	public string? Image { get; set; }
}
