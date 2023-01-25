using System.ComponentModel.DataAnnotations;

namespace WebUI.ViewModels;

public class LoginViewModel
{
	[Required,MaxLength(256)]
	public string? UsernameOrEmail { get; set; }
	[Required,DataType(DataType.Password)]
	public string? Password { get; set; }
	public bool RememberMe { get; set; }
}
