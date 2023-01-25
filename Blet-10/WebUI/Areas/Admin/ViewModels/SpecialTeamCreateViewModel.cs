using System.ComponentModel.DataAnnotations;

namespace WebUI.Areas.Admin.ViewModels;

public class SpecialTeamCreateViewModel
{
	public string? Fullname { get; set; }
	[Required(ErrorMessage = "Boş buraxma"), MaxLength(150)]
	public string? Position { get; set; }
	[Required(ErrorMessage = "Boş buraxma")]
	public IFormFile? Image { get; set; }
}
