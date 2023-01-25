using System.ComponentModel.DataAnnotations;

namespace WebUI.Areas.Admin.ViewModels;

public class SpecialTeamUpdateViewModel
{
	public int Id { get; set; }

	[Required, MaxLength(100)]
	public string? Fullname { get; set; }

	public IFormFile? Image { get; set; }

	[Required, MaxLength(150)]
	public string? Position { get; set; }

	public string? ImagePath { get; set; }
}
