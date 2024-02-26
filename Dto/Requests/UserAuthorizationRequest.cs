using System;
using System.ComponentModel.DataAnnotations;
using Dto.Requests;

namespace LessonFour.Dto
{
	public class UserAuthorizationRequest
	{
		[Required]
		[EmailAddress]
		public string Email { get; set; } = string.Empty;
		[Required]
		public string Password { get; set; } = string.Empty;
		public RoleType Role { get; set; } = RoleType.User;
	}
}

