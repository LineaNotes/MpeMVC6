using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class GasPricesController : Controller
    {
        private ApplicationDbContext _context;

        public GasPricesController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: GasPrices
        public IActionResult Index()
        {
            return View(_context.GasPrices.ToList());
        }

        // GET: GasPrices/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            GasPrice gasPrice = _context.GasPrices.Single(m => m.Id == id);
            if (gasPrice == null)
            {
                return HttpNotFound();
            }

            return View(gasPrice);
        }

        // GET: GasPrices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GasPrices/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(GasPrice gasPrice)
        {
            if (ModelState.IsValid)
            {
                _context.GasPrices.Add(gasPrice);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gasPrice);
        }

        // GET: GasPrices/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            GasPrice gasPrice = _context.GasPrices.Single(m => m.Id == id);
            if (gasPrice == null)
            {
                return HttpNotFound();
            }
            return View(gasPrice);
        }

        // POST: GasPrices/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(GasPrice gasPrice)
        {
            if (ModelState.IsValid)
            {
                _context.Update(gasPrice);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(gasPrice);
        }

        // GET: GasPrices/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            GasPrice gasPrice = _context.GasPrices.Single(m => m.Id == id);
            if (gasPrice == null)
            {
                return HttpNotFound();
            }

            return View(gasPrice);
        }

        // POST: GasPrices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            GasPrice gasPrice = _context.GasPrices.Single(m => m.Id == id);
            _context.GasPrices.Remove(gasPrice);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
