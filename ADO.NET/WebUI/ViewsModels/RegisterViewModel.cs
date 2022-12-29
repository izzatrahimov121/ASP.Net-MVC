using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = System.ComponentModel.DataAnnotations.RequiredAttribute;

namespace WebUI.ViewsModels;

public class RegisterViewModel
{
	[Required, MaxLength(100)]
	public string? Fullname { get; set; }
	[Required, MaxLength(50)]
	public string? Username { get; set; }
	[Required, MaxLength(256), DataType(DataType.EmailAddress)]
	public string? EmailAddress { get; set; }
	[Required, DataType(DataType.Password)]
	public string? Password { get; set; }
	[Required, DataType(DataType.Password), Compare(nameof(Password))]
	public string? ConfirmPassword { get; set; }
}
