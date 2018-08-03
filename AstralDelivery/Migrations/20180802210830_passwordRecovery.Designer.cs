﻿// <auto-generated />
using System;
using AstralDelivery.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AstralDelivery.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20180802210830_passwordRecovery")]
    partial class passwordRecovery
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("AstralDelivery.Domain.Entities.PasswordRecovery", b =>
                {
                    b.Property<Guid>("Token")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<Guid>("UserGuid");

                    b.HasKey("Token");

                    b.HasIndex("UserGuid");

                    b.ToTable("passwordRecoveries");
                });

            modelBuilder.Entity("AstralDelivery.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("UserGuid");

                    b.Property<string>("Email");

                    b.Property<string>("Password");

                    b.Property<int>("Role");

                    b.HasKey("UserGuid");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AstralDelivery.Domain.Entities.PasswordRecovery", b =>
                {
                    b.HasOne("AstralDelivery.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserGuid")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
