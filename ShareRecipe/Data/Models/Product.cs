using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareRecipe.Data.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Name { get; set; } = null!;

        [ForeignKey(nameof(Recipe))]
        public int  RecipeId { get; set; }
        public Recipe Recipe { get; set; } = null!;

    }
}
