using ShareRecipe.Services.Models;

namespace ShareRecipe.Contracts
{
    public interface IHomeService
    {
        Task<IEnumerable<RecipeIndexServiceModel>> GetRandomRecipesAsync();
    }
}
