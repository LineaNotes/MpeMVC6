using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;

namespace WebApplication3.Models
{
	public class GasContextSeedData
	{
		private ApplicationDbContext _context;
		private UserManager<ApplicationUser> _userManager;
		private RoleManager<IdentityRole> _roleManager;

		public GasContextSeedData(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager)
		{
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task EnsureSeedDataAsync()
		{
			if (_context.Database == null)
			{
				throw new Exception("DB is null");
			}

			// seed Gases
			//if (SeedGases()) return;

			// seed GasPrices
			//if (SeedGasPrices()) return;

			// seed Users
			await SeedUsersRoles();
		}

		private async Task SeedUsersRoles()
		{
			if (await _userManager.FindByEmailAsync("test1@test.si") != null)
			{
				return;
			}

			if (await _roleManager.FindByNameAsync("administrator") != null)
			{
				return;
			}

			await _roleManager.CreateAsync(new IdentityRole("administrator"));

			var newUser = new ApplicationUser()
			{
				UserName = "test1@test.si",
				Email = "test1@test.si"
			};

			await _userManager.CreateAsync(newUser, "P@ssw0rd!");
			await _userManager.AddToRoleAsync(newUser, "administrator");

			await _roleManager.CreateAsync(new IdentityRole("upravnik"));

			var newUser1 = new ApplicationUser()
			{
				UserName = "test2@test.si",
				Email = "test2@test.si"
			};

			await _userManager.CreateAsync(newUser1, "!P@ssw0rd");
			await _userManager.AddToRoleAsync(newUser, "upravnik");

			await _roleManager.CreateAsync(new IdentityRole("tajnik"));

			var newUser2 = new ApplicationUser()
			{
				UserName = "test3@test.si",
				Email = "test3@test.si",
			};

			await _userManager.CreateAsync(newUser2, "!P@ssw0rd");
			await _userManager.AddToRoleAsync(newUser, "tajnik");
		}

		private bool SeedGasPrices()
		{
			if (_context.GasPrices.Any())
			{
				return true;
			}
			string jsonString = File.ReadAllText(@"files\gasPrice.json");

			GasPrice[] gasPriceArray = JsonConvert.DeserializeObject<GasPrice[]>(jsonString);
			_context.GasPrices.AddRange(gasPriceArray);

			_context.SaveChanges();
			return false;
		}

		private bool SeedGases()
		{
			if (_context.Gases.Any())
			{
				return true;
			}
			{
				string jsonString = File.ReadAllText(@"files\gas.json");

				Gas[] gasArray = JsonConvert.DeserializeObject<Gas[]>(jsonString);
				_context.Gases.AddRange(gasArray);

				_context.SaveChanges();
			}
			return false;
		}
	}
}