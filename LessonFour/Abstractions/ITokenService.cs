namespace LessonFour.Abstractions
{
    public interface ITokenService
	{
		string GenerateToken(string email, string roleName);
	}
}

