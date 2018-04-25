﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Rentos.Models;
using System;

namespace Rentos.Migrations
{
    [DbContext(typeof(RentosContext))]
    [Migration("20180423101649_rentpk")]
    partial class rentpk
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Rentos.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUser");
                });

            modelBuilder.Entity("Rentos.Models.Car", b =>
                {
                    b.Property<int>("CarId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Color");

                    b.Property<bool>("IsAvailable");

                    b.Property<string>("Manufacturer");

                    b.Property<int>("Mileage");

                    b.Property<string>("Model");

                    b.Property<string>("Picture");

                    b.Property<int>("Power");

                    b.Property<int>("Price");

                    b.Property<string>("RegistrationNumber");

                    b.HasKey("CarId");

                    b.ToTable("Car");
                });

            modelBuilder.Entity("Rentos.Models.Rent", b =>
                {
                    b.Property<int>("RentId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Car_CarId");

                    b.Property<DateTime>("DropoffDate");

                    b.Property<DateTime>("PickupDate");

                    b.Property<string>("User_UserId");

                    b.HasKey("RentId");

                    b.HasIndex("Car_CarId");

                    b.HasIndex("User_UserId");

                    b.ToTable("Rent");
                });

            modelBuilder.Entity("Rentos.Models.Rent", b =>
                {
                    b.HasOne("Rentos.Models.Car", "Car")
                        .WithMany("Rents")
                        .HasForeignKey("Car_CarId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Rentos.Models.ApplicationUser", "User")
                        .WithMany("Rents")
                        .HasForeignKey("User_UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
