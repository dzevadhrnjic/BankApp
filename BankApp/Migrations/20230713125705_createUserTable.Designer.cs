﻿// <auto-generated />
using System;
using BankApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BankApp.Migrations
{
    [DbContext(typeof(UserDbContext))]
    [Migration("20230713125705_createUserTable")]
    partial class createUserTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BankApp.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("VerifyEmail")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "sfsff",
                            CreatedAt = new DateTime(2023, 7, 13, 14, 57, 5, 520, DateTimeKind.Local).AddTicks(9684),
                            Email = "Test",
                            FirstName = "Test",
                            LastName = "Test",
                            Password = "Test",
                            PhoneNumber = "Test",
                            VerifyEmail = false
                        },
                        new
                        {
                            Id = 2,
                            Address = "sfsff",
                            CreatedAt = new DateTime(2023, 7, 13, 14, 57, 5, 520, DateTimeKind.Local).AddTicks(9716),
                            Email = "Test",
                            FirstName = "Test2",
                            LastName = "Test2",
                            Password = "Test",
                            PhoneNumber = "Test",
                            VerifyEmail = false
                        },
                        new
                        {
                            Id = 3,
                            Address = "sfsff",
                            CreatedAt = new DateTime(2023, 7, 13, 14, 57, 5, 520, DateTimeKind.Local).AddTicks(9719),
                            Email = "Test",
                            FirstName = "Test3",
                            LastName = "Test3",
                            Password = "Test",
                            PhoneNumber = "Test",
                            VerifyEmail = false
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
