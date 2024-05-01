﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MovieSearchApi.Data;

#nullable disable

namespace MovieSearchApi.Migrations
{
    [DbContext(typeof(MovieDbContext))]
    [Migration("20240430171836_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("MovieSearchApi.Model.MoviesDeatils", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Actors")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Awards")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("BoxOffice")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("DVD")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ImdbID")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ImdbRating")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("ImdbVotes")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Language")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Metascore")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Plot")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Poster")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Production")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Rated")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Released")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Runtime")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Website")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Writer")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Year")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("MovieSearchApi.Model.SearchQuery", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Query")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("SearchQueries");
                });
#pragma warning restore 612, 618
        }
    }
}
