using Microsoft.EntityFrameworkCore;
using PraticalCook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public DbSet<Step> RecipeSteps => Set<Step>();

        public DbSet<Utensil> Utensils => Set<Utensil>();

        public DbSet<Recipe> Recipes => Set<Recipe>();

        public DbSet<Step> Steps => Set<Step>();

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
        }
    }
}