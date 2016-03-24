using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
	[Authorize(Roles = "administrator")]
	public class AuthController : Controller
	{
		private ApplicationDbContext _context;
		private UserManager<ApplicationUser> _userManager;
		private RoleManager<IdentityRole> _roleManager;

		public AuthController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
			RoleManager<IdentityRole> roleManager)
		{
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		// GET: Auth/Index/
		public IActionResult Index()
		{
			var users = _context.Users.Where(u => u.UserName != User.GetUserName()); 
			return View(users);
		}

		// GET: Auth/Details/5
		public async Task<IActionResult> Details(string id)
		{
			if (id == null)
			{
				return HttpNotFound();
			}

			ApplicationUser user = _context.Users.Single(m => m.Id == id);
			if (user == null)
			{
				return HttpNotFound();
			}

			var model = new UserRoleViewModel();

			var roles = await _userManager.GetRolesAsync(user);
			var rolesCollection = new Collection<IdentityRole>();

			foreach (var item in roles)
			{
				var role = await _roleManager.FindByNameAsync(item);
				rolesCollection.Add(role);
			}

			model = new UserRoleViewModel { User = user, Roles = rolesCollection };

			return View(model);
		}

		// GET: Auth/Edit/5
		public IActionResult Edit(string id)
		{
			if (id == null)
			{
				return HttpNotFound();
			}

			ApplicationUser user = _context.Users.Single(m => m.Id == id);
			if (user == null)
			{
				return HttpNotFound();
			}
			return View(user);
		}

		// POST: Auth/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(ApplicationUser user)
		{
			if (ModelState.IsValid)
			{
				_context.Update(user);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(user);
		}

		// GET: Auth/Delete/5
		[ActionName("Delete")]
		public IActionResult Delete(string id)
		{
			if (id == null)
			{
				return HttpNotFound();
			}

			ApplicationUser user = _context.Users.Single(m => m.Id == id);
			if (user == null)
			{
				return HttpNotFound();
			}

			return View(user);
		}

		// POST: Auth/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(string id)
		{
			ApplicationUser user = _context.Users.Single(m => m.Id == id);
			_context.Users.Remove(user);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}