﻿// <auto-generated />
using System;
using CryptographyWebApi.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CryptographyWebApi.Infrastructure.Migrations
{
    [DbContext(typeof(CryptographyContext))]
    [Migration("20250301161001_AddPackage")]
    partial class AddPackage
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CryptographyWebApi.Infrastructure.Entities.PackageDb", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime?>("CompletionDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FilePath")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("SenderID")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("SentDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("UserDbId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SenderID");

                    b.HasIndex("UserDbId");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("CryptographyWebApi.Infrastructure.Entities.UserDb", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Certificate")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Thumbprint")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CryptographyWebApi.Infrastructure.Entities.PackageDb", b =>
                {
                    b.HasOne("CryptographyWebApi.Infrastructure.Entities.UserDb", "Sender")
                        .WithMany()
                        .HasForeignKey("SenderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CryptographyWebApi.Infrastructure.Entities.UserDb", "UserDb")
                        .WithMany("Packages")
                        .HasForeignKey("UserDbId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Sender");

                    b.Navigation("UserDb");
                });

            modelBuilder.Entity("CryptographyWebApi.Infrastructure.Entities.UserDb", b =>
                {
                    b.Navigation("Packages");
                });
#pragma warning restore 612, 618
        }
    }
}
