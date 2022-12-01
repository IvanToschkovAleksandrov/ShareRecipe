using ShareRecipe.Models.Recipe;

namespace ShareRecipe.Contracts
{
    public interface IRecipeService
    {
        Task<AllRecipesQueryModel> AllAsync();
    }
}
