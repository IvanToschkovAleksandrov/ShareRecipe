using ShareRecipe.Data.Models;
using ShareRecipe.Services.Models;
using System.ComponentModel.DataAnnotations;

namespace ShareRecipe.Models.Recipe
{
    public class AllRecipesQueryModel
    {
        public const int RecipesPerPage = 3;

        public string? Category { get; init; }

        [Display(Name = "Search by text")]
        public string? SearchTerm { get; init; }

        public int CurrentPage { get; init; } = 1;

        public int TotalRecipesCount { get; set; }

        public IEnumerable<string> Categories { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<RecipeServiceModel> Recipes { get; set; } = new List<RecipeServiceModel>();

        public IEnumerable<RecipeProductServiceModel> Products { get; set; } = new List<RecipeProductServiceModel>();
    }
}
