﻿// <auto-generated />
using System;
using ExecuteUpdateDemo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace ExecuteUpdateDemo.Migrations
{
    [DbContext(typeof(DemoDbContext))]
    [Migration("20231014172423_AddPeriodTOCredit")]
    partial class AddPeriodTOCredit
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ExecuteUpdateDemo.Data.Contestation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("CreditId")
                        .HasColumnType("NUMBER(19)");

                    b.Property<string>("CreditReference")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<long?>("DeclarationId")
                        .HasColumnType("NUMBER(19)");

                    b.Property<string>("DeclarationReference")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(450)");

                    b.HasKey("Id");

                    b.HasIndex("Reference")
                        .IsUnique();

                    b.HasIndex("CreditId", "CreditReference")
                        .IsUnique()
                        .HasFilter("\"CreditId\" IS NOT NULL");

                    b.HasIndex("DeclarationId", "DeclarationReference")
                        .IsUnique()
                        .HasFilter("\"DeclarationId\" IS NOT NULL");

                    b.ToTable("Contestations");
                });

            modelBuilder.Entity("ExecuteUpdateDemo.Data.Credit", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<long?>("DeclarationId")
                        .HasColumnType("NUMBER(19)");

                    b.Property<string>("DeclarationReference")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(450)");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(450)");

                    b.HasKey("Id");

                    b.HasIndex("Reference")
                        .IsUnique();

                    b.HasIndex("DeclarationId", "DeclarationReference")
                        .IsUnique()
                        .HasFilter("\"DeclarationId\" IS NOT NULL");

                    b.ToTable("Credits");
                });

            modelBuilder.Entity("ExecuteUpdateDemo.Data.Declaration", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(19)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(450)");

                    b.HasKey("Id");

                    b.HasIndex("Reference")
                        .IsUnique();

                    b.ToTable("Declarations");
                });

            modelBuilder.Entity("ExecuteUpdateDemo.Data.Contestation", b =>
                {
                    b.HasOne("ExecuteUpdateDemo.Data.Credit", "Credit")
                        .WithMany()
                        .HasForeignKey("CreditId");

                    b.HasOne("ExecuteUpdateDemo.Data.Declaration", "Declaration")
                        .WithMany()
                        .HasForeignKey("DeclarationId");

                    b.Navigation("Credit");

                    b.Navigation("Declaration");
                });

            modelBuilder.Entity("ExecuteUpdateDemo.Data.Credit", b =>
                {
                    b.HasOne("ExecuteUpdateDemo.Data.Declaration", "Declaration")
                        .WithMany()
                        .HasForeignKey("DeclarationId");

                    b.OwnsOne("ExecuteUpdateDemo.Data.Period", "Period", b1 =>
                        {
                            b1.Property<long>("CreditId")
                                .HasColumnType("NUMBER(19)");

                            b1.Property<DateTime>("EndDate")
                                .HasColumnType("TIMESTAMP(7)");

                            b1.Property<DateTime>("StartDate")
                                .HasColumnType("TIMESTAMP(7)");

                            b1.HasKey("CreditId");

                            b1.ToTable("Credits");

                            b1.WithOwner()
                                .HasForeignKey("CreditId");
                        });

                    b.Navigation("Declaration");

                    b.Navigation("Period")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
