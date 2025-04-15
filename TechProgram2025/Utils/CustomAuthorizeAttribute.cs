using Microsoft.AspNetCore.Authorization;
using TechProgram2025.Models;

namespace TechProgram2025.Utils
{
	public class CustomAuthorizeAttribute : AuthorizeAttribute
	{
		public CustomAuthorizeAttribute(Roles rolesEnum)
		{
			Roles = rolesEnum.ToString().Replace(" ", string.Empty);
		}
	}
}
