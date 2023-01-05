using System.ComponentModel.DataAnnotations;

namespace WebUI.Areas.Admin.ViewsModels.Slider;

public class SlideItemViewModel
{
    [Required, MaxLength(200)]
    public string? Title { get; set; }
    [Required, MaxLength(400)]
    public string? Description { get; set; }
    [Required]
    public IFormFile? ImageSignature { get; set; }
    [Required]
    public IFormFile? Image { get; set; }

}
