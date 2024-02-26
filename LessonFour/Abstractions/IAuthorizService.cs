using System;
using Dto.Responses;
using LessonFour.Dto;

namespace LessonFour.Abstractions
{
    public interface IAuthorizService
	{
		Task<IResult> Login(UserAuthorizationRequest request);
		Task<IResult> Register(UserAuthorizationRequest request);
	}
}

