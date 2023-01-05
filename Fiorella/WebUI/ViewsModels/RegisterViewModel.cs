using System.ComponentModel.DataAnnotations;

namespace WebUI.ViewsModels;

public class RegisterViewModel
{
	[Required,MaxLength(150)]
	public string? Fullname { get; set; }
	[Required, MaxLength(150)]
	public string? Username { get; set; }
	[Required, MaxLength(256),DataType(DataType.EmailAddress)]
	public string? Email { get; set; }
	[Required,DataType(DataType.Password)]
	public string? Password { get; set; }
	[Required,DataType(DataType.Password),Compare("Password")]
	public string? ConfirmPassword { get; set; }
}
