namespace ShareRecipe.Models.Recipe
{
    public class RecipeDetailsViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
    }
}
