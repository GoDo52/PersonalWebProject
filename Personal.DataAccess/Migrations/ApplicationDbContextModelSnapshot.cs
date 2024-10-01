﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Personal.DataAccess.Data;

#nullable disable

namespace Personal.DataAccess.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Personal.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Food"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Convenience"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Medicine"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Restaurant"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Other"
                        });
                });

            modelBuilder.Entity("Personal.Models.Spending", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Spendings", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 10m,
                            CategoryId = 1,
                            DateTime = new DateTime(2024, 9, 30, 20, 49, 7, 446, DateTimeKind.Local).AddTicks(9026),
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            Amount = 19m,
                            CategoryId = 4,
                            DateTime = new DateTime(2024, 9, 30, 20, 49, 7, 446, DateTimeKind.Local).AddTicks(9078),
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            Amount = 5m,
                            CategoryId = 2,
                            DateTime = new DateTime(2024, 9, 30, 20, 49, 7, 446, DateTimeKind.Local).AddTicks(9081),
                            UserId = 1
                        });
                });

            modelBuilder.Entity("Personal.Models.Spending", b =>
                {
                    b.HasOne("Personal.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });
#pragma warning restore 612, 618
        }
    }
}
