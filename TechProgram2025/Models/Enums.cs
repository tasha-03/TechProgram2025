using System.ComponentModel.DataAnnotations;

namespace TechProgram2025.Models
{
	public enum LegalEntityType
	{
		[Display(Name = "Физическое лицо")]
		IndividualEntity = 0,
		[Display(Name = "Юридическое лицо")]
		LegalEntity = 1
	}

	public enum Roles 
	{
		[Display(Name = "Администратор")]
		Admin = 0,
		[Display(Name = "Агент")]
		Agent = 1
	}

	public enum ContractStatus
	{
		[Display(Name = "Новый")]
		New = 0,
		[Display(Name = "Действующий")]
		Active = 1,
		[Display(Name = "Неактивный")]
		Inactive = 2
	}
}
