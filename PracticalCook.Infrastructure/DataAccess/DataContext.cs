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

        public DbSet<RecipeStep> RecipeSteps => Set<RecipeStep>();

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
                .HasMany(r => r.RecipeSteps)
                .WithOne()
                .HasForeignKey(rs => rs.StepId)
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

            modelBuilder.Entity<Step>()
                .HasKey(s => s.Id);

            modelBuilder.Entity<RecipeStep>()
                .HasKey(rs => rs.Id );

            modelBuilder.Entity<RecipeStep>()
                .HasOne(rs => rs.Recipe)
                .WithMany(r => r.RecipeSteps)
                .HasForeignKey(rs => rs.RecipeId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RecipeStep>()
                .HasOne(rs => rs.Step)
                .WithMany()
                .HasForeignKey(rs => rs.StepId)
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

            modelBuilder.Entity<RecipeStepUtensil>()
                .HasKey(rs => new { rs.RecipeStepId, rs.UtensilId });

            modelBuilder.Entity<RecipeStepUtensil>()
                .HasOne(rs => rs.RecipeStep)
                .WithMany(rs => rs.StepUtensils)
                .HasForeignKey(rs => rs.RecipeStepId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RecipeStepUtensil>()
                .HasOne(rs => rs.Utensil)
                .WithMany()
                .HasForeignKey(rs => rs.UtensilId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}