using System.ComponentModel.DataAnnotations;

namespace WebUI.ViewModels;

public class ForgotPasswordViewModel
{

	[Required, MaxLength(256), DataType(DataType.EmailAddress)]
	public string? YourEmail { get; set; }

	[Required, MinLength(8), DataType(DataType.Password)]
	public string? NewPassword { get; set; }

	[Required, MinLength(8), DataType(DataType.Password), Compare(nameof(NewPassword))]
	public string? ConfirmPassword { get; set; }
}
