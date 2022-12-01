using System.ComponentModel.DataAnnotations;

namespace ShareRecipe.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; } = null!;

        public IEnumerable<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}