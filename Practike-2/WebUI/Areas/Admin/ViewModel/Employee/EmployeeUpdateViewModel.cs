using System.ComponentModel.DataAnnotations;

namespace WebUI.Areas.Admin.ViewModel.Employee;

public class EmployeeUpdateViewModel
{
	public int Id { get; set; }

	[Required, MaxLength(100)]
	public string? Fullname { get; set; }

	[Required, MaxLength(150)]
	public string? Position { get; set; }

	[Required, MaxLength(250)]
	public string? Description { get; set; }

	[Required]
	public IFormFile? Image { get; set; }

	public string? ImagePath { get; set; }
}
