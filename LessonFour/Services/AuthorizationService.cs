using System;
using Dto.Responses;
using LessonFour.Abstractions;
using LessonFour.Database.Contexts;
using LessonFour.Dto;
using LessonFour.Models;
using Microsoft.EntityFrameworkCore;

namespace LessonFour.Services
{
    public class AuthorizationService : IAuthorizService
    {
        private readonly AuthorizationContext _context;
        private readonly ITokenService _token;
        private readonly IEncryptService _encrypt;

        public AuthorizationService(ITokenService token, AuthorizationContext context, IEncryptService encrypt)
        {
            _token = token;
            _context = context;
            _encrypt = encrypt;
        }

        public async Task<IResult> Login(UserAuthorizationRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user is null)
            {
                return Results.NotFound();
            }

            var password = _encrypt.HashPassword(request.Password, user.Salt);

            if (!user.Password.SequenceEqual(password))
            {
                return Results.BadRequest();
            }

            var role = await _context.Roles.FirstAsync(r => r.Id == user.RoleId);
            var token = _token.GenerateToken(user.Email, role.Name.ToString());

            return Results.Ok(token);
        }

        public async Task<IResult> Register(UserAuthorizationRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user is not null)
            {
                return Results.Conflict();
            }

            var role = await _context.Roles.FirstAsync(r => r.Name == request.Role.ToString());
            var salt = _encrypt.GenerateSalt();

            user = new User()
            {
                Email = request.Email,
                Password = _encrypt.HashPassword(request.Password, salt),
                Salt = salt,
                RoleId = role.Id
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            var token = _token.GenerateToken(user.Email, role.Name.ToString());

            return Results.Ok(token);
        }
    }
}

