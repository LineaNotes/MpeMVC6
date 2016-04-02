using System;
using System.Linq;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using WebApplication3.Models;
using WebApplication3.Services;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNet.Authorization;

namespace WebApplication3.Controllers
{
  [Authorize(Roles = "administrator,upravnik")]
  public class GasesController : Controller
  {
    private readonly ApplicationDbContext _context;
    public static DateTime sd;
    public static DateTime se;


    public GasesController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: Gases
    public IActionResult Index(DateTime startDate, DateTime endDate, string searchBtn)
    {
      var gases = from g in _context.Gases select g;

      gases = startDate > endDate
        ? gases.Where(g => g.datum == startDate)
        : gases.Where(g => g.datum >= startDate && g.datum <= endDate);


      return View(gases);
    }

    // GET: PARTIAL VIEW - Gases/_GasResultTable 
    public IActionResult GasResultTable(DateTime startDate, DateTime endDate, string searchBtn)
    {
      var gases = from g in _context.Gases select g;
      var result = "";

      gases = startDate > endDate
        ? gases.Where(g => g.datum == startDate)
        : gases.Where(g => g.datum >= startDate && g.datum <= endDate);

      if (!gases.Any() && !string.IsNullOrEmpty(searchBtn))
      {
        result = "Za izbran razpon ni vnosa";
      }

      ViewBag.Message = result;

      var sum = gases.AsEnumerable().Sum(p => p.proizvedena_kolicina);
      ViewBag.Summa = sum;

      return PartialView("_GasResultTable", gases);
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


    // GET: PARTIAL VIEW - Gases/_GasUtilization
    public IActionResult GasUtilization(DateTime startDate, DateTime endDate, string executeBtn)
    {
      sd = startDate;
      se = endDate;
      return PartialView("_GasUtilization");
    }


    public string GetAllGasesDateOutput()
    {
      var selection = (from g in _context.Gases select new { g.datum, g.proizvedena_kolicina }).ToArray();
      var list =
        selection.Select(g => new GasSelection { Datum = Conversion.getTime(g.datum), Proizvod = g.proizvedena_kolicina })
          .ToArray();

      object[,] dataFill = new object[list.Length, 2];
      for (int i = 0; i < list.Length; i++)
      {
        dataFill[i, 0] = list[i].Datum;
        dataFill[i, 1] = list[i].Proizvod;
      }

      string sArraySerializedIndented = JsonConvert.SerializeObject(dataFill, Formatting.Indented);
      return sArraySerializedIndented;
    }

    private class GasSelection
    {
      public long Datum { get; set; }
      public float Proizvod { get; set; }
    }

    public string GetUtilization(DateTime startDate, DateTime endDate, string executeBtn)
    {
      if (DateTime.Compare(sd, new DateTime(2013, 05, 06)) > 0)
      {
        startDate = sd;
        endDate = se;
      }
      //filtering
      //var selection = (from g in _context.Gases select new { g.ura_zacetnega_vpisa, g.ura_koncnega_vpisa }).ToArray();
      //if (DateTime.Compare(startDate, new DateTime(2013, 05, 07)) < 0)
      //{
      //  startDate = new DateTime(2013, 05, 07);
      //  endDate = DateTime.Now;
      //}

      var gases = from g in _context.Gases select g;
      var result = "";

      gases = startDate > endDate
        ? gases.Where(g => g.datum == startDate)
        : gases.Where(g => g.datum >= startDate && g.datum <= endDate);

      if (!gases.Any() && !string.IsNullOrEmpty(executeBtn))
      {
        result = "Za izbran razpon ni vnosa";
      }

      ViewBag.Message = result;

      var selection = gases.AsEnumerable().Select(g => new { g.ura_zacetnega_vpisa, g.ura_koncnega_vpisa }).ToArray();

      // calculations
      var count = selection.Count();

      var totalTimeSpan = new TimeSpan[count];
      var totalMin = 0f;

      for (int i = 0; i < count; i++)
      {
        totalTimeSpan[i] = selection[i].ura_koncnega_vpisa.Subtract(selection[i].ura_zacetnega_vpisa);
        totalMin += (float)totalTimeSpan[i].TotalMinutes;
      }

      var totalPercentage = (totalMin / (count * 1440)) * 100;

      //assigning to make ready for json serialization
      object[] obj = new object[2];

      GasUtil gas = new GasUtil();
      gas.Name = "V uporabi";
      gas.Y = totalPercentage;

      GasUtil gasN = new GasUtil();
      gasN.Name = "V mirovanju";
      gasN.Y = 100f - gas.Y;

      obj[0] = gas;
      obj[1] = gasN;

      string sArrIndented = JsonConvert.SerializeObject(obj, Formatting.Indented, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

      return sArrIndented;
    }

    private class GasUtil
    {
      public string Name { get; set; }
      public float Y { get; set; }
    }

  }
}