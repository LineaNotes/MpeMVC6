using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication3.Models
{
	public class UserRoleViewModel
	{
		public ApplicationUser User { get; set; }

		public Collection<IdentityRole> Roles { get; set; }
	}
}
