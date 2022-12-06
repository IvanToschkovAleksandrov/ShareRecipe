﻿using Microsoft.EntityFrameworkCore;
using ShareRecipe.Contracts;
using ShareRecipe.Data;
using ShareRecipe.Data.Models;
using ShareRecipe.Models.Recipe;

namespace ShareRecipe.Services
{
    public class RecipeService : IRecipeService
    {
        private readonly ShareRecipeDbContext context;

        public RecipeService(ShareRecipeDbContext context)
        {
            this.context = context;
        }

        public Task<AllRecipesQueryModel> AllAsync()
        {
            //var allRecipes = await context<Recipe>()
            //    .Select(r => new AllRecipesQueryModel()
            //    {

            //    })
            //    .ToListAsync();
            throw new NotImplementedException();
        }
    }
}