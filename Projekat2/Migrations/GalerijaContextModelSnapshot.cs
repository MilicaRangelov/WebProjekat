﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Models;

namespace Projekat2.Migrations
{
    [DbContext(typeof(GalerijaContext))]
    partial class GalerijaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.0");

            modelBuilder.Entity("Models.Dostupno", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("GalerijaID")
                        .HasColumnType("int");

                    b.Property<int?>("UmetnikID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("GalerijaID");

                    b.HasIndex("UmetnikID");

                    b.ToTable("GalerijaUmetnici");
                });

            modelBuilder.Entity("Models.Galerija", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Naziv")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Galerije");
                });

            modelBuilder.Entity("Models.Izlozba", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTime>("DatumKraja")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DatumPocetka")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GalerijaID")
                        .HasColumnType("int");

                    b.Property<string>("NazivIzlozbe")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.HasIndex("GalerijaID");

                    b.ToTable("Izlozbe");
                });

            modelBuilder.Entity("Models.Izlozeno", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("IzlozbaID")
                        .HasColumnType("int");

                    b.Property<int?>("UmetnickoDeloID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("IzlozbaID");

                    b.HasIndex("UmetnickoDeloID");

                    b.ToTable("DelaIzlozbe");
                });

            modelBuilder.Entity("Models.Karta", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("ImePosetioca")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int?>("IzlozbaID")
                        .HasColumnType("int");

                    b.Property<string>("PrezimePosetioca")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ID");

                    b.HasIndex("IzlozbaID");

                    b.ToTable("Karte");
                });

            modelBuilder.Entity("Models.UmetnickoDelo", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("GalerijaID")
                        .HasColumnType("int");

                    b.Property<int>("Godina")
                        .HasMaxLength(4)
                        .HasColumnType("int");

                    b.Property<string>("Naslov")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TipDela")
                        .IsRequired()
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<int?>("UmetnikID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("GalerijaID");

                    b.HasIndex("UmetnikID");

                    b.ToTable("UmetnickaDela");
                });

            modelBuilder.Entity("Models.Umetnik", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("DrzavaRodjenja")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Ime")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Prezime")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("UmetnickoIme")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ID");

                    b.ToTable("Umetnici");
                });

            modelBuilder.Entity("Models.Dostupno", b =>
                {
                    b.HasOne("Models.Galerija", "Galerija")
                        .WithMany("GakerijaUmetnici")
                        .HasForeignKey("GalerijaID");

                    b.HasOne("Models.Umetnik", "Umetnik")
                        .WithMany("UmetniciGalerija")
                        .HasForeignKey("UmetnikID");

                    b.Navigation("Galerija");

                    b.Navigation("Umetnik");
                });

            modelBuilder.Entity("Models.Izlozba", b =>
                {
                    b.HasOne("Models.Galerija", "Galerija")
                        .WithMany("Izlozbe")
                        .HasForeignKey("GalerijaID");

                    b.Navigation("Galerija");
                });

            modelBuilder.Entity("Models.Izlozeno", b =>
                {
                    b.HasOne("Models.Izlozba", "Izlozba")
                        .WithMany("IzlozbaDelo")
                        .HasForeignKey("IzlozbaID");

                    b.HasOne("Models.UmetnickoDelo", "UmetnickoDelo")
                        .WithMany("DeloIzlozba")
                        .HasForeignKey("UmetnickoDeloID");

                    b.Navigation("Izlozba");

                    b.Navigation("UmetnickoDelo");
                });

            modelBuilder.Entity("Models.Karta", b =>
                {
                    b.HasOne("Models.Izlozba", "Izlozba")
                        .WithMany("Karte")
                        .HasForeignKey("IzlozbaID");

                    b.Navigation("Izlozba");
                });

            modelBuilder.Entity("Models.UmetnickoDelo", b =>
                {
                    b.HasOne("Models.Galerija", "Galerija")
                        .WithMany("UmetnickaDela")
                        .HasForeignKey("GalerijaID");

                    b.HasOne("Models.Umetnik", "Umetnik")
                        .WithMany("Umetnici")
                        .HasForeignKey("UmetnikID");

                    b.Navigation("Galerija");

                    b.Navigation("Umetnik");
                });

            modelBuilder.Entity("Models.Galerija", b =>
                {
                    b.Navigation("GakerijaUmetnici");

                    b.Navigation("Izlozbe");

                    b.Navigation("UmetnickaDela");
                });

            modelBuilder.Entity("Models.Izlozba", b =>
                {
                    b.Navigation("IzlozbaDelo");

                    b.Navigation("Karte");
                });

            modelBuilder.Entity("Models.UmetnickoDelo", b =>
                {
                    b.Navigation("DeloIzlozba");
                });

            modelBuilder.Entity("Models.Umetnik", b =>
                {
                    b.Navigation("Umetnici");

                    b.Navigation("UmetniciGalerija");
                });
#pragma warning restore 612, 618
        }
    }
}
