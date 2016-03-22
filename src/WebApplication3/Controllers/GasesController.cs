using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
	public class GasesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public GasesController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Gases
		public IActionResult Index(DateTime startDate, DateTime endDate, string test)
		{
			var gases = from g in _context.Gases select g;
			var result = "";

			gases = startDate > endDate
				? gases.Where(g => g.datum == startDate)
				: gases.Where(g => g.datum >= startDate && g.datum <= endDate);

			if (!gases.Any() && !string.IsNullOrEmpty(test))
			{
				result = "Za izbran razpon ni vnosa";
			}

			ViewBag.Message = result;

			var sum = gases.AsEnumerable().Sum(p => p.proizvedena_kolicina);
			ViewBag.Summa = sum;

			return View(gases);
		}

		// GET: Gases/Details/5
		public IActionResult Details(int? id)
		{
			if (id == null)
			{
				return HttpNotFound();
			}

			Gas gas = _context.Gases.Single(m => m.Id == id);
			if (gas == null)
			{
				return HttpNotFound();
			}

			return View(gas);
		}

		// GET: Gases/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: Gases/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(Gas gas)
		{
			if (ModelState.IsValid)
			{
				_context.Gases.Add(gas);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(gas);
		}

		// GET: Gases/Edit/5
		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return HttpNotFound();
			}

			Gas gas = _context.Gases.Single(m => m.Id == id);
			if (gas == null)
			{
				return HttpNotFound();
			}
			return View(gas);
		}

		// POST: Gases/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Gas gas)
		{
			if (ModelState.IsValid)
			{
				_context.Update(gas);
				_context.SaveChanges();
				return RedirectToAction("Index");
			}
			return View(gas);
		}

		// GET: Gases/Delete/5
		[ActionName("Delete")]
		public IActionResult Delete(int? id)
		{
			if (id == null)
			{
				return HttpNotFound();
			}

			Gas gas = _context.Gases.Single(m => m.Id == id);
			if (gas == null)
			{
				return HttpNotFound();
			}

			return View(gas);
		}

		// POST: Gases/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public IActionResult DeleteConfirmed(int id)
		{
			Gas gas = _context.Gases.Single(m => m.Id == id);
			_context.Gases.Remove(gas);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}

		// GET: Gases/Graphs
		public IActionResult Graphs()
		{
			return View();
		}
	}
}