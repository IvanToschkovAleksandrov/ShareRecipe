using Microsoft.AspNetCore.Mvc;
using ShareRecipe.Contracts;
using ShareRecipe.Models;
using ShareRecipe.Models.Recipe;

namespace ShareRecipe.Controllers
{
    public class RecipeController : Controller
    {
        private readonly IRecipeService recipeService;

        public RecipeController(IRecipeService recipeService)
        {
            this.recipeService = recipeService;
        }

        [HttpGet]
        public IActionResult All()
        {
            var model = new AllRecipesQueryModel();

            return View(model);
        }

        public IActionResult MyFavorite()
        {
            var model = new AllRecipesQueryModel();
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = new RecipeDetailsViewModel();
            return View(model);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var model = new RecipeFormModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(RecipeFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //Do some logic here. Add to the database. And then redirect to newly created recipe.

            return RedirectToAction(nameof(Details));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            RecipeFormModel model = new RecipeFormModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, RecipeFormModel model)
        {
            //validate model with ModelState
            // ToDo some logic for changing the data in the model entity and save changes

            return RedirectToAction(nameof(Details));
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            return View(new RecipeDetailsViewModel());
        }

        [HttpGet]
        public IActionResult Delete(RecipeDetailsViewModel model)
        {
            return RedirectToAction(nameof(All));
        }
    }
}
