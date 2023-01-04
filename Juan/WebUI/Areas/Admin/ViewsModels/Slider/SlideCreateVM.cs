using System.ComponentModel.DataAnnotations;

namespace WebUI.Areas.Admin.ViewsModels.Slider;

public class SlideCreateVM
{
	[Required, MaxLength(100)]
	public string? Category { get; set; }
	[Required]
	public IFormFile? Image { get; set; }
	[Required, MaxLength(150)]
	public string? Title { get; set; }
	[Required, MaxLength(250)]
	public string? Description { get; set; }
}
