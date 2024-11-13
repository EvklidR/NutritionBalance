﻿// <auto-generated />
using System;
using MealPlanService.Infrastructure.MSSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MealPlanService.Infrastructure.MSSQL.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241112024717_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MealPlanService.Domain.Entities.MealPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("MealPlans", "MealPlanServiceSchema");
                });

            modelBuilder.Entity("MealPlanService.Domain.Entities.MealPlanDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double>("CaloriePercentage")
                        .HasColumnType("float");

                    b.Property<int>("MealPlanId")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfDay")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MealPlanId", "NumberOfDay")
                        .IsUnique();

                    b.ToTable("MealPlanDays", "MealPlanServiceSchema");
                });

            modelBuilder.Entity("MealPlanService.Domain.Entities.NutrientOfDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CalculationType")
                        .HasColumnType("int");

                    b.Property<int>("MealPlanDayId")
                        .HasColumnType("int");

                    b.Property<int>("NutrientType")
                        .HasColumnType("int");

                    b.Property<double?>("Value")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("MealPlanDayId", "NutrientType")
                        .IsUnique();

                    b.ToTable("NutrientsOfDay", "MealPlanServiceSchema");
                });

            modelBuilder.Entity("MealPlanService.Domain.Entities.MealPlanDay", b =>
                {
                    b.HasOne("MealPlanService.Domain.Entities.MealPlan", null)
                        .WithMany("Days")
                        .HasForeignKey("MealPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MealPlanService.Domain.Entities.NutrientOfDay", b =>
                {
                    b.HasOne("MealPlanService.Domain.Entities.MealPlanDay", null)
                        .WithMany("NutrientOfDay")
                        .HasForeignKey("MealPlanDayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MealPlanService.Domain.Entities.MealPlan", b =>
                {
                    b.Navigation("Days");
                });

            modelBuilder.Entity("MealPlanService.Domain.Entities.MealPlanDay", b =>
                {
                    b.Navigation("NutrientOfDay");
                });
#pragma warning restore 612, 618
        }
    }
}
