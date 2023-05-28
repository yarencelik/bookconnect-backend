﻿// <auto-generated />
using System;
using App.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace App.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230528010533_DbInit")]
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

            modelBuilder.Entity("App.Domain.Entities.Author", b =>
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

            modelBuilder.Entity("App.Domain.Entities.Book", b =>
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
                            Id = new Guid("2c47fdea-2638-49e4-9f75-6283fe17fb32"),
                            CreatedAt = new DateTime(2023, 5, 28, 1, 5, 33, 456, DateTimeKind.Utc).AddTicks(1300),
                            Genre = "SampleGenre",
                            ISBN = "9781234567897",
                            Pages = 300,
                            Title = "SampleTitle",
                            UpdatedAt = new DateTime(2023, 5, 28, 1, 5, 33, 456, DateTimeKind.Utc).AddTicks(1301)
                        },
                        new
                        {
                            Id = new Guid("607cf571-234e-48ba-b46c-1d62cd804a86"),
                            CreatedAt = new DateTime(2023, 5, 28, 1, 5, 33, 456, DateTimeKind.Utc).AddTicks(1325),
                            Genre = "SampleGenre2",
                            ISBN = "9781234567444",
                            Pages = 200,
                            Title = "SampleTitle2",
                            UpdatedAt = new DateTime(2023, 5, 28, 1, 5, 33, 456, DateTimeKind.Utc).AddTicks(1326)
                        });
                });

            modelBuilder.Entity("App.Domain.Entities.Follow", b =>
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

            modelBuilder.Entity("App.Domain.Entities.Likes", b =>
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

            modelBuilder.Entity("App.Domain.Entities.Post", b =>
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

            modelBuilder.Entity("App.Domain.Entities.Review", b =>
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

            modelBuilder.Entity("App.Domain.Entities.User", b =>
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

                    b.HasData(
                        new
                        {
                            Id = new Guid("d1580a6e-4e9b-4f98-8d4f-de6e73f84af0"),
                            CreatedAt = new DateTime(2023, 5, 28, 1, 5, 33, 63, DateTimeKind.Utc).AddTicks(6318),
                            Email = "admin@admin.com",
                            Password = "$argon2id$v=19$m=65536,t=3,p=4$MFRlbno0YURlLUdWV2R2Z1JWZjk$hHv/O3Uob/FVXCKwvoXfdD1pRVrlm4Y4gM9HnkRgLm4",
                            Role = 0,
                            UpdatedAt = new DateTime(2023, 5, 28, 1, 5, 33, 63, DateTimeKind.Utc).AddTicks(6321),
                            Username = "admin"
                        },
                        new
                        {
                            Id = new Guid("9df75e45-23c9-42c3-8c13-93bb1e2c2049"),
                            CreatedAt = new DateTime(2023, 5, 28, 1, 5, 33, 456, DateTimeKind.Utc).AddTicks(600),
                            Email = "reader@reader.com",
                            Password = "$argon2id$v=19$m=65536,t=3,p=4$MFRlbno0YURlLUdWV2R2Z1JWZjk$L9ahKZ719vSNb9qGr2vMdi2WrUcBmdmf5YKFRzsn6c8",
                            Role = 2,
                            UpdatedAt = new DateTime(2023, 5, 28, 1, 5, 33, 456, DateTimeKind.Utc).AddTicks(603),
                            Username = "reader"
                        });
                });

            modelBuilder.Entity("App.Domain.Entities.Author", b =>
                {
                    b.HasOne("App.Domain.Entities.User", "User")
                        .WithOne("Author")
                        .HasForeignKey("App.Domain.Entities.Author", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("App.Domain.Entities.Book", b =>
                {
                    b.HasOne("App.Domain.Entities.Author", "Author")
                        .WithMany("Books")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Author");
                });

            modelBuilder.Entity("App.Domain.Entities.Follow", b =>
                {
                    b.HasOne("App.Domain.Entities.User", "Following_User")
                        .WithMany("Followers")
                        .HasForeignKey("Follower_Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("App.Domain.Entities.User", "Follower_User")
                        .WithMany("Followings")
                        .HasForeignKey("Following_Id")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Follower_User");

                    b.Navigation("Following_User");
                });

            modelBuilder.Entity("App.Domain.Entities.Likes", b =>
                {
                    b.HasOne("App.Domain.Entities.Post", "Like_Post")
                        .WithMany("Likes")
                        .HasForeignKey("Like_Post_Id");

                    b.HasOne("App.Domain.Entities.User", "Like_User")
                        .WithMany("LikedPost")
                        .HasForeignKey("Like_User_Id")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.Navigation("Like_Post");

                    b.Navigation("Like_User");
                });

            modelBuilder.Entity("App.Domain.Entities.Post", b =>
                {
                    b.HasOne("App.Domain.Entities.User", "Owner")
                        .WithMany("Posts")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("App.Domain.Entities.Review", b =>
                {
                    b.HasOne("App.Domain.Entities.Book", "Book")
                        .WithMany("Reviews")
                        .HasForeignKey("Book_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("App.Domain.Entities.User", "Reviewer")
                        .WithMany("BookReviews")
                        .HasForeignKey("Reviewer_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Reviewer");
                });

            modelBuilder.Entity("App.Domain.Entities.Author", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("App.Domain.Entities.Book", b =>
                {
                    b.Navigation("Reviews");
                });

            modelBuilder.Entity("App.Domain.Entities.Post", b =>
                {
                    b.Navigation("Likes");
                });

            modelBuilder.Entity("App.Domain.Entities.User", b =>
                {
                    b.Navigation("Author");

                    b.Navigation("BookReviews");

                    b.Navigation("Followers");

                    b.Navigation("Followings");

                    b.Navigation("LikedPost");

                    b.Navigation("Posts");
                });
#pragma warning restore 612, 618
        }
    }
}
