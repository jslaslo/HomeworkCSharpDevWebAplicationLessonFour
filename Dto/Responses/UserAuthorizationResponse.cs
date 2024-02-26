using System;
namespace Dto.Responses
{
	public class UserAuthorizationResponse
	{
		public required string Email { get; set; }
		public required string Token { get; set; }
		public required string RoleName { get; set; }
	}
}

