﻿// <auto-generated />
using DT191G_moment4.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DT191G_moment4.Migrations.Rating
{
    [DbContext(typeof(RatingContext))]
    partial class RatingContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.3");

            modelBuilder.Entity("DT191G_moment4.Models.Rating", b =>
                {
                    b.Property<int>("RatingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("SongId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SongRating")
                        .HasColumnType("INTEGER");

                    b.HasKey("RatingId");

                    b.ToTable("Ratings");
                });
#pragma warning restore 612, 618
        }
    }
}
