using ShareRecipe.Models.Recipe;
using ShareRecipe.Services.Models;

namespace ShareRecipe.Contracts
{
    public interface IRecipeService
    {
        Task<AllRecipesQueryModel> AllAsync();
        Task<IEnumerable<RecipeCategoryServiceModel>> GetAllCategoriesAsync();
        Task<IEnumerable<RecipeProductServiceModel>> GetAllProductsAsync();
        Task<int> CreateAsync(RecipeFormModel model);
    }
}
