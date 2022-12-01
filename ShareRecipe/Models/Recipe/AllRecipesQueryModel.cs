using ShareRecipe.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace ShareRecipe.Models.Recipe
{
    public class AllRecipesQueryModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Title { get; set; } = null!;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string ImageUrl { get; set; } = null!;
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
