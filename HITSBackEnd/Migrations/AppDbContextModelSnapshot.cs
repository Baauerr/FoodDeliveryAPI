﻿// <auto-generated />
using System;
using HITSBackEnd.DataBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace HITSBackEnd.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("HITSBackEnd.Services.Account.BlackListToken", b =>
                {
                    b.Property<string>("userEmail")
                        .HasColumnType("text")
                        .HasColumnOrder(0);

                    b.Property<string>("Token")
                        .HasColumnType("text")
                        .HasColumnOrder(1);

                    b.HasKey("userEmail", "Token");

                    b.ToTable("BlackListTokens");
                });

            modelBuilder.Entity("HITSBackEnd.Services.Account.UserRepository.Users", b =>
                {
                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<Guid>("Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Email");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("HITSBackEnd.Services.Dishes.DishesRepository.Dish", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<int>("Category")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("IsVegetarian")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Photo")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double>("Price")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("HITSBackEnd.Services.UserCart.Cart", b =>
                {
                    b.Property<string>("UserEmail")
                        .HasColumnType("text");

                    b.Property<int>("AmountOfDish")
                        .HasColumnType("integer");

                    b.Property<string>("DishId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("UserEmail");

                    b.ToTable("Carts");
                });
#pragma warning restore 612, 618
        }
    }
}
