﻿// <auto-generated />
using System;
using AstralDelivery.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace AstralDelivery.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.1.1-rtm-30846")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("AstralDelivery.Domain.Entities.DeliveryPoint", b =>
                {
                    b.Property<Guid>("Guid");

                    b.Property<int>("Building");

                    b.Property<string>("City");

                    b.Property<string>("Corpus");

                    b.Property<DateTime>("Date");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<int>("Office");

                    b.Property<string>("Street");

                    b.HasKey("Guid");

                    b.ToTable("DeliveryPoints");
                });

            modelBuilder.Entity("AstralDelivery.Domain.Entities.PasswordRecovery", b =>
                {
                    b.Property<Guid>("Token")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<Guid>("UserGuid");

                    b.HasKey("Token");

                    b.HasIndex("UserGuid");

                    b.ToTable("PasswordRecoveries");
                });

            modelBuilder.Entity("AstralDelivery.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("UserGuid");

                    b.Property<string>("City");

                    b.Property<DateTime>("Date");

                    b.Property<Guid>("DeliveryPointGuid");

                    b.Property<string>("Email");

                    b.Property<bool>("IsActivated");

                    b.Property<bool>("IsDeleted");

                    b.Property<string>("Name");

                    b.Property<string>("Password");

                    b.Property<string>("Patronymic");

                    b.Property<int>("Role");

                    b.Property<string>("Surname");

                    b.HasKey("UserGuid");

                    b.HasIndex("DeliveryPointGuid");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("AstralDelivery.Domain.Entities.WorkTime", b =>
                {
                    b.Property<Guid>("DeliveryPointGuid");

                    b.Property<int>("DayOfWeek");

                    b.Property<TimeSpan>("Begin");

                    b.Property<TimeSpan>("End");

                    b.HasKey("DeliveryPointGuid", "DayOfWeek");

                    b.HasAlternateKey("DayOfWeek", "DeliveryPointGuid");

                    b.ToTable("WorkTimes");
                });

            modelBuilder.Entity("AstralDelivery.Domain.Entities.PasswordRecovery", b =>
                {
                    b.HasOne("AstralDelivery.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserGuid")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AstralDelivery.Domain.Entities.User", b =>
                {
                    b.HasOne("AstralDelivery.Domain.Entities.DeliveryPoint", "DeliveryPoint")
                        .WithMany("Managers")
                        .HasForeignKey("DeliveryPointGuid")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("AstralDelivery.Domain.Entities.WorkTime", b =>
                {
                    b.HasOne("AstralDelivery.Domain.Entities.DeliveryPoint")
                        .WithMany("WorksSchedule")
                        .HasForeignKey("DeliveryPointGuid")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
