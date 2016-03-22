using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace WebApplication3.Controllers
{
	[Authorize(Roles = "administrator")]
	public class AuthController : Controller
	{
		// GET: Auth/Index/
		public IActionResult Index()
		{
			return View();
		}
	}
}