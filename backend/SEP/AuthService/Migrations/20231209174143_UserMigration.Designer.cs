﻿// <auto-generated />
using AuthService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AuthService.Migrations
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20231209174143_UserMigration")]
    partial class UserMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AuthService.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "zdravko@gmail.com",
                            Password = "$2a$11$LJD2FpYMIxY8G/WrKvUobeR0yLVktJzaB93JaO3NA/5vxN36BefHC",
                            Type = 0
                        },
                        new
                        {
                            Id = 2,
                            Email = "kurda@gmail.com",
                            Password = "$2a$11$.iaAO0HD4UUKWo5dYKzCgeGSG4pCrnhBdhzC7caQVdSxS7eGIvVnm",
                            Type = 2
                        },
                        new
                        {
                            Id = 3,
                            Email = "malina@gmail.com",
                            Password = "$2a$11$DngOWHvIh42rjiVEw8M5v.94Z9gWm76FgKEghb1bxkIGerRufE3L.",
                            Type = 6
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
