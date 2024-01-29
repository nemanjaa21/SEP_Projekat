﻿// <auto-generated />
using AgencyService.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AgencyService.Migrations
{
    [DbContext(typeof(AgencyServiceDBContext))]
    partial class AgencyServiceDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.25")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("AgencyService.Models.Agency", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Agencies");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Agency"
                        });
                });

            modelBuilder.Entity("AgencyService.Models.PaymentService", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AgencyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Subscribed")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("AgencyId");

                    b.ToTable("PaymentServices");
                });

            modelBuilder.Entity("AgencyService.Models.ServiceOffer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("AgencyService.Models.ServiceOfferItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("AgencyId")
                        .HasColumnType("int");

                    b.Property<bool>("IsAccepted")
                        .HasColumnType("bit");

                    b.Property<double>("MonthlyPrice")
                        .HasColumnType("float");

                    b.Property<string>("OfferName")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("CODIFICATION_OF_LAWS");

                    b.Property<double>("SelectedPrice")
                        .HasColumnType("float");

                    b.Property<double>("YearlyPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("AgencyId");

                    b.ToTable("OfferItems");
                });

            modelBuilder.Entity("ServiceOfferServiceOfferItem", b =>
                {
                    b.Property<int>("ServiceOfferItemsId")
                        .HasColumnType("int");

                    b.Property<int>("ServiceOffersId")
                        .HasColumnType("int");

                    b.HasKey("ServiceOfferItemsId", "ServiceOffersId");

                    b.HasIndex("ServiceOffersId");

                    b.ToTable("ServiceOfferServiceOfferItem");
                });

            modelBuilder.Entity("AgencyService.Models.PaymentService", b =>
                {
                    b.HasOne("AgencyService.Models.Agency", "Agency")
                        .WithMany("PaymentServices")
                        .HasForeignKey("AgencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agency");
                });

            modelBuilder.Entity("AgencyService.Models.ServiceOfferItem", b =>
                {
                    b.HasOne("AgencyService.Models.Agency", "Agency")
                        .WithMany("ServiceOfferItems")
                        .HasForeignKey("AgencyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Agency");
                });

            modelBuilder.Entity("ServiceOfferServiceOfferItem", b =>
                {
                    b.HasOne("AgencyService.Models.ServiceOfferItem", null)
                        .WithMany()
                        .HasForeignKey("ServiceOfferItemsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AgencyService.Models.ServiceOffer", null)
                        .WithMany()
                        .HasForeignKey("ServiceOffersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AgencyService.Models.Agency", b =>
                {
                    b.Navigation("PaymentServices");

                    b.Navigation("ServiceOfferItems");
                });
#pragma warning restore 612, 618
        }
    }
}
