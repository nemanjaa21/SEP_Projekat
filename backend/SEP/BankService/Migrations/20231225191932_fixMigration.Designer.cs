﻿// <auto-generated />
using System;
using BankService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankService.Migrations
{
    [DbContext(typeof(BankDbContext))]
    [Migration("20231225191932_fixMigration")]
    partial class fixMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BankService.Models.Account", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AccountNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Balance")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("MerchantId")
                        .HasColumnType("int");

                    b.Property<string>("Merchant_Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Reserved")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MerchantId");

                    b.HasIndex("UserId");

                    b.ToTable("Account");
                });

            modelBuilder.Entity("BankService.Models.Card", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AccountId")
                        .HasColumnType("int");

                    b.Property<string>("CardHolderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ExpirationDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Pan")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.ToTable("Card");
                });

            modelBuilder.Entity("BankService.Models.Merchant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ApiKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MerchantId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MerchantPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Merchant");
                });

            modelBuilder.Entity("BankService.Models.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AcquirerAccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("AcquirerOrderId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("AcquirerTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("IdMerchant")
                        .HasColumnType("int");

                    b.Property<int?>("IdUser")
                        .HasColumnType("int");

                    b.Property<string>("IssuerAccountNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("IssuerOrderId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("IssuerTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<long>("MerchantOrderId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("MerchantTimestamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Merchant_Id")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdMerchant");

                    b.HasIndex("IdUser");

                    b.ToTable("Transaction");
                });

            modelBuilder.Entity("BankService.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("BankService.Models.Account", b =>
                {
                    b.HasOne("BankService.Models.Merchant", "Merchant")
                        .WithMany("Accounts")
                        .HasForeignKey("MerchantId");

                    b.HasOne("BankService.Models.User", "User")
                        .WithMany("Accounts")
                        .HasForeignKey("UserId");

                    b.Navigation("Merchant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BankService.Models.Card", b =>
                {
                    b.HasOne("BankService.Models.Account", "Account")
                        .WithMany("Cards")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Account");
                });

            modelBuilder.Entity("BankService.Models.Transaction", b =>
                {
                    b.HasOne("BankService.Models.Merchant", "Merchant")
                        .WithMany("Transactions")
                        .HasForeignKey("IdMerchant")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BankService.Models.User", "User")
                        .WithMany("Transactions")
                        .HasForeignKey("IdUser");

                    b.Navigation("Merchant");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BankService.Models.Account", b =>
                {
                    b.Navigation("Cards");
                });

            modelBuilder.Entity("BankService.Models.Merchant", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("BankService.Models.User", b =>
                {
                    b.Navigation("Accounts");

                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
