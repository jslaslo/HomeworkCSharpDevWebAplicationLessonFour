using System;
namespace LessonFour.Abstractions
{
	public interface IEncryptService
	{
		byte[] GenerateSalt();
		byte[] HashPassword(string password, byte[] salt);
	}
}

