using System.ComponentModel.DataAnnotations;

namespace TechProgram2025.Models
{
	public class InsuranceCategory
	{
		public int InsuranceCategoryID { get; set; }

		[Display(Name = "Наименование")]
		public string Name { get; set; }

		[Display(Name = "Описание")]
		public string Description { get; set; }

		public int? ParentCategoryID { get; set; }
		[Display(Name = "Родительская категория")]
		public InsuranceCategory? ParentCategory { get; set; }

		public ICollection<InsuranceCategory> ChildrenCategories { get; set; } = new List<InsuranceCategory>();

		public ICollection<InsuranceVariant> insuranceVariants { get; set; } = new List<InsuranceVariant>();
	}
}
