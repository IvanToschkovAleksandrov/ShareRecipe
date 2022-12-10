using Microsoft.EntityFrameworkCore;
using ShareRecipe.Contracts;
using ShareRecipe.Data;
using ShareRecipe.Services.Models;

namespace ShareRecipe.Services
{
    public class HomeService : IHomeService
    {
        private readonly ShareRecipeDbContext context;

        public HomeService(ShareRecipeDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Take 3 random recipes in the app. 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<RecipeIndexServiceModel>> GetRandomRecipesAsync()
        {
            Random random = new Random();

            List<RecipeIndexServiceModel> result = new List<RecipeIndexServiceModel>();
            int recipesCount = 3;

            var allRecipes = await context.Recipes
                .Select(r => new RecipeIndexServiceModel()
                {
                    Id = r.Id,
                    Title = r.Title,
                    ImageUrl = r.ImageUrl
                })
                .ToListAsync();

            for (int i = 0; i < Math.Min(recipesCount, allRecipes.Count); i++)
            {
                result.Add(allRecipes[random.Next(0, allRecipes.Count)]);
            }

            return result;
        }
    }
}
