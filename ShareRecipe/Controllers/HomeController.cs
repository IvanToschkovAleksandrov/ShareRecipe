using Microsoft.AspNetCore.Mvc;
using ShareRecipe.Models;
using ShareRecipe.Models.Home;
using System.Diagnostics;

namespace ShareRecipe.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            var model = new IndexViewModel();
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}