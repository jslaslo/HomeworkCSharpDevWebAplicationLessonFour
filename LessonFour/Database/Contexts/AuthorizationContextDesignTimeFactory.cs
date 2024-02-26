using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace LessonFour.Database.Contexts
{
    public class AuthorizationContextDesignTimeFactory : IDesignTimeDbContextFactory<AuthorizationContext>
    {
       public AuthorizationContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AuthorizationContext>();
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=authorization_users_db;Username=postgres;Password=example");
            return new AuthorizationContext(optionsBuilder.Options);
        }
    }
}

