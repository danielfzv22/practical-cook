using Microsoft.EntityFrameworkCore;
using PraticalCook.Domain.Entities;

namespace PracticalCook.Infrastructure.DataAccess
{
    public class DataContext : DbContext
    {
        public DataContext()
        {
            
        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Ingredient> Ingredients => Set<Ingredient>();

        public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();

        public DbSet<RecipeUtensil> RecipeUtensils => Set<RecipeUtensil>();

        public DbSet<Utensil> Utensils => Set<Utensil>();

        public DbSet<Recipe> Recipes => Set<Recipe>();

        public DbSet<Step> Steps => Set<Step>();

        public DbSet<User> Users => Set<User>();

        public DbSet<UserRecipe> UserRecipes => Set<UserRecipe>();

        public DbSet<Tag> Tags => Set<Tag>();

        public DbSet<RecipeTag> RecipeTags => Set<RecipeTag>();


        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("A FALLBACK CONNECTION STRING");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Recipe>()
                .HasKey(r => r.Id);

            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.RecipeUtensils)
                .WithOne()
                .HasForeignKey(ru => ru.UtensilId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Steps)
                .WithOne()
                .HasForeignKey(rs => rs.Id)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.RecipeIngredients)
                .WithOne()
                .HasForeignKey(ri => ri.IngredientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Utensil>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<RecipeUtensil>()
                .HasKey(ru => new { ru.RecipeId, ru.UtensilId });

            modelBuilder.Entity<RecipeUtensil>()
                .HasOne(ru => ru.Recipe)
                .WithMany(r => r.RecipeUtensils)
                .HasForeignKey(ru => ru.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RecipeUtensil>()
                .HasOne(ru => ru.Utensil)
                .WithMany()
                .HasForeignKey(ru => ru.UtensilId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ingredient>()
                .HasKey(i => i.Id);

            modelBuilder.Entity<RecipeIngredient>()
                .HasKey(ri => new { ri.RecipeId, ri.IngredientId });

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Recipe)
                .WithMany(r => r.RecipeIngredients)
                .HasForeignKey(ri => ri.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RecipeIngredient>()
                .HasOne(ri => ri.Ingredient)
                .WithMany()
                .HasForeignKey(ri => ri.IngredientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Step>()
                .HasKey(rs => rs.Id);

            modelBuilder.Entity<Step>()
                .HasOne(rs => rs.Recipe)
                .WithMany(r => r.Steps)
                .HasForeignKey(rs => rs.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);

            modelBuilder.Entity<User>()
                .HasMany(u => u.CreatedRecipes)
                .WithOne(r => r.CreatedByUser)
                .HasForeignKey(r => r.CreatedByUserId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserRecipes)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserIngredients)
                .WithOne(i => i.CreatedByUser)
                .HasForeignKey(i => i.CreatedByUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasMany(u => u.UserUtensils)
                .WithOne(u => u.CreatedByUser)
                .HasForeignKey(u => u.CreatedByUserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRecipe>()
                .HasKey(ur => new { ur.UserId, ur.RecipeId });

            modelBuilder.Entity<UserRecipe>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRecipes)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRecipe>()
                .HasOne(ur => ur.Recipe)
                .WithMany()
                .HasForeignKey(ur => ur.RecipeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Tag>()
                .HasKey(t => t.Id);

            modelBuilder.Entity<RecipeTag>()
                .HasKey(rt => new { rt.RecipeId, rt.TagId });

            modelBuilder.Entity<RecipeTag>()
                .HasOne(rt => rt.Recipe)
                .WithMany(r => r.RecipeTags)
                .HasForeignKey(rt => rt.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RecipeTag>()
                .HasOne(rt => rt.Tag)
                .WithMany()
                .HasForeignKey(rt => rt.TagId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}