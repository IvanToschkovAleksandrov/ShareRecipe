using Microsoft.AspNetCore.Mvc;
using ShareRecipe.Contracts;
using ShareRecipe.Models;
using ShareRecipe.Models.Recipe;
using ShareRecipe.Services.Models;

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
        public async Task<IActionResult> All([FromQuery]AllRecipesQueryModel query)
        {
            var queryResult = await recipeService.AllAsync(
                query.Category,
                query.SearchTerm,
                query.CurrentPage,
                AllRecipesQueryModel.RecipesPerPage);

            query.TotalRecipesCount = queryResult.TotalRecipesCount;
            query.Recipes = queryResult.Recipes;

            var recipeCategories = await recipeService.GetAllCategoriesNamesAsync();
            query.Categories = recipeCategories;

            var recipeProducts = await recipeService.GetAllProductsAsync();
            query.Products = recipeProducts;

            return View(query);
        }

        public IActionResult MyFavorite()
        {
            var model = new AllRecipesQueryModel();
            return View(model);
        }

        /// <summary>
        /// Display details for recipe with the given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Details(int id)
        {
            if (await recipeService.ExistsAsync(id) == false)
            {
                return RedirectToAction(nameof(All));
            }

            var model = await recipeService.RecipeDetailsByIdAsync(id);

            return View(model);
        }

        /// <summary>
        /// Create RecipeFormModel and load existing categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            var products = await recipeService.GetAllProductsAsync();
            var categories = await recipeService.GetAllCategoriesAsync();

            var model = new RecipeFormModel()
            {
                Categories = categories,
                Products = products
            };

            return View(model);
        }

        /// <summary>
        /// Add new recipe to the database.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Add(RecipeFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await recipeService.CreateAsync(model);

            return RedirectToAction(nameof(Details));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if(await recipeService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            var recipe = await recipeService.RecipeDetailsByIdAsync(id);
            var categoryId = await recipeService.GetRecipeCategoryIdAsync(recipe.Id);
            var productNames = await recipeService.GetProductNamesByRecipeIdAsync(recipe.Id);
            var products = await recipeService.GetAllProductsByRecipeIdAsync(recipe.Id);

            RecipeFormModel model = new RecipeFormModel()
            {
                Title = recipe.Title,
                Description = recipe.Description,
                ImageUrl = recipe.ImageUrl,
                CategoryId = categoryId,
                Categories = await recipeService.GetAllCategoriesAsync(),
                Ingridients = string.Join(", ", productNames),
                Products = products
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, RecipeFormModel model)
        {
            if (await recipeService.ExistsAsync(id) == false)
            {
                return RedirectToAction(nameof(All));
            }

            if (!ModelState.IsValid)
            {
                model.Categories = await recipeService.GetAllCategoriesAsync();

                return View(model);
            }

            await recipeService.EditAsync(id, model.Title, model.Description, model.ImageUrl, model.CategoryId, model.Ingridients);

            return RedirectToAction(nameof(Details));
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (await recipeService.ExistsAsync(id) == false)
            {
                return BadRequest();
            }

            var recipe = await recipeService.RecipeDetailsByIdAsync(id);
            var model = new RecipeDetailsViewModel()
            {
                Id = recipe.Id,
                Title = recipe.Title,
                ImageUrl = recipe.ImageUrl
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RecipeDetailsViewModel model)
        {
            if (await recipeService.ExistsAsync(model.Id) == false)
            {
                return BadRequest();
            }

            await recipeService.DeleteAsync(model.Id);

            return RedirectToAction(nameof(All));
        }
    }
}
