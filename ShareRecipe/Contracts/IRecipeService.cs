using ShareRecipe.Models.Recipe;
using ShareRecipe.Services.Models;

namespace ShareRecipe.Contracts
{
    public interface IRecipeService
    {
        Task<RecipeQueryServiceModel> AllAsync(
            string? category = null,
            string? searchTerm = null,
            int currentPage = 1,
            int recipePerPage = 1);
        
        Task<IEnumerable<RecipeCategoryServiceModel>> GetAllCategoriesAsync();

        Task<IEnumerable<string>> GetAllCategoriesNamesAsync();
        
        Task<IEnumerable<RecipeProductServiceModel>> GetAllProductsAsync();
        
        Task<int> CreateAsync(RecipeFormModel model);

        Task<bool> Exist(int id);

        Task<RecipeDetailsServiceModel> RecipeDetailsByIdAsync(int id);
    }
}
