using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebUI.ViewsModels;

public class LoginViewModel
{
	[Required]
	public string? UsernameOrEmail { get; set; }
	[Required,DataType(DataType.Password)]
	public string? Password { get; set; }
	public bool? RememberMe { get; set; }
}
