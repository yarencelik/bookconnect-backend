﻿// <auto-generated />
using System;
using BookConnect.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BookConnect.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230530230347_DbInit")]
    partial class DbInit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BookConnect.Domain.Entities.Author", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AuthorName")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("BookConnect.Domain.Entities.Book", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ISBN")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Pages")
                        .HasColumnType("integer");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = new Guid("b4d674b8-5d3f-4b63-b10a-1ac68493d141"),
                            CreatedAt = new DateTime(2023, 5, 30, 23, 3, 47, 475, DateTimeKind.Utc).AddTicks(572),
                            Genre = "SampleGenre",
                            ISBN = "9781234567897",
                            Pages = 300,
                            Title = "SampleTitle",
                            UpdatedAt = new DateTime(2023, 5, 30, 23, 3, 47, 475, DateTimeKind.Utc).AddTicks(574)
                        },
                        new
                        {
                            Id = new Guid("1e246bc2-928a-4631-9a12-276ca8562cd1"),
                            CreatedAt = new DateTime(2023, 5, 30, 23, 3, 47, 475, DateTimeKind.Utc).AddTicks(578),
                            Genre = "SampleGenre2",
                            ISBN = "9781234567444",
                            Pages = 200,
                            Title = "SampleTitle2",
                            UpdatedAt = new DateTime(2023, 5, 30, 23, 3, 47, 475, DateTimeKind.Utc).AddTicks(578)
                        });
                });

            modelBuilder.Entity("BookConnect.Domain.Entities.BookShelf", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("BookId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ShelfId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("ShelfId");

                    b.ToTable("BookShelf");
                });

            modelBuilder.Entity("BookConnect.Domain.Entities.Follow", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("Follower_Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("Following_Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Follower_Id");

                    b.HasIndex("Following_Id");

                    b.ToTable("Follows");
                });

            modelBuilder.Entity("BookConnect.Domain.Entities.Likes", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("Like_Post_Id")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("Like_User_Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Like_Post_Id");

                    b.HasIndex("Like_User_Id");

                    b.ToTable("Likes");
                });

            modelBuilder.Entity("BookConnect.Domain.Entities.Post", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid?>("OwnerId")
                        .IsRequired()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("Review_Post_Id")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("BookConnect.Domain.Entities.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("Book_Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("Rating")
                        .HasColumnType("integer");

                    b.Property<Guid>("Reviewer_Id")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("Book_Id");

                    b.HasIndex("Reviewer_Id");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("BookConnect.Domain.Entities.Shelf", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uuid");

                    b.Property<string>("ShelfName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("OwnerId");

                    b.ToTable("Shelves");
                });

            modelBuilder.Entity("BookConnect.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BookConnect.Domain.Entities.Author", b =>
                {
                    b.HasOne("BookConnect.Domain.Entities.User", "User")
                        .WithOne("Author")
                        .HasForeignKey("BookConnect.Domain.Entities.Author", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BookConnect.Domain.Entities.Book", b =>
                {
                    b.HasOne("BookConnect.Domain.Entities.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Author");
                });

            modelBuilder.Entity("BookConnect.Domain.Entities.BookShelf", b =>
                {
                    b.HasOne("BookConnect.Domain.Entities.Book", "Book")
                        .WithMany("BookShelves")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookConnect.Domain.Entities.Shelf", "Shelf")
                        .WithMany("BookShelves")
                        .HasForeignKey("ShelfId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Shelf");
                });

            modelBuilder.Entity("BookConnect.Domain.Entities.Follow", b =>
                {
                    b.HasOne("BookConnect.Domain.Entities.User", "Following_User")
                        .WithMany("Followers")
                        .HasForeignKey("Follower_Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("BookConnect.Domain.Entities.User", "Follower_User")
                        .WithMany("Followings")
                        .HasForeignKey("Following_Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Follower_User");

                    b.Navigation("Following_User");
                });

            modelBuilder.Entity("BookConnect.Domain.Entities.Likes", b =>
                {
                    b.HasOne("BookConnect.Domain.Entities.Post", "Like_Post")
                        .WithMany("Likes")
                        .HasForeignKey("Like_Post_Id");

                    b.HasOne("BookConnect.Domain.Entities.User", "Like_User")
                        .WithMany("LikedPost")
                        .HasForeignKey("Like_User_Id")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Like_Post");

                    b.Navigation("Like_User");
                });

            modelBuilder.Entity("BookConnect.Domain.Entities.Post", b =>
                {
                    b.HasOne("BookConnect.Domain.Entities.User", "Owner")
                        .WithMany("Posts")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("BookConnect.Domain.Entities.Review", b =>
                {
                    b.HasOne("BookConnect.Domain.Entities.Book", "Book")
                        .WithMany("Reviews")
                        .HasForeignKey("Book_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookConnect.Domain.Entities.User", "Reviewer")
                        .WithMany("BookReviews")
                        .HasForeignKey("Reviewer_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Reviewer");
                });

            modelBuilder.Entity("BookConnect.Domain.Entities.Shelf", b =>
                {
                    b.HasOne("BookConnect.Domain.Entities.User", "Owner")
                        .WithMany("Shelves")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("BookConnect.Domain.Entities.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("BookConnect.Domain.Entities.Book", b =>
                {
                    b.Navigation("BookShelves");

                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("BookConnect.Domain.Entities.Post", b =>
                {
                    b.Navigation("Likes");
                });

            modelBuilder.Entity("BookConnect.Domain.Entities.Shelf", b =>
                {
                    b.Navigation("BookShelves");
                });

            modelBuilder.Entity("BookConnect.Domain.Entities.User", b =>
                {
                    b.Navigation("Author");

                    b.Navigation("BookReviews");

                    b.Navigation("Followers");

                    b.Navigation("Followings");

                    b.Navigation("LikedPost");

                    b.Navigation("Posts");

                    b.Navigation("Shelves");
                });
#pragma warning restore 612, 618
        }
    }
}
