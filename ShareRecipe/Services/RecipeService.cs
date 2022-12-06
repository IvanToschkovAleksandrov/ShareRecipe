using Microsoft.EntityFrameworkCore;
using ShareRecipe.Contracts;
using ShareRecipe.Data;
using ShareRecipe.Data.Models;
using ShareRecipe.Models.Recipe;
using ShareRecipe.Services.Models;

namespace ShareRecipe.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly ShareRecipeDbContext context;

        public RecipeService(ShareRecipeDbContext context)
        {
            this.context = context;
        }

        public async Task<AllRecipesQueryModel> AllAsync()
        {

            throw new NotImplementedException();
        }

        public async Task<int> CreateAsync(RecipeFormModel model)
        {
            var productNames = model.Ingridients.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
            List<Product> products = new List<Product>();

            foreach (var name in productNames)
            {
                Product product = new Product()
                {
                    Name = name
                };

                products.Add(product);
            }

            var recipe = new Recipe()
            {
                Title = model.Title,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                CategoryId = model.CategoryId,
                Products = products
            };

            await context.Recipes.AddAsync(recipe);
            await context.SaveChangesAsync();
             
            return recipe.Id;
        }

        /// <summary>
        /// Get all Existing Categories in the database.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<RecipeCategoryServiceModel>> GetAllCategoriesAsync()
        {
            return await context.Categories
                .Select(c => new RecipeCategoryServiceModel()
                {
                    Id = c.Id,
                    CategoryName = c.Name
                })
                .ToListAsync();
        }

        /// <summary>
        /// Get all Existing products in the database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<RecipeProductServiceModel>> GetAllProductsAsync()
        {
            return await context.Products
                .Select(p => new RecipeProductServiceModel()
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToListAsync();
        }
    }
}
