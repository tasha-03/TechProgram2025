using System.ComponentModel.DataAnnotations;

namespace TechProgram2025.Models
{
	public class Client
	{
		public int ClientID { get; set; }

		[Display(Name = "Имя клиента/Наименование организации")]
		public string ClientName { get; set; }

		[Display(Name = "Тип клиента")]
		public LegalEntityType EntityType { get; set; }

		public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
	}
}
