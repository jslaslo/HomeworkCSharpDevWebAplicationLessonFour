using System;
using System.Text;
using LessonFour.Abstractions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace LessonFour.Services
{
    public class EncryptService : IEncryptService
	{
		public EncryptService()
		{
		}

        public byte[] GenerateSalt()
        {
            return Encoding.UTF8.GetBytes(Guid.NewGuid().ToString());
        }

        public byte[] HashPassword(string password, byte[] salt)
        {
            return KeyDerivation.Pbkdf2
                (
                    password: password,
                    salt: salt,
                    prf: KeyDerivationPrf.HMACSHA512,
                    iterationCount: 1000,
                    numBytesRequested: 512/8
                ) ;
        }
    }
}

