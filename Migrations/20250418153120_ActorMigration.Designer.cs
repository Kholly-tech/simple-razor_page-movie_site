﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RazorPagesMovie.Data;

#nullable disable

namespace RazorPagesMovie.Migrations
{
    [DbContext(typeof(RazorPagesMovieContext))]
    [Migration("20250418153120_ActorMigration")]
    partial class ActorMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.4");

            modelBuilder.Entity("RazorPagesMovie.Models.Actor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Actor");
                });

            modelBuilder.Entity("RazorPagesMovie.Models.Cast", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<int>("MovieId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Role")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.ToTable("Cast");
                });

            modelBuilder.Entity("RazorPagesMovie.Models.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<decimal>("BoxOfficeRevenue")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<decimal>("IMDbRating")
                        .HasColumnType("decimal(3,1)");

                    b.Property<decimal>("Price")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReleaseCountry")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ReleaseDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Movie");
                });

            modelBuilder.Entity("RazorPagesMovie.Models.MovieActor", b =>
                {
                    b.Property<int>("MovieId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ActorId")
                        .HasColumnType("INTEGER");

                    b.HasKey("MovieId", "ActorId");

                    b.HasIndex("ActorId");

                    b.ToTable("MovieActor");
                });

            modelBuilder.Entity("RazorPagesMovie.Models.Cast", b =>
                {
                    b.HasOne("RazorPagesMovie.Models.Movie", "Movie")
                        .WithMany("Cast")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("RazorPagesMovie.Models.MovieActor", b =>
                {
                    b.HasOne("RazorPagesMovie.Models.Actor", "Actor")
                        .WithMany("MovieActors")
                        .HasForeignKey("ActorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RazorPagesMovie.Models.Movie", "Movie")
                        .WithMany("MovieActors")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Actor");

                    b.Navigation("Movie");
                });

            modelBuilder.Entity("RazorPagesMovie.Models.Actor", b =>
                {
                    b.Navigation("MovieActors");
                });

            modelBuilder.Entity("RazorPagesMovie.Models.Movie", b =>
                {
                    b.Navigation("Cast");

                    b.Navigation("MovieActors");
                });
#pragma warning restore 612, 618
        }
    }
}
