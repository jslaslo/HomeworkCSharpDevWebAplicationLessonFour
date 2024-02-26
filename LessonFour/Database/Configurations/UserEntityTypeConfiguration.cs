using Dto.Requests;
using LessonFour.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LessonFour.Database.Configurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Email).IsRequired().HasMaxLength(64);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(128);
            builder.Property(u => u.Salt).IsRequired();

            builder.HasOne<Role>().WithMany().HasForeignKey(u => u.RoleId);
        }
    }
}

