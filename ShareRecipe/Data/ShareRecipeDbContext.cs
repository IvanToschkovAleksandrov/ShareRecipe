using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ShareRecipe.Data.Configurations;
using ShareRecipe.Data.Models;

namespace ShareRecipe.Data
{
    public class ShareRecipeDbContext : IdentityDbContext
    {
        public ShareRecipeDbContext(DbContextOptions<ShareRecipeDbContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipes { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RecipeConfiguration());
            builder.ApplyConfiguration(new ProductConfiguration());
            builder.ApplyConfiguration(new CategoryConfiguration());
            builder.ApplyConfiguration(new UserConfiguration());
        }
    }
}