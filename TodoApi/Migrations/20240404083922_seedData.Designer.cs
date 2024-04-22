﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TodoModels.DBContext;

#nullable disable

namespace TodoApi.Migrations
{
    [DbContext(typeof(TodoDbContext))]
    [Migration("20240404083922_seedData")]
    partial class seedData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("TodoModels.Models.Book", b =>
                {
                    b.Property<Guid>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BookCover")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("TodoModels.Models.Credential", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("EmailId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique()
                        .HasFilter("[UserId] IS NOT NULL");

                    b.HasIndex("Username", "EmailId")
                        .IsUnique();

                    b.ToTable("Credentials");
                });

            modelBuilder.Entity("TodoModels.Models.FavBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<Guid>("BookId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("UserId");

                    b.ToTable("FavBooks");
                });

            modelBuilder.Entity("TodoModels.Models.PizzaSpecial", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("BasePrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("PizzaSpecials");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BasePrice = 9.99m,
                            Description = "It's cheesy and delicious. Why wouldn't you want one?",
                            ImageUrl = "img/pizzas/cheese.jpg",
                            Name = "Basic Cheese Pizza"
                        },
                        new
                        {
                            Id = 2,
                            BasePrice = 11.99m,
                            Description = "It has EVERY kind of bacon",
                            ImageUrl = "img/pizzas/bacon.jpg",
                            Name = "The Baconatorizor"
                        },
                        new
                        {
                            Id = 3,
                            BasePrice = 10.50m,
                            Description = "It's the pizza you grew up with, but Blazing hot!",
                            ImageUrl = "img/pizzas/pepperoni.jpg",
                            Name = "Classic pepperoni"
                        },
                        new
                        {
                            Id = 4,
                            BasePrice = 12.75m,
                            Description = "Spicy chicken, hot sauce and bleu cheese, guaranteed to warm you up",
                            ImageUrl = "img/pizzas/meaty.jpg",
                            Name = "Buffalo chicken"
                        },
                        new
                        {
                            Id = 5,
                            BasePrice = 11.00m,
                            Description = "It has mushrooms. Isn't that obvious?",
                            ImageUrl = "img/pizzas/mushroom.jpg",
                            Name = "Mushroom Lovers"
                        },
                        new
                        {
                            Id = 7,
                            BasePrice = 11.50m,
                            Description = "It's like salad, but on a pizza",
                            ImageUrl = "img/pizzas/salad.jpg",
                            Name = "Veggie Delight"
                        },
                        new
                        {
                            Id = 8,
                            BasePrice = 9.99m,
                            Description = "Traditional Italian pizza with tomatoes and basil",
                            ImageUrl = "img/pizzas/margherita.jpg",
                            Name = "Margherita"
                        });
                });

            modelBuilder.Entity("TodoModels.Models.Todo", b =>
                {
                    b.Property<Guid>("TodoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("TodoId");

                    b.HasIndex("UserId");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("TodoModels.Models.User", b =>
                {
                    b.Property<Guid>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateOnly?>("DOB")
                        .HasColumnType("date");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Profile")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TodoModels.Models.Credential", b =>
                {
                    b.HasOne("TodoModels.Models.User", "User")
                        .WithOne("Credential")
                        .HasForeignKey("TodoModels.Models.Credential", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TodoModels.Models.FavBook", b =>
                {
                    b.HasOne("TodoModels.Models.Book", "Book")
                        .WithMany("FavBooks")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TodoModels.Models.User", "User")
                        .WithMany("FavBook")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TodoModels.Models.Todo", b =>
                {
                    b.HasOne("TodoModels.Models.User", "User")
                        .WithMany("Todo")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TodoModels.Models.Book", b =>
                {
                    b.Navigation("FavBooks");
                });

            modelBuilder.Entity("TodoModels.Models.User", b =>
                {
                    b.Navigation("Credential");

                    b.Navigation("FavBook");

                    b.Navigation("Todo");
                });
#pragma warning restore 612, 618
        }
    }
}