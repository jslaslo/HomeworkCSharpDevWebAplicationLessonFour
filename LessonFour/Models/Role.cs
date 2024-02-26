using System;
using System.ComponentModel.DataAnnotations;

namespace LessonFour.Models
{
	public class Role
	{
		[Key]public Guid Id { get; set; }
		public required string Name { get; set; } = string.Empty;
	}
}

