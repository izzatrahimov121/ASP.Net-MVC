namespace WebUI.Areas.Admin.ViewModel.Employee;

public class EmployeeCreateViewModel
{
	public string? Fullname { get; set; }
	public string? Position { get; set; }
	public string? Description { get; set; }
	public IFormFile? Image { get; set; }
}
