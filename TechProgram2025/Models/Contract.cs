using System.ComponentModel.DataAnnotations;

namespace TechProgram2025.Models
{
	public class Contract
	{
		public int ContractID { get; set; }

		[Display(Name = "Клиент")]
		public int ClientID { get; set; }
		public Client Client { get; set; }

		[Display(Name = "Высокий риск")]
		public bool IsProblematic { get; set; }

		[Display(Name = "Виды страхования")]
		public ICollection<InsuranceVariant> InsuranceVariants { get; set; } = new List<InsuranceVariant>();

		[Display(Name = "Страховой агент")]
		public int AgentUserID { get; set; }
		public User Agent { get; set; }
	}
}
