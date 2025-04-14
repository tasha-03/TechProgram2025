using System.ComponentModel.DataAnnotations;

namespace TechProgram2025.Models
{
	public class InsuranceVariant
	{
		public int InsuranceVariantID { get; set; }

		[Display(Name = "Наименование")]
		public string Name { get; set; }

		[Display(Name = "Описание")]
		public string Description { get; set; }

		[Display(Name = "Категория")]
		public int CategoryID { get; set; }
		[Display(Name = "Категория")]
		public InsuranceCategory Category { get; set; }

		[Display(Name = "Цена")]
		[DataType(DataType.Currency)]
		public decimal Price { get; set; }

		public ICollection<Contract> Contracts { get; set; } = new List<Contract>();
	}
}
