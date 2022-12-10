using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShareRecipe.Data.Models
{
    public class Recipe
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(30)]
        public string Title { get; set; } = null!;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; } = null!;

        public IEnumerable<Product> Products { get; set; } = new List<Product>();

        [ForeignKey(nameof(Category))]
        public int CategoryId { get; set; }

        public Category Category { get; set; } = null!;
    }
}
