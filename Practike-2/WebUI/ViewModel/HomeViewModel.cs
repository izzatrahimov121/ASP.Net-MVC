using Core.Entities;

namespace WebUI.ViewModel;

public class HomeViewModel
{
	public IEnumerable<Employee> Employees { get; set; } = null!;
}
