using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShareRecipe.Data.Models;

namespace ShareRecipe.Data.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<IdentityUser>
    {
        public void Configure(EntityTypeBuilder<IdentityUser> builder)
        {
            builder.HasData(CreateUsers());
        }

        private List<IdentityUser> CreateUsers()
        {
            var users = new List<IdentityUser>();
            var hasher = new PasswordHasher<IdentityUser>();

            var user = new IdentityUser()
            {
                Id = "dea12856-c198-4129-b3f3-b893d8395082",
                Email = "firstUser@gmail.com",
                UserName = "First User"
            };

            user.PasswordHash = hasher.HashPassword(user, "firstUser13");
            users.Add(user);

            user = new IdentityUser()
            {
                Id = "6d5800ce-d726-4fc8-83d9-d6b3ac1f591e",
                Email = "secondUser@gmail.com",
                UserName = "Second User"
            };

            user.PasswordHash = hasher.HashPassword(user, "secondUser13");
            users.Add(user);

            return users;
        }
    }
}
