using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PPEVend.Mvc.Models;

namespace PPEVend.Mvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            HttpContext.Session.Remove("ShoppingCart");

            return View();
        }

        public IActionResult Shop()
        {
            List<string> model = HttpContext.Session.GetObject<List<string>>("ShoppingCart")??(new List<string>());
            return View(model);
        }

        public IActionResult Cart(string id)
        {
            List<string> shoppingCart = new List<string>();
            List<string> sessionCart = HttpContext.Session.GetObject<List<string>>("ShoppingCart");
            if (sessionCart != null)
            {
                shoppingCart.AddRange(sessionCart);
            }
            
            shoppingCart.Add(id);
            HttpContext.Session.SetObject("ShoppingCart", shoppingCart);


            return View(shoppingCart);
        }

        public IActionResult Remove(string id)
        {
            List<string> sessionCart = HttpContext.Session.GetObject<List<string>>("ShoppingCart");
            if (sessionCart != null && id != null)
            {
                sessionCart.Remove(id);
            }

            HttpContext.Session.SetObject("ShoppingCart", sessionCart);

            if (sessionCart.Any())
            {
                return View("Cart", sessionCart);
            }
            else
            {
                return View("Shop", sessionCart);
            }
            
        }

        public IActionResult Scan()
        {
            List<string> model = HttpContext.Session.GetObject<List<string>>("ShoppingCart") ?? (new List<string>());
            return View(model);
        }

        public IActionResult Dispense()
        {
            List<string> model = HttpContext.Session.GetObject<List<string>>("ShoppingCart") ?? (new List<string>());
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
