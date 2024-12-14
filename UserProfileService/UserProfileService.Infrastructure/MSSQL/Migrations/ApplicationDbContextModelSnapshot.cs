﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserProfileService.Infrastructure.MSSQL;

#nullable disable

namespace UserProfileService.Infrastructure.MSSQL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("UserProfileServiceSchema")
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UserProfileService.Domain.Entities.DayResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("Date")
                        .HasColumnType("date");

                    b.Property<int>("GlassesOfWater")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasDefaultValue(0);

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.Property<double?>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("DayResults", "UserProfileServiceSchema");
                });

            modelBuilder.Entity("UserProfileService.Domain.Entities.EatenFood", b =>
                {
                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.Property<int>("MealId")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("FoodId", "MealId");

                    b.HasIndex("MealId");

                    b.ToTable("EatenFoods", "UserProfileServiceSchema");
                });

            modelBuilder.Entity("UserProfileService.Domain.Entities.Food", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("Calories")
                        .HasColumnType("float");

                    b.Property<double>("Carbohydrates")
                        .HasColumnType("float");

                    b.Property<double>("Fats")
                        .HasColumnType("float");

                    b.Property<string>("FoodType")
                        .IsRequired()
                        .HasMaxLength(13)
                        .HasColumnType("nvarchar(13)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProfileId")
                        .HasColumnType("int");

                    b.Property<double>("Proteins")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Food", "UserProfileServiceSchema");

                    b.HasDiscriminator<string>("FoodType").HasValue("Food");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("UserProfileService.Domain.Entities.IngredientOfDish", b =>
                {
                    b.Property<int>("DishId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("DishId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("IngredientOfDishes", "UserProfileServiceSchema");
                });

            modelBuilder.Entity("UserProfileService.Domain.Entities.Meal", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("DayId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DayId");

                    b.ToTable("Meals", "UserProfileServiceSchema");
                });

            modelBuilder.Entity("UserProfileService.Domain.Entities.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("ActivityLevel")
                        .HasColumnType("float");

                    b.Property<DateOnly>("Birthday")
                        .HasColumnType("date");

                    b.Property<DateOnly?>("DateOfStartPlan")
                        .HasColumnType("date");

                    b.Property<int>("DesiredGlassesOfWater")
                        .HasColumnType("int");

                    b.Property<int>("Gender")
                        .HasColumnType("int");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<int?>("MealPlanId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<double>("Weight")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("UserId", "Name")
                        .IsUnique();

                    b.ToTable("Profiles", "UserProfileServiceSchema");
                });

            modelBuilder.Entity("UserProfileService.Domain.Entities.Dish", b =>
                {
                    b.HasBaseType("UserProfileService.Domain.Entities.Food");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("WeightOfPortion")
                        .HasColumnType("float");

                    b.HasDiscriminator().HasValue("Dish");
                });

            modelBuilder.Entity("UserProfileService.Domain.Entities.Ingredient", b =>
                {
                    b.HasBaseType("UserProfileService.Domain.Entities.Food");

                    b.HasDiscriminator().HasValue("Ingredient");
                });

            modelBuilder.Entity("UserProfileService.Domain.Entities.DayResult", b =>
                {
                    b.HasOne("UserProfileService.Domain.Entities.Profile", null)
                        .WithMany("DayResults")
                        .HasForeignKey("ProfileId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserProfileService.Domain.Entities.EatenFood", b =>
                {
                    b.HasOne("UserProfileService.Domain.Entities.Food", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("UserProfileService.Domain.Entities.Meal", null)
                        .WithMany("Foods")
                        .HasForeignKey("MealId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");
                });

            modelBuilder.Entity("UserProfileService.Domain.Entities.IngredientOfDish", b =>
                {
                    b.HasOne("UserProfileService.Domain.Entities.Dish", null)
                        .WithMany("Ingredients")
                        .HasForeignKey("DishId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("UserProfileService.Domain.Entities.Ingredient", "Ingredient")
                        .WithMany()
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("UserProfileService.Domain.Entities.Meal", b =>
                {
                    b.HasOne("UserProfileService.Domain.Entities.DayResult", null)
                        .WithMany("Meals")
                        .HasForeignKey("DayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("UserProfileService.Domain.Entities.DayResult", b =>
                {
                    b.Navigation("Meals");
                });

            modelBuilder.Entity("UserProfileService.Domain.Entities.Meal", b =>
                {
                    b.Navigation("Foods");
                });

            modelBuilder.Entity("UserProfileService.Domain.Entities.Profile", b =>
                {
                    b.Navigation("DayResults");
                });

            modelBuilder.Entity("UserProfileService.Domain.Entities.Dish", b =>
                {
                    b.Navigation("Ingredients");
                });
#pragma warning restore 612, 618
        }
    }
}
