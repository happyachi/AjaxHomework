using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using NuGet.Protocol;
using System.Linq;
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
            var randon = new Random();
            System.Threading.Thread.Sleep(2000);
            if (randon.Next(100) > 50)
            {
                return Content("<h1>Hello World!  你好喔</h1>", "text/html", System.Text.Encoding.UTF8);
            }
            else
            {
                return NotFound();
            }
            // return Content("<h1>Hello World!</h1>", "text/plain", System.Text.Encoding.UTF8);
            // return Content("<h1>Hello World!  你好喔</h1>", "text/html", System.Text.Encoding.UTF8);

        }
        public IActionResult Cities()
        {
            var cities = _context.Addresses.Select(a => a.City).Distinct();

            return Json(cities);
        }

        public IActionResult Avatar(int id=1)
        {
            Member? member = _context.Members.Find(id);
            if(member != null)
            {
                byte[] img = member.FileData;
                return File(img, "image/jpeg");
            }

            return NotFound();
        }

        public IActionResult First()
        {
            return View();
        }

        public IActionResult Register(string name, int age)
        {
            if (string.IsNullOrEmpty(name))
            {
                name = "Guest";
            }

            return Content($"Hello, {name},you are {age} years old.");
        }

        public IActionResult Road(string city = "", string siteId = "")
        {
            var cities = _context.Addresses.Select(c=>c.City).Distinct().ToArray();
            var siteIds = new string[] { };
            var croadities = new string[] { };

            if (!string.IsNullOrEmpty(city))
            {
                siteIds = _context.Addresses
                        .Where(s=>s.City == city)
                        .Select(c => c.SiteId).Distinct().ToArray();

                if (string.IsNullOrEmpty(siteId))
                {
                    siteIds = _context.Addresses
                            .Where(s => s.City == city)
                            .Select(c => c.SiteId).Distinct().ToArray();
                }
                else
                {
                    croadities = _context.Addresses
                                .Where(s => s.City == city & s.SiteId==siteId)
                                .Select(c => c.Road).Distinct().ToArray();
                }
            }

            var js = new { cities, siteIds, croadities };
            return Json(js);
        }

        public IActionResult CheckAccount(string name)
        {
            var memberName = _context.Members.Where(m => m.Name == name).FirstOrDefault();

            var nameCheck = memberName != null;
            var result = nameCheck ? "帳號已存在" : "帳號可使用";
            var js = new { nameCheck, result };

            return Json(js);
        }

        public IActionResult CheckEmail( string email)
        {
            var memberEmail = _context.Members.Where(m => m.Email == email).FirstOrDefault();

            var nameCheck = memberEmail != null;
            var result = nameCheck ? "電子信箱已被使用" : "電子信箱可使用";
            var js = new { nameCheck, result };

            return Json(js);
        }

        public IActionResult ChickTotal(string name, string email, int age)
        {
            string result = $"你是{name}，  電子信箱是{email}，  年齡是{age}";
            return Content(result);
        }
    }
}
