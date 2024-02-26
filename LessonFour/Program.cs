
using LessonFour.Abstractions;
using LessonFour.Database.Contexts;
using LessonFour.Securities;
using LessonFour.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace LessonFour;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AuthorizationContext>(options =>
        options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSql")
        ?? throw new Exception("PstgreSql not found in configuration"))
        );

        builder.Services.AddScoped<IAuthorizService, AuthorizationService>();
        builder.Services.AddScoped<IEncryptService, EncryptService>();
        builder.Services.AddScoped<ITokenService, TokenService>();

        builder.Services.AddSwaggerGen(
            options =>
            {
                options.AddSecurityDefinition(
                JwtBearerDefaults.AuthenticationScheme,
                new()
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert jwt with Bearer into filed",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "Jwt Token",
                    Scheme = JwtBearerDefaults.AuthenticationScheme
                });
                options.AddSecurityRequirement(
                    new()
                    {
                        {
                             new OpenApiSecurityScheme
                             {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = JwtBearerDefaults.AuthenticationScheme
                                }
                             },
                             new List<string>()
                        }

                    });
            });

        var jwt = builder.Configuration.GetSection("JwtConfiguration").Get<JwtConfiguration>()
            ?? throw new Exception("JwtConfiguration not found");
        builder.Services.AddSingleton(provider => jwt);
        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
            options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwt.Issuer,
                    ValidAudience = jwt.Audience,
                    IssuerSigningKey = jwt.GetSigningKey()
                };
            });

        builder.Services.AddControllers();


        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }


        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

