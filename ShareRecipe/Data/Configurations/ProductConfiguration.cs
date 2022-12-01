using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareRecipe.Data.Models;

namespace ShareRecipe.Data.Configurations
{
    internal class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(CreateProducts());
        }

        private List<Product> CreateProducts()
        {
            string[] productArray = { "Chicken", "Mayonnaise", "Almonds", "Celery" };
            var products = new List<Product>();

            for (int i = 0; i < productArray.Length; i++)
            {
                products.Add(new Product()
                {
                    Id = i + 1,
                    Name = productArray[i],
                    RecipeId = 1
                });
            }

            return products;
        }
    }
}
