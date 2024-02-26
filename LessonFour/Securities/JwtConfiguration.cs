using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace LessonFour.Securities
{
	public class JwtConfiguration
	{
		public required string Key { get; init; }
        public required string Issuer { get; init; }
        public required string Audience { get; init; }

        internal SymmetricSecurityKey GetSigningKey()
        {
            return new(Encoding.UTF8.GetBytes(Key));
        }
    }
}

