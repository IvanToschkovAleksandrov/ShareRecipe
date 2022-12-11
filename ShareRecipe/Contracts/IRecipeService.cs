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

        Task<int> GetRecipeCategoryIdAsync(int recipeId);

        Task<IEnumerable<string>> GetProductNamesByRecipeIdAsync(int recipeId);
        
        Task<IEnumerable<RecipeProductServiceModel>> GetAllProductsAsync();

        Task<IEnumerable<RecipeProductServiceModel>> GetAllProductsByRecipeIdAsync(int recipeId); 
        
        Task<int> CreateAsync(RecipeFormModel model);

        Task DeleteAsync(int recipeId);

        Task<bool> ExistsAsync(int id);

        Task<RecipeDetailsServiceModel> RecipeDetailsByIdAsync(int id);

        Task EditAsync(
            int recipeId,
            string title,
            string? description,
            string imageUrl,
            int categoryId,
            string ingridients);
    }
}
