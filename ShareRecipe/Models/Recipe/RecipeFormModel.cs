using System.ComponentModel.DataAnnotations;
using ShareRecipe.Services.Models;

namespace ShareRecipe.Models.Recipe
{
    public class RecipeFormModel
    {
        [Required]
        [StringLength(30, MinimumLength = 2)]
        public string Title { get; set; } = null!;

        [StringLength(500, MinimumLength = 10)]
        public string? Description { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string ImageUrl { get; set; } = null!;

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Ingridients { get; set; }

        public IEnumerable<RecipeProductServiceModel> Products { get; set; } = new List<RecipeProductServiceModel>();

        public int CategoryId { get; set; }
        public IEnumerable<RecipeCategoryServiceModel> Categories { get; set; } = new List<RecipeCategoryServiceModel>();
    }
}
