using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using System.Linq;
using System.Security.Claims;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
	[Authorize(Roles = "administrator")]
	public class AuthController : Controller
	{
		private ApplicationDbContext _context;

		public AuthController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Auth/Index/
		public IActionResult Index()
		{
			var users = _context.Users.Where(u => u.UserName != User.GetUserName()); 
			return View(users);
		}

		// GET: Auth/Details/5
		public IActionResult Details(string id)
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