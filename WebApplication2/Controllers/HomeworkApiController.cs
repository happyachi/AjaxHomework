using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeworkApiController : Controller
    {
        private readonly MyDBContext _context;

        public HomeworkApiController(MyDBContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        
    }
}
