using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Core.Entities;

public class AppUser : IdentityUser
{
	[Required,MaxLength(150)]
	public string? FullName { get; set; }
	public bool? IsActive { get; set; }
}
