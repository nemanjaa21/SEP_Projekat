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
    [Migration("20231118191109_UserMigration")]
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

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "zdravko@gmail.com",
                            Password = "$2a$11$2MFjwguj8fLG2Dw3v5HmM.r05oSzQYgMJw1BcSTysMiAQKgn6kVL6"
                        },
                        new
                        {
                            Id = 2,
                            Email = "kurda@gmail.com",
                            Password = "$2a$11$eRu4eKxGHCb9u6j9Z7Uf8uPx5/mHOtLq2GqocM9BCOXeO8RABOfUq"
                        },
                        new
                        {
                            Id = 3,
                            Email = "malina@gmail.com",
                            Password = "$2a$11$UE5tVYhZwEvwCRmcUjJNq.faVqhhACavdts5lONtHgHAQgrGLcFU6"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
