using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TechProgram2025.Models
{
	public class User
	{
		public int UserID { get; set; }

		[Display(Name = "Имя")]
		public string FullName { get; set; }

		[Display(Name = "Email")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		[Display(Name = "Пароль")]
		[DataType(DataType.Password)]
		public string? Password { get; set; }

		[Display(Name = "Номер телефона")]
		[DataType(DataType.PhoneNumber)]
		public string PhoneNumber { get; set; }

		[Display(Name = "Роль")]
		public Roles Role { get; set; }

		public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
	}
}
