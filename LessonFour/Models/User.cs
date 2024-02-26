using System;
using System.ComponentModel.DataAnnotations;

namespace LessonFour.Models
{
    public class User
    {
        [Key] public Guid Id { get; set; } = Guid.NewGuid();
        public required string Email { get; set; }
        public byte[] Password { get; set; } = Array.Empty<byte>();
        public byte[] Salt { get; set; } = Array.Empty<byte>();
        public Guid RoleId { get; set; }
    }
}

