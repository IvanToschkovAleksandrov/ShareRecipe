using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareRecipe.Data.Models;

namespace ShareRecipe.Data.Configurations
{
    internal class RecipeConfiguration : IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.HasData(new Recipe()
            {
                Id = 1,
                CategoryId = 1,
                Description = "Chop all ingridients and mix them together with the mayonnaise.",
                Title = "Chicken salad with mayonnaise",
                ImageUrl = "https://cdn-abioh.nitrocdn.com/iRwsMXPEdaMSNBlSqLBkXmjSJwoqRrps/assets/static/optimized/rev-f3f118f/wp-content/uploads/2014/10/Chicken-Salad-main-overhead.jpg"
            }); ;
        }
    }
}
