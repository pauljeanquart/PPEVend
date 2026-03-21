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
            List<string> shoppingCart = HttpContext.Session.GetObject<List<string>>("ShoppingCart") 
                ?? (new List<string>());
            return View(shoppingCart);
        }

        public IActionResult Cart(string id)
        {
            List<string> shoppingCart = HttpContext.Session.GetObject<List<string>>("ShoppingCart") 
                ?? (new List<string>());

            if (!string.IsNullOrEmpty(id))
            {
                shoppingCart.Add(id);
                HttpContext.Session.SetObject("ShoppingCart", shoppingCart);
            }


            if (shoppingCart.Any())
            {
                return View(shoppingCart);
            }
            else
            {
                return View("Shop", shoppingCart);
            }
        }

        public IActionResult Remove(string id)
        {
            List<string> shoppingCart = HttpContext.Session.GetObject<List<string>>("ShoppingCart") 
                ?? (new List<string>());
            if (!string.IsNullOrEmpty(id))
            {
                shoppingCart.Remove(id);
                HttpContext.Session.SetObject("ShoppingCart", shoppingCart);
            }

            if (shoppingCart.Any())
            {
                return View("Cart", shoppingCart);
            }
            else
            {
                return View("Shop", shoppingCart);
            }
        }

        public IActionResult Scan()
        {
            List<string> shoppingCart = HttpContext.Session.GetObject<List<string>>("ShoppingCart") 
                ?? (new List<string>());
            return View(shoppingCart);
        }

        public IActionResult Dispense()
        {
            List<string> shoppingCart = HttpContext.Session.GetObject<List<string>>("ShoppingCart") 
                ?? (new List<string>());
            if (shoppingCart.Any())
            {
                return View(shoppingCart);
            }
            else
            {
                return View("Shop", shoppingCart);
            }
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
