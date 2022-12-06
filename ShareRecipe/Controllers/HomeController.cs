using Microsoft.AspNetCore.Mvc;
using ShareRecipe.Contracts;
using ShareRecipe.Models;
using ShareRecipe.Models.Home;
using System.Diagnostics;

namespace ShareRecipe.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHomeService homeService;

        public HomeController(IHomeService homeService)
        {
            this.homeService = homeService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await homeService.GetRandomRecipesAsync();
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}