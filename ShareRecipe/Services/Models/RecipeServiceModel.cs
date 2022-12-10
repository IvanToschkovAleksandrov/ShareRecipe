using System.ComponentModel;

namespace ShareRecipe.Services.Models
{
    public class RecipeServiceModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;
        public string? Descriprion { get; set; }

        [DisplayName("Image Url")]
        public string ImageUrl { get; set; } = null!;

    }
}
