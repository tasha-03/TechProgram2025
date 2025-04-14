using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;

namespace TechProgram2025.Models
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options) 
			: base(options) 
		{ 
			Database.EnsureCreated();
		}
		public DbSet<User> Users { get; set; } = null!;
		public DbSet<Client> Clients { get; set; }
		public DbSet<InsuranceVariant> InsuranceVariants { get; set; }
		public DbSet<Contract> Contracts { get; set; }
		public DbSet<InsuranceCategory> InsuranceCategories { get; set; }
	}
}
