using Microsoft.EntityFrameworkCore;
using ShareRecipe.Contracts;
using ShareRecipe.Data;
using ShareRecipe.Data.Models;
using ShareRecipe.Models.Recipe;
using ShareRecipe.Services.Models;
using System.Diagnostics.Eventing.Reader;
using System.Linq;

namespace ShareRecipe.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly ShareRecipeDbContext context;

        public RecipeService(ShareRecipeDbContext context)
        {
            this.context = context;
        }

        public async Task<RecipeQueryServiceModel> AllAsync(
            string? category = null,
            string? searchTerm = null,
            int currentPage = 1,
            int recipesPerPage = 1)
        {
            var recipeQuery = context.Recipes.AsQueryable();

            if (!string.IsNullOrEmpty(category))
            {
                recipeQuery = context.Recipes
                    .Where(r => r.Category.Name == category);
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                recipeQuery = context.Recipes
                    .Where(r =>
                    r.Title.ToLower().Contains(searchTerm.ToLower()) ||
                    (r.Description != null && r.Description.ToLower().Contains(searchTerm.ToLower())));
                //ContainsSearchTerm(r.Products, searchTerm));
            }

            var recipes = await recipeQuery
                .Skip((currentPage - 1) * recipesPerPage)
                .Take(recipesPerPage)
                .Select(r => new RecipeServiceModel()
                {
                    Id = r.Id,
                    Title = r.Title,
                    Description = r.Description,
                    ImageUrl = r.ImageUrl
                })
                .ToListAsync();

            var totalRecipesCount = await recipeQuery.CountAsync();

            return new RecipeQueryServiceModel()
            {
                TotalRecipesCount = totalRecipesCount,
                Recipes = recipes
            };
        }

        public async Task<int> CreateAsync(RecipeFormModel model)
        {
            var productNames = model.Ingridients.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
            List<Product> products = new List<Product>();

            foreach (var name in productNames)
            {
                Product product = new Product()
                {
                    Name = name.ToLower()
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

        public async Task EditAsync(int recipeId, string title, string? description, string imageUrl, int categoryId, string ingridients)
        {
            var recipe = await context.Recipes.FirstAsync(r => r.Id == recipeId);
            var allProducts = await context.Products.Where(p => p.RecipeId == recipeId).ToListAsync();

            var productNames = ingridients.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();
            List<Product> products = new List<Product>();

            foreach (var name in productNames)
            {
                Product product = new Product()
                {
                    Name = name.ToLower()
                };

                products.Add(product);
            }

            allProducts = products;

            recipe.Title = title;
            recipe.Description = description;
            recipe.ImageUrl = imageUrl;
            recipe.CategoryId = categoryId;
            recipe.Products = products;


            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Check whether the recipe exists.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> ExistsAsync(int id)
        {
            return await context.Recipes
                .AnyAsync(r => r.Id == id);
        }

        /// <summary>
        /// Get all Existing Categories in the database.
        /// </summary>
        /// <returns>bool</returns>
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
        /// Get all names of categories in tha database
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetAllCategoriesNamesAsync()
        {
            return await context.Categories
                .Select(c => c.Name)
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

        public async Task<IEnumerable<RecipeProductServiceModel>> GetAllProductsByRecipeIdAsync(int recipeId)
        {
            return await context.Products
                .Where(p => p.RecipeId == recipeId)
                .Select(p => new RecipeProductServiceModel()
                {
                    Id = p.Id,
                    Name = p.Name
                })
                .ToListAsync();
        }


        /// <summary>
        /// Get all products of the recipe with given id.
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns>List of products</returns>
        public async Task<IEnumerable<string>> GetProductNamesByRecipeIdAsync(int recipeId)
        {
            return await context.Products
                .Where(p => p.RecipeId == recipeId)
                .Select(p => p.Name)
                .ToListAsync();
        }


        /// <summary>
        /// Get category id by recipe with given reicpe id.
        /// </summary>
        /// <param name="recipeId"></param>
        /// <returns></returns>
        public async Task<int> GetRecipeCategoryIdAsync(int recipeId)
        {
            var recipe = await context.Recipes.FirstAsync(r => r.Id == recipeId);

            return recipe.CategoryId;
        }

        /// <summary>
        /// Find and return recipe by given id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<RecipeDetailsServiceModel> RecipeDetailsByIdAsync(int id)
        {
            return await context.Recipes
                .Where(r => r.Id == id)
                .Select(r => new RecipeDetailsServiceModel()
                {
                    Id = r.Id,
                    Title = r.Title,
                    ImageUrl = r.ImageUrl,
                    Category = r.Category.Name,
                    Description = r.Description
                })
                .FirstAsync();
        }

        private bool ContainsSearchTerm(IEnumerable<Product> products, string searchTerm)
        {
            foreach (var product in products)
            {
                if (product.Name == searchTerm)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
