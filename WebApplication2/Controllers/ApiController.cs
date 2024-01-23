using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ApiController : Controller
    {
        private readonly MyDBContext _context;

        public ApiController(MyDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Cities()
        {
            var cities = _context.Addresses.Select(a => a.City);
            return Json(cities);
        }
    }
}
